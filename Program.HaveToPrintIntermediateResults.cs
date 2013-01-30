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
        public class Assignment
        {

            #region Public Methods

            public void CreateOutPutFile()
            {

                currentOutput = Console.Out;
                fileStream = new FileStream(resultFilePath, FileMode.Create);
                streamWriter = new StreamWriter(fileStream);
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
                                break;
                            default:
                                Console.WriteLine("Press any key to exit the program");
                                //Console.ReadLine();
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
                            //Console.ReadLine();
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

                        Console.WriteLine("\t Cannot find the input folder and test file. We will create the folder for you.\n\t Please Copy the test files from the intallation folder \n\t to C:\\PSP0 Program 2 Assignment\\TestData \n\t Rename the Program.cs file to Program1.cs and Press Enter");
                        programFilesDirectory = Path.GetDirectoryName(filePath);
                        Directory.CreateDirectory(programFilesDirectory);
                        //Console.ReadLine();
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
                checkForRegions = eachLineInProgramFile.StartsWith("#");
               
                isCountableLineOfCode = (checkForClosingSingleBracket == false && checkForOpeningSingleBracket == false && checkForComments == false && checkForSpaces == false && checkForRegions == false);
               
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


            public void DisplayMethodInfo(MethodInfo[] arrayOfMethods)
            {
                // Display information for all methods.
                for (int i = 0; i < arrayOfMethods.Length; i++)
                {
                    MethodInfo methodInfo = (MethodInfo)arrayOfMethods[i];

                    Console.WriteLine("\n Method No. {0} \n Method Name : {1}", i + 1, methodInfo.Name);

                }

                //Console.ReadLine();
                
            }

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

         
             public void ReadClass()
             {
                  fileContent = new StreamReader(filePath);

                  LinkedList<string> linesInClass = new LinkedList<string>();

                  foreach (string line in File.ReadAllLines(filePath))
                  {
                      eachLineInProgramFile = fileContent.ReadLine();
                      eachLineInProgramFile = eachLineInProgramFile.Trim();

                      if (eachLineInProgramFile.StartsWith("public class"))
                          { 
                              
                              linesInClass.AddFirst(eachLineInProgramFile);
                              calculatedLinesOfCode = 0;
                              eachLineInProgramFile = linesInClass.First();

                          }
                    
                      ValidateCountableLinesOfCode();
                      CalculateLinesOfCode();
                      
                 
                  }
                  
                  //Console.ReadLine();
             }

             public void ReadMethod()
             {
                 fileContent = new StreamReader(filePath);

                 LinkedList<string> linesInMethod = new LinkedList<string>();

                 foreach (string line in File.ReadAllLines(filePath))
                 {
                     eachLineInProgramFile = fileContent.ReadLine();
                     eachLineInProgramFile = eachLineInProgramFile.Trim();

                     if (eachLineInProgramFile.StartsWith("public void"))
                     {

                         linesInMethod.AddFirst(eachLineInProgramFile);
                         calculatedLinesOfCode = 0;
                         eachLineInProgramFile = linesInMethod.First();

                     }

                     ValidateCountableLinesOfCode();
                     CalculateLinesOfCode();


                 }
             
                 //Console.ReadLine();
             }

          
            public void PrintResult()
            {
                Console.WriteLine("\n Total Lines of Code is: \t {0} \n", calculatedLinesOfCode);
               // calculatedLinesOfCode = 0;
          
                //Console.ReadLine();
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
            

            #endregion

        }

       

       public class Program
        {

            static void Main(string[] args)
            {
                Console.WriteLine("\t Welcome to Hristina Koleva F66436 PSP1 Assigment No. 2 \n\n");
                Console.WriteLine("\t Please select a test file: \n\n For \"Program 1\" \t - press <1> \n For \"Program 2\" \t - press <2> \n");
                Console.WriteLine("\t Please press Enter to see the count of total LOC. Result file is created in C:\\PSP0 Program 2 Assignment\\TestData\\");
                Assignment calculateLinesOfCode = new Assignment();
               
                calculateLinesOfCode.ValidateUserInput();
                 
                calculateLinesOfCode.FindClasses();

                calculateLinesOfCode.FindMethods();

                calculateLinesOfCode.PrintResult();

                calculateLinesOfCode.ReadClass();

                calculateLinesOfCode.ReadMethod();
            
               
                
            }
        }

    }
   
}