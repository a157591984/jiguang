using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Rc.Common.StrUtility;

namespace MSWeb.Test
{
    public partial class XUV_CommonTest :  System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
//            txtCondition.Value = clsUtility.Encrypt(@"select HospitalDepartment_Usage_Id AS Common_Dict_ID,HospitalDepartment_Usage_Name AS D_Name,
//'' as D_Value,'' as [D_Code],'' as [D_Level],'' as [D_Order],'V204'
// AS [D_Type],'科室' [D_Remark],'' AS D_CreateUser ,'' as D_CreateTime,'' AS [D_ModifyUser] 
// ,null  AS [D_ModifyTime] from HospitalDepartment_Usage");

            //txtDrugID.Value = clsUtility.Encrypt("DiseaseStandard_Dict_ID|2185C512-1AF5-4127-AF4E-330872D90694");
        }
    }
}