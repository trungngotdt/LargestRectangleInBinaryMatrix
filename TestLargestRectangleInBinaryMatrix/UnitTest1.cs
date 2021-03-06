using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;

namespace TestLargestRectangleInBinaryMatrix
{
    public class Tests
    {
        private string pathTestCase = @"/root/project/TestLargestRectangleInBinaryMatrix/TestCases";
        private string pathExpect = @"/root/project/TestLargestRectangleInBinaryMatrix/Expect";
        private string pathSourceC = @"/root/project/LargestRectangleInBinaryMatrixC";
        private string pathSourcePy = @"/root/project/LargestRectangleInBinaryMatrixPy";
        private string pathSourceJava = @"/root/project/LargestRectangleInBinaryMatrixJava/src/com/company";


        private void RunCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "/bin/bash";
            startInfo.Arguments = $"-c \"{cmd}\"";
            startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            
            process.Start();
            string error = process.StandardError.ReadToEnd();
            if (!(String.IsNullOrEmpty(error)||String.IsNullOrWhiteSpace(error)))
            {
                RunCommand($"echo 'Error : {error}'");
            }
            process.WaitForExit();
        }
        [SetUp]
        public void Setup()
        {
        }
        
        private string CreateRowTableString(string[] para)
        {
            StringBuilder builder = new StringBuilder();
            int length = para.Count();
            for (int i = 0; i < length; i++)
            {
                builder.Append("|");
                var countChar = para[i].Count();
                int padLeft = (12 - countChar) / 2;
                int padRight = ((12 - countChar) % 2) == 0 ? padLeft : padLeft + 1;
                builder.Append(para[i].PadLeft(padLeft+countChar).PadRight(12));
            }
            builder.Append("|");
            
            return builder.ToString();
        }
        private string ReadFileContent(string path)
        {
            var lines = File.ReadAllLines(path);
            StringBuilder builder = new StringBuilder();
            foreach (var item in lines)
            {
                builder.Append(item);
            }
            return builder.ToString();
        }
        private string CreateBarTableString(int size)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                builder.Append("-");
            }

            return builder.ToString();
        }

        void RunTest(string commandBuild,string pathSourceCode,string typeLanguage,string commandRunProgram)
        {

            RunCommand($"cd {pathTestCase} && rm Out*");
            RunCommand($"cd {pathSourceCode} && {commandBuild}");
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
                pathFileInput = "test" +index + ".txt";// Files[i].Name;
                //RunCommand($"echo {pathFileInput}");
                pathFileOutput = typeLanguage + Files[i].Name; //"OutC" + Files[i].Name;
                
                pathFileExpect = pathExpect + "/" + "result" + index + ".txt";
                try
                {
                    RunCommand("cd " + pathTestCase + "&&" + commandRunProgram + pathFileInput + " " + pathFileOutput);
                    var testResult = File.ReadAllLines(pathFileExpect).SequenceEqual(File.ReadAllLines(pathTestCase + "/" + pathFileOutput));
                    para.AddRange( new string[] { pathFileInput, (i + 1).ToString(), testResult.ToString(),"" });
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
                    para.AddRange(  new string[] { pathFileInput, (i + 1).ToString(), "False",ex.ToString() });
                    var row = CreateRowTableString(para.ToArray());
                    RunCommand($"echo '{row}'");
                    countFalse++;
                }
            }
            RunCommand($"echo {bar}");
            Assert.IsTrue(countFalse == 0);
        }

        [Test]
        [Order(3)]
        public void TestJava()
        {

            RunCommand("echo Test Java");
            RunCommand($"cd {pathSourceJava} && rm *.class && cd {pathTestCase} && rm *.class");
            string commandRun = "java Main ";
            string commandBuild = $"javac Main.java && cp Main.class {pathTestCase}";
            string typeLanguage = "OutJava";
            RunTest(commandBuild, pathSourceJava, typeLanguage, commandRun);
            RunCommand($"cd {pathSourceJava} && rm *.class");
            RunCommand($"cd {pathTestCase} && rm *.class");
        }


        [Test]
        [Order(1)]
        public void TestPy()
        {

            RunCommand("echo Test Py");
            RunCommand($"cd {pathTestCase} && rm *.py");
            string commandRun = "python3  main.py ";
            string commandBuild = $"cp main.py {pathTestCase}";
            string typeLanguage = "OutPy";
            RunTest(commandBuild, pathSourcePy, typeLanguage, commandRun);
            RunCommand($"cd {pathTestCase} && rm *.py");
        }


        //[Test]
        //[Order(4)]
        public void TestHieuPy()
        {

            RunCommand("echo Hieu Test Py");
            RunCommand($"cd {pathTestCase} && rm *.py");
            string commandRun = "python3  Main.py ";//LargestRectangleInBinaryMatrixPy.py ";
            string commandBuild = $"cp Main.py {pathTestCase}";//LargestRectangleInBinaryMatrixPy.py {pathTestCase}";
            string typeLanguage = "OutPy";
            RunTest(commandBuild, pathSourcePy, typeLanguage, commandRun);
            RunCommand($"cd {pathTestCase} && rm *.py");
        }

        [Test]
        [Order(1)]
        public void TestC()
        {

            RunCommand("echo Test C");
            RunCommand($"cd {pathSourceC} && rm run &&cd {pathTestCase} && rm run");
            string commandRun = "./run ";
            string commandBuild = $"gcc -std=c90 -pedantic -g -rdynamic main.c -o run && cp run {pathTestCase}";
            string typeLanguage = "OutC";
            RunTest(commandBuild, pathSourceC, typeLanguage, commandRun);
            RunCommand($"cd {pathSourceC} && rm run &&cd {pathTestCase} && rm run");
        }

    }
}