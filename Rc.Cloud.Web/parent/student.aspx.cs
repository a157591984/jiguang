using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rc.BLL.Resources;
using Rc.Model.Resources;
using Rc.Common.StrUtility;
using Newtonsoft.Json;
namespace Rc.Cloud.Web.parent
{
    public partial class student : Rc.Cloud.Web.Common.FInitData
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        [WebMethod]
        public static string GetmyBaby(int PageIndex, int PageSize)
        {
            try
            {
                string strWhere = string.Empty;
                BLL_StudentToParent bll = new BLL_StudentToParent();
                Rc.Model.Resources.Model_F_User FloginUser = Rc.Common.StrUtility.clsUtility.IsFPageFlag() as Rc.Model.Resources.Model_F_User;
                strWhere = "and Parent_ID='" + FloginUser.UserId + "'";
                PageIndex = Convert.ToInt32(PageIndex.ToString().Filter());
                GH_PagerInfo<List<Model_StudentToParent>> pageInfo = bll.GetAllList(strWhere, "", PageIndex, PageSize);
                List<Model_StudentToParent> list = pageInfo.PageData;
                List<object> listReturn = new List<object>();
                int inum = 1;
                foreach (var item in list)
                {
                    listReturn.Add(new
                    {
                        Num = inum + PageSize * (PageIndex - 1),
                        BabyName = item.UserName,
                        struserid=item.Student_ID,
                        Email = string.IsNullOrEmpty(item.Email) ? "" : item.Email

                    });
                    inum++;
                }
                if (inum > 1)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        err = "null",
                        PageIndex = pageInfo.CurrentPage,
                        PageSize = pageInfo.PageSize,
                        TotalCount = pageInfo.RecordCount,
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    err = ex.Message.ToString()
                });
            }
        }

    }
}