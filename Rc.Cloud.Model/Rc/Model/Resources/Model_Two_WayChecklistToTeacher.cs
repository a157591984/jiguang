namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Two_WayChecklistToTeacher
    {
        private DateTime? _createtime;
        private string _teacher_id;
        private string _two_newwaychecklist_id;
        private string _two_waychecklist_id;
        private string _two_waychecklisttoteacher_id;

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

        public string Teacher_Id
        {
            get
            {
                return this._teacher_id;
            }
            set
            {
                this._teacher_id = value;
            }
        }

        public string Two_NewWayChecklist_Id
        {
            get
            {
                return this._two_newwaychecklist_id;
            }
            set
            {
                this._two_newwaychecklist_id = value;
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

        public string Two_WayChecklistToTeacher_Id
        {
            get
            {
                return this._two_waychecklisttoteacher_id;
            }
            set
            {
                this._two_waychecklisttoteacher_id = value;
            }
        }
    }
}

