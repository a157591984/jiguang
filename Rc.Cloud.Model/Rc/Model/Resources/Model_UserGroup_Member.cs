namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_UserGroup_Member
    {
        private string _createuser;
        private string _membershipenum;
        private DateTime? _user_applicationpasstime;
        private string _user_applicationreason;
        private string _user_applicationstatus;
        private DateTime? _user_applicationtime;
        private string _user_id;
        private string _usergroup_id;
        private string _usergroup_member_id;
        private int? _userstatus = 1;

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

        public string MembershipEnum
        {
            get
            {
                return this._membershipenum;
            }
            set
            {
                this._membershipenum = value;
            }
        }

        public DateTime? User_ApplicationPassTime
        {
            get
            {
                return this._user_applicationpasstime;
            }
            set
            {
                this._user_applicationpasstime = value;
            }
        }

        public string User_ApplicationReason
        {
            get
            {
                return this._user_applicationreason;
            }
            set
            {
                this._user_applicationreason = value;
            }
        }

        public string User_ApplicationStatus
        {
            get
            {
                return this._user_applicationstatus;
            }
            set
            {
                this._user_applicationstatus = value;
            }
        }

        public DateTime? User_ApplicationTime
        {
            get
            {
                return this._user_applicationtime;
            }
            set
            {
                this._user_applicationtime = value;
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

        public string UserGroup_Member_Id
        {
            get
            {
                return this._usergroup_member_id;
            }
            set
            {
                this._usergroup_member_id = value;
            }
        }

        public int? UserStatus
        {
            get
            {
                return this._userstatus;
            }
            set
            {
                this._userstatus = value;
            }
        }
    }
}

