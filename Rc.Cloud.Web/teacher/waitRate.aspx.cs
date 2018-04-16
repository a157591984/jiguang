using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.Model.Resources;
namespace Rc.Cloud.Web.teacher
{
    public partial class waitRate : Rc.Cloud.Web.Common.FInitData
    {
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = FloginUser.UserId;
        }
        [WebMethod]
        public static string GetOrderList(string userid, int PageIndex, int PageSize)
        {
            try
            {
                userid = userid.Filter();
                string strWhere = " UserOrder_Status=" + Convert.ToInt32(UOrderStatus.完成) + "";
                if (!string.IsNullOrEmpty(userid))
                {
                    strWhere += " and  UserId='" + userid + "'";
                }
                DataTable dt = new DataTable();
                BLL_UserOrder bll = new BLL_UserOrder();
                dt = bll.GetListByPageOrderList(strWhere, "UserOrder_FinishTime desc,UserOrder_Status", ((PageIndex - 1) * PageSize + 1), PageIndex * PageSize).Tables[0];
                int intRecordCount = bll.GetRecordCount(strWhere);
                List<object> listReturn = new List<object>();
                int inum = 1;
                string temp = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {
                        Book_Id = dt.Rows[i]["Book_Id"],
                        UserOrder_No = dt.Rows[i]["UserOrder_No"],
                        UserOrder_Id = dt.Rows[i]["UserOrder_Id"],
                        BookName = dt.Rows[i]["Book_Name"],
                        Book_Price = dt.Rows[i]["Book_Price"],
                        UserOrder_Amount = dt.Rows[i]["UserOrder_Amount"],
                        Status = dt.Rows[i]["UserOrder_Status"].ToString(),
                        UserOrder_Status = Rc.Common.EnumService.GetDescription<Rc.Model.Resources.UOrderStatus>(dt.Rows[i]["UserOrder_Status"].ToString()),
                        UserOrder_FinishTime = pfunction.ConvertToLongerDateTime(dt.Rows[i]["UserOrder_FinishTime"].ToString()),
                        CommentCount = dt.Rows[i]["CommentCount"]
                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = intRecordCount,
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

        [WebMethod]
        public static string CancelOrder(string Order_id)
        {
            try
            {
                Rc.Model.Resources.Model_UserOrder order = new Rc.Model.Resources.Model_UserOrder();
                BLL_UserOrder bll = new BLL_UserOrder();
                order = bll.GetModel(Order_id.Filter());
                if (order != null)
                {
                    order.UserOrder_Status = 4;
                    order.UserOrder_FinishTime = DateTime.Now;
                }
                if (bll.Update(order))
                {
                    return "1";
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