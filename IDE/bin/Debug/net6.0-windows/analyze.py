import os
from symtab import *
from globall import TokenType
from globall import TreeNode
from globall import StmtKind,ExpKind,DecKind,NodeKind
from globall import MAXCHILDREN
location = 0
TracerAnalizer = True
ErrorSem = open(os.path.join(os.getcwd(),'Archivo_ErrorSem.txt'),'w')
TabSymA = open(os.path.join(os.getcwd(),'Archivo_TabSym.txt'),'w')

def closeFiles():
    ErrorSem.close()
    TabSymA.close()

@staticmethod
def traverse(t:TreeNode, preProc:TreeNode, postProc:TreeNode):
    if(t != None):
        preProc(t)
        i=0
        for i in range(MAXCHILDREN):
            traverse(t.child[i],preProc,postProc)
        
        postProc(t)
        traverse(t.getSibling(),preProc,postProc)

@staticmethod
def nullProc(t:TreeNode):
    if(t==None):
        return
    else:
        return

@staticmethod
def insertNode(t:TreeNode):
    global location
    if(t.getNodeKind() == NodeKind.STMTK.value):
        if(t.getKind() == StmtKind.ASSIGNS or 
           t.getKind() == StmtKind.CINK.value):
            
            if(st_lookup(t.getAttr()) == -1):
                st_insert(t.getAttr(),t.lineno,location=+1)
            else:
                st_insert(t.getAttr(),t.lineno,0)
        
    elif (t.getNodeKind() == NodeKind.EXPK.value):
        if(t.getNodeKind() == ExpKind.IDK):
            
            if(st_lookup(t.getAttr()) == -1):
                st_insert(t.getAttr(),t.lineno,location=+1)
            else:
                st_insert(t.getAttr(),t.lineno,0)

def buildSymtab(syntaxTree:TreeNode)->None:
    traverse(syntaxTree,insertNode,nullProc)
    if(TracerAnalizer):
        TabSymA.write("\nSymbo Table:\n\n")
        printSymtab()

    #TabSymA.close()

def typeError(t:TreeNode, message:str):
    #imprimir errores Semanticos en un archivo de texto
    ErrorSem.write("Type error at line: {0} message: {1}\n".format(t.lineno,message))

def checkNode(t:TreeNode):
    if t.getNodeKind() == NodeKind.EXPK.value:
        pass
    elif t.getNodeKind() == NodeKind.STMTK.value:
        pass
    elif t.getNodeKind() == NodeKind.EXPK.value: 
        pass
    elif t.getNodeKind() == NodeKind.EXPK.value:
        pass


def typeCheck(syntaxTree:TreeNode):
    traverse(syntaxTree,nullProc,checkNode)
