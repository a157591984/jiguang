namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysDepartment
    {
        private DateTime? _createtime;
        private string _createuser;
        private bool _sysdepartment_enable;
        private string _sysdepartment_id;
        private string _sysdepartment_name;
        private string _sysdepartment_parentid;
        private string _sysdepartment_remark;
        private string _sysdepartment_tel;
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

        public bool SysDepartment_Enable
        {
            get
            {
                return this._sysdepartment_enable;
            }
            set
            {
                this._sysdepartment_enable = value;
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

        public string SysDepartment_Name
        {
            get
            {
                return this._sysdepartment_name;
            }
            set
            {
                this._sysdepartment_name = value;
            }
        }

        public string SysDepartment_ParentID
        {
            get
            {
                return this._sysdepartment_parentid;
            }
            set
            {
                this._sysdepartment_parentid = value;
            }
        }

        public string SysDepartment_Remark
        {
            get
            {
                return this._sysdepartment_remark;
            }
            set
            {
                this._sysdepartment_remark = value;
            }
        }

        public string SysDepartment_Tel
        {
            get
            {
                return this._sysdepartment_tel;
            }
            set
            {
                this._sysdepartment_tel = value;
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

