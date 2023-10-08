import os

SIZE = 211
SHIFT = 4
TabSym = open(os.path.join(os.getcwd(),'Archivo_TabSym.txt'),'a')

@staticmethod
def hash(key)->int:
    global SHIFT
    global SIZE
    temp = 0
    i = 0
    while (i < len(key)):
        temp = ((temp << SHIFT) + ord(key[i])) % SIZE
        i+=1

    return temp

class LineList:
    def __init__(self, lineno) -> None:
        self.lineno:int = lineno
        self.next:LineList = [None]
    
    def getLineno(self)->None:
        return self.lineno
    def setLineno(self,lineno):
        self.lineno = lineno

    def getNext(self)->None:
        return self.next
    def setNext(self,next):
        self.next = next
    
class BucketList:
    def __init__(self,name,meloc) -> None:
        self.name:str = name
        self.lines:LineList = [None]
        self.meloc:int = meloc
        self.next:BucketList = [None]
    
    def setName(self,name):
        self.name = name
    def getName(self):
        return self.name
    
    def setLines(self,lines:LineList):
        self.lines = lines
    def getLines(self)->LineList:
        return self.lines
    
    def setMeloc(self,meloc:int):
        self.meloc = meloc
    def getMeloc(self)->int:
        return self.meloc
    
    def setNext(self,next):
        self.next = next
    def getNext(self):
        return self.next

hashTable:BucketList = [None]*SIZE

def st_insert(name:str, lineno:int, loc:int):
    h:int = hash(name)
    #h:int=100
    l:BucketList = hashTable[h]
    while ((l != None) and not(name == l.name)):
        l = l.getNext()
    if(l == None):
        l = BucketList(name,loc)
        #l.setName(name)
        l.lines = LineList(lineno)
        #l.lines.setLineno(lineno)
        #l.setMeloc(loc)
        l.lines.setNext(None)
        l.setNext(hashTable[h])
        hashTable[h] = l
    else:
        t:LineList = l.getLines()
        while (t.next != None):
            t = t.getNext()
        t.next = LineList(lineno)
        #t.next.setLineno(lineno)
        t.next.setNext(None)

def st_lookup(name:str)->int:
    h:int = hash(name)
    l:BucketList = hashTable[h]
    while((l != None) and not(name==l.getName())):
        l = l.getNext()
    if(l == None):
        return -1
    else:
        return l.getMeloc()

def printSymtab()->None:
    #Definir la funcion de manadar a imprimir a un archivo de texto
    i = 0
    TabSym.write("Variable_Name\tLocation\tLine_Numbers\n")
    for i in range(SIZE):
        if hashTable[i] is not None:
            l:BucketList = hashTable[i]
            while l is not None:
                t:LineList = l.getLines()
                TabSym.write(str(l.getName())+"\t")
                TabSym.write(str(l.getMeloc())+"\t")
                
                while t is not None:
                    TabSym.write(str(t.getLineno())+"-")
                    t = t.getNext()
                
                TabSym.write("\n")
                l = l.getNext()


####  PRUEBAS  ####
""" 
j=0
st_insert("hola",10,0)
for r in hashTable:
    print(str(r)+" -> "+str(j))
    
    j+=1 
lin:LineList = hashTable[100].getLines()
print(str(hashTable[100].getName()))
print(str(lin.getLineno())) """