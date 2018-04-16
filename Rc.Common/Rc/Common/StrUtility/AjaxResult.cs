namespace Rc.Common.StrUtility
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public sealed class AjaxResult
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        public static AjaxResult GetErrorResult(string _errorMsg)
        {
            return new AjaxResult { status = 0, errorMsg = _errorMsg };
        }

        public static AjaxResult GetSuccessResult(object _data)
        {
            return new AjaxResult { status = 1, data = _data };
        }

        public override string ToString()
        {
            return this.jss.Serialize(this);
        }

        public object data { get; set; }

        public string errorMsg { get; set; }

        public int status { get; set; }
    }
}

