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
                if findNode(child.getAttr()) != None:
                    ErrorSem.write(f"Variable {child.getAttr()} was already declared as {findNode(child.getAttr()).getTipo()}\n")
                    child = child.getSibling()
                    continue
                    
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
    
def findNode(var:str)->BucketList:
     hashKey = hash(var)
     bList:BucketList = hashTable[hashKey]
     while bList != None and not(var == bList.name):
        bList = bList.getNext()
     if bList != None:
        return bList
     return None

def typeError(t:TreeNode, message:str):
    ErrorSem.write("Type error at line: {0} message: {1}\n".format(t.lineno,message))

def checkNode(t:TreeNode):
    if t.getNodeKind().value == NodeKind.EXPK.value:
        if t.getKind() == ExpKind.OPK.value:
            h0:TreeNode = t.getChild(0)
            h1:TreeNode = t.getChild(1)
            if (h0.getType() != DecKind.INTK.value and h0.getType() != DecKind.REALK.value) or (h1.getType() != DecKind.INTK.value and h1.getType() != DecKind.REALK.value):
                ErrorSem.write(f"Op applied to non-int or non-real {t.getType()} {t.lineno} {h0.getAttr()} {h0.getType()} {h1.getAttr()} {h1.getType()}\n")
            if t.getAttr() == TokenType.DIFF.value or t.getAttr() == TokenType.EQ.value or t.getAttr() == TokenType.LESST.value or t.getAttr() == TokenType.LESSET.value or t.getAttr() == TokenType.GREATERT.value or t.getAttr() == TokenType.GREATERET.value:
                t.setType(DecKind.BOOLEANK.value)
            else:
                if h0.getType() == DecKind.REALK.value or h1.getType() == DecKind.REALK.value:
                    t.setType(DecKind.REALK.value)
                else:
                    t.setType(DecKind.INTK.value)
                postEval(t)    
        elif t.getKind() == ExpKind.CONSTIK.value:
            
            t.setType(DecKind.INTK.value)
        elif t.getKind() == ExpKind.CONSTFK.value:
            t.setType(DecKind.REALK.value)
        elif t.getKind() == ExpKind.IDK.value:
            bList:BucketList = findNode(t.getAttr())
            if bList != None:
                t.setType(bList.getTipo())

    elif t.getNodeKind().value == NodeKind.STMTK.value:
        h0:TreeNode = t.getChild(0)
        h1:TreeNode = t.getChild(1)    
        if t.getKind() == StmtKind.IFK.value:
            if h0.getType() == DecKind.INTK.value or h0.getType() == DecKind.REALK.value:
                ErrorSem.write(f"If test is not Boolean\n")
        elif t.getKind() == StmtKind.ASSIGNS.value:
           
            bList:BucketList = findNode(t.getAttr())
            if bList != None:
                if bList.getTipo() == DecKind.INTK.value:
                    if h0.getType() != DecKind.INTK.value:
                        ErrorSem.write(f"Assign of non-int to int {t.getAttr()} line {t.lineno}\n")
                    else:
                        bList.setValor(h0.valueCalc)
                        t.valueCalc = h0.valueCalc
                elif bList.getTipo() == DecKind.REALK.value:
                    if h0.getType() != DecKind.INTK.value and h0.getType() != DecKind.REALK.value:
                        ErrorSem.write(f"Assign of non-int or non-real to real {t.getAttr()} line {t.lineno}\n")
                    else:
                        bList.setValor(h0.valueCalc)
                        t.valueCalc = h0.valueCalc
        elif t.getKind() == StmtKind.COUTK.value:
            #Falta consultar tabla...
            if h0.getKind() == ExpKind.IDK.value:
                node = findNode( h0.getAttr() )
                h0.setType(node.getTipo()) if node != None else h0.setType(-1)
            
            if h0.getType() != DecKind.INTK.value and h0.getType() != DecKind.REALK.value:
                ErrorSem.write(f"Write of non-int or non-real value\n")
        elif t.getKind() == StmtKind.UNTILK.value:
            if h1.getType() == DecKind.INTK.value or h1.getType() == DecKind.REALK.value:
                ErrorSem.write(f"Until Test is not Boolean\n")
        elif t.getKind() == StmtKind.WHILEK.value:
            if h0.getType() == DecKind.INTK.value or h0.getType() == DecKind.REALK.value:
                ErrorSem.write(f"While Test is not Boolean\n")
def typeCheck(syntaxTree:TreeNode):
    traverse(syntaxTree,nullProc,checkNode)
def calcExp(sintaxTree:TreeNode):
    traverse(sintaxTree,nullProc,postEval)
def postEval(t:TreeNode):
    temp=0
    if( t.getNodeKind().value == NodeKind.EXPK.value and t.getKind() == ExpKind.OPK.value):
        if(t.getAttr()==TokenType.PLUS.value):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            if lchild.getKind() == ExpKind.IDK.value:
                node = findNode(lchild.getAttr())
                if node != None:
                    lchild.valueCalc = node.valor
            if rchild.getKind() == ExpKind.IDK.value:    
                node = findNode(rchild.getAttr())
                if node != None:
                    rchild.valueCalc = node.valor
            t.valueCalc = lchild.valueCalc + rchild.valueCalc
        elif(t.getAttr()==TokenType.MINUS.value):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.valueCalc = lchild.valueCalc - rchild.valueCalc
        elif(t.getAttr()==TokenType.TIMES.value):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.valueCalc = lchild.valueCalc * rchild.valueCalc
        elif(t.getAttr()==TokenType.OVER.value):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.valueCalc = lchild.valueCalc / rchild.valueCalc
        elif(t.getAttr()==TokenType.RES.value):
            lchild:TreeNode = t.getChild(0)
            rchild:TreeNode = t.getChild(1)
            t.valueCalc = lchild.valueCalc % rchild.valueCalc