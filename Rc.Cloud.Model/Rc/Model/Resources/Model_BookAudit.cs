namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_BookAudit
    {
        private string _auditremark;
        private string _auditstate;
        private string _book_name;
        private DateTime? _createtime;
        private string _createuser;
        private string _resourcefolder_id;

        public string AuditRemark
        {
            get
            {
                return this._auditremark;
            }
            set
            {
                this._auditremark = value;
            }
        }

        public string AuditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
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

        public string ResourceFolder_Id
        {
            get
            {
                return this._resourcefolder_id;
            }
            set
            {
                this._resourcefolder_id = value;
            }
        }
    }
}

