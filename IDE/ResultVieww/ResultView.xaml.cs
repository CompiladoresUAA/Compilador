using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDE.ResultVieww
{
    /// <summary>
    /// Lógica de interacción para ResultView.xaml
    /// </summary>
    public partial class ResultView : Page
    {
        private Process procedure;
        private string oText = "";
        public ResultView()
        {
            
            InitializeComponent();
            shell.Text = "";
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = @"tm.exe";
            startInfo.Arguments = "intermediateCode.tm";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            procedure = new Process();
            procedure.StartInfo = startInfo;
            procedure.OutputDataReceived += Proc_OutputDataReceived;
            procedure.Start();
            procedure.BeginOutputReadLine();
            shell.Focus();
        }
    
        private void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            shell.Dispatcher.Invoke(() =>
            {
                shell.Text += e.Data+"\n" ;
                oText = shell.Text;
                shell.SelectionStart = shell.Text.Length;
            });
        }
        private void sendCommandtoC(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
             
                string aux = shell.Text;
                var strs = shell.Text.Split("\n");
                Debug.WriteLine(shell.Text.Split("\n").Last().ToString());
                //shell.Text += "\n";
                //shell.SelectionStart = shell.Text.Length;
                procedure.StandardInput.WriteLine(shell.Text.Split("\n").Last().ToString());
                shell.Text += '\n';
            }
        }
    }
}
