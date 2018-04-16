namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TestPaper_Frame
    {
        private string _analysis;
        private DateTime? _createtime;
        private string _createuser;
        private string _gradeterm;
        private int? _particularyear;
        private string _remark;
        private string _resource_version;
        private string _subject;
        private string _testpaper_frame_id;
        private string _testpaper_frame_name;
        private string _testpaper_frametype;

        public string Analysis
        {
            get
            {
                return this._analysis;
            }
            set
            {
                this._analysis = value;
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

        public int? ParticularYear
        {
            get
            {
                return this._particularyear;
            }
            set
            {
                this._particularyear = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
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

        public string TestPaper_Frame_Id
        {
            get
            {
                return this._testpaper_frame_id;
            }
            set
            {
                this._testpaper_frame_id = value;
            }
        }

        public string TestPaper_Frame_Name
        {
            get
            {
                return this._testpaper_frame_name;
            }
            set
            {
                this._testpaper_frame_name = value;
            }
        }

        public string TestPaper_FrameType
        {
            get
            {
                return this._testpaper_frametype;
            }
            set
            {
                this._testpaper_frametype = value;
            }
        }
    }
}

