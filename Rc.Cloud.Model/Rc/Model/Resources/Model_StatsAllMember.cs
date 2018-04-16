﻿namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_StatsAllMember
    {
        private DateTime? _createtime;
        private string _datedata;
        private string _datetype;
        private int? _membercount;
        private string _membertype;
        private int? _nmembercount;
        private string _statsallmember_id;

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

        public string DateData
        {
            get
            {
                return this._datedata;
            }
            set
            {
                this._datedata = value;
            }
        }

        public string DateType
        {
            get
            {
                return this._datetype;
            }
            set
            {
                this._datetype = value;
            }
        }

        public int? MemberCount
        {
            get
            {
                return this._membercount;
            }
            set
            {
                this._membercount = value;
            }
        }

        public string MemberType
        {
            get
            {
                return this._membertype;
            }
            set
            {
                this._membertype = value;
            }
        }

        public int? NMemberCount
        {
            get
            {
                return this._nmembercount;
            }
            set
            {
                this._nmembercount = value;
            }
        }

        public string StatsAllMember_Id
        {
            get
            {
                return this._statsallmember_id;
            }
            set
            {
                this._statsallmember_id = value;
            }
        }
    }
}

