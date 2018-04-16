namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysUser_token
    {
        private string _login_ip;
        private DateTime? _login_time;
        private string _product_type;
        private string _token;
        private DateTime? _token_time;
        private string _user_id;

        public string login_ip
        {
            get
            {
                return this._login_ip;
            }
            set
            {
                this._login_ip = value;
            }
        }

        public DateTime? login_time
        {
            get
            {
                return this._login_time;
            }
            set
            {
                this._login_time = value;
            }
        }

        public string product_type
        {
            get
            {
                return this._product_type;
            }
            set
            {
                this._product_type = value;
            }
        }

        public string token
        {
            get
            {
                return this._token;
            }
            set
            {
                this._token = value;
            }
        }

        public DateTime? token_time
        {
            get
            {
                return this._token_time;
            }
            set
            {
                this._token_time = value;
            }
        }

        public string user_id
        {
            get
            {
                return this._user_id;
            }
            set
            {
                this._user_id = value;
            }
        }
    }
}

