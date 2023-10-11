using System;
using System.Collections.Generic;
using System.IO;
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

namespace IDE.ErrorViews
{
    /// <summary>
    /// Lógica de interacción para ErrorsView.xaml
    /// </summary>
    public partial class ErrorsView : Page
    {
        private string _errors;
        public ErrorsView()
        {
            _errors = "";
            InitializeComponent();
            fillTextBox();
        }
        public string readSintaxFile()
        {
            return File.ReadAllText("Errores_Sintaxis.txt");
        }
        public string readLexFile()
        {
            return File.ReadAllText("Archivo_Errores.txt");
        }
        public void fillTextBox()
        {
            _errors += readLexFile() + readSintaxFile();
            textBox.Text = _errors;
        }
    }
}
