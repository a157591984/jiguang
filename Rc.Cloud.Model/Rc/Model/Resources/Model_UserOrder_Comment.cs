namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_UserOrder_Comment
    {
        private string _comment_content;
        private decimal? _comment_evaluate;
        private string _comment_id;
        private DateTime? _create_time;
        private string _order_num;
        private string _remark;
        private string _user_id;

        public string comment_content
        {
            get
            {
                return this._comment_content;
            }
            set
            {
                this._comment_content = value;
            }
        }

        public decimal? comment_evaluate
        {
            get
            {
                return this._comment_evaluate;
            }
            set
            {
                this._comment_evaluate = value;
            }
        }

        public string comment_id
        {
            get
            {
                return this._comment_id;
            }
            set
            {
                this._comment_id = value;
            }
        }

        public DateTime? create_time
        {
            get
            {
                return this._create_time;
            }
            set
            {
                this._create_time = value;
            }
        }

        public string order_num
        {
            get
            {
                return this._order_num;
            }
            set
            {
                this._order_num = value;
            }
        }

        public string remark
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

        public string user_id
        {
            get
            {
                return this._user_id;
            }
            set
            {
                this._user_id = value;
            }
        }
    }
}

