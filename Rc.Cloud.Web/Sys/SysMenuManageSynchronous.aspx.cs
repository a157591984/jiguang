using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

namespace Rc.SysManagement.Web.Sys
{
    public partial class SysMenuManageSynchronous : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //load xml file   
            if (!IsPostBack)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("/XmlFile/UserControlConfig.xml"));
                XmlNodeList xmldocSelects = xmlDoc.SelectNodes("UserControlConfig/Field");
                dataBase.Items.Clear();
                foreach (XmlNode xn in xmldocSelects)//遍历所有子节点   
                {
                    dataBase.Items.Add(new ListItem(xn.Attributes["name"].Value, xn.Attributes["name"].Value));
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            List<string> lt = new List<string>();
            if (!string.IsNullOrEmpty(dataBase.SelectedValue))
            {
                string sysData = string.Empty;
                sysData = Rc.Cloud.Web.Common.pfunction.GetCblCheckedValue(dataBase, ",");
                string [] len=sysData.Split(',');
                for (int j = 0; j < len.Length; j++)
                {
                    lt.Add(" delete [" + len[j] + "].dbo.SysModule;INSERT INTO [" + len[j] + "].dbo.SysModule SELECT * FROM [YXFW_DEVE_0.9.4].dbo.SysModule where Syscode IN (SELECT SysCode FROM [YXFW_DEVE_0.9.4].dbo.SysCode)");
                    lt.Add(" INSERT INTO SysModuleHistory SELECT *,getdate(),'" + len[j] + "' FROM [YXFW_DEVE_0.9.4].dbo.SysModule where Syscode IN (SELECT SysCode FROM [YXFW_DEVE_0.9.4].dbo.SysCode)");
                }
                int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlTran(lt);
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.Handel('覆盖成功', '1');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.showTipsErr('覆盖失败！','4');</script>");
                }
            }            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<string> lt = new List<string>();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlNew = new StringBuilder();
            if (!string.IsNullOrEmpty(dataBase.SelectedValue))
            {
                string sysData = string.Empty;
                sysData = Rc.Cloud.Web.Common.pfunction.GetCblCheckedValue(dataBase, ",");
                string [] len=sysData.Split(',');
                for (int j = 0; j < len.Length; j++)
                {
                    strSql.Append(" INSERT INTO [" + len[j] + "].dbo.SysModule");
                    strSql.Append(" SELECT * FROM [YXFW_DEVE_0.9.4].dbo.SysModule M1 WHERE");
                    strSql.Append(" NOT EXISTS(SELECT * FROM [" + len[j] + "].dbo.SysModule M2 WHERE M2.Syscode=M1.Syscode AND ");
                    strSql.Append(" M2.MODULEID=M1.MODULEID) AND Syscode IN (SELECT SysCode	FROM [YXFW_DEVE_0.9.4].dbo.SysCode);");

                    strSqlNew.Append(" INSERT INTO SysModuleHistory");
                    strSqlNew.Append(" SELECT *,getdate(),'" + len[j] + "' FROM [YXFW_DEVE_0.9.4].dbo.SysModule M1 WHERE");
                    strSqlNew.Append(" NOT EXISTS(SELECT * FROM [" + len[j] + "].dbo.SysModule M2 WHERE M2.Syscode=M1.Syscode AND ");
                    strSqlNew.Append(" M2.MODULEID=M1.MODULEID) AND Syscode IN (SELECT SysCode	FROM [YXFW_DEVE_0.9.4].dbo.SysCode);");                    
                }
                lt.Add(strSql.ToString());
                lt.Add(strSqlNew.ToString());
                int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlTran(lt);
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.Handel('新增成功', '1');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>parent.showTipsErr('无需添加，已经最新！','4');</script>");
                }
            }
        }       
    }
}