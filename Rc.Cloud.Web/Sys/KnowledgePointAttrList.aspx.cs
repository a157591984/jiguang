using Newtonsoft.Json;
using Rc.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.Model.Resources;
using Rc.BLL.Resources;
namespace Rc.Cloud.Web.Sys
{
    public partial class KnowledgePointAttrList1 : System.Web.UI.Page
    {
        public string S_KnowledgePoint_Id = string.Empty;
        public string S_KnowledgePointBasic_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            S_KnowledgePoint_Id = Request["S_KnowledgePoint_Id"].Filter();
            Model_S_KnowledgePoint model = new BLL_S_KnowledgePoint().GetModel(S_KnowledgePoint_Id);
            if (model != null)
            {
                S_KnowledgePointBasic_Id = model.S_KnowledgePointBasic_Id;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string S_KnowledgePointBasic_Id)
        {
            try
            {
                S_KnowledgePointBasic_Id = S_KnowledgePointBasic_Id.Filter();
                BLL_S_KnowledgePointAttrExtend bll = new BLL_S_KnowledgePointAttrExtend();
                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                dt = bll.GetList("S_KnowledgePointBasic_Id='" + S_KnowledgePointBasic_Id + "'").Tables[0];
                int Rcount = bll.GetRecordCount("S_KnowledgePointBasic_Id='" + S_KnowledgePointBasic_Id + "'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listReturn.Add(new
                    {

                        S_KnowledgePointAttrExtend_Id = dt.Rows[i]["S_KnowledgePointAttrExtend_Id"].ToString(),
                        S_KnowledgePointBasic_Id = dt.Rows[i]["S_KnowledgePointBasic_Id"].ToString(),
                        S_KnowledgePointAttrName = dt.Rows[i]["S_KnowledgePointAttrName"].ToString(),
                        S_KnowledgePointAttrValue = dt.Rows[i]["S_KnowledgePointAttrValue"].ToString(),
                        CreateTime = dt.Rows[i]["CreateTime"].ToString()
                    });
                }

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        Rcount = Rcount,
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }

        [WebMethod]
        public static string DeleteData(string S_KnowledgePointAttrExtend_Id)
        {
            try
            {
                BLL_S_KnowledgePointAttrExtend bll = new BLL_S_KnowledgePointAttrExtend();
                if (bll.Delete(S_KnowledgePointAttrExtend_Id))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}