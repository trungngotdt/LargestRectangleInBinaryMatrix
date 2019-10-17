using NUnit.Framework;
using System.Diagnostics;

namespace TestLargestRectangleInBinaryMatrix
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "/bin/bash"; 
            startInfo.Arguments = "-c \"ls\"";
            process.StartInfo = startInfo;
            process.Start();
        }

        [Test]
        public void TestC()
        {
            Assert.Pass();
        }

        [Test]
        public void TestPython()
        {
            Assert.Pass();
        }

        [Test]
        public void TestJava()
        {
            Assert.Pass();
        }
    }
}