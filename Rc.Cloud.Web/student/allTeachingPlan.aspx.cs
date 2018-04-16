using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.Config;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;

namespace Rc.Cloud.Web.student
{
    public partial class allTeachingPlan : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isit_load();
            }
        }

        private void isit_load()
        {
            DataTable dt = new DataTable();
            //省
            dt = new Rc.BLL.Resources.BLL_Regional_Dict().GetList("D_PartentID=''  ORDER BY CONVERT(INT,D_Code)").Tables[0];
            pfunction.SetDdl(ddlProvince, dt, "D_Name", "Regional_Dict_Id", "省份");

            string strWhere = string.Empty;
            StringBuilder html = new StringBuilder();
            //年份
            int intYear = DateTime.Now.Year;
            html.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
            for (int i = intYear - 5; i <= intYear + 1; i++)
            {
                html.AppendFormat(" <li><a href='##' ajax-value='{0}'>{0}</a></li>", i);
            }
            ddlYear.Text = html.ToString();
            html.Clear();
            //教材版本
            strWhere = " D_Type='3' order by d_order";
            dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
            html.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append(" <li><a href='##' ajax-value='" + row["Common_Dict_ID"] + "'>" + row["D_Name"] + "</a></li>");
            }
            ddlVersion.Text = html.ToString();
            html.Clear();
            //年级学期
            strWhere = " D_Type='6' order by d_order";
            dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];

            html.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append(" <li><a href='##' ajax-value='" + row["Common_Dict_ID"] + "'>" + row["D_Name"] + "</a></li>");
            }
            ddlGradeTerm.Text = html.ToString();
            html.Clear();
            //学科
            strWhere = " D_Type='7' order by d_order";
            dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
            html.Append("<li><a href='##' class='active' ajax-value=''>全部</a></li>");
            foreach (DataRow row in dt.Rows)
            {
                html.Append(" <li><a href='##' ajax-value='" + row["Common_Dict_ID"] + "'>" + row["D_Name"] + "</a></li>");
            }
            ddlSubject.Text = html.ToString();
            html.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetResourceFolderListPage(string ParticularYear, string GradeTerm, string Subject, string Resource_Version, string Province, string City, string Name, string Resource_Type, int PageIndex, int PageSize)
        {
            try
            {
                Resource_Type = Resource_Type.Filter();
                StringBuilder strHtml = new StringBuilder();
                Rc.Model.Resources.Model_F_User loginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;
                string strWhere = string.Empty;
                if (ParticularYear != "") strWhere += " and ParticularYear = '" + ParticularYear.Filter() + "' ";//年份
                if (GradeTerm != "") strWhere += " and GradeTerm = '" + GradeTerm.Filter() + "' ";//年级学期
                if (Subject != "") strWhere += " and Subject = '" + Subject.Filter() + "' ";//学科
                if (Resource_Version != "") strWhere += " and Resource_Version = '" + Resource_Version.Filter() + "' "; //教材版本
                if (Name != "") strWhere += " and ResourceFolder_Name like '%" + Name.Filter() + "%' "; //名称
                string strWhereRT = string.Empty;
                if (Resource_Type == "0")//全部
                {
                    strWhereRT += " and t.Resource_Type in('" + Resource_TypeConst.class类型文件 + "','" + Resource_TypeConst.ScienceWord类型文件 + "','" + Resource_TypeConst.testPaper类型文件 + "','" + Resource_TypeConst.class类型微课件 + "') ";
                }

                if (Resource_Type == "1")//教案
                {
                    strWhereRT += " and t.Resource_Type in('" + Resource_TypeConst.class类型文件 + "','" + Resource_TypeConst.ScienceWord类型文件 + "') ";
                }
                if (Resource_Type == "2")//习题集
                {
                    strWhereRT += " and t.Resource_Type='" + Resource_TypeConst.testPaper类型文件 + "'";
                }
                if (Resource_Type == "3")//微课件
                {
                    strWhereRT += " and t.Resource_Type='" + Resource_TypeConst.class类型微课件 + "'";
                }
                string strWhereCount = strWhere;
                if (Province != "-1" && City != "-1")//选择省市
                {
                    strWhereCount = string.Format(" and t.ResourceFolder_Id in(select ResourceFolder_Id from BookArea where City_ID='{0}') ", City);
                    strWhere += string.Format(" and ResourceFolder_Id in(select ResourceFolder_Id from BookArea where City_ID='{0}') ", City);
                }
                else if (Province != "-1" && City == "-1")//只选择省
                {
                    strWhereCount = string.Format(" and t.ResourceFolder_Id in(select ResourceFolder_Id from BookArea where Province_ID='{0}') ", Province);
                    strWhere += string.Format(" and ResourceFolder_Id in(select ResourceFolder_Id from BookArea where Province_ID='{0}') ", Province);
                }

                strSqlCount = @"select count(1) from(SELECT DISTINCT t1.BookImg_Url ,t.*,t1.bookShelvesstate from ResourceFolder t 
                                left join Bookshelves t1 on t.ResourceFolder_ID=t1.ResourceFolder_ID 
                                left join UserBuyResources t2 on t.ResourceFolder_ID=t2.book_id
                                where  t1.BookShelvesState=1 " + strWhereRT + strWhereCount + " ) a ";

                strSql = @"select * from (select  ROW_NUMBER() over(ORDER BY CreateTime DESC) row,* from (
                            SELECT DISTINCT t1.BookImg_Url ,t.*,t1.bookShelvesstate,t1.BookPrice,t2.userid,t1.Book_Name from ResourceFolder t 
                            left join Bookshelves t1 on t.ResourceFolder_ID=t1.ResourceFolder_ID 
                            left join UserBuyResources t2 on t.ResourceFolder_ID=t2.book_id and t2.userid='" + loginUser.UserId + "' where  t1.BookShelvesState=1 " + strWhereRT + ") a  where 1=1 " + strWhere
                    + " ) z where row between " + ((PageIndex - 1) * PageSize + 1) + " and " + (PageIndex * PageSize) + "  ";

                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    #region 处理图片显示宽高
                    int imgHeight = 0;
                    int imgWidth = 0;
                    string imgFilePath = HttpContext.Current.Server.MapPath(dtRes.Rows[i]["BookImg_Url"].ToString());
                    if (System.IO.File.Exists(imgFilePath))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(imgFilePath);
                        imgHeight = img.Height;
                        imgWidth = img.Width;
                        if (imgHeight / 218.0 > imgWidth / 160.0)
                        {
                            imgWidth = 0;
                            if (imgHeight > 218) imgHeight = 218;

                        }
                        else
                        {
                            imgHeight = 0;
                            if (imgWidth > 160) imgWidth = 160;
                        }
                        img.Dispose();
                    }
                    #endregion
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        Book_Name = dtRes.Rows[i]["Book_Name"].ToString(),
                        BookImg_Url = dtRes.Rows[i]["BookImg_Url"].ToString(),
                        BookPrice = dtRes.Rows[i]["BookPrice"].ToString(),
                        ResourceFolder_ID = dtRes.Rows[i]["ResourceFolder_ID"].ToString(),
                        isGouMai = (dtRes.Rows[i]["userid"].ToString() != "" ? "1" : "0"),
                        imgHeight = imgHeight,
                        imgWidth = imgWidth
                    });
                }
                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = PageIndex,
                        PageSize = PageSize,
                        TotalCount = rCount,
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

        /// <summary>
        /// 获取市区
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetAreaCityInfo(string pid)
        {
            string temp = string.Empty;
            temp = "<option value=\"-1\">市区</option>";
            try
            {
                List<Model_Regional_Dict> list = new List<Model_Regional_Dict>();
                list = new BLL_Regional_Dict().GetModelList("D_PartentID='" + pid.Filter() + "' ORDER BY CONVERT(INT,D_Code)");
                foreach (var item in list)
                {
                    temp += string.Format("<option value=\"{0}\">{1}</option>", item.Regional_Dict_ID, item.D_Name);
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }
    }
}