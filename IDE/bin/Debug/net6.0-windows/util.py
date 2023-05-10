from globall import TokenType as tp
import globall
#from scan import reservedWords as rw

import os
fileoutput = open(os.path.join(os.getcwd(),'Archivo_Tokens.txt'),'a')
fileoutputError = open(os.path.join(os.getcwd(),'Archivo_Errores.txt'),'a')

def printToken(token,tokenString):
   
 
    if (token in [tp.MAIN , tp.IF , tp.THEN , tp.ELSE
    , tp.END , tp.DO , tp.WHILE , tp.REPEAT
    , tp.UNTIL , tp.CIN , tp.COUT , tp.REAL
    , tp.INT , tp.BOOLEAN]) :
        print ( "RESERVED-WORD {0}".format(tokenString) )
        write ( "RESERVED-WORD {0}".format(tokenString) )
    elif ( tp.PLUS == token ):

        print( "PLUS\t+" )
        write( "PLUS\t+" )
    elif ( tp.MINUS == token ):
        print( "MINUS\t-" )
        write( "MINUS\t-" )
    elif ( tp.TIMES == token ):
        print( "TIMES\t*" )
        write( "TIMES\t*" )
    elif ( tp.OVER == token ):
        print( "OVER\t/" )
        write( "OVER\t/" )
    elif ( tp.RES == token ):
        print( "RES\t%" )
        write( "RES\t%" )
    elif ( tp.LESST == token ):
        print( "LESST\t<" )
        write( "LESST\t<" )
    elif ( tp.LESSET == token ):
        print( "LESSET\t<=" )
        write( "LESSET\t<=" )
    elif ( tp.GREATERT == token ):
        print( "GREATERT\t>" )
        write( "GREATERT\t>" )
    elif ( tp.GREATERET == token ):
        print( "GREATERET\t>=" )
        write( "GREATERET\t>=" )
    elif ( tp.EQ == token ):
        print( "EQ\t=" )
        write( "EQ\t=" )
    elif ( tp.DIFF == token ):
        print( "DIFF\t<>" )       
        write( "DIFF\t<>" )
    elif( token == tp.ASSIGN):
        print ( "ASSIGN\t:=" )
        write("ASSIGN\t:=")
    elif ( tp.LPAREN == token ):
        print( "LPAREN\t(" )
        write( "LPAREN\t(" )
    elif ( tp.RPAREN == token ):
        print( "RPAREN\t)" )
        write( "RPAREN\t)" )
    elif ( tp.LBPAREN == token ):
        print( "LBPAREN\t{" )
        write( "LBPAREN\t{" )
    elif ( tp.RBPAREN == token ):
        print( "RBPAREN\t}" )
        write( "RBPAREN\t}" )
    elif ( tp.PLUSP == token ):
        print( "PLUSP\t++" )
        write( "PLUSP\t++" )
    elif ( tp.LESSL == token ):
        print( "LESSL\t--" )
        write( "LESSL\t--" )
    elif ( tp.COMMA == token ):
        print( "COMMA\t," )
        write( "COMMA\t," )
    elif ( tp.SEMMICOL == token ):
        print( "SEMMICOL\t;" )
        write( "SEMMICOL\t;" )
    elif ( tp.ID == token ):
        print( "ID\t{}".format(tokenString) )
        write( "ID\t{}".format(tokenString) )

    elif ( tp.ENTERO == token ):
        print( "ENTERO\t{}".format(tokenString) )
        write( "ENTERO\t{}".format(tokenString) )

    elif ( tp.NUMREAL == token ):
        print( "REAL\t{}".format(tokenString) )
        write( "REAL\t{}".format(tokenString) )

    elif ( tp.ENDFILE == token ):
        print( "EOF" )
    elif ( tp.ERROR == token ):
        print( "  {}\t  {}\t  {}".format(tokenString,globall.lineno,globall.colpos) )
        writeErrores("  {}  \t  {}\t  {}".format(tokenString,globall.lineno,globall.colpos))

    else:
        print("UNKNOWN TOKEN\t{}".format(tokenString))
        write("UNKNOWN TOKEN\t{}".format(tokenString))
        

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