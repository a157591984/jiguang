using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.Model.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class Res_SellDetailList : Rc.Cloud.Web.Common.InitPage
    {
        protected string SYear = string.Empty;
        protected string SDocType = string.Empty;
        protected string SDocTypeName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "20400100";
            SYear = Request.QueryString["SYear"].Filter();
            SDocType = Request.QueryString["SDocType"].Filter();
            SDocTypeName = Request.QueryString["SDocTypeName"].Filter();
            if (!IsPostBack)
            {

            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string ParticularYear, string Resource_Type, string BuyType, string Time, string ReName, int PageIndex, int PageSize)
        {
            try
            {
                ParticularYear = ParticularYear.Filter();
                Resource_Type = Resource_Type.Filter();
                BuyType = BuyType.Filter();
                Time = Time.Filter();
                ReName = ReName.Filter();

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (ParticularYear != "-1") strWhere += " and ParticularYear = '" + ParticularYear + "' ";
                if (Resource_Type != "-1") strWhere += " and Resource_Type = '" + Resource_Type + "' ";
                if (!string.IsNullOrEmpty(BuyType))
                {
                    if (BuyType == "ALIPAY")
                    {
                        strWhere += " and (BuyType = '" + UserOrder_PaytoolEnum.ALIPAY.ToString() + "' or BuyType = '" + UserOrder_PaytoolEnum.WXPAY.ToString() + "') ";
                    }
                    else
                    {
                        strWhere += " and BuyType = '" + BuyType + "' ";
                    }
                }
                if (!string.IsNullOrEmpty(Time)) strWhere += " and CreateTime='" + Time + "' ";
                if (!string.IsNullOrEmpty(ReName)) strWhere += " and ResourceFolder_Name like '%" + ReName + "%' ";

                string strBasic = @"select UBR.BuyType,UBR.CreateTime
,RF.ResourceFolder_Name,RF.ParticularYear,RF.Resource_Type,FU.UserName
from UserBuyResources UBR
left join ResourceFolder RF on RF.Book_Id=UBR.Book_Id and RF.ResourceFolder_Level=5
left join F_User FU ON FU.UserId=UBR.UserId ";

                strSqlCount = string.Format("select count(*) from ( {0} ) TP where 1=1 {1} ", strBasic, strWhere);

                strSql = string.Format(@"select * from (select ROW_NUMBER() over(ORDER BY {0}) row,* from ( {1} ) TP where 1=1 {2} ) t where row between {3} and {4} "
                    , "CreateTime desc"
                    , strBasic
                    , strWhere
                    , ((PageIndex - 1) * PageSize + 1)
                    , (PageIndex * PageSize));

                dtRes = Rc.Common.DBUtility.DbHelperSQL.Query(strSql).Tables[0];
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL.GetSingle(strSqlCount).ToString());
                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        ResourceName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        BuyTime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["CreateTime"].ToString(), "yyyy-MM-dd HH:mm"),
                        BuyUser = dtRes.Rows[i]["UserName"].ToString(),
                        BuyType = (dtRes.Rows[i]["BuyType"].ToString() == "NBSQ") ? "内部授权" : "网上购买"
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }

    }
}