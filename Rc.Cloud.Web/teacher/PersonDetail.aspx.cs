using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.Common.StrUtility;
using System.Data;
using Rc.BLL.Resources;
using Rc.Common.Config;

namespace Rc.Cloud.Web.teacher
{
    public partial class PersonDetail : Rc.Cloud.Web.Common.FInitData
    {
        string ResourceFolder_Id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceFolder_Id = Request.QueryString["ResourceFolder_Id"].Filter();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(ResourceFolder_Id))
                {
                    string sql = @"select u.UserName,u.TrueName
,fileCount=(select count(*) from ResourceToResourceFolder where Book_Id=p.ResourceFolder_Id and File_Suffix<>'' and CreateFUser=p.ChargePerson and Resource_Type='" + Resource_TypeConst.集体备课文件 + @"') from [dbo].[PrpeLesson_Person] p
left join F_User u on u.userid=p.ChargePerson
where p.ResourceFolder_Id='" + ResourceFolder_Id + "'";
                    DataTable dt = Rc.Common.DBUtility.DbHelperSQL.Query(sql).Tables[0];
                    rptHW.DataSource = dt;
                    rptHW.DataBind();
                }
            }
        }
    }
}