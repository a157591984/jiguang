using Rc.Cloud.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.Sys
{
    public partial class SysMenuSynchronous : Rc.Cloud.Web.Common.InitPage
    {
        List<string> lt = new List<string>();
        BLL_SysCode bll = new BLL_SysCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetSearchP();
            Module_Id = "30203000";
            if (!IsPostBack)
            {
                init();
                SetSearchO();
            }


        }
        private void init()
        {
            DataTable dataBaseCopyLibrary = new DataTable();
            dataBaseCopyLibrary = Rc.Common.DBUtility.DbHelperSQL.Query(@"SELECT * FROM dbo.DatabaseCopyLibrary INNER JOIN CustomerInfo
		                                                                   ON CustomerInfo.CustomerInfo_ID = DatabaseCopyLibrary.CustomerInfo_ID").Tables[0];
            Rc.Cloud.Web.Common.pfunction.SetDdl(ddlDataName, dataBaseCopyLibrary, "DataBaseName", "DatabaseCopyLibrary_ID", "");

            DataTable dt = new DataTable();
            dt = Rc.Common.DBUtility.DbHelperSQL.Query(" SELECT SysCode FROM  SysCode").Tables[0];
            Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSyscode, dt, "SysCode", "SysCode", "");
        }

        protected void btn_Execution_Click(object sender, EventArgs e)
        {
            GetAddHtmlData();
        }

        //覆盖跟新
        protected void btn_Coverage_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            if (!string.IsNullOrEmpty(ddlDataName.SelectedValue))
            {
                #region
                //string insertHistroy = string.Empty;
                //string name = string.Empty;
                //string code = string.Empty;
                //string delete = string.Empty;
                //string insertSysModule = string.Empty;
                //insertHistroy += " INSERT INTO [MSExecution].dbo.SysModuleHistory ";
                //insertHistroy += " SELECT *,getdate(),'" + ddlDataName.SelectedItem.Text + "' FROM " + ddlDataName.SelectedItem.Text + ".dbo.SysModule where 1=1";
                //if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                //{
                //    name = " and MODULENAME like '%" + txtName.Text.Trim() + "%' or MODULEID like '%" + txtName.Text.ToString() + "%'";
                //}
                //if (!string.IsNullOrEmpty(ddlSyscode.SelectedValue))
                //{
                //    code = " and SysCode ='" + ddlSyscode.SelectedValue + "'";
                //}
                //lt.Add(insertHistroy + name + code);
                //delete = " delete " + ddlDataName.SelectedItem.Text + ".dbo.SysModule where 1=1 " + name + code;
                //lt.Add(delete);
                //insertSysModule = " INSERT INTO " + ddlDataName.SelectedItem.Text + ".dbo.SysModule select * from  SysModule WHERE 1=1 " + name + code;
                //lt.Add(insertSysModule);
                //string str = string.Empty;
                //for (int k = 0; k < lt.Count; k++)
                //{
                //    str += lt[k];
                //}
                //strCoverage.Value = str;
                #endregion
                lt.Add(hidCoverage.Value);
                //int i = PHHC.Share.DBUtility.DbHelperSQL.ExecuteSqlTran(lt);
                int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(lt[0]);
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>showTips('覆盖成功','', '1');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>showTipsErr('覆盖失败！','4');</script>");
                }
            }
        }

        //增量跟新
        protected void btn_Add_Click(object sender, EventArgs e)
        {

            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(ddlDataName.SelectedValue))
            {
                #region
                strSql.Append(" INSERT INTO " + ddlDataName.SelectedItem.Text + ".dbo.SysModule");
                strSql.Append(" SELECT * FROM  SysModule M1 WHERE 1=1");
                if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    strSql.Append(" and MODULENAME like '%" + txtName.Text.Trim() + "%' or MODULEID like '" + txtName.Text.ToString() + "%'");
                }
                if (!string.IsNullOrEmpty(ddlSyscode.SelectedValue))
                {
                    strSql.Append(" and SysCode = '" + ddlSyscode.SelectedValue + "' ");
                }
                strSql.Append(" and NOT EXISTS(SELECT * FROM " + ddlDataName.SelectedItem.Text + ".dbo.SysModule M2 WHERE M2.Syscode=M1.Syscode AND ");
                strSql.Append(" M2.MODULEID=M1.MODULEID);");

                lt.Add(strSql.ToString());
                string str = string.Empty;
                for (int k = 0; k < lt.Count; k++)
                {
                    str += lt[k];
                }
                strCoverage.Value = str;
                #endregion
         
                //int i = PHHC.Share.DBUtility.DbHelperSQL.ExecuteSqlTran(lt);
                int i = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(lt[0]);
                if (i > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>showTips('增加成功','','1');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fildSave", "<script type='text/javascript'>showTipsErr('增加失败！','4');</script>");
                }
            }
        }

        //获得新增数据
        protected string GetAddHtmlData()
        {
            DataTable dt = new DataTable();
            StringBuilder condition = new StringBuilder();
            StringBuilder strHtmlData = new StringBuilder();
            try
            {
                if (txtName.Text.Trim() != "" || ddlSyscode.SelectedValue != "")
                {
                    if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                    {
                        condition.Append(" and (MODULENAME like '%" + txtName.Text.Trim() + "%' or MODULEID like '" + txtName.Text.ToString() + "%')");
                    }
                    if (!string.IsNullOrEmpty(ddlSyscode.SelectedValue))
                    {
                        condition.Append(" and (SysCode = '" + ddlSyscode.SelectedValue + "')");
                    }
                    string coverage = string.Empty;
                    coverage = string.Format(@" insert into " + ddlDataName.SelectedItem.ToString() + ".dbo.SysModule");
                    coverage += "  select * from  SysModule S1 where 1=1 ";
                    if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                    {
                        coverage += string.Format(" and (  MODULENAME like '%{0}%' or MODULEID like '{0}%')", txtName.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(ddlSyscode.SelectedValue))
                    {
                        coverage += string.Format("  and( SysCode = '{0}')", ddlSyscode.SelectedValue);
                    }

                    coverage += " and NOT EXISTS( SELECT * from " + ddlDataName.SelectedItem.ToString() + ".dbo.SysModule S2 where";
                    coverage += "  S1.MODULEID=S2.MODULEID AND S1.Syscode=S2.Syscode)";

                    strCoverage.Value ="--添加脚本\r\n"+ coverage;
                    hidCoverage.Value =  coverage;
                }
                else
                {
                    strCoverage.Value = "--添加脚本\r\n" + string.Format(@" insert into {0}.dbo.SysModule 
                                    select * from  SysModule S1 where 
                                    NOT EXISTS( SELECT * from {0}.dbo.SysModule S2 where
                                    S1.MODULEID=S2.MODULEID AND S1.Syscode=S2.Syscode)", ddlDataName.SelectedItem.ToString());
                    hidCoverage.Value =  string.Format(@" insert into {0}.dbo.SysModule 
                                    select * from  SysModule S1 where 
                                    NOT EXISTS( SELECT * from {0}.dbo.SysModule S2 where
                                    S1.MODULEID=S2.MODULEID AND S1.Syscode=S2.Syscode)", ddlDataName.SelectedItem.ToString());
                }

                dt = bll.GetDataList(ddlDataName.SelectedItem.ToString().Replace("\\",@"\"), condition.ToString()).Tables[0];
                int i = 0;
                strHtmlData.Append("<table class='table_list' cellpadding='0' id='MyTable' cellspacing='0' style='width: 2000px;height:430px;margin:0px auto auto 0px;'>");
                strHtmlData.Append("<thead>");
                strHtmlData.Append("<tr class='tr_title'>");
                string columns = string.Empty;
                for (int m = 0; m < dt.Columns.Count; m++)
                {
                    strHtmlData.Append("<th>" + (dt.Columns[m].ColumnName == "r_n" ? "序号" : dt.Columns[m].ColumnName) + "</th>");
                    if (dt.Columns[m].ColumnName != "r_n")
                    {
                        columns += dt.Columns[m].ColumnName + ",";
                    }
                }
                strHtmlData.Append("</thead>");
                columns = columns.TrimEnd(',');
                strHtmlData.Append("</tr>");
                if (dt.Rows.Count > 0)
                {
                    strHtmlData.Append("<tr class='tr_con_001'><td colspan='18' style='height:20px;'>需新增的数据</td></tr>");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        i++;
                        string css = string.Empty;
                        //string strValues = string.Empty;
                        if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                        strHtmlData.Append("<tr class='" + css + "'>");
                        for (int m = 0; m < dt.Columns.Count; m++)
                        {
                            strHtmlData.Append("<td title='" + dt.Rows[j][m].ToString() + "'>" + Rc.Cloud.Web.Common.pfunction.SplitStr(dt.Rows[j][m].ToString(), 36) + "</td>");
                            //if (m > 0)
                            //{
                            //    strValues += "'" + dt.Rows[j][m] + "'" + ",";
                            //}
                        }
                        //strValues = strValues.TrimEnd(',');
                        //strSqlAdd += string.Format(" insert {0}.dbo.SysModule({2}) values({3});\r\n", ddlDataName.SelectedItem.ToString(), chkDataItem.SelectedValue.Split(',')[0], columns, strValues);
                        strHtmlData.Append("</tr>");
                    }
                }
                //需更新数据
                DataTable dtColumns = new DataTable();
                string where = string.Empty;
                dtColumns = bll.GetSysModuleColumnName().Tables[0];
                where = string.Format(" f1.{0} = f2.{0} and(", "MODULEID");
                for (int m = 0; m < dtColumns.Columns.Count; m++)
                {
                    if (dtColumns.Columns[m].ColumnName != "MODULEID" && dtColumns.Columns[m].ColumnName != "Syscode")
                    {
                        where += string.Format(" f1.{0}<>f2.{0} or", dtColumns.Columns[m].ColumnName);
                    }
                }
                where = where.Trim().TrimEnd('r').TrimEnd('o') + ")";
                where += string.Format("  and f1.{0} = f2.{0}", "Syscode");
                DataTable dtUpdate = new DataTable();
                dtUpdate = bll.GetUpdateDataList(where, ddlDataName.SelectedItem.ToString(),condition.ToString()).Tables[0];
                if (dtUpdate.Rows.Count > 0)
                {
                    strHtmlData.Append("<tr class='tr_con_001'><td colspan='18' style='height:20px;'>需要更新的数据</td></tr>");
                    string coverageUpdate = string.Empty;
                    string insertSysModuleHistory = string.Empty;
                    for (int y = 0; y < dtUpdate.Rows.Count; y++)
                    {
                        i++;
                        string css = string.Empty;
                        if (i % 2 == 0) { css = "tr_con_001"; } else { css = "tr_con_002"; }
                        strHtmlData.Append("<tr class='" + css + "'>");
                        DataTable dtCom = new DataTable();
                        dtCom = bll.GetComparisonDataList(dtUpdate.Rows[y]["MODULEID"].ToString(), dtUpdate.Rows[y]["Syscode"].ToString(), ddlDataName.SelectedItem.ToString()).Tables[0];
                        for (int m = 0; m < dtUpdate.Columns.Count; m++)
                        {
                            if (m == 0)
                            {
                                strHtmlData.Append("<td>" + dtUpdate.Rows[y][m].ToString() + "</td>");
                            }
                            else
                            {
                                strHtmlData.Append("<td " + GetResult(dtUpdate.Rows[y][m].ToString(), dtCom.Rows[0][m].ToString()) + ">" + Rc.Cloud.Web.Common.pfunction.SplitStr(dtUpdate.Rows[y][m].ToString(), 36) + "</td>");
                            }
                        }
                        strHtmlData.Append("</tr>");

                        insertSysModuleHistory += string.Format(@"  INSERT INTO [MSExecution].dbo.SysModuleHistory select *,getdate(),'{0}' from {0}.dbo.SysModule where MODULEID='{1}' and  Syscode='{2}'",
                            ddlDataName.SelectedItem.Text, dtUpdate.Rows[y]["MODULEID"].ToString(), dtUpdate.Rows[y]["Syscode"].ToString()) + "\r\n";

                        coverageUpdate += string.Format(@" update " + ddlDataName.SelectedItem.ToString() + @".dbo.SysModule set MODULENAME='{0}',PARENTID='{1}',SLEVEL='{2}'
                            ,URL='{3}',QUERYFORM='{4}',OTHKEY='{5}',REMARK='{6}',IMGICON='{7}',ISINTREE='{8}',MODULETYPE='{9}',ATTACH_SQL='{10}',ISINTAB='{11}',Depth={12}
                            ,IsLast={13},DefaultOrder={14} where  MODULEID='{15}' and Syscode='{16}'",
                            dtUpdate.Rows[y]["MODULENAME"].ToString(), dtUpdate.Rows[y]["PARENTID"].ToString(), dtUpdate.Rows[y]["SLEVEL"].ToString(),
                            dtUpdate.Rows[y]["URL"].ToString(), dtUpdate.Rows[y]["QUERYFORM"].ToString(), dtUpdate.Rows[y]["OTHKEY"].ToString(),
                            dtUpdate.Rows[y]["REMARK"].ToString(), dtUpdate.Rows[y]["IMGICON"].ToString(), dtUpdate.Rows[y]["ISINTREE"].ToString(), dtUpdate.Rows[y]["MODULETYPE"].ToString(),
                            dtUpdate.Rows[y]["ATTACH_SQL"].ToString(), dtUpdate.Rows[y]["ISINTAB"].ToString(), dtUpdate.Rows[y]["Depth"].ToString(), dtUpdate.Rows[y]["IsLast"].ToString(),
                            dtUpdate.Rows[y]["DefaultOrder"].ToString(), dtUpdate.Rows[y]["MODULEID"].ToString(), dtUpdate.Rows[y]["Syscode"].ToString())+"\r\n";
                    }
                    strCoverage.Value += "\r\n--插入历史菜单记录\r\n" + insertSysModuleHistory+ "\r\n--更新脚本\r\n" + coverageUpdate;
                    updatadata.Value = coverageUpdate;
                    hidCoverage.Value +=  insertSysModuleHistory  + coverageUpdate;
                }
               
                strHtmlData.Append("</table>");
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法：{1},错误信息：{2}， 详细：{3}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message, ex.StackTrace));
                throw ex;
            }
            ulA.InnerHtml = strHtmlData.ToString();
            return strHtmlData.ToString();
        }

        protected void btn_Create_Click(object sender, EventArgs e)
        {

            string insertDeleteData = string.Empty;
            string condition = string.Empty;
            condition = "  where 1=1";       
            if (!string.IsNullOrEmpty(ddlDataName.SelectedValue))
            {
                condition += " and syscode=" + ddlSyscode.SelectedItem.Text ;
            }
            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                condition += " and (MODULENAME like '%" + txtName.Text.Trim() + "%' or MODULEID like '" + txtName.Text.ToString() + "%')";
            }            
            insertDeleteData += string.Format("--删除插入脚本，请慎重操作\r\n \r\n-- delete dbo.SysModule {0} \r\n ", condition);
            insertDeleteData += string.Format("-- insert into dbo.SysModule select * from  SysModule {0} \r\n ", condition);
            
            Response.AddHeader("Content-Disposition", "attachment;filename= 更新SysModule表脚本.sql");
            //Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            //Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            Response.Write(strCoverage.Value + "\r\n" + insertDeleteData);
            //Response.Flush();
            Response.End();

        }
        //给查询对象附值
        private void SetSearchO()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["a"]))
                txtName.Text = Request.QueryString["a"];
            if (!string.IsNullOrEmpty(Request.QueryString["c"]))
                ddlSyscode.SelectedValue = Request.QueryString["c"];
            if (!string.IsNullOrEmpty(Request.QueryString["b"]))
                ddlDataName.SelectedValue = Request.QueryString["b"];
            if (!string.IsNullOrEmpty(Request.QueryString["a"]))
            {
                GetAddHtmlData();
            }

        }
        //URL加密
        private void SetSearchP()
        {
            if (!string.IsNullOrEmpty(Request["PageIndex"]))
            {
                if (!int.TryParse(Request["PageIndex"].ToString(), out PageIndex)) { PageIndex = 1; }
                if (PageIndex <= 0) { PageIndex = 1; }
            }
            if (!string.IsNullOrEmpty(Request["PageSize"]))
            {
                if (!int.TryParse(Request["PageSize"].ToString(), out PageSize)) { PageSize = 10; }
            }
            strUrlPara = strPageName + "?PageIndex={0}&PageSize={1}";
            strUrlPara += "&a=" + Server.UrlEncode(Request.QueryString["a"]);
            strUrlPara += "&b=" + Server.UrlEncode(Request.QueryString["b"]);
            strUrlPara += "&c=" + Server.UrlEncode(Request.QueryString["c"]);
        }

        //分页
        protected string GetPageIndex()
        {
            return Rc.Cloud.Web.Common.PageFuntion.GetPageIndex(PageIndex, PageSize, rCount, pCount, strUrlPara);
        }

        //查询
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            updatadata.Value = null;
            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                strUrlPara = strPageName + "?PageIndex=1&PageSize=" + PageSize;
                strUrlPara += "&a=" + txtName.Text.Trim();
                strUrlPara += "&b=" + ddlDataName.SelectedValue;
                strUrlPara += "&c=" + ddlSyscode.SelectedValue;
                Response.Redirect(strUrlPara);
            }
            else
            {
                btn_Execution_Click(sender, e);
            }
        }

        //比对是否相等
        private string GetResult(string value, string comValue)
        {
            string temp = string.Empty;
            if (value == comValue)
            {
                temp = "title =" + value;
            }
            else
            {
                temp = "title=" + comValue + " style='color:red;'";
            }
            return temp;
        }

        protected void btn_Createoutline_Click(object sender, EventArgs e)
        {
            string add = strCoverage.Value;
            if (add.Equals(string.Empty))
                return;
            string[] adds = add.Split('*');
            string[] addsec = adds[2].Split('-');
            add = "select *" + adds[1] + "*" + addsec[0];
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(add).Tables[0];
            StringBuilder sb = new StringBuilder();
            sb.Append("添加新增的插入语句\r\n");
            foreach (DataRow inrow in dt.Rows)
            {
                sb.AppendFormat(@"INSERT INTO [SysModule]
                                 VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}' ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,'{14}' ,'{15}' ,'{16}')
                            ", inrow[0], inrow[1], inrow[2], inrow[3], inrow[4], inrow[5], inrow[6], inrow[7], inrow[8], inrow[9], inrow[10], inrow[11], inrow[12], inrow[13], inrow[14], inrow[15], inrow[16]);
                sb.Append("\r\n");
            }
            sb.Append("需要更新的数据\r\n");
            string updata = updatadata.Value;
            updata = updata.Replace(ddlDataName.SelectedItem.ToString()+".", "");
            sb.Append(updata);
            Response.AddHeader("Content-Disposition", "attachment;filename= 外网专用脚本.sql");
            Response.ContentType = "text/plain";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.Write("\r\n" + sb);
            //Response.Flush();
            Response.End();
        }

    }
}