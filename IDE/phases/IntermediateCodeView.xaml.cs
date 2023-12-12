using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace IDE.phases
{
    /// <summary>
    /// Lógica de interacción para IntermediateCodeView.xaml
    /// </summary>
    /// 
    public class ItemInterCode
    {
        public string Procedure { get; set; }
    }

    public partial class IntermediateCodeView : Page
    {
        private string fileToRead;

        public IntermediateCodeView()
        {
            InitializeComponent();
            this.fileToRead = @"intermediateCode.tm";
            this.fillDataGrid();
        }

        public async void fillDataGrid()
        {

            using (StreamReader readerObj = new StreamReader(this.fileToRead))
            {
                string line;
                char[] spearator = { '\t' };
                while ((line = readerObj.ReadLine()) != null)
                {
                    //string[] lines = line.Split(spearator);
                    /*Debug.WriteLine(lines[0]);
                    Debug.WriteLine(lines[1]);
                    Debug.WriteLine(lines[2]);
                    Debug.WriteLine(lines[3]);*/

                    if (line[0] == ' ')
                    {
                        this.dataGrid.Items.Add(new ItemInterCode() { Procedure = line });
                    }
                    
                    //this.dataGrid.Items.Add(new ItemInterCode() { Procedure = line});
                    await Task.Delay(10);

                }
            }


        }

    }
}
