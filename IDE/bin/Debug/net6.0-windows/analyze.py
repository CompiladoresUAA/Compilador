from symtab import *
from globall import TokenType
from globall import TreeNode
from globall import StmtKind,ExpKind,DecKind,NodeKind
from globall import MAXCHILDREN
location = 0

@staticmethod
def traverse(t:TreeNode, preProc:TreeNode, postProc:TreeNode):
    if(t != None):
        preProc(t)
        i=0
        for i in range(MAXCHILDREN):
            traverse(t.child[i],preProc,postProc)
        
        postProc(t)
        traverse(t.sibling,preProc,postProc)

@staticmethod
def nullProc(t:TreeNode):
    if(t==None):
        return
    else:
        return


