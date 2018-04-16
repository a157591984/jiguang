namespace Rc.Cloud.Model
{
    using System;

    [Serializable]
    public class Model_Sys_Function
    {
        private string _functionid;
        private string _functionname;

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

        public string FUNCTIONName
        {
            get
            {
                return this._functionname;
            }
            set
            {
                this._functionname = value;
            }
        }
    }
}

