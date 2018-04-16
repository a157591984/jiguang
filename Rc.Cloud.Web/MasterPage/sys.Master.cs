using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rc.Cloud.BLL;
using Rc.Cloud.Model;
using System.Configuration;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.MasterPage
{

    public partial class sys : System.Web.UI.MasterPage
    {
        //protected LoginUser loginUser = null;

        public Model_VSysUserRole loginUser;
        protected StringBuilder strTree = new StringBuilder();
        protected string strSidebarData = string.Empty;

        bool isFirst = true;
        protected string strLeftTitle = string.Empty;
        //得到内容页的模块编码
        public string Module_Id
        {
            get { return this.hid_Module_Id.Value; }
            set { this.hid_Module_Id.Value = value; }
        }
        //得到内容页的模块编码所有上级包括本级
        public string Module_Ids
        {
            get { return this.hid_Module_Ids.Value; }
            set { this.hid_Module_Ids.Value = value; }
        }
        //得到内容页登录用户
        public int LoginUserId
        {
            get { return int.Parse(this.hid_LoginUserId.Value); }
            set { this.hid_LoginUserId.Value = value.ToString(); }
        }
        protected string strSysName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            strSysName = ConfigurationManager.AppSettings["SysName"].ToString();
            loginUser = Rc.Common.StrUtility.clsUtility.IsPageFlag(this.Page) as Model_VSysUserRole;
            //FullTree(GetTreeData());
            initTreeData(GetTreeData());
            GetFristMenu();
        }
        protected string GetFristMenu()
        {
            //临时字符串
            string strTemp = string.Empty;
            //css样式
            string strCss = string.Empty;


            StringBuilder strMenu = new StringBuilder();
            //从缓存中读取菜单
            DataTable dt = new BLL_SysModule().GetOwenModuleListByCacheBySysCode(loginUser.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','));
            if (dt != null)
            {
                //查询出一级菜单
                var drs = dt.Select(" PARENTID='0' ");
                //最上级

                string urldefault = string.Empty;
                for (int i = 0; i < drs.Length; i++)
                {
                    if (Module_Ids.IndexOf(drs[i]["ModuleId"].ToString()) >= 0) { strCss = "active"; } else { strCss = ""; }
                    strMenu.Append("<li class=\"" + strCss + "\">");

                    DataTable dtTemp = new DataTable();
                    dtTemp = new BLL_clsAuth().GetOwenTreeByCatchBySysCode(loginUser.SysUser_ID, Rc.Common.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), drs[i]["moduleid"].ToString().Substring(0, 2));
                    DataRow[] drTemp = dtTemp.Select("isLast='1' and url<>'#'", "DefaultOrder desc");
                    if (drTemp.Length > 0)
                    {
                        urldefault = drTemp[0]["url"].ToString();
                        //Response.Redirect("/" + drs[0]["url"].ToString());
                    }
                    else
                    {
                        //PHHC.Share.StrUtility.clsUtility.ErrorDispose(this.Page, 6, false);
                    }
                    strMenu.Append("<a href='" + Rc.Common.StrUtility.clsUtility.getHostPath() + "/" + urldefault + "'\">" + drs[i]["modulename"] + "</a></li> ");
                    //strMenu.Append("<div class=\"div_menu_split\" id=\"div_menu_split_00" + (i + 1) + "\"></div>   ");
                }
            }

            return strMenu.ToString();
        }
        private DataTable GetTreeData()
        {
            DataTable dt = new DataTable();

            var newStr = Rc.Common.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ',');
            string moduleID = null;
            if (Module_Id.Length >= 2)
                moduleID = Module_Id.Substring(0, 2);

            dt = new BLL_clsAuth().GetOwenTreeByCatchBySysCode(loginUser.SysUser_ID, newStr, moduleID);
            //dt = MS.Authority.clsAuth.GetOwenTreeByCatch(loginUser.DoctorInfo_ID, PHHC.Share.StrUtility.clsUtility.ReDoStr(loginUser.SysRole_IDs, ','), "4");
            return dt;
        }

        private void initTreeData(DataTable dt)
        {
            try
            {
                List<object> listReturn = new List<object>();
                foreach (DataRow item in dt.Rows)
                {
                    if (item["PARENTID"].ToString() != "0")
                    {
                        listReturn.Add(new
                        {
                            id = item["MODULEID"].ToString(),
                            pid = (item["SLEVEL"].ToString().LastIndexOf(".") == 2) ? "0" : item["PARENTID"].ToString(),
                            name = item["MODULENAME"].ToString(),
                            url = (!string.IsNullOrEmpty(item["URL"].ToString().Replace("#", ""))) ? Rc.Cloud.Web.Common.pfunction.getHostPath() + "/" + item["URL"].ToString() : ""
                        });
                    }
                }
                strSidebarData = JsonConvert.SerializeObject(new
                {
                    data = listReturn
                });
            }
            catch (Exception ex)
            {
                strSidebarData = JsonConvert.SerializeObject(new
                {
                    data = ex.Message.ToString()
                });
            }
        }




        private void FullTree(DataTable dt)
        {
            InitNavigationTree("-1", dt);
            strTree.Append("</div>");
        }
        private void InitNavigationTree(string strParentModuleID, DataTable dt)
        {
            DataView dvw = new DataView();
            TreeNode nodTemp = new TreeNode();
            dvw.Table = dt;
            if (strParentModuleID == "-1")
            {
                dvw.RowFilter = " PARENTID ='0'";
            }
            else
            {
                dvw.RowFilter = " PARENTID = '" + strParentModuleID + "' ";
            }
            foreach (DataRowView drv in dvw)
            {
                string strModuleID = string.Empty;
                if (drv["PARENTID"].ToString() == "0")
                {
                    strLeftTitle = drv["MODULENAME"].ToString();
                }
                else
                {

                    if (drv["Depth"].ToString() == "1")
                    {
                        if (!isFirst)
                        {
                            strTree.Append("</div>\n\r");
                        }
                        strTree.Append("<span id=\"span_" + drv["MODULEID"].ToString() + "\">" + drv["MODULENAME"].ToString() + "</span>");
                        strTree.Append("<div>\n\r");

                        isFirst = false;
                    }
                    else if (drv["Depth"].ToString() == "2")
                    {
                        if (drv["URL"].ToString() != "#" & drv["URL"].ToString() != "")
                        {
                            strTree.Append("<a href='" + Rc.Cloud.Web.Common.pfunction.getHostPath() + "/" + drv["URL"].ToString() + "'>" + drv["MODULENAME"].ToString() + "</a>\n\r");
                        }
                        else
                        {
                            strTree.Append("<a href='javascript:void(0);'>" + drv["MODULENAME"].ToString() + "</a>\n\r");
                        }

                    }

                }

                InitNavigationTree(drv["MODULEID"].ToString(), dt);
            }
        }
        //初始化菜单树
        private void InitNavigationTree(TreeNodeCollection tncCurrent, string cSupMenuSysCode, DataTable dt)
        {
            DataView dvw = new DataView();
            TreeNode nodTemp = new TreeNode();
            dvw.Table = dt;
            if (cSupMenuSysCode == "-1")
            {
                dvw.RowFilter = " PARENTID ='0'";
            }
            else
            {
                dvw.RowFilter = " PARENTID = '" + cSupMenuSysCode + "' ";
            }
            if (dvw != null)
            {
                foreach (DataRowView drv in dvw)
                {
                    nodTemp = new TreeNode();
                    nodTemp.Value = drv["MODULEID"].ToString();

                    string strNodeText = string.Empty;

                    strNodeText = drv["MODULENAME"].ToString();

                    nodTemp.Text = strNodeText;
                    nodTemp.ToolTip = drv["MODULENAME"].ToString();
                    if (drv["URL"].ToString() != "#" & drv["URL"].ToString() != "")
                    {
                        nodTemp.NavigateUrl = Rc.Common.StrUtility.clsUtility.getHostPath() + "/" + drv["URL"].ToString();
                    }
                    else
                    {
                        nodTemp.NavigateUrl = "javascript:void(0);";
                    }
                    tncCurrent.Add(nodTemp);
                    //strTempID = drv["ORG_ARCHI_ID"].ToString();
                    InitNavigationTree(nodTemp.ChildNodes, strNodeText, dt);
                }
            }

        }


    }
}