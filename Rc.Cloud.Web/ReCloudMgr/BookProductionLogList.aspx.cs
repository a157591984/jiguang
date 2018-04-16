using Rc.Cloud.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Cloud.Web.Common;
using Newtonsoft.Json;
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class BookProductionLogList : Rc.Cloud.Web.Common.InitPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Module_Id = "20300200";

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                string strWhere = string.Empty;
                //年份
                Rc.Cloud.Web.Common.pfunction.SetDdlYear(ddlYear, DateTime.Now.Year - 5, DateTime.Now.Year + 2, true, "年份");

                //文档类型
                strWhere = string.Format(" D_Type='1' AND Common_Dict_ID!='{0}' order by d_order", Rc.Common.Config.Resource_TypeConst.按属性生成的目录);
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlResource_Type, dt, "D_Name", "Common_Dict_ID", "文档类型");

            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string ParticularYear, string Resource_Type, string Time, string ReName, string CreateUsesr, int PageIndex, int PageSize)
        {
            try
            {
                ParticularYear = ParticularYear.Filter();
                Resource_Type = Resource_Type.Filter();
                Time = Time.Filter();
                ReName = ReName.Filter();
                CreateUsesr = CreateUsesr.Filter();

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strSql = string.Empty;
                string strSqlCount = string.Empty;

                string strWhere = string.Empty;
                if (ParticularYear != "-1") strWhere += " and ParticularYear = '" + ParticularYear + "' ";
                if (Resource_Type != "-1") strWhere += " and Resource_Type = '" + Resource_Type + "' ";
                if (!string.IsNullOrEmpty(Time)) strWhere += " and CreateTime='" + Time + "' ";
                if (!string.IsNullOrEmpty(ReName)) strWhere += " and ResourceFolder_Name like '%" + ReName + "%' ";
                if (!string.IsNullOrEmpty(CreateUsesr)) strWhere += " and CreateUserName like '%" + CreateUsesr + "%' ";

                string strBasic = @"select BPL.BookId,BPL.ParticularYear,BPL.Resource_Type,BPL.CreateUser,convert(nvarchar(10),BPL.CreateTime,23) CreateTime
,Count(1) as ReCount
,RF.ResourceFolder_Name,dic.D_Name as Resource_TypeName,SU.SysUser_LoginName as CreateUserName
,STime=(select min(CreateTime) from BookProductionLog BPL2 where BPL2.BookId=BPL.BookId and convert(nvarchar(10),BPL2.CreateTime,23)=convert(nvarchar(10),BPL.CreateTime,23))
,ETime=(select max(CreateTime) from BookProductionLog BPL2 where BPL2.BookId=BPL.BookId and convert(nvarchar(10),BPL2.CreateTime,23)=convert(nvarchar(10),BPL.CreateTime,23))
from BookProductionLog BPL
inner join ResourceFolder RF on RF.ResourceFolder_Level=5 and RF.Book_ID=BPL.BookId
left join Common_Dict dic on dic.Common_Dict_Id=RF.Resource_Type
left join SysUser SU on SU.SysUser_ID=BPL.CreateUser
group by BPL.BookId,BPL.ParticularYear,BPL.Resource_Type,BPL.CreateUser,convert(nvarchar(10),BPL.CreateTime,23),RF.ResourceFolder_Name,dic.D_Name,SU.SysUser_LoginName";

                strSqlCount = string.Format("select count(*) from ( {0} ) TP where 1=1 {1} ", strBasic, strWhere);

                strSql = string.Format(@"select * from (select ROW_NUMBER() over(ORDER BY {0}) row,* from ( {1} ) TP where 1=1 {2} ) t where row between {3} and {4} "
                    , "ParticularYear,Resource_Type,CreateTime desc"
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
                        ParticularYear = dtRes.Rows[i]["ParticularYear"].ToString(),
                        Resource_TypeName = dtRes.Rows[i]["Resource_TypeName"].ToString(),
                        ResourceName = dtRes.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        CreateTime = dtRes.Rows[i]["CreateTime"].ToString(),
                        STime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["STime"].ToString(), "HH:mm:ss"),
                        ETime = pfunction.ConvertToLongDateTime(dtRes.Rows[i]["ETime"].ToString(), "HH:mm:ss"),
                        ReCount = dtRes.Rows[i]["ReCount"].ToString(),
                        CreateUser = dtRes.Rows[i]["CreateUserName"].ToString()
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