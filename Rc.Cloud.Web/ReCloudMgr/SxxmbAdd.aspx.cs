using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Common;
using System.Text;
using System.Data;
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SxxmbAdd : Rc.Cloud.Web.Common.InitPage
    {
        public string TestPaper_Frame_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            if (!IsPostBack)
            {
                TestPaper_Frame_Id = Request.QueryString["TestPaper_Frame_Id"];
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string Two_WayChecklist_Id = Guid.NewGuid().ToString();
                string CreateUser = loginUser.SysUser_ID;
                StringBuilder sbSql = new StringBuilder();
                #region 插入双向细目表主表
                sbSql.AppendFormat(@" insert into  Two_WayChecklist (Two_WayChecklist_Id,Two_WayChecklist_Name,ParticularYear,GradeTerm
,Resource_Version,[Subject],[Remark],[Two_WayChecklistType],[CreateUser],[CreateTime],[ParentId])
select '{0}','{3}',[ParticularYear] ,[GradeTerm],[Resource_Version]
,[Subject],'{4}','0','{1}',GetDate(),'{2}' from TestPaper_Frame where TestPaper_Frame_Id='{2}' ;"
                    , Two_WayChecklist_Id
                    , CreateUser
                    , TestPaper_Frame_Id
                    , this.txtTwo_WayChecklist_Name.Text.Trim()
                    , this.txtRemark.Text.TrimEnd());
                #endregion
                #region 插入双向细目表主表
                string sql = "select * from TestPaper_FrameDetail where TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'";
                DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                DataRow[] drP = dt.Select("ParentId='0'");
                foreach (DataRow item in drP)
                {
                    string ParentID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat(@" insert into Two_WayChecklistDetail([Two_WayChecklistDetail_Id],[Two_WayChecklist_Id],[ParentId],[TestQuestions_Num]
,[TestQuestions_NumStr],[TestQuestions_Type],[TargetText],[ComplexityText],[KnowledgePoint],[Score]
,[Remark],[Two_WayChecklistType],[CreateUser],[CreateTime],TestPaper_FrameDetail_Id) values('{0}','{1}','0','{2}','{3}','{4}','{5}','{6}','{7}'
,'{8}','{9}','{10}','{11}','{12}','{13}')"
                        , ParentID
                        , Two_WayChecklist_Id
                        , item["TestQuestions_Num"].ToString()
                        , item["TestQuestions_NumStr"].ToString()
                        , item["TestQuestions_Type"].ToString()
                        , item["TargetText"].ToString()
                        , item["ComplexityText"].ToString()
                        , item["KnowledgePoint"].ToString()
                        , 0
                        , item["Remark"].ToString()
                        , item["TestPaper_FrameType"].ToString()
                        , CreateUser
                        , DateTime.Now.ToString()
                        , item["TestPaper_FrameDetail_Id"].ToString());
                    DataRow[] dr = dt.Select("ParentId='" + item["TestPaper_FrameDetail_Id"] + "'");
                    if (dr.Length > 0)
                    {
                        foreach (DataRow itemSon in dr)
                        {
                            sbSql.AppendFormat(@" insert into Two_WayChecklistDetail([Two_WayChecklistDetail_Id],[Two_WayChecklist_Id],[ParentId],[TestQuestions_Num]
,[TestQuestions_NumStr],[TestQuestions_Type],[TargetText],[ComplexityText],[KnowledgePoint],[Score]
,[Remark],[Two_WayChecklistType],[CreateUser],[CreateTime],TestPaper_FrameDetail_Id) values (NewId(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'
,'{8}','{9}','{10}','{11}','{12}','{13}');"
                            , Two_WayChecklist_Id
                            , ParentID
                            , itemSon["TestQuestions_Num"].ToString()
                        , itemSon["TestQuestions_NumStr"].ToString()
                        , itemSon["TestQuestions_Type"].ToString()
                        , itemSon["TargetText"].ToString()
                        , itemSon["ComplexityText"].ToString()
                        , itemSon["KnowledgePoint"].ToString()
                        , itemSon["Score"].ToString()
                        , itemSon["Remark"].ToString()
                        , itemSon["TestPaper_FrameType"].ToString()
                        , CreateUser
                        , DateTime.Now.ToString()
                        , itemSon["TestPaper_FrameDetail_Id"].ToString());
                        }
                    }
                }
                #endregion
                #region 出入双向细目表与题关系
                sbSql.AppendFormat(@" insert into Two_WayChecklistDetailToTestQuestions ([Two_WayChecklistDetailToTestQuestions_Id]
,[Two_WayChecklist_Id],[Two_WayChecklistDetail_Id],[ResourceToResourceFolder_Id],[TestQuestions_Id],[CreateUser],[CreateTime])
select NEWID(), twd.Two_WayChecklist_Id,twd.Two_WayChecklistDetail_Id,ResourceToResourceFolder_Id,[TestQuestions_Id]
,'{0}',getdate() from TestPaper_FrameDetailToTestQuestions t
inner join Two_WayChecklistDetail twd on twd.TestPaper_FrameDetail_Id=t.TestPaper_FrameDetail_Id
where twd.Two_WayChecklist_Id='{1}' and t.TestPaper_Frame_Id='{2}'"
                    , CreateUser
                    , Two_WayChecklist_Id
                    , TestPaper_Frame_Id);
                #endregion
                #region 插入双向细目表与知识点的关系
                sbSql.AppendFormat(@"insert into [Two_WayChecklistDetailToAttr]([Two_WayChecklistDetailToAttr_Id],[Two_WayChecklist_Id],[Two_WayChecklistDetail_Id]
,[Attr_Type],[Attr_Value],[CreateUser],[CreateTime])
select NEWID(),'{0}',d.Two_WayChecklistDetail_Id,[Attr_Type],[Attr_Value],'{1}','{2}' from [TestPaper_FrameDetailToTestQuestions_Attr] tpAttr
inner join [Two_WayChecklistDetail] d on d.TestPaper_FrameDetail_Id=tpAttr.TestPaper_FrameDetail_Id and d.Two_WayChecklist_Id='{0}'
where tpAttr.TestPaper_Frame_Id='{3}'"
                    , Two_WayChecklist_Id
                    , CreateUser
                    , DateTime.Now
                    , TestPaper_Frame_Id);
                #endregion
                int row = Rc.Common.DBUtility.DbHelperSQL.ExecuteSql(sbSql.ToString());
                if (row > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('添加成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});})</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.ready(function () {layer.msg('添加失败!',{ time: 2000,icon:2}) });</script>");
                    return;
                }

            }
            catch (Exception ex)
            {
                Rc.Common.SystemLog.SystemLog.AddLogErrorFromBS(TestPaper_Frame_Id, "", string.Format("新建双向细目表失败：双向细目表Id{0}|错误信息{1}", TestPaper_Frame_Id, ex.Message.ToString()));
            }
        }

    }
}