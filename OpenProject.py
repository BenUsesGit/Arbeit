# encoding: utf-8
# Release 17.2	
import time, datetime
#print "Öffne Dummy-Projekt"
RunScript(ProgLoc + "OpenProject.wbjn")
try:
	log = open(LogLoc,"a")
	print "txt-datei geöffnet"
	ts = time.time()
	st = datetime.datetime.fromtimestamp(ts).strftime('%d.%m.%Y  %H:%M:%S')
	print "got timestamp!"
	text = "\n"+str(10)+"\t"+st+"\t"+str(-1)+"\t"+"Loading project ready"
	print "stringconversion successfull"
	log.write(text)
	print "wrote into file"
	log.close()
	print "file closed"
except:
	print "Da ist etwas schiefgegangen"
	log.close()