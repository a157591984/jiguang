namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SystemLog
    {
        private DateTime? _systemlog_createdate;
        private string _systemlog_desc;
        private string _systemlog_drugid;
        private string _systemlog_id;
        private string _systemlog_ip;
        private string _systemlog_level;
        private string _systemlog_loginid;
        private string _systemlog_model;
        private string _systemlog_remark;
        private int? _systemlog_source;
        private string _systemlog_tabledataid;
        private string _systemlog_tablename;
        private int? _systemlog_type;

        public DateTime? SystemLog_CreateDate
        {
            get
            {
                return this._systemlog_createdate;
            }
            set
            {
                this._systemlog_createdate = value;
            }
        }

        public string SystemLog_Desc
        {
            get
            {
                return this._systemlog_desc;
            }
            set
            {
                this._systemlog_desc = value;
            }
        }

        public string SystemLog_DrugID
        {
            get
            {
                return this._systemlog_drugid;
            }
            set
            {
                this._systemlog_drugid = value;
            }
        }

        public string SystemLog_ID
        {
            get
            {
                return this._systemlog_id;
            }
            set
            {
                this._systemlog_id = value;
            }
        }

        public string SystemLog_IP
        {
            get
            {
                return this._systemlog_ip;
            }
            set
            {
                this._systemlog_ip = value;
            }
        }

        public string SystemLog_Level
        {
            get
            {
                return this._systemlog_level;
            }
            set
            {
                this._systemlog_level = value;
            }
        }

        public string SystemLog_LoginID
        {
            get
            {
                return this._systemlog_loginid;
            }
            set
            {
                this._systemlog_loginid = value;
            }
        }

        public string SystemLog_Model
        {
            get
            {
                return this._systemlog_model;
            }
            set
            {
                this._systemlog_model = value;
            }
        }

        public string SystemLog_Remark
        {
            get
            {
                return this._systemlog_remark;
            }
            set
            {
                this._systemlog_remark = value;
            }
        }

        public int? SystemLog_Source
        {
            get
            {
                return this._systemlog_source;
            }
            set
            {
                this._systemlog_source = value;
            }
        }

        public string SystemLog_TableDataID
        {
            get
            {
                return this._systemlog_tabledataid;
            }
            set
            {
                this._systemlog_tabledataid = value;
            }
        }

        public string SystemLog_TableName
        {
            get
            {
                return this._systemlog_tablename;
            }
            set
            {
                this._systemlog_tablename = value;
            }
        }

        public int? SystemLog_Type
        {
            get
            {
                return this._systemlog_type;
            }
            set
            {
                this._systemlog_type = value;
            }
        }
    }
}

