namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_BookArea
    {
        private string _book_name;
        private string _bookarea_id;
        private string _city_id;
        private string _county_id;
        private DateTime? _createtime;
        private string _createuser;
        private string _province_id;
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

        public string BookArea_ID
        {
            get
            {
                return this._bookarea_id;
            }
            set
            {
                this._bookarea_id = value;
            }
        }

        public string City_ID
        {
            get
            {
                return this._city_id;
            }
            set
            {
                this._city_id = value;
            }
        }

        public string County_ID
        {
            get
            {
                return this._county_id;
            }
            set
            {
                this._county_id = value;
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

        public string Province_ID
        {
            get
            {
                return this._province_id;
            }
            set
            {
                this._province_id = value;
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

