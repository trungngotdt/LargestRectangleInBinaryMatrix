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

        [Test]
        public void TestC()
        {
            RunCommand($"cd /root/project/LargestRectangleInBinaryMatrixC && gcc -std=c90 -pedantic -g -rdynamic Source.c -o run && cp run {pathTestCase}");
            DirectoryInfo d = new DirectoryInfo(pathTestCase);
            FileInfo[] Files = d.GetFiles("*.txt"); 
            string str = "";
            foreach (FileInfo file in Files)
            {
                string input=file.Name+".txt";
                string output = "OutC" + file.Name + ".txt";
                RunCommand("cd "+pathTestCase +"&& ./run "+input+" "+output);
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