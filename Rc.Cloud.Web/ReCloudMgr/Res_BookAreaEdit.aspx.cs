using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Common.StrUtility;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class Res_BookAreaEdit : Rc.Cloud.Web.Common.InitData
    {
        protected string selProvince = string.Empty;
        protected string selCity = string.Empty;
        string bid = string.Empty;
        string bname = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            bid = Request.QueryString["bid"].Filter();
            bname = Server.UrlDecode(Request.QueryString["bname"].Filter());
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            List<Model_BookArea> list = new BLL_BookArea().GetModelList("ResourceFolder_Id='" + bid + "'");
            string selectedAreaId = string.Empty;
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.Province_ID))
                {
                    selectedAreaId += item.Province_ID + ",";
                }
                if (!string.IsNullOrEmpty(item.City_ID))
                {
                    selectedAreaId += item.City_ID + ",";
                }

            }
            selectedAreaId = selectedAreaId.TrimEnd(',');
            hidAreaIds.Value = selectedAreaId;

            //            string strSql = @"select rd.*,pCheck=case when ba.BookArea_Id is null then '' else 'checked' end from dbo.Regional_Dict rd 
            //left join BookArea ba on ba.ResourceFolder_Id='" + bid + @"' and (ba.Province_Id=rd.Regional_Dict_ID or ba.City_Id=rd.Regional_Dict_ID or ba.County_Id=rd.Regional_Dict_ID)
            //where rd.D_PartentID=''  ORDER BY D_Code";
            rptProvince.DataSource = new BLL_Regional_Dict().GetList("D_PartentID=''  ORDER BY CONVERT(INT,D_Code)").Tables[0]; //Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
            rptProvince.DataBind();
        }

        [WebMethod]
        public static string GetAreaInfo(string pid, string selectedAreaId)
        {
            string temp = string.Empty;
            try
            {
                string[] strArr = selectedAreaId.Split(',');
                List<Model_Regional_Dict> list = new List<Model_Regional_Dict>();
                list = new BLL_Regional_Dict().GetModelList("D_PartentID='" + pid.Filter() + "' ORDER BY D_Code");
                foreach (var item in list)
                {
                    temp += string.Format("<div class='col-xs-3'><label><input type=\"checkbox\" value=\"{0}^{1}\" {3} />{2}</label></div>",
                        item.D_PartentID, item.Regional_Dict_ID, item.D_Name, strArr.Contains(item.Regional_Dict_ID) ? "checked" : "");
                }
            }
            catch (Exception ex)
            {

            }
            return temp;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<Model_BookArea> listM = new List<Model_BookArea>();
                string[] strArr = hidAreaIds.Value.Split(',');
                for (int i = 0; i < strArr.Length; i++)
                {
                    string[] strAreaId = strArr[i].Split('^');
                    Model_BookArea model = new Model_BookArea();
                    model.BookArea_ID = Guid.NewGuid().ToString();
                    model.ResourceFolder_Id = bid;
                    model.Book_Name = bname;
                    model.Province_ID = strAreaId[0];
                    if (strAreaId.Length > 1) model.City_ID = strAreaId[1];
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = loginUser.SysUser_ID;
                    listM.Add(model);
                }
                if (new BLL_BookArea().AddModelList(bid, listM) > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>$(function(){ Handel('1','操作成功')});</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>$(function(){ Handel('2','操作失败')});</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "<script type='text/javascript'>$(function(){ Handel('2','" + ex.Message + "')});</script>");
            }
        }

    }
}