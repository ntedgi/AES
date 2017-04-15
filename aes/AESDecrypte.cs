using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityEx2
{
    class AESDecrypte
    {
        RijndaelTables _Rijndael;

        public AESDecrypte()
        {
            _Rijndael = new RijndaelTables();
        }
        #region decryption steps
        public List<stateBlock> revSubByte(List<stateBlock> _data)
        {
            foreach (stateBlock item in _data)
            {
                _Rijndael.subLocationRev(item);
            }

            return _data;


        }
        public List<stateBlock> revShiftRow(List<stateBlock> _datas)
        {

            foreach (stateBlock item in _datas)
            {
                int i, j;
                int t, count;
                for (i = 1; i < 4; i++)
                    for (count = 0; count < i; count++)
                    {
                        t = item._data[i, 3];
                        for (j = 3; j > 0; j--)
                            item._data[i, j] = item._data[i, j - 1];
                        item._data[i, j] = Convert.ToByte(t);
                    }
            }
            return _datas;
        }
        public List<stateBlock> revMixColumns(List<stateBlock> _datas)
        {

            List<stateBlock> result = new List<stateBlock>();
            foreach (stateBlock item in _datas)
            {
                stateBlock T = new stateBlock(item);

                for (int c = 0; c < 4; c++)
                {
                    T._data[0, c] = (Byte)(_Rijndael.mixCol(14, item._data[0, c]) ^ _Rijndael.mixCol(11, item._data[1, c]) ^ _Rijndael.mixCol(13, item._data[2, c]) ^ _Rijndael.mixCol(9, item._data[3, c]));
                    T._data[1, c] = (Byte)(_Rijndael.mixCol(9, item._data[0, c]) ^ _Rijndael.mixCol(14, item._data[1, c]) ^ _Rijndael.mixCol( 11, item._data[2, c]) ^ _Rijndael.mixCol(13, item._data[3, c]));
                    T._data[2, c] = (Byte)(_Rijndael.mixCol(13, item._data[0, c]) ^ _Rijndael.mixCol(9, item._data[1, c]) ^ _Rijndael.mixCol( 14, item._data[2, c]) ^ _Rijndael.mixCol(11, item._data[3, c]));
                    T._data[3, c] = (Byte)(_Rijndael.mixCol(11, item._data[0, c]) ^ _Rijndael.mixCol(13, item._data[1, c]) ^ _Rijndael.mixCol(9, item._data[2, c]) ^ _Rijndael.mixCol(14, item._data[3, c]));
                }
                result.Add(T);
            }
            return result;

         
        }
        public List<stateBlock> revAddRoundKey(List<stateBlock> _datas, stateBlock Key)
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
