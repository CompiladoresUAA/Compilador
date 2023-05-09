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
using System.Diagnostics;
using ICSharpCode.AvalonEdit.Document;
using System.ComponentModel;
//using System.Windows.Forms;

enum LNG{LEXICO,SINTACTICO,SEMANTICO,CDGOINTERMEDIO };
namespace IDE
{
    public class MyViewModel : INotifyPropertyChanged
    {
        private int _linea=1;
        private int _columna=1;
        public int Linea
        {
            get { return _linea; }
            set
            {
                _linea = value;
                OnPropertyChanged(nameof(Linea));
            }
        }
        public int Columna
        {
            get { return _columna; }
            set
            {
                _columna = value;
                //nameof regresa el nombre como tal de la variable, en este caso 'Columna'
                OnPropertyChanged(nameof(Columna));
            }
        }
        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
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

        Style avalonstyles;
        public int linea { get; set; }
        public int columna {get;set; }
        private MyViewModel values = new MyViewModel();
        public MainWindow()
        {
            linea = 0;
            columna = 0;

            
            DataContext = values;
            this.save_file = new SaveFileDialog();
            this.open_file = new OpenFileDialog();
            myBorder1 = new Border();
            mnu_lenguajes = "";
            InitializeComponent();
            XmlReader reader = XmlReader.Create("../../../avalonfiles/keywords.xml");//xshd
            codigo.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            //this.avalonstyles = (Style)Application.Current.FindResource("avalonstyles");
            this.codigo.FontSize = 12;
            this.feedback.FontSize = 12;
            this.trans.FontSize = 12;
            codigo.TextArea.Caret.PositionChanged += onCursorPositionChanged;

        }
        private void onCursorPositionChanged(object sender,EventArgs e)
        {
            int caretOffset = codigo.TextArea.Caret.Offset;

            // Obtener la línea correspondiente a la posición del cursor
            DocumentLine caretLine = codigo.Document.GetLineByOffset(caretOffset);
            linea = caretLine.LineNumber;
            columna = caretOffset - caretLine.Offset + 1;

            values.Linea = linea;
            values.Columna = columna;
            // Imprimir la línea y la columna actuales en la consola
            Debug.WriteLine("Línea: {0}, Columna: {1}", caretLine.LineNumber, caretOffset - caretLine.Offset + 1);
        }
        private void eventoLexico(object sender, RoutedEventArgs e)
        {

            if(this.open_file.FileName != "" )
            {
                File.WriteAllText(this.open_file.FileName, this.codigo.Text);

            }else if( this.save_file.FileName != "" )
            {
                File.WriteAllText(this.save_file.FileName,this.codigo.Text);
            }
            else
            {
                this.saveAs(new object (),new RoutedEventArgs ());
            }
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
            string path = Directory.GetCurrentDirectory();
            string r = "/c python " + path.Remove(path.Length - 25, 25) + "\\scan.py " + this.open_file.FileName;
            //MessageBox.Show("Ruta: "+r);/*Revisa la ruta a ejecutar del python*/
            Debug.WriteLine("r-> " + r);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                //Arguments = "/c python C:\\Users\\kris_\\OneDrive\\Documentos\\Compiladores\\IDE\\scan.py "+this.open_file.FileName,
                Arguments = r,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow= true,
            };
            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
          

            this.options = LNG.LEXICO;
            this.trans.Text = output;
            Debug.WriteLine("out-> " + output);
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

        private void lightTheme(object sender, RoutedEventArgs e)
        {
            // avalonstyles.Setters.Remove(avalonstyles.Setters.First(s => s.Equals(TextEditor.BackgroundProperty) ));
            this.trans.Background = Brushes.White;
            this.trans.Foreground = Brushes.Black;
            this.feedback.Background= Brushes.White;
            this.feedback.Foreground = Brushes.Black;
            this.codigo.Background = Brushes.White;
            this.codigo.Foreground = Brushes.Black;
            XmlReader reader = XmlReader.Create("../../../avalonfiles/keywords2.xml");
            codigo.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);


        }

        private void tamSource(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Entre -> ");
        }

        private void darkTheme(object sender, RoutedEventArgs e)
        {
            this.trans.Background = new BrushConverter().ConvertFromString("#2E2E2E") as Brush; 
            this.trans.Foreground = Brushes.White;
            this.feedback.Background = new BrushConverter().ConvertFromString("#2E2E2E") as Brush;
            this.feedback.Foreground = Brushes.White;
            this.codigo.Background = new BrushConverter().ConvertFromString("#2E2E2E") as Brush;
            this.codigo.Foreground = Brushes.White;

            XmlReader reader2 = XmlReader.Create("../../../avalonfiles/keywords.xml");
            codigo.SyntaxHighlighting = HighlightingLoader.Load(reader2, HighlightingManager.Instance);





        }

        private void tamPequenio(object sender, RoutedEventArgs e)
        {
            this.codigo.FontSize = 12;
            this.feedback.FontSize = 12;
            this.trans.FontSize = 12;
        }

        private void tamMediana(object sender, RoutedEventArgs e)
        {
            this.codigo.FontSize = 20;
            this.feedback.FontSize = 20;
            this.trans.FontSize = 20;
        }

        private void tamGrande(object sender, RoutedEventArgs e)
        {
            this.codigo.FontSize = 30;
            this.feedback.FontSize = 30;
            this.trans.FontSize = 30;
        }

        private void updatePosition(object sender, KeyEventArgs e)
        {
           
        }
    }
}
