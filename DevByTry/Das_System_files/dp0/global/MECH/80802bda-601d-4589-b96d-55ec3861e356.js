debugger;
function Update() {
    //Get DesignSpace object
        //var DS = WB.AppletList.Applet("DSApplet").App;
        //var Branch = DS.Tree.Branches(1);
        //var MeshCtrlGrp = Branch.MeshControlGroup;

        ////change Meshelementsize

        //MeshCtrlGrp.RelevanceCenter = 2;    // Fine

        //var object = new Object(null);
        //DS.AlbionSimAddinCaller.OnUpdateMeshCell.Fire(object, object);
        return 0;
}

function ReadResults(lstResults) {
    var DS = WB.AppletList.Applet("DSApplet").App;
    var Branch = DS.Tree.Branches(1);
    new out(lstResults.length)

    if (Branch.Results.Count > 0) {
        for (i = 0; i < lstResults.length; i++) {
            for (j = 0; j < Branch.Results.Count; j++) {
                if (Branch.Results(j).Name==lstResults[i]){
                    out[i] = Branch.Results(j).Name;
                    break;
                }
                else {
                    out[i] = "NaN";
                }
            }
        }
    }

    return 0;
}

function getDate() {
    var stamp = new Date();
    var para = [stamp.getUTCDay(),stamp.getUTCMonth(),stamp.getUTCFullYear(),stamp.getHours(),stamp.getMinutes(),stamp.getSeconds()];
    return stupidConversion(para);
}

//this function converts the date-format into the string-format used in the log-file
function stupidConversion(arr) {
    debugger;
    var retStr = "";
    for (i = 0; i = 5; i++){
        switch (i) {
            // days
            case 0:
                arr[i]++;
                if (arr[i] < 10) {                     
                    retStr = retStr + "0" + arr[i].toString() + ".";;
                }
            // month
            case 1:
                 arr[i]++;
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ".";
                 }
            //year
            case 2:
                retStr = retStr + arr[i] + " ";
            //hour
            case 3:
                arr[i]++;
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ":";
                }
            //minutes
            case 4:
                arr[i]++;
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ":";
                }
            //seconds
            case 5:
                arr[i]++;
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ":";
                }
        }
    }
    return retStr;    
}

function writeResults(path, data) {
    var DS = WB.AppletList.Applet("DSApplet").App;
    var fso = DS.Script.GetFileSystemObject();
    var str = fso.OpenTextFile(path, 1, false)
    var str1 = str.ReadAll()+"\n" + data;
    str.close();
    var file = fso.OpenTextFile(path,2);
   file.Write(str1)
        file.close();
    return 0;
}

writeResults("D:\\Skripte\\VBA\\Splines\\Test\\Ichbinhier\\AnsLog.txt", "*" + "\t" + getDate() + "\t" + "Javatest" + "\t" +"*");Update();
