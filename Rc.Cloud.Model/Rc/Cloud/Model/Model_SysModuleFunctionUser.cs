namespace Rc.Cloud.Model
{
    using System;

    [Serializable]
    public class Model_SysModuleFunctionUser
    {
        private string _functionid;
        private string _moduleid;
        private string _user_id;

        public string FUNCTIONID
        {
            get
            {
                return this._functionid;
            }
            set
            {
                this._functionid = value;
            }
        }

        public string MODULEID
        {
            get
            {
                return this._moduleid;
            }
            set
            {
                this._moduleid = value;
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
    }
}

