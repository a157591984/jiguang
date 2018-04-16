using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Web.Services;

namespace Rc.Cloud.Web.Sys
{
    public partial class ConfigSchool_BookAttr : Rc.Cloud.Web.Common.InitPage
    {
        protected string SchoolId = string.Empty;
        protected string SysUserId = string.Empty;
        BLL_BookAttr_School bll = new BLL_BookAttr_School();
        Model_BookAttr_School model = new Model_BookAttr_School();
        protected void Page_Load(object sender, EventArgs e)
        {
            SchoolId = Request.QueryString["SchoolId"].ToString().Filter();
            SysUserId = loginUser.SysUser_ID.Filter();
            if (!IsPostBack)
            {
                List<Model_BookAttr_School> listModel = new BLL_BookAttr_School().GetModelList("SchoolId='" + SchoolId + "'");
                foreach (var item in listModel)
                {
                    if (item.AttrEnum == BookAttrEnum.Print.ToString())
                    {
                        ddlPrint.SelectedValue = item.AttrValue;
                    }
                    else if (item.AttrEnum == BookAttrEnum.Save.ToString())
                    {
                        ddlSave.SelectedValue = item.AttrValue;
                    }
                    else if (item.AttrEnum == BookAttrEnum.Copy.ToString())
                    {
                        ddlCopy.SelectedValue = item.AttrValue;
                    }
                }
            }
        }

        [WebMethod]
        public static string OperateResourceAuth(string SysUserId, string SchoolId, string AttrEnum, string AttrValue)
        {
            string temp = "0";
            try
            {
                BLL_BookAttr_School bll = new BLL_BookAttr_School();
                Model_BookAttr_School model = new Model_BookAttr_School();
                List<Model_BookAttr_School> list = bll.GetModelList("SchoolId='" + SchoolId + "' and AttrEnum='" + AttrEnum + "' ");
                bool result = false;
                if (list.Count != 0)
                {
                    model = list[0];
                    model.AttrValue = AttrValue;
                    model.CreateTime = DateTime.Now;
                    result = bll.Update(model);
                }
                else
                {
                    model.BookAttr_School_Id = Guid.NewGuid().ToString();
                    model.SchoolId = SchoolId;
                    model.AttrEnum = AttrEnum;
                    model.AttrValue = AttrValue;
                    model.CreateUser = SysUserId;
                    model.CreateTime = DateTime.Now;
                    result = bll.Add(model);
                }
                if (result)
                {
                    temp = "1";
                }
            }
            catch (Exception)
            {

            }
            return temp;
        }

    }
}