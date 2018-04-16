using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Rc.BLL.Resources;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;
using Rc.Model.Resources;

namespace Homework.student
{
    public partial class classMember : Rc.Cloud.Web.Common.FInitData
    {
        protected string ugroupId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ugroupId = Request.QueryString["ugroupId"].Filter();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            DataTable dt = new BLL_UserGroup().GetList(string.Format("UserGroup_Id in(select UserGroup_Id from UserGroup_Member where User_ApplicationStatus='passed' and UserStatus='0' and User_Id='{0}') order by CreateTime desc ", FloginUser.UserId)).Tables[0];
            if (string.IsNullOrEmpty(ugroupId) && dt.Rows.Count > 0)
            {
                ugroupId = dt.Rows[0]["UserGroup_Id"].ToString();
            }
            rptClass.DataSource = dt;
            rptClass.DataBind();
        }
        protected string GetStyle(string strGradeTerm_ID)
        {
            string strTemp = string.Empty;
            if (ugroupId.Trim() == strGradeTerm_ID.Trim())
            {
                strTemp = " active";
            }
            return strTemp;
        }

        [WebMethod]
        public static string GetClassMember(string UserGroup_Id, int PageIndex, int PageSize)
        {
            try
            {
                UserGroup_Id = UserGroup_Id.Filter();

                DataTable dt = new DataTable();
                List<object> listReturn = new List<object>();
                string strSqlCount = string.Empty;
                string strWhere = string.Format("User_ApplicationStatus='passed' and UserGroup_Id='{0}'", UserGroup_Id);
                string orderBy = string.Format("charindex(MembershipEnum,'{0},{1},{2}'),TrueName,UserStatus,User_ApplicationPassTime desc",
                   MembershipEnum.headmaster, MembershipEnum.teacher, MembershipEnum.student);
                dt = new BLL_UserGroup_Member().GetClassMemberListByPageEX(strWhere, orderBy, ((PageIndex - 1) * PageSize + 1), (PageIndex * PageSize)).Tables[0];
                int rCount = new BLL_UserGroup_Member().GetRecordCount(strWhere);
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    listReturn.Add(new
                    {
                        Row = dt.Rows[i]["Row"],
                        UserGroup_Member_Id = dt.Rows[i]["UserGroup_Member_Id"],
                        UserName = dt.Rows[i]["UserName"],
                        TrueName = string.IsNullOrEmpty(dt.Rows[i]["TrueName"].ToString()) ? dt.Rows[i]["UserName"] : dt.Rows[i]["TrueName"],
                        User_ApplicationPassTime = pfunction.ConvertToLongDateTime(dt.Rows[i]["User_ApplicationPassTime"].ToString()),
                        UserStatus = dt.Rows[i]["UserStatus"],
                        Email = dt.Rows[i]["Email"].ToString(),
                        MembershipEnum = dt.Rows[i]["MembershipEnum"],
                        MembershipEnumName = (dt.Rows[i]["MembershipEnum"].ToString() == MembershipEnum.teacher.ToString()) ? dt.Rows[i]["SubjectName"].ToString() + "老师" : Rc.Common.EnumService.GetDescription<MembershipEnum>(dt.Rows[i]["MembershipEnum"].ToString()),
                    });
                }

                if (inum > 0)
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
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = "error"//ex.Message.ToString()
                });
            }
        }
    }
}