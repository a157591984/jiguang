namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Two_WayChecklistDetailToTestQuestions
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _resourcetoresourcefolder_id;
        private string _testquestions_id;
        private string _two_waychecklist_id;
        private string _two_waychecklistdetail_id;
        private string _two_waychecklistdetailtotestquestions_id;

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

        public string Two_WayChecklist_Id
        {
            get
            {
                return this._two_waychecklist_id;
            }
            set
            {
                this._two_waychecklist_id = value;
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

        public string Two_WayChecklistDetailToTestQuestions_Id
        {
            get
            {
                return this._two_waychecklistdetailtotestquestions_id;
            }
            set
            {
                this._two_waychecklistdetailtotestquestions_id = value;
            }
        }
    }
}

