namespace Rc.Cloud.BLL
{
    using Rc.Cloud.DAL;
    using System;
    using System.Runtime.InteropServices;

    public class BLL_Login
    {
        private readonly DAL_Login dal = new DAL_Login();

        public bool UpdateSysUserChangePassword(string old_, string new_, string login_name)
        {
            return this.dal.UpdateSysUserChangePassword(old_, new_, login_name);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct stru_Func
        {
            public bool page;
            public bool Add;
            public bool Edit;
            public bool Delete;
            public bool Select;
            public bool Check;
            public bool Input;
            public bool Output;
        }
    }
}

