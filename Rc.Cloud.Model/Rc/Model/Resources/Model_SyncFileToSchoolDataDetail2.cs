namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SyncFileToSchoolDataDetail2
    {
        private string _bookid;
        private string _bookname;
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _resource_name;
        private string _resource_type;
        private string _resource_typename;
        private string _resourcetoresourcefolder_id;
        private string _schoolid;
        private string _schoolname;
        private string _syncfiletoschooldatadetail2_id;

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

        public string BookName
        {
            get
            {
                return this._bookname;
            }
            set
            {
                this._bookname = value;
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

        public string Remark
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

        public string Resource_Name
        {
            get
            {
                return this._resource_name;
            }
            set
            {
                this._resource_name = value;
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

        public string Resource_TypeName
        {
            get
            {
                return this._resource_typename;
            }
            set
            {
                this._resource_typename = value;
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

        public string SchoolId
        {
            get
            {
                return this._schoolid;
            }
            set
            {
                this._schoolid = value;
            }
        }

        public string SchoolName
        {
            get
            {
                return this._schoolname;
            }
            set
            {
                this._schoolname = value;
            }
        }

        public string SyncFileToSchoolDataDetail2_Id
        {
            get
            {
                return this._syncfiletoschooldatadetail2_id;
            }
            set
            {
                this._syncfiletoschooldatadetail2_id = value;
            }
        }
    }
}

