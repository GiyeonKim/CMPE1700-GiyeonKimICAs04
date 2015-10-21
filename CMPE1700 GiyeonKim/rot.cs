using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPE1700_GiyeonKim
{
    class Program
    {
        static void Main(string[] args)
        {
           bool decrypt = false;  //Am I encrypting or decrypting?
            uint n = 0; //What is the amount I am (de)rotating?
            bool fileIO = false; //Am I encrypting/decrypting a file?
            string text = ""; //What is the string I am [en|de]crypting?
            string fileName = ""; //What is the file I am [en|de]crypting?
            string Words;
            int Value=0;
            
            //Check args
            
            //Confirm I have between 3 and 4 args
            if (args.Count() < 3 || args.Count() > 4)
                PrintError("Invalid number of arguments (" + args.Count() + ")", "Expected either 3 or 4 arguments.", true, true, -1);

            //First arg should be n
            try
            {
                n = uint.Parse(args[0]);
            }
            catch (Exception e)
            {
                PrintError("Invalid rotation number (" + args[0] + ").", e.Message, true, true, -2);
            }

            //Next arg should be either -e or -d
            switch (args[1])
            {
                case "-e": decrypt = false; break;
                case "-d": decrypt = true; break;
                default:
                    PrintError("Unexpected encryption/decryption flag (" + args[1] + ").", "Expected -e or -d.", false, true, -3);
                    break;
            }

            //Next arg should be either -f or the string to (de)rotate.
            if (args[2] == "-f")
            {
                if (args.Count() < 4)
                    PrintError("Expected filename after -f argument.", "", false, true, -4);
                fileIO = true;
                fileName = args[3];
            }
            else
            {
                if (args.Count() > 3)
                    PrintError("Unexpected arguments after string, starting with \"" + args[3] + "\". Perhaps you need to put the string in quotes?"
                        , "", false, true, -5);
                fileIO = false;
                text = args[2];
            }

            //REPLACE THIS SECTION WITH YOUR OWN CODE.  YOU'LL WANT A FEW ADDITIONAL METHODS, TOO.
            // if the decrypt is false, it print out as encryption
            // However if is a true it prints out as decryption
            if (decrypt == false)
            {
                 Value= Convert.ToInt32(n);
            }
            else
            {
                Value = Convert.ToInt32((n) * -1);
            }
           
            if (!fileIO)
            {
                Console.WriteLine("Here is where I would " + (decrypt ? "decrypt" : "encrypt") +
                    " the string \"" + text + "\" using rot " + n + ".");
                Console.WriteLine();
                string[] CDing = new string[text.Length];
                CDing = Test(text, Value);
                for (int i = 0; i < text.Length; i++)
                {
                    Console.Write(CDing[i]);
                }
                Console.WriteLine();
               
            }
            else
            {
                string FindFile = fileName;
                Console.WriteLine();
                Console.WriteLine("Here is where I would " + (decrypt ? "decrypt" : "encrypt") +
                " the file \"" + fileName + "\" using rot " + n + ".");
                Console.WriteLine();
                System.IO.StreamReader Userfile = new System.IO.StreamReader(FindFile.ToString());
                Console.Write("Here is what the sentence and the " + (decrypt ? "decrypt" : "encrypt") +
                    " sentence(s) would be."); 
                while ((Words = Userfile.ReadLine()) != null)
                {
                    string[] Decrypt = new string[Words.Length];
                    Console.WriteLine(Words);
                    Decrypt = Test(Words, Value);
                    for (int i = 0; i < Words.Length; i++)
                    {

                        Console.Write(Decrypt[i]);
                    }
                    Console.WriteLine();

                }

                Userfile.Close();
            }

        }
        // Encryption or Decryption
        static string[] Test(string values, int move)
        {
            int ShiftValue = Convert.ToInt32(move);
            int NumberShift = Convert.ToInt32(move);
            // if the value is bigger than 26
            if (ShiftValue > 26)
            {
                ShiftValue = (ShiftValue % 26);
            }
            // number of the decrypting
            if (NumberShift > 10)
            {
                NumberShift = NumberShift % 10;
            }

            char[] Moo = values.ToCharArray();
            string[] EndResult = new string[values.Length];
            for (int i = 0; i < Moo.Length; i++)
            {
                // Text of the letter
                char Texts = Moo[i];
                string SomeNum;
                int RealNum;


                if (char.IsLetter(Moo[i]))
                {
                    // Add shift to all the text letters.
                    Texts = (char)(Texts + ShiftValue);

                    // Subtract the 26 when the value is overflow.
                    // And add 26 the underflow to get the value.
                    if (Texts > 'z')
                    {
                        Texts = (char)(Texts - 26);
                    }
                    else if (Texts < 'a')
                    {
                        Texts = (char)(Texts + 26);
                    }

                    EndResult[i] = (Texts.ToString());
                }
                //Number shifting
                if (char.IsDigit(Moo[i]))
                {
                    SomeNum = Convert.ToString(Moo[i]);
                    RealNum = Convert.ToInt32(SomeNum);
                    RealNum = (RealNum + NumberShift);
                    if (RealNum > 9)
                    {
                        RealNum = (int)(RealNum - 10);
                    }
                    else if (RealNum < 0)
                    {
                        RealNum = (int)(RealNum + 10);
                    }
                    EndResult[i] = Convert.ToString(RealNum);

                }
                // character of punctuation for shifting
                if (char.IsPunctuation(Moo[i]))
                {
                    string n = Convert.ToString(Moo[i]);
                    EndResult[i] = n;
                }
                //For the Upper Text Shifting
                if (char.IsUpper(Moo[i]))
                {

                    string LowText = Moo[i].ToString();
                    LowText = LowText.ToLower();
                    char value = Convert.ToChar(LowText);
                    value = (char)(value + ShiftValue);

                    // Subtract the 26 when the value is overflow.
                    // And add 26 the underflow to get the value.
                    if (value > 'z')
                    {
                        value = (char)(value - 26);
                    }
                    else if (value < 'a')
                    {
                        value = (char)(value + 26);
                    }

                    EndResult[i] = (value.ToString().ToUpper());
                }
                //For the Space 
                if (char.IsWhiteSpace(Moo[i]))
                {
                    EndResult[i] = " ";
                }
            }
            return EndResult;
        }

    
    public static void PrintError(string Err = "Unknown Failure", string Dbg = "",
                                    bool printUsage = true, bool exit = false, int exitVal = 1)
        {

            //1 Print out error message
            ConsoleColor currBackColor = Console.BackgroundColor;
            ConsoleColor currForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("Error: " + Err);
            if (Dbg.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Error.WriteLine(Dbg);
            }
            if (printUsage)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintUsage();
            }
            Console.ForegroundColor = currForeColor;
            Console.BackgroundColor = currBackColor;

            if (exit)
                Environment.Exit(exitVal); // By convention, exit with a value != 0 for error
        }

        //General usage message
        public static void PrintUsage()
        {
            Console.WriteLine("Usage: " + System.AppDomain.CurrentDomain.FriendlyName + " <n> <-e | -d> [ <str> | -f <filename>] \n" +
                   @"Performs rot-n encryption and decryption.
<n> specifies amount to rotate or de-rotate by.
-e specifies to encrypt (rotate by n)
-d specifies to decrypt (rotate by -n)
<str> is the string to rotate by, unless -f is instead specified
-f <filename> means perform the operation on the text file specified instead of a string.");
        
        }
    }
}
