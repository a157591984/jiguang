namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsHelper
    {
        private string _classid;
        private DateTime? _correct_time;
        private string _createuser;
        private string _exec_status;
        private DateTime? _exec_time;
        private string _gradeid;
        private string _homework_id;
        private DateTime? _hw_createtime;
        private string _resourcetoresourcefolder_id;
        private string _schoolid;
        private string _statshelper_id;
        private string _stype;
        private string _teacherid;

        public string ClassId
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public DateTime? Correct_Time
        {
            get
            {
                return this._correct_time;
            }
            set
            {
                this._correct_time = value;
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

        public string Exec_Status
        {
            get
            {
                return this._exec_status;
            }
            set
            {
                this._exec_status = value;
            }
        }

        public DateTime? Exec_Time
        {
            get
            {
                return this._exec_time;
            }
            set
            {
                this._exec_time = value;
            }
        }

        public string GradeId
        {
            get
            {
                return this._gradeid;
            }
            set
            {
                this._gradeid = value;
            }
        }

        public string Homework_Id
        {
            get
            {
                return this._homework_id;
            }
            set
            {
                this._homework_id = value;
            }
        }

        public DateTime? HW_CreateTime
        {
            get
            {
                return this._hw_createtime;
            }
            set
            {
                this._hw_createtime = value;
            }
        }

        public string ResourceToResourceFolder_Id
        {
            get
            {
                return this._resourcetoresourcefolder_id;
            }
            set
            {
                this._resourcetoresourcefolder_id = value;
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

        public string StatsHelper_Id
        {
            get
            {
                return this._statshelper_id;
            }
            set
            {
                this._statshelper_id = value;
            }
        }

        public string SType
        {
            get
            {
                return this._stype;
            }
            set
            {
                this._stype = value;
            }
        }

        public string TeacherId
        {
            get
            {
                return this._teacherid;
            }
            set
            {
                this._teacherid = value;
            }
        }
    }
}

