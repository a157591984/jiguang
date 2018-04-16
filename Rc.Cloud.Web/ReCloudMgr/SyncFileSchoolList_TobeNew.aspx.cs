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
using Rc.BLL.Resources;

namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SyncFileSchoolList_TobeNew : Rc.Cloud.Web.Common.InitPage
    {
        protected string SysUser_ID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            SysUser_ID = loginUser.SysUser_ID;

            string strSql = string.Empty;
            strSql = "select top 1 FileSyncExecRecord_TimeStart from FileSyncExecRecord where FileSyncExecRecord_Status = '0' and FileSyncExecRecord_Type='更新待同步图书数量' order by FileSyncExecRecord_TimeStart desc";
            string str = Rc.Common.DBUtility.DbHelperSQL_Operate.GetSingle(strSql).ToString();
            if (str != "")
            {
                btnConfirm.Value = "正在更新待同步图书数量，开始时间为" + str;
                btnConfirm.Attributes.Add("disabled", "disabled");
            }

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        [WebMethod]
        public static string GetDataList(string Name, string IsNeed, int PageSize, int PageIndex)
        {
            try
            {
                BLL_SyncFileToSchoolData bll = new BLL_SyncFileToSchoolData();
                Name = Name.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strWhere = " 1=1 ";
                if (!string.IsNullOrEmpty(Name))
                {
                    strWhere += " and SchoolName like '%" + Name + "%' ";
                }
                if (IsNeed == "1")
                {
                    strWhere += " and BookTobeCount>0 ";
                }
                dt = bll.GetListByPage_Operate(strWhere, "BookTobeCount desc", ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = bll.GetRecordCount_Operate(strWhere);

                //                string strSql = string.Empty;
                //                strSql = string.Format(@"select * from (select ROW_NUMBER() over(order by SchoolName) as row,count(1) as icount
                //,SchoolId,SchoolName,D_PublicValue
                // from (
                //select rf2.Resource_Type,dic.D_Name as Resource_TypeName,rf2.Book_Id as bookId,rf2.ResourceFolder_Name as bookName,min(t.CreateTime) as CreateTime
                //,vw.SchoolName,vw.SchoolId
                //,cs.D_PublicValue
                //from SyncData t
                //inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
                //inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
                //left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
                //inner join UserBuyResources ub on ub.Book_Id=rtrf.Book_Id
                //inner join VW_UserOnClassGradeSchool vw on vw.SchoolId is not null and vw.UserId=ub.UserId
                //inner join Common_Dict dic on dic.Common_Dict_Id=rf2.Resource_Type
                //inner join ConfigSchool cs on cs.School_ID=vw.SchoolId
                //where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus not in(0,2)  
                //and rtrf.Resource_Class='{0}' {1}
                //and ba.AuditState='1' 
                //and not exists(select 1 from SyncFileToSchool where SchoolId=vw.SchoolId and ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id and CreateTime>t.CreateTime)
                //group by rf2.Resource_Type,dic.D_Name,rf2.Book_Id,rf2.ResourceFolder_Name,vw.SchoolName,vw.SchoolId,cs.D_PublicValue
                //) t group by SchoolId,SchoolName,D_PublicValue ) temp where row between {2} and {3} "
                //                    , Resource_ClassConst.云资源
                //                    , strWhere
                //                    , ((PageIndex - 1) * PageSize + 1)
                //                    , (PageIndex * PageSize));

                //                dtRes = Rc.Common.DBUtility.DbHelperSQL_Operate.Query(strSql, 600).Tables[0];

                //                string strSqlCount = string.Empty;
                //                strSqlCount = string.Format(@"select count(1) from (select count(1) as icount
                //,SchoolId,SchoolName,D_PublicValue
                // from (
                //select rf2.Resource_Type,dic.D_Name as Resource_TypeName,rf2.Book_Id as bookId,rf2.ResourceFolder_Name as bookName,min(t.CreateTime) as CreateTime
                //,vw.SchoolName,vw.SchoolId
                //,cs.D_PublicValue
                //from SyncData t
                //inner join ResourceToResourceFolder rtrf on rtrf.ResourceToResourceFolder_Id=t.DataId 
                //inner join ResourceFolder rf2 on rf2.Book_Id=rtrf.Book_Id and rf2.ResourceFolder_Level=5
                //left join BookAudit ba on ba.ResourceFolder_Id=rtrf.Book_Id
                //inner join UserBuyResources ub on ub.Book_Id=rtrf.Book_Id
                //inner join VW_UserOnClassGradeSchool vw on vw.SchoolId is not null and vw.UserId=ub.UserId
                //inner join Common_Dict dic on dic.Common_Dict_Id=rf2.Resource_Type
                //inner join ConfigSchool cs on cs.School_ID=vw.SchoolId
                //where t.TableName='ResourceToResourceFolder' and t.OperateType in('add','modify') and t.SyncStatus not in(0,2)  
                //and rtrf.Resource_Class='{0}' {1}
                //and ba.AuditState='1' 
                //and not exists(select 1 from SyncFileToSchool where SchoolId=vw.SchoolId and ResourceToResourceFolder_Id=rtrf.ResourceToResourceFolder_Id and CreateTime>t.CreateTime)
                //group by rf2.Resource_Type,dic.D_Name,rf2.Book_Id,rf2.ResourceFolder_Name,vw.SchoolName,vw.SchoolId,cs.D_PublicValue
                //) t group by SchoolId,SchoolName,D_PublicValue) temp "
                //                        , Resource_ClassConst.云资源
                //                        , strWhere);
                //                int rCount = Convert.ToInt32(Rc.Common.DBUtility.DbHelperSQL_Operate.GetSingle(strSqlCount, 600).ToString());

                foreach (DataRow item in dt.Rows)
                {
                    listReturn.Add(new
                    {
                        SchoolId = item["SchoolId"].ToString(),
                        SchoolName = item["SchoolName"].ToString(),
                        SchoolUrl = item["SchoolUrl"].ToString(),
                        BookAllCount = item["BookAllCount"].ToString(),
                        BookTobeCount = item["BookTobeCount"].ToString()
                    });
                }

                if (dt.Rows.Count > 0)
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

        protected void btnExec_Click(object sender, EventArgs e)
        {
            btnConfirm.Value = "正在更新待同步图书数量，开始时间为" + DateTime.Now.ToShortDateString();
            btnConfirm.Attributes.Add("disabled", "disabled");

            Thread thread1 = new Thread(new ThreadStart(ExecProcedureData));
            if (!thread1.IsAlive)
            {
                thread1.Start();
            }
        }

        private void ExecProcedureData()
        {
            string strFileSyncExecRecord_id = Guid.NewGuid().ToString();
            Rc.Model.Resources.Model_FileSyncExecRecord model_FileSyncExecRecord = new Rc.Model.Resources.Model_FileSyncExecRecord();
            Rc.BLL.Resources.BLL_FileSyncExecRecord bll_FileSyncExecRecord = new Rc.BLL.Resources.BLL_FileSyncExecRecord();
            model_FileSyncExecRecord.FileSyncExecRecord_id = strFileSyncExecRecord_id;
            model_FileSyncExecRecord.FileSyncExecRecord_Type = "更新待同步图书数量";
            model_FileSyncExecRecord.FileSyncExecRecord_TimeStart = DateTime.Now;
            model_FileSyncExecRecord.FileSyncExecRecord_Remark = "执行进行中...";
            model_FileSyncExecRecord.FileSyncExecRecord_Status = "0";
            model_FileSyncExecRecord.createUser = SysUser_ID;
            try
            {
                //记录同步之前的信息
                bll_FileSyncExecRecord.Add_Operate(model_FileSyncExecRecord);

                Rc.Common.DBUtility.DbHelperSQL_Operate.ExecuteSqlByTime("exec P_GenerateSyncFileToSchoolData '" + SysUser_ID + "' ", 3600);

                model_FileSyncExecRecord.FileSyncExecRecord_TimeEnd = DateTime.Now;
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "1";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "执行已完成";
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update_Operate(model_FileSyncExecRecord);

            }
            catch (Exception ex)
            {
                model_FileSyncExecRecord.FileSyncExecRecord_Status = "2";
                model_FileSyncExecRecord.FileSyncExecRecord_Remark = "执行失败：" + ex.Message.ToString();
                //记录同步完成后的信息
                bll_FileSyncExecRecord.Update_Operate(model_FileSyncExecRecord);
            }
        }
    }
}