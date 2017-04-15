using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecurityEx2
{
    class Program
    {

        static void Main(string[] args)
        {
            eunIt(args);
            // if (testUnit1() > 0)Console.WriteLine("testUnit1 Faild");
            // if (testUnit2() > 0)Console.WriteLine("testUnit2 Faild");
            // if (testUnit3() > 0)Console.WriteLine("testUnit3 Faild");
            // if (testUnit4() > 0)Console.WriteLine("testUnit4 Faild");
            // if (testUnit5() > 0)Console.WriteLine("testUnit5 Faild");
            // if (testUnit6() > 0)Console.WriteLine("testUnit6 Faild");
            // if (testUnit7() > 0)Console.WriteLine("testUnit7 Faild");
            // if (testUnit8() > 0)Console.WriteLine("testUnit8 Faild");
            // if (testUnit9() > 0) Console.WriteLine("testUnit9 Faild");
            //if (testUnit10() > 0) Console.WriteLine("testUnit10 Faild");

        }
        private static void eunIt(string[] args)
        {

            AES _aes = new AES();
            HackTool hackTool = new HackTool();

            try
            {
                string commends = "";
                foreach (string item in args)
                {
                    commends += item + " ";
                }

                if (args[0] == "-t")
                {
                    TestCompare(args[1], args[2]);
                }
                else
                {
                    Match r_algo = Regex.Match(commends, @"a\s(AES1\s|AES3\s)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match r_action_fleg = Regex.Match(commends, @"(-e|-d|-b)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match r_key = Regex.Match(commends, @"-k\s(\S)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match r_input = Regex.Match(commends, @"-i\s(\S)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match r_output = Regex.Match(commends, @"-o\s(\S)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match r_messege = Regex.Match(commends, @"-m\s(\S)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match r_cipher = Regex.Match(commends, @"-c\s(\S)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    string algo = Regex.Replace(r_algo.ToString(), @"a\s", "");
                    algo = Regex.Replace(algo.ToString(), @" ", "");
                    string action_fleg = Regex.Replace(r_action_fleg.ToString(), @"\s", "");
                    string key = Regex.Replace(r_key.ToString(), @"-k\s", "");
                    string input = Regex.Replace(r_input.ToString(), @"-i\s", "");
                    string output = Regex.Replace(r_output.ToString(), @"-o\s", "");
                    string messege = Regex.Replace(r_messege.ToString(), @"-m\s", "");
                    string cipher = Regex.Replace(r_cipher.ToString(), @"-c\s", "");
                     
                    key = Directory.GetCurrentDirectory() +"\\"+ key;
                    input = Directory.GetCurrentDirectory() + "\\" + input;
                    output = Directory.GetCurrentDirectory() + "\\" + output;
                    messege = Directory.GetCurrentDirectory() + "\\" + messege;
                    cipher = Directory.GetCurrentDirectory() + "\\" + cipher;
                      

                    if (r_action_fleg.ToString() == "-e")
                        _aes.Encrypte(input, output, key, algo);
                    else if (r_action_fleg.ToString() == "-d")
                        _aes.Decrypte(input, output, key, algo);
                    else if (r_action_fleg.ToString() == "-b")
                    {

                        if (algo == "AES1")
                            hackTool.HackProcAes1(messege, cipher, output);
                        else
                            hackTool.HackProcAes3(messege, cipher, output);
                    }
                }




            }
            catch (Exception ex)
            {
                Console.WriteLine();

                Console.WriteLine("Illegal Argument Exception  : Command Dosent enter in correct Sequance");

                Console.WriteLine();

            }



        }

        private static void TestCompare(string p1, string p2)
        {

            p1 = Directory.GetCurrentDirectory() + p1;
            p2 = Directory.GetCurrentDirectory() + p2;
            List<stateBlock> surce = createDataState(createByteArr(p1));
            List<stateBlock> relese = createDataState(createByteArr(p2));


            int counter = 0;

            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            //Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }

            if (counter == 0)
                Console.WriteLine("Test Success");
            else
                Console.WriteLine("Faild");


        }
        private static int testUnit8()
        {
            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\key_AES1";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\1.jpg";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\NewChiper";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\message1.jpg";

            a.Encrypte(fileName, outpot, Key, "AES1");
            a.Decrypte(outpot, outpot2, Key, "AES1");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));

            int counter = 0;
            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            // Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }

            Console.WriteLine("AES1-longMessage  Encrypte + Decrypte image");
            return counter;
        }
        private static int testUnit7()
        {
            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\key_AES1";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\1.jpg";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\NewChiper";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\message1.jpg";

            a.Encrypte(fileName, outpot, Key, "AES1");
            a.Decrypte(outpot, outpot2, Key, "AES1");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));

            int counter = 0;
            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            //  Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }

            Console.WriteLine("AES1-shotMessage Encrypte + Decrypte image");
            return counter;
        }
        private static int testUnit6()
        {
            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\key_AES_3";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\1.jpg";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\NewChiper";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\message2.jpg";
            string chiper = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\cypher_AES_3";


            a.Encrypte(fileName, outpot, Key, "AES3");
            a.Decrypte(outpot, outpot2, Key, "AES3");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));






            int counter = 0;

            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            //   Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }


            Console.WriteLine("AES_3longMessage Encrypte + Decrypte");
            return counter;
        }
        private static int testUnit5()
        {

            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\key_AES_3";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\1.jpg";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\NewChiper";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\message2.jpg";
            string chiper = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\cypher_AES_3";


            a.Encrypte(fileName, outpot, Key, "AES3");
            a.Decrypte(outpot, outpot2, Key, "AES3");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));






            int counter = 0;

            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            // Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }

            Console.WriteLine("AES1-shotMessage Encrypte + Decrypte");
            return counter;
        }
        private static int testUnit1()
        {
            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\key_AES1";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\message";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\NewChiper.jpg";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES1-shotMessage\message2";

            a.Encrypte(fileName, outpot, Key, "AES1");
            a.Decrypte(outpot, outpot2, Key, "AES1");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));

            int counter = 0;
            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            //Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }
            Console.WriteLine("AES1-shotMessage Encrypte + Decrypte");
            return counter;

        }
        private static int testUnit2()
        {
            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\key_AES1";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\message";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\NewChiper.jpg";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\message2";

            a.Encrypte(fileName, outpot, Key, "AES1");
            a.Decrypte(outpot, outpot2, Key, "AES1");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));

            int counter = 0;
            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            //  Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }

            Console.WriteLine("AES1-longMessage Encrypte + Decrypte");
            return counter;
        }
        private static int testUnit4()
        {
            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\key_AES_3";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\message";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\NewChiper";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\message2";
            string chiper = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\cypher_AES_3";


            a.Encrypte(fileName, outpot, Key, "AES3");
            a.Decrypte(outpot, outpot2, Key, "AES3");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));






            int counter = 0;

            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            //Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }


            Console.WriteLine("AES_3longMessage Encrypte + Decrypte");
            return counter;

        }
        private static int testUnit3()
        {
            AES a = new AES();

            string Key = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\key_AES_3";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\message";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\NewChiper";
            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\message2";
            string chiper = @"C:\Users\naor\Desktop\outputs\AES_3-shortMessage\cypher_AES_3";


            a.Encrypte(fileName, outpot, Key, "AES3");
            a.Decrypte(outpot, outpot2, Key, "AES3");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));






            int counter = 0;

            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            //Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }


            Console.WriteLine("AES_3-shortMessage Encrypte + Decrypte");
            return counter;

        }

        //Hacking TOOL Tests
        private static int testUnit9()
        {
            AES a = new AES();
            HackTool h = new HackTool();

            string Key = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\key_AES_3";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\1.jpg";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\NewChiper";




            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\message2.jpg";
            string chiper = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\NewChiper";
            string outpotKey = @"C:\Users\naor\Desktop\outputs\AES_3longMessage\outpotKey";


            a.Encrypte(fileName, outpot, Key, "AES3");

            h.HackProcAes3(fileName, chiper, outpotKey);

            a.Decrypte(outpot, outpot2, outpotKey, "AES3");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));


            int counter = 0;

            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            Console.WriteLine("Faild");
                            counter++;
                        }
                    }
                }
            }


            Console.WriteLine("AES_3longMessage Encrypte + Decrypte +hACKING TOOL");
            return counter;
        }


        private static int testUnit10()
        {
            AES a = new AES();
            HackTool h = new HackTool();

            string Key = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\key_AES1";
            string fileName = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\1.jpg";
            string outpot = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\NewChiper";

            string outpot2 = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\message2.jpg";
            string chiper = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\NewChiper";
            string outpotKey = @"C:\Users\naor\Desktop\outputs\AES1-longMessage\outpotKey";


            a.Encrypte(fileName, outpot, Key, "AES1");

            h.HackProcAes1(fileName, chiper, outpotKey);

            a.Decrypte(outpot, outpot2, outpotKey, "AES1");


            List<stateBlock> surce = createDataState(createByteArr(fileName));
            List<stateBlock> relese = createDataState(createByteArr(outpot2));


            int counter = 0;

            for (int i = 0; i < surce.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (surce[i]._data[j, z] != relese[i]._data[j, z])
                        {
                            Console.WriteLine("BLOCK " + i + "Faild" + "(" + j + " ," + z + ")");
                            //Console.Write(i);
                            counter++;
                        }
                    }
                }
            }
            Console.WriteLine("totalCounterEror: " + counter);

            Console.WriteLine("AES_3longMessage Encrypte + Decrypte +hACKING TOOL");
            return counter;
        }


        static byte[] createByteArr(string Path)
        {
            //check if exist? size>0 , try catch.

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




        private static void runIt(string[] args)
        {
            AES aes = new AES();
            if (args[2] == "-e")
                aes.Encrypte(args[6], args[8], args[4], args[2]);
            else if (args[2] == "-d")
                aes.Encrypte(args[6], args[8], args[4], args[2]);
            else if (args[2] == "-b")
            {
                HackTool hakingTool = new HackTool();

                if (args[1] == "AES1")
                    hakingTool.HackProcAes1(args[6], args[8], args[4]);
                else
                    hakingTool.HackProcAes3(args[6], args[8], args[4]);


            }



        }





    }
}
