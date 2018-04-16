namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsStuHW_Analysis_KP
    {
        private string _book_type;
        private string _complexitytext;
        private DateTime? _createtime;
        private decimal? _gkscore;
        private int? _hwcount;
        private int? _kpimportant;
        private decimal? _kpmastery;
        private string _kpnamebasic;
        private string _parent_id;
        private string _resource_version;
        private string _s_knowledgepoint_id;
        private decimal? _score;
        private string _statsstuhw_analysis_kp_id;
        private string _student_id;
        private string _subject;
        private decimal? _totalscore;
        private int? _tqcount_right;
        private int? _tqcount_wrong;
        private decimal? _tqmastery_no;

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

        public string ComplexityText
        {
            get
            {
                return this._complexitytext;
            }
            set
            {
                this._complexitytext = value;
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

        public decimal? GKScore
        {
            get
            {
                return this._gkscore;
            }
            set
            {
                this._gkscore = value;
            }
        }

        public int? HWCount
        {
            get
            {
                return this._hwcount;
            }
            set
            {
                this._hwcount = value;
            }
        }

        public int? KPImportant
        {
            get
            {
                return this._kpimportant;
            }
            set
            {
                this._kpimportant = value;
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

        public decimal? Score
        {
            get
            {
                return this._score;
            }
            set
            {
                this._score = value;
            }
        }

        public string StatsStuHW_Analysis_KP_Id
        {
            get
            {
                return this._statsstuhw_analysis_kp_id;
            }
            set
            {
                this._statsstuhw_analysis_kp_id = value;
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

        public decimal? TotalScore
        {
            get
            {
                return this._totalscore;
            }
            set
            {
                this._totalscore = value;
            }
        }

        public int? TQCount_Right
        {
            get
            {
                return this._tqcount_right;
            }
            set
            {
                this._tqcount_right = value;
            }
        }

        public int? TQCount_Wrong
        {
            get
            {
                return this._tqcount_wrong;
            }
            set
            {
                this._tqcount_wrong = value;
            }
        }

        public decimal? TQMastery_No
        {
            get
            {
                return this._tqmastery_no;
            }
            set
            {
                this._tqmastery_no = value;
            }
        }
    }
}

