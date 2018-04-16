namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SyncFileToSchool
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private string _resourcetoresourcefolder_id;
        private string _schoolid;
        private string _syncfiletoschool_id;

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

        public string SyncFileToSchool_Id
        {
            get
            {
                return this._syncfiletoschool_id;
            }
            set
            {
                this._syncfiletoschool_id = value;
            }
        }
    }
}

