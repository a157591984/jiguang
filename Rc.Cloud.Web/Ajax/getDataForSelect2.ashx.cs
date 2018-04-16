using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rc.Common.StrUtility;
using System.Data;
using Newtonsoft.Json;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.Ajax
{
    /// <summary>
    /// getDataForSelect2 的摘要说明
    /// </summary>
    public class getDataForSelect2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;
            context.Response.ContentType = "text/plain";
            List<object> listReturn = new List<object>();
            string key = context.Request["key"].Filter();
            int pageSize = Convert.ToInt16(context.Request["pageSize"].Filter());
            int pageIndex = Convert.ToInt16(context.Request["pageIndex"].Filter());
            if (pageIndex == 0) pageIndex = 1;

            try
            {
                string strWhere = string.Empty;
                string GradeTerm = context.Request["GradeTerm"].Filter();
                string Subject = context.Request["Subject"].Filter();
                string name = context.Request["name"].Filter();
                strWhere += string.Format(" GradeTerm='{0}' and Subject='{1}' ", GradeTerm, Subject);

                switch (key)
                {
                    case "kp":
                        #region 基本知识点
                        if (!string.IsNullOrEmpty(name))
                        {
                            strWhere += string.Format(" and KPNameBasic like '%{0}%' ", name);
                        }
                        BLL_S_KnowledgePointBasic bll = new BLL_S_KnowledgePointBasic();
                        DataTable dt = bll.GetListByPage(strWhere, " KPNameBasic ", ((pageIndex - 1) * pageSize + 1), (pageIndex * pageSize)).Tables[0];

                        int recordCount = bll.GetRecordCount(strWhere);

                        foreach (DataRow item in dt.Rows)
                        {
                            listReturn.Add(new
                            {
                                id = item["S_KnowledgePointBasic_Id"].ToString(),
                                text = item["KPNameBasic"].ToString(),
                            });
                        }
                        result = JsonConvert.SerializeObject(new
                        {
                            err = "null",
                            pageIndex = pageIndex,
                            items = listReturn,
                            total_count = recordCount
                        });
                        #endregion
                        break;
                    case "tp":
                        #region 基本考点
                        if (!string.IsNullOrEmpty(name))
                        {
                            strWhere += string.Format(" and TPNameBasic like '%{0}%' ", name);
                        }
                        BLL_S_TestingPointBasic bllTP = new BLL_S_TestingPointBasic();
                        DataTable dtTP = bllTP.GetListByPage(strWhere, " TPNameBasic ", ((pageIndex - 1) * pageSize + 1), (pageIndex * pageSize)).Tables[0];

                        recordCount = bllTP.GetRecordCount(strWhere);

                        foreach (DataRow item in dtTP.Rows)
                        {
                            listReturn.Add(new
                            {
                                id = item["S_TestingPointBasic_Id"].ToString(),
                                text = item["TPNameBasic"].ToString(),
                            });
                        }
                        result = JsonConvert.SerializeObject(new
                        {
                            err = "null",
                            pageIndex = pageIndex,
                            items = listReturn,
                            total_count = recordCount
                        });
                        #endregion
                        break;
                }
            }
            catch (Exception)
            {
                listReturn.Add(new
                {
                    id = "",
                    text = "加载失败",
                });
                result = JsonConvert.SerializeObject(new
                {
                    err = "异常",
                    pageIndex = pageIndex,
                    items = listReturn
                });
            }

            context.Response.Write(result);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}