namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsLog
    {
        private DateTime? _ctime;
        private string _cuser;
        private string _dataid;
        private string _dataname;
        private string _datatype;
        private string _gradeid;
        private string _logstatus;
        private string _statslogid;

        public DateTime? CTime
        {
            get
            {
                return this._ctime;
            }
            set
            {
                this._ctime = value;
            }
        }

        public string CUser
        {
            get
            {
                return this._cuser;
            }
            set
            {
                this._cuser = value;
            }
        }

        public string DataId
        {
            get
            {
                return this._dataid;
            }
            set
            {
                this._dataid = value;
            }
        }

        public string DataName
        {
            get
            {
                return this._dataname;
            }
            set
            {
                this._dataname = value;
            }
        }

        public string DataType
        {
            get
            {
                return this._datatype;
            }
            set
            {
                this._datatype = value;
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

        public string LogStatus
        {
            get
            {
                return this._logstatus;
            }
            set
            {
                this._logstatus = value;
            }
        }

        public string StatsLogId
        {
            get
            {
                return this._statslogid;
            }
            set
            {
                this._statslogid = value;
            }
        }
    }
}

