using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDE.phases
{
    internal class SintaxAnalyzer
    {
        private string fileTokens;
        private string pathArch = Directory.GetCurrentDirectory();
        public SintaxAnalyzer()
        {
            this.fileTokens = this.pathArch + "\\Archivo_Tokens2.txt";
        }
        public Process doAnalisis()
        {
            string pathArch = Directory.GetCurrentDirectory(),
            pathError = pathArch + "\\Archivo_Errores.txt",
            pathToken = pathArch + "\\Archivo_Tokens.txt";
            string path = Directory.GetCurrentDirectory();
            Process process;

            string r = "/c python " + path + "\\parse.py ";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = r,
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();

            return process;
        }

    }
}
