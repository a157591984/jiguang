namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ClientTabToTab
    {
        private string _clienttabtotab_id;
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _tabindex;
        private string _totabindex;

        public string ClientTabToTab_Id
        {
            get
            {
                return this._clienttabtotab_id;
            }
            set
            {
                this._clienttabtotab_id = value;
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

        public string ToTabindex
        {
            get
            {
                return this._totabindex;
            }
            set
            {
                this._totabindex = value;
            }
        }
    }
}

