using Rc.Cloud.Web.Common;
using Rc.Common.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Newtonsoft.Json;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SyncFileSchoolList_Tobe : Rc.Cloud.Web.Common.InitPage
    {
        protected string SysUser_ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SysUser_ID = loginUser.SysUser_ID;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string Name, int PageSize, int PageIndex)
        {
            try
            {
                Name = Name.Filter();

                DataTable dtRes = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(Name))
                {
                    strWhere += " and vw.SchoolName like '%" + Name + "%' ";
                }

                string strSql = string.Empty;
                strSql = string.Format(@"select * from (select ROW_NUMBER() over(order by SchoolName) as row,count(1) as icount
,SchoolId,SchoolName,D_PublicValue
 from (
select rf2.Resource_Type,dic.D_Name as Resource_TypeName,rf2.Book_Id as bookId,rf2.ResourceFolder_Name as bookName,min(t.CreateTime) as CreateTime
,vw.SchoolName,vw.SchoolId
,cs.D_PublicValue
from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
inner join UserBuyResources ub on ub.Book_Id=rtrf.Book_Id
inner join VW_UserOnClassGradeSchool vw on vw.SchoolId is not null and vw.UserId=ub.UserId
inner join Common_Dict dic on dic.Common_Dict_Id=rf2.Resource_Type
inner join ConfigSchool cs on cs.School_ID=vw.SchoolId
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus not in(0,2)  
and rtrf.Resource_Class='{0}' {1}
and ba.AuditState='1' 
and not exists(select 1 from SyncFileToSchool where SchoolId=vw.SchoolId and ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id and CreateTime>t.CreateTime)
group by rf2.Resource_Type,dic.D_Name,rf2.Book_Id,rf2.ResourceFolder_Name,vw.SchoolName,vw.SchoolId,cs.D_PublicValue
) t group by SchoolId,SchoolName,D_PublicValue ) temp where row between {2} and {3} "
                    , Resource_ClassConst.云资源
                    , strWhere
                    , ((PageIndex - 1) * PageSize + 1)
                    , (PageIndex * PageSize));

                dtRes = Rc.Common.DBUtility.DbHelperSQL_Operate.Query(strSql, 600).Tables[0];

                string strSqlCount = string.Empty;
                strSqlCount = string.Format(@"select count(1) from (select count(1) as icount
,SchoolId,SchoolName,D_PublicValue
 from (
select rf2.Resource_Type,dic.D_Name as Resource_TypeName,rf2.Book_Id as bookId,rf2.ResourceFolder_Name as bookName,min(t.CreateTime) as CreateTime
,vw.SchoolName,vw.SchoolId
,cs.D_PublicValue
from SyncData t
inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
inner join UserBuyResources ub on ub.Book_Id=rtrf.Book_Id
inner join VW_UserOnClassGradeSchool vw on vw.SchoolId is not null and vw.UserId=ub.UserId
inner join Common_Dict dic on dic.Common_Dict_Id=rf2.Resource_Type
inner join ConfigSchool cs on cs.School_ID=vw.SchoolId
where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus not in(0,2)  
and rtrf.Resource_Class='{0}' {1}
and ba.AuditState='1' 
and not exists(select 1 from SyncFileToSchool where SchoolId=vw.SchoolId and ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id and CreateTime>t.CreateTime)
group by rf2.Resource_Type,dic.D_Name,rf2.Book_Id,rf2.ResourceFolder_Name,vw.SchoolName,vw.SchoolId,cs.D_PublicValue
) t group by SchoolId,SchoolName,D_PublicValue) temp "
                        , Resource_ClassConst.云资源
                        , strWhere);
                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL_Operate.GetSingle(strSqlCount, 600).ToString());

                int inum = 0;
                for (int i = 0; i < dtRes.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        inum = (i + 1),
                        SchoolId = dtRes.Rows[i]["SchoolId"].ToString(),
                        SchoolName = dtRes.Rows[i]["SchoolName"].ToString(),
                        D_PublicValue = dtRes.Rows[i]["D_PublicValue"].ToString(),
                        icount = dtRes.Rows[i]["icount"].ToString()
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
                    err = "error" //ex.Message.ToString()
                });
            }
        }
    }
}