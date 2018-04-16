using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.Payment.WxPayAPI
{
    /// <summary>
    /// GetWxPayState 的摘要说明
    /// </summary>
    public class GetWxPayState : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string rid = context.Request.QueryString["rid"].Filter();
                rid = Rc.Common.DBUtility.DESEncrypt.Decrypt(rid);
                Model_UserOrder model = new Model_UserOrder();
                BLL_UserOrder bll = new BLL_UserOrder();
                model = bll.GetModelByOrderNo(rid);
                if (model.UserOrder_Status == (int)UOrderStatus.完成 && !string.IsNullOrEmpty(model.trade_no) && !string.IsNullOrEmpty(model.trade_status))
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("fail");
                }
            }
            catch (Exception ex)
            {
                new Rc.Cloud.BLL.BLL_clsAuth().AddLogErrorFromBS("", string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message));
                context.Response.Write("error");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}