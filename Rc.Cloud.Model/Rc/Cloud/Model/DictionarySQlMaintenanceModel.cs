namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class DictionarySQlMaintenanceModel
    {
        public DateTime? DictionarySQlMaintenance_CreateTime { get; set; }

        public string DictionarySQlMaintenance_CretateUser { get; set; }

        public string DictionarySQlMaintenance_Explanation { get; set; }

        public string DictionarySQlMaintenance_ID { get; set; }

        public string DictionarySQlMaintenance_Mark { get; set; }

        public string DictionarySQlMaintenance_Name { get; set; }

        public string DictionarySQlMaintenance_SQL { get; set; }

        public DateTime? DictionarySQlMaintenance_UpdateTime { get; set; }

        public string DictionarySQlMaintenance_UpdateUser { get; set; }
    }
}

