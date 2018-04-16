namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_HomeWork
    {
        private DateTime? _begintime;
        private string _correctmode;
        private DateTime? _createtime;
        private string _homework_assignteacher;
        private DateTime? _homework_finishtime;
        private string _homework_id;
        private string _homework_name;
        private int? _homework_status;
        private string _iscountdown;
        private int? _ishide;
        private int? _isshowanswer;
        private int? _istimelength;
        private int? _istimelimt;
        private string _resourcetoresourcefolder_id;
        private string _rtrfid_old;
        private DateTime? _stoptime;
        private string _subjectid;
        private string _usergroup_id;

        public DateTime? BeginTime
        {
            get
            {
                return this._begintime;
            }
            set
            {
                this._begintime = value;
            }
        }

        public string CorrectMode
        {
            get
            {
                return this._correctmode;
            }
            set
            {
                this._correctmode = value;
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

        public string HomeWork_AssignTeacher
        {
            get
            {
                return this._homework_assignteacher;
            }
            set
            {
                this._homework_assignteacher = value;
            }
        }

        public DateTime? HomeWork_FinishTime
        {
            get
            {
                return this._homework_finishtime;
            }
            set
            {
                this._homework_finishtime = value;
            }
        }

        public string HomeWork_Id
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

        public string HomeWork_Name
        {
            get
            {
                return this._homework_name;
            }
            set
            {
                this._homework_name = value;
            }
        }

        public int? HomeWork_Status
        {
            get
            {
                return this._homework_status;
            }
            set
            {
                this._homework_status = value;
            }
        }

        public string IsCountdown
        {
            get
            {
                return this._iscountdown;
            }
            set
            {
                this._iscountdown = value;
            }
        }

        public int? IsHide
        {
            get
            {
                return this._ishide;
            }
            set
            {
                this._ishide = value;
            }
        }

        public int? IsShowAnswer
        {
            get
            {
                return this._isshowanswer;
            }
            set
            {
                this._isshowanswer = value;
            }
        }

        public int? isTimeLength
        {
            get
            {
                return this._istimelength;
            }
            set
            {
                this._istimelength = value;
            }
        }

        public int? isTimeLimt
        {
            get
            {
                return this._istimelimt;
            }
            set
            {
                this._istimelimt = value;
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

        public string rtrfId_Old
        {
            get
            {
                return this._rtrfid_old;
            }
            set
            {
                this._rtrfid_old = value;
            }
        }

        public DateTime? StopTime
        {
            get
            {
                return this._stoptime;
            }
            set
            {
                this._stoptime = value;
            }
        }

        public string SubjectId
        {
            get
            {
                return this._subjectid;
            }
            set
            {
                this._subjectid = value;
            }
        }

        public string UserGroup_Id
        {
            get
            {
                return this._usergroup_id;
            }
            set
            {
                this._usergroup_id = value;
            }
        }
    }
}

