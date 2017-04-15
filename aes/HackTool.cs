using SecurityEx2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityEx2
{
    public class HackTool
    {

        public HackTool() { }


        public void HackProcAes3(string fileName, string chiper, string output)
        {
            Stopwatch stopwatch = new Stopwatch();

            List<stateBlock> _temp = createDataState(createByteArr(fileName));

            List<stateBlock> testChiper = createDataState(createByteArr(chiper));

            byte[] Key = new byte[16];

            stateBlock Null_State = new stateBlock(Key);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Null_State._data[i, j] = (byte)(255);
                }
            }

            stateBlock theKey = new stateBlock(Key);

            int[] res;
            for (int i = 0; i < 4; i++)
            {
                res = noMoveChiper(_temp[0]._data[0, i], testChiper[0]._data[0, i]);

               

                theKey._data[0, i] = (byte)res[2];
            }



            res = noMoveChiper(_temp[0]._data[1, 0], testChiper[0]._data[1, 1]);

           
            theKey._data[1, 1] = (byte)res[2];



            res = noMoveChiper(_temp[0]._data[1, 1], testChiper[0]._data[1, 2]);

            
            theKey._data[1, 2] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[1, 2], testChiper[0]._data[1, 3]);

          

            theKey._data[1, 3] = (byte)res[2];

            res = noMoveChiper(_temp[0]._data[1, 3], testChiper[0]._data[1, 0]);

           
            theKey._data[1, 0] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[2, 0], testChiper[0]._data[2, 2]);

          
            theKey._data[2, 2] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[2, 1], testChiper[0]._data[2, 3]);

         
            theKey._data[2, 3] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[2, 2], testChiper[0]._data[2, 0]);

          
            theKey._data[2, 0] = (byte)res[2];



            res = noMoveChiper(_temp[0]._data[2, 3], testChiper[0]._data[2, 1]);

           
            theKey._data[2, 1] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[3, 0], testChiper[0]._data[3, 3]);

           
            theKey._data[3, 3] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[3, 1], testChiper[0]._data[3, 0]);

            
            theKey._data[3, 0] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[3, 2], testChiper[0]._data[3, 1]);

            
            theKey._data[3, 1] = (byte)res[2];


            res = noMoveChiper(_temp[0]._data[3, 3], testChiper[0]._data[3, 2]);

           
            theKey._data[3, 2] = (byte)res[2];


            List<stateBlock> Pk = new List<stateBlock>();


            
            Pk.Add(Null_State);
            Pk.Add(Null_State);
            Pk.Add(theKey);


            byte[] ToWrite = BuildKey(Pk);

            File.WriteAllBytes(output, ToWrite);









        }


        public void HackProcAes1(string fileName, string chiper, string output)
        {
            AESEncrypte aesDyc = new AESEncrypte();
            List<stateBlock> _temp = createDataState(createByteArr(fileName));

            List<stateBlock> testChiper = createDataState(createByteArr(chiper));

            Stopwatch stopwatch = new Stopwatch();

            _temp = aesDyc.mixColumns(aesDyc.shiftRow(aesDyc.subByte(_temp)));

            stopwatch.Start();

            byte[] block = new byte[16];

            List<stateBlock> TheKey = new List<stateBlock>();

            for (int countState = 0; countState < 1; countState++)
            {

                stateBlock s = new stateBlock(block);

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        s._data[i, j] = (byte)(testChiper[countState]._data[i, j] ^ _temp[countState]._data[i, j]);
                    }
                }

                TheKey.Add(s);

            }


            stopwatch.Stop();
            Console.WriteLine("Total Time to Hacking: " + stopwatch.Elapsed.ToString());

            byte[] curr = BuildKey(TheKey);

            File.WriteAllBytes(output, curr);


        }

        public int[] noMoveChiper(byte surce, byte Dest)
        {
            int[] result = new int[3];

          /*
            for (int keyX = 0; keyX < 256; keyX++)
            {
                for (int keyY = 0; keyY < 256; keyY++)
                {
           */
                    for (int keyZ = 0; keyZ < 256; keyZ++)
                    {
                        byte sol = (byte)(((surce ^ (byte)255) ^ (byte)255) ^ (byte)keyZ);
                        if (Dest == sol)
                        {
                            result[0] = 255;
                            result[1] = 255;
                            result[2] = keyZ;
                        }
                    }
            /*
                }
            }
             */

            return result;

        }

        private static byte[] BuildKey(List<stateBlock> _temp)
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
