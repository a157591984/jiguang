namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_visit_web
    {
        private string _class_id;
        private DateTime? _close_time;
        private DateTime? _open_time;
        private string _resource_data_id;
        private string _user_id;
        private string _visit_web_id;

        public string class_id
        {
            get
            {
                return this._class_id;
            }
            set
            {
                this._class_id = value;
            }
        }

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

        public string visit_web_id
        {
            get
            {
                return this._visit_web_id;
            }
            set
            {
                this._visit_web_id = value;
            }
        }
    }
}

