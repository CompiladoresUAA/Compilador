using IDE.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace IDE.phases.StrategyPattern
{
    class SintaxStrategy : TreeStrategy
    {
        public TreeViewItem BuildTree(Global.TreeNode node)
        {
            TreeViewItem item = new TreeViewItem();
            item.Foreground = new SolidColorBrush(Colors.White);
            string value = "";
            Debug.WriteLine(node.sibling);
            if (node != null)
            {
                switch (node.nodeKind)
                {
                    case (int)Global.nodeKind.STMTK:
                        switch (node.kind)
                        {
                            case (int)Global.stmtKind.IFK:
                                value = "IF";
                                break;
                            case (int)Global.stmtKind.WHILEK:
                                value = "WHILE";
                                break;
                            case (int)Global.stmtKind.DOK:
                                value = "DO";
                                break;
                            case (int)Global.stmtKind.UNTILK:
                                value = "UNTIL";
                                break;
                            case (int)Global.stmtKind.CINK:
                                value = $"READ: {node.valor}";
                                break;
                            case (int)Global.stmtKind.COUTK:
                                value = $"WRITE {node.valor}";
                                break;
                            case (int)Global.stmtKind.ASSIGNS:
                                value = "ASSIGN TO: " + node.valor;
                                break;
                            case (int)Global.stmtKind.MAINK:
                                value = "MAIN";
                                break;
                            case (int)Global.stmtKind.DECK:
                                value = "DEC";
                                break;
                            case (int)Global.stmtKind.TYPEDEF:
                                value = $"TYPE {Global.decKindDic[node.type]}";
                                break;
                            case (int)Global.stmtKind.ELSEK:
                                value = "ELSE";
                                break;
                            default:
                                value = "Unknown stmt node";
                                break;

                        }
                        break;

                    case (int)Global.nodeKind.EXPK:

                        switch (node.kind)
                        {
                            case (int)Global.expKind.OPK:
                                value = Global.tokens[int.Parse(node.valor)];
                                break;
                            case (int)Global.expKind.CONSTIK:
                                value = node.valor;
                                break;
                            case (int)Global.expKind.CONSTFK:
                                value = node.valor;
                                break;
                            case (int)Global.expKind.IDK:
                                value = node.valor;
                                break;
                        }
                        break;
                    default:
                        value = "Unknown node kind";
                        break;
                }
                item.Header = value;
                List<Global.TreeNode> children = new List<Global.TreeNode>();
                if (node.firstChild != null) children.Add(node.firstChild);
                if (node.secondChild != null) children.Add(node.secondChild);
                if (node.thirdChild != null) children.Add(node.thirdChild);
                foreach (Global.TreeNode childNode in children)
                {
                    TreeViewItem childItem = BuildTree(childNode);
                    childItem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20fa8d"));
                    item.Items.Add(childItem);

                    Global.TreeNode sibling = childNode.sibling;
                    while (sibling != null)
                    {
                        TreeViewItem sib = BuildTree(sibling);
                        sib.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20fa8d"));
                        item.Items.Add(sib);
                        sibling = sibling.sibling;

                    }

                }


            }


            return item;
        }
    }
}
