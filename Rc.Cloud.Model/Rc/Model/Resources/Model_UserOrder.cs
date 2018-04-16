namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_UserOrder
    {
        private string _book_id;
        private string _book_name;
        private decimal _book_price;
        private string _bookimg_url;
        private string _trade_no;
        private string _trade_status;
        private string _userid;
        private decimal _userorder_amount;
        private DateTime _userorder_finishtime = DateTime.Now;
        private string _userorder_id;
        private string _userorder_no;
        private string _userorder_paytool;
        private string _userorder_remark;
        private int _userorder_status;
        private DateTime _userorder_time = DateTime.Now;
        private string _userorder_type = "1";
        private string _usreorder_buyeremail;

        public string Book_Id
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

        public decimal Book_Price
        {
            get
            {
                return this._book_price;
            }
            set
            {
                this._book_price = value;
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

        public string trade_no
        {
            get
            {
                return this._trade_no;
            }
            set
            {
                this._trade_no = value;
            }
        }

        public string trade_status
        {
            get
            {
                return this._trade_status;
            }
            set
            {
                this._trade_status = value;
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

        public decimal UserOrder_Amount
        {
            get
            {
                return this._userorder_amount;
            }
            set
            {
                this._userorder_amount = value;
            }
        }

        public DateTime UserOrder_FinishTime
        {
            get
            {
                return this._userorder_finishtime;
            }
            set
            {
                this._userorder_finishtime = value;
            }
        }

        public string UserOrder_Id
        {
            get
            {
                return this._userorder_id;
            }
            set
            {
                this._userorder_id = value;
            }
        }

        public string UserOrder_No
        {
            get
            {
                return this._userorder_no;
            }
            set
            {
                this._userorder_no = value;
            }
        }

        public string UserOrder_Paytool
        {
            get
            {
                return this._userorder_paytool;
            }
            set
            {
                this._userorder_paytool = value;
            }
        }

        public string UserOrder_Remark
        {
            get
            {
                return this._userorder_remark;
            }
            set
            {
                this._userorder_remark = value;
            }
        }

        public int UserOrder_Status
        {
            get
            {
                return this._userorder_status;
            }
            set
            {
                this._userorder_status = value;
            }
        }

        public DateTime UserOrder_Time
        {
            get
            {
                return this._userorder_time;
            }
            set
            {
                this._userorder_time = value;
            }
        }

        public string UserOrder_Type
        {
            get
            {
                return this._userorder_type;
            }
            set
            {
                this._userorder_type = value;
            }
        }

        public string UsreOrder_Buyeremail
        {
            get
            {
                return this._usreorder_buyeremail;
            }
            set
            {
                this._usreorder_buyeremail = value;
            }
        }
    }
}

