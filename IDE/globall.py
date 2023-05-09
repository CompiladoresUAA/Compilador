from enum import Enum
import os

rutaToken = os.path.join(os.getcwd(),'Archivo_Tokens.txt')
rutaError =os.path.join(os.getcwd(),'Archivo_Errores.txt')

if os.path.exists(rutaToken):
    os.remove(rutaToken)
    #print("Removio el Token")

if os.path.exists(rutaError):
    os.remove(rutaError)
    #print("Removio el Errores")

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