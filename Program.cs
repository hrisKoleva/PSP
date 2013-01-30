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
    class PSPSecondAssignment
    {
        //ClassBegin      
        public class Assignment : IDisposable
        {

            #region Public Methods

            //MethodBegin
            public void CreateOutPutFile()
            {

                currentOutput = Console.Out;
                fileStream = new FileStream(resultFilePath, FileMode.Create);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.AutoFlush = true;
               
                Console.SetOut(streamWriter);
                
            } 
            //MethodEnd  
         
            //MethodBegin
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
                                resultFilePath = @"C:\PSP0 Program 2 Assignment\TestData\Result.Program1.txt";
                                break;
                            case 2:
                                filePath = @"C:\PSP0 Program 2 Assignment\TestData\Program2.cs";
                                break;
                            default:
                                Console.WriteLine("Press any key to exit the program");
                                Console.ReadLine();
                                break;
                        }

                        CreateOutPutFile();

                        IsValidInputFile();
                        return; 
                    }
                    else
                    {

                        Console.WriteLine("Please Press <1>, <2> or <3> to select a test file");
                        ValidateUserInput();
                        break;
                    }

                }
                
            }
            //MethodEnd  
   
            //MethodBegin
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
                            Console.WriteLine("You selected {0} \n \n", filePath);
                            ReadFile();
                           
                            return;
                        }
                    }
                    else
                    {

                        Console.WriteLine("\t Cannot find the input folder and test file. We will create the folder for you.\n\t Please Copy the test files from the intallation folder \n\t to C:\\PSP0 Program 2 Assignment\\TestData \n\t Rename the Program.cs file to Program1.cs and Press Enter");
                        programFilesDirectory = Path.GetDirectoryName(filePath);
                        Directory.CreateDirectory(programFilesDirectory);
                        Console.ReadLine();
                        IsValidInputFile();
                        break;
                    }

                }
              
            }
            //MethodEnd   
  
            //MethodBegin
            public void ReadFile()
            {

                fileContent = new StreamReader(filePath);
                linesInFile = new LinkedList<string>();

                foreach (string line in File.ReadAllLines(filePath))
                {
                    eachLineInProgramFile = fileContent.ReadLine();
                    eachLineInProgramFile = eachLineInProgramFile.Trim();
                    linesInFile.AddLast(eachLineInProgramFile);
                    ValidateCountableLinesOfCode();
                    CalculateLinesOfCode();
               
                }
               
            }
            //MethodEnd    

            public void ReadListOfLines()
            {
                
                     classBegin =  linesInFile.Find(@"//ClassBegin");
                     classEnd = linesInFile.Find(@"//ClassEnd");

                     methodBegin = linesInFile.Find(@"//MethodBegin");
                     methodEnd = linesInFile.Find(@"//MethodEnd");
                     nextLineInClass = classBegin;
                    
                    
                

                foreach (string internalLineInClass in linesInFile)
                {
                    while (true)
                    {
                        if (internalLineInClass.Equals(classBegin.Value))
                        {
                            if (!internalLineInClass.Equals(classEnd.Value))
                            {
                                eachLineInProgramFile = nextLineInClass.Value;
                                calculatedLinesOfCode = 0;
                                ValidateCountableLinesOfCode();
                                CalculateLinesOfCode();
                            }
                            else
                            {
                                nextLineInClass = nextLineInClass.Next;
                               
                            }

                        }

                        //else
                        //{
                        //    ReadListOfLines();
                            
                        //}
                        break;
                     
                    }
                }

                    foreach (string internalLineInMethod in linesInFile)
                    {
                        while (true)
                        {
                            if (internalLineInMethod.Equals(methodBegin))
                            {
                                CountInternalListLines();
                            }
                            else if (internalLineInMethod.Equals(methodEnd))
                            {
                                ReadListOfLines();
                            }
                        }
                    }
               
            }

            //MethodBegin
            private void CountInternalListLines()
            {
              //  int i = 0;
                calculatedLinesOfCode = 0;
              //  eachLineInProgramFile = linesInFile.ElementAt<string>(i);
                ValidateCountableLinesOfCode();
                CalculateLinesOfCode();
             //   i++;
               // nextLineInClass = nextLineInClass.Next;
            }
            //MethodEnd
 
            //MethodBegin
            public void ValidateCountableLinesOfCode()
            {
                checkForSpaces = eachLineInProgramFile.Equals("");
                checkForComments = eachLineInProgramFile.StartsWith(@"/");
                checkForOpeningSingleBracket = eachLineInProgramFile.Equals("{");
                checkForClosingSingleBracket = eachLineInProgramFile.Equals("}");
                checkForRegions = eachLineInProgramFile.StartsWith("#");
               
                isCountableLineOfCode = (checkForClosingSingleBracket == false && checkForOpeningSingleBracket == false && checkForComments == false && checkForSpaces == false && checkForRegions == false);
               
            }
            //MethodEnd     

            //MethodBegin
            public void CalculateLinesOfCode()
            {

                if (isCountableLineOfCode)
                {

                    calculatedLinesOfCode++;
                }

                Console.WriteLine("{0} : {1}", calculatedLinesOfCode, eachLineInProgramFile);
               
                return;
              
            }
            //MethodEnd     

            //MethodBegin
            public void DisplayMethodInfo(MethodInfo[] arrayOfMethods)
            {
                // Display information for all methods.
                for (int i = 0; i < arrayOfMethods.Length; i++)
                {
                    methodInfo = (MethodInfo)arrayOfMethods[i];

                    Console.WriteLine("\n Method No. {0} \n Method Name : {1}", i + 1, methodInfo.Name);

                }

                //Console.ReadLine();
                
            }
            //MethodEnd     

            //MethodBegin
            public void FindMethods()
            {
                Type Methods = (typeof(Assignment));

                // Get the  methods.
                MethodInfo[] ArrayOfMethodInfo = Methods.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                numberOfMethods = ArrayOfMethodInfo.Length;
                Console.WriteLine("\n Number of Items is: \t {0} \n", numberOfMethods);
                Console.WriteLine("\n Item Names are: \n");
                DisplayMethodInfo(ArrayOfMethodInfo);
              
            }
            //MethodEnd     

            //MethodBegin
            public void FindClasses()
             {
                 listOfClasses = new List<Type>();
                 assembly = Assembly.GetExecutingAssembly();

                foreach (var currentClass in assembly.GetTypes())
                {
                    if (currentClass.Namespace == "LOCCounter")
                    {

                        listOfClasses.Add(currentClass);
                        className = currentClass.Name;
                        Console.WriteLine("\n Part Names are: \t {0} \n", className);
                    }

                }

                numberOfClasses = listOfClasses.Count;
                Console.WriteLine("\n Number of Actual Parts is: \t {0} \n", numberOfClasses - 1);
              
               
             }
            //MethodEnd    
 
            //MethodBegin
            public void ReadClass()
             {
                  fileContent = new StreamReader(filePath);

                  foreach (string line in File.ReadAllLines(filePath))
                  {
                      eachLineInProgramFile = fileContent.ReadLine();
                      eachLineInProgramFile = eachLineInProgramFile.Trim();

                      if (eachLineInProgramFile.Equals(@"//ClassBegin"))
                      {
                         
                          calculatedLinesOfCode = 0;
                          do
                          {
                                  eachLineInProgramFile = fileContent.ReadLine();
                                  eachLineInProgramFile = eachLineInProgramFile.Trim();
                                  ValidateCountableLinesOfCode();
                                  CalculateLinesOfCode();
                          }
                                
                          while (!eachLineInProgramFile.Equals(@"//ClassEnd"));
                    
                          return;
                      }
                      
                  }

                  //Console.ReadLine();
             }
            //MethodEnd 
    
           //MethodBegin
            public void ReadMethod()
            {
                fileContent = new StreamReader(filePath);

                foreach (string linesInMethod in File.ReadAllLines(filePath))
                {
                    eachLineInProgramFile = fileContent.ReadLine();
                    eachLineInProgramFile = eachLineInProgramFile.Trim();

                    if (eachLineInProgramFile.Equals(@"//MethodBegin"))
                    {

                        calculatedLinesOfCode = 0;

                        do
                        {
                            eachLineInProgramFile = fileContent.ReadLine();
                            eachLineInProgramFile = eachLineInProgramFile.Trim();
                            ValidateCountableLinesOfCode();
                            CalculateLinesOfCode();

                        }

                        while (!eachLineInProgramFile.Equals(@"//MethodEnd"));

                        return;
                    }

                    Console.ReadLine();
                }

            }
             //MethodEnd    
 
             //MethodBegin
             public void OpenResultFile()
             {
                 Process openFile = new System.Diagnostics.Process();
                 openFile.StartInfo.FileName = resultFilePath;
                 openFile.Start();
             }
             //MethodEnd  
   
             //MethodBegin
            public void PrintResult()
            {
                Console.WriteLine("\n Total Lines of Code is: \t {0} \n", calculatedLinesOfCode);
                //Console.ReadLine();
                return;
            }
            //MethodEnd     

            //MethodBegin
            public void DisposeStreams()
            {
                Console.SetOut(currentOutput);
                Dispose();
            }
            //MethodEnd     

            //MethodBegin
            public void Dispose()
            {
                streamWriter.Dispose();
                fileStream.Dispose();
                fileContent.Dispose();
            }
            //MethodEnd     
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
            private bool checkForRegions;
            private int numberOfMethods;
            private List<Type> listOfClasses;
            private Assembly assembly;
            private int numberOfClasses;
            private string className;
            private FileStream fileStream;
            private StreamWriter streamWriter;
            private string resultFilePath;
            private TextWriter currentOutput;
            private MethodInfo methodInfo;
            private LinkedList<string> linesInFile;
            private LinkedListNode<string> classBegin;
            private LinkedListNode<string> classEnd;
            private LinkedListNode<string> methodBegin;
            private LinkedListNode<string> methodEnd;
            private LinkedListNode<string> nextLineInClass;

            #endregion
            
        }
       
       //ClassEnd

       //ClassBegin

       public class Program
        {

            static void Main(string[] args)
            {
                Console.WriteLine("\t Welcome to Hristina Koleva F66436 PSP1 Assigment No. 2 \n\n");
                Console.WriteLine("\t Please select a test file: \n\n For \"Program 1\" \t - press <1> \n For \"Program 2\" \t - press <2> \n");
                Console.WriteLine("\t Please press Enter generate result file. \n \t Result file is created in C:\\PSP0 Program 2 Assignment\\TestData\\");
               
                Assignment calculateLinesOfCode = new Assignment();
               
                calculateLinesOfCode.ValidateUserInput();
                 
                calculateLinesOfCode.FindClasses();

                calculateLinesOfCode.FindMethods();

                calculateLinesOfCode.PrintResult();

                //calculateLinesOfCode.ReadClass();

                //calculateLinesOfCode.ReadMethod();

                calculateLinesOfCode.ReadListOfLines();

                calculateLinesOfCode.OpenResultFile();

                calculateLinesOfCode.DisposeStreams();
                
            }
        }

        //ClassEnd

    }
   
}