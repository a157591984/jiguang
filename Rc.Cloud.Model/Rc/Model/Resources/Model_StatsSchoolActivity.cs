namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsSchoolActivity
    {
        private int? _activity;
        private DateTime? _createtime;
        private string _datedata;
        private string _datetype;
        private int? _nactivity;
        private string _producttype;
        private string _resourceclass;
        private string _schoolid;
        private string _schoolname;
        private string _statsschoolactivity_id;

        public int? Activity
        {
            get
            {
                return this._activity;
            }
            set
            {
                this._activity = value;
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

        public string DateData
        {
            get
            {
                return this._datedata;
            }
            set
            {
                this._datedata = value;
            }
        }

        public string DateType
        {
            get
            {
                return this._datetype;
            }
            set
            {
                this._datetype = value;
            }
        }

        public int? NActivity
        {
            get
            {
                return this._nactivity;
            }
            set
            {
                this._nactivity = value;
            }
        }

        public string ProductType
        {
            get
            {
                return this._producttype;
            }
            set
            {
                this._producttype = value;
            }
        }

        public string ResourceClass
        {
            get
            {
                return this._resourceclass;
            }
            set
            {
                this._resourceclass = value;
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

        public string SchoolName
        {
            get
            {
                return this._schoolname;
            }
            set
            {
                this._schoolname = value;
            }
        }

        public string StatsSchoolActivity_Id
        {
            get
            {
                return this._statsschoolactivity_id;
            }
            set
            {
                this._statsschoolactivity_id = value;
            }
        }
    }
}

