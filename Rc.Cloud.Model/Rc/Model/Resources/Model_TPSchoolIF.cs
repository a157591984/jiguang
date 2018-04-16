namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TPSchoolIF
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _schoolid;
        private string _schoolif_code;
        private string _schoolif_id;
        private string _schoolif_name;

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

        public string SchoolId
        {
            get
            {
                return this._schoolid;
            }
            set
            {
                this._schoolid = value;
            }
        }

        public string SchoolIF_Code
        {
            get
            {
                return this._schoolif_code;
            }
            set
            {
                this._schoolif_code = value;
            }
        }

        public string SchoolIF_Id
        {
            get
            {
                return this._schoolif_id;
            }
            set
            {
                this._schoolif_id = value;
            }
        }

        public string SchoolIF_Name
        {
            get
            {
                return this._schoolif_name;
            }
            set
            {
                this._schoolif_name = value;
            }
        }
    }
}

