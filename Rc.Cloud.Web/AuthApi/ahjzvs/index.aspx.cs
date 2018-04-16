using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Text;

namespace Rc.Cloud.Web.AuthApi.ahjzvs
{
    public partial class index : System.Web.UI.Page
    {
        string iurl = string.Empty;
        string schoolCode = Rc.Model.Resources.ThirdPartyEnum.ahjzvs.ToString();

        string requestUrl = "http://ahjzsso.ahjzvs.com/";
        string redirecturl = "http://www.bncz365.com/authapi/ahjzvs/";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String sid = Request["sid"].Filter();

                //判断用户是否登录了身份认证
                if (sid == null || sid.Equals(""))
                {
                    //没有登录，则跳转到单点登录系统                    
                    Response.Redirect(requestUrl + "login.aspx?redirecturl=" + redirecturl, false);
                }
                else
                {
                    //通过接口，获取当前登录用户信息
                    string appid = "06a77ad7-dd19-421a-ac13-18cec232cdb9";
                    string appsecret = "sA8zEHNYcM8ehMANXhSxqg==";
                    string para = string.Format("appid={0}&appsecret={1}&type=ssouser&token={2}", appid, appsecret, sid);
                    string result = Rc.Common.RemotWeb.PostDataToServer(requestUrl + "sync.ashx?" + para, null, Encoding.UTF8, "GET");
                    ResultModel model = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultModel>(result);
                    //没有登录，则跳转到单点登录系统      
                    if (!model.Result)
                    {
                        Response.Write(model.Msg);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(model.Data.loginName))
                        {
                            Response.Write("用户登录名为空，登录失败");
                            return;
                        }

                        iurl = HandelThirdPartyUserInfo.HandelUserInfo(schoolCode, model.Data.loginName, model.Data.username);

                        Response.Redirect(iurl, false);
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("登录异常err");
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", schoolCode + "用户登录-接口", ex.Message.ToString());
            }

        }

        public class ResultModel
        {
            /// <summary>
            /// 请求结果
            /// </summary>
            public bool Result { get; set; }
            /// <summary>
            /// 消息
            /// </summary>
            public string Msg { get; set; }
            /// <summary>
            /// 登录的用户
            /// </summary>
            public UserInfo Data { get; set; }
        }
        public class UserInfo
        {
            /// <summary>
            /// 用户唯一标识
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 登录人员唯一身份id，如教职工id或学生id
            /// </summary>
            public string tid { get; set; }
            /// <summary>
            /// 用户姓名
            /// </summary>
            public string username { get; set; }
            /// <summary>
            /// 用户登录名
            /// </summary>
            public string loginName { get; set; }

        }

    }
}