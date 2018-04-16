using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
namespace Rc.Cloud.Web.Sys
{
    public partial class DictRelationDetail_Edit : Rc.Cloud.Web.Common.InitPage
    {
        public string Id = string.Empty;
        public string Name = string.Empty;
        public string Type = string.Empty;
        public string DictRelation_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Request.QueryString["Id"].Filter();
            Name = Server.UrlDecode(Request.QueryString["Name"].Filter());
            Type = Request.QueryString["Type"].Filter();
            DictRelation_Id = Request["DictRelation_Id"].Filter();
            if (!IsPostBack)
            {
                this.ltlName.Text = string.Format("<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"active\">{1}</a>", Id, Name);
            }
        }
        /// <summary>
        /// 三级
        /// </summary>
        /// <param name="FirstCode"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetLastData(string Id, string DictRelation_Id)
        {
            try
            {
                Rc.Model.Resources.Model_DictRelation dic = new Rc.Model.Resources.Model_DictRelation();
                dic = new Rc.BLL.Resources.BLL_DictRelation().GetModel(DictRelation_Id);

                DataTable dt = new DataTable();
                string Temp = "<label><input type=\"checkbox\" value=\"{0}\" {2} />{1}</label>&nbsp;&nbsp;&nbsp;";
                string Str = string.Empty;
                string title = string.Empty;
                string sqlTitle = "select * from Common_Dict where Common_Dict_Id='" + dic.SonDict_Id + "'";
                DataTable dtT = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTitle).Tables[0];
                title = dtT.Rows[0]["D_Name"].ToString();
                string sql = @"select dc.*,dr.Parent_Id from Common_Dict dc 
 left  join DictRelation_Detail dr on dr.Dict_Id=dc.Common_Dict_Id and Parent_Id='" + Id + @"'
  where dc.D_Type=(select D_Value from Common_Dict where Common_Dict_Id='" + dic.SonDict_Id + "') order by D_Order";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Str += string.Format(Temp, dt.Rows[i]["Common_Dict_ID"].ToString(), dt.Rows[i]["D_name"].ToString(), string.IsNullOrEmpty(dt.Rows[i]["Parent_Id"].ToString()) ? "" : "checked");
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        err = "成功",
                        title = title,
                        StrCode = Str

                    });
                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        err = "异常"
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }
        [WebMethod]
        public static string AddData(string FirstCode, string SecondCode, string LastCode, string DictRelation_Id)
        {
            try
            {
                Rc.Cloud.Model.Model_VSysUserRole loginUser = (Rc.Cloud.Model.Model_VSysUserRole)HttpContext.Current.Session["LoginUser"];
                string[] arr = LastCode.Split(',');
                List<Rc.Model.Resources.Model_DictRelation_Detail> list = new List<Rc.Model.Resources.Model_DictRelation_Detail>();
                for (int i = 0; i < arr.Length; i++)
                {
                    Rc.Model.Resources.Model_DictRelation_Detail dict = new Rc.Model.Resources.Model_DictRelation_Detail();
                    dict.DictRelation_Detail_Id = Guid.NewGuid().ToString();
                    dict.DictRelation_Id = DictRelation_Id;
                    dict.Dict_Id = arr[i].ToString();
                    dict.Dict_Type = FirstCode;
                    dict.Parent_Id = SecondCode;
                    dict.Remark = "";
                    dict.CreateTime = DateTime.Now;
                    dict.CreateUser = loginUser.SysUser_ID;
                    list.Add(dict);
                }
                if (list.Count > 0)
                {
                    if (new Rc.BLL.Resources.BLL_DictRelation_Detail().Add(DictRelation_Id,SecondCode, list))
                    {
                        return "1";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}