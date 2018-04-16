namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TestPaper_FrameToTestpaper
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _resourcetoresourcefolder_id;
        private string _testpaper_frame_id;
        private string _testpaper_frametotestpaper_id;
        private string _testpaper_frametotestpaper_type = "0";

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

        public string TestPaper_FrameToTestpaper_Id
        {
            get
            {
                return this._testpaper_frametotestpaper_id;
            }
            set
            {
                this._testpaper_frametotestpaper_id = value;
            }
        }

        public string TestPaper_FrameToTestpaper_Type
        {
            get
            {
                return this._testpaper_frametotestpaper_type;
            }
            set
            {
                this._testpaper_frametotestpaper_type = value;
            }
        }
    }
}

