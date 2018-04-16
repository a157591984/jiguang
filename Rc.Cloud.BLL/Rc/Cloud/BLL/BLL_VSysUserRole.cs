namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using Rc.Cloud.Model;
    using System;

    public class BLL_VSysUserRole
    {
        private readonly DAL_VSysUserRole dal = new DAL_VSysUserRole();

        public Model_VSysUserRole GetSysUserInfoModelBySysUserId(string SysUserId)
        {
            return this.dal.GetSysUserInfoModelBySysUserId(SysUserId);
        }

        public Model_VSysUserRole GetVDoctorInfoModelByLogin(string DoctorLoginInfo_LoginName, string DoctorLoginInfo_Pwd)
        {
            return this.dal.GetVDoctorInfoModelByLogin(DoctorLoginInfo_LoginName, DoctorLoginInfo_Pwd);
        }
    }
}

