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
        static readonly string pathTestCase = pathCurrent + @"/TestCases";
        static readonly string pathExpect = pathCurrent + @"/Expect";
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
                RunCommand($"echo 'Error : {error}'");
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
            var lines = File.ReadAllLines(path);
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
            RunCommand($"cd {pathTestCase} ; Remove-Item Out*");
            RunCommand($"cd {pathSourceCode} ; {commandBuild}");
            DirectoryInfo d = new DirectoryInfo(pathTestCase);

            FileInfo[] Files = d.GetFiles("*.txt");
            var totalFile = Files.Length;
            string pathFileInput = String.Empty;
            string pathFileOutput = String.Empty;
            string pathFileExpect = String.Empty;
            List<string> para;
            var bar = CreateBarTableString(53);
            RunCommand($"echo {bar}");
            var barTop = CreateRowTableString(new string[] { "File", "Case", "Result", "Details" });
            RunCommand($"echo '{barTop}'");
            var countFalse = 0;
            for (int i = 0; i < totalFile; i++)
            {
                int index = i + 1;
                para = new List<string>();
                pathFileInput = "test" + index + ".txt";
                pathFileOutput = typeLanguage + Files[i].Name;

                pathFileExpect = pathExpect + "/" + "result" + index + ".txt";
                try
                {
                    RunCommand("cd " + pathTestCase + ";" + commandRunProgram + pathFileInput + " " + pathFileOutput);
                    var testResult = File.ReadAllLines(pathFileExpect).SequenceEqual(File.ReadAllLines(pathTestCase + "/" + pathFileOutput));
                    para.AddRange(new string[] { pathFileInput, (i + 1).ToString(), testResult.ToString(), "" });
                    if (!testResult)
                    {
                        para.Add("Expect : " + ReadFileContent(pathFileExpect) + " Actual : " + ReadFileContent(pathTestCase + "/" + pathFileOutput));
                        countFalse++;
                    }
                    var row = CreateRowTableString(para.ToArray());
                    RunCommand($"echo '{row}'");
                }
                catch (Exception ex)
                {
                    para.AddRange(new string[] { pathFileInput, (i + 1).ToString(), "False", ex.ToString() });
                    var row = CreateRowTableString(para.ToArray());
                    RunCommand($"echo '{row}'");
                    countFalse++;
                }
            }
            RunCommand($"echo {bar}");
        }

        public static void TestJava()
        {

            RunCommand("echo Test Java");
            RunCommand($"cd {pathSourceJava} ; Remove-Item *.class ; cd {pathTestCase} ; Remove-Item *.class");
            string commandRun = "java Main ";
            string commandBuild = $"javac Main.java ; cp Main.class {pathTestCase}";
            string typeLanguage = "OutJava";
            RunTest(commandBuild, pathSourceJava, typeLanguage, commandRun);
            RunCommand($"cd {pathSourceJava} ; Remove-Item *.class");
            RunCommand($"cd {pathTestCase} ; Remove-Item *.class");
        }

        public static void TestPy()
        {

            RunCommand("echo Hieu Test Py");
            RunCommand($"cd {pathTestCase} ; Remove-Item *.py");
            string commandRun = "python  LargestRectangleInBinaryMatrixPy.py ";
            string commandBuild = $"copy LargestRectangleInBinaryMatrixPy.py {pathTestCase}";
            string typeLanguage = "OutPy";
            RunTest(commandBuild, pathSourcePy, typeLanguage, commandRun);
            RunCommand($"cd {pathTestCase} ; Remove-Item *.py");
        }
        public static void TestC()
        {

            RunCommand("echo Test C");
            RunCommand($"cd {pathSourceC} ; Remove-Item run ;cd {pathTestCase} ; Remove-Item run");
            string commandRun = "./run ";
            string commandBuild = $"gcc -std=c90 -pedantic -g -rdynamic Source.c -o run ; cp run {pathTestCase}";
            string typeLanguage = "OutC";
            RunTest(commandBuild, pathSourceC, typeLanguage, commandRun);
            RunCommand($"cd {pathSourceC} ; Remove-Item run ;cd {pathTestCase} ; Remove-Item run");
        }
        static void Main(string[] args)
        {
            //RunCommand("ls");
            TestJava();
            TestPy();
            //TestC();
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
