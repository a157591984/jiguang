namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_S_MeasureTarget
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _gradeterm;
        private string _mtcode;
        private string _mtlevel;
        private string _mtname;
        private string _parent_id;
        private string _resource_version;
        private string _s_measuretarget_id;
        private string _subject;
        private DateTime? _updatetime;
        private string _updateuser;

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

        public string GradeTerm
        {
            get
            {
                return this._gradeterm;
            }
            set
            {
                this._gradeterm = value;
            }
        }

        public string MTCode
        {
            get
            {
                return this._mtcode;
            }
            set
            {
                this._mtcode = value;
            }
        }

        public string MTLevel
        {
            get
            {
                return this._mtlevel;
            }
            set
            {
                this._mtlevel = value;
            }
        }

        public string MTName
        {
            get
            {
                return this._mtname;
            }
            set
            {
                this._mtname = value;
            }
        }

        public string Parent_Id
        {
            get
            {
                return this._parent_id;
            }
            set
            {
                this._parent_id = value;
            }
        }

        public string Resource_Version
        {
            get
            {
                return this._resource_version;
            }
            set
            {
                this._resource_version = value;
            }
        }

        public string S_MeasureTarget_Id
        {
            get
            {
                return this._s_measuretarget_id;
            }
            set
            {
                this._s_measuretarget_id = value;
            }
        }

        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }

        public DateTime? UpdateTime
        {
            get
            {
                return this._updatetime;
            }
            set
            {
                this._updatetime = value;
            }
        }

        public string UpdateUser
        {
            get
            {
                return this._updateuser;
            }
            set
            {
                this._updateuser = value;
            }
        }
    }
}

