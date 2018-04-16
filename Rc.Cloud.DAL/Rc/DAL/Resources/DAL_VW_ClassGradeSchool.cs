namespace Rc.DAL.Resources
{
    using Rc.Common.DBUtility;
    using System;
    using System.Data;
    using System.Text;

    public class DAL_VW_ClassGradeSchool
    {
        public DataSet GetGradeList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT DISTINCT SchoolId,GradeName,GradeId FROM VW_ClassGradeSchool");
            builder.Append(" where " + strWhere);
            return DbHelperSQL.Query(builder.ToString());
        }
    }
}

