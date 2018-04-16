namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ResourceToResourceFolder_Property
    {
        private string _bookscode;
        private string _booksunitcode;
        private DateTime? _createtime;
        private string _guiddoc;
        private string _paperheaderdoc;
        private string _paperheaderhtml;
        private string _resourcetoresourcefolder_id;
        private string _testpapername;

        public string BooksCode
        {
            get
            {
                return this._bookscode;
            }
            set
            {
                this._bookscode = value;
            }
        }

        public string BooksUnitCode
        {
            get
            {
                return this._booksunitcode;
            }
            set
            {
                this._booksunitcode = value;
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

        public string GuidDoc
        {
            get
            {
                return this._guiddoc;
            }
            set
            {
                this._guiddoc = value;
            }
        }

        public string paperHeaderDoc
        {
            get
            {
                return this._paperheaderdoc;
            }
            set
            {
                this._paperheaderdoc = value;
            }
        }

        public string paperHeaderHtml
        {
            get
            {
                return this._paperheaderhtml;
            }
            set
            {
                this._paperheaderhtml = value;
            }
        }

        public string ResourceToResourceFolder_Id
        {
            get
            {
                return this._resourcetoresourcefolder_id;
            }
            set
            {
                this._resourcetoresourcefolder_id = value;
            }
        }

        public string TestPaperName
        {
            get
            {
                return this._testpapername;
            }
            set
            {
                this._testpapername = value;
            }
        }
    }
}

