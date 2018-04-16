using System;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using Rc.Cloud.Model;
using Rc.Cloud.BLL;


namespace Rc.Cloud.Web.Common
{
    public class InitPage_Rc : System.Web.UI.Page
    {
        private Model_F_User _loginUser;
        public int PageSize = 10;
        public int PageIndex = 1;
        public int rCount = 0;
        public int pCount = 0;
        public string Module_Id;
        public string siteMap;
        public string siteMapIds;
        public Model_Struct_Func UserFun;
        public string strPageName
        {
            get;
            set;
        }
        /// <summary>
        /// 当前页面带参数的URL
        /// </summary>
        public string strPageNameAndParm
        {
            get;
            set;
        }
        //public int PageSize { get { return _PageSize; } set { _PageSize = value; } }
        //public int PageIndex { get { return _PageIndex; } set { _PageIndex = value; } }
        public string strUrlPara
        { get; set; }
        public Model_F_User loginUser
        {
            get { return _loginUser; }
            set { _loginUser = value; }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //Session.Timeout = 20;
            ////自动登录 只可在测试时开发此功能，此功能开放时严禁把此文件签入SVN
#if DEBUG
            //UserLoginAuto.UserAutoLogin();
#endif
            strPageName = GetPageName();
            strPageNameAndParm = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            loginUser = Rc.Common.StrUtility.clsUtility.IsPageFlag(this.Page) as Model_F_User;

        }
        /// <summary>
        /// 页面名称
        /// </summary>

        public string GetPageName()
        {
            string url = HttpContext.Current.Request.Path.ToString();
            int tag = url.LastIndexOf("/") + 1;
            int mm = url.IndexOf(".aspx") - url.LastIndexOf("/") - 1;
            string urlName = url.Substring(tag, mm);
            return urlName + ".aspx";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!string.IsNullOrEmpty(Module_Id))
            {
                SetSiteMap();
                SetSiteMapIds();
                if (!IsPostBack)
                {
                    SetMasterHiddenField();
                }
            }
        }
        /// <summary>
        /// 给模块页面赋初始值
        /// </summary>
        private void SetMasterHiddenField()
        {
            if (Master != null)
            {
                ((HiddenField)Master.FindControl("hid_Module_Id")).Value = Module_Id;
                ((HiddenField)Master.FindControl("hid_Module_Ids")).Value = siteMapIds;

                ((HiddenField)Master.FindControl("hid_LoginUserId")).Value = loginUser.UserId.ToString();
                //Master.
            }
        }
        /// <summary>
        /// 导航
        /// </summary>
        public virtual void SetSiteMap()
        {
            siteMap = new BLL_SysModule().GetModule_PathBySetMapBySysCode(Module_Id, "1");
        }
        /// <summary>
        /// 导航
        /// </summary>
        public virtual void SetSiteMapIds()
        {
            siteMapIds = new BLL_SysModule().GetSetMapByCacheBySysCode(Module_Id, "2");
        }

        protected override void OnError(EventArgs e)
        {
            LogError(e);
        }

        internal void LogError(EventArgs e)
        {
            //Exception ex = Server.GetLastError();
            //base.OnError(e);

            ////记录错误日志
            ////new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法{1},错误信息：{2}", ex.TargetSite.DeclaringType.ToString(), ex.TargetSite.Name.ToString(), ex.Message));

            //string strErrorMessage = string.Empty;
            //strErrorMessage = PHHC.Share.StrUtility.clsUtility.Encrypt(ex.Message);
            ////开发时使用：
            //PHHC.Share.StrUtility.clsUtility.ErrorDispose(this.Page, 5, strErrorMessage, false);
        }
    }
}