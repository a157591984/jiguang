using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Data;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class VerifySchoolPublicUrl : System.Web.UI.Page
    {
        //请求地址
        protected string hostPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            hostPath = Rc.Cloud.Web.Common.pfunction.getHostPath() + "/";
            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='自动检测学校公网' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSql).ToString();
            if (str == "")
            {
                Thread thread1 = new Thread(new ThreadStart(VerifyUrl));
                if (!thread1.IsAlive)
                {
                    thread1.Start();

                }
            }
            else
            {
                strSql = @"update FileSyncExecRecord set FileSyncExecRecord_Status='2',FileSyncExecRecord_Remark='已执行120分钟，超时，自动更新状态'
where FileSyncExecRecord_Type='自动检测学校公网' and FileSyncExecRecord_Status='0' and DateDiff(MI,FileSyncExecRecord_TimeStart,getdate())>120 ";
                Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(strSql);
            }
        }

        private void VerifyUrl()
        {
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            #region 记录检测开始信息
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "自动检测学校公网";
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "正在检测...";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
            #endregion
            try
            {
                Model_SendMessageTemplate model = new BLL_SendMessageTemplate().GetModelBySType(Rc.Model.Resources.SMSPAYTemplateEnum.SMS.ToString());

                #region 检测 主web服务器，教案服务器，习题集服务器
                string strName = string.Empty;
                string strStudentAnswerWebSiteUrl = Rc.Common.ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl");
                string strTeachingPlanWebSiteUrl = Rc.Common.ConfigHelper.GetConfigString("TeachingPlanWebSiteUrl");
                string strTestWebSiteUrl = Rc.Common.ConfigHelper.GetConfigString("TestWebSiteUrl");
                
                if (hostPath != strStudentAnswerWebSiteUrl)
                {
                    #region 检测 主web服务器
                    strName = "主web服务器";
                    string getResult = Rc.Common.RemotWeb.PostDataToServer(strStudentAnswerWebSiteUrl + "AuthApi/?key=onlinecheck", "", System.Text.Encoding.UTF8, "GET");
                    string schoolUrl = strStudentAnswerWebSiteUrl.Replace("http://", "").TrimEnd('/');
                    string strWhere = string.Empty;
                    #region 给运营平台发送短信
                    if (string.IsNullOrEmpty(getResult)) // 无法访问
                    {
                        strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' and datediff(MI,ctime,getdate())<180 order by CTime desc "
                            , model.Mobile, strName);
                        DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                        if (dt.Rows.Count == 0 || (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() != "false"))
                        {
                            // 180分钟内未发送短信 或 最新短信不是无法访问
                            Dictionary<string, string> Para = new Dictionary<string, string>();
                            Para.Add("template_id", "10580");
                            Para.Add("template_para_schoolname", strName);
                            Para.Add("template_para_schoolurl", schoolUrl);
                            string msg = (strName + schoolUrl + "无法访问");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "false", "");
                        }
                    }
                    else
                    {
                        strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' order by CTime desc "
                            , model.Mobile, strName);
                        DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                        if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "false")
                        {
                            Dictionary<string, string> Para = new Dictionary<string, string>();
                            Para.Add("template_id", "10581");
                            Para.Add("template_para_schoolname", strName);
                            Para.Add("template_para_schoolurl", schoolUrl);
                            string msg = (strName + schoolUrl + "已恢复正常");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "true", "");
                        }
                    }
                    #endregion
                    #endregion
                }

                if (strTeachingPlanWebSiteUrl != strStudentAnswerWebSiteUrl && strTeachingPlanWebSiteUrl != hostPath)
                {
                    #region 检测 教案服务器
                    strName = "教案服务器";
                    string getResult = Rc.Common.RemotWeb.PostDataToServer(strTeachingPlanWebSiteUrl + "AuthApi/?key=onlinecheck", "", System.Text.Encoding.UTF8, "GET");
                    string schoolUrl = strTeachingPlanWebSiteUrl.Replace("http://", "").TrimEnd('/');
                    string strWhere = string.Empty;
                    #region 给运营平台发送短信
                    if (string.IsNullOrEmpty(getResult)) // 无法访问
                    {
                        strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' and datediff(MI,ctime,getdate())<180 order by CTime desc "
                            , model.Mobile, strName);
                        DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                        if (dt.Rows.Count == 0 || (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() != "false"))
                        {
                            // 180分钟内未发送短信 或 最新短信不是无法访问
                            Dictionary<string, string> Para = new Dictionary<string, string>();
                            Para.Add("template_id", "10580");
                            Para.Add("template_para_schoolname", strName);
                            Para.Add("template_para_schoolurl", schoolUrl);
                            string msg = (strName + schoolUrl + "无法访问");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "false", "");
                        }
                    }
                    else
                    {
                        strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' order by CTime desc "
                            , model.Mobile, strName);
                        DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                        if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "false")
                        {
                            Dictionary<string, string> Para = new Dictionary<string, string>();
                            Para.Add("template_id", "10581");
                            Para.Add("template_para_schoolname", strName);
                            Para.Add("template_para_schoolurl", schoolUrl);
                            string msg = (strName + schoolUrl + "已恢复正常");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "true", "");
                        }
                    }
                    #endregion
                    #endregion
                }
                if (strTestWebSiteUrl != strStudentAnswerWebSiteUrl && strTestWebSiteUrl != strTeachingPlanWebSiteUrl && strTestWebSiteUrl != hostPath)
                {
                    #region 检测 习题集服务器
                    strName = "习题集服务器";
                    string getResult = Rc.Common.RemotWeb.PostDataToServer(strTeachingPlanWebSiteUrl + "AuthApi/?key=onlinecheck", "", System.Text.Encoding.UTF8, "GET");
                    string schoolUrl = strTeachingPlanWebSiteUrl.Replace("http://", "").TrimEnd('/');
                    string strWhere = string.Empty;
                    #region 给运营平台发送短信
                    if (string.IsNullOrEmpty(getResult)) // 无法访问
                    {
                        strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' and datediff(MI,ctime,getdate())<180 order by CTime desc "
                            , model.Mobile, strName);
                        DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                        if (dt.Rows.Count == 0 || (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() != "false"))
                        {
                            // 180分钟内未发送短信 或 最新短信不是无法访问
                            Dictionary<string, string> Para = new Dictionary<string, string>();
                            Para.Add("template_id", "10580");
                            Para.Add("template_para_schoolname", strName);
                            Para.Add("template_para_schoolurl", schoolUrl);
                            string msg = (strName + schoolUrl + "无法访问");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "false", "");
                        }
                    }
                    else
                    {
                        strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' order by CTime desc "
                            , model.Mobile, strName);
                        DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                        if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "false")
                        {
                            Dictionary<string, string> Para = new Dictionary<string, string>();
                            Para.Add("template_id", "10581");
                            Para.Add("template_para_schoolname", strName);
                            Para.Add("template_para_schoolurl", schoolUrl);
                            string msg = (strName + schoolUrl + "已恢复正常");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "true", "");
                        }
                    }
                    #endregion
                    #endregion
                }

                #endregion

                List<Model_ConfigSchool> list = new List<Model_ConfigSchool>();
                list = new BLL_ConfigSchool().GetModelList("");
                string Sql = string.Format(@" select cs.School_Id,cs.School_Name,ssp.PhoneNum from 
ConfigSchool cs
inner join [dbo].[SchoolSMS_Person] ssp on ssp.School_Id=cs.School_Id ");
                DataTable dtPerson = Rc.Common.DBUtility.DbHelperSQL.Query(Sql).Tables[0];
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(model.Mobile) && !string.IsNullOrEmpty(item.D_PublicValue))
                    {
                        string getResult = Rc.Common.RemotWeb.PostDataToServer(item.D_PublicValue + "AuthApi/?key=onlinecheck", "", System.Text.Encoding.UTF8, "GET");
                        string schoolUrl = item.D_PublicValue.Replace("http://", "").TrimEnd('/');
                        string strWhere = string.Empty;
                        #region 给运营平台发送短信
                        if (string.IsNullOrEmpty(getResult)) // 无法访问
                        {
                            strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' and datediff(MI,ctime,getdate())<180 order by CTime desc "
                                , model.Mobile, item.School_Name);
                            DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                            if (dt.Rows.Count == 0 || (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() != "false"))
                            {
                                // 180分钟内未发送短信 或 最新短信不是无法访问
                                Dictionary<string, string> Para = new Dictionary<string, string>();
                                Para.Add("template_id", "10580");
                                Para.Add("template_para_schoolname", item.School_Name);
                                Para.Add("template_para_schoolurl", schoolUrl);
                                string msg = (item.School_Name + schoolUrl + "无法访问");
                                Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "false", item.School_ID);
                            }
                        }
                        else
                        {
                            strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and Content like '%{1}%' order by CTime desc "
                                , model.Mobile, item.School_Name);
                            DataTable dt = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                            if (dt.Rows.Count > 0 && dt.Rows[0]["Status"].ToString() == "false")
                            {
                                Dictionary<string, string> Para = new Dictionary<string, string>();
                                Para.Add("template_id", "10581");
                                Para.Add("template_para_schoolname", item.School_Name);
                                Para.Add("template_para_schoolurl", schoolUrl);
                                string msg = (item.School_Name + schoolUrl + "已恢复正常");
                                Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", model.Mobile, Para, msg, "自动检测学校公网", "true", item.School_ID);
                            }
                        }
                        #endregion
                        #region 给学校群发短信
                        if (dtPerson.Rows.Count > 0)
                        {
                            SendMsgSchoolPerson(dtPerson, item.School_ID, model.Mobile, item.D_PublicValue, getResult, schoolUrl);
                        }
                        #endregion
                    }
                }
                #region 记录检测结束信息并保存数据
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "检测成功完成";
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";

                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                #endregion
            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "检测失败：" + ex.Message.ToString();
                bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
            }

        }
        /// <summary>
        /// 给学校群发短息
        /// </summary>
        /// <param name="School_Id"></param>
        /// <param name="AdminMobile"></param>
        /// <param name="D_PublicValue"></param>
        /// <param name="getResult"></param>
        /// <param name="schoolUrl"></param>
        public void SendMsgSchoolPerson(DataTable dt, string School_Id, string AdminMobile, string D_PublicValue, string getResult, string schoolUrl)
        {
            try
            {

                if (!string.IsNullOrEmpty(School_Id) && !string.IsNullOrEmpty(AdminMobile) && !string.IsNullOrEmpty(D_PublicValue))
                {

                    //所有人
                    DataRow[] dr = dt.Select("School_Id='" + School_Id + "'");
                    string FalsePhone = string.Empty;//不可以访问站点手机号
                    string TruePhone = string.Empty;//可以访问站点手机号
                    #region 存在配置
                    if (dr.Length > 0)//存在配置
                    {
                        foreach (DataRow item in dr)
                        {
                            string strWhere = string.Empty;
                            if (string.IsNullOrEmpty(getResult)) // 无法访问
                            {

                                strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and SchoolId='{1}' and datediff(MI,ctime,getdate())<180 order by CTime desc "
                                    , item["PhoneNum"].ToString(), School_Id);
                                DataTable dtY = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                                if (dtY.Rows.Count == 0 || (dtY.Rows.Count > 0 && dtY.Rows[0]["Status"].ToString() != "false"))
                                {
                                    FalsePhone += item["PhoneNum"].ToString() + ",";
                                    // 180分钟内未发送短信 或 最新短信不是无法访问
                                }
                            }
                            else
                            {

                                strWhere = string.Format(" Mobile='{0}' and SType='自动检测学校公网' and SchoolId='{1}' order by CTime desc "
                                    , item["PhoneNum"].ToString(), School_Id);
                                DataTable dtN = new BLL_SendSMSRecord().GetList(strWhere).Tables[0];
                                if (dtN.Rows.Count > 0 && dtN.Rows[0]["Status"].ToString() == "false")
                                {
                                    TruePhone += item["PhoneNum"].ToString() + ",";
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(TruePhone.TrimEnd(',')))//正常访问
                        {
                            Dictionary<string, string> ParaTrue = new Dictionary<string, string>();
                            ParaTrue.Add("template_id", "10581");
                            ParaTrue.Add("template_para_schoolname", dr[0]["School_Name"].ToString());
                            ParaTrue.Add("template_para_schoolurl", schoolUrl);
                            string msgTrue = (dr[0]["School_Name"].ToString() + schoolUrl + "已恢复正常");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", TruePhone.TrimEnd(','), ParaTrue, msgTrue, "自动检测学校公网", "true", School_Id);
                        }
                        if (!string.IsNullOrEmpty(FalsePhone.TrimEnd(',')))//无法访问
                        {
                            Dictionary<string, string> ParaFalse = new Dictionary<string, string>();
                            ParaFalse.Add("template_id", "10580");
                            ParaFalse.Add("template_para_schoolname", dr[0]["School_Name"].ToString());
                            ParaFalse.Add("template_para_schoolurl", schoolUrl);
                            string msgFalse = (dr[0]["School_Name"].ToString() + schoolUrl + "无法访问");
                            Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", FalsePhone.TrimEnd(','), ParaFalse, msgFalse, "自动检测学校公网", "false", School_Id);
                        }
                    }
                    #endregion
                    //#region 不存在配置通知运营平台
                    //else
                    //{
                    //    //Dictionary<string, string> Para1 = new Dictionary<string, string>();
                    //    //Para1.Add("template_id", "10581");
                    //    //Para1.Add("template_para_schoolname", dr[0]["School_Name"].ToString());
                    //    //Para1.Add("template_para_schoolurl", schoolUrl);
                    //    //string msg = (dr[0]["School_Name"].ToString() + schoolUrl + "未配置收件人");
                    //    //// Rc.Cloud.Web.Common.pfunction.SendSMS_New("/sendtongzhi/", AdminMobile, Para1, msg, "自动检测学校公网", "true", School_Id);
                    //}
                    //#endregion
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("发送短信失败", "异常：" + ex.Message.ToString());
            }
        }

    }
}

