using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;


namespace LOCCounter
{
    class Assignment
    {

        #region Public Methods

        public void CreateOutPutFile()
        {
            //The following lines saves the output to a file. Can't have both for now
            fileStream = new FileStream(resultFilePath, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
            streamWriter.AutoFlush = true;
            Console.SetOut(streamWriter);
            Console.Out.WriteLine();

            //The following lines bring the output to the console
            Console.Out.Close();
            streamWriter = new StreamWriter(Console.OpenStandardOutput());
            streamWriter.AutoFlush = true;
            Console.SetOut(streamWriter);

        }


        public void ValidateUserInput()
        {
          
           
            int userInput;
            bool isValidUserInput = int.TryParse(Console.ReadLine(), out userInput);

            while (true)
            {
                if (isValidUserInput == true)
                {
                    //Destinguish user input and set the selected file path
                    switch (userInput)
                    {
                        case 1:
                            filePath = @"C:\PSP0 Program 2 Assignment\TestData\Program1.cs";
                            resultFilePath = @"C:\PSP0 Program 2 Assignment\TestData\Result.Program1.cs";
                            break;
                        case 2:
                            filePath = @"C:\PSP0 Program 2 Assignment\TestData\Program2.cs";
                            resultFilePath = @"C:\PSP0 Program 2 Assignment\TestData\Result.Program2.cs";
                            break;
                        default:
                            Console.WriteLine("Press any key to exit the program");
                            Console.ReadLine();
                            break;
                    }

                    CreateOutPutFile();
                    IsValidInputFile();
                    return; //WATCH!!!
                }
                else
                {
                  
                    Console.WriteLine("Please Press <1>, <2> or <3> to select a test file");
                    ValidateUserInput();
                    break;
                }
                
            }
          // It doesn't exit here!!!!  
        }

        public void IsValidInputFile()
        {
            while (true)
            {
                //Validate that the input files are not empty
                if (File.Exists(filePath))
                {
                    FileInfo file = new FileInfo(filePath);

                    if (file.Length == 0)
                    {
                       
                        //Promt user to verify the correct file content
                        Console.WriteLine("\t Please Copy the program files from the installation folder. \n\t Press Enter to continue");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("You selected {0}", filePath);
                        ReadFile();
                        return;
                    }
                }
                else
                {
                   
                    Console.WriteLine("\t Cannot find the input folder and test file. We will create the folder for you.\n\t Please Copy the test files from the intallation folder \n\t to C:\\PSP0 Program 2 Assignment\\TestData and Press Enter");
                    programFilesDirectory = Path.GetDirectoryName(filePath);
                    Directory.CreateDirectory(programFilesDirectory);
                    Console.ReadLine();
                    IsValidInputFile();
                    break;
                }
               
            }
            
        }

        public void ReadFile()
        {
           
            fileContent = new StreamReader(filePath);
            
            foreach (string line in File.ReadAllLines(filePath))
                {
                    eachLineInProgramFile = fileContent.ReadLine();
                    eachLineInProgramFile = eachLineInProgramFile.Trim();
                    ValidateCountableLinesOfCode();
                    CalculateLinesOfCode();
                }
            
        }

        public void ValidateCountableLinesOfCode()
        {
            checkForSpaces = eachLineInProgramFile.Equals("");
            checkForComments = eachLineInProgramFile.StartsWith(@"/");
            checkForOpeningSingleBracket = eachLineInProgramFile.Equals("{");
            checkForClosingSingleBracket = eachLineInProgramFile.Equals("}");

            isCountableLineOfCode = (checkForClosingSingleBracket == false && checkForOpeningSingleBracket == false && checkForComments == false && checkForSpaces == false);

        }

        public void CalculateLinesOfCode()
        {

            if (isCountableLineOfCode)
            {

                calculatedLinesOfCode++;
            }
           
            Console.WriteLine("{0} : {1}", calculatedLinesOfCode, eachLineInProgramFile);
            return;

        }

        public void PrintResult()
        {
            Console.WriteLine("\n LOC = {0}", calculatedLinesOfCode);
            Console.ReadLine();
            return;
        }

       
        #endregion

        #region Private Properties


        private string filePath;
        private string eachLineInProgramFile;
        private StreamReader fileContent;
        private bool checkForSpaces;
        private bool checkForComments;
        private bool checkForOpeningSingleBracket;
        private bool checkForClosingSingleBracket;
        private bool isCountableLineOfCode;
        private int calculatedLinesOfCode;
        private string programFilesDirectory;
        private string resultFilePath;
      //  private string resultFileName;
        private StreamWriter streamWriter;


        #endregion

        #region Public Properties

        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }

        }

        public StreamReader FileContent
        {
            get
            {
                return fileContent;
            }

            set
            {
                fileContent = value;
            }
        }

        public int CalculatedLinesOfCode
        {
            get
            {
                return calculatedLinesOfCode;
            }

            set
            {
                calculatedLinesOfCode = 0;
            }
        }
 
     
        #endregion




        public FileStream fileStream { get; set; }

        public TextWriterTraceListener writeToResultFile { get; set; }

        public ConsoleTraceListener copyResultFromConsole { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\t Welcome to Hristina Koleva F66436 PSP1 Assigment No. 2 \n\n");
            Console.WriteLine("\t Please select a test file: \n\n For \"Program 1\" \t - press <1> \n For \"Program 2\" \t - press <2> \n");
           
            Assignment calculateLinesOfCode = new Assignment();
           
            calculateLinesOfCode.ValidateUserInput();
            
            calculateLinesOfCode.PrintResult();
           
        }
    }
}

//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string filePath = @"C:\PSP0 Program 2 Assignment\TestData\Program1.cs";
//            int linesOfCode = 0;
//            string eachLineinProgramFile = null;

//            while (true)
//            {
//                if (File.Exists(filePath))
//                {
//                    FileInfo file = new FileInfo(filePath);

//                    StreamReader fileContent = new StreamReader(filePath);

//                    foreach (string line in File.ReadAllLines(filePath))
//                    {
//                        eachLineinProgramFile = fileContent.ReadLine();
//                        eachLineinProgramFile = eachLineinProgramFile.Trim();

//                        bool checkSpaces = eachLineinProgramFile.Equals("");
//                        bool checkComments = eachLineinProgramFile.StartsWith("/");
//                        bool checkSingleBrackets = eachLineinProgramFile.Equals("{");
//                        bool checkClosingBrackets = eachLineinProgramFile.Equals("}");

//                        if (checkSpaces == false && checkComments == false && checkClosingBrackets == false && checkSingleBrackets == false)
//                        {


//                            linesOfCode++;

//                        }

//                        Console.WriteLine("{0} : {1}", linesOfCode, eachLineinProgramFile);
//                    }

//                    Console.WriteLine("\n LOC = {0}", linesOfCode);
//                    Console.ReadLine();
//                    return;
//                }

//            }


//        }

//    }
//}
