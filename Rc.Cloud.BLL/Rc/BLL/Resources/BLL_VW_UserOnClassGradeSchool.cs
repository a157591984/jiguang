namespace Rc.BLL.Resources
{
    using Rc.DAL.Resources;
    using System;

    public class BLL_VW_UserOnClassGradeSchool
    {
        private readonly DAL_VW_UserOnClassGradeSchool dal = new DAL_VW_UserOnClassGradeSchool();

        public bool VerifyExistsByStrWhere(string strWhere)
        {
            return this.dal.VerifyExistsByStrWhere(strWhere);
        }
    }
}

