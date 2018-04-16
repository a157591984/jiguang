        $("#div_Pop").easydrag();
        $("#div_Pop").setHandler("div_Pop_Title");

        function DeleteItemDesc(id) {
            if (confirm('确定要删除吗？')) {
                jQuery.get("../Ajax/HospitalAjax.aspx", { key: "HospitalAreaTempDelete", Allid: id, net4: Math.random() },
            function (data) {
                if (data == "1") {
                    showTips('删除成功', '/Sys/HospitalArea.aspx', '1')
                }
                else {
                    showTipsErr('删除失败', '3')
                }
            });
            }
        }

        function showPop(id, handel) {
            var page = "HospitalAreaAdd.aspx?id=" + id;
            if (handel == "1") {//添加
                $("#div_ShowDailg_Title_left").html("添加区域信息");
            }
            else {
                $("#div_ShowDailg_Title_left").html("修改区域信息");
            }
            $("#divIfram").html("<iframe id='iframDrugFiled' frameborder='0' width='100%' style='margin: 0px' src='" + page + "'  height='270px'></iframe>");
            $("#div_Pop").show();
            SetDialogPosition("div_Pop");
        }
        function Handel(sign, error) {
            if (sign == "1") {
                showTips('操作成功', '/Sys/HospitalArea.aspx', '1')
                CloseDialog();
                CloseDocumentDivBG();
            }
            else {
                showTipsErr('操作失败' + " " + error, '4')
            }
        }
        function ShowAlert(str) {
            $.dialog.alert(str);
        }