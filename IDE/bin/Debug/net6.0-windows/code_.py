
pc = 7

mp = 6

gp = 5

ac = 0

ac1 = 1
#endif

code = None

TraceCode = True
emitLoc = 0
highEmitLoc = 0

def emitComment(c):
    if TraceCode:
        code.write("* %s\n" % c)

def emitRO(op, r, s, t, c):
    code.write("%3d:  %5s  %d,%d,%d " % (emitLoc, op, r, s, t))
    emitLoc += 1
    if TraceCode:
        code.write("\t%s" % c)
    code.write("\n")
    if highEmitLoc < emitLoc:
        highEmitLoc = emitLoc

def emitRM(op, r, d, s, c):
    code.write("%3d:  %5s  %d,%d(%d) " % (emitLoc, op, r, d, s))
    emitLoc += 1
    if TraceCode:
        code.write("\t%s" % c)
    code.write("\n")
    if highEmitLoc < emitLoc:
        highEmitLoc = emitLoc

def emitSkip(howMany):
    i = emitLoc
    emitLoc += howMany
    if highEmitLoc < emitLoc:
        highEmitLoc = emitLoc
    return i

def emitBackup(loc):
    if loc > highEmitLoc:
        emitComment("BUG in emitBackup")
    emitLoc = loc

def emitRestore():
    emitLoc = highEmitLoc

def emitRM_Abs(op, r, a, c):
    code.write("%3d:  %5s  %d,%d(%d) " % (emitLoc, op, r, a-(emitLoc+1), pc))
    emitLoc += 1
    if TraceCode:
        code.write("\t%s" % c)
    code.write("\n")
    if highEmitLoc < emitLoc:
        highEmitLoc = emitLoc
def closeFile(file):
    file.close()
def openFile(fileName):
    global code
    code = open(fileName,"w")