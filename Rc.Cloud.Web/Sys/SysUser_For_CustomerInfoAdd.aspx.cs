using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysUser_For_CustomerInfoAdd : System.Web.UI.Page
    {
        SysUserBLL BLL = new SysUserBLL();
        string sysUser_ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtSysCode = new DataTable();                
                Bind();
            }
        }

        //绑定值
        protected void Bind()
        { 
            if (!string.IsNullOrEmpty(Request.QueryString["sysUser_Name"].ToString()))
            {
                lbSysUser_Name.Text = Request.QueryString["sysUser_Name"].ToString();
            }
            sysUser_ID =Rc.Common.StrUtility.clsUtility.Decrypt(Request.QueryString["SysUser_ID"]);
            DataTable dt = new DataTable();
            string strname = string.Empty;
            string strId = string.Empty;
            dt = BLL.GetSysUser_For_CustomerInfo(sysUser_ID).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strname += dt.Rows[i]["CustomerInfo_NameCN"].ToString() + ",";
                strId += dt.Rows[i]["CustomerInfo_ID"].ToString() + ",";
            }
            txtContactName.Value=strname.TrimEnd(',');
            hidContactId.Value = strId.TrimEnd(',');
        }

        //保存数据
        protected void DataSave()
        {
            string customerInfo_ID = string.Empty;
            customerInfo_ID = hidContactId.Value;
            if (BLL.EditSysUser_For_CustomerInfo(Request.QueryString["SysUser_ID"], customerInfo_ID))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.Handel('1','');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.Handel( '2','');</script>");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataSave();
        }
    }
}