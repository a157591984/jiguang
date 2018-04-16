namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using System;
    using System.Data;

    public class BLL_DictForHospital
    {
        private readonly DAL_DictForHospital dal = new DAL_DictForHospital();

        public DataSet GetHospitalLevel(string strWhere)
        {
            return this.dal.GetHospitalLevel(strWhere);
        }

        public DataSet GetHospitalProperty(string strWhere)
        {
            return this.dal.GetHospitalProperty(strWhere);
        }

        public DataSet GetHospitalRegional(string strWhere)
        {
            return this.dal.GetHospitalRegional(strWhere);
        }
    }
}

