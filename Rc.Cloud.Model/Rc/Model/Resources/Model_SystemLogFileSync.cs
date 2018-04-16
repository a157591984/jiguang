namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SystemLogFileSync
    {
        private DateTime? _createtime;
        private string _errormark;
        private decimal? _filesize;
        private string _msgtype;
        private string _syncurl;
        private string _systemlogfilesyncid;

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

        public string ErrorMark
        {
            get
            {
                return this._errormark;
            }
            set
            {
                this._errormark = value;
            }
        }

        public decimal? FileSize
        {
            get
            {
                return this._filesize;
            }
            set
            {
                this._filesize = value;
            }
        }

        public string MsgType
        {
            get
            {
                return this._msgtype;
            }
            set
            {
                this._msgtype = value;
            }
        }

        public string SyncUrl
        {
            get
            {
                return this._syncurl;
            }
            set
            {
                this._syncurl = value;
            }
        }

        public string SystemLogFileSyncID
        {
            get
            {
                return this._systemlogfilesyncid;
            }
            set
            {
                this._systemlogfilesyncid = value;
            }
        }
    }
}

