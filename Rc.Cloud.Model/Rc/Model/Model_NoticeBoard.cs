namespace Rc.Model
{
    using System;

    [Serializable]
    public class Model_NoticeBoard
    {
        private DateTime? _create_time;
        private string _create_userid;
        private DateTime? _end_time;
        private string _notice_content;
        private string _notice_id;
        private string _notice_title;
        private DateTime? _start_time;

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

        public DateTime? end_time
        {
            get
            {
                return this._end_time;
            }
            set
            {
                this._end_time = value;
            }
        }

        public string notice_content
        {
            get
            {
                return this._notice_content;
            }
            set
            {
                this._notice_content = value;
            }
        }

        public string notice_id
        {
            get
            {
                return this._notice_id;
            }
            set
            {
                this._notice_id = value;
            }
        }

        public string notice_title
        {
            get
            {
                return this._notice_title;
            }
            set
            {
                this._notice_title = value;
            }
        }

        public DateTime? start_time
        {
            get
            {
                return this._start_time;
            }
            set
            {
                this._start_time = value;
            }
        }
    }
}

