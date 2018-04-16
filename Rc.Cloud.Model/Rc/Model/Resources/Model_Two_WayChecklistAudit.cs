namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Two_WayChecklistAudit
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _remark;
        private int? _status;
        private string _two_waychecklist_id;
        private string _user_id;

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

        public int? Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
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

        public string User_Id
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
    }
}

