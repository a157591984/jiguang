namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_ExecutionStep
    {
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public string ExecutionStep_Code { get; set; }

        public bool? ExecutionStep_Enable { get; set; }

        public string ExecutionStep_Name { get; set; }

        public string ExecutionStep_Remark { get; set; }

        public string ExecutionStep_Type { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateUser { get; set; }
    }
}

