namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_BookProductionLog
    {
        private string _bookid;
        private string _bookproductionlog_id;
        private DateTime _createtime;
        private string _createuser;
        private string _logremark;
        private string _logtypeenum;
        private int _particularyear;
        private string _resource_type;
        private string _resourcetoresourcefolder_id;

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

        public string BookProductionLog_Id
        {
            get
            {
                return this._bookproductionlog_id;
            }
            set
            {
                this._bookproductionlog_id = value;
            }
        }

        public DateTime CreateTime
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

        public int ParticularYear
        {
            get
            {
                return this._particularyear;
            }
            set
            {
                this._particularyear = value;
            }
        }

        public string Resource_Type
        {
            get
            {
                return this._resource_type;
            }
            set
            {
                this._resource_type = value;
            }
        }

        public string ResourceToResourceFolder_Id
        {
            get
            {
                return this._resourcetoresourcefolder_id;
            }
            set
            {
                this._resourcetoresourcefolder_id = value;
            }
        }
    }
}

