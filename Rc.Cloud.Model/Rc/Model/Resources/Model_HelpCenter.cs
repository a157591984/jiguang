namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_HelpCenter
    {
        private DateTime? _create_time;
        private string _create_userid;
        private string _help_content;
        private string _help_id;
        private string _help_title;

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

        public string create_userid
        {
            get
            {
                return this._create_userid;
            }
            set
            {
                this._create_userid = value;
            }
        }

        public string help_content
        {
            get
            {
                return this._help_content;
            }
            set
            {
                this._help_content = value;
            }
        }

        public string help_id
        {
            get
            {
                return this._help_id;
            }
            set
            {
                this._help_id = value;
            }
        }

        public string help_title
        {
            get
            {
                return this._help_title;
            }
            set
            {
                this._help_title = value;
            }
        }
    }
}

