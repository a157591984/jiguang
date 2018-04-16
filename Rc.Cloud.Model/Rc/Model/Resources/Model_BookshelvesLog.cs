namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_BookshelvesLog
    {
        private string _bookid;
        private string _bookshelveslog_id;
        private DateTime? _createtime;
        private string _createuser;
        private string _logremark;
        private string _logtypeenum;

        public string BookId
        {
            get
            {
                return this._bookid;
            }
            set
            {
                this._bookid = value;
            }
        }

        public string BookshelvesLog_Id
        {
            get
            {
                return this._bookshelveslog_id;
            }
            set
            {
                this._bookshelveslog_id = value;
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

        public string LogRemark
        {
            get
            {
                return this._logremark;
            }
            set
            {
                this._logremark = value;
            }
        }

        public string LogTypeEnum
        {
            get
            {
                return this._logtypeenum;
            }
            set
            {
                this._logtypeenum = value;
            }
        }
    }
}

