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
    TabSymA.write("\nSymbo Table:\n\n")
    printSymtab()
    closeFiles()
    #TabSymA.close()

def typeError(t:TreeNode, message:str):
    #imprimir errores Semanticos en un archivo de texto
    ErrorSem.write("Type error at line: {0} message: {1}\n".format(t.lineno,message))

#####################################################################
## AUN NO TERMINADA EL CHECKNODE ####################################
#####################################################################
def checkNode(t:TreeNode):
    if t.getNodeKind() == NodeKind.EXPK.value:
        if t.getKind() == ExpKind.OPK.value:
            h0:TreeNode = t.getChild(0)
            h1:TreeNode = t.getChild(1)
            if (( h0.getType() is not DecKind.INTK.value ) or 
                (h1.getType() is not DecKind.INTK.value)):
                typeError(t,"Op applied to non-int")
            elif (( h0.getType() is not DecKind.REALK.value ) or 
                (h1.getType() is not DecKind.REALK.value)):
                pass



    elif t.getNodeKind() == NodeKind.STMTK.value:
        if t.getKind() == StmtKind.IFK.value:
            pass


def typeCheck(syntaxTree:TreeNode):
    traverse(syntaxTree,nullProc,checkNode)

def postEval(t:TreeNode):
    temp=0
    if(t.getKind() == ExpKind.OPK):
        postEval(t.getChild(0)) #izq
        postEval(t.getChild(1)) #der
        if(t.getAttr()==TokenType.PLUS):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.setAttr(lchild.getAttr() + rchild.getAttr())
        elif(t.getAttr()==TokenType.MINUS):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.setAttr(lchild.getAttr() - rchild.getAttr())
        elif(t.getAttr()==TokenType.TIMES):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.setAttr(lchild.getAttr() * rchild.getAttr())
        elif(t.getAttr()==TokenType.OVER):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.setAttr(lchild.getAttr() / rchild.getAttr())
        elif(t.getAttr()==TokenType.RES):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.setAttr(lchild.getAttr() % rchild.getAttr())
    
def evalType(t:TreeNode):
    if(t.getNodeKind() == NodeKind.STMTK.value):
        lchild:TreeNode = t.getChild(0)
        rchild:TreeNode = t.getChild(1)
        evalType(lchild)
        rchild.setType(lchild.getType)
        evalType(rchild)

    elif(t.getNodeKind()): # type
        pass

    elif(t.getNodeKind()): # id
        
        pass