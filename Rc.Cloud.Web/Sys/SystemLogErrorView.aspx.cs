using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;

namespace Rc.Cloud.Web.Sys
{
    public partial class SystemLogErrorAdd : System.Web.UI.Page
    {
        SystemLogErrorBLL BLL = new SystemLogErrorBLL();
        //模板ID
        string sysID;
        string sysUser_Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            //设置页面类型，并接受Request参数
            SetActionType();
            if (!IsPostBack)
            {
                //如果是修改，则绑定页面信息
                BindPageInfo();

            }
        }
        //绑定页面信息
        private void BindPageInfo()
        {
            //获取数据
            var getInfo = GetInfo();
            //绑定控件
            if (getInfo != null)
            {
                txtaddress.Text = getInfo.SystemLog_IP;
                txtpagepath.Text = getInfo.SystemLog_PagePath;
                txtsyspath.Text = getInfo.SystemLog_SysPath;
                txtcontent.Text = getInfo.SystemLog_Desc;
                txtperson.Text = sysUser_Name;
                txttime.Text = getInfo.SystemLog_CreateDate.ToString();
            }
        }
        //获取模板信息
        public Model_SystemLogError GetInfo()
        {
            return BLL.GetModel_SystemLogErrorByPK(sysID);
        }
        //设置页面命令类型
        private void SetActionType()
        {
            sysID = Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["id"]);
            sysUser_Name = Request.QueryString["sysUser_Name"];
        }
    }
}