namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_DatabaseCopyLibrary
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _customerinfo_id;
        private string _databaseaddress;
        private string _databasecopylibrary_id;
        private string _databasecopylibrary_remark;
        private string _databasename;
        private string _loginname;
        private string _loginpassword;
        private DateTime? _updatetime;
        private string _updateuser;

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

        public string CreateUser
        {
            get
            {
                return this._createuser;
            }
            set
            {
                this._createuser = value;
            }
        }

        public string CustomerInfo_ID
        {
            get
            {
                return this._customerinfo_id;
            }
            set
            {
                this._customerinfo_id = value;
            }
        }

        public string DataBaseAddress
        {
            get
            {
                return this._databaseaddress;
            }
            set
            {
                this._databaseaddress = value;
            }
        }

        public string DatabaseCopyLibrary_ID
        {
            get
            {
                return this._databasecopylibrary_id;
            }
            set
            {
                this._databasecopylibrary_id = value;
            }
        }

        public string DatabaseCopyLibrary_Remark
        {
            get
            {
                return this._databasecopylibrary_remark;
            }
            set
            {
                this._databasecopylibrary_remark = value;
            }
        }

        public string DataBaseName
        {
            get
            {
                return this._databasename;
            }
            set
            {
                this._databasename = value;
            }
        }

        public string LoginName
        {
            get
            {
                return this._loginname;
            }
            set
            {
                this._loginname = value;
            }
        }

        public string LoginPassword
        {
            get
            {
                return this._loginpassword;
            }
            set
            {
                this._loginpassword = value;
            }
        }

        public DateTime? UpdateTime
        {
            get
            {
                return this._updatetime;
            }
            set
            {
                this._updatetime = value;
            }
        }

        public string UpdateUser
        {
            get
            {
                return this._updateuser;
            }
            set
            {
                this._updateuser = value;
            }
        }
    }
}

