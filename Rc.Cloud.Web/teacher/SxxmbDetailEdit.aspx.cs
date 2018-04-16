using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.Common;
using Rc.BLL.Resources;
using System.Data;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.teacher
{
    public partial class SxxmbDetailEdit : Rc.Cloud.Web.Common.FInitData
    {
        public string Two_WayChecklistDetail_Id = string.Empty;
        public string Two_WayChecklist_Id = string.Empty;
        public string type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Two_WayChecklistDetail_Id = Request["Two_WayChecklistDetail_Id"].Filter();
            Two_WayChecklist_Id = Request["Two_WayChecklist_Id"].Filter();
            type = Request["type"].Filter();
            if (!IsPostBack)
            {
                //InitInfo();
                Model_Two_WayChecklistDetail model = new Model_Two_WayChecklistDetail();
                model = new BLL_Two_WayChecklistDetail().GetModel(Two_WayChecklistDetail_Id);
                ///ddlTestQuestions_Type.Enabled = false;
                LoadData();

            }
        }
        private void InitInfo()
        {
            foreach (TestQuestions_Type item in Enum.GetValues(typeof(TestQuestions_Type)))
            {
                //ddlTestQuestions_Type.Items.Add(new ListItem(EnumService.GetDescription(item), item.ToString()));
            }
            //foreach (ComplexityText item in Enum.GetValues(typeof(ComplexityText)))
            //{
            //    ddlComplexityText.Items.Add(new ListItem(EnumService.GetDescription(item), item.ToString()));
            //}
        }
        public void LoadData()
        {
            try
            {
                Model_Two_WayChecklistDetail model = new BLL_Two_WayChecklistDetail().GetModel(Two_WayChecklistDetail_Id);
                if (model != null)
                {
                    txtSort.Text = model.TestQuestions_Num.ToString();
                    this.txtTestQuestions_NumStr.Text = model.TestQuestions_NumStr;
                    this.txtScore.Text = model.Score.ToString();
                    //ddlKnowledgePoint.SelectedValue = model.KnowledgePoint.TrimEnd();
                    //ddlTargetText.SelectedValue = model.TargetText.TrimEnd();
                    //this.txtRemark.Text = model.Remark;
                    //ddlComplexityText.SelectedValue = model.ComplexityText.TrimEnd();
                    //ddlTestQuestions_Type.SelectedValue = model.TestQuestions_Type;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Model_Two_WayChecklistDetail model = new BLL_Two_WayChecklistDetail().GetModel(Two_WayChecklistDetail_Id);

                model.TestQuestions_NumStr = this.txtTestQuestions_NumStr.Text.TrimEnd();
                model.TestQuestions_Num = Convert.ToInt32(this.txtSort.Text.TrimEnd());
                model.Score = Convert.ToDecimal(this.txtScore.Text.TrimEnd());
                //model.KnowledgePoint = ddlKnowledgePoint.SelectedValue;
                //model.TargetText = ddlTargetText.SelectedValue;
                //model.Remark = this.txtRemark.Text.TrimEnd();
                //model.ComplexityText = ddlComplexityText.SelectedValue;
                //model.TestQuestions_Type = ddlTestQuestions_Type.SelectedValue;
               // model.Two_WayChecklistType = ddlTestQuestions_Type.SelectedValue == "complex" ? "complex" : "simple";
                if (new BLL_Two_WayChecklistDetail().Update(model))
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData('" + Two_WayChecklist_Id + "');parent.layer.close(index)});</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败!',{ time: 2000,icon:2});</script>");
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public void IsTestQuestion(string ContentText, string TargetText, string ComplexityText, string Two_WayChecklistDetail_Id)
        {
            try
            {
                string TestPaper_Frame_Id = string.Empty;
                Model_Two_WayChecklist model = new BLL_Two_WayChecklist().GetModel(Two_WayChecklist_Id);
                if (model != null)
                {
                    TestPaper_Frame_Id = model.ParentId;
                }
                //所有试题对应知识点，测量目标，难易度
                string sql = @"
	select distinct tq.TestQuestions_Id,tqs.ContentText,tqs.TargetText,tqs.ComplexityText,tf.TestPaper_FrameDetail_Id 
	from TestQuestions tq 
	left join TestQuestions_Score tqs on tqs.TestQuestions_Id=tq.TestQuestions_Id 
	inner join TestPaper_FrameDetailToTestQuestions tf on tf.TestQuestions_Id=tq.TestQuestions_Id
	where   tq.[type]='simple' and    tf.TestPaper_Frame_Id='" + TestPaper_Frame_Id + @"'

	union
	
	select distinct tq.TestQuestions_Id,tqs.ContentText,tqs.TargetText,tqs.ComplexityText,tf.TestPaper_FrameDetail_Id 
	from TestQuestions tq2 
	inner join TestPaper_FrameDetailToTestQuestions tf on tf.TestQuestions_Id=tq2.TestQuestions_Id 
	inner join TestQuestions tq on tq.Parent_Id<>'0' and tq.[type]='complex' and tq.Parent_Id=tq2.TestQuestions_Id 
	inner join TestQuestions_Score tqs on tqs.TestQuestions_Id=tq.TestQuestions_Id 
	where tq2.Parent_Id='0' and tq2.[type]='complex' and tf.TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'";
                DataTable dtTestQuestion = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                if (dtTestQuestion.Rows.Count > 0)
                {

                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}