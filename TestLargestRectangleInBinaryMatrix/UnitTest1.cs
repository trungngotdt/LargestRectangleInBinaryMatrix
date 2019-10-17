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
        private string pathExcept = @"/root/project/TestLargestRectangleInBinaryMatrix/Except";
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
            string pathFileinput = String.Empty;
            string pathFileoutput = String.Empty;
            string pathFileexpect = String.Empty;
            string md5Fileoutput = String.Empty;
            string md5Fileexpect = String.Empty;

            for (int i = 0; i < totalFile; i++)
            {
                pathFileinput = Files[i].Name;
                pathFileoutput = "OutC" + Files[i].Name;
                pathFileexpect =pathExcept+"/"+ "result" + i + ".txt";
                RunCommand("cd " + pathTestCase + "&& ./run " + pathFileinput + " " + pathFileoutput);
                md5Fileexpect = CalculateMD5(pathFileexpect);
                md5Fileoutput = CalculateMD5(pathTestCase+ pathFileoutput);
                Assert.AreEqual(md5Fileoutput,md5Fileexpect);
                if (!md5Fileexpect.Equals(md5Fileoutput))
                {
                    RunCommand("cd " + pathTestCase + "&& cat "+pathFileoutput);

                    RunCommand("cd " + pathExcept + "&& cat " + pathFileexpect);
                }
            }
            foreach (FileInfo file in Files)
            {
               
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