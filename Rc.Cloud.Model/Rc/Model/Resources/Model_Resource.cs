namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Resource
    {
        private DateTime? _createtime;
        private string _resource_contenthtml;
        private decimal? _resource_contentlength;
        private string _resource_datastrem;
        private string _resource_id;
        private string _resource_md5;

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

        public string Resource_ContentHtml
        {
            get
            {
                return this._resource_contenthtml;
            }
            set
            {
                this._resource_contenthtml = value;
            }
        }

        public decimal? Resource_ContentLength
        {
            get
            {
                return this._resource_contentlength;
            }
            set
            {
                this._resource_contentlength = value;
            }
        }

        public string Resource_DataStrem
        {
            get
            {
                return this._resource_datastrem;
            }
            set
            {
                this._resource_datastrem = value;
            }
        }

        public string Resource_Id
        {
            get
            {
                return this._resource_id;
            }
            set
            {
                this._resource_id = value;
            }
        }

        public string Resource_MD5
        {
            get
            {
                return this._resource_md5;
            }
            set
            {
                this._resource_md5 = value;
            }
        }
    }
}

