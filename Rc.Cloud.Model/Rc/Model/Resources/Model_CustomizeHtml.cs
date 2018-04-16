namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_CustomizeHtml
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _customizehtml_id;
        private string _htmlcontent;
        private string _htmltype;
        private string _remark;

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

        public string CustomizeHtml_Id
        {
            get
            {
                return this._customizehtml_id;
            }
            set
            {
                this._customizehtml_id = value;
            }
        }

        public string HtmlContent
        {
            get
            {
                return this._htmlcontent;
            }
            set
            {
                this._htmlcontent = value;
            }
        }

        public string HtmlType
        {
            get
            {
                return this._htmltype;
            }
            set
            {
                this._htmltype = value;
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
    }
}

