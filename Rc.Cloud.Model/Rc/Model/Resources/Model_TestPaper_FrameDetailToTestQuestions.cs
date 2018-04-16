namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TestPaper_FrameDetailToTestQuestions
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _resourcetoresourcefolder_id;
        private string _testpaper_frame_id;
        private string _testpaper_framedetail_id;
        private string _testpaper_framedetailtotestquestions_id;
        private string _testquestions_id;

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

        public string TestPaper_FrameDetail_Id
        {
            get
            {
                return this._testpaper_framedetail_id;
            }
            set
            {
                this._testpaper_framedetail_id = value;
            }
        }

        public string TestPaper_FrameDetailToTestQuestions_Id
        {
            get
            {
                return this._testpaper_framedetailtotestquestions_id;
            }
            set
            {
                this._testpaper_framedetailtotestquestions_id = value;
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
    }
}

