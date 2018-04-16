using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.student
{
    public partial class message : Rc.Cloud.Web.Common.FInitData
    {
        protected string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = FloginUser.UserId;
        }

        [WebMethod]
        public static string GetMessageList(string MsgAccepter, int PageIndex, int PageSize)
        {
            try
            {
                BLL_Msg bll = new BLL_Msg();
                MsgAccepter = MsgAccepter.Filter();

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(MsgAccepter))
                {
                    strWhere += " and MsgAccepter='" + MsgAccepter + "' ";
                }

                dtRes = bll.GetListForSearch(strWhere, "T.MsgStatus desc,T.CreateTime desc", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount(strWhere);
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + (PageIndex - 1) * PageSize + 1),
                        MsgId = dtRes.Rows[i]["MsgId"].ToString(),
                        MsgAccepter = dtRes.Rows[i]["MsgAccepter"].ToString(),
                        MsgSender = dtRes.Rows[i]["MsgSender"].ToString(),
                        MsgSenderName = string.IsNullOrEmpty(dtRes.Rows[i]["TrueName"].ToString()) ? dtRes.Rows[i]["UserName"].ToString() : dtRes.Rows[i]["TrueName"].ToString(),
                        MsgTitle = dtRes.Rows[i]["MsgTitle"].ToString(),
                        MsgStatus = dtRes.Rows[i]["MsgStatus"].ToString(),
                        MsgStatusText = Rc.Common.EnumService.GetDescription<MsgStatus>(dtRes.Rows[i]["MsgStatus"].ToString()),
                        CreateTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString())
                    });
                }

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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        /// <summary>
        /// 修改 阅读状态
        /// </summary>
        [WebMethod]
        public static string ChangeIsRead(string MsgId, int IsRead)
        {
            try
            {
                BLL_Msg bll = new BLL_Msg();
                List<Model_Msg> list = new List<Model_Msg>();
                string[] strArr = MsgId.Split(',');
                for (int i = 0; i < strArr.Length; i++)
                {
                    Model_Msg model = bll.GetModel(strArr[i]);
                    model.MsgStatus = MsgStatus.Read.ToString();
                    list.Add(model);
                }

                if (bll.BatchMarkReadStatus(list))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

    }
}