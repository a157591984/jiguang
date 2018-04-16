namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_CustomVisitAttachment
    {
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public string CustomVisit_ID { get; set; }

        public string CustomVisitAttachment_ID { get; set; }

        public string CustomVisitAttachment_ServerName { get; set; }

        public string CustomVisitAttachment_SourceName { get; set; }

        public string CustomVisitAttachment_URL { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateUser { get; set; }
    }
}

