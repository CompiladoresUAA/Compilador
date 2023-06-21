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
using System.Threading;
using IDE.phases;
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
        private CancellationTokenSource cancellationTokenSource;
        private bool iscomp = false;
        private Border myBorder1;
        private String mnu_lenguajes;
        LNG options;
        SaveFileDialog save_file;
        OpenFileDialog open_file;
        Process process;
        Style avalonstyles;
        public int linea { get; set; }
        public int columna {get;set; }
        private MyViewModel values = new MyViewModel();
        private BackgroundWorker worker = new BackgroundWorker();
        private bool isProcessRuning = false;
        private bool isCompilado = false;
        public MainWindow()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            //manejadores...
            worker.DoWork += workerDoWork;
            worker.ProgressChanged += workerProgressChanged;
            worker.RunWorkerCompleted += workerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation= true;
            //worker.RunWorkerAsync();
            //worker.CancelAsync();
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
        private async Task doWorkAsync(CancellationToken cancellationToken)
        {
           

 
            this.isProcessRuning = true;

            TimeSpan totalProccessorTime = DateTime.Now - process.StartTime;
            int i = 0;
            //await process.WaitForExitAsync();

            while (!this.process.HasExited)
            {
                TimeSpan proccessRuningTime = DateTime.Now - process.StartTime;
                double executionPorcentage = (totalProccessorTime.TotalMilliseconds / proccessRuningTime.TotalMilliseconds)*100 ;
                executionPorcentage = Math.Max(0, Math.Min(executionPorcentage, 100));
                //float cpuUsage = cpuCounter.NextValue();
                Debug.WriteLine("Porcentaje {0}%",100- executionPorcentage);
                if (i <= 100) i++;
                progress.Dispatcher.Invoke(new Action(() =>
                {
                    progress.Value = 100-executionPorcentage;

                }));
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(100); 
            }
            
            progress.Dispatcher.Invoke(new Action(() =>
            {
                progress.Value = 100;

            }));
            await Task.Delay(500);
            msgCompila.Dispatcher.Invoke(new Action(() => {
                msgCompila.Visibility = Visibility.Collapsed;
                this.isProcessRuning = false;
            }));        
        }
        private void workerDoWork(object sender ,DoWorkEventArgs e)
        {
            
                this.isProcessRuning = true;
            
            //obtenemos la instancia
            var work = (BackgroundWorker)sender;
            //for (int i = 0; i <= 100; i++)
            //{
            //    if (work.CancellationPending)
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //    Thread.Sleep(500);
            //    work.ReportProgress(i);
            //}
            //progress.Dispatcher.Invoke(new Action(() =>
            //{

            //}));
            TimeSpan totalProccessorTime = DateTime.Now - process.StartTime;
            int i = 0;
            while (!process.HasExited)
            {
                TimeSpan proccessRuningTime = DateTime.Now - process.StartTime;
                double executionPorcentage = (totalProccessorTime.TotalMilliseconds / proccessRuningTime.TotalMilliseconds) * 100;
                executionPorcentage = Math.Max(0, Math.Min(executionPorcentage, 100));
                //float cpuUsage = cpuCounter.NextValue();
                Debug.WriteLine("Porcentaje {0}%", executionPorcentage);
                if (work.CancellationPending)
                {
                       e.Cancel = true;
                       return;
                }
                    if (i <= 100)  i++ ;
                progress.Dispatcher.Invoke(new Action(() =>
                {
                    progress.Value = i;

                }));
                Thread.Sleep(10000);
            }
            progress.Dispatcher.Invoke(new Action(() =>
            {
                progress.Value = 100;

            }));

            System.Threading.Thread.Sleep(500);
            msgCompila.Dispatcher.Invoke(new Action(() => {
                msgCompila.Visibility= Visibility.Collapsed;
                this.isProcessRuning = false;
            }));
            
        }

            static void workerProgressChanged(object sender ,ProgressChangedEventArgs e)
        {
            //actualiza progreso
            Debug.WriteLine("progreso {0}", e.ProgressPercentage);
        }
        static void workerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            if( e.Cancelled)
            {
                //se cancelo
                Debug.WriteLine("se cancela");
            }else if ( e.Error != null)
            {
                Debug.WriteLine("Error: {0}", e.Error.Message);
            }
            else
            {
                Debug.WriteLine("Resultado: {0}", e.Result);
            }
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
        
        private string  lecturaArchivo(string file)
        {
            string read = File.ReadAllText(file);
            return read;
        }

        private async void compilarFileSource(object sender, RoutedEventArgs e)
        {

            if (!this.isProcessRuning)
            {
                msgCompila.Visibility = Visibility.Visible;
                
                
                if (this.open_file.FileName != "")
                {
                    File.WriteAllText(this.open_file.FileName, this.codigo.Text);

                }
                else if (this.save_file.FileName != "")
                {
                    File.WriteAllText(this.save_file.FileName, this.codigo.Text);
                }
                else
                {
                    this.saveAs(new object(), new RoutedEventArgs());
                }
                string pathArch = Directory.GetCurrentDirectory(),
                pathError = pathArch + "\\Archivo_Errores.txt",
                pathToken = pathArch + "\\Archivo_Tokens.txt";
                string path = Directory.GetCurrentDirectory();


                string r = "/c python " + path + "\\scan.py " + this.open_file.FileName;
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

               // process.WaitForExit();

                // PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName, true);
                //progress.Visibility = Visibility.Visible;

                this.cancellationTokenSource = new CancellationTokenSource();
                try
                {
                    await this.doWorkAsync(this.cancellationTokenSource.Token);
                    Debug.WriteLine("acabe...");
                    //aqui va el sintaxis
                    this.iscomp = true;
                    try
                    {
                        await doSintax(new CancellationTokenSource().Token);
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.WriteLine("se cancelo");
                        this.iscomp = false;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        //limpia el cancellation token source
                    }


                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("se cancelo");
                    this.iscomp = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    //limpia el cancellation token source
                    msgCompila.Dispatcher.Invoke(new Action(() => {
                        msgCompila.Visibility = Visibility.Collapsed;
                        this.isProcessRuning = false;
                    }));
                    this.cancellationTokenSource.Dispose();
                }


                // progress.Value= 100;
                //string output = process.StandardOutput.ReadToEnd();
                Debug.WriteLine("saliiiiii");
                //               process.WaitForExit();
                //progress.Visibility = Visibility.Collapsed;
            }
        }
        private async Task doSintax(CancellationToken cancellationToken)
        {
            SintaxAnalyzer obj = new SintaxAnalyzer();
            obj.doAnalisis();
            Sintax sin = new Sintax();

            sin.Show();
            await Task.Delay(500);

        }
        private void eventoLexico(object sender, RoutedEventArgs e)
        {
            if ( !this.iscomp)
            {
                MessageBox.Show("Compila primero.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
           // this.iscomp = false;
            this.trans.Text = "";
            this.feedback.Text ="Error\tFila\tColumna\n";
            this.options = LNG.LEXICO;



            string pathArch = Directory.GetCurrentDirectory(),
            pathError = pathArch + "\\Archivo_Errores.txt",
            pathToken = pathArch + "\\Archivo_Tokens.txt";

           
 //           MessageBox.Show(pathToken+"\n"+pathError);
            this.trans.Text += this.lecturaArchivo(pathToken);
            this.feedback.Text += this.lecturaArchivo(pathError);
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

        private void cancelaCompila(object sender, RoutedEventArgs e)
        {
            if (this.isProcessRuning)
            {
                this.cancellationTokenSource.Cancel();
                this.iscomp = false;
            }
            //this.worker.CancelAsync();
            this.isProcessRuning = false;
            Debug.WriteLine("boton...");
        }
    }
}
