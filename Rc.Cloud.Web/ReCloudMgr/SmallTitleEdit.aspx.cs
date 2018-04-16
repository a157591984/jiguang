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
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class SmallTitleEdit : Rc.Cloud.Web.Common.InitPage
    {
        public string TestPaper_FrameDetail_Id = string.Empty;
        public string TestPaper_Frame_Id = string.Empty;
        public string type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPaper_FrameDetail_Id = Request["TestPaper_FrameDetail_Id"].Filter();
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            type = Request["type"].Filter();
            if (!IsPostBack)
            {
                InitInfo();
                DataTable dt = new DataTable();
                string strWhere = string.Empty;

                //教材版本
                strWhere = " D_Type='24' order by d_order";
                dt = new Rc.BLL.Resources.BLL_Common_Dict().GetList(strWhere).Tables[0];
                Rc.Cloud.Web.Common.pfunction.SetDdl(ddlTestQuestionType_Web, dt, "D_Name", "Common_Dict_ID", "-请选择-");
                if (!string.IsNullOrEmpty(type))
                {
                    LoadData();
                }
                else
                {
                    string Sql = "select isnull(max(TestQuestions_Num),0)+1 from TestPaper_FrameDetail where TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'";
                    object i = Rc.Common.DBUtility.DbHelperSQL.GetSingle(Sql);
                    this.txtSort.Text = i.ToString();
                }
            }
        }
        private void InitInfo()
        {
            foreach (TestQuestions_Type item in Enum.GetValues(typeof(TestQuestions_Type)))
            {
                ddlTestQuestions_Type.Items.Add(new ListItem(EnumService.GetDescription(item), item.ToString()));
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
                Model_TestPaper_FrameDetail model = new BLL_TestPaper_FrameDetail().GetModel(TestPaper_FrameDetail_Id);
                if (model != null)
                {
                    txtSort.Text = model.TestQuestions_Num.ToString();
                    this.txtTestQuestions_NumStr.Text = model.TestQuestions_NumStr;
                    this.txtScore.Text = model.Score.ToString();
                    ddlTestQuestionType_Web.SelectedValue = model.TestQuestionType_Web;
                    //this.txtKnowledgePoint.Text = model.KnowledgePoint;
                    //this.txtTargetText.Text = model.TargetText;
                    ////this.txtRemark.Text = model.Remark;
                    //ddlComplexityText.SelectedValue = model.ComplexityText;
                    ddlTestQuestions_Type.SelectedValue = model.TestQuestions_Type;
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
                if (new BLL_TestPaper_FrameToTestpaper().GetRecordCount("TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败,此试卷结构已关联试卷无法操作。',{ time: 2000,icon:2});</script>");
                    return;
                }
                if (new BLL_TestPaper_FrameToTestpaper().GetRecordCount("TestPaper_Frame_Id='" + TestPaper_FrameDetail_Id + "'") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败,此试卷结构明细已导入试题无法操作。',{ time: 2000,icon:2});</script>");
                    return;
                }
                if (!string.IsNullOrEmpty(type))
                {

                    Model_TestPaper_FrameDetail model = new BLL_TestPaper_FrameDetail().GetModel(TestPaper_FrameDetail_Id);

                    model.TestQuestions_NumStr = this.txtTestQuestions_NumStr.Text.TrimEnd();
                    model.TestQuestions_Num = Convert.ToInt32(this.txtSort.Text.TrimEnd());
                    model.Score = Convert.ToDecimal(this.txtScore.Text.TrimEnd());
                    //model.KnowledgePoint = this.txtKnowledgePoint.Text.TrimEnd();
                    //model.TargetText = this.txtTargetText.Text.TrimEnd();
                    ////model.Remark = this.txtRemark.Text.TrimEnd();
                    //model.ComplexityText = ddlComplexityText.SelectedValue;
                    model.TestQuestions_Type = ddlTestQuestions_Type.SelectedValue;
                    model.TestPaper_FrameType = ddlTestQuestions_Type.SelectedValue == "complex" ? "complex" : "simple";
                    model.TestQuestionType_Web = ddlTestQuestionType_Web.SelectedValue;
                    if (new BLL_TestPaper_FrameDetail().Update(model))
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('修改失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                }
                else
                {
                    Model_TestPaper_FrameDetail model = new Model_TestPaper_FrameDetail();
                    model.TestPaper_FrameDetail_Id = Guid.NewGuid().ToString();
                    model.TestPaper_Frame_Id = TestPaper_Frame_Id;
                    model.ParentId = TestPaper_FrameDetail_Id;
                    model.TestQuestions_NumStr = this.txtTestQuestions_NumStr.Text.TrimEnd();
                    model.TestQuestions_Num = Convert.ToInt32(this.txtSort.Text.TrimEnd());
                    model.Score = Convert.ToDecimal(this.txtScore.Text.TrimEnd());
                    //model.KnowledgePoint = this.txtKnowledgePoint.Text.TrimEnd();
                    //model.TargetText = this.txtTargetText.Text.TrimEnd();
                    ////model.Remark = this.txtRemark.Text.TrimEnd();
                    //model.ComplexityText = ddlComplexityText.SelectedValue;
                    model.TestQuestions_Type = ddlTestQuestions_Type.SelectedValue;
                    model.TestQuestionType_Web = ddlTestQuestionType_Web.SelectedValue;
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;
                    model.TestPaper_FrameType = ddlTestQuestions_Type.SelectedValue == "complex" ? "complex" : "simple";
                    if (new BLL_TestPaper_FrameDetail().Add(model))
                    {

                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('增加成功!',{ time: 2000,icon:1},function(){parent.loadData();parent.layer.close(index)});</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('增加失败!',{ time: 2000,icon:2});</script>");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}