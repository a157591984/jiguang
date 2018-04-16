namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_FileSyncRecord
    {
        private string _filesyncrecord_id;
        private string _remark;
        private string _synclong;
        private DateTime? _synctime;
        private string _synctype;
        private string _syncuserid;
        private string _syncusername;

        public string FileSyncRecord_Id
        {
            get
            {
                return this._filesyncrecord_id;
            }
            set
            {
                this._filesyncrecord_id = value;
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

        public string SyncLong
        {
            get
            {
                return this._synclong;
            }
            set
            {
                this._synclong = value;
            }
        }

        public DateTime? SyncTime
        {
            get
            {
                return this._synctime;
            }
            set
            {
                this._synctime = value;
            }
        }

        public string SyncType
        {
            get
            {
                return this._synctype;
            }
            set
            {
                this._synctype = value;
            }
        }

        public string SyncUserId
        {
            get
            {
                return this._syncuserid;
            }
            set
            {
                this._syncuserid = value;
            }
        }

        public string SyncUserName
        {
            get
            {
                return this._syncusername;
            }
            set
            {
                this._syncusername = value;
            }
        }
    }
}

