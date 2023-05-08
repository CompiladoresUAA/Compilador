from globall import TokenType as tp
import globall

import os
fileoutput = open(os.path.join(os.getcwd(),'Archivo_Tokens.txt'),'a')
fileoutputError = open(os.path.join(os.getcwd(),'Archivo_Errores.txt'),'a')

def printToken(token,tokenString):
   
 
    if (token in [tp.MAIN , tp.IF , tp.THEN , tp.ELSE
    , tp.END , tp.DO , tp.WHILE , tp.REPEAT
    , tp.UNTIL , tp.CIN , tp.COUT , tp.REAL
    , tp.INT , tp.BOOLEAN]) :
        print ( "RESERVED WORD {0}".format(tokenString) )
        write ( "RESERVED WORD {0}".format(tokenString) )
    elif ( tp.PLUS == token ):
        print( "+" )
        write( "+" )
    elif ( tp.MINUS == token ):
        print( "-" )
        write( "-" )
    elif ( tp.TIMES == token ):
        print( "*" )
        write( "*" )
    elif ( tp.OVER == token ):
        print( "/" )
        write( "/" )
    elif ( tp.RES == token ):
        print( "%" )
        write( "%" )
    elif ( tp.LESST == token ):
        print( "<" )
        write( "<" )
    elif ( tp.LESSET == token ):
        print( "<=" )
        write( "<=" )
    elif ( tp.GREATERT == token ):
        print( ">" )
        write( ">" )
    elif ( tp.GREATERET == token ):
        print( ">=" )
        write( ">=" )
    elif ( tp.EQ == token ):
        print( "=" )
        write( "=" )
    elif ( tp.DIFF == token ):
        print( "<>" )       
        write( "<>" )
    elif( token == tp.ASSIGN):
        print ( ":=" )
        write(":=")
    elif ( tp.LPAREN == token ):
        print( "(" )
        write( "(" )
    elif ( tp.RPAREN == token ):
        print( ")" )
        write( ")" )
    elif ( tp.LBPAREN == token ):
        print( "{" )
        write( "{" )
    elif ( tp.RBPAREN == token ):
        print( "}" )
        write( "}" )
    elif ( tp.PLUSP == token ):
        print( "++" )
        write( "++" )
    elif ( tp.LESSL == token ):
        print( "--" )
        write( "--" )
    elif ( tp.COMMA == token ):
        print( "," )
        write( "," )
    elif ( tp.SEMMICOL == token ):
        print( ";" )
        write( ";" )
    elif ( tp.ID == token ):
        print( "ID, NAME: {}".format(tokenString) )
        write( "ID, NAME: {}".format(tokenString) )

    elif ( tp.ENTERO == token ):
        print( "ENTERO, VALUE: {}".format(tokenString) )
        write( "ENTERO, VALUE: {}".format(tokenString) )

    elif ( tp.NUMREAL == token ):
        print( "REAL, VALUE: {}".format(tokenString) )
        write( "REAL, VALUE: {}".format(tokenString) )

    elif ( tp.ENDFILE == token ):
        print( "EOF" )
    elif ( tp.ERROR == token ):
        print( "ERROR: {} linea: {} columna:{}".format(tokenString,globall.lineno,globall.colpos) )
        writeErrores("ERROR: {} linea: {} columna:{}".format(tokenString,globall.lineno,globall.colpos))

    else:
        print("UNKNOWN TOKEN: {}".format(tokenString))
        write("UNKNOWN TOKEN: {}".format(tokenString))
        

def write(text):
    try:
       
        # Procesamiento para escribir en el fichero
        fileoutput.write(text+"\n")
    except:
        pass
    finally:
        pass

def writeErrores(text):
    try:
       
        # Procesamiento para escribir en el fichero
        fileoutputError.write(text+"\n")
    except:
        pass
    finally:
        pass  