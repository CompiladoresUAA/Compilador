using Newtonsoft.Json.Linq;
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
    /// Lógica de interacción para SintaxView.xaml
    /// </summary>
    public partial class SintaxView : Page
    {
        public SintaxView()
        {
            InitializeComponent();
            
            string currentDirectory = Environment.CurrentDirectory;
            Debug.WriteLine("Current Directory: " + currentDirectory);

            string json = File.ReadAllText("tree_data.json");
            // Analizar el contenido JSON
            JToken rootNode = JToken.Parse(json);

            // Construir el árbol
            TreeViewItem rootItem = BuildTreeViewItem(rootNode);

            // Agregar el nodo raíz al TreeView
            treeView.Items.Add(rootItem);
            
            foreach (object item in treeView.Items)
            {
                if (item is TreeViewItem treeViewItem)
                {
                    ExpandAllTreeViewItems(treeViewItem);
                }
            }
        }
        public static string readErrors()
        {
            int a=1,b=2;
            
            string errors = File.ReadAllText("Errores_Sintaxis.txt");
            return errors;
        }
        private TreeViewItem BuildTreeViewItem(JToken node)
        {

            TreeViewItem item = new TreeViewItem();
            item.Foreground = new SolidColorBrush(Colors.White);
            item.Header = node["name"].ToString();

            foreach (JToken childNode in node["children"])
            {
                TreeViewItem childItem = BuildTreeViewItem(childNode);
                childItem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20fa8d")); 
                item.Items.Add(childItem);
            }

            return item;
        }

        private void ExpandAllTreeViewItems(TreeViewItem item)
        {
            item.IsExpanded = true;
            foreach (object childItem in item.Items)
            {
                if (childItem is TreeViewItem childTreeViewItem)
                {
                    ExpandAllTreeViewItems(childTreeViewItem);
                }
            }
        }
    }
}
