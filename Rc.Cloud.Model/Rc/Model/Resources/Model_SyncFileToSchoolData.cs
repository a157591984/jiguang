namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SyncFileToSchoolData
    {
        private int? _bookallcount;
        private int? _booktobecount;
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _schoolid;
        private string _schoolname;
        private string _schoolurl;
        private string _syncfiletoschooldata_id;

        public int? BookAllCount
        {
            get
            {
                return this._bookallcount;
            }
            set
            {
                this._bookallcount = value;
            }
        }

        public int? BookTobeCount
        {
            get
            {
                return this._booktobecount;
            }
            set
            {
                this._booktobecount = value;
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

        public string SchoolName
        {
            get
            {
                return this._schoolname;
            }
            set
            {
                this._schoolname = value;
            }
        }

        public string SchoolUrl
        {
            get
            {
                return this._schoolurl;
            }
            set
            {
                this._schoolurl = value;
            }
        }

        public string SyncFileToSchoolData_Id
        {
            get
            {
                return this._syncfiletoschooldata_id;
            }
            set
            {
                this._syncfiletoschooldata_id = value;
            }
        }
    }
}

