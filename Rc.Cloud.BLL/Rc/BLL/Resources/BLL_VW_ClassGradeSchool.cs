namespace Rc.BLL.Resources
{
    using Rc.DAL.Resources;
    using System;
    using System.Data;

    public class BLL_VW_ClassGradeSchool
    {
        private readonly DAL_VW_ClassGradeSchool dal = new DAL_VW_ClassGradeSchool();

        public DataSet GetGradeList(string strWhere)
        {
            return this.dal.GetGradeList(strWhere);
        }
    }
}

