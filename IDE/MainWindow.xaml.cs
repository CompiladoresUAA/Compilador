using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;

enum LNG{LEXICO,SINTACTICO,SEMANTICO,CDGOINTERMEDIO };
namespace IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Border myBorder1;
        private String mnu_lenguajes;
        LNG options;
        SaveFileDialog save_file;
        OpenFileDialog open_file;
        public MainWindow()
        {
            this.save_file = new SaveFileDialog();
            this.open_file = new OpenFileDialog();
            myBorder1 = new Border();
            mnu_lenguajes = "";
            InitializeComponent();
            XmlReader reader = XmlReader.Create("../../../avalonfiles/keywords.xml");//xshd
            codigo.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
        }

        private void eventoLexico(object sender, RoutedEventArgs e)
        {
            /*this.removeBorder();
            this.options=LNG.LEXICO;
            myBorder1.BorderBrush = Brushes.SlateBlue;
            myBorder1.BorderThickness = new Thickness(5, 10, 15, 20);
            myBorder1.Background = Brushes.AliceBlue;
            myBorder1.Padding = new Thickness(5);
            myBorder1.CornerRadius = new CornerRadius(15);
            
            this.setBorder(this.mnulexico);
            
            /*this.option.BorderBrush = Brushes.AntiqueWhite;
            this.option.BorderThickness = new Thickness(1);
            this.option.Background = Brushes.Aqua;
            this.option.FontWeight= FontWeights.Bold;
            this.option.Margin= new Thickness(-2);
            */

            this.options = LNG.LEXICO;
            this.trans.Text = "Lexico";
        }
       
        private void removeBorder()
        {
            MenuItem mnu = new MenuItem();

            if (this.options == LNG.LEXICO)
            {
                mnu = this.mnulexico;
            } else if (this.options == LNG.SINTACTICO)
            {
                mnu = this.mnusintactico;
            }
            else if (this.options == LNG.SEMANTICO)
            {

            }else if (this.options==LNG.CDGOINTERMEDIO)
            {

            }
            else
            {

            }
            mnu.BorderBrush= null;
            mnu.FontWeight = FontWeights.Regular;
            mnu.Background= null;
            mnu.Margin = new Thickness(0);
        }
        private void setBorder(MenuItem option)
        {
            option.BorderBrush = Brushes.AntiqueWhite;
            option.BorderThickness = new Thickness(1);
            option.Background = Brushes.Aqua;
            option.FontWeight = FontWeights.Bold;
            option.Margin = new Thickness(-2);
        }

        private void eventoSintactico(object sender, RoutedEventArgs e)
        {
            this.options = LNG.SINTACTICO;
            this.trans.Text = "Sintactico";
        }

        private void saveCode(object sender, RoutedEventArgs e)
        {
            this.save_file.Filter = "Text file (*.txt)|*.txt";
            if (File.Exists(this.save_file.FileName) || File.Exists(this.open_file.FileName))
            {
                MessageBox.Show("Si existe");
                if(File.Exists(this.save_file.FileName))
                   File.WriteAllText(this.save_file.FileName, this.codigo.Text);
                else
                    File.WriteAllText(this.open_file.FileName, this.codigo.Text);
            }
            else
            {
                MessageBox.Show("No existe");
                if (this.save_file.ShowDialog() == true)
                {
                    this.open_file.FileName = this.save_file.FileName;
                    File.WriteAllText(this.save_file.FileName, this.codigo.Text);

                }
            }
           
        }

        private void openFile(object sender, RoutedEventArgs e)
        {
            if (this.open_file.ShowDialog() == true)
            {

                this.save_file.FileName = this.open_file.FileName;
                this.codigo.Text = File.ReadAllText(this.open_file.FileName);
            }
        }

        private void closeFile(object sender, RoutedEventArgs e)
        {
            
            if (this.open_file.FileName == null || this.open_file.FileName == "")
            {
                if (this.codigo.Text != "")
                {
                    this.save_file.Filter = "Text file (*.txt)|*.txt";

                    if (this.save_file.ShowDialog() == true)
                    {

                       
                        File.WriteAllText(this.save_file.FileName, this.codigo.Text);

                    }
                }
                this.codigo.Text = "";
                this.save_file.FileName = "";
                this.open_file.FileName = "";
                return;
            }
            
            string txt = File.ReadAllText(this.open_file.FileName);
            if (txt == this.codigo.Text)
            {
                
            }
            else
            {
                if(MessageBox.Show("¿Deseas guardar tu archivo?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    File.WriteAllText(this.open_file.FileName, this.codigo.Text);
                    MessageBox.Show("Guardando");

                }

            }
            this.codigo.Text = "";
            this.save_file.FileName = "";
            this.open_file.FileName = "";

        }

        private void saveAs(object sender, RoutedEventArgs e)
        {
            this.save_file.Filter = "Text file (*.txt)|*.txt";

            if (this.save_file.ShowDialog() == true)
            {

                this.open_file.FileName = this.save_file.FileName;
                File.WriteAllText(this.save_file.FileName, this.codigo.Text);

            }
        }

        private void eventoSemantico(object sender, RoutedEventArgs e)
        {
            this.options = LNG.SEMANTICO;
            this.trans.Text = "Semantico";
        }

        private void eventoInter(object sender, RoutedEventArgs e)
        {
            this.options = LNG.CDGOINTERMEDIO;
            this.trans.Text = "Código intermedio";
        }
    }
}
