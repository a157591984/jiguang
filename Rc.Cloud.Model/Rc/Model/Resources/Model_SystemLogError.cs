namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SystemLogError
    {
        private DateTime? _systemlog_createdate;
        private string _systemlog_desc;
        private string _systemlog_id;
        private string _systemlog_ip;
        private string _systemlog_loginid;
        private string _systemlog_pagepath;
        private int? _systemlog_source;
        private string _systemlog_syspath;

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

        public string SystemLog_PagePath
        {
            get
            {
                return this._systemlog_pagepath;
            }
            set
            {
                this._systemlog_pagepath = value;
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

        public string SystemLog_SysPath
        {
            get
            {
                return this._systemlog_syspath;
            }
            set
            {
                this._systemlog_syspath = value;
            }
        }
    }
}

