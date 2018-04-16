namespace Rc.Cloud.Model
{
    using System;

    [Serializable]
    public class Model_SysProduct
    {
        private DateTime? _sys_createtime;
        private string _sys_createuser;
        private DateTime? _sys_updatetime;
        private string _sys_updateuser;
        private string _syscode;
        private string _sysicon;
        private string _sysname;
        private int? _sysorder;
        private string _sysurl;

        public DateTime? Sys_CreateTime
        {
            get
            {
                return this._sys_createtime;
            }
            set
            {
                this._sys_createtime = value;
            }
        }

        public string Sys_CreateUser
        {
            get
            {
                return this._sys_createuser;
            }
            set
            {
                this._sys_createuser = value;
            }
        }

        public DateTime? Sys_UpdateTime
        {
            get
            {
                return this._sys_updatetime;
            }
            set
            {
                this._sys_updatetime = value;
            }
        }

        public string Sys_UpdateUser
        {
            get
            {
                return this._sys_updateuser;
            }
            set
            {
                this._sys_updateuser = value;
            }
        }

        public string SysCode
        {
            get
            {
                return this._syscode;
            }
            set
            {
                this._syscode = value;
            }
        }

        public string SysIcon
        {
            get
            {
                return this._sysicon;
            }
            set
            {
                this._sysicon = value;
            }
        }

        public string SysName
        {
            get
            {
                return this._sysname;
            }
            set
            {
                this._sysname = value;
            }
        }

        public int? SysOrder
        {
            get
            {
                return this._sysorder;
            }
            set
            {
                this._sysorder = value;
            }
        }

        public string SysURL
        {
            get
            {
                return this._sysurl;
            }
            set
            {
                this._sysurl = value;
            }
        }
    }
}

