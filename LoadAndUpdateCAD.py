# encoding: utf-8
# Release 17.2

import time, datetime
print "Lade CAD Model..."
RunScript(ProgLoc + "\LoadCADModel.wbjn")
print "Update ..."
RunScript(ProgLoc + "\UpdateProj.wbjn")
try:
	log = open(LogLoc,"a")
	print "txt-datei geöffnet"
	ts = time.time()
	st = datetime.datetime.fromtimestamp(ts).strftime('%d.%m.%Y  %H:%M:%S')
	print "got timestamp!"
	text = "\n"+str(40)+"\t"+st+"\t"+str(actfile)+"\t"+"Loading CAD and updating project finished"
	print "stringconversion successfull"
	log.write(text)
	print "wrote into file"
	log.close()
	print "file closed"
except:
	print "Da ist etwas schiefgegangen"
	log.close()