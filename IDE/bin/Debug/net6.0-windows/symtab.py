SIZE = 211
SHIFT = 4


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
    def __init__(self, lineno, next) -> None:
        self.lineno:int = lineno
        self.next:LineList = [None]
    
    def getLineno(self):
        return self.lineno
    def setLineno(self,lineno):
        self.lineno = lineno

    def getNext(self):
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
    l:BucketList = hashTable[h]
    while ((l != None) and not(name == l.name)):
        l = l.next
    if(l == None):
        l = BucketList()
        l.setName(name)
        l.lines = LineList()
        l.lines.setLineno(lineno)
        l.setMeloc(loc)
        l.lines.setNext(None)
        l.setNext(hashTable[h])
        hashTable[h] = l
    else:
        t:LineList = l.getLines()
        while (t.next != None):
            t = t.getNext()
        t.next = LineList()
        t.next.setLineno(lineno)
        t.next.setNext(None)


