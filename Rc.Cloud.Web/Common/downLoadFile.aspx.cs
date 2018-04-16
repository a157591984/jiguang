using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.Common
{
    public partial class downLoadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string iid = Request["iid"].Filter();
            string tabid = Request["TabId"].Filter();
            string UserId = Request["UserId"].Filter();
            string productType = Request["ProductType"].Filter();            
            
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = @"select A.Book_ID,A.Resource_Url,A.File_Suffix,A.Resource_Name from ResourceToResourceFolder A 
 where ResourceToResourceFolder_Id='" + iid.Filter() + "' ";
                dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                if (dt.Rows.Count == 1)
                {
                    #region 记录教案访问情况
                    Model_visit_client modelVC = new Model_visit_client();
                    modelVC.visit_client_id = Guid.NewGuid().ToString();
                    modelVC.user_id = UserId;
                    modelVC.resource_data_id = iid;
                    modelVC.product_type = productType;
                    modelVC.tab_id = tabid;
                    modelVC.open_time = DateTime.Now;
                    modelVC.operate_type = "view";
                    new BLL_visit_client().Add(modelVC);
                    #endregion

                    string strResourceUrl = string.Empty;
                    /// BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[1]["Book_ID"].ToString());
                    strResourceUrl = Server.MapPath(string.Format("{0}/{1}", Rc.Common.ConfigHelper.GetConfigString("DocumentUrl"), dt.Rows[0]["Resource_Url"].ToString()));

                    string strFileSuffix = string.Empty;
                    strFileSuffix = dt.Rows[0]["File_Suffix"].ToString();
                    if (strFileSuffix == "testPaper")
                    {
                        strFileSuffix = "dsc";
                    }
                    //string strJson = "{isPrint:" + bkAttrModel.IsPrint + ",isSave:" + bkAttrModel.IsSave + "}";
                    pfunction.ToDownloadBase64(strResourceUrl, dt.Rows[0]["Resource_Name"].ToString().ReplaceForFilter() + "." + dt.Rows[0]["File_Suffix"].ToString());
                }
            }
        }
        /// <summary>
        /// 获取资源属性（是否可打印，存盘）
        /// </summary>
        /// <param name="ResourceFolder_Id"></param>
        /// <returns></returns>
        public BookAttrModel GetBookAttrValue(string ResourceFolder_Id)
        {
            BookAttrModel model = new BookAttrModel();
            model.IsPrint = false;
            model.IsSave = false;
            try
            {
                List<Model_BookAttrbute> listModel = new BLL_BookAttrbute().GetModelListByCache(ResourceFolder_Id);
                foreach (var item in listModel)
                {
                    if (item.AttrEnum == BookAttrEnum.Print.ToString() && item.AttrValue == "1")
                    {
                        model.IsPrint = true;
                    }
                    else if (item.AttrEnum == BookAttrEnum.Save.ToString() && item.AttrValue == "1")
                    {
                        model.IsSave = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            return model;
        }
    }
    /// <summary>
    /// 资源属性 类
    /// </summary>
    public class BookAttrModel
    {
        /// <summary>
        /// 是否允许打印
        /// </summary>
        public bool IsPrint { get; set; }
        /// <summary>
        /// 是否允许存盘
        /// </summary>
        public bool IsSave { get; set; }
    }
}