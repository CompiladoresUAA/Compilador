using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IDE.phases
{
    /// <summary>
    /// Lógica de interacción para Sintax.xaml
    /// </summary>
    public partial class Sintax : Window
    {
        public Sintax()
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

        private TreeViewItem BuildTreeViewItem(JToken node)
        {

            TreeViewItem item = new TreeViewItem();
            item.Header = node["name"].ToString();

            foreach (JToken childNode in node["children"])
            {
                TreeViewItem childItem = BuildTreeViewItem(childNode);
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

// Uso: Llamar a este método para expandir todos los nodos del árbol

        // Uso: Llamar a este método para expandir todos los nodos del árbol

    }
}
