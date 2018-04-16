namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_S_TestQuestions_TP
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _resourcetoresourcefolder_id;
        private string _s_testingpoint_id;
        private string _s_testquestions_tp_id;
        private string _testquestions_id;
        private string _testquestions_score_id;

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

        public string S_TestingPoint_Id
        {
            get
            {
                return this._s_testingpoint_id;
            }
            set
            {
                this._s_testingpoint_id = value;
            }
        }

        public string S_TestQuestions_TP_Id
        {
            get
            {
                return this._s_testquestions_tp_id;
            }
            set
            {
                this._s_testquestions_tp_id = value;
            }
        }

        public string TestQuestions_Id
        {
            get
            {
                return this._testquestions_id;
            }
            set
            {
                this._testquestions_id = value;
            }
        }

        public string TestQuestions_Score_ID
        {
            get
            {
                return this._testquestions_score_id;
            }
            set
            {
                this._testquestions_score_id = value;
            }
        }
    }
}

