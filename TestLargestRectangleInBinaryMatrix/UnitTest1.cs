using NUnit.Framework;
using System.Diagnostics;
using System.IO;

namespace TestLargestRectangleInBinaryMatrix
{
    public class Tests
    {
        private string pathTestCase = @"/root/project/TestLargestRectangleInBinaryMatrix/TestCases";

        private void RunCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = " / bin/bash";
            startInfo.Arguments = $"-c \"{cmd}\"";
            process.StartInfo = startInfo;
            process.Start();
        }
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestC()
        {
            RunCommand("gcc --std=c90 --pedantic -g -rdynamic /root/project/LargestRectangleInBinaryMatrixC/Source.c -o run");
            DirectoryInfo d = new DirectoryInfo(pathTestCase);
            FileInfo[] Files = d.GetFiles("*.txt"); 
            string str = "";
            foreach (FileInfo file in Files)
            {
                RunCommand($"cd /root/project/LargestRectangleInBinaryMatrixC && ./run {pathTestCase+"/"+ file.Name} {pathTestCase + "/" + "OutC" +file.Name} ");
            }
            RunCommand($"cd /root/project/TestLargestRectangleInBinaryMatrix/TestCases && ls");


            Assert.Pass();
        }

        [Test]
        public void TestPython()
        {

            RunCommand("cd /root/project/LargestRectangleInBinaryMatrixPy");
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