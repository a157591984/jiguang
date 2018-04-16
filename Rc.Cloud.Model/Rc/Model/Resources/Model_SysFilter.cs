namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysFilter
    {
        private DateTime? _create_time;
        private string _create_userid;
        private string _keyword;
        private string _sysfilter_id;

        public DateTime? Create_Time
        {
            get
            {
                return this._create_time;
            }
            set
            {
                this._create_time = value;
            }
        }

        public string Create_UserId
        {
            get
            {
                return this._create_userid;
            }
            set
            {
                this._create_userid = value;
            }
        }

        public string KeyWord
        {
            get
            {
                return this._keyword;
            }
            set
            {
                this._keyword = value;
            }
        }

        public string SysFilter_id
        {
            get
            {
                return this._sysfilter_id;
            }
            set
            {
                this._sysfilter_id = value;
            }
        }
    }
}

