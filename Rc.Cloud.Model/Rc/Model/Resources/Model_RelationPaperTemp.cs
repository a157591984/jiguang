namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_RelationPaperTemp
    {
        private string _createuser;
        private string _relationpaper_id;
        private string _relationpapertemp_id;
        private string _testquestions_id;
        private string _two_waychecklistdetail_id;

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

        public string RelationPaper_Id
        {
            get
            {
                return this._relationpaper_id;
            }
            set
            {
                this._relationpaper_id = value;
            }
        }

        public string RelationPaperTemp_id
        {
            get
            {
                return this._relationpapertemp_id;
            }
            set
            {
                this._relationpapertemp_id = value;
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

        public string Two_WayChecklistDetail_Id
        {
            get
            {
                return this._two_waychecklistdetail_id;
            }
            set
            {
                this._two_waychecklistdetail_id = value;
            }
        }
    }
}

