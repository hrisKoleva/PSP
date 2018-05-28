using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;


namespace ConsoleApplication1
{
    class Program
    {
        public static void Main(string[] args)
        {

            /*Declaration of necessary variables to store the mean value and standard deviation.*/
            double meanValue = 0.0;
            double standardDeviation = 0.0;

            /*Variable "difference" is used to store the difference of the numbers and  
             * standard deviation used in the calculation of standard deviation.*/
            double difference = 0.0;

            /*"eachLineInFile" value is used to store the data at each row in the text file*/
            string eachLineInFile;

            /*"filePath" - the file path of the input test files*/
            string filePath = null;
           

            /*Print welcome message to the user and prompt to select input test file*/
            Console.WriteLine("\t Welcome to Hristina Koleva F66436 PSP0 Assigment No. 1 \n\n");
            Console.WriteLine("\t Please select a test file: \n\n For \"EstimatedProxySize\" test\t - press <1> \n For \"Development Hour\" test\t - press <2> \n For \"Ëxample\" test\t - press <3> \n");

            /*A regular expression used to validate the format of the numbers in the file. 
             *Used to avoid exceptions from containing characters. 
             *The decimal numbers in the file must be formatted with a decimal point not comma
             *and must contain at least one numeric character*/
            Regex formatInputFile = new Regex( @"[^0-9\.]+");

            /*Variable used to store user input from keyboard*/
            int userInput;

            /*Validate user input as integer value*/
            bool isValidUserInput = int.TryParse(Console.ReadLine(), out userInput);

            /*A while loop is used to validate the application will handle invalid user input*/
            while (true)
            {
                if (isValidUserInput == true)
                {
                    /*Destinguish user input and set the selected file path*/
                    switch (userInput)
                    {
                        case 1:
                            filePath = @"C:\PSP0 Program 1 Assignment\TestData\PSPTest02.EstimatedProxySize.txt";
                            break;
                        case 2:
                            filePath = @"C:\PSP0 Program 1 Assignment\TestData\PSPTest01.DevelopmentHours.txt";
                            break;
                        case 3:
                            filePath = @"C:\PSP0 Program 1 Assignment\TestData\PSPTest03.Example.txt";
                            break;
                        default:
                            Console.WriteLine("Press any key to exit the program");
                            Console.ReadLine();
                            break;
                    }

                    /*Message to the user to indicate selected file*/
                    Console.WriteLine("\t You selected \n\t {0} test file \n", filePath);

                    /*While loop is used to validate that the input files exists*/
                    while (true)
                    {
                        /*Validate that the input files are not empty*/
                        if (File.Exists(filePath))
                        {
                            FileInfo file = new FileInfo(filePath);

                            if (file.Length == 0)
                            {
                                /*Promt user to verify the correct file content*/
                                Console.WriteLine("\t Please make sure the selected test file contains requried values.\n\t Please make sure decimal symbol is <.>\n\t You may Copy the file from the installation folder. \n\t Press Enter to continue");
                                Console.ReadLine();
                            }
                            else
                            {
                                /*Create a StreamReader with the content of the file. 
                                 *Declare a linked list collection as required in the assigment*/
                                StreamReader fileContent = new StreamReader(filePath);
                                LinkedList<double> listOfRealNumbers = new LinkedList<double>();

                                /*Read the file. Format each line in the file and remove non numeric characters. 
                                 *Each line must contain at least one numeric character or the application will not stop working*/
                                foreach (string line in File.ReadAllLines(filePath))
                                {
                                    eachLineInFile = fileContent.ReadLine();

                                       
                                    eachLineInFile = formatInputFile.Replace(eachLineInFile, String.Empty);
                                        
                                    /*Store the numbers from the file in a Linked List*/
                                    listOfRealNumbers.AddFirst(double.Parse(eachLineInFile));
                                }
                                
                            
                                Console.WriteLine("\t The values in the selected file are:");
                                Console.WriteLine("\t __________________________ \n");

                                /*Print the values in the file and
                                 *Calculate the mean value!*/
                                for (int i = 0; i < listOfRealNumbers.Count; i++)
                                {

                                    Console.WriteLine("\t \t {0}", listOfRealNumbers.ElementAt<double>(i));
                                    meanValue = listOfRealNumbers.Average();
                                }

                                /*Print the calculated mean value*/
                                Console.WriteLine();
                                Console.WriteLine("\t Calculated mean value is:  {0:F}!", meanValue);
                                Console.WriteLine("\t __________________________ \n");

                                /*Calculate the numerator in the formula of standard deviation separately and then
                                 *Calculate the standard deviation!*/
                                for (int k = 0; k < listOfRealNumbers.Count; k++)
                                {
                                    difference += Math.Pow((listOfRealNumbers.ElementAt<double>(k) - meanValue), 2);
                                    standardDeviation = Math.Sqrt(difference / (listOfRealNumbers.Count - 1));
                                }

                                /*Print the calculated mean value*/
                                Console.WriteLine("\t Calculated standard deviation is: {0:F}!\n", standardDeviation);
                                Console.WriteLine("\t Press Enter to exit");
                                Console.ReadLine();
                                return;
                            }

                        }

                        /*If the file or folder do not exist, 
                         *create the folder and prompt user to copy the input test files from the installation folder.
                         *The calculations will be then processed for the first correctly selected file* */
                        else
                        {
                            Console.WriteLine("\t Cannot find the input folder and test file. We will create the folder for you.\n\t Please Copy the test files from the intallation folder \n\t to C:\\PSP0 Program 1 Assignment\\TestData and Press Enter");

                            string missingDirectory = Path.GetDirectoryName(filePath);
                            Directory.CreateDirectory(missingDirectory);
                            Console.ReadLine();

                        }
                    }
                }
                    /*If the user does not enter is not 1,2 or 3, prompt the user to enter correct number 
                     * and wait untill it is correct.
                     *The calculations will be then processed for the first correctly selected file*/
                else
                {
                    Console.WriteLine("Please Press <1>, <2> or <3> to select a test file");
                    isValidUserInput = int.TryParse(Console.ReadLine(), out userInput);
                }

            } 

        }
       
    }
}
