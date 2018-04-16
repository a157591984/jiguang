namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_UserBuyResources
    {
        private string _book_id;
        private decimal? _bookprice;
        private string _buytype;
        private DateTime? _createtime;
        private string _createuser;
        private string _userbuyresources_id;
        private string _userid;

        public string Book_id
        {
            get
            {
                return this._book_id;
            }
            set
            {
                this._book_id = value;
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

        public string BuyType
        {
            get
            {
                return this._buytype;
            }
            set
            {
                this._buytype = value;
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

        public string UserBuyResources_ID
        {
            get
            {
                return this._userbuyresources_id;
            }
            set
            {
                this._userbuyresources_id = value;
            }
        }

        public string UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }
    }
}

