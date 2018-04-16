namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Two_WayChecklistToTestpaper
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _resourcetoresourcefolder_id;
        private string _two_waychecklist_id;
        private string _Two_WayChecklistToTestpaper_id;

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

        public string Two_WayChecklistToTestpaper_Id
        {
            get
            {
                return this._Two_WayChecklistToTestpaper_id;
            }
            set
            {
                this._Two_WayChecklistToTestpaper_id = value;
            }
        }
    }
}

