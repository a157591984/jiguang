namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysUser
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _sysdepartment_id;
        private bool _sysuser_enable;
        private string _sysuser_id;
        private string _sysuser_loginname;
        private string _sysuser_name;
        private string _sysuser_password;
        private string _sysuser_remark;
        private string _sysuser_tel;
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

        public string SysDepartment_ID
        {
            get
            {
                return this._sysdepartment_id;
            }
            set
            {
                this._sysdepartment_id = value;
            }
        }

        public bool SysUser_Enable
        {
            get
            {
                return this._sysuser_enable;
            }
            set
            {
                this._sysuser_enable = value;
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

        public string SysUser_LoginName
        {
            get
            {
                return this._sysuser_loginname;
            }
            set
            {
                this._sysuser_loginname = value;
            }
        }

        public string SysUser_Name
        {
            get
            {
                return this._sysuser_name;
            }
            set
            {
                this._sysuser_name = value;
            }
        }

        public string SysUser_PassWord
        {
            get
            {
                return this._sysuser_password;
            }
            set
            {
                this._sysuser_password = value;
            }
        }

        public string SysUser_Remark
        {
            get
            {
                return this._sysuser_remark;
            }
            set
            {
                this._sysuser_remark = value;
            }
        }

        public string SysUser_Tel
        {
            get
            {
                return this._sysuser_tel;
            }
            set
            {
                this._sysuser_tel = value;
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

