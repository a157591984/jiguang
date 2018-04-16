using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class Test : Rc.Cloud.Web.Common.InitPage
    {
        StringBuilder strHtml = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "10101000 ";
            if (!IsPostBack)
            {

                litTree.Text = GetTreeHtml();
            }
        }
        /// <summary>
        /// 填充树结构
        /// </summary>
        ///  
        private string GetTreeHtml()
        {

            DataTable dt = new Rc.BLL.Resources.BLL_ResourceFolder().GetAllList().Tables[0];

            strHtml = new StringBuilder();
            InitNavigationTree("0", 1, dt);
            return strHtml.ToString();
        }
        /// <summary>
        /// 递归加载树菜单
        /// </summary>
        /// <param name="TreeIDCurrent"></param>
        /// <param name="TreeLevelCurrent"></param>
        /// <param name="dtAll"></param>
        /// <returns></returns>
        protected string InitNavigationTree(string TreeIDCurrent, int TreeLevelCurrent, DataTable dtAll)
        {
            DataView dvw = new DataView();
            dvw.Table = dtAll;
            if (TreeIDCurrent == "0")
            {
                dvw.RowFilter = " ResourceFolder_ParentId ='0'";
            }
            else
            {
                dvw.RowFilter = " ResourceFolder_ParentId = '" + TreeIDCurrent + "' ";
            }
            if (dvw.Count > 0)
            {
               
                foreach (DataRowView drv in dvw)
                { 
                    
                    string bid = "0";
                    if (GetNextFloderNum(drv["ResourceFolder_Id"].ToString(), dtAll) > 0)
                    {
                        bid = "1";
                    }
                    if (GetNextFloderNum(drv["ResourceFolder_Id"].ToString(), dtAll) == 0)
                    {
                        bid = "-1";
                    }

                    if (drv["ResourceFolder_ParentId"].ToString() == "0")
                    {
                        strHtml.Append("<div class='clearDiv'></div>");
                        strHtml.Append("<div class='left_tree'>");
                        strHtml.Append("<dl>");
                        strHtml.AppendFormat("<dt><a href='##'>{0}<i class='fa fa-angle-right'></i></a>", drv["ResourceFolder_Name"].ToString().ReplaceForFilter());
                        strHtml.Append("</dt>");
                        strHtml.Append("</dl>");
                    }
                    else
                    {
                        strHtml.AppendFormat("<p bid=\"{0}\" gid=\"{1}\" id=\"tree_list_{2}\">", bid, TreeLevelCurrent, drv["ResourceFolder_ParentId"].ToString());
                        strHtml.Append(" <b class=\"arrow\"></b>");
                        strHtml.AppendFormat(" <span onclick=\"ShowSubDocument('{0}','{1}','{2}','{3}')\"><i class=\"icon_tree icon_tree_floder16\"></i>{2}</span>"
                            , 1.ToString(), drv["ResourceFolder_Id"].ToString(), drv["ResourceFolder_Name"].ToString().ReplaceForFilter(), drv["ResourceFolder_ParentId"].ToString());
                        strHtml.Append("</p>");
                       
                    }
                  
                    InitNavigationTree(drv["ResourceFolder_Id"].ToString(), GetTreeLevelNext(TreeLevelCurrent), dtAll);
                }
                strHtml.Append("</div>");
            }
            return strHtml.ToString();
        }
        /// <summary>
        /// 得以一个目录中目录的个数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtAll"></param>
        /// <returns></returns>
        protected int GetNextFloderNum(string id, DataTable dtAll)
        {
            DataView dvw = new DataView();
            dvw.Table = dtAll;
            if (id == "0")
            {
                dvw.RowFilter = " ResourceFolder_ParentId ='0'";
            }
            else
            {
                dvw.RowFilter = " ResourceFolder_ParentId = '" + id + "' ";
            }
            int treeNum = dvw.Count;
            return treeNum;
        }
        /// <summary>
        /// 下一个菜单的等级
        /// </summary>
        /// <param name="TreeLevelCurrent"></param>
        /// <returns></returns>
        protected int GetTreeLevelNext(int TreeLevelCurrent)
        {
            TreeLevelCurrent += 1;
            return TreeLevelCurrent;
        }

    }
}