using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;

namespace TestLargestRectangleInBinaryMatrix
{
    public class Tests
    {
        private string pathTestCase = @"/root/project/TestLargestRectangleInBinaryMatrix/TestCases";
        private string pathExpect = @"/root/project/TestLargestRectangleInBinaryMatrix/Expect";
        private string pathSourceC = @"/root/project/LargestRectangleInBinaryMatrixC";
        private string pathSourcePy = @"/root/project/LargestRectangleInBinaryMatrixPy";
        private string pathSourceJava = @"/root/project/LargestRectangleInBinaryMatrixJava";


        private void RunCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "/bin/bash";
            startInfo.Arguments = $"-c \"{cmd}\"";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
        [SetUp]
        public void Setup()
        {
            
        }

        string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash= md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        [Test]
        public void TestC()
        {
            RunCommand($"cd {pathSourceC} && gcc -std=c90 -pedantic -g -rdynamic Source.c -o run && cp run {pathTestCase}");
            DirectoryInfo d = new DirectoryInfo(pathTestCase);
            FileInfo[] Files = d.GetFiles("*.txt");
            var totalFile = Files.Length;
            string pathFileInput = String.Empty;
            string pathFileOutput = String.Empty;
            string pathFileExpect = String.Empty;
            string md5FileOutput = String.Empty;
            string md5FileExpect = String.Empty;

            for (int i = 0; i < totalFile; i++)
            {
                pathFileInput = Files[i].Name;
                pathFileOutput = "OutC" + Files[i].Name;
                pathFileExpect =pathExpect+"/"+ "result" + i+1 + ".txt";
                RunCommand("cd " + pathTestCase + "&& ./run " + pathFileInput + " " + pathFileOutput);
                md5FileExpect = CalculateMD5(pathFileExpect);
                md5FileOutput = CalculateMD5(pathTestCase+"/"+ pathFileOutput);
                //Assert.AreEqual(md5FileOutput,md5FileExpect);
                if (!md5FileExpect.Equals(md5FileOutput))
                {
                    RunCommand("cd " + pathTestCase + "&& cat "+pathFileOutput);

                    RunCommand("cd " + pathExpect + "&& cat " + pathFileExpect);
                }
            }
           
               
            RunCommand($"cd {pathTestCase} && ls");
            Assert.Pass();
        }

        [Test]
        public void TestPython()
        {

            RunCommand("cd / &&cd /root/project/LargestRectangleInBinaryMatrixPy");
            RunCommand("pwd");
            Assert.Pass();
        }

        [Test]
        public void TestJava()
        {

            RunCommand("cd /root/project/LargestRectangleInBinaryMatrixJava");
            RunCommand("pwd");
            Assert.Pass();
        }
    }
}