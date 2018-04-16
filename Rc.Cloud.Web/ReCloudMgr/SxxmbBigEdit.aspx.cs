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
    public partial class SxxmbBigEdit : Rc.Cloud.Web.Common.InitPage
    {
        public string Two_WayChecklistDetail_Id = string.Empty;
        public string Two_WayChecklist_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Two_WayChecklistDetail_Id = Request["Two_WayChecklistDetail_Id"].Filter();
            Two_WayChecklist_Id = Request["Two_WayChecklist_Id"].Filter();
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Two_WayChecklistDetail_Id))
                {
                    LoadData();
                }
                else
                {
                    string Sql = "select isnull(max(TestQuestions_Num),0)+1 from Two_WayChecklistDetail where Two_WayChecklist_Id='" + Two_WayChecklist_Id + "'";
                    object i = Rc.Common.DBUtility.DbHelperSQL.GetSingle(Sql);
                    this.txtSort.Text = i.ToString();
                }
            }

        }
        public void LoadData()
        {
            try
            {
                Model_Two_WayChecklistDetail model = new BLL_Two_WayChecklistDetail().GetModel(Two_WayChecklistDetail_Id);
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
                if (new BLL_Two_WayChecklistToTestpaper().GetRecordCount("Two_WayChecklist_Id='" + Two_WayChecklist_Id + "'") > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('操作失败,此双向细目表已关联试卷无法操作。',{ time: 2000,icon:2});</script>");
                    return;
                }
                if (!string.IsNullOrEmpty(Two_WayChecklistDetail_Id))
                {
                    Model_Two_WayChecklistDetail model = new BLL_Two_WayChecklistDetail().GetModel(Two_WayChecklistDetail_Id);
                    if (model != null)
                    {
                        //model.Remark = txtRemark.Text.TrimEnd();
                        model.TestQuestions_NumStr = txtBigNum.Text.TrimEnd();
                        model.TestQuestions_Num = Convert.ToInt32(txtSort.Text.TrimEnd());
                        //model.TestQuestions_Type = ddlTestQuestions_Type.SelectedValue;
                        if (new BLL_Two_WayChecklistDetail().Update(model))
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
                    Model_Two_WayChecklistDetail model = new Model_Two_WayChecklistDetail();
                    model.Two_WayChecklistDetail_Id = Guid.NewGuid().ToString();
                    model.Two_WayChecklist_Id = Two_WayChecklist_Id;
                    //model.Remark = txtRemark.Text.TrimEnd();
                    model.TestQuestions_NumStr = txtBigNum.Text.TrimEnd();
                    model.TestQuestions_Num = Convert.ToInt32(txtSort.Text.TrimEnd());
                    model.ParentId = "0";
                    model.CreateUser = loginUser.SysUser_ID;
                    model.CreateTime = DateTime.Now;
                    //model.TestQuestions_Type = ddlTestQuestions_Type.SelectedValue;
                    if (new BLL_Two_WayChecklistDetail().Add(model))
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