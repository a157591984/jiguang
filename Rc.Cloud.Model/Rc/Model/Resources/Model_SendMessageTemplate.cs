namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SendMessageTemplate
    {
        private string _content;
        private DateTime? _ctime;
        private string _cuser;
        private int? _isstart;
        private string _method;
        private string _mobile;
        private string _msgurl;
        private string _password;
        private string _sendsmstemplateid;
        private string _stype;
        private string _userid;
        private string _username;

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public DateTime? CTime
        {
            get
            {
                return this._ctime;
            }
            set
            {
                this._ctime = value;
            }
        }

        public string CUser
        {
            get
            {
                return this._cuser;
            }
            set
            {
                this._cuser = value;
            }
        }

        public int? IsStart
        {
            get
            {
                return this._isstart;
            }
            set
            {
                this._isstart = value;
            }
        }

        public string Method
        {
            get
            {
                return this._method;
            }
            set
            {
                this._method = value;
            }
        }

        public string Mobile
        {
            get
            {
                return this._mobile;
            }
            set
            {
                this._mobile = value;
            }
        }

        public string MsgUrl
        {
            get
            {
                return this._msgurl;
            }
            set
            {
                this._msgurl = value;
            }
        }

        public string PassWord
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string SendSMSTemplateId
        {
            get
            {
                return this._sendsmstemplateid;
            }
            set
            {
                this._sendsmstemplateid = value;
            }
        }

        public string SType
        {
            get
            {
                return this._stype;
            }
            set
            {
                this._stype = value;
            }
        }

        public string UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
    }
}

