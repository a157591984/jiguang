namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SchoolSMS
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _school_id;
        private string _school_name;
        private int? _smscount;
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

        public string School_Id
        {
            get
            {
                return this._school_id;
            }
            set
            {
                this._school_id = value;
            }
        }

        public string School_Name
        {
            get
            {
                return this._school_name;
            }
            set
            {
                this._school_name = value;
            }
        }

        public int? SMSCount
        {
            get
            {
                return this._smscount;
            }
            set
            {
                this._smscount = value;
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

