
using System.Collections.Generic;

namespace IDE.Common
{
    public static class Global
    {
        public static readonly Dictionary<int,string> tokens = new Dictionary<int, string>(){
        {3, "id"},
        {38, "integer"},
        {37, "real"},
        {6, "+"},
        {7, "-"},
        {8, "*"},
        {9, "/"},
        {40, "%"},
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
    }

    




}