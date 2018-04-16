function ModuleTagHandel(sign, message, module) {
    showTips(message, '', '1');
    //GetTagModules("9", 0, "divTag");
    //GetModules("9", 0, "divTag");
    //CloseDialog();

    setTimeout('window.top.location.reload()', 500);
}


function DeleteDepartment(id) {
    if (confirm('确定要删除吗？')) {
        jQuery.get("../Ajax/HospitalConfigAjax.aspx", { key: "DeleteDepartmentByID", id: id, net4: Math.random() },
            function (data) {
                if (data == "1") {
                    showTips('删除成功', 'BasicDepartment.aspx', '2');
                }
                else {
                    showTipsErr('此科室正在使用中，不允许删除', '3');
                }
            });
    }
}