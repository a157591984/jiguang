var publicclass = new Object();

publicclass.AlertInit = function (div_Pop, div_Pop_Title) {
    $("#" + div_Pop).easydrag();
    $("#" + div_Pop).setHandler(div_Pop_Title);
};

publicclass.AlertShow = function (div_Pop, div_Pop_Title) {
    SetDialogPosition(div_Pop);
    ShowDocumentDivBG();
    $("#" + div_Pop).show();
};
