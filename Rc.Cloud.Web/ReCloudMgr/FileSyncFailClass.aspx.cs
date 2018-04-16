using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.BLL;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Configuration;
using System.Text;
using Rc.Common.Config;
using System.Data;
using System.IO;
using System.Collections;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class FileSyncFailClass : System.Web.UI.Page
    {
        StringBuilder strLog = new StringBuilder();
        public string TimeLenght = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetDataList(int PageIndex, int PageSize)
        {
            try
            {
                string strWhere = string.Empty;
                BLL_FileSyncRecordFail bll = new BLL_FileSyncRecordFail();
                strWhere = "";
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                DataTable dt = bll.GetListByPageForFileSyncRecordFail("", " SyncFailTime", (PageIndex - 1) * PageSize + 1, PageIndex * PageSize).Tables[0];

                List<object> listReturn = new List<object>();
                int RecordCount = bll.GetRecordCount("");
                int inum = 1;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listReturn.Add(new
                        {
                            Num = inum + PageSize * (PageIndex - 1),
                            SyncFailTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["SyncFailTime"].ToString()),
                            Name = dt.Rows[i]["Resource_Name"].ToString(),
                            type = dt.Rows[i]["File_Type"].ToString()
                        });
                        inum++;
                    }
                }

                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = RecordCount,
                        list = listReturn
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "暂无数据"
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }

        protected void btnOn_Click(object sender, EventArgs e)
        {
            try
            {
                //删除
                BLL_FileSyncRecordFail fsrfbll = new BLL_FileSyncRecordFail();
                fsrfbll.DeleteClass(" Resource_Type in ('b8ea8767-4ae6-4b90-883f-93415a14e461','f1d1db22-c427-47e6-a8af-13d516d90633','e3a3e85e-2903-4ae6-ba17-f7f2ad9d7e0d')");
                List<Rc.Model.Resources.Model_ResourceToResourceFolder> rtrflist = new List<Model_ResourceToResourceFolder>();
                string StrWhere = string.Empty;
                string StartTime = txtStartTime.Text;
                string EndTime = txtEndTiem.Text;
                string uploadPath = "\\Upload\\Resource\\";
                string filePath = string.Empty;
                string imgPath = string.Empty;
                string savePath = string.Empty;
                string StrText = string.Empty;
                StrWhere = " Resource_Class='" + Resource_ClassConst.云资源 + "'  ";
                if (!string.IsNullOrEmpty(StartTime))
                {
                    StrWhere += " and  CreateTime >= '" + StartTime.Filter() + "'";
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    StrWhere += " and CreateTime <= '" + EndTime.Filter() + "'";
                }
                if (!string.IsNullOrEmpty(hidtxtBook.Value))
                {
                    StrWhere += " and Book_ID='" + hidtxtBook.Value + "'";
                }
                StrWhere += " and (Resource_Type in ('b8ea8767-4ae6-4b90-883f-93415a14e461','f1d1db22-c427-47e6-a8af-13d516d90633','e3a3e85e-2903-4ae6-ba17-f7f2ad9d7e0d'))";

                BLL_ResourceToResourceFolder rtrfbll = new BLL_ResourceToResourceFolder();
                rtrflist = rtrfbll.GetModelList(StrWhere);

                if (rtrflist != null)
                {
                    string SqlTemp = "delete from FileSyncRecordFail where Resource_Type in ('b8ea8767-4ae6-4b90-883f-93415a14e461','f1d1db22-c427-47e6-a8af-13d516d90633','e3a3e85e-2903-4ae6-ba17-f7f2ad9d7e0d')";
                    Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(SqlTemp);
                    foreach (var item in rtrflist)
                    {
                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", item.ParticularYear, item.GradeTerm,
                                        item.Resource_Version, item.Subject);

                        #region 教案
                        if ("1" == "1")
                        {

                            switch (item.Resource_Type)
                            {
                                case Resource_TypeConst.ScienceWord类型文件:
                                    filePath = "swDocument\\";
                                    imgPath = "swView\\";
                                    StrText = "ScienceWord类型文件";
                                    break;
                                case Resource_TypeConst.class类型微课件:
                                    filePath = "microClassDocument\\";
                                    imgPath = "microClassView\\";
                                    StrText = "class类型微课件";
                                    break;
                                case Resource_TypeConst.class类型文件:
                                    filePath = "classDocument\\";
                                    imgPath = "classView\\";
                                    StrText = "class类型文件";
                                    break;
                            }
                            #region 判断源文件 和html文件
                            if (!File.Exists(Server.MapPath(uploadPath + filePath + savePath + item.ResourceToResourceFolder_Id + "." + item.File_Suffix)))
                            {
                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + filePath + savePath + item.ResourceToResourceFolder_Id + "." + item.File_Suffix), item.Resource_Type, StrText);
                            }
                            if (!File.Exists(Server.MapPath(uploadPath + filePath + savePath + item.ResourceToResourceFolder_Id + ".htm")))
                            {
                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + filePath + savePath + item.ResourceToResourceFolder_Id + ".htm"), item.Resource_Type, StrText);
                            }
                            #endregion
                            #region 判断图片
                            BLL_ResourceToResourceFolder_img rtrfimgbll = new BLL_ResourceToResourceFolder_img();
                            DataTable dt = rtrfimgbll.GetList(" ResourceToResourceFolder_id='" + item.ResourceToResourceFolder_Id + "'").Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (!File.Exists(Server.MapPath(uploadPath + imgPath + savePath + dt.Rows[i]["ResourceToResourceFolder_img_id"] + ".jpg")))
                                    {
                                        InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + imgPath + savePath + dt.Rows[i]["ResourceToResourceFolder_img_id"] + ".jpg"), item.Resource_Type, StrText + "jpg");
                                    }
                                }
                            }
                            #endregion

                        }
                        #endregion
                        #region 试卷
                        else
                        {
                            BLL_TestQuestions tqbll = new BLL_TestQuestions();
                            List<Model_TestQuestions> tqlist = new List<Model_TestQuestions>();
                            tqlist = tqbll.GetModelList("ResourceToResourceFolder_Id='" + item.ResourceToResourceFolder_Id + "'");
                            if (tqlist != null)
                            {
                                foreach (var tqitem in tqlist)
                                {
                                    #region 题干testQuestionBody
                                    if (!File.Exists(Server.MapPath(uploadPath + "testQuestionBody\\" + savePath + tqitem.TestQuestions_Id + ".htm")))
                                    {
                                        InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "testQuestionBody\\" + savePath + tqitem.TestQuestions_Id + ".htm"), item.Resource_Type, "题干htm");
                                    }
                                    if (!File.Exists(Server.MapPath(uploadPath + "testQuestionBody\\" + savePath + tqitem.TestQuestions_Id + ".txt")))
                                    {
                                        InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "testQuestionBody\\" + savePath + tqitem.TestQuestions_Id + ".txt"), item.Resource_Type, "题干txt");
                                    }
                                    #endregion

                                    #region 分值表
                                    BLL_TestQuestions_Score tqsbll = new BLL_TestQuestions_Score();
                                    DataTable tqsdt = tqsbll.GetList("TestQuestions_Id='" + tqitem.TestQuestions_Id + "'").Tables[0];
                                    if (tqsdt.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < tqsdt.Rows.Count; k++)
                                        {
                                            #region 选项testQuestionOption
                                            if (!File.Exists(Server.MapPath(uploadPath + "testQuestionOption\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt")))
                                            {
                                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "testQuestionOption\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt"), item.Resource_Type, "选项txt");
                                            }
                                            #endregion
                                            #region 答案testQuestionCurrent
                                            if (!File.Exists(Server.MapPath(uploadPath + "testQuestionCurrent\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt")))
                                            {
                                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "testQuestionCurrent\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt"), item.Resource_Type, "答案txt");
                                            }
                                            #endregion
                                            #region 解析AnalyzeData/AnalyzeHtml
                                            if (!File.Exists(Server.MapPath(uploadPath + "AnalyzeData\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt")))
                                            {
                                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "AnalyzeData\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt"), item.Resource_Type, "解析txt");
                                            }
                                            if (!File.Exists(Server.MapPath(uploadPath + "AnalyzeHtml\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".htm")))
                                            {
                                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "AnalyzeHtml\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".htm"), item.Resource_Type, "解析htm");
                                            }
                                            #endregion
                                            #region 强化训练TrainData/TrainHtml
                                            if (!File.Exists(Server.MapPath(uploadPath + "TrainData\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt")))
                                            {
                                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "TrainData\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".txt"), item.Resource_Type, "强化训练txt");
                                            }
                                            if (!File.Exists(Server.MapPath(uploadPath + "TrainHtml\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".htm")))
                                            {
                                                InsertFail(item.ResourceToResourceFolder_Id, item.Book_ID, (uploadPath + "TrainHtml\\" + savePath + tqsdt.Rows[k]["TestQuestions_Score_ID"] + ".htm"), item.Resource_Type, "强化训练htm");
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                }
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {
                new BLL_clsAuth().AddLogErrorFromBS("检测同步完成情况失败：", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString()
                   , ex.TargetSite.Name.ToString(), ex.Message));
                Response.Redirect(Request.Url.ToString());
            }
        }

        /// <summary>
        /// 插入失败表中
        /// </summary>
        /// <param name="ResourceToResourceFolder_id"></param>
        /// <param name="BookId"></param>
        /// <param name="FileUrl"></param>
        /// <param name="Resource_Type"></param>
        /// <param name="Text"></param>
        public void InsertFail(string ResourceToResourceFolder_id, string BookId, string FileUrl, string Resource_Type, string Text)
        {
            try
            {
                Model_FileSyncRecordFail fsrf = new Model_FileSyncRecordFail();
                BLL_FileSyncRecordFail fsrfbll = new BLL_FileSyncRecordFail();
                fsrf.FileSyncRecordFail_Id = Guid.NewGuid().ToString();
                fsrf.Book_Id = BookId;
                fsrf.SyncFailTime = DateTime.Now;
                fsrf.ResourceToResourceFolder_Id = ResourceToResourceFolder_id;
                fsrf.FileUrl = FileUrl;
                fsrf.File_Type = Text;
                fsrf.Resource_Type = Resource_Type;
                fsrfbll.Add(fsrf);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}