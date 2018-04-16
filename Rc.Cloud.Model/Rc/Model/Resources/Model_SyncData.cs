namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SyncData
    {
        private DateTime? _createtime;
        private string _dataid;
        private string _operatetype;
        private string _syncdataid;
        private string _syncstatus = "0";
        private string _tablename;

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

        public string OperateType
        {
            get
            {
                return this._operatetype;
            }
            set
            {
                this._operatetype = value;
            }
        }

        public string SyncDataId
        {
            get
            {
                return this._syncdataid;
            }
            set
            {
                this._syncdataid = value;
            }
        }

        public string SyncStatus
        {
            get
            {
                return this._syncstatus;
            }
            set
            {
                this._syncstatus = value;
            }
        }

        public string TableName
        {
            get
            {
                return this._tablename;
            }
            set
            {
                this._tablename = value;
            }
        }
    }
}

