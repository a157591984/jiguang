namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ResourceFolder
    {
        private string _book_id;
        private string _createfuser;
        private DateTime? _createtime;
        private string _gradeterm;
        private string _lessonplan_type;
        private int? _particularyear;
        private string _resource_class;
        private string _resource_type;
        private string _resource_version;
        private string _resourcefolder_id;
        private string _resourcefolder_islast;
        private int? _resourcefolder_level;
        private string _resourcefolder_name;
        private int? _resourcefolder_order;
        private string _resourcefolder_owner;
        private string _resourcefolder_parentid;
        private string _resourcefolder_remark;
        private string _subject;

        public string Book_ID
        {
            get
            {
                return this._book_id;
            }
            set
            {
                this._book_id = value;
            }
        }

        public string CreateFUser
        {
            get
            {
                return this._createfuser;
            }
            set
            {
                this._createfuser = value;
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

        public string LessonPlan_Type
        {
            get
            {
                return this._lessonplan_type;
            }
            set
            {
                this._lessonplan_type = value;
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

        public string Resource_Class
        {
            get
            {
                return this._resource_class;
            }
            set
            {
                this._resource_class = value;
            }
        }

        public string Resource_Type
        {
            get
            {
                return this._resource_type;
            }
            set
            {
                this._resource_type = value;
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

        public string ResourceFolder_Id
        {
            get
            {
                return this._resourcefolder_id;
            }
            set
            {
                this._resourcefolder_id = value;
            }
        }

        public string ResourceFolder_isLast
        {
            get
            {
                return this._resourcefolder_islast;
            }
            set
            {
                this._resourcefolder_islast = value;
            }
        }

        public int? ResourceFolder_Level
        {
            get
            {
                return this._resourcefolder_level;
            }
            set
            {
                this._resourcefolder_level = value;
            }
        }

        public string ResourceFolder_Name
        {
            get
            {
                return this._resourcefolder_name;
            }
            set
            {
                this._resourcefolder_name = value;
            }
        }

        public int? ResourceFolder_Order
        {
            get
            {
                return this._resourcefolder_order;
            }
            set
            {
                this._resourcefolder_order = value;
            }
        }

        public string ResourceFolder_Owner
        {
            get
            {
                return this._resourcefolder_owner;
            }
            set
            {
                this._resourcefolder_owner = value;
            }
        }

        public string ResourceFolder_ParentId
        {
            get
            {
                return this._resourcefolder_parentid;
            }
            set
            {
                this._resourcefolder_parentid = value;
            }
        }

        public string ResourceFolder_Remark
        {
            get
            {
                return this._resourcefolder_remark;
            }
            set
            {
                this._resourcefolder_remark = value;
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
    }
}

