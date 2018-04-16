namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TPIFUser
    {
        private DateTime? _createtime;
        private string _remark;
        private string _school;
        private string _thirdpartyifuser_id;
        private string _username;

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

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
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

        public string ThirdPartyIFUser_Id
        {
            get
            {
                return this._thirdpartyifuser_id;
            }
            set
            {
                this._thirdpartyifuser_id = value;
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

