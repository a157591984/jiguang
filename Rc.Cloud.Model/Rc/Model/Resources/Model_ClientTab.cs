namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ClientTab
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _tabindex;
        private string _tabname;
        private string _tabtype;
        private DateTime? _updatetime;
        private string _updateuser;

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

        public string Tabindex
        {
            get
            {
                return this._tabindex;
            }
            set
            {
                this._tabindex = value;
            }
        }

        public string TabName
        {
            get
            {
                return this._tabname;
            }
            set
            {
                this._tabname = value;
            }
        }

        public string TabType
        {
            get
            {
                return this._tabtype;
            }
            set
            {
                this._tabtype = value;
            }
        }

        public DateTime? UpdateTime
        {
            get
            {
                return this._updatetime;
            }
            set
            {
                this._updatetime = value;
            }
        }

        public string UpdateUser
        {
            get
            {
                return this._updateuser;
            }
            set
            {
                this._updateuser = value;
            }
        }
    }
}

