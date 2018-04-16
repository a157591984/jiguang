namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_S_TestingPointBasic
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _gradeterm;
        private string _s_testingpointbasic_id;
        private string _subject;
        private string _tpnamebasic;
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

        public string S_TestingPointBasic_Id
        {
            get
            {
                return this._s_testingpointbasic_id;
            }
            set
            {
                this._s_testingpointbasic_id = value;
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

        public string TPNameBasic
        {
            get
            {
                return this._tpnamebasic;
            }
            set
            {
                this._tpnamebasic = value;
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

