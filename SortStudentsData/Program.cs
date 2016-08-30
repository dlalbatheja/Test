using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortStudentsData
{
    class Program
    {   
        static void Main(string[] args)
        {
            string inputFileName = string.Empty;

            #if !DEBUG

            if (args != null && args.Length >= 1)
            {
                inputFileName = args[0];
                SortData(inputFileName);
            }
            else
            {
                Exit("Program expects one parameter. Parameter is missing!");
            }

            #else

            //Unit Test Case A - Valid File - Different Scores
            inputFileName = Environment.CurrentDirectory + @"\UnitTestData\ClassA.txt";
            SortData(inputFileName);
            
            //Unit Test Case A - Valid File - Same Scores
            inputFileName = Environment.CurrentDirectory + @"\UnitTestData\ClassB.txt";
            SortData(inputFileName);

            //Unit Test Case B - InValid File
            inputFileName = Environment.CurrentDirectory + @"\UnitTestData\ClassB1.txt";
            SortData(inputFileName);

            #endif
        }

        private static void SortData(string inputFileName)
        {
            FileInfo inputFileInfo;

            if ((inputFileInfo = Utility.GetFileInfo(inputFileName)) != null)
            {
                StudentsData.GetFromFile(inputFileInfo);
                Console.WriteLine("{0} record(s) read from input file \"{1}\"!", StudentsData.Records.Count, inputFileInfo.FullName);
                
                StudentsData.SortRecords();
                Console.WriteLine("Data of {0} record(s) sorted based on score. If scores are the same, sorted by their last name followed by first name!", StudentsData.Records.Count);

                string outputFileName = StudentsData.WriteToFile();
                Console.WriteLine("Sorted data of {0} record(s) written to output file \"{1}\"!", StudentsData.Records.Count, outputFileName);

                Console.Write("Hit Any Key to Exit...");
                Console.ReadKey();
            }
            else
            {
                Exit("Input file is not valid. Please check if it exists or it is accessible!");
            }
        }

        static void Exit(string messgae)
        {
            Console.WriteLine(messgae);
            Console.Write("Hit Any Key to Exit...");
            Console.ReadKey();
        }
    }
}
