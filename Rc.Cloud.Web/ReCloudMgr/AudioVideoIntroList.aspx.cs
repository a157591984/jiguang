using Rc.Cloud.Model;
using Rc.Cloud.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
using Rc.Cloud.BLL;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Common;
using System.IO;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class AudioVideoIntroList : Rc.Cloud.Web.Common.InitPage
    {
        public string AudioVideoBookId = string.Empty;
        public string BookName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            AudioVideoBookId = Request.QueryString["AudioVideoBookId"];
            BookName = Server.UrlDecode(Request.QueryString["BookName"]);
            Module_Id = "10400100";
            if (!IsPostBack)
            {
                ddlAudioVideoTypeEnum.Items.Add(new ListItem("--视频类型--", "-1"));
                foreach (var item in Enum.GetValues(typeof(Rc.Model.Resources.AudioVideoTypeEnum)))
                {
                    ddlAudioVideoTypeEnum.Items.Add(new ListItem(EnumService.GetDescription<Rc.Model.Resources.AudioVideoTypeEnum>(item.ToString()), item.ToString()));
                }
            }
        }
        /// <summary>
        /// 获取书本目录
        /// </summary>
        /// <param name="DocName"></param>
        /// <param name="strResource_Type"></param>
        /// <param name="strResource_Class"></param>
        /// <param name="strGradeTerm"></param>
        /// <param name="strSubject"></param>
        /// <param name="strResource_Version"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetAudioVideoIntroList(string AudioVideoBookId, string AudioVideoTypeEnum, string FileName, string AudioVideoName, int PageIndex, int PageSize)
        {
            try
            {
                AudioVideoTypeEnum = AudioVideoTypeEnum.Filter();
                FileName = FileName.Filter();
                AudioVideoName = AudioVideoName.Filter();
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                Model_VSysUserRole loginUser = (Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                #region 资源信息
                DataTable dtBook = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string StrWhere = " where AudioVideoBookId='" + AudioVideoBookId + "' ";
                if (!string.IsNullOrEmpty(FileName))
                {
                    StrWhere += " and FileName like '%" + FileName.TrimEnd() + "'%";
                }
                if (!string.IsNullOrEmpty(AudioVideoName))
                {
                    StrWhere += " and AudioVideoName like '%" + AudioVideoName.TrimEnd() + "'%";
                }
                if (!string.IsNullOrEmpty(AudioVideoTypeEnum) && AudioVideoTypeEnum != "-1")
                {
                    StrWhere += " and AudioVideoTypeEnum='" + AudioVideoTypeEnum + "'";
                }

                strSqlCount = @"select count(*) from AudioVideoIntro" + StrWhere + " ";
                strSql = @"select * from (select ROW_NUMBER() over(ORDER BY A.CreateTime DESC) row,A.* from dbo.AudioVideoIntro A"
                    + StrWhere + " ) t where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";
                dtBook = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtBook.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        AudioVideoIntroId = dtBook.Rows[i]["AudioVideoIntroId"].ToString(),
                        CreateTime = pfunction.ConvertToLongDateTime(dtBook.Rows[i]["CreateTime"].ToString()),
                        FileName = dtBook.Rows[i]["FileName"].ToString(),
                        AudioVideoTypeEnum = dtBook.Rows[i]["AudioVideoTypeEnum"].ToString(),
                        AudioVideoTypeEnumName = EnumService.GetDescription<AudioVideoTypeEnum>(dtBook.Rows[i]["AudioVideoTypeEnum"].ToString()),
                        AudioVideoName = dtBook.Rows[i]["AudioVideoName"].ToString(),
                        AudioVideoUrl = File.Exists(HttpContext.Current.Server.MapPath(dtBook.Rows[i]["AudioVideoUrl"].ToString())) ? dtBook.Rows[i]["AudioVideoUrl"].ToString() : "False",
                        AudioVideoBookId = dtBook.Rows[i]["AudioVideoBookId"].ToString()
                    });
                }
                #endregion

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
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
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        [WebMethod]
        public static string DeleteData(string AudioVideoIntroId)
        {
            try
            {
                if (!string.IsNullOrEmpty(AudioVideoIntroId.Filter()))
                {
                    BLL_AudioVideoIntro bll = new BLL_AudioVideoIntro();
                    if (bll.Delete(AudioVideoIntroId))
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}