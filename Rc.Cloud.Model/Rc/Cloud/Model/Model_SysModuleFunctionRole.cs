namespace Rc.Cloud.Model
{
    using System;

    [Serializable]
    public class Model_SysModuleFunctionRole
    {
        private string _functionid;
        private string _moduleid;
        private string _sysrole_id;

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

        public string SysRole_ID
        {
            get
            {
                return this._sysrole_id;
            }
            set
            {
                this._sysrole_id = value;
            }
        }
    }
}

