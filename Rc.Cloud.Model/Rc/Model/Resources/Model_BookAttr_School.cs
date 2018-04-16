namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_BookAttr_School
    {
        private string _attrenum;
        private string _attrvalue;
        private string _bookattr_school_id;
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _schoolid;

        public string AttrEnum
        {
            get
            {
                return this._attrenum;
            }
            set
            {
                this._attrenum = value;
            }
        }

        public string AttrValue
        {
            get
            {
                return this._attrvalue;
            }
            set
            {
                this._attrvalue = value;
            }
        }

        public string BookAttr_School_Id
        {
            get
            {
                return this._bookattr_school_id;
            }
            set
            {
                this._bookattr_school_id = value;
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

        public string SchoolId
        {
            get
            {
                return this._schoolid;
            }
            set
            {
                this._schoolid = value;
            }
        }
    }
}

