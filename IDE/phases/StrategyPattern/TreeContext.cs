using IDE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace IDE.phases.StrategyPattern
{
    class TreeContext
    {
        private TreeStrategy treeStrategy;
        public TreeContext(TreeStrategy treeStrategy)
        {
            this.treeStrategy = treeStrategy;
        }
        public void SetStrategy(TreeStrategy strategy)
        {
            this.treeStrategy = strategy;
        }
        public TreeViewItem BuildTree(Global.TreeNode node)
        {
            return this.treeStrategy.BuildTree(node);
        }
        public void ExpandAllTreeViewItems(TreeViewItem item)
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
