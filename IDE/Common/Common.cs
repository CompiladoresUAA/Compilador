
using Newtonsoft.Json;
using System.Collections.Generic;
namespace IDE.Common
{
    public static class Global
    {

        public enum nodeKind { STMTK = 1, EXPK }
        public enum stmtKind { IFK = 1, WHILEK, DOK, UNTILK, CINK, COUTK, ASSIGNS, MAINK, DECK, TYPEDEF, ELSEK }
        public enum expKind { OPK = 1, CONSTIK, CONSTFK, IDK,STRINGK }
        public enum decKind { INTK = 1, REALK, VOIDK, BOOLEANK }
        public class TreeNode
        {
            public string valor = "";
            public int nodeKind;
            public int kind;
            public int type;
            public float? valCalc=null;
            public TreeNode? firstChild;
            public TreeNode? secondChild;
            public TreeNode? thirdChild;
            public TreeNode? sibling;


        }
        public class Bucket
        {
            public string? name;
            public int? type;
            public float? valor;
            public string? lines;
            public int meloc;
            public Bucket? next;
        }
        public class TabIndice
        {
            public int indice;
            public Bucket? tab;
        }
        public class TabHash
        {
            [JsonProperty("table")]
            public List<TabIndice>? table;
        }
        public static readonly Dictionary<int,string> tokens = new Dictionary<int, string>(){
        {3, "id"},
        {38, "integer"},
        {37, "real"},
        {6, "+"},
        {7, "-"},
        {8, "*"},
        {9, "/"},
        {40, "%"},
        {41, "string"},
        {11, "<"},
        {12, "<="},
        {13, ">"},
        {14, ">="},
        {15, "="},
        {16, "<>"},
        {17, ":="},
        {18, "("},
        {19, ")"},
        {20, "++"},
        {21, "--"},
        {22, ","},
        {23, ";"},
        {24, "{"},
        {25, "}"}


        };
        public static readonly Dictionary<int, string> decKindDic = new Dictionary<int, string>(){
        {-1, "Null"},
        {1, "int"},
        {2, "real"},
        {3, "void"},
        {4, "boolean"},


        };
    }

    




}