namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_BookAttrbute
    {
        private string _attrenum;
        private string _attrvalue = "0";
        private string _bookattrid;
        private DateTime? _createtime = new DateTime?(DateTime.Now);
        private string _createuser;
        private string _resourcefolder_id;

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

        public string BookAttrId
        {
            get
            {
                return this._bookattrid;
            }
            set
            {
                this._bookattrid = value;
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

        public string ResourceFolder_Id
        {
            get
            {
                return this._resourcefolder_id;
            }
            set
            {
                this._resourcefolder_id = value;
            }
        }
    }
}

