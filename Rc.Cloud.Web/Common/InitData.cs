using System;
using System.Web.UI;
using System.Web;
using Rc.Cloud.Model;
using Rc.Cloud.BLL;


namespace Rc.Cloud.Web.Common
{
    /// <summary>
    /// 不使用模板页的普通页面
    /// </summary>
    public class InitData : System.Web.UI.Page
    {
        private Model_VSysUserRole _loginUser;
        public int PageSize = 10;
        public int PageIndex = 1;
        public int rCount = 0;
        public int pCount = 0;
        public string Module_Id;
        public string siteMap;
        public string siteMapIds;
        //public MS.Authority.clsAuth.stru_Func UserFun;
        /// <summary>
        /// 当前页面名称
        /// </summary>
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
        public Model_VSysUserRole loginUser
        {
            get
            {

                if (_loginUser == null && Session["LoginUser"] != null)
                    _loginUser = Session["LoginUser"] as Model_VSysUserRole;

                return _loginUser;
            }
            set { _loginUser = value; }

        }
        public string strPageUrl
        { get; set; }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session.Timeout = 3600;
            ////自动登录 只可在测试时开发此功能，此功能开放时严禁把此文件签入SVN
#if DEBUG
            //UserLoginAuto.UserAutoLogin();
#endif
            strPageName = GetPageName();
            strPageNameAndParm = strPageName + "?" + Rc.Cloud.Web.Common.pfunction.getPageParam();
            loginUser = Rc.Common.StrUtility.clsUtility.IsPageFlag(this.Page) as Model_VSysUserRole;

        }
        /// <summary>
        /// 操作成功 
        /// </summary>
        /// <param name="mgr"></param>
        public void OperationSuccess(string mgr)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'> $(function () {Handel('1','" + mgr + "');})</script>");
        }
        /// <summary>
        /// 操作失败 
        /// </summary>
        /// <param name="mgr"></param>
        public void OperationFailed(string mgr)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "handle", "<script type='text/javascript'>$(function () {Handel('2','" + mgr + "');})</script>");
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
                    //SetMasterHiddenField();
                }
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
            Exception ex = Server.GetLastError();
            base.OnError(e);

            //记录错误日志
            new BLL_clsAuth().AddLogErrorFromBS(Module_Id, string.Format("类：{0}，方法{1},错误信息：{2}"
                , ex.TargetSite.DeclaringType.ToString()
                , ex.TargetSite.Name.ToString(), ex.Message));

            //string strErrorMessage = string.Empty;
            //strErrorMessage = PHHC.Share.StrUtility.clsUtility.Encrypt(ex.Message);
            //开发时使用：
            //PHHC.Share.StrUtility.clsUtility.ErrorDispose(this.Page, 5, strErrorMessage, false);
            //PHHC.Share.StrUtility.clsUtility.ErrorDispose(this.Page, 5, ex.Message, false);
        }
    }
}