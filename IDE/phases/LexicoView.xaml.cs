using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDE.phases
{
    /// <summary>
    /// Lógica de interacción para LexicoView.xaml
    /// </summary>
    /// 
    public class Item
    {
        public string Token { get; set; }
        public string Lexema { get; set; }
    }
    public partial class LexicoView : Page
    {
        private string fileToRead;
        public LexicoView()
        {
            InitializeComponent();
            this.fileToRead = @"Archivo_Tokens.txt";
            this.fillDataGrid();

        }
        
        public async void fillDataGrid()
        {
            
                    using (StreamReader readerObj = new StreamReader(this.fileToRead))
                    {
                        string line;
                        char[] spearator = { '\t', ' ' };
                        while ((line = readerObj.ReadLine()) != null)
                        {
                            string[] lines = line.Split(spearator);
                            Debug.WriteLine(lines[0]);
                            Debug.WriteLine(lines[1]);
                            this.dataGrid.Items.Add(new Item() { Token = lines[0], Lexema = lines[1] });
                            await Task.Delay(10);

                        }
                    }

                
         }
    }
}
