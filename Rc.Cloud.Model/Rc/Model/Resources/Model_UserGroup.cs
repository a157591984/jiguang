namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_UserGroup
    {
        private decimal? _class;
        private DateTime? _createtime;
        private decimal? _grade;
        private string _gradetype;
        private decimal? _startschoolyear;
        private string _subject;
        private string _user_id;
        private string _usergroup_attrenum;
        private string _usergroup_briefintroduction;
        private string _usergroup_id;
        private string _usergroup_name;
        private string _usergroup_parentid;
        private int? _usergrouporder = 1;
        private string _usergroupp_type;

        public decimal? Class
        {
            get
            {
                return this._class;
            }
            set
            {
                this._class = value;
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

        public decimal? Grade
        {
            get
            {
                return this._grade;
            }
            set
            {
                this._grade = value;
            }
        }

        public string GradeType
        {
            get
            {
                return this._gradetype;
            }
            set
            {
                this._gradetype = value;
            }
        }

        public decimal? StartSchoolYear
        {
            get
            {
                return this._startschoolyear;
            }
            set
            {
                this._startschoolyear = value;
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

        public string User_ID
        {
            get
            {
                return this._user_id;
            }
            set
            {
                this._user_id = value;
            }
        }

        public string UserGroup_AttrEnum
        {
            get
            {
                return this._usergroup_attrenum;
            }
            set
            {
                this._usergroup_attrenum = value;
            }
        }

        public string UserGroup_BriefIntroduction
        {
            get
            {
                return this._usergroup_briefintroduction;
            }
            set
            {
                this._usergroup_briefintroduction = value;
            }
        }

        public string UserGroup_Id
        {
            get
            {
                return this._usergroup_id;
            }
            set
            {
                this._usergroup_id = value;
            }
        }

        public string UserGroup_Name
        {
            get
            {
                return this._usergroup_name;
            }
            set
            {
                this._usergroup_name = value;
            }
        }

        public string UserGroup_ParentId
        {
            get
            {
                return this._usergroup_parentid;
            }
            set
            {
                this._usergroup_parentid = value;
            }
        }

        public int? UserGroupOrder
        {
            get
            {
                return this._usergrouporder;
            }
            set
            {
                this._usergrouporder = value;
            }
        }

        public string UserGroupp_Type
        {
            get
            {
                return this._usergroupp_type;
            }
            set
            {
                this._usergroupp_type = value;
            }
        }
    }
}

