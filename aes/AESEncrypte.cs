using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityEx2
{
    class AESEncrypte
    {
        RijndaelTables _Rijndael;

        #region encryption_steps
        public AESEncrypte()
        {
            _Rijndael = new RijndaelTables();
        }
        public List<stateBlock> subByte(List<stateBlock> _datas)
        {
            foreach (stateBlock item in _datas)
            {
                _Rijndael.subByte(item);

            }

            return _datas;
        }
        public List<stateBlock> shiftRow(List<stateBlock> _datas)
        {
            foreach (stateBlock item in _datas)
            {
                int i, j;
                int count;
                byte t;
                for (i = 1; i < 4; i++)
                    for (count = 0; count < i; count++)
                    {
                        t = item._data[i, 0];
                        for (j = 0; j < 3; j++)
                            item._data[i, j] = item._data[i, j + 1];
                        item._data[i, j] = t;
                    }
            }
            return _datas;

        }
        public List<stateBlock> mixColumns(List<stateBlock> _datas)
        {
            List<stateBlock> result = new List<stateBlock>();

            foreach (stateBlock item in _datas)
            {
                stateBlock T = new stateBlock(item);
                for (int c = 0; c < 4; c++)
                {
                    T._data[0, c] = (Byte)(_Rijndael.mixCol(0x02, item._data[0, c]) ^ _Rijndael.mixCol(0x03, item._data[1, c]) ^ item._data[2, c] ^ item._data[3, c]);
                    T._data[1, c] = (Byte)(item._data[0, c] ^ _Rijndael.mixCol(0x02, item._data[1, c]) ^ _Rijndael.mixCol(0x03, item._data[2, c]) ^ item._data[3, c]);
                    T._data[2, c] = (Byte)(item._data[0, c] ^ item._data[1, c] ^ _Rijndael.mixCol(0x02, item._data[2, c]) ^ _Rijndael.mixCol(0x03, item._data[3, c]));
                    T._data[3, c] = (Byte)(_Rijndael.mixCol(0x03, item._data[0, c]) ^ item._data[1, c] ^ item._data[2, c] ^ _Rijndael.mixCol(0x02, item._data[3, c]));
                }
                result.Add(T);
            }
            return result;

        }
        public List<stateBlock> addRoundKey(List<stateBlock> _datas, stateBlock Key)
        {

            foreach (stateBlock item in _datas)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        item._data[i, j] ^= Key._data[i, j];

            }
            return _datas;

        }

        #endregion
    }
}
