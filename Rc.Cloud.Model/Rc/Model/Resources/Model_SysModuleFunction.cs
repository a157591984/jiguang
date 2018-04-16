namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysModuleFunction
    {
        private string _functionid;
        private int? _isdefault;
        private string _moduleid;
        private string _syscode;

        public string FunctionId
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

        public int? IsDefault
        {
            get
            {
                return this._isdefault;
            }
            set
            {
                this._isdefault = value;
            }
        }

        public string ModuleID
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

        public string syscode
        {
            get
            {
                return this._syscode;
            }
            set
            {
                this._syscode = value;
            }
        }
    }
}

