namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsStuHW_KP
    {
        private DateTime? _createtime;
        private string _homework_id;
        private DateTime? _homeworkcreatetime;
        private decimal? _kpmastery;
        private string _kpnamebasic;
        private string _s_knowledgepoint_id;
        private string _statsstuhw_kp_id;
        private string _student_homework_id;
        private string _student_id;
        private decimal? _sumscore;
        private string _testtype;
        private string _topicnumber;
        private string _topicnumber_right;
        private string _topicnumber_wrong;

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

        public string HomeWork_Id
        {
            get
            {
                return this._homework_id;
            }
            set
            {
                this._homework_id = value;
            }
        }

        public DateTime? HomeWorkCreateTime
        {
            get
            {
                return this._homeworkcreatetime;
            }
            set
            {
                this._homeworkcreatetime = value;
            }
        }

        public decimal? KPMastery
        {
            get
            {
                return this._kpmastery;
            }
            set
            {
                this._kpmastery = value;
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

        public string StatsStuHW_KP_Id
        {
            get
            {
                return this._statsstuhw_kp_id;
            }
            set
            {
                this._statsstuhw_kp_id = value;
            }
        }

        public string Student_HomeWork_Id
        {
            get
            {
                return this._student_homework_id;
            }
            set
            {
                this._student_homework_id = value;
            }
        }

        public string Student_Id
        {
            get
            {
                return this._student_id;
            }
            set
            {
                this._student_id = value;
            }
        }

        public decimal? SumScore
        {
            get
            {
                return this._sumscore;
            }
            set
            {
                this._sumscore = value;
            }
        }

        public string TestType
        {
            get
            {
                return this._testtype;
            }
            set
            {
                this._testtype = value;
            }
        }

        public string topicNumber
        {
            get
            {
                return this._topicnumber;
            }
            set
            {
                this._topicnumber = value;
            }
        }

        public string topicNumber_Right
        {
            get
            {
                return this._topicnumber_right;
            }
            set
            {
                this._topicnumber_right = value;
            }
        }

        public string topicNumber_Wrong
        {
            get
            {
                return this._topicnumber_wrong;
            }
            set
            {
                this._topicnumber_wrong = value;
            }
        }
    }
}

