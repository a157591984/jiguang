namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ResourceToResourceFolder
    {
        private string _book_id;
        private string _createfuser;
        private DateTime? _createtime;
        private string _file_name;
        private string _file_owner;
        private string _file_suffix;
        private string _gradeterm;
        private string _lessonplan_type;
        private int? _particularyear;
        private string _resource_class;
        private string _resource_domain;
        private string _resource_id;
        private string _resource_name;
        private string _resource_shared;
        private string _resource_type;
        private string _resource_url;
        private string _resource_version;
        private string _resourcefolder_id;
        private string _resourcetoresourcefolder_id;
        private int? _resourcetoresourcefolder_order = 0;
        private string _subject;
        private DateTime? _updatetime;

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

        public string File_Name
        {
            get
            {
                return this._file_name;
            }
            set
            {
                this._file_name = value;
            }
        }

        public string File_Owner
        {
            get
            {
                return this._file_owner;
            }
            set
            {
                this._file_owner = value;
            }
        }

        public string File_Suffix
        {
            get
            {
                return this._file_suffix;
            }
            set
            {
                this._file_suffix = value;
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

        public string Resource_Domain
        {
            get
            {
                return this._resource_domain;
            }
            set
            {
                this._resource_domain = value;
            }
        }

        public string Resource_Id
        {
            get
            {
                return this._resource_id;
            }
            set
            {
                this._resource_id = value;
            }
        }

        public string Resource_Name
        {
            get
            {
                return this._resource_name;
            }
            set
            {
                this._resource_name = value;
            }
        }

        public string Resource_shared
        {
            get
            {
                return this._resource_shared;
            }
            set
            {
                this._resource_shared = value;
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

        public string Resource_Url
        {
            get
            {
                return this._resource_url;
            }
            set
            {
                this._resource_url = value;
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

        public int? ResourceToResourceFolder_Order
        {
            get
            {
                return this._resourcetoresourcefolder_order;
            }
            set
            {
                this._resourcetoresourcefolder_order = value;
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

        public DateTime? UpdateTime
        {
            get
            {
                return this._updatetime;
            }
            set
            {
                this._updatetime = value;
            }
        }
    }
}

