namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ResourceShare
    {
        private DateTime? _createtime;
        private string _createuserid;
        private string _remark;
        private string _resourceshareid;
        private string _resourcesharetype;
        private string _resourcetoresourcefolder_id;
        private string _shareobjectid;

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

        public string CreateUserId
        {
            get
            {
                return this._createuserid;
            }
            set
            {
                this._createuserid = value;
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

        public string ResourceShareId
        {
            get
            {
                return this._resourceshareid;
            }
            set
            {
                this._resourceshareid = value;
            }
        }

        public string ResourceShareType
        {
            get
            {
                return this._resourcesharetype;
            }
            set
            {
                this._resourcesharetype = value;
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

        public string ShareObjectId
        {
            get
            {
                return this._shareobjectid;
            }
            set
            {
                this._shareobjectid = value;
            }
        }
    }
}

