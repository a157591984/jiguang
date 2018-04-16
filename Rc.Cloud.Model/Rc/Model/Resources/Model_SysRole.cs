namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysRole
    {
        private DateTime? _createtime;
        private string _createuser;
        private bool _sysrole_enable;
        private string _sysrole_id;
        private string _sysrole_name;
        private int? _sysrole_order;
        private string _sysrole_remark;
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

        public bool SysRole_Enable
        {
            get
            {
                return this._sysrole_enable;
            }
            set
            {
                this._sysrole_enable = value;
            }
        }

        public string SysRole_ID
        {
            get
            {
                return this._sysrole_id;
            }
            set
            {
                this._sysrole_id = value;
            }
        }

        public string SysRole_Name
        {
            get
            {
                return this._sysrole_name;
            }
            set
            {
                this._sysrole_name = value;
            }
        }

        public int? SysRole_Order
        {
            get
            {
                return this._sysrole_order;
            }
            set
            {
                this._sysrole_order = value;
            }
        }

        public string SysRole_Remark
        {
            get
            {
                return this._sysrole_remark;
            }
            set
            {
                this._sysrole_remark = value;
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

