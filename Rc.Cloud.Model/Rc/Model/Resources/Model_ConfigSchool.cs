namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ConfigSchool
    {
        private string _configenum;
        private DateTime? _d_createtime;
        private string _d_createuser;
        private string _d_name;
        private int? _d_order;
        private string _d_publicvalue;
        private string _d_remark;
        private string _d_type;
        private DateTime? _d_updatetime;
        private string _d_updateuser;
        private string _d_value;
        private string _mobile;
        private string _school_id;
        private string _school_name;
        private string _schoolip;

        public string ConfigEnum
        {
            get
            {
                return this._configenum;
            }
            set
            {
                this._configenum = value;
            }
        }

        public DateTime? D_CreateTime
        {
            get
            {
                return this._d_createtime;
            }
            set
            {
                this._d_createtime = value;
            }
        }

        public string D_CreateUser
        {
            get
            {
                return this._d_createuser;
            }
            set
            {
                this._d_createuser = value;
            }
        }

        public string D_Name
        {
            get
            {
                return this._d_name;
            }
            set
            {
                this._d_name = value;
            }
        }

        public int? D_Order
        {
            get
            {
                return this._d_order;
            }
            set
            {
                this._d_order = value;
            }
        }

        public string D_PublicValue
        {
            get
            {
                return this._d_publicvalue;
            }
            set
            {
                this._d_publicvalue = value;
            }
        }

        public string D_Remark
        {
            get
            {
                return this._d_remark;
            }
            set
            {
                this._d_remark = value;
            }
        }

        public string D_Type
        {
            get
            {
                return this._d_type;
            }
            set
            {
                this._d_type = value;
            }
        }

        public DateTime? D_UpdateTime
        {
            get
            {
                return this._d_updatetime;
            }
            set
            {
                this._d_updatetime = value;
            }
        }

        public string D_UpdateUser
        {
            get
            {
                return this._d_updateuser;
            }
            set
            {
                this._d_updateuser = value;
            }
        }

        public string D_Value
        {
            get
            {
                return this._d_value;
            }
            set
            {
                this._d_value = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this._mobile;
            }
            set
            {
                this._mobile = value;
            }
        }

        public string School_ID
        {
            get
            {
                return this._school_id;
            }
            set
            {
                this._school_id = value;
            }
        }

        public string School_Name
        {
            get
            {
                return this._school_name;
            }
            set
            {
                this._school_name = value;
            }
        }

        public string SchoolIP
        {
            get
            {
                return this._schoolip;
            }
            set
            {
                this._schoolip = value;
            }
        }
    }
}

