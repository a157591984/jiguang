using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Com.Alipay
{
    public partial class AlipayRefund : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                //U_Orders model = new U_Orders();
                //U_OrdersBLL bll = new U_OrdersBLL();
                string rid = Request.QueryString["rid"].Filter();
                rid = Rc.Common.DBUtility.DESEncrypt.Decrypt(rid);
                string domain = Rc.Cloud.Web.Common.pfunction.getHostPath();

                ////////////////////////////////////////////请求参数////////////////////////////////////////////

                //服务器异步通知页面路径
                string notify_url = string.Format("http://{0}/Payment/notify_AlipayRefund.aspx", domain);
                //需http://格式的完整路径，不允许加?id=123这类自定义参数

                //卖家支付宝帐户
                string seller_email = Config.Seller_email;
                //必填

                //退款当天日期
                string refund_date = now.ToString("yyyy-MM-dd HH:mm:ss");
                //必填，格式：年[4位]-月[2位]-日[2位] 小时[2位 24小时制]:分[2位]:秒[2位]，如：2007-10-01 13:13:13

                string maxBatchno = "";// bll.GetMaxOrdersBatchNoByDate(now.ToString("yyyy-MM-dd"));
                if (maxBatchno == "0")
                {
                    maxBatchno = "00000001";
                }
                else
                {
                    maxBatchno = (Convert.ToInt32(maxBatchno.Substring(8)) + 1).ToString();
                    switch (maxBatchno.Length)
                    {
                        case 1:
                            maxBatchno = "0000000" + maxBatchno;
                            break;
                        case 2:
                            maxBatchno = "000000" + maxBatchno;
                            break;
                        case 3:
                            maxBatchno = "00000" + maxBatchno;
                            break;
                        case 4:
                            maxBatchno = "0000" + maxBatchno;
                            break;
                        case 5:
                            maxBatchno = "000" + maxBatchno;
                            break;
                        case 6:
                            maxBatchno = "00" + maxBatchno;
                            break;
                        case 7:
                            maxBatchno = "0" + maxBatchno;
                            break;
                        default:
                            break;
                    }
                }
                //model = bll.GetModelByOrderNo(rid);
                //if (model != null && model.user_id != 0 && !string.IsNullOrEmpty(model.trade_no))
                //{

                //    //批次号
                //    string batch_no = now.ToString("yyyyMMdd") + maxBatchno;
                //    //必填，格式：当天日期[8位]+序列号[3至24位]，如：201008010000001

                //    //退款笔数
                //    string batch_num = "1";
                //    //必填，参数detail_data的值中，“#”字符出现的数量加1，最大支持1000笔（即“#”字符出现的数量999个）

                //    //退款详细数据
                //    string detail_data = string.Format("{0}^{1}^{2}", model.trade_no, model.PayAmount, XHZ.Web.Common.pfunction.GetSubString_(model.ACReason.Replace("^", ""), 150));
                //    //必填，具体格式请参见接口技术文档


                //    ////////////////////////////////////////////////////////////////////////////////////////////////

                //    //把请求参数打包成数组
                //    SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
                //    sParaTemp.Add("partner", Config.Partner);
                //    sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
                //    sParaTemp.Add("service", "refund_fastpay_by_platform_pwd");
                //    sParaTemp.Add("notify_url", notify_url);
                //    sParaTemp.Add("seller_email", seller_email);
                //    sParaTemp.Add("refund_date", refund_date);
                //    sParaTemp.Add("batch_no", batch_no);
                //    sParaTemp.Add("batch_num", batch_num);
                //    sParaTemp.Add("detail_data", detail_data);

                //    #region 更新订单退款批次号
                //    model.batch_no = batch_no;
                //    model.refund_date = refund_date;
                //    model.detail_data = detail_data;
                //    model.OrderStatus = (int)UOrderStatus.等待退款;
                //    #endregion
                //    if (bll.Update(model))
                //    {
                //        //建立请求
                //        string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
                //        Response.Write(sHtmlText);
                //    }
                //    else
                //    {
                //        Response.Write("<script language=\"javascript\">alert('参数错误,请返回重新操作');history.back();</script>");
                //    }
                //}
                //else
                //{
                //    Response.Write("<script language=\"javascript\">alert('参数错误,请返回重新操作');history.back();</script>");
                //}
            }
            catch (Exception)
            {
                Response.Write("<script language=\"javascript\">alert('参数错误,请返回重新操作');history.back();</script>");
                //Response.Write(ex.Message.ToString());
            }
        }
    }
}