using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PortableTestLargestRectangleInBinaryMatrix
{


    class Program
    {
        static readonly string pathCurrent = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string pathTestCase = pathCurrent + @"TestCases";
        static readonly string pathExpect = pathCurrent + @"Expect";
        static readonly string pathSourceC = pathCurrent;
        static readonly string pathSourcePy = pathCurrent;
        static readonly string pathSourceJava = pathCurrent;
        static Process process = null;
        static ProcessStartInfo startInfo =null;
        private static void RunCommand(string cmd)
        {
            if (process != null && startInfo != null)
            {
                startInfo.Arguments = $"-c \"{cmd}\"";
            }
            else
            {
                process = new Process();
                startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";
                startInfo.Arguments = $"-c \"{cmd}\"";
                startInfo.RedirectStandardError = true;
                process.StartInfo = startInfo;
            }


            process.Start();
            string error = process.StandardError.ReadToEnd();
            if (!(String.IsNullOrEmpty(error) || String.IsNullOrWhiteSpace(error)))
            {
                Console.WriteLine($"Error : {error}");
            }
            process.WaitForExit();
        }

        private static string CreateRowTableString(string[] para)
        {
            StringBuilder builder = new StringBuilder();
            int length = para.Count();
            for (int i = 0; i < length; i++)
            {
                builder.Append("|");
                var countChar = para[i].Count();
                int padLeft = (12 - countChar) / 2;
                int padRight = ((12 - countChar) % 2) == 0 ? padLeft : padLeft + 1;
                builder.Append(para[i].PadLeft(padLeft + countChar).PadRight(12));
            }
            builder.Append("|");

            return builder.ToString();
        }
        private static string ReadFileContent(string path)
        {
            var lines = File.ReadLines(path).ElementAt(0);
            StringBuilder builder = new StringBuilder();
            foreach (var item in lines)
            {
                builder.Append(item);
            }
            return builder.ToString();
        }
        private static string CreateBarTableString(int size)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                builder.Append("-");
            }

            return builder.ToString();
        }

        static void RunTest(string commandBuild, string pathSourceCode, string typeLanguage, string commandRunProgram)
        {
            RunCommand($"cd {pathTestCase} ; Remove-Item Out* ; cd {pathSourceCode} ; {commandBuild}");
            DirectoryInfo d = new DirectoryInfo(pathTestCase);

            FileInfo[] Files = d.GetFiles("*.txt");
            var totalFile = Files.Length;
            string pathFileInput = String.Empty;
            string pathFileOutput = String.Empty;
            string pathFileExpect = String.Empty;
            List<string> para;
            var bar = CreateBarTableString(53);
            Console.WriteLine(bar);
            var barTop = CreateRowTableString(new string[] { "File", "Case", "Result", "Details" });
            Console.WriteLine(barTop);
            var countFalse = 0;
            StringBuilder runProgram =new StringBuilder("cd " + pathTestCase + ";");
            string prefixPathFile = (typeLanguage.Equals("OutC") ? pathTestCase + @"\" : "");
            for (int i = 0; i < totalFile; i++)
            {
                int index = i + 1;
                pathFileInput =prefixPathFile+ "test" + index + ".txt";
                pathFileOutput =prefixPathFile+ typeLanguage + Files[i].Name;
                runProgram.Append(commandRunProgram);
                runProgram.Append(pathFileInput);
                runProgram.Append(" ");
                runProgram.Append(pathFileOutput);
                if (i!=(totalFile-1))
                {
                    runProgram.Append(";");
                }
                
            }
            RunCommand(runProgram.ToString());
            for (int i = 0; i < totalFile; i++)
            {
                int index = i + 1;
                para = new List<string>();
                pathFileInput = "test" + index + ".txt";
                pathFileOutput = pathTestCase + @"\"+typeLanguage + Files[i].Name;

                pathFileExpect = pathExpect + @"\" + "result" + index + ".txt";
                try
                {
                    if (!File.Exists(pathFileOutput))
                    {
                        para.Add("Output is not exist");
                        countFalse++;
                        continue;
                    }
                    var testResult = File.ReadLines(pathFileExpect).ElementAt(0).Equals(File.ReadLines(pathFileOutput).ElementAt(0));
                    para.AddRange(new string[] { pathFileInput, (i + 1).ToString(), testResult.ToString(), "" });
                    if (!testResult)
                    {
                        para.Add("Expect : " + ReadFileContent(pathFileExpect) + " Actual : " + ReadFileContent(pathFileOutput));
                        countFalse++;
                    }
                    var row = CreateRowTableString(para.ToArray());
                    Console.WriteLine(row);
                }
                catch (Exception ex)
                {
                    para.AddRange(new string[] { pathFileInput, (i + 1).ToString(), "False", ex.ToString() });
                    var row = CreateRowTableString(para.ToArray());
                    Console.WriteLine(row);
                    countFalse++;
                }
            }
            Console.WriteLine( bar);
        }

        public static void TestJava()
        {

            Console.WriteLine("Test Java");
            RunCommand($"cd {pathSourceJava} ; Remove-Item *.class ; cd {pathTestCase} ; Remove-Item *.class");
            string commandRun = "java Main ";
            string commandBuild = $"javac Main.java ; Copy-Item Main.class {pathTestCase}";
            string typeLanguage = "OutJava";
            RunTest(commandBuild, pathSourceJava, typeLanguage, commandRun);
            RunCommand($"cd {pathSourceJava} ; Remove-Item *.class");
            RunCommand($"cd {pathTestCase} ; Remove-Item *.class ;Remove-Item Out.*");
        }

        public static void TestPy()
        {

            Console.WriteLine("Test Py");
            RunCommand($"cd {pathTestCase} ; Remove-Item *.py");
            string commandRun = "python  LargestRectangleInBinaryMatrixPy.py ";
            string commandBuild = $"copy LargestRectangleInBinaryMatrixPy.py {pathTestCase}";
            string typeLanguage = "OutPy";
            RunTest(commandBuild, pathSourcePy, typeLanguage, commandRun);
            RunCommand($"cd {pathTestCase} ; Remove-Item *.py;");
        }
        public static void TestC()
        {
            var linesOfFilePathGcc = File.ReadLines(pathCurrent + "pathGcc.txt");
            string pathGcc=linesOfFilePathGcc.Any()?linesOfFilePathGcc.ElementAt(0):" ";
            if (String.IsNullOrWhiteSpace( pathGcc))
            {
                Console.WriteLine("Can't not find path of gcc!");
            }
            else
            {
                Console.WriteLine("Test C");
                RunCommand($"cd {pathSourceC} ; Remove-Item sourceOut.* ;cd {pathTestCase} ; Remove-Item sourceOut.*");
                string commandRun = $"{pathGcc+@"\"}sourceOut.exe ";
                string commandBuild = $"Copy-Item Source.c {pathGcc};cd {pathGcc};./gcc.exe -std=c90 -pedantic -g -rdynamic Source.c -o sourceOut.exe ;";
                string typeLanguage = "OutC";
                RunTest(commandBuild, pathSourceC, typeLanguage, commandRun);
                RunCommand($"cd {pathGcc} ; Remove-Item source* ; cd {pathSourceC} ; Remove-Item sourceOut.* ;cd {pathTestCase} ; Remove-Item sourceOut.* ;Remove-Item Out*");
            }
            
        }
        static void Main(string[] args)
        {
            //RunCommand("ls");
            TestJava();
            TestPy();
            TestC();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
