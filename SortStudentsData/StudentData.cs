using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortStudentsData
{
    class StudentData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Score { get; set; }
        public string LastFirstName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }        

        static public StudentData DecodeFromLine(string line, char fieldSep)
        {
            try
            {
                string[] lineData = line.Split(fieldSep);

                StudentData studentData = new StudentData
                {
                    LastName = lineData.Length >= 1 ? lineData[0].Trim() : string.Empty,
                    FirstName = lineData.Length >= 2 ? lineData[1].Trim() : string.Empty,
                    Score = lineData.Length >= 3 ? decimal.Parse(lineData[2]) : 0
                };

                return studentData;
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        static public string EncodeInToLine(StudentData studentData, char fieldSep)
        {
            return studentData.LastName + fieldSep + studentData.FirstName + fieldSep + studentData.Score;
        }
    }

    class StudentsData
    {
        const string FILE_NAME_SUFFIX = "-graded";
        public static List<StudentData> Records { get; private set; }
        static FileInfo InputFileInfo;
        static char FieldSep;

        static public bool GetFromFile(FileInfo fileInfo, char fieldSep = ',')
        {
            InputFileInfo = fileInfo;
            FieldSep = fieldSep;
            Records = new List<StudentData>();

            if (InputFileInfo.Exists)
            {
                StreamReader streamReader = new StreamReader(InputFileInfo.FullName);
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    StudentData studentData = StudentData.DecodeFromLine(line, FieldSep);

                    if (studentData != null)
                    {
                        Records.Add(studentData);
                    }
                }

                return true;
            }

            return false;
        }

        static public void SortRecords()
        {
            int recordsCount = Records.Count;

            for (int i = 0; i < recordsCount-1; i++)
            {
                bool isSwapped = false;

                for (int j = 0; j < recordsCount-1; j++)
                {
                    int myIndex = j;
                    int myNextOneIndex = j + 1;
                    bool canSwap = false;

                    if (Records[myIndex].Score > Records[myNextOneIndex].Score)
                    {
                        canSwap = true;
                    }
                    else if (Records[myIndex].Score == Records[myNextOneIndex].Score && String.Compare(Records[myIndex].LastFirstName, Records[myNextOneIndex].LastFirstName) > 0)
                    {
                        canSwap = true;
                    }

                    if (canSwap)
                    {
                        Swap(Records[myIndex], Records[myNextOneIndex]);
                        isSwapped = true;
                    }
                }

                //If noSwap took place at all, then list is sorted. So break from rest of loop
                if (!isSwapped)
                    break;
            }
        }

        static public string WriteToFile()
        {
            string outputFile = InputFileInfo.FullName.RemoveExtension() + FILE_NAME_SUFFIX + InputFileInfo.Extension;

            if (Records.Count > 0)
            {
                string fileText = string.Empty;

                foreach (StudentData studentData in Records)
                {
                    if (string.IsNullOrEmpty(fileText))
                        fileText = StudentData.EncodeInToLine(studentData, FieldSep);
                    else
                        fileText = fileText + Environment.NewLine + StudentData.EncodeInToLine(studentData, FieldSep);
                }

                File.WriteAllText(outputFile, fileText);

                return outputFile;
            }

            return string.Empty;
        }

        private static bool Swap(StudentData studentData1, StudentData studentData2)
        {
            string student1FirstName = studentData1.FirstName;
            string student1LastName = studentData1.LastName;
            decimal student1Score = studentData1.Score;

            studentData1.FirstName = studentData2.FirstName;
            studentData1.LastName = studentData2.LastName;
            studentData1.Score = studentData2.Score;

            studentData2.FirstName = student1FirstName;
            studentData2.LastName = student1LastName;
            studentData2.Score = student1Score;

            return true;
        }
    }
}
