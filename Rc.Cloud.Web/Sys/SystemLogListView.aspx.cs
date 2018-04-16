using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;


namespace Rc.Cloud.Web.Sys
{
    public partial class SystemLogListAdd : System.Web.UI.Page
    {
        private readonly BLL_SystemLog BLL = new BLL_SystemLog();
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
                txtaddress.Text = getInfo.SystemLog_Level;
                txtIP.Text = getInfo.SystemLog_IP;
                txtmodel.Text = getInfo.SystemLog_Model;
                txtcontent.Text = getInfo.SystemLog_Desc;
                txtperson.Text = sysUser_Name;
                txtReason.Text = getInfo.SystemLog_Remark;
                txttime.Text = getInfo.SystemLog_CreateDate.ToString();
            }
        }
        //获取模板信息
        public Model_SystemLog GetInfo()
        {
            return BLL.GetModel_SystemLogByPK(sysID);
        }
        //设置页面命令类型
        private void SetActionType()
        {
            sysID =Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["id"]);
            sysUser_Name = Request.QueryString["sysUser_Name"];
        }
    }
}