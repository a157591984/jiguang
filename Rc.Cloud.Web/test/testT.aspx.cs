using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Net.Http;
using System.Net;
using System.Web.Services;
using System.IO;
using System.Threading;
using System.Data;
using Rc.Cloud.Web.Common;
using System.Net.Sockets;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Common.Config;
using System.DirectoryServices.Protocols;

namespace Rc.Cloud.Web.test
{
    public partial class testT : System.Web.UI.Page
    {
        string hw_id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(GetRFNameByRTRFId("e3e06f32-4413-41bd-862d-16d134cc9707a"));
            //CheckSchoolUser("90823925", "Aa987654321");
            //System.Drawing.Bitmap bitImg = new System.Drawing.Bitmap(Server.MapPath("1509698698.png"));//二维码图片路径
            //ThoughtWorks.QRCode.Codec.QRCodeDecoder qrDecoder = new ThoughtWorks.QRCode.Codec.QRCodeDecoder();//创建一个解码器
            //string msg = qrDecoder.decode(new ThoughtWorks.QRCode.Codec.Data.QRCodeBitmapImage(bitImg), Encoding.UTF8);//解码返回一个字符串
            //Response.Write(msg);
            //Response.Write("<br>");

            //string rurl = "http://117.143.201.162:8083/upload/resource/TrainHtml/2017/538ac246-691a-4ca6-a3d6-fe7546dc3ff1/8fad9ca9-d7e4-41e0-8c9d-c167c1520b46/b9719c2a-ab54-4d11-a583-12cc05dd97bf/";
            //string downloadFileMsg = pfunction.DownLoadFileByWebClient(rurl, "/test/", "5f6d6e05-b577-4ed0-8b4e-2f0c53c920c3.htm");
            //Response.Write(downloadFileMsg);
            //Response.Write("<br>");
            //Response.Write(downloadFileMsg.Contains("404"));
            //Response.Write("<br>");

            //Response.Write(Rc.Common.Base64.EncodeBase64("C++ C# $-_."));
            //Response.Write("<br>");
            //Response.Write(Rc.Common.Base64.DecodeBase64("QysrIEMjICQtXy4="));
            //Response.Write("<br>");
            //Response.Write(YunHuaTong.Lib.Encrypt.UrlEncode("￥ http://ww-w.tui_ti.cn/ "));
            //Response.Write("<br>");
            //Response.Write(Uri.EscapeDataString("￥ http://ww-w.tui_ti.cn/ "));
            //Response.Write("<br>");
            //Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            //Response.Write("<br>");
            //DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query("select top 10 * from F_User ").Tables[0];
            //Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            //Response.Write("<br>");
            //Response.Write("<br>");

            //string server_name = Request.ServerVariables["SERVER_NAME"]; //主机名
            //Response.Write("server_name :" + server_name);
            //Response.Write("<br>");
            //string host = Request.Url.Host; //主机名
            //Response.Write("Host :" + host);
            //Response.Write("<br>");
            //string dns = Request.Url.DnsSafeHost; //主机名
            //Response.Write("DnsSafeHost :" + dns);
            //Response.Write("<br>");
            //string aut = Request.Url.Authority;  //主机名和端口
            //Response.Write("Authority :" + aut);
            //Response.Write("<br>");

            //Response.Write("Request.Url :" + Request.Url.ToString());
            //Response.Write("<br>");


            ////接受请求的服务器端口号
            //Response.Write("Server_Port :" + Request.ServerVariables["Server_Port"]);
            //Response.Write("<br>");

            ////发出请求的远程主机的IP地址
            //Response.Write("Remote_Addr :" + Request.ServerVariables["Remote_Addr"]);
            //Response.Write("<br>");

            ////发出请求的远程主机名称
            //Response.Write("Remote_Host :" + Request.ServerVariables["Remote_Host"]);
            //Response.Write("<br>");

            ////返回接受请求的服务器地址
            //Response.Write("Local_Addr :" + Request.ServerVariables["Local_Addr"]);
            //Response.Write("<br>");

            ////返回服务器地址
            //Response.Write("Http_Host :" + Request.ServerVariables["Http_Host"]);
            //Response.Write("<br>");

            ////服务器的主机名、DNS地址或IP地址
            //Response.Write("Server_Name :" + Request.ServerVariables["Server_Name"]);
            //Response.Write("<br>");



            //Dictionary<string, string> Para = new Dictionary<string, string>();
            //Para.Add("template_id", "10580");
            //Para.Add("template_para_schoolname", "人大-附中 空格 ");
            //Para.Add("template_para_schoolurl", "47.92.11.11");
            //Rc.Cloud.Web.Common.pfunction.SendSMS_New("sendtongzhi/", "13121133313", Para, "人大附中站点无法访问", "自动检测学校公网", "false");

            //List<Model_Common_Dict> listAll = new BLL_Common_Dict().GetModelList("1=1 order by d_type,d_name");//所有数据
            //List<Model_Common_Dict> listDistict = listAll.Where((x, i) => listAll.FindIndex(z => z.D_Type == x.D_Type) == i).ToList();//去重后数据
            //// list.Distinct(new ModelComparer()).ToList();
            //StringBuilder stbHtml = new StringBuilder();
            //foreach (var item in listDistict)
            //{
            //    stbHtml.AppendFormat("<span>D_Type:{0}</span><span>D_Name:{1}</span><br>子级:<br>", item.D_Type, item.D_Name);
            //    List<Model_Common_Dict> listSub = listAll.Where(x => x.D_Type == item.D_Type).ToList();//子级数据
            //    foreach (var itemSub in listSub)
            //    {
            //        stbHtml.AppendFormat("<span>D_Name:{0}</span><br>", itemSub.D_Name);
            //    }
            //}
            //ltlCommonDict.Text = stbHtml.ToString();
            //hw_id = "99ab59fe-f23b-4d8e-9de9-19ac59959639";
            //if (!IsPostBack)
            //{
            //    LoadData();

            //    //string msg = string.Empty;
            //    //VerifyTestpaper("276d7051-ac19-4f8b-baa6-6257c088ea58", "2d3a9bca-d2da-460b-8ea4-a6df4294df14".Split(','), out  msg);
            //    //Response.Write(msg);
            //}
            //string strText = Rc.Common.DBUtility.DESEncrypt.Decrypt("4301BFBAFE079F58A570A26B95B9E46B7698F7B641B517486893E908682D618E6F6C506A851FFC877891BAFC6A704C3C1D846E4555384483F68FFBDB0B466E2B3F365F1D57199F70DC694FAABBDF6E2C6F757D6F3E61E961DD61C51B1B5FCCBF");
            //Response.Write(strText);
        }

        [WebMethod]
        public static string checklocalhost()
        {
            try
            {
                string getResult = Rc.Common.RemotWeb.PostDataToServer("http://192.168.0.104:801/" + "AuthApi/?key=onlinecheck", "", System.Text.Encoding.UTF8, "GET");
                if (getResult == "ok")
                {
                    return "1";
                }
                else
                {
                    return "2";
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        protected void btnDDD_Click(object sender, EventArgs e)
        {
            //string para = "key=testPaperAnswerSubmit&token=32F7F52D0B64CDBFEFC4C4D74F211653CC868C9A&userId=E8D7C912-D84F-4943-98DD-7140EF08E0B2&userName=&resourceId=29942cf6-5ef0-47b7-a4b0-5a3ee06cda68&productType=skill&tabId=StudentSkillNew";

            //string result = Rc.Common.RemotWeb.PostDataToServer("http://localhost:1528/AuthApi/index.aspx", para, System.Text.Encoding.UTF8, "Post");
            //Response.Write(result);

            try
            {
                Thread thread_1 = new Thread(new ThreadStart(ExecuteSubmit_1));
                if (!thread_1.IsAlive)
                {
                    thread_1.Start();
                }

                //Thread thread_2 = new Thread(new ThreadStart(ExecuteSubmit_2));
                //if (!thread_2.IsAlive)
                //{
                //    thread_2.Start();
                //}

            }
            catch (Exception ex)
            {
                Rc.Common.LogContext.AddLogInfo(Server.MapPath("/upload/resource/SubmitWrong/" + hw_id + "/") + "0.txt", ex.Message.ToString(), true);
            }

        }

        private void ExecuteSubmit_1()
        {
            try
            {
                string strSql = @"select * from (
select ROW_NUMBER() over(order by Student_Id) as Row,Student_HomeWork_Id,Student_Id from Student_HomeWork where HomeWork_Id='" + hw_id + "') t where Row between 601 and 1000";

                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    string para = "key=testPaperAnswerSubmit&token=32F7F52D0B64CDBFEFC4C4D74F211653CC868C9A&userId=" + item["Student_Id"] + "&userName=&resourceId=" + item["Student_HomeWork_Id"] + "&productType=skill&tabId=StudentSkillNew";
                    string result = Rc.Common.RemotWeb.PostDataToServer("http://localhost:1528/AuthApi/index.aspx", para, System.Text.Encoding.UTF8, "Post");
                }
            }
            catch (Exception ex)
            {
                Rc.Common.LogContext.AddLogInfo(Server.MapPath("/upload/resource/SubmitWrong/" + hw_id + "/") + "1.txt", ex.Message.ToString(), true);
            }
        }
        private void ExecuteSubmit_2()
        {
            try
            {
                string strSql = @"select * from (
select ROW_NUMBER() over(order by Student_Id) as Row,Student_HomeWork_Id,Student_Id from Student_HomeWork where HomeWork_Id='" + hw_id + "') t where Row between 501 and 1000";

                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                foreach (DataRow item in dt.Rows)
                {
                    string para = "key=testPaperAnswerSubmit&token=32F7F52D0B64CDBFEFC4C4D74F211653CC868C9A&userId=" + item["Student_Id"] + "&userName=&resourceId=" + item["Student_HomeWork_Id"] + "&productType=skill&tabId=StudentSkillNew";
                    string result = Rc.Common.RemotWeb.PostDataToServer("http://localhost:1528/AuthApi/index.aspx", para, System.Text.Encoding.UTF8, "Post");
                }
            }
            catch (Exception ex)
            {
                Rc.Common.LogContext.AddLogInfo(Server.MapPath("/upload/resource/SubmitWrong/" + hw_id + "/") + "2.txt", ex.Message.ToString(), true);
            }
        }

        private void LoadData()
        {
            //if (true)
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "temp", "layer.load();", true);
            //}
            //Rc.Common.DBUtility.DbHelperSQL.ExecuteSql("insert into testT(col) values('0')");
            //int num = 1000000;
            //for (int i = 0; i <= num; i++)
            //{
            //    if (i == num)
            //    {
            //        Rc.Common.DBUtility.DbHelperSQL.ExecuteSql("insert into testT(col) values('" + num + "')");
            //    }
            //}
        }

        //public class ModelComparer : IEqualityComparer<Model_Common_Dict>
        //{
        //    public bool Equals(Model_Common_Dict x, Model_Common_Dict y)
        //    {
        //        return x.D_Type == y.D_Type;
        //    }
        //    public int GetHashCode(Model_Common_Dict obj)
        //    {
        //        return obj.D_Type.GetHashCode();
        //    }
        //}

        public static HttpResponseMessage Upload()
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
            HttpPostedFile file = HttpContext.Current.Request.Files[0];

            // do something with the file in this space
            // {....}
            // end of file doing
            SaveAs(HttpContext.Current.Server.MapPath("~/Images/") + file.FileName, file);

            // Now we need to wire up a response so that the calling script understands what happened
            HttpContext.Current.Response.ContentType = "text/plain";
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = new { name = file.FileName };


            HttpContext.Current.Response.Write(serializer.Serialize(result));
            HttpContext.Current.Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private static void SaveAs(string saveFilePath, HttpPostedFile file)
        {
            long lStartPos = 0;
            int startPosition = 0;
            int endPosition = 0;
            var contentRange = HttpContext.Current.Request.Headers["Content-Range"];
            //bytes 10000-19999/1157632
            if (!string.IsNullOrEmpty(contentRange))
            {
                contentRange = contentRange.Replace("bytes", "").Trim();
                contentRange = contentRange.Substring(0, contentRange.IndexOf("/"));
                string[] ranges = contentRange.Split('-');
                startPosition = int.Parse(ranges[0]);
                endPosition = int.Parse(ranges[1]);
            }
            System.IO.FileStream fs;
            if (System.IO.File.Exists(saveFilePath))
            {
                fs = System.IO.File.OpenWrite(saveFilePath);
                lStartPos = fs.Length;

            }
            else
            {
                fs = new System.IO.FileStream(saveFilePath, System.IO.FileMode.Create);
                lStartPos = 0;
            }
            if (lStartPos > endPosition)
            {
                fs.Close();
                return;
            }
            else if (lStartPos < startPosition)
            {
                lStartPos = startPosition;
            }
            else if (lStartPos > startPosition && lStartPos < endPosition)
            {
                lStartPos = startPosition;
            }
            fs.Seek(lStartPos, System.IO.SeekOrigin.Current);
            byte[] nbytes = new byte[512];
            int nReadSize = 0;
            nReadSize = file.InputStream.Read(nbytes, 0, 512);
            while (nReadSize > 0)
            {
                fs.Write(nbytes, 0, nReadSize);
                nReadSize = file.InputStream.Read(nbytes, 0, 512);
            }
            fs.Close();
        }

        [WebMethod]
        public static string GetState()
        {
            Upload();
            return "";
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string strTips = string.Empty;
            string Two_WayChecklist_Id = txtTwo.Text.Trim();
            string[] arrRTRF = txtRTRF.Text.Split(',');

            VerifyTestpaper(Two_WayChecklist_Id, arrRTRF, out strTips);
            ltlTips.Text = strTips;
        }

        //276d7051-ac19-4f8b-baa6-6257c088ea58
        private bool VerifyTestpaper(string Two_WayChecklist_Id, string[] arrRTRF, out string msg)
        {
            bool flag = true;
            msg = string.Empty;
            try
            {
                BLL_Two_WayChecklistDetail bll = new BLL_Two_WayChecklistDetail();
                BLL_TestQuestions_Score bllTQScore = new BLL_TestQuestions_Score();
                DataTable dtTWC_Count = new DataTable(); // 双向细目表总分、试题数量
                DataTable dtTWC = new DataTable();       // 双向细目表明细
                DataTable dtTQ_Count = new DataTable();  // 习题集资源总分、试题数量
                DataTable dtTQScore = new DataTable();   // 习题集试题明细

                string strSql = string.Empty;
                strSql = string.Format(@"select count(1) as ICount,sum(Score) as SumScore from Two_WayChecklistDetail 
where ParentId!='0' and Two_WayChecklist_Id='{0}' ", Two_WayChecklist_Id);
                dtTWC_Count = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                DataRow drTWC_Count = dtTWC_Count.Rows[0];
                int twCount = 0;
                double twSumScore = 0;
                int.TryParse(drTWC_Count["ICount"].ToString(), out twCount);
                double.TryParse(drTWC_Count["SumScore"].ToString(), out twSumScore);

                strSql = string.Empty;
                strSql = string.Format(@"select t.* from Two_WayChecklistDetail t 
inner join Two_WayChecklistDetail t2 on t.ParentId=t2.Two_WayChecklistDetail_Id 
where t.ParentId!='0' and t.Two_WayChecklist_Id='{0}' order by t2.TestQuestions_Num,t.TestQuestions_Num ", Two_WayChecklist_Id);
                dtTWC = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];

                foreach (var rtrfId in arrRTRF)
                {
                    string strSqlTQ = string.Empty;
                    strSqlTQ = string.Format(@"select max(rtrf.Resource_Name) as Resource_Name,count(1) as ICount ,sum(t.TestQuestions_SumScore) as SumScore from (
select TestQuestions_SumScore,tq.ResourceToResourceFolder_Id from TestQuestions tq 
where tq.Parent_Id!='0' and tq.[type]='simple' and tq.ResourceToResourceFolder_Id='{0}' 
union all
select TestQuestions_SumScore,tq.ResourceToResourceFolder_Id from TestQuestions tq 
where tq.Parent_Id='0' and tq.[type]='complex' and tq.ResourceToResourceFolder_Id='{0}' 
)  t inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.ResourceToResourceFolder_Id", rtrfId);
                    dtTQ_Count = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];
                    DataRow drTQ_Count = dtTQ_Count.Rows[0];
                    int tqCount = 0;
                    double tqSumScore = 0;
                    int.TryParse(drTQ_Count["ICount"].ToString(), out tqCount);
                    double.TryParse(drTQ_Count["SumScore"].ToString(), out tqSumScore);
                    if (flag && tqCount != twCount)
                    {
                        flag = false;
                        msg = string.Format("资源【{0}】试题数不等于双向细目表明细数", drTQ_Count["Resource_Name"].ToString().ReplaceForFilter());
                    }
                    if (flag && tqSumScore != twSumScore)
                    {
                        flag = false;
                        msg = string.Format("资源【{0}】总分不等于双向细目表总分", drTQ_Count["Resource_Name"].ToString().ReplaceForFilter());
                    }
                    if (flag)
                    {
                        string strSqlScore = string.Empty;
                        strSqlScore = string.Format(@"select * from (
select tq.TestQuestions_Type,tq.[type],tq.TestQuestions_Num,tq.topicNumber,tq.TestQuestions_SumScore from TestQuestions tq 
where tq.Parent_Id!='0' and tq.[type]='simple' and tq.ResourceToResourceFolder_Id='{0}' 
union all
select tq.TestQuestions_Type,tq.[type],tq.TestQuestions_Num,tq.topicNumber,tq.TestQuestions_SumScore from TestQuestions tq 
where tq.Parent_Id='0' and tq.[type]='complex' and tq.ResourceToResourceFolder_Id='{0}'
) t order by TestQuestions_Num ", rtrfId);
                        dtTQScore = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];
                        int row = 0;
                        foreach (DataRow item in dtTWC.Rows)
                        {
                            if (flag && item["Two_WayChecklistType"].ToString() == "simple" && item["TestQuestions_Type"].ToString() != dtTQScore.Rows[row]["TestQuestions_Type"].ToString())
                            {
                                flag = false;
                                msg = string.Format("资源【{0}】第{1}题{2}与双向细目表不匹配"
                                    , drTQ_Count["Resource_Name"].ToString().ReplaceForFilter()
                                    , dtTQScore.Rows[row]["topicNumber"]
                                    , "试题类型");
                            }
                            if (flag && item["Two_WayChecklistType"].ToString() == "complex" && item["Two_WayChecklistType"].ToString() != dtTQScore.Rows[row]["type"].ToString())
                            {
                                flag = false;
                                msg = string.Format("资源【{0}】第{1}题{2}与双向细目表不匹配"
                                    , drTQ_Count["Resource_Name"].ToString().ReplaceForFilter()
                                    , dtTQScore.Rows[row]["topicNumber"]
                                    , "试题类型");
                            }

                            double twScore = 0; // 双向细目表分值
                            double tqScore = 0; // 试题分值
                            double.TryParse(item["Score"].ToString(), out twScore);
                            double.TryParse(dtTQScore.Rows[row]["TestQuestions_SumScore"].ToString(), out tqScore);
                            if (flag && twScore != tqScore)
                            {
                                flag = false;
                                msg = string.Format("资源【{0}】第{1}题{2}与双向细目表不匹配"
                                    , drTQ_Count["Resource_Name"].ToString().ReplaceForFilter()
                                    , dtTQScore.Rows[row]["topicNumber"]
                                    , "试题分值");
                            }

                            row++;
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                flag = false;
                msg = ex.Message.ToString();
            }
            return flag;
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            pfunction.DownLoadFileByWebClient(@"http://114.112.106.36/", @"D:\Novoasoft1\作业\trunk\Rc.Cloud.Web\Upload\Resource\", "15rewer.txt", "2.txt");
        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "temp", "alert('字符串不能为空');", true);
                return;
            }
            else
            {
                string temp = txtKey.Text.Trim();
                txtDeKey.Text = Rc.Common.DBUtility.DESEncrypt.EncryptConfig(temp);
            }
        }

        protected void btnDeEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "temp", "alert('字符串不能为空');", true);
                return;
            }
            else
            {
                string temp = txtKey.Text.Trim();
                txtDeKey.Text = Rc.Common.DBUtility.DESEncrypt.DecryptConfig(temp);
            }
        }

        protected void btnDelRes_Click(object sender, EventArgs e)
        {
            string strJson = string.Empty;
            try
            {
                string uploadPath = Server.MapPath("..\\Upload\\Resource\\");//存储文件基础路径
                string pathId = txtRTRFId.Text.Trim();
                if (new BLL_HomeWork().GetRecordCount_Operate("ResourceToResourceFolder_Id='" + pathId + "'") > 0)
                {
                    #region 判断运营平台存在已布置作业,无法删除
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = false,
                        errorMsg = "存在已布置作业,无法删除",
                        errorCode = "DeletePath"
                    });
                    #endregion
                }
                else
                {
                    #region 删除文件
                    Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(pathId);
                    List<string> listFileUrl = GetResourceFile(modelRTRF, uploadPath);
                    if (new BLL_Resource().DeleteResource(pathId, modelRTRF.Resource_Id))
                    {
                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = true,
                            errorMsg = "",
                            errorCode = ""
                        });
                        DeleteResourceFile(listFileUrl);
                    }
                    else
                    {
                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            status = false,
                            errorMsg = "文件删除失败",
                            errorCode = "DeletePath"
                        });
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    status = false,
                    errorMsg = ex.Message.ToString(),
                    errorCode = "DeletePath"
                });
            }
            txtResult.Text = strJson;
        }

        /// <summary>
        /// 获取资源对应文件路径 17-06-07TS
        /// </summary>
        private List<string> GetResourceFile(Model_ResourceToResourceFolder modelRTRF, string uploadPath)
        {
            try
            {
                List<string> listReturn = new List<string>();
                //生成存储路径
                string savePath = string.Empty;
                string savePathOwn = string.Empty;
                if (modelRTRF.Resource_Class == Resource_ClassConst.云资源)
                {
                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                       modelRTRF.Resource_Version, modelRTRF.Subject);
                }
                if (modelRTRF.Resource_Class == Resource_ClassConst.自有资源)
                {
                    DateTime dateTime = Convert.ToDateTime(modelRTRF.CreateTime);
                    savePathOwn = string.Format("{0}\\", dateTime.ToString("yyyy-MM-dd"));
                }

                if (modelRTRF.Resource_Type == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                {
                    string fileUrl = uploadPath + savePathOwn + "{0}\\" + savePath + "{1}.{2}";
                    #region 习题集文件
                    DataTable dtTQ = new BLL_TestQuestions().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    DataTable dtTQ_Score = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    foreach (DataRow item in dtTQ.Rows)
                    {
                        listReturn.Add(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "htm"));
                        listReturn.Add(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"], "htm"));
                    }
                    foreach (DataRow item in dtTQ_Score.Rows)
                    {
                        listReturn.Add(string.Format(fileUrl, "testQuestionCurrent", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "testQuestionOption", item["TestQuestions_Score_Id"], "txt"));

                        listReturn.Add(string.Format(fileUrl, "AnalyzeData", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "AnalyzeHtml", item["TestQuestions_Score_Id"], "htm"));

                        listReturn.Add(string.Format(fileUrl, "TrainData", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "TrainHtml", item["TestQuestions_Score_Id"], "htm"));

                        listReturn.Add(string.Format(fileUrl, "bodySub", item["TestQuestions_Score_Id"], "txt"));
                    }
                    #endregion
                }
                else
                {
                    #region 教案文件
                    string filePath = string.Empty;//文件存储路径
                    //判断产品类型
                    switch (modelRTRF.Resource_Type)
                    {
                        case Resource_TypeConst.ScienceWord类型文件:
                            filePath += "swDocument\\";
                            break;
                        case Resource_TypeConst.class类型微课件:
                            filePath += "microClassDocument\\";
                            break;
                        case Resource_TypeConst.class类型文件:
                            filePath += "classDocument\\";
                            break;
                    }
                    #region 文件及图片
                    filePath += savePath;
                    filePath = savePathOwn + filePath;

                    listReturn.Add(uploadPath + filePath + modelRTRF.ResourceToResourceFolder_Id + "." + modelRTRF.File_Suffix);
                    listReturn.Add(uploadPath + filePath + modelRTRF.ResourceToResourceFolder_Id + ".htm");

                    DataTable dtImg = new BLL_ResourceToResourceFolder_img().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    foreach (DataRow item in dtImg.Rows)
                    {
                        listReturn.Add(uploadPath + item["ResourceToResourceFolderImg_Url"].ToString());
                    }

                    #endregion

                    #endregion
                }

                return listReturn;
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("获取资源对应所有文件路径失败。{0}", ex.Message.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据文件路径，删除文件（资源对应文件） 17-06-07TS
        /// </summary>
        private void DeleteResourceFile(List<string> list)
        {
            try
            {
                foreach (string fileUrl in list)
                {
                    if (File.Exists(fileUrl))
                    {
                        File.Delete(fileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("删除文件失败。{0}", ex.Message.ToString()));
            }
        }

        //protected void btnWordToPdf_Click(object sender, EventArgs e)
        //{
        //    if (WordToPDF(Server.MapPath("/upload/2.docx"), Server.MapPath("/upload/2.pdf")))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "temp", "alert('转换成功');", true);
        //        return;
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "temp", "alert('转换失败');", true);
        //        return;
        //    }
        //}
        //public string PreviewWord(string physicalPath, string url)
        //{
        //    Microsoft.Office.Interop.Word._Application application = null;
        //    Microsoft.Office.Interop.Word._Document doc = null;
        //    application = new Microsoft.Office.Interop.Word.Application();
        //    object missing = Type.Missing;
        //    object trueObject = true;
        //    application.Visible = false;
        //    application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
        //    doc = application.Documents.Open(physicalPath, missing, trueObject, missing, missing, missing,
        //      missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
        //    //Save Excel to Html
        //    object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
        //    string htmlName = Path.GetFileNameWithoutExtension(physicalPath) + ".html";
        //    String outputFile = Path.GetDirectoryName(physicalPath) + "\\" + htmlName;
        //    doc.SaveAs(outputFile, format, missing, missing, missing,
        //             missing, missing, missing,
        //             missing, missing, missing, missing);
        //    doc.Close();
        //    application.Quit();
        //    return Path.GetDirectoryName(Server.UrlDecode(url)) + "\\" + htmlName;
        //}
        //public bool WordToPDF(string sourcePath, string targetPath)
        //{
        //    bool result = false;
        //    Microsoft.Office.Interop.Word._Application application = new Microsoft.Office.Interop.Word.Application();
        //    Microsoft.Office.Interop.Word._Document document = null;
        //    try
        //    {
        //        application.Visible = false;
        //        document = application.Documents.Open(sourcePath);
        //        document.ExportAsFixedFormat(targetPath, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
        //        result = true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        result = false;
        //    }
        //    finally
        //    {
        //        document.Close();
        //    }
        //    return result;
        //}

        private void CheckSchoolUser(string portal, string password)
        {
            String LDAP_Server = "ca.zucc.edu.cn";
            int LDAP_Port = 389;
            String LDAP_Search_DN = "ou=people,dc=zucc,dc=edu,dc=cn";
            LdapDirectoryIdentifier ldapDir = new LdapDirectoryIdentifier(LDAP_Server, LDAP_Port);
            LdapConnection ldapConn = new LdapConnection(ldapDir);
            ldapConn.AuthType = AuthType.Basic;
            //portal是用户名，11111111是密码
            System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential("uid=" + portal + "," + LDAP_Search_DN, password);
            try
            {
                ldapConn.Bind(myCredentials);
            }
            catch (LdapException e1)
            {
                Console.WriteLine("Exception in auth with LDAP Server:" + e1.Message);
                Response.Write("Auth failed!");
                return;
            }
            finally
            {
                if (ldapConn != null)
                    ldapConn.Dispose();
            }
            Response.Write("Auth successful!");
        }

        private string GetRFNameByRTRFId(string rtrfId)
        {
            string temp = string.Empty;
            try
            {
                string strSql = string.Format(@"select t.ResourceFolder_Id,t.ResourceToResourceFolder_Id,t.Resource_Name,t.Resource_Class,t.ParticularYear 
,t1.D_Name as GradeTerm_Name,t2.D_Name as Resource_Version_Name,t3.D_Name as Subject_Name
from ResourceToResourceFolder t 
left join Common_Dict t1 on t1.Common_Dict_Id=t.GradeTerm
left join Common_Dict t2 on t2.Common_Dict_Id=t.Resource_Version
left join Common_Dict t3 on t3.Common_Dict_Id=t.Subject
where t.ResourceToResourceFolder_Id='{0}' ", rtrfId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        temp = string.Format("{0}/{1}/{2}/{3}/{4}"
                            , dt.Rows[0]["ParticularYear"].ToString()
                            , dt.Rows[0]["GradeTerm_Name"].ToString()
                            , dt.Rows[0]["Resource_Version_Name"].ToString()
                            , dt.Rows[0]["Subject_Name"].ToString()
                            , GetRFNameByRFId(dt.Rows[0]["ResourceFolder_Id"].ToString(), "")
                            );
                    }
                    else if (dt.Rows[0]["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        temp = GetRFNameByRFId(dt.Rows[0]["ResourceFolder_Id"].ToString(), "");
                    }
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }

        private string GetRFNameByRFId(string rfId, string result)
        {
            try
            {
                Model_ResourceFolder modelRF = new BLL_ResourceFolder().GetModel(rfId);
                if (modelRF != null && !string.IsNullOrEmpty(modelRF.ResourceFolder_Name))
                {
                    result = modelRF.ResourceFolder_Name + "/" + result;
                    result = result.TrimEnd('/');
                    if (modelRF.ResourceFolder_ParentId != "0" && !string.IsNullOrEmpty(modelRF.ResourceFolder_ParentId))
                    {
                        result = GetRFNameByRFId(modelRF.ResourceFolder_ParentId, result);
                    }
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

    }
}