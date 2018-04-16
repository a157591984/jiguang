using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using System.Data.OleDb;
using Rc.Model.Resources;
using Rc.Common.Config;
using Rc.Cloud.Web.Common;
using Rc.BLL.Resources;
using System.IO;

namespace Rc.Cloud.Web.Sys
{
    public partial class ImportClass : Rc.Cloud.Web.Common.InitPage
    {
        string userGroupParentId = string.Empty;
        string strTempletTitle1 = string.Empty;
        string strTemplet = string.Empty;
        static string strGoodData = string.Empty;
        static string strBadData = string.Empty;
        static DataTable excelData = new DataTable();
        static string strQtype = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "90200100";
            strQtype = "1";
            strTempletTitle1 = "班级名称,班级简介,入学年份（级）";
            strTemplet = string.Empty;
            userGroupParentId = Request.QueryString["userGroupParentId"].Filter();
            hidBackurl.Value = Request.QueryString["backurl"].Filter();
            if (!IsPostBack)
            {
                string strSchoolName = Request.QueryString["school"].Filter();
                string strGradeName = Request.QueryString["grade"].Filter();
                litTitle.Text = string.Format("正在导入学校【{0}】年级【{1}】的班级数据。", strSchoolName, strGradeName);
                excelData.Clear();
                strGoodData = string.Empty;
                strBadData = string.Empty;
            }
            ShowHandel();

        }

        protected void btnImp_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string uploadFileName = string.Empty;

            if (fileUpload.HasFile)
            {
                try
                {
                    uploadFileName = fileUpload.FileName;
                    string errorMessage = string.Empty;

                    if (!CheckFileName(fileUpload.FileName, out errorMessage))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "check", "layer.msg('" + errorMessage + "', { time: 2000, icon: 2 });", true);
                        return;
                    }
                    DeleteInvoiceFile();
                    string fileName = Server.MapPath("~/Upload/ImportExcel/" + Guid.NewGuid() + ".xls");
                    fileUpload.SaveAs(fileName);
                    if (!pfunction.VerifyFileExtensionForImportExcel(fileName))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "check", "layer.msg('文件格式不正确', { time: 2000, icon: 2 });", true);
                        return;
                    }
                    OleDbConnection conn;
                    OleDbDataAdapter da;
                    System.Data.DataTable tblSchema;//存放领域表的结构
                    IList<string> tblNames;//sheet名称
                    GetExcelSchema(fileName, out conn, out da, out tblSchema, out tblNames, "YES", 1);
                    if (ds != null) ds.Clear();
                    ds = GetEachSheetContent(conn, ref da, tblSchema, ref tblNames);
                    if (ds.Tables.Count == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "check", "layer.msg('导入失败，你可能选择了错误的模板，请重新上传。', { time: 2000, icon: 2 });", true);
                        return;
                    }
                    //验证模板结构
                    strTemplet = strTempletTitle1;
                    if (!CheckUploadDataStructure(ds, strTemplet, out errorMessage))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "check", "layer.msg('" + errorMessage + "', { time: 2000, icon: 2 });", true);
                        return;
                    }
                    excelData = new DataTable();
                    excelData = ds.Tables[0];
                    DataColumn dcErr = new DataColumn();
                    dcErr.ColumnName = "ErrorData";
                    dcErr.DataType = typeof(string);
                    dcErr.Caption = "错误提示";
                    excelData.Columns.Add(dcErr);
                    CheckUploadData(strQtype);
                }
                catch (Exception ex)
                {
                    Response.Output.WriteLine(ex.Message);
                    Response.Output.WriteLine(ex.StackTrace);
                    Response.End();
                }
            }
            ShowHandel();
        }
        /// <summary>
        /// 验证上传的文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool CheckFileName(string fileName, out string errorMessage)
        {
            if (fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower() != "xls")
            {
                errorMessage = "请选择后缀名为.xls的EXCEL文件上传";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }
        private void ShowHandel()
        {

            if (excelData.Rows.Count == 0)
            {
                divHandel.Visible = false;
            }
            else
            {
                btnImpRight.Visible = true;
                DataRow[] drCorrect = excelData.Select("ErrorData='正确'");
                if (drCorrect.Count() == 0)
                {
                    btnImpRight.Visible = false;
                }
                divHandel.Visible = true;
            }

        }
        private void InputExcelDataToDB()
        {
            string strLog = string.Empty;
            int exData = 0;
            try
            {
                DataRow[] dr = excelData.Select("ErrorData='正确'");
                if (dr.Count() > 0)
                {
                    if (strQtype == "1")
                    {
                        List<Model_UserGroup> listModelUG = new List<Model_UserGroup>();
                        List<Model_F_User> listModelFU = new List<Model_F_User>();
                        List<Model_ClassPool> listModelCP = new List<Model_ClassPool>();
                        List<Model_UserGroup_Member> listModelUGM = new List<Model_UserGroup_Member>();
                        for (int i = 0; i < dr.Count(); i++)
                        {
                            string gradeId = pfunction.GetNewUserGroupID();
                            #region 班级群组
                            Model_UserGroup modelUG = new Model_UserGroup();
                            modelUG.UserGroup_Id = gradeId;
                            modelUG.UserGroup_ParentId = userGroupParentId;
                            modelUG.User_ID = "";
                            modelUG.UserGroup_Name = dr[i]["班级名称"].ToString().Trim();
                            modelUG.UserGroup_BriefIntroduction = dr[i]["班级简介"].ToString().Trim();
                            modelUG.StartSchoolYear = Convert.ToInt16(dr[i]["入学年份（级）"].ToString());
                            modelUG.CreateTime = DateTime.Now;
                            modelUG.UserGroup_AttrEnum = UserGroup_AttrEnum.Class.ToString();
                            listModelUG.Add(modelUG);
                            #endregion
                            #region 群组池
                            Model_ClassPool modelClassPool = new BLL_ClassPool().GetModelByClass_Id(gradeId);
                            modelClassPool.IsUsed = 1;
                            listModelCP.Add(modelClassPool);
                            #endregion
                            #region 群组成员表
                            Model_UserGroup_Member modelUGM = new Model_UserGroup_Member();
                            modelUGM.UserGroup_Member_Id = Guid.NewGuid().ToString();
                            modelUGM.UserGroup_Id = userGroupParentId;
                            modelUGM.User_ID = gradeId;
                            modelUGM.User_ApplicationStatus = "passed";
                            modelUGM.User_ApplicationTime = DateTime.Now;
                            modelUGM.User_ApplicationReason = "导入班级";
                            modelUGM.User_ApplicationPassTime = DateTime.Now;
                            modelUGM.UserStatus = 0;
                            modelUGM.MembershipEnum = MembershipEnum.classrc.ToString();
                            modelUGM.CreateUser = loginUser.SysUser_ID;
                            listModelUGM.Add(modelUGM);
                            #endregion
                        }
                        exData = new BLL_UserGroup().ImportGradeData(listModelFU, listModelUG, listModelCP, listModelUGM);
                    }
                    strLog = "班级导入信息 ： 操作人ID：【" + loginUser.SysUser_ID + "】";
                }
                if (exData > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'>layer.msg('【" + dr.Count() + "】班级被成功导入',{time:1000,icon:1},function(){historyBack();});</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'>layer.msg('操作失败',{time:2000,icon:2});</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'>layer.msg('操作失败',{time:2000,icon:2});</script>");
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("导入班级失败：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message));
            }

            excelData.Clear();

        }
        /// <summary>
        /// 验证导入数据
        /// </summary>
        /// <param name="goodData"></param>
        /// <param name="badData"></param>
        /// <param name="dataAll"></param>
        private void CheckUploadData(string type)
        {
            for (int i = 0; i < excelData.Rows.Count; i++)
            {
                DataRow dr = excelData.Rows[i];
                string strErr = string.Empty;
                CheckOption(dr, type, out strErr);
                excelData.Rows[i]["ErrorData"] = strErr;
            }
        }
        /// <summary>
        /// 显示验证通过的数据
        /// </summary>
        /// <returns></returns>
        protected string GetGoodData()
        {
            if (excelData.Rows.Count == 0)
            {
                return "";
            }
            DataRow[] dr = excelData.Select("ErrorData='正确'");
            if (dr.Count() == 0)
            {
                return "";
            }
            string strAlert = string.Empty;

            if (dr.Count() == excelData.Rows.Count)
            {
                strAlert = "<font color='green'>全部班级都已验证通过</font>";
            }
            else
            {
                strAlert = "<font color='green'>【" + dr.Count() + "】班级验证通过</font>";
            }
            #region
            if (strQtype == "1")
            {
                strGoodData = string.Empty;
                strGoodData += "<div class='clearDiv'></div><div class=\"div_right_listtitle\">";
                strGoodData += "<div style=\"margin-left:15px;font-weight:bold;padding-top:7px;\">" + strAlert + "</div>";
                strGoodData += "</div>";
                strGoodData += "<table class='table_list' cellpadding='0' cellspacing='0' >";
                strGoodData += "<tr class='tr_title'>";
                strGoodData += "<td style='width:12%;'>班级名称</td>";
                strGoodData += "<td>班级简介</td>";
                strGoodData += "<td>入学年份（级）</td>";
                strGoodData += "</tr>";
                for (int i = 0; i < dr.Count(); i++)
                {
                    strGoodData += "<tr class='tr_con_001'>";
                    strGoodData += "<td>" + dr[i]["班级名称"] + "</td>";
                    strGoodData += "<td>" + dr[i]["班级简介"] + "</td>";
                    strGoodData += "<td>" + dr[i]["入学年份（级）"] + "</td>";
                    strGoodData += "</tr>";
                }
                strGoodData += "</table>";
            }

            #endregion

            return strGoodData;
        }
        /// <summary>
        /// 显示验证没有通过的数据
        /// </summary>
        /// <returns></returns>
        protected string GetBadData()
        {
            if (excelData.Rows.Count == 0)
            {
                return "";
            }
            DataRow[] dr = excelData.Select("ErrorData<>'正确'");
            if (dr.Count() == 0)
            {
                return "";
            }
            string strAlert = string.Empty;
            if (dr.Count() == excelData.Rows.Count)
            {
                strAlert = "<font color='red'><div class='clearDiv'></div>全部班级都没有验证通过</font>";
            }
            else
            {
                strAlert = "<font color='red'>【" + dr.Count() + "】班级验证没通过</font>";
            }
            #region
            if (strQtype == "1")
            {
                strBadData = string.Empty;
                strBadData += "<div class=\"div_right_listtitle\">";
                strBadData += "<div style=\"margin-left:15px;font-weight:bold;padding-top:7px;\">" + strAlert + "</div>";
                strBadData += "</div>";
                strBadData += "<table class='table_list' cellpadding='0' cellspacing='0' >";
                strBadData += "<tr class='tr_title'>";
                strBadData += "<td style='width:12%;'>班级名称</td>";
                strBadData += "<td>班级简介</td>";
                strBadData += "<td>入学年份（级）</td>";
                strBadData += "<td>错误提示</td>";
                strBadData += "</tr>";

                for (int i = 0; i < dr.Count(); i++)
                {
                    strBadData += "<tr class='tr_con_001'>";
                    strBadData += "<td>" + dr[i]["班级名称"] + "</td>";
                    strBadData += "<td>" + dr[i]["班级简介"] + "</td>";
                    strBadData += "<td>" + dr[i]["入学年份（级）"] + "</td>";
                    strBadData += "<td  style='text-align:left;'>" + dr[i]["ErrorData"] + "</td>";
                    strBadData += "</tr>";
                }
                strBadData += "</table>";
            }

            #endregion
            return strBadData;
        }
        /// <summary>
        /// 核对每一项的值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="type"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        private bool CheckOption(DataRow dr, string type, out string strErr)
        {
            bool bTemp = true;
            strErr = string.Empty;

            switch (type)
            {
                #region
                case "1":
                    if (dr["班级名称"].ToString().Trim() == "")
                    {
                        bTemp = false;
                        strErr += "班级名称为空值<br>";
                    }
                    if (dr["班级名称"].ToString().Length > 50)
                    {
                        strErr += "班级名称值过长（不能超过50个汉字或字母）<br>";
                        bTemp = false;
                    }
                    if (bTemp && new BLL_UserGroup().GetRecordCount("UserGroup_Name='" + dr["班级名称"].ToString().Trim().Filter() + @"' and UserGroup_Id in(
SELECT USER_ID from UserGroup_Member where MembershipEnum='" + MembershipEnum.classrc + "' and UserGroup_Id='" + userGroupParentId + "' ) ") > 0)
                    {
                        bTemp = false;
                        strErr += "班级名称已存在<br>";
                    }
                    if (dr["班级简介"].ToString().Length > 300)
                    {
                        strErr += "班级简介值过长（不能超过300个汉字或字母）<br>";
                        bTemp = false;
                    }
                    if (bTemp)
                    {
                        if (dr["入学年份（级）"].ToString().Trim() == "")
                        {
                            bTemp = false;
                            strErr += "入学年份（级）为空值<br>";
                        }
                        int iyear = 0;
                        int.TryParse(dr["入学年份（级）"].ToString().Trim(), out iyear);
                        if (iyear < 2000)
                        {
                            bTemp = false;
                            strErr += "入学年份（级）值填写错误<br>";
                        }
                    }
                    break;
                #endregion
                default:
                    bTemp = false;
                    break;
            }
            if (strErr.Length > 4)
            {
                strErr = strErr.Remove(strErr.Length - 4);
            }
            if (bTemp)
            {
                strErr = "正确";
            }

            return bTemp;

        }
        /// <summary>
        /// 验证上传数据的模板结构
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="list"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool CheckUploadDataStructure(DataSet ds, string strTitle, out string errorMessage)
        {

            if (ds.Tables[0].Columns.Count != strTitle.Split(',').Count())
            {
                errorMessage = "上传文件模板字段的个数不对应,请核对模板后重新上传";
                return false;
            }
            List<string> listTemp = new List<string>();
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (ds.Tables[0].Columns[i].ColumnName.Trim().ToUpper() != strTitle.Split(',')[i].ToUpper())
                {
                    errorMessage = "第【" + (i + 1) + "】个字段的名称与定义模板中的名称不一致，请修改后重新上传";
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// 删除上传的文件
        /// </summary>
        private void DeleteInvoiceFile()
        {
            foreach (string file in Directory.GetFiles(Server.MapPath("~/Upload/ImportExcel/")))
            {
                File.Delete(file);
            }
        }
        #region Excel读取
        private void GetExcelSchema(string filename, out OleDbConnection conn, out OleDbDataAdapter da, out System.Data.DataTable tblSchema, out IList<string> tblNames, string ifFirst, int i)
        {
            // 读取Excel数据，填充DataSet
            // 连接字符串            
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                            "Extended Properties=\"Excel 8.0;HDR=" + ifFirst + ";IMEX=" + i + "\";" + // 指定扩展属性为 Microsoft Excel 8.0 (97) 9.0 (2000) 10.0 (2002)，并且第一行作为数据返回，且以文本方式读取
                            "data source=" + filename;
            //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
            //                "Extended Properties=\"Excel 12.0 Xml;HDR=" + ifFirst + ";IMEX=" + i + "\";" + // 指定扩展属性为 Microsoft Excel 8.0 (97) 9.0 (2000) 10.0 (2002)，并且第一行作为数据返回，且以文本方式读取
            //                "data source=" + filename;
            conn = null;
            da = null;
            tblSchema = null;
            tblNames = null;
            // 初始化连接，并打开
            conn = new OleDbConnection(connStr);
            conn.Open();
            //获取数据源的表定义元数据
            tblSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            conn.Close();
        }
        private static DataSet GetEachSheetContent(OleDbConnection conn, ref OleDbDataAdapter da, System.Data.DataTable tblSchema, ref IList<string> tblNames)
        {
            tblNames = new List<string>();
            foreach (DataRow row in tblSchema.Rows)
            {
                string tableName = (string)row["TABLE_NAME"];
                if (!tableName.StartsWith("_")) //skip system tables
                {
                    tblNames.Add((string)row["TABLE_NAME"]); // 读取sheet名
                }
            }

            //*********************************************************
            // 初始化适配器
            da = new OleDbDataAdapter();
            // 准备数据，导入DataSet
            DataSet ds = new DataSet();
            string sql_F = "SELECT * FROM [{0}]";
            string sheetName = "班级$";
            if (strQtype == "1")
            {
                sheetName = "班级$";
            }
            foreach (string tblName in tblNames)
            {
                if (tblNames.Count > 1 && !tblName.StartsWith(sheetName))
                {
                    continue;
                }
                da.SelectCommand = new OleDbCommand(String.Format(sql_F, tblName), conn);
                try
                {
                    da.Fill(ds, tblName);
                }
                catch
                {
                    // 关闭连接
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

            // 关闭连接
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            return ds;
        }
        #endregion
        /// <summary>
        /// 用户事件 写入导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImpRight_Click(object sender, EventArgs e)
        {
            InputExcelDataToDB();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            excelData.Clear();
            ShowHandel();
        }
    }
}