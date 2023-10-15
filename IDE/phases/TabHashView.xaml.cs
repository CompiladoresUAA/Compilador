using IDE.Common;
using Newtonsoft.Json;
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
    /// Lógica de interacción para TabHashView.xaml
    /// </summary>
    public partial class TabHashView : Page
    {
        private Global.TabHash tabHash;
        public TabHashView()
        {
            InitializeComponent();
            string json = File.ReadAllText("tabHash.json");
            this.tabHash = JsonConvert.DeserializeObject<Global.TabHash>(json) ?? new Global.TabHash();
            this.fillDataGrid();
        }
        public void fillDataGrid()
        {

            foreach(var item in this.tabHash.table)
            {
                Global.Bucket container = item.tab;
                while(container != null)
                {
                    this.dataGrid.Items.Add(new { name = container.name,
                    valor = container.valor,type = container.type,lines = container.lines,meloc = container.meloc});
                    container = container.next;
                }
            }
           
            


        }
    }
}
