namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Bookshelves
    {
        private string _book_name;
        private string _bookbrief;
        private string _bookimg_url;
        private decimal? _bookprice;
        private string _bookshelvesstate;
        private DateTime? _createtime;
        private string _createuser;
        private DateTime? _putdowntime;
        private DateTime? _putuptime;
        private string _resourcefolder_id;

        public string Book_Name
        {
            get
            {
                return this._book_name;
            }
            set
            {
                this._book_name = value;
            }
        }

        public string BookBrief
        {
            get
            {
                return this._bookbrief;
            }
            set
            {
                this._bookbrief = value;
            }
        }

        public string BookImg_Url
        {
            get
            {
                return this._bookimg_url;
            }
            set
            {
                this._bookimg_url = value;
            }
        }

        public decimal? BookPrice
        {
            get
            {
                return this._bookprice;
            }
            set
            {
                this._bookprice = value;
            }
        }

        public string BookShelvesState
        {
            get
            {
                return this._bookshelvesstate;
            }
            set
            {
                this._bookshelvesstate = value;
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

        public DateTime? PutDownTime
        {
            get
            {
                return this._putdowntime;
            }
            set
            {
                this._putdowntime = value;
            }
        }

        public DateTime? PutUpTime
        {
            get
            {
                return this._putuptime;
            }
            set
            {
                this._putuptime = value;
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

