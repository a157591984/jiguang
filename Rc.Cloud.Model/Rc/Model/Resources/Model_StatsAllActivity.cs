namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsAllActivity
    {
        private int? _activity;
        private DateTime? _createtime;
        private string _datedata;
        private string _datetype;
        private int? _nactivity;
        private string _producttype;
        private string _resourceclass;
        private string _statsallactivity_id;

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

        public string StatsAllActivity_Id
        {
            get
            {
                return this._statsallactivity_id;
            }
            set
            {
                this._statsallactivity_id = value;
            }
        }
    }
}

