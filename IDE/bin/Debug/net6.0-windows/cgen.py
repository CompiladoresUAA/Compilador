from code_ import *
from globall import NodeKind,StmtKind,TreeNode
from symtab import st_lookup
tmpOffset = 0

def genStmt(tree:TreeNode):
    p1, p2, p3 = None, None, None
    savedLoc1, savedLoc2, currentLoc = 0, 0, 0
    loc = 0
    if tree.kind == StmtKind.IFK.value:
        if TraceCode:
            emitComment("-> if")
        p1 = tree.child[0]
        p2 = tree.child[1]
        p3 = tree.child[2]
        
        cGen(p1)
        savedLoc1 = emitSkip(1)
        emitComment("if: jump to else belongs here")
        
        cGen(p2)
        savedLoc2 = emitSkip(1)
        emitComment("if: jump to end belongs here")
        currentLoc = emitSkip(0)
        emitBackup(savedLoc1)
        emitRM_Abs("JEQ", ac, currentLoc, "if: jmp to else")
        emitRestore()
        
        cGen(p3)
        currentLoc = emitSkip(0)
        emitBackup(savedLoc2)
        emitRM_Abs("LDA", pc, currentLoc, "jmp to end")
        emitRestore()
        if TraceCode:
            emitComment("<- if")
        if tree.kind == StmtKind.ASSIGNS.value:
            if TraceCode:
                emitComment("-> assign")
            
            cGen(tree.child[0])
            
            loc = st_lookup(tree.attr)
            emitRM("ST", ac, loc, gp, "assign: store value")
            
            if TraceCode:
                emitComment("<- assign")




def genExp(tree:TreeNode):
    pass



def codeGen(syntaxTree, codefile):
    s = "File: " + codefile
    openFile(codefile)
    emitComment("TINY Compilation to TM Code")
    emitComment(s)
    
    emitComment("Standard prelude:")
    emitRM("LD", mp, 0, ac, "load maxaddress from location 0")
    emitRM("ST", ac, 0, ac, "clear location 0")
    emitComment("End of standard prelude.")
    
    cGen(syntaxTree)
    
    emitComment("End of execution.")
    emitRO("HALT", 0, 0, 0, "")
    closeFile(codefile)



def cGen(tree):
    if tree is not None:
        if tree.nodekind == NodeKind.STMTK.value:
            genStmt(tree)
        elif tree.nodekind == NodeKind.EXPK.value:
            genExp(tree)
        cGen(tree.sibling)

