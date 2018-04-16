namespace Rc.BLL.Resources
{
    using Rc.DAL.Resources;
    using Rc.Model.Resources;
    using System;

    public class BLL_F_User_Client
    {
        private readonly DAL_F_User_Client dal = new DAL_F_User_Client();

        public Model_F_User_Client GetTPUserModelByClientLogin(string user_name)
        {
            return this.dal.GetTPUserModelByClientLogin(user_name);
        }

        public Model_F_User_Client GetUserModelByClientLogin(string user_name)
        {
            return this.dal.GetUserModelByClientLogin(user_name);
        }

        public Model_F_User_Client GetUserModelByClientLogin(string user_name, string user_pw)
        {
            return this.dal.GetUserModelByClientLogin(user_name, user_pw);
        }

        public Model_F_User_Client GetUserModelByClientToken(string user_id, string token, string product_type)
        {
            return this.dal.GetUserModelByClientToken(user_id, token, product_type);
        }
    }
}

