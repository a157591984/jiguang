namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysUserRoleRelation
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _sysrole_id;
        private string _sysuser_id;
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

        public string SysUser_ID
        {
            get
            {
                return this._sysuser_id;
            }
            set
            {
                this._sysuser_id = value;
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

