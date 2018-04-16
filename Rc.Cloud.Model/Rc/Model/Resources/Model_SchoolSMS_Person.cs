namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SchoolSMS_Person
    {
        private string _company;
        private DateTime? _createtime;
        private string _createuser;
        private string _job;
        private string _name;
        private string _phonenum;
        private string _school_id;
        private string _schoolsms_person_id;

        public string Company
        {
            get
            {
                return this._company;
            }
            set
            {
                this._company = value;
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

        public string Job
        {
            get
            {
                return this._job;
            }
            set
            {
                this._job = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string PhoneNum
        {
            get
            {
                return this._phonenum;
            }
            set
            {
                this._phonenum = value;
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

        public string SchoolSMS_Person_Id
        {
            get
            {
                return this._schoolsms_person_id;
            }
            set
            {
                this._schoolsms_person_id = value;
            }
        }
    }
}

