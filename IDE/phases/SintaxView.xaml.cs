﻿using Newtonsoft.Json;
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
using IDE.phases.StrategyPattern;

namespace IDE.phases
{
    
    /// <summary>
    /// Lógica de interacción para SintaxView.xaml
    /// </summary>
    public partial class SintaxView : Page
    {
        private TreeContext treeContext;     
        public SintaxView()
        {
            InitializeComponent();
            this.treeContext = new TreeContext(new SintaxStrategy());
            string currentDirectory = Environment.CurrentDirectory;
            Debug.WriteLine("Current Directory: " + currentDirectory);

            string json = File.ReadAllText("arbol.json");
            // Analizar el contenido JSON
            JToken rootNode = JToken.Parse(json);
            Global.TreeNode root = JsonConvert.DeserializeObject<Global.TreeNode>(json) ?? new Global.TreeNode();
            
            // Construir el árbol
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
        public static string readErrors()
        {
            int a=1,b=2;
            
            string errors = File.ReadAllText("Errores_Sintaxis.txt");
            return errors;
        }
     
    }
}
