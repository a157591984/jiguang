namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TestPaper_FrameDetailToTestQuestions_Attr
    {
        private string _attr_type;
        private string _attr_value;
        private DateTime? _createtime;
        private string _createuser;
        private string _testpaper_frame_id;
        private string _testpaper_framedetail_id;
        private string _testpaper_framedetailtotestquestions_attr_id;

        public string Attr_Type
        {
            get
            {
                return this._attr_type;
            }
            set
            {
                this._attr_type = value;
            }
        }

        public string Attr_Value
        {
            get
            {
                return this._attr_value;
            }
            set
            {
                this._attr_value = value;
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

        public string TestPaper_FrameDetailToTestQuestions_Attr_Id
        {
            get
            {
                return this._testpaper_framedetailtotestquestions_attr_id;
            }
            set
            {
                this._testpaper_framedetailtotestquestions_attr_id = value;
            }
        }
    }
}

