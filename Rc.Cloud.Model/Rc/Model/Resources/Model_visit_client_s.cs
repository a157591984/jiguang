namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_visit_client_s
    {
        private DateTime? _close_time;
        private DateTime? _open_time;
        private string _operate_type;
        private string _product_type;
        private string _resource_data_id;
        private string _tab_id;
        private string _user_id;
        private string _visit_client_id;

        public DateTime? close_time
        {
            get
            {
                return this._close_time;
            }
            set
            {
                this._close_time = value;
            }
        }

        public DateTime? open_time
        {
            get
            {
                return this._open_time;
            }
            set
            {
                this._open_time = value;
            }
        }

        public string operate_type
        {
            get
            {
                return this._operate_type;
            }
            set
            {
                this._operate_type = value;
            }
        }

        public string product_type
        {
            get
            {
                return this._product_type;
            }
            set
            {
                this._product_type = value;
            }
        }

        public string resource_data_id
        {
            get
            {
                return this._resource_data_id;
            }
            set
            {
                this._resource_data_id = value;
            }
        }

        public string tab_id
        {
            get
            {
                return this._tab_id;
            }
            set
            {
                this._tab_id = value;
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

        public string visit_client_id
        {
            get
            {
                return this._visit_client_id;
            }
            set
            {
                this._visit_client_id = value;
            }
        }
    }
}

