using Rc.Cloud.Web.Common;
using Rc.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web.test
{
    public partial class DownloadPlanFiles : System.Web.UI.Page
    {
        string strLocalPhsicalPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strLocalPhsicalPath = "E:\\Document\\Upload\\{0}\\";
            strLocalPhsicalPath = string.Format(strLocalPhsicalPath, txtFolder.Text.Trim());
            Response.Write(strLocalPhsicalPath);
            if (!IsPostBack)
            {

                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //入学年份
                int years = DateTime.Now.Year;
                Rc.Cloud.Web.Common.pfunction.SetDdlStartSchoolYear(ddlYear, years - 5, years + 1, true, "入学年份");
                //年级学期
                strWhere = " D_Type='6' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlGradeTerm, dt, "D_Name", "Common_Dict_ID", "--年级学期--");
                //教材版本
                strWhere = " D_Type='3' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Version, dt, "D_Name", "Common_Dict_ID", "--教材版本--");
                //学科
                strWhere = " D_Type='7' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlSubject, dt, "D_Name", "Common_Dict_ID", "--学科--");

            }
        }
        /// <summary>
        /// 得到教案,htm文件的相关数据信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFilePlan()
        {
            #region 读取需要下载的数据
            string strSql = string.Empty;
            string strSqlTemp = string.Empty;
            string Year = ddlYear.SelectedValue;
            string GradeTerm = ddlGradeTerm.SelectedValue;
            string Resource_Version = ddlResource_Version.SelectedValue;
            string Subject = ddlSubject.SelectedValue;
            if (!string.IsNullOrEmpty(Year) && Year != "-1")
            {
                strSqlTemp += string.Format(" and ParticularYear='{0}'", Year);
            }
            if (!string.IsNullOrEmpty(GradeTerm) && GradeTerm != "-1")
            {
                strSqlTemp += string.Format(" and GradeTerm='{0}'", GradeTerm);
            }
            if (!string.IsNullOrEmpty(Resource_Version) && Resource_Version != "-1")
            {
                strSqlTemp += string.Format(" and Resource_Version='{0}'", Resource_Version);
            }
            if (!string.IsNullOrEmpty(Subject) && Subject != "-1")
            {
                strSqlTemp += string.Format(" and Subject='{0}'", Subject);
            }
            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select t1.Book_Id, ParticularYear,GradeTerm,Resource_Version,Subject,Resource_Type,ResourceToResourceFolder_Id,ResourceToResourceFolder_Id  as FileName 
                from ResourceToResourceFolder t1
                left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
                where 1=1
                and t1.Resource_Type in('{0}','{1}','{2}')
                and ba.AuditState='1'
                {3}
                order by t1.Createtime desc", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件, strSqlTemp);
            DataTable dtFilePlan = new DataTable();

            dtFilePlan = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFilePlan;
            #endregion
        }
        /// <summary>
        /// 得到教案预览图片文件的相关数据信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDtFilePlanView()
        {
            #region 读取需要同步的数据
            string strSql = string.Empty;
            string strSqlTemp = string.Empty;
            string Year = ddlYear.SelectedValue;
            string GradeTerm = ddlGradeTerm.SelectedValue;
            string Resource_Version = ddlResource_Version.SelectedValue;
            string Subject = ddlSubject.SelectedValue;
            if (!string.IsNullOrEmpty(Year) && Year != "-1")
            {
                strSqlTemp += string.Format(" and ParticularYear='{0}'", Year);
            }
            if (!string.IsNullOrEmpty(GradeTerm) && GradeTerm != "-1")
            {
                strSqlTemp += string.Format(" and GradeTerm='{0}'", GradeTerm);
            }
            if (!string.IsNullOrEmpty(Resource_Version) && Resource_Version != "-1")
            {
                strSqlTemp += string.Format(" and Resource_Version='{0}'", Resource_Version);
            }
            if (!string.IsNullOrEmpty(Subject) && Subject != "-1")
            {
                strSqlTemp += string.Format(" and Subject='{0}'", Subject);
            }

            //教案(SW与class)BASE64内容 SW暂时使用此地址
            strSql = string.Format(@"select   t1.Book_Id,ParticularYear,GradeTerm,Resource_Type,Resource_Version,Subject,t1.ResourceToResourceFolder_id
,t2.ResourceToResourceFolder_img_id  as FileName
from ResourceToResourceFolder t1 
inner join ResourceToResourceFolder_img t2 on T1.ResourceToResourceFolder_id=t2.ResourceToResourceFolder_id
left join BookAudit ba on ba.ResourceFolder_Id=t1.Book_Id
where 1=1
and t1.Resource_Type in('{0}','{1}','{2}')
and ba.AuditState='1'
{3}
order by t2.Createtime desc", Resource_TypeConst.ScienceWord类型文件, Resource_TypeConst.class类型文件, Resource_TypeConst.class类型微课件, strSqlTemp);
            DataTable dtFilePlanView = new DataTable();

            dtFilePlanView = Rc.Common.DBUtility.DbHelperSQL.Query(strSql, 300).Tables[0];
            return dtFilePlanView;
            #endregion
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="dvFile"></param>
        /// <param name="strPathType"></param>
        /// <param name="strFileNameEx"></param>
        /// <param name="strProductPublicUrl"></param>
        /// <param name="strFileSyncExecRecord_id"></param>
        /// <param name="strRemark"></param>
        /// <param name="isCoverDownload">true: 覆盖下载</param>
        private void DownLoadFile(DataRow[] dvFile, string strPathType, string strFileNameEx, string strProductPublicUrl, string strFileSyncExecRecord_id, string strRemark, bool isCoverDownload)
        {
            bool isDownload = false;//是否要下载文件
            foreach (var item in dvFile)
            {
                #region 组织文件源的URL与本地存储的路径
                string strPathCommon = string.Empty;
                string strFileName = string.Empty;//文件名（没后缀）
                strFileName = item["FileName"].ToString();
                strPathCommon = string.Format("{0}/{1}/{2}/{3}/{4}/"
                   , strPathType
                  , item["ParticularYear"].ToString()
                  , item["GradeTerm"].ToString()
                  , item["Resource_Version"].ToString()
                  , item["Subject"].ToString());
                string strFilePath = string.Empty;
                string strFileNameFullDocument = strFileName + "." + strFileNameEx;
                string remotingFileUrl = string.Empty;
                string localFilePath = string.Empty;
                remotingFileUrl = strProductPublicUrl + "Upload/Resource/" + strPathCommon;
                localFilePath = strLocalPhsicalPath + strPathCommon;
                string remotingViewFileUrl = string.Empty;
                string localViewFilePath = string.Empty;
                #endregion
                isDownload = false;
                if (!File.Exists(localFilePath + strFileNameFullDocument))//文件不存在
                {
                    isDownload = true;
                }
                else
                {
                    if (isCoverDownload)//是覆盖下载
                    {
                        isDownload = true;
                    }
                }
                if (isDownload)
                {
                    Rc.Model.Resources.Model_FileSyncExecRecordDetail model_FileSyncExecRecordDetail = new Rc.Model.Resources.Model_FileSyncExecRecordDetail();
                    Rc.BLL.Resources.BLL_FileSyncExecRecordDetail bll_FileSyncExecRecordDetail = new Rc.BLL.Resources.BLL_FileSyncExecRecordDetail();
                    model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id = Guid.NewGuid().ToString();
                    model_FileSyncExecRecordDetail.FileSyncExecRecord_id = strFileSyncExecRecord_id;
                    model_FileSyncExecRecordDetail.Book_Id = item["Book_Id"].ToString();
                    model_FileSyncExecRecordDetail.ResourceToResourceFolder_Id = item["ResourceToResourceFolder_Id"].ToString();
                    model_FileSyncExecRecordDetail.Resource_Type = item["Resource_Type"].ToString();
                    model_FileSyncExecRecordDetail.FileUrl = strPathCommon + strFileNameFullDocument;
                    model_FileSyncExecRecordDetail.Detail_Remark = "开始同步：" + strRemark;
                    model_FileSyncExecRecordDetail.Detail_Status = "0";
                    model_FileSyncExecRecordDetail.Detail_TimeStart = DateTime.Now;
                    bll_FileSyncExecRecordDetail.Add(model_FileSyncExecRecordDetail);

                    try
                    {
                        string downloadFileMsg = pfunction.DownLoadFileByWebClient(remotingFileUrl, localFilePath, strFileNameFullDocument);

                        model_FileSyncExecRecordDetail.Detail_TimeEnd = DateTime.Now;
                        if (downloadFileMsg != "true")
                        {
                            model_FileSyncExecRecordDetail.Detail_Status = "2";
                            if (downloadFileMsg.Contains("404"))
                            {
                                model_FileSyncExecRecordDetail.Detail_Status = "1";//如果是远程服务器未找到文件，设置为状态为成功
                                // 17-08-21如果是远程服务器未找到文件，删除同步记录
                                bll_FileSyncExecRecordDetail.Delete(model_FileSyncExecRecordDetail.FileSyncExecRecordDetail_id);
                            }
                            else
                            {
                                model_FileSyncExecRecordDetail.Detail_Remark = strRemark + ":" + downloadFileMsg;
                                bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                            }
                        }
                        else
                        {
                            model_FileSyncExecRecordDetail.Detail_Remark = "同步成功：" + strRemark;
                            if (File.Exists(localFilePath + strFileNameFullDocument))
                            {
                                model_FileSyncExecRecordDetail.Detail_Status = "1";
                            }
                            else //文件不存在，更新同步状态为失败 17-09-11TS
                            {
                                model_FileSyncExecRecordDetail.Detail_Status = "2";
                            }
                            bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);
                        }
                    }
                    catch (Exception ex)
                    {
                        //同步异常 删除已创建文件 17-09-11TS
                        if (File.Exists(localFilePath + strFileNameFullDocument)) File.Delete(localFilePath + strFileNameFullDocument);

                        model_FileSyncExecRecordDetail.Detail_Remark = "同步失败，异常：" + ex.Message.ToString();
                        model_FileSyncExecRecordDetail.Detail_Status = "2";
                        bll_FileSyncExecRecordDetail.Update(model_FileSyncExecRecordDetail);

                        throw;
                    }

                }
            }
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            //生产环境web站点存放文件的主目录
            string strProductPublicUrl = ConfigurationManager.AppSettings["ProductPublicUrl"].ToString();
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            bool isCover = false;
            //string strCondition = string.Empty;
            //strCondition = string.Format("日期【{0}】；书本【{1}】【{2}】；是覆盖：【{3}】", txtSDate.Value, txtBOOK.Value, hidtxtBOOK.Value, chbCover.Checked);
            //#region 记录同步开始信息
            //Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            //Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            //model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            //model_FileSyncExecRecord.FileSyncExecRecord_Type = "同步教案";
            //model_FileSyncExecRecord.FileSyncExecRecord_Remark = "正在同步...";
            //model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            //model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            //model_FileSyncExecRecord.createUser = SysUser_ID;
            //model_FileSyncExecRecord.FileSyncExecRecord_Condition = strCondition;

            //#endregion
            //isCover = true;
            try
            {
                //bll_FileSyncExecRecord.Add(model_FileSyncExecRecord);
                //教案文件，htm文件相关数据
                DataTable dtFilePlan = GetDtFilePlan();
                #region 下载文件
                //教案SW+thm
                DataRow[] drSWdsc = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.ScienceWord类型文件));
                DownLoadFile(drSWdsc, "swDocument", "dsc", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", isCover);
                DownLoadFile(drSWdsc, "swDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案SWdsc", isCover);
                //教案class+thm
                DataRow[] drClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型文件));
                DownLoadFile(drClass, "classDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", isCover);
                DownLoadFile(drClass, "classDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "教案class", isCover);
                //微课class+thm
                DataRow[] drMicroClass = dtFilePlan.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型微课件));
                DownLoadFile(drMicroClass, "microClassDocument", "class", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", isCover);
                DownLoadFile(drMicroClass, "microClassDocument", "htm", strProductPublicUrl, strFileSyncExecRecord_id, "微课class", isCover);

                //教案预览图片文件相关数据
                DataTable dtFilePlanView = GetDtFilePlanView();
                //教案SW+预览图片
                DataRow[] drSWdscView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.ScienceWord类型文件));
                DownLoadFile(drSWdscView, "swView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案SW预览图片", isCover);
                //教案Class+预览图片
                DataRow[] drClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型文件));
                DownLoadFile(drClassView, "classView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "教案Class预览图片", isCover);
                //微课Class+预览图片
                DataRow[] drMicroClassView = dtFilePlanView.Select(string.Format(" Resource_Type='{0}'", Resource_TypeConst.class类型微课件));
                DownLoadFile(drMicroClassView, "microClassView", "jpg", strProductPublicUrl, strFileSyncExecRecord_id, "微课Class预览图片", isCover);
                #endregion

                //#region 记录同步结束信息并保存数据
                //model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                //model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步成功完成";
                //model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";

                //bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);
                //#endregion
            }
            catch (Exception ex)
            {
                //model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                //model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                //model_FileSyncExecRecord.FileSyncExecRecord_Remark = "同步失败：" + ex.Message.ToString();
                //bll_FileSyncExecRecord.Update(model_FileSyncExecRecord);

            }
        }

    }
}