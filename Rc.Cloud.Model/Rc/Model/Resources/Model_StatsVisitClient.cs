namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsVisitClient
    {
        private int? _createowncount_all;
        private int? _createowncount_plan;
        private int? _createowncount_testpaper;
        private DateTime? _createtime;
        private string _datedata;
        private string _datetype;
        private string _isused;
        private string _statsvisitclient_id;
        private string _teacherid;
        private string _teachername;
        private int? _visitcount_all;
        private int? _visitcount_cloud;
        private int? _visitcount_own;
        private int? _visitfile_all;
        private int? _visitfile_cloud;
        private int? _visitfile_own;

        public int? CreateOwnCount_All
        {
            get
            {
                return this._createowncount_all;
            }
            set
            {
                this._createowncount_all = value;
            }
        }

        public int? CreateOwnCount_Plan
        {
            get
            {
                return this._createowncount_plan;
            }
            set
            {
                this._createowncount_plan = value;
            }
        }

        public int? CreateOwnCount_TestPaper
        {
            get
            {
                return this._createowncount_testpaper;
            }
            set
            {
                this._createowncount_testpaper = value;
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

        public string DateData
        {
            get
            {
                return this._datedata;
            }
            set
            {
                this._datedata = value;
            }
        }

        public string DateType
        {
            get
            {
                return this._datetype;
            }
            set
            {
                this._datetype = value;
            }
        }

        public string IsUsed
        {
            get
            {
                return this._isused;
            }
            set
            {
                this._isused = value;
            }
        }

        public string StatsVisitClient_Id
        {
            get
            {
                return this._statsvisitclient_id;
            }
            set
            {
                this._statsvisitclient_id = value;
            }
        }

        public string TeacherId
        {
            get
            {
                return this._teacherid;
            }
            set
            {
                this._teacherid = value;
            }
        }

        public string TeacherName
        {
            get
            {
                return this._teachername;
            }
            set
            {
                this._teachername = value;
            }
        }

        public int? VisitCount_All
        {
            get
            {
                return this._visitcount_all;
            }
            set
            {
                this._visitcount_all = value;
            }
        }

        public int? VisitCount_Cloud
        {
            get
            {
                return this._visitcount_cloud;
            }
            set
            {
                this._visitcount_cloud = value;
            }
        }

        public int? VisitCount_Own
        {
            get
            {
                return this._visitcount_own;
            }
            set
            {
                this._visitcount_own = value;
            }
        }

        public int? VisitFile_All
        {
            get
            {
                return this._visitfile_all;
            }
            set
            {
                this._visitfile_all = value;
            }
        }

        public int? VisitFile_Cloud
        {
            get
            {
                return this._visitfile_cloud;
            }
            set
            {
                this._visitfile_cloud = value;
            }
        }

        public int? VisitFile_Own
        {
            get
            {
                return this._visitfile_own;
            }
            set
            {
                this._visitfile_own = value;
            }
        }
    }
}

