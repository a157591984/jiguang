namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatRes_Doc_Attr
    {
        private DateTime? _createtime;
        private string _id;
        private string _sattrtype;
        private string _sdata_id;
        private string _sdata_name;
        private string _sdoctype;
        private string _sdoctypename;
        private int? _sdownloadcount;
        private int? _sproductioncount;
        private int? _ssalecount;
        private int? _syear;

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

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string SAttrType
        {
            get
            {
                return this._sattrtype;
            }
            set
            {
                this._sattrtype = value;
            }
        }

        public string SData_ID
        {
            get
            {
                return this._sdata_id;
            }
            set
            {
                this._sdata_id = value;
            }
        }

        public string SData_Name
        {
            get
            {
                return this._sdata_name;
            }
            set
            {
                this._sdata_name = value;
            }
        }

        public string SDocType
        {
            get
            {
                return this._sdoctype;
            }
            set
            {
                this._sdoctype = value;
            }
        }

        public string SDocTypeName
        {
            get
            {
                return this._sdoctypename;
            }
            set
            {
                this._sdoctypename = value;
            }
        }

        public int? SDownloadCount
        {
            get
            {
                return this._sdownloadcount;
            }
            set
            {
                this._sdownloadcount = value;
            }
        }

        public int? SProductionCount
        {
            get
            {
                return this._sproductioncount;
            }
            set
            {
                this._sproductioncount = value;
            }
        }

        public int? SSaleCount
        {
            get
            {
                return this._ssalecount;
            }
            set
            {
                this._ssalecount = value;
            }
        }

        public int? SYear
        {
            get
            {
                return this._syear;
            }
            set
            {
                this._syear = value;
            }
        }
    }
}

