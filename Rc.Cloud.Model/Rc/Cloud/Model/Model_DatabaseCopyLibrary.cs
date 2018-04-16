namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_DatabaseCopyLibrary
    {
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public string CustomerInfo_ID { get; set; }

        public string DataBaseAddress { get; set; }

        public string DatabaseCopyLibrary_ID { get; set; }

        public string DatabaseCopyLibrary_Remark { get; set; }

        public string DataBaseName { get; set; }

        public string LoginName { get; set; }

        public string LoginPassword { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateUser { get; set; }
    }
}

