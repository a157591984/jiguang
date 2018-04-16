namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_SystemLog
    {
        private string _doctorInfo_Name;

        public string DoctorInfo_Name
        {
            get
            {
                return this._doctorInfo_Name;
            }
            set
            {
                this._doctorInfo_Name = value;
            }
        }

        public DateTime? SystemLog_CreateDate { get; set; }

        public string SystemLog_Desc { get; set; }

        public string SystemLog_ID { get; set; }

        public string SystemLog_IP { get; set; }

        public string SystemLog_Level { get; set; }

        public string SystemLog_LoginID { get; set; }

        public string SystemLog_Model { get; set; }

        public string SystemLog_Remark { get; set; }

        public int? SystemLog_Source { get; set; }

        public string SystemLog_TableDataID { get; set; }

        public string SystemLog_TableName { get; set; }

        public int? SystemLog_Type { get; set; }
    }
}

