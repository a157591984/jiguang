using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Web.Services;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using System.Data;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.teacher
{
    public partial class assignCorrect : Rc.Cloud.Web.Common.FInitData
    {
        public string HomeWork_Id = string.Empty;
        public string StudentHomeWork_Id = string.Empty;
        public string Correct_Guid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            HomeWork_Id = Request["HomeWork_Id"].Filter();
            //StudentHomeWork_Id = Request["Shw_Id"].Filter();
            if (!IsPostBack)
            {
                #region 判断是否为修改
                string sql = string.Format(@"select top 1 * from Student_Mutual_Correct where HomeWork_Id='{0}'", HomeWork_Id);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Correct_Guid = dt.Rows[0]["Correct_Guid"].ToString();
                    string sqlCopy = string.Empty;
                    #region 把数据复制到被批改人临时表中
                    string sqlSub = string.Format(@"select top 1 * from Student_Mutual_CorrectSub where HomeWork_Id='{0}'", HomeWork_Id);
                    DataTable dtSub = Rc.Common.DBUtility.DbHelperSQL.Query(sqlSub).Tables[0];
                    if (dtSub.Rows.Count > 0)
                    {
                        sqlCopy += "delete from Student_Mutual_CorrectSub_Temp where Correct_Guid='" + Correct_Guid + "';";
                        sqlCopy += @"INSERT INTO [dbo].[Student_Mutual_CorrectSub_Temp]
           ([Student_Mutual_CorrectSub_Temp_Id]
           ,[Student_Mutual_Correct_Temp_Id]
           ,[Correct_Guid]
           ,[HomeWork_Id]
           ,[Student_HomeWork_Id]
           ,[Student_Id]
           ,[CreateTime]
           ,[Remark])
           select NewId(),Student_Mutual_Correct_Id,Correct_Guid,HomeWork_Id,t1.Student_HomeWork_Id,Student_Id,CreateTime,Remark from Student_Mutual_CorrectSub t1
		   inner join Student_HomeWork_Correct t2 on t2.Student_HomeWork_Id=t1.Student_HomeWork_Id and t2.Student_HomeWork_CorrectStatus<>'1'
		    where Correct_Guid='" + Correct_Guid + "'";
                    }
                    #endregion
                    #region 把数据复制到批改人临时表中
                    sqlCopy += "delete from Student_Mutual_Correct_Temp where Correct_Guid='" + Correct_Guid + "';";
                    sqlCopy += @"INSERT INTO [dbo].[Student_Mutual_Correct_Temp]
           ([Student_Mutual_Correct_Temp_Id]
           ,[Correct_Guid]
           ,[HomeWork_Id]
           ,[Student_Id]
           ,[CreateTime]
           ,[Remark])
            select NewId(),Correct_Guid,HomeWork_Id,Student_Id,CreateTime,Remark from Student_Mutual_Correct
			 where Correct_Guid='" + Correct_Guid + "' and Student_Id in (select Student_Mutual_Correct_Temp_Id from Student_Mutual_CorrectSub_Temp where Correct_Guid='" + Correct_Guid + "')";

                    #endregion

                    Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sqlCopy.ToString());
                }
                else
                {
                    string sqlTemp = string.Format(@"select top 1 * from Student_Mutual_Correct_Temp where HomeWork_Id='{0}'", HomeWork_Id);
                    DataTable dtTemp = Rc.Common.DBUtility.DbHelperSQL.Query(sqlTemp).Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        Correct_Guid = dtTemp.Rows[0]["Correct_Guid"].ToString();
                    }
                    else
                    {
                        Correct_Guid = Guid.NewGuid().ToString();
                    }
                }
                #endregion

            }
        }
        /// <summary>
        /// 随机分配
        /// </summary>
        /// <param name="Student_Mutual_Correct_Guid"></param>
        /// <param name="HomeWork_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RandomData(string Correct_Guid, string HomeWork_Id)
        {
            try
            {
                int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec P_Student_Mutual_Correct'" + HomeWork_Id + "','" + Correct_Guid.Filter() + "'", 7200);
                if (row > 0)
                {
                    return "1";
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
        /// 加载数据
        /// </summary>
        /// <param name="Student_Mutual_Correct_Guid"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetData(string Correct_Guid)
        {
            try
            {
                Correct_Guid = Correct_Guid.Filter();
                string Str = string.Empty;
                string temp = " <tr><td>{0}</td><td data-name='correctedPersonTd'>{1}</td><td>{2}</td><td class=\"opera\"><a href=\"javascript:;\" data-name=\"delCorrectionPerson\" data-value=\"{3}\">删除</a></td</tr>";
                string sql = @" select  Student_Mutual_Correct_Temp_Id,Student_Id,u.userName,u.trueName from Student_Mutual_Correct_Temp sc
inner join f_user u on u.userId=sc.Student_Id where Correct_Guid='" + Correct_Guid.Filter() + "' ";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Str += string.Format(temp
                            , item["trueName"].ToString() + "(" + item["userName"] + ")"
                            , GetCorrectStudent(Correct_Guid, item["Student_Id"].ToString())
                            , GetCountCorrect(Correct_Guid, item["Student_Id"].ToString())
                            , item["Student_Mutual_Correct_Temp_Id"].ToString());
                    }
                    return Str;
                }
                else
                {
                    return "<tr><td class=\"text-center\" colspan=\"100\">暂无数据</td></tr>";
                }
            }
            catch (Exception ex)
            {
                return " ";
            }
        }
        /// <summary>
        /// 获取被批改人
        /// </summary>
        /// <param name="Student_Mutual_Correct_Guid"></param>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public static string GetCorrectStudent(string Correct_Guid, string StudentId)
        {
            try
            {
                Correct_Guid = Correct_Guid.Filter();
                StudentId = StudentId.Filter();
                string str = string.Empty;
                string temp = " <span class=\"tag\" data-name=\"tag\">{0}<i data-name=\"delCorrectedPerson\" data-value=\"{1}\">×</i></span>";
                string tempAdd = "<span class=\"tag_add\" data-name=\"selectCorrected\" data-smcid=\"{0}\" data-stuid=\"{1}\">+</span>";
                string sql = @"select  Student_Mutual_CorrectSub_Temp_Id,Student_Id,u.userName,u.trueName from Student_Mutual_CorrectSub_Temp sc
inner join f_user u on u.userId=sc.Student_Id where Correct_Guid='" + Correct_Guid.Filter() + "' and Student_Mutual_Correct_Temp_Id='" + StudentId.Filter() + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        str += string.Format(temp
                            , item["trueName"].ToString() + "(" + item["userName"] + ")"
                            , item["Student_Mutual_CorrectSub_Temp_Id"].ToString());
                    }
                    return str += string.Format(tempAdd, Correct_Guid, StudentId);
                }
                else
                {
                    return string.Format(tempAdd, Correct_Guid, StudentId);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 获取被批改人数量
        /// </summary>
        /// <param name="Student_Mutual_Correct_Guid"></param>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public static string GetCountCorrect(string Correct_Guid, string StudentId)
        {
            try
            {
                Correct_Guid = Correct_Guid.Filter();
                StudentId = StudentId.Filter();
                int i = new BLL_Student_Mutual_CorrectSub_Temp().GetRecordCount("Correct_Guid='" + Correct_Guid + "' and Student_Mutual_Correct_Temp_Id='" + StudentId.Filter() + "'");
                if (i > 0)
                {
                    return i.ToString();

                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// 删除被修改人
        /// </summary>
        /// <param name="Student_Mutual_Correct_Temp_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteBeCorrect(string Student_Mutual_CorrectSub_Temp_Id)
        {
            try
            {
                Student_Mutual_CorrectSub_Temp_Id = Student_Mutual_CorrectSub_Temp_Id.Filter();
                if (new BLL_Student_Mutual_CorrectSub_Temp().Delete(Student_Mutual_CorrectSub_Temp_Id))
                {
                    return "1";
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
        /// 选择被批改人
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="Correct_Guid">唯一标识</param>
        /// <returns></returns>
        [WebMethod]
        public static string SelectBeCorrected(string HomeWork_Id, string Correct_Guid, string StudentId)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                Correct_Guid = Correct_Guid.Filter();
                StudentId = StudentId.Filter();
                string Str = string.Empty;
                string temp = "<label>{0}<input type=\"checkbox\" name=\"name\" sname=\"{0}\" value=\"{1}\" shwid=\"{2}\" tt=\"{3}\" /></label>";
                //这个作业下所有人
                string sql = @"select shw.HomeWork_Id,shw.Student_HomeWork_Id,shw.Student_Id,u.userName,u.trueName,Student_Mutual_CorrectSub_Temp_Id,
sum(shwa.Student_Score) as Student_Score from [dbo].[Student_HomeWork] shw
inner join [dbo].[Student_HomeWork_Submit] shwS on shwS.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwS.Student_HomeWork_Status='1'
inner join [dbo].[Student_HomeWork_Correct] shwC on shwC.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwC.Student_HomeWork_CorrectStatus='0'
inner join f_user u on u.userId=shw.Student_Id
left join [Student_Mutual_CorrectSub_Temp] smc on smc.Student_Id=shw.Student_Id and shw.HomeWork_Id=smc.HomeWork_Id and smc.Correct_Guid='" + Correct_Guid + @"'
 left join Student_HomeWorkAnswer shwa on shwa.Student_HomeWork_Id=shw.Student_HomeWork_Id 
where shw.HomeWork_Id='" + HomeWork_Id + "' and shw.Student_Id<>'" + StudentId + "' group by shw.HomeWork_Id,shw.Student_HomeWork_Id,shw.Student_Id,u.userName,u.trueName,Student_Mutual_CorrectSub_Temp_Id order by Student_Score";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("Student_Mutual_CorrectSub_Temp_Id is null");//未被批改的人
                    if (dr.Length > 0)//存在被批改人g
                    {
                        foreach (DataRow item in dr)
                        {
                            Str += string.Format(temp
                                , item["trueName"].ToString() + "(" + item["userName"] + ")"
                                , item["Student_Id"].ToString()
                                , item["Student_HomeWork_Id"].ToString()
                                , Guid.NewGuid().ToString());
                        }
                        return Str;
                    }
                    else
                    {
                        return "<span class=\"text-center\">暂无可批改的学生</span>";
                    }
                }
                else
                {
                    return "<span class=\"text-center\">暂无可批改的学生</span>";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 选择批改人
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="Correct_Guid">唯一标识</param>
        /// <returns></returns>
        [WebMethod]
        public static string SelectCorrected(string HomeWork_Id, string Correct_Guid)
        {
            try
            {
                HomeWork_Id = HomeWork_Id.Filter();
                Correct_Guid = Correct_Guid.Filter();
                string Str = string.Empty;
                string temp = "<label>{0}<input type=\"checkbox\" name=\"test\" sname=\"{0}\" value=\"{1}\" tt=\"{2}\" /></label>";
                //这个作业下所有人
                string sql = @"select hw.HomeWork_Id,hw.UserGroup_Id,UGM.MembershipEnum,u.userId as Student_Id,u.userName,u.trueName,Student_Mutual_Correct_Temp_Id from HomeWork hw
inner join dbo.UserGroup_Member UGM on ugm.UserGroup_Id=hw.UserGroup_Id and User_ApplicationStatus='passed' and UserStatus='0' and MembershipEnum='student'
inner join f_user u on u.userId=UGM.User_ID
left join [Student_Mutual_Correct_Temp] smc on smc.Student_Id=u.userId and hw.HomeWork_Id=smc.HomeWork_Id and smc.Correct_Guid='" + Correct_Guid + @"'
where hw.HomeWork_Id='" + HomeWork_Id + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("Student_Mutual_Correct_Temp_Id is null");//批改的人
                    if (dr.Length > 0)//存在批改人g
                    {
                        foreach (DataRow item in dr)
                        {
                            Str += string.Format(temp
                                , item["trueName"].ToString() + "(" + item["userName"] + ")"
                                , item["Student_Id"].ToString()
                                , Guid.NewGuid().ToString());
                        }
                        return Str;
                    }
                    else
                    {
                        return "<span class=\"text-center\">暂无数据</span>";
                    }
                }
                else
                {
                    return "<span class=\"text-center\">暂无数据</span>";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// 增加被批改人
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="Correct_Guid"></param>
        /// <param name="ArrStudenId"></param>
        /// <param name="ArrShwId"></param>
        /// <param name="ArrId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string AddBeCorrected(string HomeWork_Id, string StudentId, string Correct_Guid, string ArrStudenId, string ArrShwId, string ArrId)
        {
            try
            {
                string sql = string.Empty;
                string temp = @"INSERT INTO [dbo].[Student_Mutual_CorrectSub_Temp]
           ([Student_Mutual_CorrectSub_Temp_Id]
           ,[Student_Mutual_Correct_Temp_Id]
           ,[Correct_Guid]
           ,[HomeWork_Id]
           ,[Student_HomeWork_Id]
           ,[Student_Id]
           ,[CreateTime]
           ,[Remark]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');";
                string[] strStudenId = ArrStudenId.Split(',');
                string[] strShwId = ArrShwId.Split(',');
                string[] strId = ArrId.Split(',');
                if (strStudenId.Length > 0)
                {
                    for (int i = 0; i < strStudenId.Length; i++)
                    {
                        sql += string.Format(temp, strId[i], StudentId, Correct_Guid, HomeWork_Id, strShwId[i], strStudenId[i], DateTime.Now, "");
                    }
                    if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
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
        /// <summary>
        /// 删除批改人，同时删除子表被批改人
        /// </summary>
        /// <param name="Student_Mutual_Correct_Temp_Id"></param>
        /// <returns></returns>
        [WebMethod]
        public static string DeleteCorrect(string Student_Mutual_Correct_Temp_Id)
        {
            try
            {
                Model_Student_Mutual_Correct_Temp model = new BLL_Student_Mutual_Correct_Temp().GetModel(Student_Mutual_Correct_Temp_Id);
                if (model != null)
                {
                    string sql = string.Format(@" delete from Student_Mutual_Correct_Temp where Student_Mutual_Correct_Temp_Id='{0}';
                                                  delete from Student_Mutual_CorrectSub_Temp where Correct_Guid='{1}' and Student_Mutual_Correct_Temp_Id='{2}' and HomeWork_Id='{3}'", model.Student_Mutual_Correct_Temp_Id, model.Correct_Guid, model.Student_Id, model.HomeWork_Id);
                    if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
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

        /// <summary>
        /// 分配被批改人的进度
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="Correct_Guid"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetProgressData(string HomeWork_Id, string Correct_Guid)
        {
            try
            {
                string sql = string.Format(@"select COUNT(*) as countStudent from [dbo].[Student_HomeWork] shw
inner join [dbo].[Student_HomeWork_Submit] shwS on shwS.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwS.Student_HomeWork_Status='1'
inner join [dbo].[Student_HomeWork_Correct] shwC on shwC.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwC.Student_HomeWork_CorrectStatus='0'
where HomeWork_Id='{0}'
union all
select count(*) as countStudent from [dbo].[Student_Mutual_CorrectSub_Temp]
where HomeWork_Id='{0}' and Correct_Guid='{1}'", HomeWork_Id, Correct_Guid);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string CountStudent = dt.Rows[0]["countStudent"].ToString();
                    string CountDistributionStudent = dt.Rows[1]["countStudent"].ToString();

                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        CountStudent = CountStudent,//总学生数
                        CountDistributionStudent = CountDistributionStudent//已分配数
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ""
                });
            }
        }

        /// <summary>
        /// 完成分配
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="Correct_Guid"></param>
        /// <returns></returns>
        [WebMethod]
        public static string CompleteAllocation(string HomeWork_Id, string Correct_Guid)
        {
            try
            {
                string sql = string.Empty;
                #region 删除历史数据（不删除已批改的）
                sql = string.Format(@" delete from Student_Mutual_CorrectSub
  where Correct_Guid='{1}' and 
  Student_HomeWork_Id  in (select t1.Student_HomeWork_Id from Student_Mutual_CorrectSub t1
  inner join Student_HomeWork_Correct t2 on t2.Student_HomeWork_Id=t1.Student_HomeWork_Id and t2.Student_HomeWork_CorrectStatus<>'1' and Correct_Guid='{1}' and HomeWork_Id='{0}');
                                        delete from Student_Mutual_Correct where 
  Correct_Guid='{1}' and Student_Id not in
  (select Student_Mutual_Correct_Id from Student_Mutual_CorrectSub where Correct_Guid='{1}' and HomeWork_Id='{0}')", HomeWork_Id, Correct_Guid);
                #endregion
                //复制批改人表
                sql += string.Format(@"INSERT INTO [dbo].[Student_Mutual_Correct]
           ([Student_Mutual_Correct_Id]
           ,[Correct_Guid]
           ,[HomeWork_Id]
           ,[Student_Id]
           ,[CreateTime]
           ,[Remark]) select NewId(),Correct_Guid,HomeWork_Id,Student_Id,getdate(),'' from Student_Mutual_Correct_Temp where HomeWork_Id='{0}' and Correct_Guid='{1}' ;", HomeWork_Id, Correct_Guid);
                //复制被批改人
                sql += string.Format(@" INSERT INTO [dbo].[Student_Mutual_CorrectSub]
           ([Student_Mutual_CorrectSub_Id]
           ,[Student_Mutual_Correct_Id]
           ,[Correct_Guid]
           ,[HomeWork_Id]
           ,[Student_HomeWork_Id]
           ,[Student_Id]
           ,[CreateTime]
           ,[Remark]) select NewId(),Student_Mutual_Correct_Temp_Id,Correct_Guid,HomeWork_Id,Student_HomeWork_Id,Student_Id,getdate(),'' from Student_Mutual_CorrectSub_Temp where  HomeWork_Id='{0}' and Correct_Guid='{1}' ;", HomeWork_Id, Correct_Guid);
                if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
                {
                    return "1";
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
        /// 平均分配
        /// </summary>
        /// <param name="HomeWork_Id"></param>
        /// <param name="Correct_Guid"></param>
        /// <returns></returns>
        [WebMethod]
        public static string AllocationAvg(string HomeWork_Id, string Correct_Guid, string arrStuId)
        {
            try
            {
                bool IsRandom = false;//是否启用随机分配
                HomeWork_Id = HomeWork_Id.Filter();
                Correct_Guid = Correct_Guid.Filter();
                int count = 0;//未分配的学生数
                arrStuId = arrStuId.Filter();
                string[] strStudenId = arrStuId.Split(',');
                #region 判断被批改人数的是否小于批改人
                //所有未被批改的学生
                string sqlAll = @"select shw.*,smc.Student_Mutual_CorrectSub_Temp_Id   from [dbo].[Student_HomeWork] shw
inner join [dbo].[Student_HomeWork_Submit] shwS on shwS.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwS.Student_HomeWork_Status='1'
inner join [dbo].[Student_HomeWork_Correct] shwC on shwC.Student_HomeWork_Id=shw.Student_HomeWork_Id and shwC.Student_HomeWork_CorrectStatus='0'
left join [Student_Mutual_CorrectSub_Temp] smc on smc.Student_Id=shw.Student_Id and shw.HomeWork_Id=smc.HomeWork_Id and smc.Correct_Guid='" + Correct_Guid + @"'
where shw.HomeWork_Id='" + HomeWork_Id + "' ";
                DataTable dtAll = Rc.Common.DBUtility.DbHelperSQL.Query(sqlAll).Tables[0];
                DataRow[] dr = dtAll.Select("Student_Mutual_CorrectSub_Temp_Id is null");//未被批改的人
                if (dr.Length == strStudenId.Length)//如果批改人数量等于被批改人的数量就用随机分配
                {
                    IsRandom = true;
                }
                //已经分配过的被批改学生
                string sqlCorrected = @" select *  from Student_Mutual_CorrectSub_Temp where HomeWork_Id='" + HomeWork_Id + "' and Correct_Guid='" + Correct_Guid + "'";
                DataTable dtC = Rc.Common.DBUtility.DbHelperSQL.Query(sqlCorrected).Tables[0];
                count = dtAll.Rows.Count - dtC.Rows.Count;
                if (strStudenId.Length > count)//批改人数大于被批改人数
                {
                    return "2";
                }
                if (strStudenId.Length == 1 && dr.Length == 1)
                {
                    if (strStudenId.Contains(dr[0]["Student_Id"].ToString()))//相同的人
                    {
                        return "3";
                    }
                }
                #endregion


                string sql = string.Empty;
                string temp = @"INSERT INTO [dbo].[Student_Mutual_Correct_Temp]
           ([Student_Mutual_Correct_Temp_Id]
           ,[Correct_Guid]
           ,[HomeWork_Id]
           ,[Student_Id]
           ,[CreateTime]
           ,[Remark])
     VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');";
                if (strStudenId.Length > 0)
                {
                    for (int i = 0; i < strStudenId.Length; i++)
                    {
                        sql += string.Format(temp, Guid.NewGuid().ToString(), Correct_Guid, HomeWork_Id, strStudenId[i].ToString(), DateTime.Now, "");
                    }
                    if (Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sql) > 0)
                    {
                        int row = 0;
                        if (dtC.Rows.Count > 0)
                        {
                            row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec P_Student_Mutual_Correct_Avg1'" + HomeWork_Id + "','" + Correct_Guid.Filter() + "'", 7200);
                        }
                        else
                        {
                            //if (IsRandom)//是否启用随机分配
                            //{
                            //    row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec P_Student_Mutual_Correct'" + HomeWork_Id + "','" + Correct_Guid.Filter() + "'", 7200);
                            //}
                            //else
                            //{
                            row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSqlByTime("exec P_Student_Mutual_Correct_Avg'" + HomeWork_Id + "','" + Correct_Guid.Filter() + "'", 7200);
                            //}
                        }
                        if (row > 0)
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