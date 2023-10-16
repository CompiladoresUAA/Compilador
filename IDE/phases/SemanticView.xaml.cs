using IDE.Common;
using IDE.phases.StrategyPattern;
using Newtonsoft.Json;
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
    /// Lógica de interacción para SemanticView.xaml
    /// </summary>
    public partial class SemanticView : Page
    {
        private TreeContext treeContext;
        public SemanticView()
        {
            InitializeComponent();
            this.treeContext = new TreeContext(new SemanticStrategy());
            
            string jsonTree = File.ReadAllText("arbol.json");
            // Analizar el contenido JSON
            Global.TreeNode root = JsonConvert.DeserializeObject<Global.TreeNode>(jsonTree) ?? new Global.TreeNode();

           
            TreeViewItem rootItem = this.treeContext.BuildTree(root);

            // Agregar el nodo raíz al TreeView
            treeView.Items.Add(rootItem);

            foreach (object item in treeView.Items)
            {
                if (item is TreeViewItem treeViewItem)
                {
                    this.treeContext.ExpandAllTreeViewItems(treeViewItem);
                }
            }

        }
    }
}
