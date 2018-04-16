namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SendSMSRecord
    {
        private string _content;
        private DateTime _ctime;
        private string _mobile;
        private string _returncontent;
        private string _returnstatus;
        private string _schoolid;
        private string _sendsmsrecordid;
        private string _status;
        private string _stype;

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public DateTime CTime
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

        public string Mobile
        {
            get
            {
                return this._mobile;
            }
            set
            {
                this._mobile = value;
            }
        }

        public string ReturnContent
        {
            get
            {
                return this._returncontent;
            }
            set
            {
                this._returncontent = value;
            }
        }

        public string ReturnStatus
        {
            get
            {
                return this._returnstatus;
            }
            set
            {
                this._returnstatus = value;
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

        public string SendSMSRecordId
        {
            get
            {
                return this._sendsmsrecordid;
            }
            set
            {
                this._sendsmsrecordid = value;
            }
        }

        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
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
    }
}

