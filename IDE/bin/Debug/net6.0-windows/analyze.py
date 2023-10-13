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
    if(t.getNodeKind().value == NodeKind.STMTK.value):
        
        if ( t.getKind() == StmtKind.DECK.value ):
            
            child:TreeNode = t.getChild(1)
            while child != None:
               
                st_insert(child.getAttr(),t.getChild(0).getType(),"",t.lineno,location)
                location+=1
                child = child.getSibling()
        elif(t.getKind() == StmtKind.ASSIGNS.value or 
           t.getKind() == StmtKind.CINK.value):
    
            if(st_lookup(t.getAttr()) != -1):
                st_insert(t.getAttr(),t.getType(),"",t.lineno,location)
                location+=1
            else:
                ErrorSem.write(f"Undeclared variable {t.getAttr()}\n")
        
    elif (t.getNodeKind().value == NodeKind.EXPK.value):
        if(t.getKind() == ExpKind.IDK):
            
            if(st_lookup(t.getAttr()) != -1):
                st_insert(t.getAttr(),t.getType(),"",t.lineno,location)
                
def buildSymtab(syntaxTree:TreeNode)->None:
    traverse(syntaxTree,insertNode,nullProc)
    TabSymA.write("\nSymbo Table:\n\n")
    printSymtab()
    closeFiles()
    TabSymA.close()

def typeError(t:TreeNode, message:str):
    #imprimir errores Semanticos en un archivo de texto
    ErrorSem.write("Type error at line: {0} message: {1}\n".format(t.lineno,message))

#####################################################################
## AUN NO TERMINADA EL CHECKNODE ####################################
#####################################################################
def checkNode(t:TreeNode):
    if t.getNodeKind().value == NodeKind.EXPK.value:
        if t.getKind() == ExpKind.OPK.value:
            h0:TreeNode = t.getChild(0)
            h1:TreeNode = t.getChild(1)
            if h0.getType().value != DecKind.INTK.value or h0.getType().value != DecKind.REALK.value or h1.getType().value != DecKind.INTK.value or h1.getType().value != DecKind.REALK.value:
                ErrorSem.write(f"Op applied to non-int or non-real")
            if t.getAttr() == TokenType.DIFF.value or t.getAttr() == TokenType.EQ.value or t.getAttr() == TokenType.LESST.value or t.getAttr() == TokenType.LESSET.value or t.getAttr() == TokenType.GREATERT.value or t.getAttr() == TokenType.GREATERET.value:
                t.setType(DecKind.BOOLEANK.value)
            else:
                if h0.getType().value == DecKind.REALK.value or h1.getType() == DecKind.REALK.value:
                    t.setType(DecKind.REALK.value)
                else:
                    t.setType(DecKind.INTK.value)
        elif t.getKind() == ExpKind.CONSTIK.value:
            t.setType(DecKind.INTK.value)
        elif t.getKind() == ExpKind.CONSTFK.value:
            t.setType(DecKind.REALK.value)
        elif t.getKind() == ExpKind.IDK.value:
            hashKey = hash(t.getAttr())
            bList:BucketList = hashTable[hashKey]
            while bList != None and not(t.getAttr() == bList.name):
                bList = bList.getNext()
            if bList != None:
                t.setType(bList.getTipo())



    elif t.getNodeKind().value == NodeKind.STMTK.value:
        print( f"Assign : {t.getKind()}" )
        h0:TreeNode = t.getChild(0)
        h1:TreeNode = t.getChild(1)    
        if t.getKind() == StmtKind.IFK.value:
            if h0.getType().value == DecKind.INTK.value or h0.getType().value == DecKind.REALK.value:
                ErrorSem.write(f"If test is not Boolean")
        elif t.getKind() == StmtKind.ASSIGNS.value:
            hashKey = hash(h0.getAttr())
            bList:BucketList = hashTable[hashKey]
            while bList != None and not(h0.getAttr() == bList.name):
                bList = bList.getNext()
            if bList != None:
                if bList.getTipo() == DecKind.INTK.value:
                    if h0.getType().value == DecKind.REALK.value:
                        ErrorSem.write(f"Assign of non-int to int")
                    
        elif t.getKind() == StmtKind.COUTK.value:
            if h0.getType().value != DecKind.INTK.value or h0.getType().value != DecKind.REALK.value:
                ErrorSem.write(f"Write of non-int or non-real value")
        elif t.getKind() == StmtKind.UNTILK.value:
            if h1.getType().value == DecKind.INTK.value or h1.getType().value == DecKind.REALK.value:
                ErrorSem.write(f"Until Test is not Boolean")
        elif t.getKind() == StmtKind.WHILEK.value:
            if h0.getType().value == DecKind.INTK.value or h0.getType().value == DecKind.REALK.value:
                ErrorSem.write(f"While Test is not Boolean")
        

def typeCheck(syntaxTree:TreeNode):
    traverse(syntaxTree,nullProc,checkNode)

def postEval(t:TreeNode):
    temp=0
    if(t.getKind() == ExpKind.OPK.value):
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