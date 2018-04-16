namespace Rc.Cloud.Model
{
    using System;

    [Serializable]
    public class Model_F_User
    {
        private DateTime? _birthday;
        private string _city;
        private string _county;
        private DateTime? _createtime;
        private string _email;
        private string _mobile;
        private string _password;
        private string _province;
        private string _school;
        private string _sex;
        private string _truename;
        private string _userid;
        private string _useridentity;
        private string _username;

        public DateTime? Birthday
        {
            get
            {
                return this._birthday;
            }
            set
            {
                this._birthday = value;
            }
        }

        public string City
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
            }
        }

        public string County
        {
            get
            {
                return this._county;
            }
            set
            {
                this._county = value;
            }
        }

        public DateTime? CreateTime
        {
            get
            {
                return this._createtime;
            }
            set
            {
                this._createtime = value;
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this._mobile;
            }
            set
            {
                this._mobile = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string Province
        {
            get
            {
                return this._province;
            }
            set
            {
                this._province = value;
            }
        }

        public string School
        {
            get
            {
                return this._school;
            }
            set
            {
                this._school = value;
            }
        }

        public string Sex
        {
            get
            {
                return this._sex;
            }
            set
            {
                this._sex = value;
            }
        }

        public string TrueName
        {
            get
            {
                return this._truename;
            }
            set
            {
                this._truename = value;
            }
        }

        public string UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public string UserIdentity
        {
            get
            {
                return this._useridentity;
            }
            set
            {
                this._useridentity = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
    }
}

