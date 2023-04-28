# ------------------------------------------------------------
# calclex.py
#
# tokenizer for a simple expression evaluator for
# numbers and +,-,*,/
# ------------------------------------------------------------
import ply.lex as lex
import sys

# List of token names.   This is always required
tokens = (
   'HEX_NUMBER',
   'DEC_NUMBER',
   'PLUS',
   'MINUS',
   'TIMES',
   'DIVIDE',
   'LPAREN',
   'RPAREN',
   'IDENTIFIER',
   'zero'
)

# Regular expression rules for simple tokens

t_IDENTIFIER =  r'[a-zA-Z][a-zA-Z0-9_]*'
t_PLUS    = r'\+'
t_MINUS   = r'-'
t_TIMES   = r'\*'
t_DIVIDE  = r'/'
t_LPAREN  = r'\('
t_RPAREN  = r'\)'

# A regular expression rule with some action code
def t_DEC_NUMBER(t):
    r'[1-9][0-9]*'
    t.value = int(t.value)
    return t
def t_HEX_NUMBER(t):
    r'0[xX][0-9a-fA-F]+'
    t.value = int(t.value, 16)
    return t
# Define a rule so we can track line numbers
def t_newline(t):
    r'\n+'
    t.lexer.lineno += len(t.value)
def t_zero(t):
    r'0'
    t.value = int(t.value)
    return t
# A string containing ignored characters (spaces and tabs)
t_ignore  = ' \t'

precedence = (
    ('left', 'HEX_NUMBER'),
    ('left', 'DEC_NUMBER')
)
# Error handling rule
def t_error(t):
    print("Illegal character '%s'" % t.value[0])
    t.lexer.skip(1)

# Build the lexer
lexer = lex.lex()
# Test it out
data = '''
3 + 4 * 10
  + -20 *2
'''

# Give the lexer some input
lexer.input(data)

# Test it out
data = '''
3 + 4 * 10
  + -20 *2
'''

# Give the lexer some input
#lexer.input(data)

def leer_archivo(nombre_archivo):
    with open(nombre_archivo) as archivo:
        contenido = archivo.read()
        lexer = lex.lex()
        lexer.input(contenido)
        while True:
            tok = lexer.token()
            if not tok:
                break
            print(tok)
            print(tok.lineno)

# Ejemplo de uso
if len(sys.argv) > 1:
    archivo = sys.argv[1]
    leer_archivo(archivo)
else:
    print("No se proporciono archivo de entrada")

# Tokenize
#while True:
#    tok = lexer.token()
#    if not tok: break      # No more input
#    print (tok)