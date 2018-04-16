namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_SystemLogError
    {
        public string DoctorName { get; set; }

        public DateTime? SystemLog_CreateDate { get; set; }

        public string SystemLog_Desc { get; set; }

        public string SystemLog_ID { get; set; }

        public string SystemLog_IP { get; set; }

        public string SystemLog_LoginID { get; set; }

        public string SystemLog_PagePath { get; set; }

        public int? SystemLog_Source { get; set; }

        public string SystemLog_SysPath { get; set; }
    }
}

