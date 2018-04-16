namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_PrpeLesson
    {
        private DateTime? _createtime;
        private string _createuser;
        private DateTime? _endtime;
        private string _grade;
        private string _namerule;
        private string _remark;
        private string _require;
        private string _resourcefolder_id;
        private string _stage;
        private DateTime? _starttime;
        private string _subject;

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

        public DateTime? EndTime
        {
            get
            {
                return this._endtime;
            }
            set
            {
                this._endtime = value;
            }
        }

        public string Grade
        {
            get
            {
                return this._grade;
            }
            set
            {
                this._grade = value;
            }
        }

        public string NameRule
        {
            get
            {
                return this._namerule;
            }
            set
            {
                this._namerule = value;
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

        public string Require
        {
            get
            {
                return this._require;
            }
            set
            {
                this._require = value;
            }
        }

        public string ResourceFolder_Id
        {
            get
            {
                return this._resourcefolder_id;
            }
            set
            {
                this._resourcefolder_id = value;
            }
        }

        public string Stage
        {
            get
            {
                return this._stage;
            }
            set
            {
                this._stage = value;
            }
        }

        public DateTime? StartTime
        {
            get
            {
                return this._starttime;
            }
            set
            {
                this._starttime = value;
            }
        }

        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }
    }
}

