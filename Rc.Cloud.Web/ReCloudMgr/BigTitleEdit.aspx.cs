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
namespace Rc.Cloud.Web.ReCloudMgr
{
    public partial class BigTitleEdit : Rc.Cloud.Web.Common.InitPage
    {
        public string TestPaper_FrameDetail_Id = string.Empty;
        public string TestPaper_Frame_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPaper_FrameDetail_Id = Request["TestPaper_FrameDetail_Id"].Filter();
            TestPaper_Frame_Id = Request["TestPaper_Frame_Id"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(TestPaper_FrameDetail_Id))
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
        public void LoadData()
        {
            try
            {
                Model_TestPaper_FrameDetail model = new BLL_TestPaper_FrameDetail().GetModel(TestPaper_FrameDetail_Id);
                if (model != null)
                {
                    this.txtBigNum.Text = model.TestQuestions_NumStr;
                    this.txtSort.Text = model.TestQuestions_Num.ToString();
                    //this.txtRemark.Text = model.Remark;
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
                if (new BLL_TestPaper_FrameToTestpaper().GetRecordCount("TestPaper_Frame_Id='" + TestPaper_Frame_Id + "'") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败,此试卷结构已关联试卷无法操作。',{ time: 2000,icon:2});</script>");
                    return;
                }
                if (!string.IsNullOrEmpty(TestPaper_FrameDetail_Id))
                {
                    Model_TestPaper_FrameDetail model = new BLL_TestPaper_FrameDetail().GetModel(TestPaper_FrameDetail_Id);
                    if (model != null)
                    {
                        //model.Remark = txtRemark.Text.TrimEnd();
                        model.TestQuestions_NumStr = txtBigNum.Text.TrimEnd();
                        model.TestQuestions_Num = Convert.ToInt32(txtSort.Text.TrimEnd());
                        //model.TestQuestions_Type = ddlTestQuestions_Type.SelectedValue;
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
                }
                else
                {
                    Model_TestPaper_FrameDetail model = new Model_TestPaper_FrameDetail();
                    model.TestPaper_FrameDetail_Id = Guid.NewGuid().ToString();
                    model.TestPaper_Frame_Id = TestPaper_Frame_Id;
                    //model.Remark = txtRemark.Text.TrimEnd();
                    model.TestQuestions_NumStr = txtBigNum.Text.TrimEnd();
                    model.TestQuestions_Num = Convert.ToInt32(txtSort.Text.TrimEnd());
                    model.ParentId = "0";
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;
                    //model.TestQuestions_Type = ddlTestQuestions_Type.SelectedValue;
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