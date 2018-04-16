namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using System;
    using System.Text;

    public class DAL_VW_UserOnClassGradeSchool
    {
        public bool VerifyExistsByStrWhere(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from VW_UserOnClassGradeSchool ");
            builder.Append(" where " + strWhere);
            return DbHelperSQL.Exists(builder.ToString());
        }
    }
}

