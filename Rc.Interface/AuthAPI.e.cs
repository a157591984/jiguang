using Rc.BLL.Resources;
using Rc.Model.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Rc.Common.StrUtility;
using Rc.Common.Config;
using Rc.Common;
using Newtonsoft.Json;
using System.Web;
using System.Threading;

namespace Rc.Interface.AuthApi
{
    public partial class indexNew
    {
        /// <summary>
        /// 获取列表：学生可见的testpaper资源列表
        /// </summary>
        private string GetStudentResource(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            bool isWritable = false;
            bool isPrintable = false;                   // 资源是否可打印，true可打印。
            bool isPrivate = false;                     // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                    // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = false;                     // 资源是否可删除，默认为 true
            bool permitMove = false;                    // 资源是否可移动，默认为 true
            bool permitSave = false;                    // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
            bool permitCopy = false;                    // 资源是否可复制，默认为 true
            bool TheBookWasPaied = false;               // 是否购买了此资源 ，true 购买了
            #region 学生
            if (folderId == "0")
            {
                #region 学科
                DataTable dtF = new DataTable();
                strSql = new StringBuilder();
                strSql.Append(" select dic.Common_Dict_ID,dic.D_Name,dic.D_CreateTime,dic.D_CreateUser ");
                string strWhere = string.Empty;
                if (tabid == EnumTabindex.StudentSkillNew.ToString())
                {
                    strWhere += " and SHWSubmit.Student_HomeWork_Status='0' ";
                }
                else if (tabid == EnumTabindex.StudentSkillSubminted.ToString())
                {
                    strWhere += " and SHWSubmit.Student_HomeWork_Status='1' ";
                }
                else if (tabid == EnumTabindex.StudentSkillWrong.ToString())
                {
                    strWhere += string.Format(" and SHWSubmit.Student_HomeWork_Status='1' and SHWCorrect.Student_HomeWork_CorrectStatus='1' ");
                    strWhere += string.Format(" and shw.Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWorkAnswer where Student_Id='{0}' and  Student_Answer_Status!='right' and Student_Answer_Status!='unknown' ) "
                        , userId);
                }

                strSql.AppendFormat(@" from Common_Dict dic where dic.D_Type='7' and Common_Dict_Id in(
select SubjectId from HomeWork hw inner join Student_HomeWork shw on shw.HomeWork_Id=hw.HomeWork_Id
                                  inner join Student_HomeWork_Submit SHWSubmit on SHWSubmit.Student_HomeWork_Id=shw.Student_HomeWork_id
                                   inner join Student_HomeWork_Correct SHWCorrect on SHWCorrect.Student_HomeWork_Id=shw.Student_HomeWork_id where shw.Student_Id='{0}' )  ", userId);
                dtF = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

                string strSqlCount = string.Format(@"select hw.SubjectId,count(1) as icount from dbo.Student_HomeWork SHW
                inner join HomeWork HW on SHW.HomeWork_Id=HW.HomeWork_Id
                inner join Student_HomeWork_Submit SHWSubmit on SHWSubmit.Student_HomeWork_Id=shw.Student_HomeWork_id
                inner join Student_HomeWork_Correct SHWCorrect on SHWCorrect.Student_HomeWork_Id=shw.Student_HomeWork_id
                where SHW.Student_Id='{0}' {1} group by hw.SubjectId ", userId, strWhere);
                DataTable dtCount = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlCount).Tables[0];

                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    int intCount = 0;
                    DataRow[] drCount = dtCount.Select("SubjectId='" + dtF.Rows[i]["Common_Dict_ID"].ToString() + "'");
                    if (drCount.Length > 0)
                    {
                        int.TryParse(drCount[0]["icount"].ToString(), out intCount);
                    }
                    listObj.Add(new
                    {
                        id = dtF.Rows[i]["Common_Dict_ID"].ToString(),
                        title = string.Format("{0}({1})", dtF.Rows[i]["D_Name"].ToString(), intCount),
                        isFolder = true,
                        ext = "",
                        typeId = Rc.Common.Config.Resource_TypeConst.testPaper类型文件,
                        typeName = "作业",
                        fileType = "folder",
                        dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dtF.Rows[i]["D_CreateTime"].ToString()),
                        userId = dtF.Rows[i]["D_CreateUser"].ToString(),
                        //userName = GetUserNameByUserId(item.D_CreateUser),
                        isWritable = isWritable,                    // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = false,                        // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                      // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                    // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                      // 资源是否可删除，默认为 true
                        permitMove = permitMove,                    // 资源是否可移动，默认为 true
                        permitSave = permitSave,                    // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                        permitCopy = permitCopy,                    // 资源是否可复制，默认为 true
                        version = "",
                        streamUrl = "",
                        downloadUrl = "",
                        visitUrl = ""
                    });
                }
                #endregion
            }
            else
            {
                #region 学生
                strSql.Append("select top 100 SHW.Student_HomeWork_Id,SHW.HomeWork_Id,SHW.Student_Id,SHW.CreateTime,Student_Answer_Time,CorrectTime,SHWSubmit.Student_HomeWork_Status,SHWCorrect.Student_HomeWork_CorrectStatus,HW.HomeWork_Name,HW.HomeWork_AssignTeacher,HW.BeginTime,HW.StopTime,HW.isTimeLimt,RTRF.ResourceToResourceFolder_Id,RTRF.ResourceFolder_Id,RTRF.Resource_Id,");
                strSql.Append("RTRF.File_Name,RTRF.File_Owner,REPLACE(RTRF.File_Suffix,'.','') File_Suffix,RTRF.Resource_Type,RTRF.CreateFUser,RTRF.Resource_Version,ISNULL(UBR.Book_ID,'-1') AS TheBookWasPaied,bk.BookPrice,RTRF.Resource_Class from dbo.Student_HomeWork SHW");
                strSql.Append(" inner join HomeWork HW on SHW.HomeWork_Id=HW.HomeWork_Id");
                strSql.Append(" inner join Student_HomeWork_Submit SHWSubmit on SHWSubmit.Student_HomeWork_Id=SHW.Student_HomeWork_id");
                strSql.Append(" inner join Student_HomeWork_Correct SHWCorrect on SHWCorrect.Student_HomeWork_Id=SHW.Student_HomeWork_id");
                strSql.Append(" left join ResourceToResourceFolder RTRF on RTRF.ResourceToResourceFolder_Id=HW.ResourceToResourceFolder_Id");
                strSql.Append(" LEFT JOIN Bookshelves bk ON bk.ResourceFolder_Id=RTRF.Book_id ");
                strSql.AppendFormat(" LEFT JOIN UserBuyResources UBR ON RTRF.Book_ID=UBR.Book_id AND UBR.UserId='{0}'", userId);
                //strSql.AppendFormat(" where 1=1");
                strSql.AppendFormat(" where hw.SubjectId='{0}' and ((HW.IsHide=1 AND HW.BeginTime<=GETDATE()) OR HW.IsHide=0 )", folderId);
                string StrDateTime = string.Empty;
                if (tabid == EnumTabindex.StudentSkillSubminted.ToString())//已完成作业
                {
                    strSql.AppendFormat(" and SHWSubmit.Student_HomeWork_Status='1' ");
                    isWritable = false;                     // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                    // 资源是否可打印，true可打印。
                    isPrivate = true;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = true;                      // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                      // 资源是否可删除，默认为 true
                    permitMove = false;                     // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = false;                     // 资源是否可复制，默认为 true
                    StrDateTime = "1";
                }
                else if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业
                {
                    isWritable = false;                     // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                    // 资源是否可打印，true可打印。
                    isPrivate = false;                      // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = false;                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                      // 资源是否可删除，默认为 true
                    permitMove = false;                     // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = false;                     // 资源是否可复制，默认为 true
                    strSql.AppendFormat(" and SHWSubmit.Student_HomeWork_Status='0' ");
                    StrDateTime = "2";
                }
                else if (tabid == EnumTabindex.StudentSkillWrong.ToString())//错题集
                {
                    strSql.Append(" and SHWSubmit.Student_HomeWork_Status='1' and SHWCorrect.Student_HomeWork_CorrectStatus='1' ");
                    strSql.AppendFormat(" and shw.Student_HomeWork_Id in(select Student_HomeWork_Id from Student_HomeWorkAnswer where Student_Id='{0}' and  Student_Answer_Status!='right' and Student_Answer_Status!='unknown' ) "
                        , userId);
                    StrDateTime = "3";
                }
                strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string time = string.Empty;
                    if (StrDateTime == "1")
                    {
                        time = dt.Rows[i]["Student_Answer_Time"].ToString();
                    }
                    else if (StrDateTime == "2")
                    {
                        time = dt.Rows[i]["CreateTime"].ToString();
                    }
                    else if (StrDateTime == "3")
                    {
                        time = dt.Rows[i]["CorrectTime"].ToString();
                    }
                    bool boolIsFolder = false;
                    string strNoPaiedDesc = string.Empty;
                    if (dt.Rows[i]["TheBookWasPaied"].ToString().Trim() == "-1"
                        && dt.Rows[i]["BookPrice"].ToString() != "0.00"
                        && dt.Rows[i]["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        TheBookWasPaied = false;
                        isWritable = false;
                        strNoPaiedDesc = "你尚未购买此练习册，请联系您的老师。";
                    }
                    else
                    {
                        TheBookWasPaied = true;
                        strNoPaiedDesc = "";
                    }
                    if (dt.Rows[i]["isTimeLimt"].ToString() == "2")//考试
                    {
                        if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业 提交作业截止时间
                        {
                            DateTime stopTime = DateTime.Now;
                            DateTime.TryParse(dt.Rows[i]["StopTime"].ToString(), out stopTime);
                            if (stopTime < DateTime.Now)
                            {
                                TheBookWasPaied = false;
                                isWritable = false;
                                strNoPaiedDesc = "已超过考试提交截止日期。";
                            }
                        }
                    }
                    string strFileType = dt.Rows[i]["File_Suffix"].ToString();
                    if (strFileType != "dsc" && strFileType != "class" && strFileType != "testPaper" && strFileType != "folder")
                    {
                        strFileType = "other";
                    }
                    #region 得到文件下载地址
                    string downLoadUrl = string.Empty;
                    string strDownLoadFileID = string.Empty;
                    string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                    string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                    strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                    strDownLoadFileType = tabid;
                    if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                    {
                        strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                    }
                    //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                    downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                       , AuthAPI_pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                    #endregion
                    listObj.Add(new
                    {
                        id = dt.Rows[i]["Student_HomeWork_Id"],
                        title = dt.Rows[i]["HomeWork_Name"],
                        isFolder = boolIsFolder,
                        ext = strFileType,
                        typeId = dt.Rows[i]["Resource_Type"],
                        typeName = "学生作业",// GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                        fileType = strFileType,
                        dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(time),
                        userId = dt.Rows[i]["CreateFUser"],
                        //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                        isWritable = isWritable,                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = false,                            // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                          // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                          // 资源是否可删除，默认为 true
                        permitMove = permitMove,                        // 资源是否可移动，默认为 true
                        permitSave = permitSave,                        // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                        permitCopy = permitCopy,                        // 资源是否可复制，默认为 true
                        wasPaied = TheBookWasPaied,                     // 是否购买了此资源，默认为true
                        noPaiedDesc = strNoPaiedDesc,                   // 没购买的提示
                        buyUrl = "",                  // 购书URL     
                        version = dt.Rows[i]["Resource_Version"],
                        streamUrl = "",
                        downloadUrl = downLoadUrl,
                        visitUrl = ""
                    });
                }
                #endregion
            }
            #endregion
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 获取列表：学生可见的 云资源/微课件 资源列表
        /// </summary>
        private string GetStudentClassResource(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            bool isWritable = false;
            bool isPrintable = false;                   // 资源是否可打印，true可打印。
            bool isPrivate = false;                     // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                    // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = false;                     // 资源是否可删除，默认为 true
            bool permitMove = false;                    // 资源是否可移动，默认为 true
            bool permitSave = false;                    // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
            bool permitCopy = false;                    // 资源是否可复制，默认为 true
            bool TheBookWasPaied = true;                // 是否购买了此资源 ，true 购买了
            #region 学生
            if (folderId == "0")
            {
                #region 学科
                DataTable dtF = new DataTable();
                strSql = new StringBuilder();
                strSql.Append(" select dic.Common_Dict_ID,dic.D_Name,dic.D_CreateTime,dic.D_CreateUser ");
                strSql.AppendFormat(@",DataCount=(select COUNT(1) from ResourceFolder where Subject=dic.Common_Dict_ID and Resource_Type='{0}'
and ResourceFolder_Id in(select Book_ID from UserBuyResources where UserId='{1}'))", strResource_Type, userId);


                strSql.AppendFormat(@" from Common_Dict dic where dic.D_Type='7' and Common_Dict_Id in( select [Subject] from dbo.ResourceFolder
where book_id in(select Book_Id from dbo.UserBuyResources where UserId='{0}') )  ", userId);
                dtF = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
                for (int i = 0; i < dtF.Rows.Count; i++)
                {
                    listObj.Add(new
                    {
                        id = dtF.Rows[i]["Common_Dict_ID"].ToString() + "?",//多返回?，区分是否为学科文件夹
                        title = string.Format("{0}({1})", dtF.Rows[i]["D_Name"].ToString(), dtF.Rows[i]["DataCount"].ToString()),
                        isFolder = true,
                        ext = "",
                        typeId = strResource_Type,
                        typeName = "",//"微课件",
                        fileType = "folder",
                        dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dtF.Rows[i]["D_CreateTime"].ToString()),
                        userId = dtF.Rows[i]["D_CreateUser"].ToString(),
                        //userName = GetUserNameByUserId(item.D_CreateUser),
                        isWritable = isWritable,                // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = false,                    // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                  // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                  // 资源是否可删除，默认为 true
                        permitMove = permitMove,                // 资源是否可移动，默认为 true
                        permitSave = permitSave,                // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                        permitCopy = permitCopy,                // 资源是否可复制，默认为 true
                        version = "",
                        streamUrl = "",
                        downloadUrl = "",
                        visitUrl = ""
                    });
                }
                #endregion
            }
            else
            {
                #region 学生

                if (folderId.IndexOf('?') > 0)//根据学科加载数据
                {
                    strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder  a
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
 where ResourceFolder_Level='5' and Subject='{1}' "
                 , userId
                 , folderId.TrimEnd('?'));
                }
                else
                {
                    strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder  a
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
 where ResourceFolder_ParentId='{1}'"
                 , userId
                 , folderId);
                }

                strSql.AppendFormat(" and Resource_Type ='{0}'", strResource_Type);
                strSql.AppendFormat(" and Resource_Class ='{0}'", Rc.Common.Config.Resource_ClassConst.云资源);

                strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool boolIsFolder = false;
                    //RType=为文件夹
                    if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;
                    string strNoPaiedDesc = string.Empty;
                    string strFileType = dt.Rows[i]["File_Suffix"].ToString();
                    if (strFileType != "dsc" && strFileType != "class" && strFileType != "testPaper" && strFileType != "folder")
                    {
                        strFileType = "other";
                    }
                    #region 得到文件下载地址
                    string downLoadUrl = string.Empty;
                    string strDownLoadFileID = string.Empty;
                    string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                    string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                    strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                    strDownLoadFileType = tabid;
                    if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                    {
                        strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                    }
                    //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                    downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                      , AuthAPI_pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                    #endregion

                    if (dt.Rows[i]["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        #region 云资源权限控制
                        BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[i]["Book_ID"].ToString());
                        isPrintable = bkAttrModel.IsPrint;
                        permitSave = bkAttrModel.IsSave;
                        permitCopy = bkAttrModel.IsCopy;
                        #endregion
                    }

                    listObj.Add(new
                    {
                        id = dt.Rows[i]["ResourceFolder_Id"],
                        title = dt.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                        isFolder = boolIsFolder,
                        ext = strFileType,
                        typeId = dt.Rows[i]["Resource_Type"],
                        typeName = "",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                        fileType = strFileType,
                        dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                        userId = dt.Rows[i]["CreateFUser"],
                        //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                        isWritable = isWritable,                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                        isPrintable = isPrintable,                      // 资源是否可打印，true可打印。
                        isPrivate = isPrivate,                          // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                        isSubmited = isSubmited,                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                        permitDel = permitDel,                          // 资源是否可删除，默认为 true
                        permitMove = permitMove,                        // 资源是否可移动，默认为 true
                        permitSave = permitSave,                        // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                        permitCopy = permitCopy,                        // 资源是否可复制，默认为 true
                        wasPaied = TheBookWasPaied,                     // 是否购买了此资源，默认为true
                        noPaiedDesc = strNoPaiedDesc,                   // 没购买的提示
                        buyUrl = "",                                    // 购书URL     
                        version = dt.Rows[i]["Resource_Version"],
                        streamUrl = "",
                        downloadUrl = downLoadUrl,
                        visitUrl = ""
                    });
                }
                #endregion
            }
            #endregion
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 获取列表：老师自有资源(包括：老师的ScienceWord文件类型,class文件类型，testPaper文件类型)
        /// </summary>
        private string GetTeacherOwnResource(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            bool isWritable = true;
            bool isPrintable = false;                   // 资源是否可打印，true可打印。
            bool isPrivate = false;                     // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                    // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = true;                     // 资源是否可删除，默认为 true
            bool permitMove = true;                    // 资源是否可移动，默认为 true
            bool permitSave = true;                    // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
            bool permitCopy = true;                    // 资源是否可复制，默认为 true

            #region 初始化默认文件夹
            string strWhere = string.Format("ResourceFolder_ParentId='0' and Resource_Class='{0}' and ResourceFolder_Owner='{1}' and Resource_Type='{2}'"
                , Rc.Common.Config.Resource_ClassConst.自有资源
                , userId
                , strResource_Type);
            BLL_ResourceFolder bllRF = new BLL_ResourceFolder();
            if (bllRF.GetRecordCount(strWhere) == 0)
            {
                Model_ResourceFolder modelRF = new Model_ResourceFolder();
                modelRF.ResourceFolder_Id = Guid.NewGuid().ToString();
                modelRF.ResourceFolder_ParentId = "0";
                modelRF.ResourceFolder_Name = "默认文件夹";
                modelRF.ResourceFolder_Level = 0;
                modelRF.Resource_Type = strResource_Type;
                modelRF.Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                modelRF.Resource_Version = "";
                modelRF.ResourceFolder_Remark = "";
                modelRF.ResourceFolder_Order = -1;
                modelRF.ResourceFolder_Owner = userId;
                modelRF.CreateFUser = userId;
                modelRF.CreateTime = DateTime.Now;
                modelRF.ResourceFolder_isLast = "0";
                bllRF.Add(modelRF);

            }
            #endregion

            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder where ResourceFolder_ParentId='{0}' and CreateFUser='{1}'"
              , folderId, userId);

            if (strResource_Type != "")
            {
                strSql.AppendFormat(" and Resource_Type='{0}'", strResource_Type);
            }
            strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

            #region 学校自有资源权限控制
            BookAttrModel bkAttrModel = GetBookAttrValue_SchoolByUserId(userId);
            isPrintable = bkAttrModel.IsPrint;
            permitSave = bkAttrModel.IsSave;
            permitCopy = bkAttrModel.IsCopy;
            #endregion

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool boolIsFolder = false;
                //RType=0为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;
                string strFileType = string.Empty;
                //老师ScienceWord 自有习题集
                if (tabid == EnumTabindex.TeacherScienceWordOwnSkill.ToString())
                {
                    strFileType = "testPaper";
                }
                #region 得到文件下载地址
                string downLoadUrl = string.Empty;
                string strDownLoadFileID = string.Empty;
                string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                strDownLoadFileType = tabid;
                if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                {
                    strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                }
                //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                       , AuthAPI_pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                #endregion

                listObj.Add(new
                {
                    id = dt.Rows[i]["ResourceFolder_Id"],
                    title = dt.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                    ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                    isFolder = boolIsFolder,
                    ext = dt.Rows[i]["File_Suffix"],
                    typeId = dt.Rows[i]["Resource_Type"],
                    typeName = "自有习题集",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                    fileType = strFileType,
                    dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateFUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = isWritable,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = isPrintable,                    // 资源是否可打印，true可打印。
                    isPrivate = isPrivate,                      // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = isSubmited,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = permitDel,                       // 资源是否可删除，默认为 true
                    permitMove = permitMove,                      // 资源是否可移动，默认为 true
                    permitSave = permitSave,                      // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = permitCopy,                      // 资源是否可复制，默认为 true
                    version = dt.Rows[i]["Resource_Version"],
                    streamUrl = "",
                    downloadUrl = downLoadUrl,
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }

        /// <summary>
        /// 获取老师集体备课资源
        /// </summary>
        private string GetTeacherCollectiveLessonPreparationResource(Model_F_User_Client modelFUser, string folderId, string strResource_Type, string tabid, string productType, string strFile_Suffix, string userId)
        {
            bool isWritable = false;
            bool isPrintable = false;                   // 资源是否可打印，true可打印。
            bool isPrivate = false;                     // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                    // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = false;                     // 资源是否可删除，默认为 true
            bool permitMove = false;                    // 资源是否可移动，默认为 true
            bool permitSave = false;                    // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
            bool permitCopy = false;                    // 资源是否可复制，默认为 true

            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            if (folderId == "0")
            {
                strSql.AppendFormat(@"select t.* from VW_ResourceAndResourceFolder t where t.ResourceFolder_ParentId='{0}' ", folderId);
                strSql.Append(GetCollectiveLessonPreparationDataRange(modelFUser.UserId, modelFUser.UserPost, modelFUser.Subject));
            }
            //else
            //{
            //    strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder t where t.ResourceFolder_ParentId='{0}' and t.File_Suffix='{1}' "
            //        , folderId, strFile_Suffix);
            //}
            else
            {
                strSql.AppendFormat(@"select * from VW_ResourceAndResourceFolder t where t.ResourceFolder_ParentId='{0}' and (t.File_Suffix='{2}' or t.File_Suffix='' or t.File_Suffix is null)"
              , folderId, userId, strFile_Suffix);
            }

            if (strResource_Type != "")
            {
                strSql.AppendFormat(" and t.Resource_Type='{0}'", strResource_Type);
            }
            strSql.Append(" order by t.CreateTime desc,t.ResourceFolder_Name ");
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool boolIsFolder = false;
                //RType=0为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;
                string strFileType = dt.Rows[i]["File_Suffix"].ToString();
                #region 得到文件下载地址
                string downLoadUrl = string.Empty;
                string strDownLoadFileID = string.Empty;
                string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                strDownLoadFileType = tabid;
                if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                {
                    strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                }
                //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                       , AuthAPI_pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, modelFUser.UserId, productType);
                #endregion
                #region 编辑权限控制
                if (folderId != "0" && dt.Rows[i]["CreateFUser"].ToString() == modelFUser.UserId)
                {
                    isWritable = true;
                    isPrintable = true;
                    permitDel = true;
                    //permitMove = true;
                    //permitSave = true;
                    permitCopy = true;
                }
                #endregion

                listObj.Add(new
                {
                    id = dt.Rows[i]["ResourceFolder_Id"],
                    title = dt.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                    ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                    isFolder = boolIsFolder,
                    ext = dt.Rows[i]["File_Suffix"],
                    typeId = dt.Rows[i]["Resource_Type"],
                    typeName = "集体备课",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                    fileType = strFileType,
                    dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateFUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = isWritable,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = isPrintable,                    // 资源是否可打印，true可打印。
                    isPrivate = isPrivate,                      // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = isSubmited,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = permitDel,                       // 资源是否可删除，默认为 true
                    permitMove = permitMove,                      // 资源是否可移动，默认为 true
                    permitSave = permitSave,                      // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = permitCopy,                      // 资源是否可复制，默认为 true
                    version = dt.Rows[i]["Resource_Version"],
                    streamUrl = "",
                    downloadUrl = downLoadUrl,
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 集体备课 数据范围 17-12-29TS
        /// </summary>
        public static string GetCollectiveLessonPreparationDataRange(string tchId, string tchPost, string tchSubject)
        {
            string conditon = string.Empty;
            try
            {
                switch (tchPost)
                {
                    case UserPost.校长:
                    case UserPost.副校长:
                    case UserPost.教务主任:
                        conditon = string.Format(@" and ResourceFolder_Id in( select ResourceFolder_Id from PrpeLesson where Grade in( select distinct GradeId from VW_UserOnClassGradeSchool where GradeId!='' 
and SchoolId in(select SchoolId from VW_UserOnClassGradeSchool where SchoolId!='' and UserId='{0}' ) ) ) ", tchId);
                        break;
                    case UserPost.年级组长:
                    case UserPost.教研组长:
                        conditon = string.Format(@" and ResourceFolder_Id in( select ResourceFolder_Id from PrpeLesson where Grade in(select GradeId from VW_UserOnClassGradeSchool where GradeId!='' and UserId='{0}' ) ) "
                            , tchId);
                        break;
                    case UserPost.备课组长:
                        conditon = string.Format(@" and ResourceFolder_Id in( select ResourceFolder_Id from PrpeLesson where Subject='{0}' and Grade in(select GradeId from VW_UserOnClassGradeSchool where GradeId!='' and UserId='{1}' ) ) "
                            , tchSubject, tchId);
                        break;

                    case UserPost.普通老师:
                        conditon = string.Format(@"  and ResourceFolder_Id in( select ResourceFolder_Id from PrpeLesson_Person where ChargePerson='{0}' ) "
                            , tchId);
                        break;
                }
            }
            catch (Exception)
            {

            }
            return conditon;
        }
        /// <summary>
        /// 获取列表：老师讲评
        /// </summary>
        private string GetTeacherComment(string token, string userId, string folderId, string strResource_Type, string tabid, string productType)
        {
            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            DataTable dt = new DataTable();
            if (folderId == "0")
            {
                #region 班级目录

                string strWhere = @" and ClassId in(select ClassId from (
select distinct vw.ClassId,AnalysisDataCount=(select COUNT(1) from HomeWork where UserGroup_Id=vw.ClassId)
from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum in('" + MembershipEnum.headmaster + "','" + MembershipEnum.teacher + "') and UserId='" + userId + "') t where AnalysisDataCount>0) ";
                strSql.AppendFormat(@"select distinct ug.UserGroup_Id as Id,ug.UserGroup_Name as Name,ug.UserGroup_ParentId as ParentId,ug.UserGroupOrder,ug.CreateTime,ug.User_Id as CreateUser ,0 as RType 
from VW_UserOnClassGradeSchool vw
inner join UserGroup ug on ug.UserGroup_Id=vw.ClassId
where ClassMemberShipEnum in('{0}','{1}')
and UserId='{2}'" + strWhere + " order by ug.UserGroupOrder,ug.UserGroup_Name"
                , MembershipEnum.headmaster
                , MembershipEnum.teacher
                , userId);
                #endregion
            }
            else
            {
                Model_F_User modelFUser = new BLL_F_User().GetModel(userId);
                #region 作业列表
                strSql.Append(@"SELECT T.ResourceToResourceFolder_Id,T.HomeWork_Id as Id,T.HomeWork_Name as Name,T.CreateTime,T.HomeWork_AssignTeacher as CreateUser,T.HomeWork_Status,re.Subject SubjectID,sc.StatsClassHW_ScoreID,sc.HighestScore,sc.LowestScore,sc.Mode,sc.AVGScore,sc.Median,1 as RType 
from homework T 
inner join dbo.ResourceToResourceFolder re on re.ResourceToResourceFolder_Id=T.ResourceToResourceFolder_Id 
left join UserGroup ug on ug.UserGroup_Id=T.UserGroup_Id
left join StatsClassHW_Score sc on T.HomeWork_Id=sc.HomeWork_ID ");
                strSql.AppendFormat(" WHERE T.UserGroup_Id = '{0}' ", folderId);
                //                strSql.AppendFormat(@" and t.HomeWork_Id in(select t.HomeWork_Id from Student_HomeWork t
                //inner join HomeWork t2 on t2.HomeWork_Id=t.HomeWork_Id
                //inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=t.Student_HomeWork_Id
                //where shwSubmit.Student_HomeWork_Status=1 and t2.UserGroup_Id='{0}') ", folderId);
                //                strSql.Append(GetStrWhereBySelfClassForComment(modelFUser.Subject, modelFUser.UserId));
                strSql.Append(" order by T.CreateTime desc ");
                #endregion
            }

            dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool boolIsFolder = false;
                string strfileType = string.Empty;
                string strgoWebPageType = string.Empty;
                string strgoWebPageParameterID = string.Empty;
                //RType=0为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0")
                {
                    boolIsFolder = true;
                }
                else
                {
                    strfileType = "goWebPage";
                    strgoWebPageType = "CommentTestPaper";
                    strgoWebPageParameterID = string.Format("{0}^{1}^{2}"
                         , dt.Rows[i]["ResourceToResourceFolder_Id"]
                         , dt.Rows[i]["Id"]
                         , Server.UrlEncode(dt.Rows[i]["Name"].ToString()));
                }

                listObj.Add(new
                {
                    id = dt.Rows[i]["Id"],
                    title = dt.Rows[i]["Name"].ToString(),
                    ParentId = "",
                    isFolder = boolIsFolder,
                    ext = "",
                    typeId = "",
                    typeName = "",
                    fileType = strfileType,
                    goWebPageType = strgoWebPageType,
                    goWebPageParameterID = strgoWebPageParameterID,
                    dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = true,                      // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false,                    // 资源是否可打印，true可打印。
                    isPrivate = false,                      // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = false,                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = true,                       // 资源是否可删除，默认为 true
                    permitMove = true,                      // 资源是否可移动，默认为 true
                    permitSave = true,                      // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = true,                      // 资源是否可复制，默认为 true
                    version = "",
                    streamUrl = "",
                    downloadUrl = "",
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        ///  获取列表：云资源(包括：老师的ScienceWord文件类型,class文件类型，testPaper文件类型)
        /// </summary>
        private string GetCloudResource(string token, string userId, string folderId, string strResource_Type, string tabid, string UserIdentity, string productType)
        {
            bool isWritable = false;                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
            bool isPrintable = false;                       // 资源是否可打印，true可打印。
            bool isPrivate = false;                         // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = false;                         // 资源是否可删除，默认为 true
            bool permitMove = false;                        // 资源是否可移动，默认为 true
            bool permitSave = false;                        // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
            bool permitCopy = false;                        // 资源是否可复制，默认为 true

            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            if (UserIdentity == "A")
            {
                strSql.AppendFormat(@"select vw.*,ba.AuditState from VW_ResourceAndResourceFolder_Mgr vw left join BookAudit ba on ba.ResourceFolder_Id=vw.Book_Id where vw.ResourceFolder_ParentId='{0}'"
              , folderId, userId);
                strSql.AppendFormat(" and vw.Resource_Type in('{0}','{1}')", strResource_Type, Rc.Common.Config.Resource_TypeConst.按属性生成的目录);
                strSql.AppendFormat(" and vw.CreateFUser ='{0}'", userId);
            }
            else
            {
                if (folderId == "" || folderId == "0")//先取第一级
                {
                    strSql.AppendFormat(@"select a.*,ba.AuditState from VW_ResourceAndResourceFolder a left join BookAudit ba on ba.ResourceFolder_Id=a.Book_Id 
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
where ResourceFolder_Level='{1}'"
                 , userId, 5);

                }
                else
                { //只取给老师授权的书籍
                    strSql.AppendFormat(@"select a.*,ba.AuditState from VW_ResourceAndResourceFolder  a left join BookAudit ba on ba.ResourceFolder_Id=a.Book_Id 
inner join UserBuyResources b on a.Book_ID=b.Book_id and b.userid='{0}'
 where ResourceFolder_ParentId='{1}'"
                 , userId, folderId);


                }
                strSql.AppendFormat(" and Resource_Type ='{0}'", strResource_Type);

            }

            strSql.AppendFormat(" and Resource_Class ='{0}'", Rc.Common.Config.Resource_ClassConst.云资源);

            strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            Rc.Cloud.Model.Model_Struct_Func UserFun;
            string Module_Id = "10100100"; //生产任务分配
            Rc.Cloud.Model.Model_VSysUserRole loginModel = new Rc.Cloud.BLL.BLL_VSysUserRole().GetSysUserInfoModelBySysUserId(userId);
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(userId, (loginModel == null ? "''" : clsUtility.ReDoStr(loginModel.SysRole_IDs, ',')), Module_Id);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strFileType = string.Empty;
                string strFileSuffix = string.Empty;

                strFileSuffix = dt.Rows[i]["File_Suffix"].ToString();
                strFileType = dt.Rows[i]["Resource_Type"].ToString();
                if (strFileSuffix == "testPaper")
                {
                    strFileSuffix = "dsc";
                }

                if (strResource_Type == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                {
                    strFileType = "testPaper";
                }

                if (UserIdentity == "A")
                {
                    int inttemp = 0;
                    int.TryParse(dt.Rows[i]["ResourceFolder_Level"].ToString(), out inttemp);
                    isWritable = UserFun.Add;
                    permitDel = UserFun.Delete;
                    permitMove = UserFun.Move;
                    permitSave = UserFun.Edit;
                    permitCopy = UserFun.Copy;

                    if (inttemp < 4 && inttemp != -1)
                    {
                        isWritable = false;
                        permitDel = false;
                        permitMove = false;
                        permitSave = false;
                        permitCopy = false;
                    }
                    //if (dt.Rows[i]["AuditState"].ToString() == "1")
                    //{
                    //    permitDel = false;
                    //    permitMove = false;
                    //    permitSave = false;
                    //    permitCopy = false;
                    //}
                }

                if (UserIdentity == "T" && dt.Rows[i]["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                {
                    #region 云资源权限控制
                    BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[i]["Book_ID"].ToString());
                    isPrintable = bkAttrModel.IsPrint;
                    permitSave = bkAttrModel.IsSave;
                    permitCopy = bkAttrModel.IsCopy;
                    #endregion
                }

                bool boolIsFolder = false;
                //RType=为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;
                #region 得到文件下载地址
                string downLoadUrl = string.Empty;
                string strDownLoadFileID = string.Empty;
                string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                strDownLoadFileType = tabid;
                if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                {
                    strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                }
                //downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                       , AuthAPI_pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                #endregion
                listObj.Add(new
                {
                    id = dt.Rows[i]["ResourceFolder_Id"],
                    title = GetSubstringFolderName(dt.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(), dt.Rows[i]["ResourceFolder_Order"].ToString(), UserIdentity),
                    ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                    isFolder = boolIsFolder,
                    ext = strFileSuffix,
                    typeId = dt.Rows[i]["Resource_Type"],
                    typeName = "云作业",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                    fileType = strFileType,
                    dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateFUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = isWritable,                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = isPrintable,                      // 资源是否可打印，true可打印。
                    isPrivate = isPrivate,                          // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = isSubmited,                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = permitDel,                          // 资源是否可删除，默认为 true
                    permitMove = permitMove,                        // 资源是否可移动，默认为 true
                    permitSave = permitSave,                        // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = permitCopy,                        // 资源是否可复制，默认为 true
                    version = dt.Rows[i]["Resource_Version"],
                    streamUrl = "",
                    downloadUrl = downLoadUrl,
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }
        /// <summary>
        /// 添加目录：老师自有资源目录(包括：老师的ScienceWord文件类型,class文件类型，testPaper文件类型)
        /// </summary>
        private string AddTeacherOwnResourcesForder(Model_F_User_Client modelFuser, string userId, string token, string folderId, string title, string strResource_Type, string tabid)
        {
            string strJsion = string.Empty;
            BLL_ResourceFolder bll = new BLL_ResourceFolder();
            Model_ResourceFolder modelRFParent = new Model_ResourceFolder();
            Model_ResourceFolder modelRF = new Model_ResourceFolder();
            int order = 0;
            if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
            {
                int.TryParse(title.Substring(0, 3), out order);
                title = title.Substring(4);
            }
            if (bll.GetRecordCount("ResourceFolder_ParentId='" + folderId + "' and ResourceFolder_Name='" + title + "' and CreateFUser='" + userId + "' and Resource_Type='" + strResource_Type + "'") > 0)
            {
                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    status = false,
                    errorMsg = "文件夹名称已存在",
                    errorCode = "AddFolder"
                });
            }
            else
            {
                string guid = Guid.NewGuid().ToString();
                modelRF.ResourceFolder_Id = guid;
                modelRF.ResourceFolder_Name = title;
                modelRF.CreateFUser = userId;
                modelRF.CreateTime = DateTime.Now;
                modelRF.ResourceFolder_isLast = "0";
                modelRF.ResourceFolder_ParentId = folderId;
                modelRF.Resource_Type = strResource_Type;
                modelRF.Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                modelRF.ResourceFolder_Owner = userId;
                modelRF.Subject = modelFuser.Subject;
                modelRF.ResourceFolder_Order = order;
                //如果为第一级目录
                if (folderId == "0")
                {
                    modelRF.ResourceFolder_Level = 0;
                    modelRF.Book_ID = guid;
                }
                else
                {
                    modelRFParent = bll.GetModel(folderId);
                    if (modelRFParent != null)
                    {
                        modelRF.LessonPlan_Type = modelRFParent.LessonPlan_Type;
                        modelRF.GradeTerm = modelRFParent.GradeTerm;
                        modelRF.Subject = modelRFParent.Subject;
                        modelRF.ResourceFolder_Level = modelRFParent.ResourceFolder_Level + 1;
                        //modelRF.Resource_Type = modelRFParent.Resource_Type;
                        modelRF.Resource_Class = modelRFParent.Resource_Class;
                        modelRF.Resource_Version = modelRFParent.Resource_Version;
                        modelRF.Book_ID = modelRFParent.Book_ID;
                    }
                }
                if (bll.Add(modelRF))
                {
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = true,
                        errorMsg = "",
                        errorCode = ""
                    });
                }
                else
                {
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = false,
                        errorMsg = "添加时，执行失败",
                        errorCode = "AddFolder"
                    });
                }
            }
            return strJsion;
        }

        /// <summary>
        /// 添加目录：老师集体备课
        /// </summary>
        private string AddTeacherCLPResourcesForder(Model_F_User_Client modelFuser, string userId, string token, string folderId, string title, string strResource_Type, string tabid)
        {
            string strJsion = string.Empty;
            BLL_ResourceFolder bll = new BLL_ResourceFolder();
            Model_ResourceFolder modelRFParent = new Model_ResourceFolder();
            Model_ResourceFolder modelRF = new Model_ResourceFolder();
            int order = 0;
            if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
            {
                int.TryParse(title.Substring(0, 3), out order);
                title = title.Substring(4);
            }
            if (folderId == "0" || string.IsNullOrEmpty(folderId))
            {
                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    status = false,
                    errorMsg = "不能在根目录创建文件夹",
                    errorCode = "AddFolder"
                });
            }
            else if (bll.GetRecordCount("ResourceFolder_ParentId='" + folderId + "' and ResourceFolder_Name='" + title + "' and Resource_Type='" + strResource_Type + "'") > 0)
            {
                strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    status = false,
                    errorMsg = "文件夹名称已存在",
                    errorCode = "AddFolder"
                });
            }
            else
            {
                string guid = Guid.NewGuid().ToString();
                modelRF.ResourceFolder_Id = guid;
                modelRF.ResourceFolder_Name = title;
                modelRF.CreateFUser = userId;
                modelRF.CreateTime = DateTime.Now;
                modelRF.ResourceFolder_isLast = "0";
                modelRF.ResourceFolder_ParentId = folderId;
                modelRF.Resource_Type = strResource_Type;
                modelRF.Resource_Class = Rc.Common.Config.Resource_ClassConst.自有资源;
                modelRF.ResourceFolder_Owner = userId;
                modelRF.Subject = modelFuser.Subject;
                modelRF.ResourceFolder_Order = order;
                //如果为第一级目录
                if (folderId == "0")
                {
                    modelRF.ResourceFolder_Level = 0;
                    modelRF.Book_ID = guid;
                }
                else
                {
                    modelRFParent = bll.GetModel(folderId);
                    if (modelRFParent != null)
                    {
                        modelRF.LessonPlan_Type = modelRFParent.LessonPlan_Type;
                        modelRF.GradeTerm = modelRFParent.GradeTerm;
                        modelRF.Subject = modelRFParent.Subject;
                        modelRF.ResourceFolder_Level = modelRFParent.ResourceFolder_Level + 1;
                        //modelRF.Resource_Type = modelRFParent.Resource_Type;
                        modelRF.Resource_Class = modelRFParent.Resource_Class;
                        modelRF.Resource_Version = modelRFParent.Resource_Version;
                        modelRF.Book_ID = modelRFParent.Book_ID;
                    }
                }
                if (bll.Add(modelRF))
                {
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = true,
                        errorMsg = "",
                        errorCode = ""
                    });
                }
                else
                {
                    strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        status = false,
                        errorMsg = "添加时，执行失败",
                        errorCode = "AddFolder"
                    });
                }
            }
            return strJsion;
        }

        /// <summary>
        /// 添加目录：如果是管理员、第1个TAB 添加目录(管理员添加云资源目录)暂时不在客户端实现
        /// </summary>
        private string AddMgrResourcesForder(string userId, string token, string folderId, string title, string strResource_Type, string tabid)
        {
            string strError = string.Empty;
            string strJsion = string.Empty;
            BLL_ResourceFolder bll = new BLL_ResourceFolder();
            Model_ResourceFolder modelRFParent = new Model_ResourceFolder();
            string guid = Guid.NewGuid().ToString();
            Model_ResourceFolder modelRF = new Model_ResourceFolder();
            bool b = false;//是否可创建目录
            string errorMsg = string.Empty;

            int order = 0;
            if (title.Length > 4 && (title.Substring(3, 1) == "-" || title.Substring(3, 1) == "－"))
            {
                int.TryParse(title.Substring(0, 3), out order);
                title = title.Substring(4);
            }
            if (bll.GetRecordCount("ResourceFolder_ParentId='" + folderId + "' and ResourceFolder_Name='" + title + "' ") > 0)
            {
                errorMsg = "文件夹名称已存在";
            }
            else
            {
                modelRF.ResourceFolder_Id = guid;
                modelRF.ResourceFolder_Name = title;
                modelRF.ResourceFolder_Order = order;
                modelRF.CreateFUser = userId;
                modelRF.CreateTime = DateTime.Now;
                // modelRF.ResourceFolder_isLast = "0";
                modelRF.Resource_Type = strResource_Type;
                modelRFParent = bll.GetModelA(folderId);
                if (modelRFParent != null)
                {
                    if (modelRFParent.ResourceFolder_Level < 4)//如果在强制目录下创建目录
                    //if (false)
                    {
                        errorMsg = "此目录下不可创建目录或文件。";
                    }
                    else
                    {
                        modelRF.ParticularYear = modelRFParent.ParticularYear;
                        modelRF.GradeTerm = modelRFParent.GradeTerm;
                        modelRF.Resource_Version = modelRFParent.Resource_Version;
                        modelRF.Subject = modelRFParent.Subject;

                        modelRF.ResourceFolder_ParentId = folderId;
                        modelRF.LessonPlan_Type = modelRFParent.LessonPlan_Type;
                        //modelRF.GradeTerm = modelRFParent.GradeTerm;
                        //modelRF.Subject = modelRFParent.Subject;
                        modelRF.ResourceFolder_Level = modelRFParent.ResourceFolder_Level + 1;
                        //modelRF.Resource_Type = modelRFParent.Resource_Type;
                        modelRF.Resource_Class = Rc.Common.Config.Resource_ClassConst.云资源;
                        //modelRF.Resource_Version = modelRFParent.Resource_Version;
                        if (modelRF.ResourceFolder_Level == 5)//当为书本的第一级目录时
                        {
                            modelRF.Book_ID = guid;
                        }
                        else
                        {
                            modelRF.Book_ID = modelRFParent.Book_ID;
                        }
                        if (bll.Add(modelRF))
                        {
                            #region 记录需要同步的数据
                            Model_SyncData modelSD = new Model_SyncData();
                            modelSD.SyncDataId = Guid.NewGuid().ToString();
                            modelSD.TableName = "ResourceFolder";
                            modelSD.DataId = guid;
                            modelSD.OperateType = "add";
                            modelSD.CreateTime = DateTime.Now;
                            modelSD.SyncStatus = "0";
                            new BLL_SyncData().Add(modelSD);
                            #endregion
                            b = true;
                        }
                        else
                        {
                            errorMsg = "sql执行失败";
                        }
                    }
                }
                else
                {
                    errorMsg = "文件夹父级编号参数错误";
                }
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = b,
                errorMsg = errorMsg,
                errorCode = "AddFolder"
            });
            return strJsion;
        }

        /// <summary>
        /// 管理员 获取云资源目录树
        /// </summary>
        private object GetCloudResourceFolderTree(string strResourceFolder_ParentId, string strResource_Type, DataTable dt)
        {
            List<object> listObj = new List<object>();
            //List<Rc.Model.Resources.Model_ResourceFolderForClient> modelList = new List<Rc.Model.Resources.Model_ResourceFolderForClient>();
            string strWhere = string.Empty;
            strWhere = string.Format(" ResourceFolder_ParentId='{0}'", strResourceFolder_ParentId);
            DataRow[] dr = dt.Select(strWhere, "ResourceFolder_Order,ResourceFolder_Name");
            int rowsCount = dr.Length;
            if (rowsCount > 0)
            {
                Rc.Model.Resources.Model_ResourceFolderForClient model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dr[n]);
                    if (model != null)
                    {
                        string resourceFolder_Name = model.ResourceFolder_Name.ReplaceForFilter();
                        int order = 0;
                        int.TryParse(model.ResourceFolder_Order, out order);
                        if (order > 0)
                        {
                            resourceFolder_Name = GetSubstringFolderName(model.ResourceFolder_Name.ReplaceForFilter(), model.ResourceFolder_Order, "A"); //string.Format("{0}-{1}", model.ResourceFolder_Order, model.ResourceFolder_Name);
                        }
                        listObj.Add(new
                        {
                            ResourceFolder_Id = model.ResourceFolder_Id,
                            ResourceFolder_Name = resourceFolder_Name,
                            list = GetCloudResourceFolderTree(model.ResourceFolder_Id, strResource_Type, dt)
                        });
                    }
                }
                return listObj;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 老师获取自有资源目录树
        /// </summary>
        private object GetTeacherOwnResourceFolderTree(string userId, string strResourceFolder_ParentId, string strResource_Type, DataTable dt, string tabid)
        {
            List<object> listObj = new List<object>();
            string strWhere = string.Empty;
            if (strResourceFolder_ParentId == "0")
            {
                strWhere = string.Format(" ResourceFolder_ParentId='{0}'", "0");
            }
            else
            {
                strWhere = string.Format(" ResourceFolder_ParentId='{0}'", strResourceFolder_ParentId);
            }

            List<Rc.Model.Resources.Model_ResourceFolderForClient> modelList = new List<Rc.Model.Resources.Model_ResourceFolderForClient>();
            DataRow[] dr = dt.Select(strWhere, "ResourceFolder_Order,ResourceFolder_Name");
            int rowsCount = dr.Length;
            if (rowsCount > 0)
            {
                Rc.Model.Resources.Model_ResourceFolderForClient model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dr[n]);
                    if (model != null)
                    {
                        listObj.Add(new
                        {
                            ResourceFolder_Id = model.ResourceFolder_Id,
                            ResourceFolder_Name = model.ResourceFolder_Name.ReplaceForFilter(),
                            list = GetTeacherOwnResourceFolderTree(userId, model.ResourceFolder_Id, strResource_Type, dt, tabid)
                        });
                    }
                }
                return listObj;
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// 老师获取集体备课目录树
        /// </summary>
        private object GetTeacherCLPResourceFolderTree(string userId, string strResourceFolder_ParentId, string strResource_Type, DataTable dt, string tabid)
        {
            List<object> listObj = new List<object>();
            string strWhere = string.Empty;
            if (strResourceFolder_ParentId == "0")
            {
                strWhere = string.Format(" ResourceFolder_ParentId='{0}'", "0");
            }
            else
            {
                strWhere = string.Format(" ResourceFolder_ParentId='{0}'", strResourceFolder_ParentId);
            }

            //List<Rc.Model.Resources.Model_ResourceFolderForClient> modelList = new List<Rc.Model.Resources.Model_ResourceFolderForClient>();
            DataRow[] dr = dt.Select(strWhere, "ResourceFolder_Order,ResourceFolder_Name");
            int rowsCount = dr.Length;
            if (rowsCount > 0)
            {
                //Rc.Model.Resources.Model_ResourceFolderForClient model;
                //for (int n = 0; n < rowsCount; n++)
                //{
                //    model = DataRowToModel(dr[n]);
                //    if (model != null)
                //    {
                //        listObj.Add(new
                //        {
                //            ResourceFolder_Id = model.ResourceFolder_Id,
                //            ResourceFolder_Name = model.ResourceFolder_Name.ReplaceForFilter(),
                //            list = GetTeacherCLPResourceFolderTree(userId, model.ResourceFolder_Id, strResource_Type, dt, tabid)
                //        });
                //    }
                //}
                foreach (DataRow item in dr)
                {
                    listObj.Add(new
                    {
                        ResourceFolder_Id = item["ResourceFolder_Id"].ToString(),
                        ResourceFolder_Name = item["ResourceFolder_Name"].ToString().ReplaceForFilter(),
                        list = GetTeacherCLPResourceFolderTree(userId, item["ResourceFolder_Id"].ToString(), strResource_Type, dt, tabid)
                    });
                }
                return listObj;
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// 搜索功能 获取搜索数据列表2016-06-08TS
        /// </summary>
        private string GetSearchResourceList(string keywords, string max, string userId, string strResource_Type, string productType, string tabid, string UserIdentity)
        {
            bool isWritable = false;                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
            bool isPrintable = false;                       // 资源是否可打印，true可打印。
            bool isPrivate = false;                         // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
            bool isSubmited = false;                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
            bool permitDel = true;                          // 资源是否可删除，默认为 true
            bool permitMove = true;                         // 资源是否可移动，默认为 true
            bool permitSave = true;                         // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
            bool permitCopy = true;                         // 资源是否可复制，默认为 true
            bool TheBookWasPaied = true;                    // 是否购买了此资源 ，true 购买了

            List<object> listObj = new List<object>();
            string strJsion = string.Empty;
            StringBuilder strSql = new StringBuilder();
            int intMax = 10;
            int.TryParse(max, out intMax);
            if (UserIdentity == "A")
            {
                strSql.AppendFormat(@"select top {0} * from VW_ResourceAndResourceFolder_Mgr where RType='1' ", intMax);
                strSql.AppendFormat(" and CreateFUser='{0}' and Resource_Type='{1}' and ResourceFolder_Name  like '%{2}%' ", userId, strResource_Type, keywords);
            }
            else if (UserIdentity == "T")
            {
                strSql.AppendFormat(@"select top {0} * from VW_ResourceAndResourceFolder where RType='1'", intMax);
                if (tabid == EnumTabindex.TeacherScienceWordCloudTeachingPlan.ToString() || tabid == EnumTabindex.TeacherClassCloudTeachingPlan.ToString())
                {
                    strSql.AppendFormat(" and Book_Id in(select Book_Id from UserBuyResources where UserId='{0}') ", userId);
                }
                else
                {
                    strSql.AppendFormat(" and CreateFUser='{0}' ", userId);
                }
                strSql.AppendFormat(" and Resource_Type ='{0}'  and ResourceFolder_Name  like '%{1}%' ", strResource_Type, keywords);
            }
            else if (UserIdentity == "S")
            {
                strSql.AppendFormat("select top {0} SHW.Student_HomeWork_Id as ResourceFolder_Id,SHW.CreateTime,HW.HomeWork_Name as ResourceFolder_Name,1 AS RType,NULL AS Book_ID,HW.HomeWork_AssignTeacher,HW.BeginTime,HW.StopTime,RTRF.ResourceToResourceFolder_Id as ResourceFolder_Id,RTRF.ResourceToResourceFolder_Order as ResourceFolder_Order,RTRF.ResourceFolder_Id as ResourceFolder_ParentId", intMax);
                strSql.Append(",RTRF.File_Name,RTRF.File_Owner,REPLACE(RTRF.File_Suffix,'.','') File_Suffix,RTRF.Resource_Class,RTRF.Resource_Type,RTRF.CreateFUser,RTRF.Resource_Version,ISNULL(UBR.Book_ID,'-1') AS TheBookWasPaied from dbo.Student_HomeWork SHW");
                strSql.Append(" inner join HomeWork HW on SHW.HomeWork_Id=HW.HomeWork_Id");
                strSql.Append(" inner join Student_HomeWork_Submit shwSubmit on shwSubmit.Student_HomeWork_Id=SHW.Student_HomeWork_Id");
                strSql.Append(" inner join ResourceToResourceFolder RTRF on RTRF.ResourceToResourceFolder_Id=HW.ResourceToResourceFolder_Id");
                strSql.AppendFormat(" LEFT JOIN UserBuyResources UBR ON RTRF.Book_ID=UBR.Book_id AND UBR.UserId='{0}'", userId);
                //strSql.AppendFormat(" where 1=1");
                strSql.AppendFormat(" where RTRF.File_Name like '%{0}%' and ((HW.IsHide=1 AND HW.BeginTime<=GETDATE()) OR HW.IsHide=0 )", keywords);
                if (tabid == EnumTabindex.StudentSkillSubminted.ToString())//已完成作业
                {
                    strSql.AppendFormat(" and shwSubmit.Student_HomeWork_Status='1' ");
                    strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                    isWritable = false;                     // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                    // 资源是否可打印，true可打印。
                    isPrivate = true;                       // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = true;                      // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                      // 资源是否可删除，默认为 true
                    permitMove = false;                     // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = false;                     // 资源是否可复制，默认为 true
                }
                else if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业
                {
                    isWritable = false;                     // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = false;                    // 资源是否可打印，true可打印。
                    isPrivate = false;                      // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = false;                     // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = false;                      // 资源是否可删除，默认为 true
                    permitMove = false;                     // 资源是否可移动，默认为 true
                    permitSave = false;                     // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = false;                     // 资源是否可复制，默认为 true
                    strSql.AppendFormat(" and shwSubmit.Student_HomeWork_Status='0' ");
                    strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                }
                else if (tabid == EnumTabindex.StudentSkillWrong.ToString())//错题集
                {
                    strSql.AppendFormat(@" and HW.HomeWork_Id in (SELECT DISTINCT t1.HomeWork_Id FROM Student_HomeWorkAnswer t1
INNER JOIN Student_WrongHomeWork  t2 ON t1.Student_HomeWorkAnswer_Id=t2.Student_HomeWorkAnswer_Id
WHERE t1.Student_Id='{0}')", userId);
                    strSql.AppendFormat(" and SHW.Student_Id='{0}' order by SHW.CreateTime desc ", userId);
                }
                else if (tabid == EnumTabindex.StudentClassMicroClass.ToString())//学生微课件
                {
                    strSql = new StringBuilder();
                    strSql.AppendFormat(@"select top {0} * from VW_ResourceAndResourceFolder where RType='1'", intMax);
                    strSql.AppendFormat(" and Book_Id in(select Book_Id from UserBuyResources where UserId='{0}') ", userId);
                    strSql.AppendFormat(" and Resource_Type ='{0}'  and ResourceFolder_Name  like '%{1}%' ", strResource_Type, keywords);
                    strSql.Append(" order by ResourceFolder_Order,ResourceFolder_Name ");
                }
            }

            DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(strSql.ToString()).Tables[0];
            Rc.Cloud.Model.Model_Struct_Func UserFun;
            string Module_Id = "10100100"; //生产任务分配
            Rc.Cloud.Model.Model_VSysUserRole loginModel = new Rc.Cloud.BLL.BLL_VSysUserRole().GetSysUserInfoModelBySysUserId(userId);
            UserFun = new Rc.Cloud.BLL.BLL_clsAuth().GetUserFunc(userId, (loginModel == null ? "''" : clsUtility.ReDoStr(loginModel.SysRole_IDs, ',')), Module_Id);

            #region 学校自有资源权限控制
            BookAttrModel bkAttrModel_School = GetBookAttrValue_SchoolByUserId(userId);
            isPrintable = bkAttrModel_School.IsPrint;
            permitSave = bkAttrModel_School.IsSave;
            permitCopy = bkAttrModel_School.IsCopy;
            #endregion

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strNoPaiedDesc = string.Empty;
                if (productType == "skill")
                {
                    if (dt.Rows[i]["TheBookWasPaied"].ToString().Trim() == "-1")
                    {
                        TheBookWasPaied = false;
                        isWritable = false;
                        strNoPaiedDesc = "你尚未购买此练习册，请联系您的老师。";
                    }
                    else
                    {
                        TheBookWasPaied = true;
                        strNoPaiedDesc = "";
                    }
                }
                if (tabid == EnumTabindex.StudentSkillNew.ToString())//最新作业 提交作业截止时间
                {
                    DateTime stopTime = DateTime.Now;
                    DateTime.TryParse(dt.Rows[i]["StopTime"].ToString(), out stopTime);
                    if (stopTime < DateTime.Now)
                    {
                        TheBookWasPaied = false;
                        isWritable = false;
                        strNoPaiedDesc = "已超过作业提交截止日期。";
                    }
                }
                string strFileType = string.Empty;
                string strFileSuffix = string.Empty;
                strFileSuffix = dt.Rows[0]["File_Suffix"].ToString();
                strFileType = dt.Rows[0]["File_Suffix"].ToString();
                if (strFileSuffix == "testPaper")
                {
                    strFileSuffix = "dsc";
                }
                if (strResource_Type == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                {
                    strFileType = "testPaper";
                }

                if (UserIdentity == "A")
                {
                    int inttemp = 0;
                    int.TryParse(dt.Rows[i]["ResourceFolder_Level"].ToString(), out inttemp);
                    isWritable = UserFun.Add;
                    permitDel = UserFun.Delete;
                    permitMove = UserFun.Move;
                    permitSave = UserFun.Edit;
                    permitCopy = UserFun.Copy;

                    if (inttemp < 4 && inttemp != -1)
                    {
                        isWritable = false;
                        permitDel = false;
                        permitMove = false;
                        permitSave = false;
                        permitCopy = false;
                    }

                }
                if (UserIdentity == "T" && dt.Rows[i]["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                {
                    #region 云资源权限控制
                    BookAttrModel bkAttrModel = GetBookAttrValue(dt.Rows[i]["Book_ID"].ToString());
                    isPrintable = bkAttrModel.IsPrint;
                    permitSave = bkAttrModel.IsSave;
                    permitCopy = bkAttrModel.IsCopy;
                    #endregion
                }

                bool boolIsFolder = false;
                //RType=为文件夹
                if (dt.Rows[i]["RType"].ToString() == "0") boolIsFolder = true;

                #region 得到文件下载地址
                string downLoadUrl = string.Empty;
                string strDownLoadFileID = string.Empty;
                string strDownLoadFileType = string.Empty;// 接口使用tabid标识
                string strDownLoadFileLocalInfo = string.Empty;//局域网信息

                strDownLoadFileID = dt.Rows[i]["ResourceFolder_Id"].ToString();
                strDownLoadFileType = tabid;
                if (!string.IsNullOrEmpty(Request["localUrlActive"]))
                {
                    strDownLoadFileLocalInfo = Request["localUrlActive"].ToString();
                }
                // downLoadUrl = pfunction.GetDownLoadFileUrl(strDownLoadFileID, strDownLoadFileType, strDownLoadFileLocalInfo, userId, productType);
                downLoadUrl = string.Format("{0}/AuthApi/?key=downLoadResource&iid={1}&TabId={2}&UserId={3}&ProductType={4}"
                                          , AuthAPI_pfunction.getHostPath(), strDownLoadFileID, strDownLoadFileType, userId, productType);
                #endregion


                listObj.Add(new
                {
                    id = dt.Rows[i]["ResourceFolder_Id"],
                    title = GetSubstringFolderName(dt.Rows[i]["ResourceFolder_Name"].ToString().ReplaceForFilter(), dt.Rows[i]["ResourceFolder_Order"].ToString(), UserIdentity),
                    ParentId = dt.Rows[i]["ResourceFolder_ParentId"],
                    isFolder = boolIsFolder,
                    ext = strFileSuffix,
                    typeId = dt.Rows[i]["Resource_Type"],
                    typeName = "搜索",//GetD_Name(dt.Rows[i]["Resource_Type"].ToString()),
                    fileType = strFileType,
                    dateCreated = AuthAPI_pfunction.ConvertToLongDateTime(dt.Rows[i]["CreateTime"].ToString()),
                    userId = dt.Rows[i]["CreateFUser"],
                    //userName = GetUserNameByUserId(dt.Rows[i]["CreateFUser"].ToString()),
                    isWritable = isWritable,                        // 资源是否可改写，如果不可改写则不能向服务器端保存，只能另存文档到本地磁盘。
                    isPrintable = isPrintable,                      // 资源是否可打印，true可打印。
                    isPrivate = isPrivate,                          // 资源是否是私有资源，(true)私有资源只能在Reader(skill/ClassPlayer)版中打开。
                    isSubmited = isSubmited,                        // 资源是否是显示答题结果，true: 双击打开资源则显示答题结果的报表。
                    permitDel = permitDel,                          // 资源是否可删除，默认为 true
                    permitMove = permitMove,                        // 资源是否可移动，默认为 true
                    permitSave = permitSave,                        // 资源是否可保存（class中的保存、另存为、导出为PDF；scienceWord中的保存、另存为、导出），默认为 true
                    permitCopy = permitCopy,                        // 资源是否可复制，默认为 true
                    wasPaied = TheBookWasPaied,                     // 是否购买了此资源，默认为true
                    noPaiedDesc = strNoPaiedDesc,                   // 没购买的提示
                    version = dt.Rows[i]["Resource_Version"],
                    streamUrl = "",
                    downloadUrl = downLoadUrl,
                    visitUrl = ""
                });
            }
            strJsion = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = true,
                errorMsg = "",
                errorCode = "",
                list = listObj
            });
            return strJsion;
        }

        /// <summary>
        /// 客户端 验证用户是否登录(token是否有效)
        /// </summary>
        private Model_F_User_Client checkTokenIsValidBackModel(string userId, string token, string product_type)
        {
            try
            {
                return new BLL_F_User_Client().GetUserModelByClientToken(userId, token, product_type);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 客户端 用户登录
        /// </summary>
        private Model_F_User_Client userLoginBackModel(string userName, string pass)
        {
            try
            {
                return new BLL_F_User_Client().GetUserModelByClientLogin(userName, pass);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户名验证用户是否存在F_User/SysUser 17-12-19TS
        /// </summary>
        private Model_F_User_Client GetLoginModelByUesrName(string userName)
        {
            try
            {
                return new BLL_F_User_Client().GetUserModelByClientLogin(userName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 第三方用户根据用户名获取用户实体 17-12-19TS
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private Model_F_User_Client TPUserLoginBackModelByUserName(string userName)
        {
            try
            {
                return new BLL_F_User_Client().GetTPUserModelByClientLogin(userName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Rc.Model.Resources.Model_ResourceFolderForClient DataRowToModel(DataRow row)
        {
            Rc.Model.Resources.Model_ResourceFolderForClient model = new Rc.Model.Resources.Model_ResourceFolderForClient();
            if (row != null)
            {
                if (row["ResourceFolder_Id"] != null)
                {
                    model.ResourceFolder_Id = row["ResourceFolder_Id"].ToString();
                }
                if (row["ResourceFolder_ParentId"] != null)
                {
                    model.ResourceFolder_ParentId = row["ResourceFolder_ParentId"].ToString();
                }
                if (row["ResourceFolder_Name"] != null)
                {
                    model.ResourceFolder_Name = row["ResourceFolder_Name"].ToString();
                }
                if (row["ResourceFolder_Order"] != null)
                {
                    model.ResourceFolder_Order = row["ResourceFolder_Order"].ToString();
                }
                if (row["isStore_Files"] != null && row["isStore_Files"].ToString() != "")
                {
                    model.isStore_Files = row["isStore_Files"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 获取云资源属性（是否可打印，存盘等）
        /// </summary>
        /// <param name="ResourceFolder_Id"></param>
        /// <returns></returns>
        public BookAttrModel GetBookAttrValue(string ResourceFolder_Id)
        {
            ResourceFolder_Id = ResourceFolder_Id.Filter();
            BookAttrModel model = new BookAttrModel();
            model.IsPrint = false;  // 默认是不允许打印
            model.IsSave = false;   // 默认是不允许存盘
            model.IsCopy = true;    // 默认是允许复制
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
                    else if (item.AttrEnum == BookAttrEnum.Copy.ToString() && item.AttrValue == "0")
                    {
                        model.IsCopy = false;
                    }
                }
            }
            catch (Exception)
            {

            }
            return model;
        }

        /// <summary>
        /// 获取学校自有资源属性（是否可打印，存盘等）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public BookAttrModel GetBookAttrValue_SchoolByUserId(string UserId)
        {
            UserId = UserId.Filter();
            BookAttrModel model = new BookAttrModel();
            model.IsPrint = true;   // 默认是允许打印
            model.IsSave = true;    // 默认是允许存盘
            model.IsCopy = true;    // 默认是允许复制
            try
            {
                List<Model_BookAttr_School> listModel = new BLL_BookAttr_School().GetModelListByCache(UserId);
                foreach (var item in listModel)
                {
                    if (item.AttrEnum == BookAttrEnum.Print.ToString() && item.AttrValue == "0")
                    {
                        model.IsPrint = false;
                    }
                    else if (item.AttrEnum == BookAttrEnum.Save.ToString() && item.AttrValue == "0")
                    {
                        model.IsSave = false;
                    }
                    else if (item.AttrEnum == BookAttrEnum.Copy.ToString() && item.AttrValue == "0")
                    {
                        model.IsCopy = false;
                    }
                }
            }
            catch (Exception)
            {

            }
            return model;
        }

        public string GetSubstringFolderName(string folderName, string folderOrder, string UserIdentity)
        {
            try
            {
                if (UserIdentity == "A")//管理员
                {
                    int order = 0;
                    int.TryParse(folderOrder, out order);
                    if (order > 0)
                    {
                        string strOrder = "";
                        strOrder = "00" + order.ToString();
                        strOrder = strOrder.Substring(strOrder.Length - 3, 3);
                        folderName = string.Format("{0}-{1}", strOrder, folderName);
                    }
                    return folderName;
                }
                else
                {
                    return folderName;
                }
            }
            catch (Exception)
            {
                return folderName;
            }
        }

        /// <summary>
        /// 学生打开作业
        /// </summary>
        public string GetTestPaperFileForStudent(string Student_HomeWork_Id, string userId, string tabid)
        {
            string result = string.Empty;
            try
            {
                string uploadPath = "..\\Upload\\Resource\\studentPaper\\";//存储文件基础路径
                string rtrfId = string.Empty;

                Student_HomeWork_Id = Student_HomeWork_Id.Filter();
                #region 获取JSON结构
                //学生答题状态为未答题，打开试卷时更新‘打开时间’
                BLL_Student_HomeWork bllSHW = new BLL_Student_HomeWork();
                Model_Student_HomeWork modelSHW = bllSHW.GetModel(Student_HomeWork_Id);
                BLL_Student_HomeWork_Submit bllshwSubmit = new BLL_Student_HomeWork_Submit();
                Model_Student_HomeWork_Submit mdoelshwSubmit = bllshwSubmit.GetModel(Student_HomeWork_Id);
                mdoelshwSubmit.OpenTime = DateTime.Now;
                mdoelshwSubmit.StudentIP = AuthAPI_pfunction.GetRealIP();
                if (mdoelshwSubmit.Student_HomeWork_Status == 0) bllshwSubmit.Update(mdoelshwSubmit);
                string HomeWork_Id = modelSHW.HomeWork_Id;
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(modelSHW.HomeWork_Id);
                rtrfId = modelHW.ResourceToResourceFolder_Id;
                string strHomeWork_Name = modelHW.HomeWork_Name;

                string filePath = AuthAPI_pfunction.ConvertToLongDateTime(modelHW.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + HomeWork_Id + ".txt";
                if (File.Exists(Server.MapPath(uploadPath) + filePath))
                {
                    result = AuthAPI_pfunction.ReadAllText(Server.MapPath(uploadPath) + filePath);
                    //如果作业文件小于50KB，则重新生成作业文件
                    if (result.Length < 50 * 1024)
                    {
                        result = string.Empty;
                        File.Delete(Server.MapPath(uploadPath) + filePath);
                    }
                }

                if (string.IsNullOrEmpty(result))
                {
                    BLL_HW_TestPaper bllHWTP = new BLL_HW_TestPaper();
                    Model_HW_TestPaper modelHWTP = new Model_HW_TestPaper();
                    modelHWTP = bllHWTP.GetModel(HomeWork_Id);
                    if (modelHWTP == null)
                    {
                        modelHWTP = new Model_HW_TestPaper();
                        modelHWTP.HW_TestPaper_Id = HomeWork_Id;
                        modelHWTP.TestPaper_Path = uploadPath + filePath;
                        modelHWTP.TestPaper_Status = "0";
                        modelHWTP.CreateTime = DateTime.Now;
                        bllHWTP.Add(modelHWTP);

                        GenerateTestPaperFileForStudent_API(HomeWork_Id);

                        modelHWTP.TestPaper_Status = "1";
                        bllHWTP.Update(modelHWTP);

                        result = AuthAPI_pfunction.ReadAllText(Server.MapPath(uploadPath) + filePath);
                    }
                    else
                    {
                        if (modelHWTP.TestPaper_Status == "0")
                        {
                            return "generating";
                        }
                        if (!File.Exists(Server.MapPath(uploadPath) + filePath))
                        {
                            GenerateTestPaperFileForStudent_API(HomeWork_Id);
                            result = File.ReadAllText(Server.MapPath(uploadPath) + filePath);
                        }
                        else
                        {
                            result = AuthAPI_pfunction.ReadAllText(Server.MapPath(uploadPath) + filePath);
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("学生打开作业失败：{0}", ex.Message.ToString()));
                result = "error";
            }
            return result;
        }
        /// <summary>
        /// 生成学生作业txt 17-07-17TS
        /// </summary>
        private void GenerateTestPaperFileForStudent_API(string HomeWork_Id)
        {
            try
            {
                string uploadPath = "..\\Upload\\Resource\\studentPaper\\";//存储文件基础路径
                string strTestWebSiteUrl = ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl");//学校局域网地址
                Model_HomeWork modelHW = new BLL_HomeWork().GetModel(HomeWork_Id);
                string rtrfId = modelHW.ResourceToResourceFolder_Id;
                string strHomeWork_Name = modelHW.HomeWork_Name;
                int isTimeLimt = 0;
                int isTimeLength = 0;
                bool isShowAnswer = false;
                int.TryParse(modelHW.isTimeLimt.ToString(), out isTimeLimt);
                int.TryParse(modelHW.isTimeLength.ToString(), out isTimeLength);
                if (modelHW.IsShowAnswer == 1)
                {
                    isShowAnswer = true;
                }
                string filePath = AuthAPI_pfunction.ConvertToLongDateTime(modelHW.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + HomeWork_Id + ".txt";
                string filePathForTch = AuthAPI_pfunction.ConvertToLongDateTime(modelHW.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + HomeWork_Id + ".tch.txt";
                List<object> listTQObjForTch = new List<object>();//老师客户端批改
                List<object> listTQObj = new List<object>();//学生客户端答题

                List<object> listTQObjForTchBig = new List<object>();//老师客户端批改
                List<object> listTQObjBig = new List<object>();//学生客户端答题

                strTestWebSiteUrl += "/Upload/Resource/";

                #region 试题
                //试题数据
                string strSqlTQ = string.Format(@"select  t1.TestQuestions_Id,t1.TestQuestions_Num,t1.TestQuestions_Type,t1.Parent_Id,t1.[type],t1.topicNumber
,T2.Resource_Version,T2.GradeTerm,t2.Subject,t2.ParticularYear,t2.Resource_Class,t2.CreateTime,t2.LessonPlan_Type
FROM TestQuestions t1 inner join ResourceToResourceFolder t2 ON t1.ResourceToResourceFolder_ID=T2.ResourceToResourceFolder_ID 
where  t1.ResourceToResourceFolder_Id='{0}' order by TestQuestions_Num ", rtrfId);
                DataTable dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];

                //获取这个试卷的所有试题分值
                string strSqlScore = string.Empty;
                strSqlScore = string.Format(@"select  [TestQuestions_Score_ID]
      ,[ResourceToResourceFolder_Id]
      ,[TestQuestions_Id]
      ,[TestCorrect]
      ,[AnalyzeHyperlinkData]
      ,[TrainHyperlinkData]
      ,[TestType]
      ,[ScoreText],[testIndex],[TestType]
,TestQuestions_OrderNum FROM TestQuestions_Score where ResourceToResourceFolder_Id='{0}' order by TestQuestions_OrderNum ", rtrfId);
                DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];

                #region 普通题型 list
                DataRow[] drList = dtTQ.Select("Parent_Id='' ", "TestQuestions_Num");
                foreach (DataRow item in drList)
                {
                    string savePath = string.Empty;
                    string saveOwnerPath = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPath = string.Format("{0}\\", AuthAPI_pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    DataRow drTQ_S = null;
                    DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", item["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                    #region 试题分数
                    List<object> listTQ_SObjForTch = new List<object>();//老师客户端批改
                    List<object> listTQ_SObj = new List<object>();//学生客户端答题
                    int intIndex = 0;
                    for (int j = 0; j < drTQ_Score.Length; j++)
                    {

                        drTQ_S = drTQ_Score[j];
                        string strAnalyzeUrl = string.Empty;
                        string strTrainUrl = string.Empty;
                        if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                        {
                            strAnalyzeUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["AnalyzeHyperlinkData"].ToString());
                        }
                        if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                        {
                            strTrainUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["TrainHyperlinkData"].ToString());
                        }
                        listTQ_SObj.Add(new
                        {
                            testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                            testIndex = drTQ_S["testIndex"].ToString(),
                            analyzeUrl = strAnalyzeUrl,
                            trainUrl = strTrainUrl
                        });

                        string strtestCorrectBase64 = string.Empty;
                        string strtestCorrect = string.Empty;
                        switch (drTQ_Score[j]["TestType"].ToString())
                        {
                            case "selection":
                            case "clozeTest":
                            case "truefalse":
                                strtestCorrect = drTQ_Score[j]["TestCorrect"].ToString();
                                break;
                            //case "fill":
                            //case "answers":
                            //    strtestCorrectBase64 = RemotWeb.PostDataToServer(strTestWebSiteUrl + saveOwnerPath + "testQuestionCurrent\\" + savePath + drTQ_Score[j]["TestQuestions_Score_ID"].ToString() + ".txt", "", Encoding.UTF8, "Get");
                            //    break;
                        }
                        listTQ_SObjForTch.Add(new
                        {
                            testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                            testIndex = drTQ_S["testIndex"].ToString(),
                            scoreText = drTQ_Score[j]["ScoreText"].ToString(),
                            testCorrect = strtestCorrect
                        });
                    }
                    //if (listTQ_SObjForTch.Count == 0)
                    //{
                    //    listTQ_SObjForTch.Add(null);
                    //}
                    #endregion
                    string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                    if (drTQ_S != null)
                    {
                        string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObj
                        });

                        if (drTQ_S["TestType"].ToString() == "selection" || drTQ_S["TestType"].ToString() == "clozeTest" || drTQ_S["TestType"].ToString() == "truefalse")
                        {
                            strtestQuestionBody = "";
                            strtextTitle = "";
                        }
                        listTQObjForTch.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObjForTch
                        });
                    }
                    else
                    {
                        string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = ""
                        });

                        listTQObjForTch.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = strtestQuestionBody,
                            textTitle = strtextTitle,
                            list = listTQ_SObjForTch
                        });
                    }
                }
                #endregion
                #region 综合题型 listBig
                DataRow[] drListBig = dtTQ.Select("Parent_Id='0' ", "TestQuestions_Num");
                foreach (DataRow item in drListBig)
                {
                    List<object> listTQObjForTchBig_Sub = new List<object>();
                    List<object> listTQObjBig_Sub = new List<object>();
                    DataRow[] drBig_Sub = dtTQ.Select("Parent_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_Num");
                    foreach (DataRow itemSub in drBig_Sub)
                    {
                        string savePath = string.Empty;
                        string saveOwnerPath = string.Empty;
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", itemSub["ParticularYear"].ToString(), itemSub["GradeTerm"],
                                itemSub["Resource_Version"].ToString(), itemSub["Subject"].ToString());
                        }
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                        {
                            saveOwnerPath = string.Format("{0}\\", AuthAPI_pfunction.ConvertToLongDateTime(itemSub["CreateTime"].ToString(), "yyyy-MM-dd"));
                        }
                        DataRow drTQ_S = null;
                        DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", itemSub["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                        #region 试题分数
                        List<object> listTQ_SObjForTch = new List<object>();//老师客户端批改 分值，正确答案
                        List<object> listTQ_SObj = new List<object>();//学生客户端答题 解析，强化训练
                        int intIndex = 0;
                        for (int j = 0; j < drTQ_Score.Length; j++)
                        {

                            drTQ_S = drTQ_Score[j];
                            string strAnalyzeUrl = string.Empty;
                            string strTrainUrl = string.Empty;
                            if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                            {
                                strAnalyzeUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["AnalyzeHyperlinkData"].ToString());
                            }
                            if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                            {
                                strTrainUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["TrainHyperlinkData"].ToString());
                            }
                            listTQ_SObj.Add(new
                            {
                                testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                                testIndex = drTQ_S["testIndex"].ToString(),
                                analyzeUrl = strAnalyzeUrl,
                                trainUrl = strTrainUrl
                            });

                            string strtestCorrectBase64 = string.Empty;
                            string strtestCorrect = string.Empty;
                            switch (drTQ_Score[j]["TestType"].ToString())
                            {
                                case "selection":
                                case "clozeTest":
                                case "truefalse":
                                    strtestCorrect = drTQ_Score[j]["TestCorrect"].ToString();
                                    break;
                                //case "fill":
                                //case "answers":
                                //    strtestCorrectBase64 = RemotWeb.PostDataToServer(strTestWebSiteUrl + saveOwnerPath + "testQuestionCurrent\\" + savePath + drTQ_Score[j]["TestQuestions_Score_ID"].ToString() + ".txt", "", Encoding.UTF8, "Get");
                                //    break;
                            }
                            listTQ_SObjForTch.Add(new
                            {
                                testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                                testIndex = drTQ_S["testIndex"].ToString(),
                                scoreText = drTQ_Score[j]["ScoreText"].ToString(),
                                testCorrect = strtestCorrect
                            });
                        }
                        if (listTQ_SObjForTch.Count == 0)
                        {
                            listTQ_SObjForTch.Add(null);
                        }
                        #endregion
                        string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                        if (drTQ_S != null)
                        {
                            string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObj
                            });

                            if (drTQ_S["TestType"].ToString() == "selection" || drTQ_S["TestType"].ToString() == "clozeTest" || drTQ_S["TestType"].ToString() == "truefalse")
                            {
                                strtestQuestionBody = "";
                                strtextTitle = "";
                            }
                            listTQObjForTchBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObjForTch
                            });
                        }
                        else
                        {
                            string strtestQuestionBody = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strtextTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get");
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = ""
                            });

                            listTQObjForTchBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = strtestQuestionBody,
                                textTitle = strtextTitle,
                                list = listTQ_SObjForTch
                            });
                        }
                    }
                    string savePathBig = string.Empty;
                    string saveOwnerPathBig = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePathBig = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPathBig = string.Format("{0}\\", AuthAPI_pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    string fileUrlBig = strTestWebSiteUrl + saveOwnerPathBig + "{0}\\" + savePathBig + "{1}.{2}";//自有资源 savePath为空，云资源saveOwnerPath为空
                    string strdocBase64 = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    string strdocHtml = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "htm"), "", Encoding.UTF8, "Get");
                    string textTitle = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "textTitle", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    listTQObjBig.Add(new
                    {
                        docBase64 = strdocBase64,
                        docHtml = strdocHtml,
                        textTitle = textTitle,
                        topicNumber = (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷) ? item["topicNumber"].ToString() : "",
                        list = listTQObjBig_Sub,
                        type = item["type"].ToString()
                    });
                    if (drBig_Sub.Length > 0) //没有子级试题，不加载节点
                    {
                        listTQObjForTchBig.Add(new
                        {
                            docBase64 = strdocBase64,
                            docHtml = strdocHtml,
                            textTitle = textTitle,
                            list = listTQObjForTchBig_Sub,
                            type = item["type"].ToString()
                        });
                    }

                }
                #endregion

                #endregion
                string strJson = string.Empty;
                strJson = JsonConvert.SerializeObject(new
                {
                    status = true,
                    errorMsg = "",
                    errorCode = "",
                    paperHeaderDoc = GetPaperHeaderDoc(rtrfId),
                    testPaperName = "",
                    isTimeLimt = isTimeLimt,
                    isTimeLength = isTimeLength,
                    sysTime = DateTime.Now.ToString(),
                    isShowAnswerAfterSubmiting = isShowAnswer,
                    list = listTQObj,
                    listBig = listTQObjBig
                });
                AuthAPI_pfunction.WriteToFile(HttpContext.Current.Server.MapPath(uploadPath) + filePath, strJson, true);

                string strJsonForTch = string.Empty;
                strJsonForTch = JsonConvert.SerializeObject(new
                {
                    list = listTQObjForTch,
                    listBig = listTQObjForTchBig
                });
                AuthAPI_pfunction.WriteToFile(HttpContext.Current.Server.MapPath(uploadPath) + filePathForTch, strJsonForTch, true);

            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("生成学生作业失败：{0}", ex.Message.ToString()));
            }
        }

        /// <summary>
        /// 老师打开作业
        /// </summary>
        public string GetTestPaperFileForTeacher(string ResourceToResourceFolder_ID, string userId)
        {
            string result = string.Empty;
            string rtrfId = string.Empty;
            rtrfId = ResourceToResourceFolder_ID.Filter();
            try
            {
                string uploadPath = "..\\Upload\\Resource\\teacherPaper\\";//存储文件基础路径               
                #region 获取JSON结构
                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                string filePath = AuthAPI_pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + rtrfId + ".txt";
                string basicFilePath = Server.MapPath(uploadPath) + filePath;
                if (File.Exists(basicFilePath))
                {
                    DataTable dtSyncData = new BLL_SyncData().GetList("TableName='ResourceToResourceFolder' and DataId='" + ResourceToResourceFolder_ID + "' order by CreateTime desc").Tables[0];
                    FileInfo testPaperFI = new FileInfo(basicFilePath);
                    result = AuthAPI_pfunction.ReadAllText(basicFilePath);
                    // 如果作业文件小于50KB，则重新生成作业文件
                    // 如果作业文件创建时间小于试卷修改时间，则重新生成作业文件
                    if (result.Length < 50 * 1024 ||
                        (dtSyncData.Rows.Count > 0 && testPaperFI.CreationTime < Convert.ToDateTime(dtSyncData.Rows[0]["CreateTime"])))
                    {
                        result = string.Empty;
                        File.Delete(basicFilePath);
                    }
                }

                if (string.IsNullOrEmpty(result))
                {
                    BLL_HW_TestPaper bllHWTP = new BLL_HW_TestPaper();
                    Model_HW_TestPaper modelHWTP = new Model_HW_TestPaper();
                    modelHWTP = bllHWTP.GetModel(rtrfId);
                    if (modelHWTP == null)
                    {
                        modelHWTP = new Model_HW_TestPaper();
                        modelHWTP.HW_TestPaper_Id = rtrfId;
                        modelHWTP.TestPaper_Path = uploadPath + filePath;
                        modelHWTP.TestPaper_Status = "0";
                        modelHWTP.CreateTime = DateTime.Now;
                        bllHWTP.Add(modelHWTP);

                        GenerateTestPaperFileForTeacher_API(rtrfId);

                        modelHWTP.TestPaper_Status = "1";
                        bllHWTP.Update(modelHWTP);

                        result = AuthAPI_pfunction.ReadAllText(basicFilePath);
                    }
                    else if (modelHWTP != null && Convert.ToDateTime(modelHWTP.CreateTime).AddMinutes(2) < DateTime.Now)
                    {
                        GenerateTestPaperFileForTeacher_API(rtrfId);

                        modelHWTP.CreateTime = DateTime.Now;
                        modelHWTP.TestPaper_Status = "1";
                        bllHWTP.Update(modelHWTP);
                    }
                    else
                    {
                        result = GetTestPaperFileForTeacherSub(modelHWTP, uploadPath, filePath, rtrfId);

                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("老师打开作业失败。{0}", ex.Message.ToString()));
                //删除作业文件生成记录
                new BLL_HW_TestPaper().Delete(rtrfId);
                result = "error";
            }
            return result;
        }

        private string GetTestPaperFileForTeacherSub(Model_HW_TestPaper modelHWTP, string uploadPath, string filePath, string rtrfId)
        {
            string result = string.Empty;
            if (HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] == null)
            {
                HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] = 1;
            }
            else
            {
                HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] = (int)HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] + 1;
            }

            if (modelHWTP.TestPaper_Status == "0")
            {
                if ((int)HttpContext.Current.Session["reqGetTestPaperFileForTeacherSubCount"] < 20)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return result;
                }
                result = GetTestPaperFileForTeacherSub(modelHWTP, uploadPath, filePath, rtrfId);
            }
            if (!File.Exists(Server.MapPath(uploadPath) + filePath))
            {
                GenerateTestPaperFileForTeacher_API(rtrfId);
                result = AuthAPI_pfunction.ReadAllText(Server.MapPath(uploadPath) + filePath);
            }
            else
            {
                result = AuthAPI_pfunction.ReadAllText(Server.MapPath(uploadPath) + filePath);
            }
            return result;
        }
        /// <summary>
        /// 生成老师作业txt 17-07-17TS
        /// </summary>
        private void GenerateTestPaperFileForTeacher_API(string ResourceToResourceFolder_ID)
        {
            try
            {
                string uploadPath = "..\\Upload\\Resource\\teacherPaper\\";//存储文件基础路径
                string strTestWebSiteUrl = ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl");//学校局域网地址
                string rtrfId = ResourceToResourceFolder_ID;
                Model_ResourceToResourceFolder modelRTRF = new BLL_ResourceToResourceFolder().GetModel(rtrfId);
                strTestWebSiteUrl += "/Upload/Resource/";
                string filePath = AuthAPI_pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd") + "\\" + rtrfId + ".txt";

                List<object> listTQObj = new List<object>();
                List<object> listTQObjBig = new List<object>();



                #region 试题
                //试题数据
                string strSqlTQ = string.Format(@"select  t1.TestQuestions_Id,t1.TestQuestions_Num,t1.TestQuestions_Type,t1.Parent_Id,t1.[type],t1.topicNumber
,T2.Resource_Version,T2.GradeTerm,t2.Subject,t2.ParticularYear,t2.Resource_Class,t2.CreateTime,t2.LessonPlan_Type
FROM TestQuestions t1 inner join ResourceToResourceFolder t2 ON t1.ResourceToResourceFolder_ID=T2.ResourceToResourceFolder_ID 
where  t1.ResourceToResourceFolder_Id='{0}' order by TestQuestions_Num ", rtrfId);
                DataTable dtTQ = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlTQ).Tables[0];

                //获取这个试卷的所有试题分值
                string strSqlScore = string.Empty;
                strSqlScore = string.Format(@"select  [TestQuestions_Score_ID]
      ,[ResourceToResourceFolder_Id]
      ,[TestQuestions_Id]
      ,[TestCorrect]
      ,[AnalyzeHyperlinkData]
      ,[TrainHyperlinkData],[testIndex],[TestType]
,TestQuestions_OrderNum FROM TestQuestions_Score where ResourceToResourceFolder_Id='{0}' order by TestQuestions_OrderNum ", rtrfId);
                DataTable dtTQ_Score = Rc.Common.DBUtility.DbHelperSQL.Query(strSqlScore).Tables[0];

                #region 普通题型 list
                DataRow[] drList = dtTQ.Select("Parent_Id='' ", "TestQuestions_Num");
                foreach (DataRow item in drList)
                {
                    string savePath = string.Empty;
                    string saveOwnerPath = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPath = string.Format("{0}\\"
                            , AuthAPI_pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd"));
                    }
                    DataRow drTQ_S = null;
                    DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", item["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                    #region 试题分数
                    List<object> listTQ_SObj = new List<object>();
                    int intIndex = 0;
                    for (int j = 0; j < drTQ_Score.Length; j++)
                    {
                        drTQ_S = drTQ_Score[j];
                        string strAnalyzeUrl = string.Empty;
                        string strTrainUrl = string.Empty;
                        if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                        {
                            strAnalyzeUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["AnalyzeHyperlinkData"].ToString());
                        }
                        if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                        {
                            strTrainUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["TrainHyperlinkData"].ToString());
                        }
                        listTQ_SObj.Add(new
                        {
                            testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                            testIndex = drTQ_S["testIndex"].ToString(),
                            analyzeUrl = strAnalyzeUrl,
                            trainUrl = strTrainUrl
                        });
                    }
                    #endregion
                    string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                    if (drTQ_S != null)
                    {
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            list = listTQ_SObj
                        });
                    }
                    else
                    {
                        string strTopicNumber = string.Empty;
                        if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                        {
                            strTopicNumber = item["topicNumber"].ToString();
                        }
                        listTQObj.Add(new
                        {
                            Testid = item["TestQuestions_Id"],
                            testType = item["TestQuestions_Type"],
                            topicNumber = strTopicNumber,
                            docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                            list = ""
                        });
                    }
                }
                #endregion
                #region 综合题型 listBig
                DataRow[] drListBig = dtTQ.Select("Parent_Id='0' ", "TestQuestions_Num");
                foreach (DataRow item in drListBig)
                {
                    List<object> listTQObjBig_Sub = new List<object>();
                    DataRow[] drBig_Sub = dtTQ.Select("Parent_Id='" + item["TestQuestions_Id"] + "'", "TestQuestions_Num");
                    foreach (DataRow itemSub in drBig_Sub)
                    {
                        string savePath = string.Empty;
                        string saveOwnerPath = string.Empty;
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                        {
                            savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", itemSub["ParticularYear"].ToString(), itemSub["GradeTerm"],
                                itemSub["Resource_Version"].ToString(), itemSub["Subject"].ToString());
                        }
                        if (itemSub["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                        {
                            saveOwnerPath = string.Format("{0}\\"
                                , AuthAPI_pfunction.ConvertToLongDateTime(modelRTRF.CreateTime.ToString(), "yyyy-MM-dd"));
                        }
                        DataRow drTQ_S = null;
                        DataRow[] drTQ_Score = dtTQ_Score.Select(string.Format("TestQuestions_ID='{0}'", itemSub["TestQuestions_ID"].ToString()), "TestQuestions_OrderNum");

                        #region 试题分数
                        List<object> listTQ_SObj = new List<object>();
                        int intIndex = 0;
                        for (int j = 0; j < drTQ_Score.Length; j++)
                        {
                            drTQ_S = drTQ_Score[j];
                            string strTestIndex = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷
                                && drTQ_S["TestType"].ToString() == "clozeTest")
                            {
                                intIndex++;
                                strTestIndex = intIndex + ".";
                            }
                            string strAnalyzeUrl = string.Empty;
                            string strTrainUrl = string.Empty;
                            if (!string.IsNullOrEmpty(drTQ_S["AnalyzeHyperlinkData"].ToString()))
                            {
                                strAnalyzeUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["AnalyzeHyperlinkData"].ToString());
                            }
                            if (!string.IsNullOrEmpty(drTQ_S["TrainHyperlinkData"].ToString()))
                            {
                                strTrainUrl = AuthAPI_pfunction.GetAnalyzeTrainUrl(ConfigHelper.GetConfigString("StudentAnswerWebSiteUrl"), drTQ_S["TrainHyperlinkData"].ToString());
                            }
                            listTQ_SObj.Add(new
                            {
                                testScoreId = drTQ_S["TestQuestions_Score_ID"].ToString(),
                                testIndex = strTestIndex,
                                analyzeUrl = strAnalyzeUrl,
                                trainUrl = strTrainUrl
                            });
                        }
                        #endregion
                        string fileUrl = strTestWebSiteUrl + saveOwnerPath + "{0}\\" + savePath + "{1}.txt";//自有资源 savePath为空，云资源saveOwnerPath为空
                        if (drTQ_S != null)
                        {
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                list = listTQ_SObj
                            });
                        }
                        else
                        {
                            string strTopicNumber = string.Empty;
                            if (item["LessonPlan_Type"].ToString() == LessonPlan_TypeConst.组卷)
                            {
                                strTopicNumber = itemSub["topicNumber"].ToString();
                            }
                            listTQObjBig_Sub.Add(new
                            {
                                Testid = itemSub["TestQuestions_Id"],
                                testType = itemSub["TestQuestions_Type"],
                                topicNumber = strTopicNumber,
                                docBase64 = RemotWeb.PostDataToServer(string.Format(fileUrl, "testQuestionBody", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                textTitle = RemotWeb.PostDataToServer(string.Format(fileUrl, "textTitle", itemSub["TestQuestions_Id"].ToString()), "", Encoding.UTF8, "Get"),
                                list = ""
                            });
                        }
                    }
                    string savePathBig = string.Empty;
                    string saveOwnerPathBig = string.Empty;
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.云资源)
                    {
                        savePathBig = string.Format("{0}\\{1}\\{2}\\{3}\\", item["ParticularYear"].ToString(), item["GradeTerm"],
                            item["Resource_Version"].ToString(), item["Subject"].ToString());
                    }
                    if (item["Resource_Class"].ToString() == Resource_ClassConst.自有资源)
                    {
                        saveOwnerPathBig = string.Format("{0}\\", AuthAPI_pfunction.ConvertToLongDateTime(item["CreateTime"].ToString(), "yyyy-MM-dd"));
                    }
                    string fileUrlBig = strTestWebSiteUrl + saveOwnerPathBig + "{0}\\" + savePathBig + "{1}.{2}";//自有资源 savePath为空，云资源saveOwnerPath为空
                    string strdocBase64 = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    string strdocHtml = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "testQuestionBody", item["TestQuestions_Id"].ToString(), "htm"), "", Encoding.UTF8, "Get");
                    string textTitle = RemotWeb.PostDataToServer(string.Format(fileUrlBig, "textTitle", item["TestQuestions_Id"].ToString(), "txt"), "", Encoding.UTF8, "Get");
                    listTQObjBig.Add(new
                    {
                        docBase64 = strdocBase64,
                        docHtml = strdocHtml,
                        textTitle = textTitle,
                        list = listTQObjBig_Sub,
                        type = item["type"].ToString()
                    });
                }
                #endregion

                #endregion
                string strJson = string.Empty;
                strJson = JsonConvert.SerializeObject(new
                {
                    status = true,
                    errorMsg = "",
                    errorCode = "",
                    paperHeaderDoc = GetPaperHeaderDoc(rtrfId),
                    testPaperName = "",
                    isTimeLimt = 0,
                    isTimeLength = 0,
                    sysTime = DateTime.Now.ToString(),
                    isShowAnswerAfterSubmiting = false,
                    list = listTQObj,
                    listBig = listTQObjBig
                });
                AuthAPI_pfunction.WriteToFile(HttpContext.Current.Server.MapPath(uploadPath) + filePath, strJson, true);

            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("生成老师作业失败。{0}", ex.Message.ToString()));
            }
        }

        /// <summary>
        /// 获取资源对应文件路径 17-06-07TS
        /// </summary>
        private List<string> GetResourceFile(Model_ResourceToResourceFolder modelRTRF, string uploadPath)
        {
            try
            {
                List<string> listReturn = new List<string>();
                //生成存储路径
                string savePath = string.Empty;
                string savePathOwn = string.Empty;
                if (modelRTRF.Resource_Class == Resource_ClassConst.云资源)
                {
                    savePath = string.Format("{0}\\{1}\\{2}\\{3}\\", modelRTRF.ParticularYear, modelRTRF.GradeTerm,
                       modelRTRF.Resource_Version, modelRTRF.Subject);
                }
                if (modelRTRF.Resource_Class == Resource_ClassConst.自有资源)
                {
                    DateTime dateTime = Convert.ToDateTime(modelRTRF.CreateTime);
                    savePathOwn = string.Format("{0}\\", dateTime.ToString("yyyy-MM-dd"));
                }

                if (modelRTRF.Resource_Type == Rc.Common.Config.Resource_TypeConst.testPaper类型文件)
                {
                    string fileUrl = uploadPath + savePathOwn + "{0}\\" + savePath + "{1}.{2}";
                    #region 习题集文件
                    DataTable dtTQ = new BLL_TestQuestions().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    DataTable dtTQ_Score = new BLL_TestQuestions_Score().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    foreach (DataRow item in dtTQ.Rows)
                    {
                        listReturn.Add(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "testQuestionBody", item["TestQuestions_Id"], "htm"));
                        listReturn.Add(string.Format(fileUrl, "textTitle", item["TestQuestions_Id"], "htm"));
                    }
                    foreach (DataRow item in dtTQ_Score.Rows)
                    {
                        listReturn.Add(string.Format(fileUrl, "testQuestionCurrent", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "testQuestionOption", item["TestQuestions_Score_Id"], "txt"));

                        listReturn.Add(string.Format(fileUrl, "AnalyzeData", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "AnalyzeHtml", item["TestQuestions_Score_Id"], "htm"));

                        listReturn.Add(string.Format(fileUrl, "TrainData", item["TestQuestions_Score_Id"], "txt"));
                        listReturn.Add(string.Format(fileUrl, "TrainHtml", item["TestQuestions_Score_Id"], "htm"));

                        listReturn.Add(string.Format(fileUrl, "bodySub", item["TestQuestions_Score_Id"], "txt"));
                    }
                    #endregion
                }
                else
                {
                    #region 教案文件
                    string filePath = string.Empty;//文件存储路径
                    //判断产品类型
                    switch (modelRTRF.Resource_Type)
                    {
                        case Resource_TypeConst.ScienceWord类型文件:
                            filePath += "swDocument\\";
                            break;
                        case Resource_TypeConst.class类型微课件:
                            filePath += "microClassDocument\\";
                            break;
                        case Resource_TypeConst.class类型文件:
                            filePath += "classDocument\\";
                            break;
                    }
                    #region 文件及图片
                    filePath += savePath;
                    filePath = savePathOwn + filePath;

                    listReturn.Add(uploadPath + filePath + modelRTRF.ResourceToResourceFolder_Id + "." + modelRTRF.File_Suffix);
                    listReturn.Add(uploadPath + filePath + modelRTRF.ResourceToResourceFolder_Id + ".htm");

                    DataTable dtImg = new BLL_ResourceToResourceFolder_img().GetList("ResourceToResourceFolder_Id='" + modelRTRF.ResourceToResourceFolder_Id + "'").Tables[0];
                    foreach (DataRow item in dtImg.Rows)
                    {
                        listReturn.Add(uploadPath + item["ResourceToResourceFolderImg_Url"].ToString());
                    }

                    #endregion

                    #endregion
                }

                return listReturn;
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("获取资源对应所有文件路径失败。{0}", ex.Message.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据文件路径，删除文件（资源对应文件） 17-06-07TS
        /// </summary>
        private void DeleteResourceFile(List<string> list)
        {
            try
            {
                foreach (string fileUrl in list)
                {
                    if (File.Exists(fileUrl))
                    {
                        File.Delete(fileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS("", Request.Url.ToString(), string.Format("删除文件失败。{0}", ex.Message.ToString()));
            }
        }

        public static string GetStrWhereBySelfClassForComment(string SubjectId, string UserId)
        {
            string strWhere = string.Empty;
            try
            {
                strWhere = @" and T.UserGroup_Id in(select ClassId from (
select distinct vw.ClassId,AnalysisDataCount=(select COUNT(1) from HomeWork where UserGroup_Id=vw.ClassId)
from VW_UserOnClassGradeSchool vw where ClassMemberShipEnum in('" + MembershipEnum.headmaster + "','" + MembershipEnum.teacher + "') and UserId='" + UserId + "') t where AnalysisDataCount>0) ";
            }
            catch (Exception)
            {

            }
            return strWhere;
        }

        private string GetPaperHeaderDoc(string strResourceToResourceFolder_Id)
        {
            string strTemp = string.Empty;
            object obj = new BLL_ResourceToResourceFolder_Property().GetpaperHeaderDoc(strResourceToResourceFolder_Id);
            if (obj != null)
            {
                strTemp = obj.ToString();
            }
            return strTemp;
        }

    }
}
