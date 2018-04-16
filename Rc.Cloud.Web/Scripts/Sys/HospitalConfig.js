function SelectHospitalType() {

    if ($("#selHosptialConfigType").val() == "1") {
        $("#desc").show();
    }
    else  {
        $("#desc").hide();
    }
}
function chkInput() {
    if ($("#selHosptialConfigType").val() == "1") {
        if ($("#txtDoctorInfoName").val() == "") {
            alert("请输入医生名称。");
            return false;
        }
        if ($("#txtUnion").val() == "") {
            alert("请输入联系方式。");
            return false;
        }
    }
    return true;
}
$(document).ready(function () {
    if ($("#selHosptialConfigType").val() == "1") {
        $("#desc").show();
    }
    else  {
        $("#desc").hide();
    }
});