debugger;
function Update() {
        return 0;
}

// reads from a given array of results for a given individual with an ID
function ReadResults(id, lstResults) {
    debugger;
    var DS = WB.AppletList.Applet("DSApplet").App;
    var Branch = DS.Tree.Branches(1); //
    var out = new Array(lstResults.length);

    if (Branch.Results.Count > 0) {
        for (i = 0; i < lstResults.length; i++) {
            for (j = 1; j < Branch.Results.Count; j++) {
                if (Branch.Results(j).Name==lstResults[i]){
                    out[i] = lstResults[i] + ":" + "\t" + Branch.Results(j).Maximum + "\t";
                    break;
                }
                else {
                    out[i] = "NaN";
                }
            }
        }
    }

    // stitch the out-array together to one single string
    var retStr = "";
    for (i = 0; i < out.length;i++){
        retStr = retStr + out[i];
    }

    return retStr;
}

function ReadProps() {
    debugger;
    var DS = WB.AppletList.Applet("DSApplet").App;
    var path = DS.Tree.Branches(1).Model.Children(1).Children(1)   // "path" to the desired mass value. as the model changes, this path may change too!

    // stitch the out-array together to one single string
    var retStr = path.Mass;

    return retStr;
}

// function gets the date and passes it for conversion
function getDate() {
    var stamp = new Date();
    var para = [stamp.getUTCDay(),stamp.getUTCMonth(),stamp.getUTCFullYear(),stamp.getHours(),stamp.getMinutes(),stamp.getSeconds()];
    return stupidConversion(para);
}

//this function converts the date-format into the string-format used in the log-file
function stupidConversion(arr) {
    debugger;
    var retStr = "";
    for (i = 0; i < 6; i++){
        switch (i) {
            // days
            case 0:
                arr[i]++;
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ".";;
                } else {
                    retStr = retStr + arr[i].toString() + ".";
                }
                break;
            // month
            case 1:
                 arr[i]++;
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ".";
                } else {
                    retStr = retStr + arr[i].toString() + ".";
                }
                break;
            //year
            case 2:
                retStr = retStr + arr[i].toString() + " ";
                break;
            //hour
            case 3:
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ":";
                } else {
                    retStr = retStr + arr[i].toString() + ":";
                }
                break;
            //minutes
            case 4:
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString() + ":";
                } else {
                    retStr = retStr + arr[i].toString() + ":";
                }
                break;
            //seconds
            case 5:
                if (arr[i] < 10) {
                    retStr = retStr + "0" + arr[i].toString();
                } else {
                    retStr = retStr + arr[i].toString();
                }
                break;
        }
    }
    return retStr;    
}

function write(id, string) {
    var path = "D:/Skripte/VBA/Splines/Test/Ichbinhier/AnsLog.txt"
    var DS = WB.AppletList.Applet("DSApplet").App;
    debugger;
    var fso = DS.Script.GetFileSystemObject();
    var str = fso.OpenTextFile(path, 1, false)
    var mass = ReadProps();
    var str1 = str.ReadAll() + "\n" + 45 + "\t" + getDate() + "\t" + id.toString() + "\t" + string + "Mass:" + "\t" + mass + "\t" + /*"\n"
        + 50 + "\t" + getDate() + "\t" +id.toString() + "\t" +*/ "done getting results";
    str.close();
    var file = fso.OpenTextFile(path, 2);
    file.Write(str1)
    file.close();
}
