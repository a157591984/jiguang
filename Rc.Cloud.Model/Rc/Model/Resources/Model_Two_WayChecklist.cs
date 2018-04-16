namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Two_WayChecklist
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _gradeterm;
        private string _parentid;
        private int? _particularyear;
        private string _remark;
        private string _resource_version;
        private string _subject;
        private string _two_waychecklist_id;
        private string _two_waychecklist_name;
        private string _two_waychecklisttype;

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

        public string ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
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

        public string Two_WayChecklist_Name
        {
            get
            {
                return this._two_waychecklist_name;
            }
            set
            {
                this._two_waychecklist_name = value;
            }
        }

        public string Two_WayChecklistType
        {
            get
            {
                return this._two_waychecklisttype;
            }
            set
            {
                this._two_waychecklisttype = value;
            }
        }
    }
}

