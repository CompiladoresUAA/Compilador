from enum import Enum
import os

with open(os.path.join(os.getcwd(),'Archivo_Tokens.txt'), "a") as archivo:
        pass  # No se realiza ninguna operación, simplemente se crea el archivo
with open(os.path.join(os.getcwd(),'Archivo_Errores.txt'), "a") as archivo:
        pass  # No se realiza ninguna operación, simplemente se crea el archivo
with open(os.path.join(os.getcwd(),'Archivo_Tokens2.txt'), "a") as archivo:
        pass  # No se realiza ninguna operación, simplemente se crea el archivo

MAXRESERVEDWORDS = 14

class TokenType(Enum):
    ENDFILE = 1
    ERROR = 2

    ID = 3
    ENTERO = 4
    NUMREAL = 5

    PLUS = 6
    MINUS = 7
    TIMES = 8
    OVER = 9
    RES = 10
    LESST = 11
    LESSET = 12
    GREATERT = 13
    GREATERET = 14
    EQ = 15
    DIFF = 16
    ASSIGN = 17
    LPAREN = 18
    RPAREN = 19
    PLUSP = 20
    LESSL = 21
    COMMA = 22
    SEMMICOL = 23
    LBPAREN = 24
    RBPAREN = 25
    

    MAIN = 26
    IF = 27
    THEN = 28
    ELSE = 29
    END = 30
    DO = 31
    WHILE = 32
    REPEAT = 33
    UNTIL = 34
    CIN = 35
    COUT = 36
    REAL = 37
    INT = 38
    BOOLEAN = 39

    REMAINDER = 40#%
lineno = 0
colpos = 0

#############################################################################
####################                     ####################################
#################### Analisis Sintactico ####################################
####################                     ####################################
#############################################################################
class NodeKind:
    STMTK = 1
    EXPK = 2
    DECK = 3

class StmtKind:
    IFK = 1
    WHILEK = 2
    DOK = 3
    UNTILK = 4
    CINK = 5
    COUTK = 6

class ExpKind:
    OPK = 1
    CONSTK = 2
    IDK = 3

class DecKind:
    INTK = 1
    REALK = 2
    VOIDK = 3
    BOOLEANK = 4

class TreeNode:
    def __init__(self):
        self.child:list = []
        self.sibling:list = []
        self.lineno:int = 0
        self.nodekind:NodeKind = 0
        self.kind:Union[StmtKind, ExpKind, DecKind] = -1
        self.attr:Union[TokenType,int,str] = -1

    #def __init__(self,child,sibling,lineno,nodekind,kind,attr):
    #    self.child:list = child
    #    self.sibling:list = sibling
    #    self.lineno:int = lineno
    #    self.nodekind:NodeKind = nodekind
    #    self.kind:Union[StmtKind, ExpKind, DecKind] = kind
    #    self.attr:Union[TokenType,int,str] = attr
    def toString(self)->str:
        return ''+str(self.lineno)+' '+str(self.nodekind)+' '+str(self.kind)+' '+str(self.attr)



    


