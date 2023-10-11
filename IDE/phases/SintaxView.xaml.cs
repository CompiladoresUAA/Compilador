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
using IDE.Common;
namespace IDE.phases
{
    enum nodeKind { STMTK=1, EXPK}
    enum stmtKind { IFK=1,WHILEK,DOK,UNTILK,CINK,COUTK,ASSIGNS,MAINK,DECK,TYPEDEF,ELSEK}
    enum expKind { OPK = 1,CONSTIK,CONSTFK,IDK}
    enum decKind { INTK=1,REALK,VOIDK,BOOLEANK}
    public class TreeNode
    {
        public string valor = "";
        public int nodeKind;
        public int kind;
        public int type;
        public TreeNode ?firstChild;
        public TreeNode ?secondChild;
        public TreeNode ?thirdChild;
        public TreeNode ?sibling;


    }
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

            string json = File.ReadAllText("arbol.json");
            // Analizar el contenido JSON
            JToken rootNode = JToken.Parse(json);
            TreeNode root = JsonConvert.DeserializeObject<TreeNode>(json) ?? new TreeNode();
            // Construir el árbol
            TreeViewItem rootItem = BuildTreeViewItem(root);

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
        private TreeViewItem BuildTreeViewItem(TreeNode node)
        {

            TreeViewItem item = new TreeViewItem();
            item.Foreground = new SolidColorBrush(Colors.White);
            string value = "";
            Debug.WriteLine(node.sibling);
            if (node != null)
            {
                switch (node.nodeKind)
                {
                    case (int)nodeKind.STMTK:
                        switch (node.kind)
                        {
                            case (int)stmtKind.IFK:
                                value = "IF";
                                break;
                            case (int)stmtKind.WHILEK:
                                value = "WHILE";
                                break;
                            case (int)stmtKind.DOK:
                                value = "DO";
                                break;
                            case (int)stmtKind.UNTILK:
                                value = "UNTIL";
                                break;
                            case (int)stmtKind.CINK:
                                value = $"READ: {node.valor}";
                                break;
                            case (int)stmtKind.COUTK:
                                value = $"WRITE {node.valor}";
                                break;
                            case (int)stmtKind.ASSIGNS:
                                value = "ASSIGN TO: " + node.valor;
                                break;
                            case (int)stmtKind.MAINK:
                                value = "MAIN";
                                break;
                            case (int)stmtKind.DECK:
                                value = "DEC";
                                break;
                            case (int)stmtKind.TYPEDEF:
                                value = "TYPE";
                                break;
                            case (int)stmtKind.ELSEK:
                                value = "ELSE";
                                break;
                            default:
                                value = "Unknown stmt node";
                                break;

                        }
                        break;

                    case (int)nodeKind.EXPK:

                        switch (node.kind)
                        {
                            case (int)expKind.OPK:
                                value = Global.tokens[ int.Parse(node.valor) ];
                                break;
                            case (int)expKind.CONSTIK:
                                value = node.valor;
                                break;
                            case (int)expKind.CONSTFK:
                                value = node.valor;
                                break;
                            case (int)expKind.IDK:
                                value = node.valor;
                                break;
                        }
                        break;
                    default:
                        value = "Unknown node kind";
                        break;
                }
                item.Header = value;
                List<TreeNode> children = new List<TreeNode>();
                if (node.firstChild != null) children.Add(node.firstChild);
                if (node.secondChild != null) children.Add(node.secondChild);
                if (node.thirdChild != null) children.Add(node.thirdChild);
                foreach (TreeNode childNode in children)
                {
                    TreeViewItem childItem = BuildTreeViewItem(childNode);
                    childItem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20fa8d"));
                    item.Items.Add(childItem);

                    TreeNode sibling = childNode.sibling;
                    while (sibling != null)
                    {
                        TreeViewItem sib = BuildTreeViewItem(sibling);
                        sib.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20fa8d"));
                        item.Items.Add(sib);
                        sibling = sibling.sibling;

                    }

                }


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
