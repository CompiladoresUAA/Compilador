from globall import TokenType as tp
import globall
#from scan import reservedWords as rw

import os
<<<<<<< HEAD
fileoutput = open(os.path.join(os.getcwd(),'Archivo_Tokens.txt'),'r+')
#fileoutput.truncate()
fileoutputError = open(os.path.join(os.getcwd(),'Archivo_Errores.txt'),'r+')
#fileoutputError.truncate()
fileoutput2 = open(os.path.join(os.getcwd(),'Archivo_Tokens2.txt'),'r+')
#fileoutput2.truncate()
=======
fileoutput = open(os.path.join(os.getcwd(),'Archivo_Tokens.txt'),'w')
fileoutputError = open(os.path.join(os.getcwd(),'Archivo_Errores.txt'),'w')
fileoutput2 = open(os.path.join(os.getcwd(),'Archivo_Tokens2.txt'),'w')

>>>>>>> 874c9464bb9153011d6e31ed7b4d7f94443caf9a

def printToken(token,tokenString):
   
 
    if (token in [tp.MAIN , tp.IF , tp.THEN , tp.ELSE
    , tp.END , tp.DO , tp.WHILE , tp.REPEAT
    , tp.UNTIL , tp.CIN , tp.COUT , tp.REAL
    , tp.INT , tp.BOOLEAN]) :
        print ( "RESERVED-WORD \t{0}".format(tokenString)+"\t{}\t  {}".format(globall.lineno,globall.colpos) )
        write ( "RESERVED-WORD\t{0}".format(tokenString),"\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.PLUS == token ):

        print( "PLUS\t+\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "PLUS\t+","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.MINUS == token ):
        print( "MINUS\t-\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "MINUS\t-","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.TIMES == token ):
        print( "TIMES\t*\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "TIMES\t*","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.OVER == token ):
        print( "OVER\t/\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "OVER\t/","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.RES == token ):
        print( "RES\t%\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "RES\t%","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.LESST == token ):
        print( "LESST\t<\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "LESST\t<","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.LESSET == token ):
        print( "LESSET\t<=\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "LESSET\t<=","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.GREATERT == token ):
        print( "GREATERT\t>\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "GREATERT\t>","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.GREATERET == token ):
        print( "GREATERET\t>=\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "GREATERET\t>=","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.EQ == token ):
        print( "EQ\t=\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "EQ\t=","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.DIFF == token ):
        print( "DIFF\t<>\t {}\t  {}".format(globall.lineno,globall.colpos) )       
        write( "DIFF\t<>","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif( token == tp.ASSIGN):
        print ( "ASSIGN\t:=\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write("ASSIGN\t:=","\t{}\t{}".format(globall.lineno,globall.colpos))
    elif ( tp.LPAREN == token ):
        print( "LPAREN\t(\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "LPAREN\t(","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.RPAREN == token ):
        print( "RPAREN\t)\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "RPAREN\t)","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.LBPAREN == token ):
        print( "LBPAREN\t{"+"\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "LBPAREN\t{","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.RBPAREN == token ):
        print( "RBPAREN\t"+"}"+"\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "RBPAREN\t"+"}","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.PLUSP == token ):
        print( "PLUSP\t++\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "PLUSP\t++","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.LESSL == token ):
        print( "LESSL\t--\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "LESSL\t--","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.COMMA == token ):
        print( "COMMA\t,\t {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "COMMA\t,","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.SEMMICOL == token ):
        print( "SEMMICOL\t;\t  {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "SEMMICOL\t;","\t{}\t{}".format(globall.lineno,globall.colpos) )
    elif ( tp.ID == token ):
        print( "ID\t {}\t  {}\t  {}".format(tokenString,globall.lineno,globall.colpos) )
        write( "ID\t{}".format(tokenString),"\t{}\t{}".format(globall.lineno,globall.colpos) )

    elif ( tp.ENTERO == token ):
        print( "ENTERO\t{0}".format(tokenString)+"\t  {}\t  {}".format(globall.lineno,globall.colpos) )
<<<<<<< HEAD
        write( "ENTERO\t{0}".format(tokenString),"\t  {}\t  {}".format(globall.lineno,globall.colpos) )
=======
        write( "ENTERO\t{0}".format(tokenString),"\t{}\t{}".format(globall.lineno,globall.colpos) )
>>>>>>> 874c9464bb9153011d6e31ed7b4d7f94443caf9a

    elif ( tp.NUMREAL == token ):
        print( "NUMREAL\t {0}".format(tokenString)+"\t  {}\t  {}".format(globall.lineno,globall.colpos) )
        write( "NUMREAL\t{0}".format(tokenString),"\t{}\t{}".format(globall.lineno,globall.colpos))

    elif ( tp.ENDFILE == token ):
        print( "EOF" )
        write( "EOF\t EOF","\t  {}\t  {}".format(globall.lineno,globall.colpos))
    elif ( tp.ERROR == token ):
        print( "  {}\t  {}\t  {}".format(tokenString,globall.lineno,globall.colpos) )
        writeErrores("  {}  \t  {}\t  {}".format(tokenString,globall.lineno,globall.colpos))

    else:
        print("UNKNOWN TOKEN\t{}".format(tokenString))
        write("UNKNOWN TOKEN\t{}".format(tokenString))
        

def write(text,txt):
    try:
       
        # Procesamiento para escribir en el fichero
        fileoutput.write(text+"\n")
        fileoutput2.write(text+txt+"\n")
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