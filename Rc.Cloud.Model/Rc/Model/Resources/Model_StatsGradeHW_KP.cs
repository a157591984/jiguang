namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsGradeHW_KP
    {
        private DateTime? _createtime;
        private string _gradeid;
        private string _gradename;
        private string _kpname;
        private decimal? _kpscoreavg;
        private decimal? _kpscoreavgrate;
        private decimal? _kpscorestudentsum;
        private decimal? _kpscoresum;
        private string _resource_name;
        private string _resourcetoresourcefolder_id;
        private string _statsgradehw_kpid;
        private string _subjectid;
        private string _subjectname;
        private string _testquestionnums;
        private string _testquestionnumstrs;

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

        public string Gradeid
        {
            get
            {
                return this._gradeid;
            }
            set
            {
                this._gradeid = value;
            }
        }

        public string GradeName
        {
            get
            {
                return this._gradename;
            }
            set
            {
                this._gradename = value;
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

        public decimal? KPScoreAvg
        {
            get
            {
                return this._kpscoreavg;
            }
            set
            {
                this._kpscoreavg = value;
            }
        }

        public decimal? KPScoreAvgRate
        {
            get
            {
                return this._kpscoreavgrate;
            }
            set
            {
                this._kpscoreavgrate = value;
            }
        }

        public decimal? KPScoreStudentSum
        {
            get
            {
                return this._kpscorestudentsum;
            }
            set
            {
                this._kpscorestudentsum = value;
            }
        }

        public decimal? KPScoreSum
        {
            get
            {
                return this._kpscoresum;
            }
            set
            {
                this._kpscoresum = value;
            }
        }

        public string Resource_Name
        {
            get
            {
                return this._resource_name;
            }
            set
            {
                this._resource_name = value;
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

        public string StatsGradeHW_KPID
        {
            get
            {
                return this._statsgradehw_kpid;
            }
            set
            {
                this._statsgradehw_kpid = value;
            }
        }

        public string SubjectID
        {
            get
            {
                return this._subjectid;
            }
            set
            {
                this._subjectid = value;
            }
        }

        public string SubjectName
        {
            get
            {
                return this._subjectname;
            }
            set
            {
                this._subjectname = value;
            }
        }

        public string TestQuestionNums
        {
            get
            {
                return this._testquestionnums;
            }
            set
            {
                this._testquestionnums = value;
            }
        }

        public string TestQuestionNumStrs
        {
            get
            {
                return this._testquestionnumstrs;
            }
            set
            {
                this._testquestionnumstrs = value;
            }
        }
    }
}

