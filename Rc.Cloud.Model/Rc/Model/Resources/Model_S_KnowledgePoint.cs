namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_S_KnowledgePoint
    {
        private string _book_type;
        private string _cognitive_level;
        private DateTime? _createtime;
        private string _createuser;
        private string _gradeterm;
        private string _islast;
        private string _kpcode;
        private string _kplevel;
        private string _kpname;
        private string _parent_id;
        private string _resource_version;
        private string _s_knowledgepoint_id;
        private string _s_knowledgepointbasic_id;
        private string _subject;
        private DateTime? _updatetime;
        private string _updateuser;

        public string Book_Type
        {
            get
            {
                return this._book_type;
            }
            set
            {
                this._book_type = value;
            }
        }

        public string Cognitive_Level
        {
            get
            {
                return this._cognitive_level;
            }
            set
            {
                this._cognitive_level = value;
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

        public string IsLast
        {
            get
            {
                return this._islast;
            }
            set
            {
                this._islast = value;
            }
        }

        public string KPCode
        {
            get
            {
                return this._kpcode;
            }
            set
            {
                this._kpcode = value;
            }
        }

        public string KPLevel
        {
            get
            {
                return this._kplevel;
            }
            set
            {
                this._kplevel = value;
            }
        }

        public string KPName
        {
            get
            {
                return this._kpname;
            }
            set
            {
                this._kpname = value;
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

        public string S_KnowledgePoint_Id
        {
            get
            {
                return this._s_knowledgepoint_id;
            }
            set
            {
                this._s_knowledgepoint_id = value;
            }
        }

        public string S_KnowledgePointBasic_Id
        {
            get
            {
                return this._s_knowledgepointbasic_id;
            }
            set
            {
                this._s_knowledgepointbasic_id = value;
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

