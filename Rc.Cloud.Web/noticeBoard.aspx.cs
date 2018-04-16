using Rc.BLL;
using Rc.Cloud.Web.Common;
using Rc.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rc.Cloud.Web
{
    public partial class noticeBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    loadData();
                }
            }
            catch (Exception)
            {

            }
        }


        private void loadData()
        {
            BLL_NoticeBoard bll = new BLL_NoticeBoard();
            DataTable dt = new DataTable();
            dt = bll.GetList(1, "", " create_time desc").Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                ltlContent.Text = item["notice_content"].ToString();
                ltlCreateTime.Text = pfunction.ConvertToLongDateTime(item["create_time"].ToString(), "yyyy年MM月dd日");
            }
        }
    }
}