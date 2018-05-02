//Get DesignSpace object
    var DS = WB.AppletList.Applet("DSApplet").App;

    var DSTreeObject = DS.Tree;

    var Branch = DSTreeObject.Branches(1);
    var MeshCtrlGrp = Branch.MeshControlGroup;

    // change Meshelementsize

    MeshCtrlGrp.RelevanceCenter = 2;    // Fine

    var object = new Object(null);
    DS.AlbionSimAddinCaller.OnUpdateMeshCell.Fire(object, object);
