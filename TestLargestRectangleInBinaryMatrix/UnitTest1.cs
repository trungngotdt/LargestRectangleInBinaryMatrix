using NUnit.Framework;
using System.Diagnostics;

namespace TestLargestRectangleInBinaryMatrix
{
    public class Tests
    {
        private void RunCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "/bin/bash";
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
            RunCommand("cd /root/project/LargestRectangleInBinaryMatrixC");
            Assert.Pass();
        }

        [Test]
        public void TestPython()
        {

            RunCommand("cd /root/project/LargestRectangleInBinaryMatrixPy");
            Assert.Pass();
        }

        [Test]
        public void TestJava()
        {

            RunCommand("cd /root/project/LargestRectangleInBinaryMatrixJava");
            Assert.Pass();
        }
    }
}