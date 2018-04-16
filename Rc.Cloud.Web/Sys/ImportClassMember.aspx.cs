using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.Config;

namespace Rc.Cloud.Web.Sys
{
    public partial class ImportClassMember : Rc.Cloud.Web.Common.InitPage
    {
        string schoolId = string.Empty;
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
            strTempletTitle1 = "姓名,登录账号,密码,身份,学科";
            strTemplet = string.Empty;
            userGroupParentId = Request.QueryString["userGroupParentId"].Filter();
            schoolId = pfunction.GetSchoolIdByClassId(userGroupParentId);
            hidBackurl.Value = Request.QueryString["backurl"].Filter();
            if (!IsPostBack)
            {
                string strClassName = Request.QueryString["class"].Filter();
                string strGradeName = Request.QueryString["grade"].Filter();
                litTitle.Text = string.Format("正在导入年级【{0}】班级【{1}】的成员数据。", strGradeName, strClassName);
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
                        Model_UserGroup modelUG = new Model_UserGroup();
                        List<Model_F_User> listModelFU = new List<Model_F_User>();
                        List<Model_UserGroup_Member> listModelUGM = new List<Model_UserGroup_Member>();
                        for (int i = 0; i < dr.Count(); i++)
                        {
                            string userId = string.Empty;
                            #region 用户
                            Model_F_User modelFUser = new BLL_F_User().GetModelByUserName(dr[i]["登录账号"].ToString().Trim().Filter());

                            if (modelFUser == null)
                            {
                                modelFUser = new Model_F_User();
                                #region 新增用户
                                userId = Guid.NewGuid().ToString();
                                modelFUser.UserId = userId;
                                modelFUser.UserName = dr[i]["登录账号"].ToString().Trim();
                                string pass = dr[i]["密码"].ToString().Trim();
                                if (string.IsNullOrEmpty(pass)) pass = "123456";
                                modelFUser.Password = Rc.Common.StrUtility.DESEncryptLogin.EncryptString(pass);
                                modelFUser.TrueName = dr[i]["姓名"].ToString().Trim();
                                modelFUser.CreateTime = DateTime.Now;
                                if (dr[i]["身份"].ToString().Trim() == "学生")
                                {
                                    modelFUser.UserIdentity = "S";
                                }
                                else
                                {
                                    modelFUser.UserIdentity = "T";
                                    modelFUser.UserPost = UserPost.普通老师;
                                    modelFUser.Subject = pfunction.GetCommon_DictId("7", dr[i]["学科"].ToString().Trim());
                                }
                                listModelFU.Add(modelFUser);
                                #endregion
                            }
                            else
                            {
                                userId = modelFUser.UserId;
                            }
                            #endregion
                            #region 更新群组User_Id
                            if (dr[i]["身份"].ToString().Trim() == "班主任")
                            {
                                modelUG = new BLL_UserGroup().GetModel(userGroupParentId);
                                modelUG.User_ID = userId;
                            }
                            #endregion
                            #region 群组成员表
                            Model_UserGroup_Member modelUGM = new Model_UserGroup_Member();
                            modelUGM.UserGroup_Member_Id = Guid.NewGuid().ToString();
                            modelUGM.UserGroup_Id = userGroupParentId;
                            modelUGM.User_ID = userId;
                            modelUGM.User_ApplicationStatus = "passed";
                            modelUGM.User_ApplicationTime = DateTime.Now;
                            modelUGM.User_ApplicationReason = "导入用户";
                            modelUGM.User_ApplicationPassTime = DateTime.Now;
                            modelUGM.UserStatus = 0;
                            if (dr[i]["身份"].ToString().Trim() == "学生")
                            {
                                modelUGM.MembershipEnum = MembershipEnum.student.ToString();
                            }
                            else if (dr[i]["身份"].ToString().Trim() == "班主任")
                            {
                                modelUGM.MembershipEnum = MembershipEnum.headmaster.ToString();
                            }
                            else if (dr[i]["身份"].ToString().Trim() == "代课老师")
                            {
                                modelUGM.MembershipEnum = MembershipEnum.teacher.ToString();
                            }
                            modelUGM.CreateUser = loginUser.SysUser_ID;
                            listModelUGM.Add(modelUGM);
                            #endregion
                        }
                        exData = new BLL_UserGroup_Member().ImportClassMemberData(listModelFU, listModelUGM, modelUG);
                    }
                    strLog = "班级成员导入信息 ： 操作人ID：【" + loginUser.SysUser_ID + "】";
                }
                if (exData > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'>layer.msg('【" + dr.Count() + "】班级成员被成功导入',{time:1000,icon:1},function(){historyBack();});</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'>layer.msg('操作失败',{time:2000,icon:2});</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'>layer.msg('操作失败',{time:2000,icon:2});</script>");
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("导入班级成员失败：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message));
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
                strAlert = "<font color='green'>全部班级成员都已验证通过</font>";
            }
            else
            {
                strAlert = "<font color='green'>【" + dr.Count() + "】班级成员验证通过</font>";
            }
            #region
            if (strQtype == "1")
            {
                strGoodData = string.Empty;
                strGoodData += "<div class='alert alert-success'>" + strAlert + "</div>";
                strGoodData += "<table class='table table-hover table-bordered'>";
                strGoodData += "<thead>";
                strGoodData += "<tr>";
                strGoodData += "<th>姓名</th>";
                strGoodData += "<th>登录账号</th>";
                strGoodData += "<th>身份</th>";
                strGoodData += "<th>学科</th>";
                strGoodData += "</tr>";
                strGoodData += "</thead>";
                strGoodData += "<tbody>";
                for (int i = 0; i < dr.Count(); i++)
                {
                    strGoodData += "<tr>";
                    strGoodData += "<td>" + dr[i]["姓名"] + "</td>";
                    strGoodData += "<td>" + dr[i]["登录账号"] + "</td>";
                    strGoodData += "<td>" + dr[i]["身份"] + "</td>";
                    strGoodData += "<td>" + dr[i]["学科"] + "</td>";
                    strGoodData += "</tr>";
                }
                strGoodData += "</tbody>";
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
                strAlert = "<font color='red'><div class='clearDiv'></div>全部班级成员都没有验证通过</font>";
            }
            else
            {
                strAlert = "<font color='red'>【" + dr.Count() + "】班级成员验证没通过</font>";
            }
            #region
            if (strQtype == "1")
            {
                strBadData = string.Empty;
                strBadData += "<div class='alert alert-danger'>" + strAlert + "</div>";
                strBadData += "<table class='table table-hover table-bordered'>";
                strBadData += "<thead>";
                strBadData += "<tr>";
                strBadData += "<th>姓名</th>";
                strBadData += "<th>登录账号</th>";
                strBadData += "<th>身份</th>";
                strBadData += "<th>学科</th>";
                strBadData += "<th>错误提示</th>";
                strBadData += "</tr>";
                strBadData += "</thead>";
                strBadData += "<tbody>";

                for (int i = 0; i < dr.Count(); i++)
                {
                    strBadData += "<tr>";
                    strBadData += "<td>" + dr[i]["姓名"] + "</td>";
                    strBadData += "<td>" + dr[i]["登录账号"] + "</td>";
                    strBadData += "<td>" + dr[i]["身份"] + "</td>";
                    strBadData += "<td>" + dr[i]["学科"] + "</td>";
                    strBadData += "<td>" + dr[i]["ErrorData"] + "</td>";
                    strBadData += "</tr>";
                }
                strBadData += "</tbody>";

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
            int length = 248;
            bool bTemp = true;
            strErr = string.Empty;

            switch (type)
            {
                #region
                case "1":
                    if (dr["姓名"].ToString().Trim() == "")
                    {
                        bTemp = false;
                        strErr += "姓名为空值<br>";
                    }
                    if (dr["姓名"].ToString().Length > 50)
                    {
                        strErr += "姓名值过长（不能超过50个汉字或字母）<br>";
                        bTemp = false;
                    }
                    if (dr["登录账号"].ToString().Trim() == "")
                    {
                        bTemp = false;
                        strErr += "登录账号为空值<br>";
                    }
                    if (dr["登录账号"].ToString().Length > 20)
                    {
                        strErr += "登录账号值过长（不能超过20个汉字或字母）<br>";
                        bTemp = false;
                    }
                    if (bTemp)
                    {
                        bool verifySchoolUserExists = new BLL_VW_UserOnClassGradeSchool().VerifyExistsByStrWhere(" (SchoolId is null or SchoolId!='" + schoolId + "') and UserName='" + dr["登录账号"].ToString().Trim().Filter() + "' ");
                        if (verifySchoolUserExists)
                        {
                            bTemp = false;
                            strErr += "登录账号已在其他学校存在<br>";
                        }
                    }
                    if (bTemp && new BLL_VW_UserOnClassGradeSchool().VerifyExistsByStrWhere(" ClassId='" + userGroupParentId + "' and UserName='" + dr["登录账号"].ToString().Trim().Filter() + "' "))
                    {
                        bTemp = false;
                        strErr += "班级成员已存在<br>";
                    }
                    if (dr["身份"].ToString().Trim() == "")
                    {
                        bTemp = false;
                        strErr += "身份为空值<br>";
                    }
                    if (dr["身份"].ToString().Length > 10)
                    {
                        strErr += "身份值过长（不能超过10个汉字或字母）<br>";
                        bTemp = false;
                    }
                    if (dr["身份"].ToString().Trim() != "班主任" && dr["身份"].ToString().Trim().Filter() != "代课老师" && dr["身份"].ToString().Trim().Filter() != "学生")
                    {
                        bTemp = false;
                        strErr += "身份值填写错误<br>";
                    }
                    if (bTemp && dr["身份"].ToString().Trim() != "学生")
                    {
                        if (dr["学科"].ToString().Trim() == "")
                        {
                            bTemp = false;
                            strErr += "学科为空值<br>";
                        }
                    }
                    if (bTemp && dr["身份"].ToString().Trim() == "班主任")
                    {
                        if (new BLL_VW_UserOnClassGradeSchool().VerifyExistsByStrWhere(" ClassId='" + userGroupParentId + "' and ClassMembershipEnum='" + MembershipEnum.headmaster + "' "))
                        {
                            bTemp = false;
                            strErr += "班主任已存在<br>";
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
            string sheetName = "班级成员$";
            if (strQtype == "1")
            {
                sheetName = "班级成员$";
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