﻿namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsGradeHW_Subsection
    {
        private decimal? _avgscore;
        private decimal? _correctedcount;
        private DateTime? _createtime;
        private decimal? _gradeallcount;
        private string _gradeid;
        private string _gradename;
        private decimal? _highestscore;
        private decimal? _hwscore;
        private decimal? _lowestscore;
        private string _resource_name;
        private string _resourcetoresourcefolder_id;
        private string _schoolid;
        private string _schoolname;
        private string _statsgradehw_subsectionid;
        private string _subjectid;
        private string _subjectname;
        private decimal? _subsectioncount;
        private string _subsectionname;

        public decimal? AVGScore
        {
            get
            {
                return this._avgscore;
            }
            set
            {
                this._avgscore = value;
            }
        }

        public decimal? CorrectedCount
        {
            get
            {
                return this._correctedcount;
            }
            set
            {
                this._correctedcount = value;
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

        public decimal? GradeAllCount
        {
            get
            {
                return this._gradeallcount;
            }
            set
            {
                this._gradeallcount = value;
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

        public decimal? HighestScore
        {
            get
            {
                return this._highestscore;
            }
            set
            {
                this._highestscore = value;
            }
        }

        public decimal? HWScore
        {
            get
            {
                return this._hwscore;
            }
            set
            {
                this._hwscore = value;
            }
        }

        public decimal? LowestScore
        {
            get
            {
                return this._lowestscore;
            }
            set
            {
                this._lowestscore = value;
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

        public string SchoolID
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

        public string SchoolName
        {
            get
            {
                return this._schoolname;
            }
            set
            {
                this._schoolname = value;
            }
        }

        public string StatsGradeHW_SubsectionID
        {
            get
            {
                return this._statsgradehw_subsectionid;
            }
            set
            {
                this._statsgradehw_subsectionid = value;
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

        public decimal? SubsectionCount
        {
            get
            {
                return this._subsectioncount;
            }
            set
            {
                this._subsectioncount = value;
            }
        }

        public string SubsectionName
        {
            get
            {
                return this._subsectionname;
            }
            set
            {
                this._subsectionname = value;
            }
        }
    }
}

