namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_HW_TestPaper
    {
        private DateTime? _createtime;
        private string _hw_testpaper_id;
        private string _testpaper_path;
        private string _testpaper_status;

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

        public string HW_TestPaper_Id
        {
            get
            {
                return this._hw_testpaper_id;
            }
            set
            {
                this._hw_testpaper_id = value;
            }
        }

        public string TestPaper_Path
        {
            get
            {
                return this._testpaper_path;
            }
            set
            {
                this._testpaper_path = value;
            }
        }

        public string TestPaper_Status
        {
            get
            {
                return this._testpaper_status;
            }
            set
            {
                this._testpaper_status = value;
            }
        }
    }
}

