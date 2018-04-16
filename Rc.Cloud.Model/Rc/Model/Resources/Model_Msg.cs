namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Msg
    {
        private DateTime? _createtime = new DateTime?(DateTime.Now);
        private string _createuser;
        private string _msgaccepter;
        private string _msgcontent;
        private string _msgenum;
        private string _msgid;
        private string _msgsender;
        private string _msgstatus;
        private string _msgtitle;
        private string _msgtypeenum;
        private string _resourcedataid;

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

        public string MsgAccepter
        {
            get
            {
                return this._msgaccepter;
            }
            set
            {
                this._msgaccepter = value;
            }
        }

        public string MsgContent
        {
            get
            {
                return this._msgcontent;
            }
            set
            {
                this._msgcontent = value;
            }
        }

        public string MsgEnum
        {
            get
            {
                return this._msgenum;
            }
            set
            {
                this._msgenum = value;
            }
        }

        public string MsgId
        {
            get
            {
                return this._msgid;
            }
            set
            {
                this._msgid = value;
            }
        }

        public string MsgSender
        {
            get
            {
                return this._msgsender;
            }
            set
            {
                this._msgsender = value;
            }
        }

        public string MsgStatus
        {
            get
            {
                return this._msgstatus;
            }
            set
            {
                this._msgstatus = value;
            }
        }

        public string MsgTitle
        {
            get
            {
                return this._msgtitle;
            }
            set
            {
                this._msgtitle = value;
            }
        }

        public string MsgTypeEnum
        {
            get
            {
                return this._msgtypeenum;
            }
            set
            {
                this._msgtypeenum = value;
            }
        }

        public string ResourceDataId
        {
            get
            {
                return this._resourcedataid;
            }
            set
            {
                this._resourcedataid = value;
            }
        }
    }
}

