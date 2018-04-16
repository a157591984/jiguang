using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Rc.Cloud.Web.Common;
using Rc.Common.StrUtility;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using System.Data;
using System.Web.Services;

namespace Rc.Cloud.Web.teacher
{
    public partial class GradeNums : System.Web.UI.Page
    {
        protected static string ugroupId = string.Empty;
        protected static string ugroupTitle = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ugroupId = Request.QueryString["ugroupId"].Filter();
            Model_UserGroup ugModel = new BLL_UserGroup().GetModel(ugroupId);
            if (ugModel!=null&&ugModel.UserGroup_Id!=null)
            {
                ugroupTitle = string.Format("{0}({1})", ugModel.UserGroup_Name, ugroupId);
            }
        }

        [WebMethod]
        public static string GetGradeList(string ClassName)
        {
            try
            {
                ClassName = ClassName.Filter();

                string strWhere = string.Format("User_ApplicationStatus='passed' and UserStatus=0 and UserGroup_Id='{0}' ", ugroupId);
                if (!string.IsNullOrEmpty(ClassName))
                {
                    strWhere += string.Format(" and UserGroup_Name like '%{0}%' ", ClassName);
                }
                string orderBy = string.Format("charindex(MembershipEnum,'{0},{1},{2}'),UserStatus,UserGroupOrder,User_ApplicationPassTime desc"
                    , MembershipEnum.gradedirector, MembershipEnum.GroupLeader, MembershipEnum.classrc);
                List<object> listReturn = new List<object>();
                DataTable dt = new BLL_UserGroup_Member().GetGradeMemberListByPageEX(strWhere, orderBy, 1, 60).Tables[0];
                int inum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    inum++;
                    string strTeacherCount = dt.Rows[i]["TeacherCount"].ToString();
                    string strStudentCount = dt.Rows[i]["StudentCount"].ToString();
                    if (strTeacherCount == "0") strTeacherCount = "-";
                    if (strStudentCount == "0") strStudentCount = "-";
                    listReturn.Add(new
                    {
                        Row = dt.Rows[i]["Row"],
                        UserGroup_Id = dt.Rows[i]["User_Id"],
                        UserGroup_Name = dt.Rows[i]["UserGroup_Name"],
                        PostName = dt.Rows[i]["PostName"],
                        MembershipEnum = dt.Rows[i]["MembershipEnum"],
                        TeacherCount = strTeacherCount,
                        StudentCount = strStudentCount
                    });
                }

                if (inum > 0)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
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