from code_ import *
from globall import NodeKind,StmtKind,TreeNode, ExpKind, TokenType
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
    global tmpOffset
    loc = 0
    p1, p2 = None, None
    if tree.kind == ExpKind.CONSTFK.value:
        if TraceCode: 
            emitComment("-> Const")

        emitRM("LDF",ac,tree.attr,0,"load const") # LDF es una carga de una constante REAL (Float)
        if TraceCode: 
            emitComment("<- Const")

        pass
    elif tree.kind == ExpKind.CONSTIK.value:
        if TraceCode: 
            emitComment("-> Const")

        emitRM("LDC",ac,tree.attr,0,"load const")
        if TraceCode: 
            emitComment("<- Const")

    elif tree.kind == ExpKind.IDK.value:
        if TraceCode:
            emitComment("-> Id")
        loc = st_lookup(tree.attr)
        emitRM("LD",ac,loc,gp,"load id value")
        if TraceCode:
            emitComment("<- Id")

    elif tree.kind == ExpKind.OPK.value: # Start OpK
        if TraceCode:
            emitComment("-> Op")
        p1 = tree.child[0]
        p2 = tree.child[1]

        cGen(p1)

        emitRM("ST",ac,tmpOffset,mp,"op: push left") # tmpOffset--
        tmpOffset-=1
        
        cGen(p2)
        
        tmpOffset += 1
        emitRM("LD",ac1,tmpOffset,mp,"op: load left") # ++tmpOffset

        if ( tree.attr == TokenType.PLUS.value):
            emitRO("ADD",ac,ac1,ac,"op +")
        
        elif tree.attr == TokenType.MINUS.value:
            emitRO("SUB",ac,ac1,ac,"op -")
        
        elif tree.attr == TokenType.TIMES.value:
            emitRO("MUL",ac,ac1,ac,"op *")
        
        elif tree.attr == TokenType.OVER.value:
            emitRO("DIV",ac,ac1,ac,"op /")
        
        elif tree.attr == TokenType.RES.value:
            emitRO("RES",ac,ac1,ac,"op %")

        elif tree.attr == TokenType.LESST.value:
            emitRO("SUB",ac,ac1,ac,"op <")
            emitRM("JLT",ac,2,pc,"br if true")
            emitRM("LDC",ac,0,ac,"false case")
            emitRM("LDA",pc,1,pc,"unconditional jmp") 
            emitRM("LDC",ac,1,ac,"true case")

        elif tree.attr == TokenType.LESSET.value:
            emitRO("SUB",ac,ac1,ac,"op <=")
            emitRM("JLE",ac,2,pc,"br if true")
            emitRM("LDC",ac,0,ac,"false case")
            emitRM("LDA",pc,1,pc,"unconditional jmp") 
            emitRM("LDC",ac,1,ac,"true case")
        
        elif tree.attr == TokenType.EQ.value:
            emitRO("SUB",ac,ac1,ac,"op =")
            emitRM("JEQ",ac,2,pc,"br if true")
            emitRM("LDC",ac,0,ac,"false case")
            emitRM("LDA",pc,1,pc,"unconditional jmp")
            emitRM("LDC",ac,1,ac,"true case")

        elif tree.attr == TokenType.GREATERT.value:
            emitRO("SUB",ac,ac1,ac,"op >")
            emitRM("JGT",ac,2,pc,"br if true")
            emitRM("LDC",ac,0,ac,"false case")
            emitRM("LDA",pc,1,pc,"unconditional jmp") 
            emitRM("LDC",ac,1,ac,"true case")

        elif tree.attr == TokenType.GREATERET.value:
            emitRO("SUB",ac,ac1,ac,"op >=")
            emitRM("JGE",ac,2,pc,"br if true")
            emitRM("LDC",ac,0,ac,"false case")
            emitRM("LDA",pc,1,pc,"unconditional jmp")
            emitRM("LDC",ac,1,ac,"true case")

        elif tree.attr == TokenType.DIFF.value:
            emitRO("SUB",ac,ac1,ac,"op <>")
            emitRM("JNE",ac,2,pc,"br if true")
            emitRM("LDC",ac,0,ac,"false case")
            emitRM("LDA",pc,1,pc,"unconditional jmp")
            emitRM("LDC",ac,1,ac,"true case")

        else:
            emitComment("BUG: Unknow operator")

        if TraceCode:
            emitComment("<- Op")
        
        # OpK end



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

