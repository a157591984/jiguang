namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_S_KnowledgePointBasic
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _gradeterm;
        private string _kpnamebasic;
        private string _s_knowledgepointbasic_id;
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

        public string KPNameBasic
        {
            get
            {
                return this._kpnamebasic;
            }
            set
            {
                this._kpnamebasic = value;
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

