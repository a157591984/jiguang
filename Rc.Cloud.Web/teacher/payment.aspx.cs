using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;

namespace Rc.Cloud.Web.teacher
{
    public partial class payment : Rc.Cloud.Web.Common.FInitData
    {
        protected string rid = string.Empty;
        protected string orderType = string.Empty;
        protected string orderNo = string.Empty;
        protected string enOrderNo = string.Empty;
        string strTips = "<script>payErrTips('{0}');</script>";
        protected void Page_Load(object sender, EventArgs e)
        {
            rid = Request.QueryString["rid"].Filter();
            orderType = Request.QueryString["orderType"].Filter();
            orderNo = Request.QueryString["orderNo"].Filter();
            if (string.IsNullOrEmpty(orderType)) orderType = "1";
            if (string.IsNullOrEmpty(rid))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "参数错误,订单提交失败,请重新提交订单"));
                return;
            }
            else
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            Model_Bookshelves modelBook = new BLL_Bookshelves().GetModel(rid);
            BLL_UserOrder bll = new BLL_UserOrder();
            Model_UserOrder model = new Model_UserOrder();

            try
            {

                //if (bll.GetRecordCount("UserId='" + userId + "' and Book_Id='" + rid + "' and UserOrder_Status='" + (int)UOrderStatus.完成 + "' ") > 0)
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "您已购买此教材,无需重复购买."));
                //    return;
                //}
                decimal amount = Convert.ToDecimal(modelBook.BookPrice);
                ltlAmount.Text = amount.ToString();
                if (amount <= 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "订单提交失败,请重新提交订单."));
                    return;
                }
                if (string.IsNullOrEmpty(orderNo))
                {
                    string userId = FloginUser.UserId;
                    DataTable dt = bll.GetList("UserId='" + userId + "' and Book_Id='" + rid + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow[] dr = dt.Select("UserOrder_Status='" + (int)UOrderStatus.完成 + "'");
                        if (dr.Length > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "您已购买此教材,无需重复购买."));
                            return;
                        }
                        DataRow[] dr1 = dt.Select("UserOrder_Status='" + (int)UOrderStatus.待付款 + "'");
                        if (dr1.Length > 0)
                        {
                            orderNo = dr1[0]["UserOrder_No"].ToString();
                            enOrderNo = Rc.Common.DBUtility.DESEncrypt.Encrypt(orderNo);
                            ltlUserOrder_No.Text = orderNo;
                        }
                        else
                        {
                            DataRow[] dr2 = dt.Select("UserOrder_Status='" + (int)UOrderStatus.已取消 + "'");
                            if (dr2.Length > 0)
                            {
                                orderNo = GenerateUMOrderNo();
                                //if (bll.GetRecordCount("UserOrder_No='" + orderNo + "'") > 0)
                                //{
                                //    ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "订单提交失败,请重新提交订单."));
                                //    return;
                                //}
                                enOrderNo = Rc.Common.DBUtility.DESEncrypt.Encrypt(orderNo);
                                ltlUserOrder_No.Text = orderNo;
                                model.UserOrder_Id = Guid.NewGuid().ToString();
                                model.UserId = userId;
                                model.UserOrder_No = orderNo;
                                model.UserOrder_Time = DateTime.Now;
                                model.UserOrder_Paytool = "";
                                model.UsreOrder_Buyeremail = "";
                                model.UserOrder_Type = orderType;
                                model.UserOrder_Amount = amount;
                                model.UserOrder_Status = (int)Rc.Model.Resources.UOrderStatus.待付款;
                                model.Book_Id = modelBook.ResourceFolder_Id;
                                model.Book_Name = modelBook.Book_Name;
                                model.Book_Price = Convert.ToDecimal(modelBook.BookPrice);
                                model.BookImg_Url = modelBook.BookImg_Url;
                                if (!bll.Add(model))
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "订单提交失败,请重新提交订单..."));
                                    return;
                                }
                                else
                                {
                                    #region 0元资源，自动购买，暂不启用
                                    //model.UserOrder_Status = (int)UOrderStatus.完成;
                                    //model.UserOrder_FinishTime = DateTime.Now;
                                    //#region 用户购买资源表
                                    //Model_UserBuyResources buyModel = new Model_UserBuyResources();
                                    //buyModel.UserBuyResources_ID = Guid.NewGuid().ToString();
                                    //buyModel.UserId = model.UserId;
                                    //buyModel.Book_id = model.Book_Id;
                                    //buyModel.BookPrice = model.Book_Price;
                                    //buyModel.BuyType = UserOrder_PaytoolEnum.FREE.ToString();
                                    //buyModel.CreateTime = DateTime.Now;
                                    //#endregion
                                    //bll.UpdateAndAddUserBuyResources(model, buyModel);
                                    #endregion
                                }
                            }
                        }

                    }
                    else
                    {
                        orderNo = GenerateUMOrderNo();
                        if (bll.GetRecordCount("UserOrder_No='" + orderNo + "'") > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "订单提交失败,请重新提交订单."));
                            return;
                        }
                        enOrderNo = Rc.Common.DBUtility.DESEncrypt.Encrypt(orderNo);
                        ltlUserOrder_No.Text = orderNo;
                        model.UserOrder_Id = Guid.NewGuid().ToString();
                        model.UserId = userId;
                        model.UserOrder_No = orderNo;
                        model.UserOrder_Time = DateTime.Now;
                        model.UserOrder_Paytool = "";
                        model.UsreOrder_Buyeremail = "";
                        model.UserOrder_Type = orderType;
                        model.UserOrder_Amount = amount;
                        model.UserOrder_Status = (int)Rc.Model.Resources.UOrderStatus.待付款;
                        model.Book_Id = modelBook.ResourceFolder_Id;
                        model.Book_Name = modelBook.Book_Name;
                        model.Book_Price = Convert.ToDecimal(modelBook.BookPrice);
                        model.BookImg_Url = modelBook.BookImg_Url;
                        if (!bll.Add(model))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "订单提交失败,请重新提交订单..."));
                            return;
                        }
                        else
                        {
                            #region 0元资源，自动购买，暂不启用
                            //model.UserOrder_Status = (int)UOrderStatus.完成;
                            //model.UserOrder_FinishTime = DateTime.Now;
                            //#region 用户购买资源表
                            //Model_UserBuyResources buyModel = new Model_UserBuyResources();
                            //buyModel.UserBuyResources_ID = Guid.NewGuid().ToString();
                            //buyModel.UserId = model.UserId;
                            //buyModel.Book_id = model.Book_Id;
                            //buyModel.BookPrice = model.Book_Price;
                            //buyModel.BuyType = UserOrder_PaytoolEnum.FREE.ToString();
                            //buyModel.CreateTime = DateTime.Now;
                            //#endregion
                            //bll.UpdateAndAddUserBuyResources(model, buyModel);
                            #endregion
                        }
                    }
                }
                else
                {
                    enOrderNo = Rc.Common.DBUtility.DESEncrypt.Encrypt(orderNo);
                    ltlUserOrder_No.Text = orderNo;
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "temp1", string.Format(strTips, "订单提交失败,请重新提交订单...."));
                return;
            }

        }

        public static string GenerateUMOrderNo()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            DateTime now = DateTime.Now;
            string temp = string.Empty;
            try
            {
                temp += now.ToString("yyMMddHHmmsss") + rand.Next(100000, 999999);

                return temp;
            }
            catch (Exception)
            {
                temp += now.ToString("yyMMddHHmmsss") + rand.Next(100000, 999999);
                return temp;
            }
        }

    }
}