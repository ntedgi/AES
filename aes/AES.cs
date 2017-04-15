using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityEx2
{
    class AES
    {
        public AESDecrypte de;
        public AESEncrypte en;
        public AES()
        {
            de = new AESDecrypte();
            en = new AESEncrypte();
        }
        public void Encrypte(string input, string outpot, string key, string alg)
        {

            List<stateBlock> _keys = createDataState(createByteArr(key));
            List<stateBlock> _temp = createDataState(createByteArr(input));

            if (alg == "AES3")
            {

                foreach (stateBlock item in _keys)
                {
                    _temp = en.addRoundKey(en.shiftRow(_temp), item);
                }

            }
            else if (alg == "AES1")
            {
                foreach (stateBlock item in _keys)
                {
                    _temp = en.addRoundKey(en.mixColumns(en.shiftRow(en.subByte(_temp))), item);

                }
            }
            else
            {
                Console.WriteLine("Bad input");
                throw new Exception("bad input");
            }
            byte[] curr = Build(_temp);
            Console.WriteLine();
            File.WriteAllBytes(outpot, curr);

        }
        private static byte[] Build(List<stateBlock> _temp)
        {
            byte[] temp = new byte[_temp.Count * 16];
            int counter = 0;
            for (int x = 0; x < _temp.Count; x++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp[counter] = _temp[x]._data[j, i];
                        counter++;
                    }
                }
            }

            return temp;
        }
        public void Decrypte(string input, string outpot, string key, string alg)
        {

            List<stateBlock> _keys = createDataState(createByteArr(key));
            List<stateBlock> _temp = createDataState(createByteArr(input));


            if (alg == "AES3")
            {

                for (int i = _keys.Count-1; i >= 0; i--)
                {
                    _temp = de.revShiftRow(en.addRoundKey(_temp, _keys[i]));

                }
                

            }
            else if (alg == "AES1")
            {
                foreach (stateBlock item in _keys)
                {

                    _temp = de.revSubByte(de.revShiftRow(de.revMixColumns(en.addRoundKey(_temp, item))));
                }
            }
            else
            {
                Console.WriteLine("Bad input");
                throw new Exception("bad input");
            }

            byte[] curr = Build(_temp);

            File.WriteAllBytes(outpot, curr);

        }
        static byte[] createByteArr(string Path)
        {

            byte[] bytesData = System.IO.File.ReadAllBytes(Path);
            return bytesData;
        }
        static List<stateBlock> createDataState(byte[] data)
        {
            List<stateBlock> dataParts = new List<stateBlock>();
            byte[] tempPart = new byte[16];
            for (int i = 0; i < data.Length; i++)
            {
                if (i % 16 == 0 & i > 0)
                {
                    dataParts.Add(new stateBlock(tempPart));
                    tempPart = new byte[16];
                }

               
                tempPart[i % 16] = data[i];


            }
            dataParts.Add(new stateBlock(tempPart));

            return dataParts;

        }

    }
}
