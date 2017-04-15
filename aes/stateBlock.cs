using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityEx2
{
    class stateBlock
    {

         public byte[,] _data;
        public stateBlock(byte[] single)
        {
            _data = new byte[4, 4];
            initilizedData(single);
        }
        public stateBlock(stateBlock other)
        {
            _data = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _data[i, j] = other._data[i, j];
                }                                                          
            }
        }

        private void initilizedData(byte[] single)
        {
            int directorySingle = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _data[j, i] = single[directorySingle];
                    directorySingle++;
                }
            }
        }



    }
}
