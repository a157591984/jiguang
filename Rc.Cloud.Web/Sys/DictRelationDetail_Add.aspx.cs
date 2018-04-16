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
    public partial class DictRelationDetail_Add : Rc.Cloud.Web.Common.InitPage
    {
        public string DictRelation_Id = string.Empty;
        public string HeadDict_Id = string.Empty;
        public string SonDict_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            DictRelation_Id = Request["DictRelation_Id"].Filter();
            HeadDict_Id = Request["HeadDict_Id"].Filter();
            SonDict_Id = Request["SonDict_Id"].Filter();
            if (!IsPostBack)
            {
                string Temp = "<a href=\"javascript:;\" ajax-value=\"{0}\" class=\"active\">{1}</a>";
                string sql = "select * from Common_Dict where Common_Dict_Id='" + HeadDict_Id + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ltlFilst.Text = string.Format(Temp, dt.Rows[0]["D_Value"].ToString(), dt.Rows[0]["D_Name"].ToString());
                }
            }
        }
        /// <summary>
        /// 二级
        /// </summary>
        /// <param name="FirstCode"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetSecondData(string FirstCode)
        {
            try
            {
                string Temp = "<label><input type=\"radio\" value=\"{0}\" name=\"SecondCode\" {2} />{1}</label>&nbsp;&nbsp;&nbsp;";
                string Str = string.Empty;
                string sql = "select * from Common_Dict where D_Type='" + FirstCode.Filter() + "' order by D_Order";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            Str += string.Format(Temp, dt.Rows[i]["Common_Dict_ID"].ToString(), dt.Rows[i]["D_name"].ToString(), "checked");
                        }
                        else
                        {
                            Str += string.Format(Temp, dt.Rows[i]["Common_Dict_ID"].ToString(), dt.Rows[i]["D_name"].ToString(), "");
                        }
                    }
                    return Str;
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
        /// <summary>
        /// 三级
        /// </summary>
        /// <param name="FirstCode"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetLastData(string SonDict_Id)
        {
            try
            {
                DataTable dt = new DataTable();
                string Temp = "<label><input type=\"checkbox\" value=\"{0}\" />{1}</label>&nbsp;&nbsp;&nbsp;";
                string Str = string.Empty;
                string title = string.Empty;
                string Sql = @"select t.D_Name as Title,t1.Common_Dict_ID,t1.D_Name from Common_Dict t 
inner join Common_Dict t1 on t.D_Value=t1.D_Type
where t.Common_Dict_ID='" + SonDict_Id.Filter() + "' order by t1.D_Order";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(Sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    title = dt.Rows[0]["Title"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Str += string.Format(Temp, dt.Rows[i]["Common_Dict_ID"].ToString(), dt.Rows[i]["D_name"].ToString());
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
                    if (new Rc.BLL.Resources.BLL_DictRelation_Detail().Add(DictRelation_Id, SecondCode, list))
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