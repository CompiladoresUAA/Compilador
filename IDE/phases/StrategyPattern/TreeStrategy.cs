using IDE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IDE.phases.StrategyPattern
{
    interface TreeStrategy
    {
        public TreeViewItem BuildTree(Global.TreeNode node); 
    }
}
