using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using Rc.Model.Resources;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
namespace Rc.Cloud.Web.student
{
    public partial class rate : Rc.Cloud.Web.Common.FInitData
    {
        public string order_num = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            order_num = Request.QueryString["order_num"].Filter();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Model_UserOrder_Comment commet = new Model_UserOrder_Comment();
                BLL_UserOrder_Comment bll = new BLL_UserOrder_Comment();
                commet.comment_id = Guid.NewGuid().ToString();
                commet.create_time = DateTime.Now;
                if (pfunction.FilterKeyWords(this.txtcomment.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "save", "<script type='text/javascript'>layer.msg('评论内容存在敏感词汇，请重新填写。',{icon:2,time:2000});</script>");
                    return;
                }
                else
                {
                    commet.comment_content = txtcomment.Value.Trim();
                }
                commet.comment_evaluate = Convert.ToDecimal(hidscore.Value);
                commet.order_num = order_num;
                commet.user_id = FloginUser.UserId;
                if (bll.Add(commet))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('评论成功',{time:1000,icon:1},function(){window.parent.loadData();parent.layer.close(index);});", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('评论失败',{time:2000,icon:2});", true);
                }
            }
            catch (Exception ex)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "temp", "layer.msg('" + ex + "',{time:5000,icon:2});", true);
            }
        }
    }
}