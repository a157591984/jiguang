namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Common_DictModel
    {
        public string Common_Dict_ID { get; set; }

        public string D_Code { get; set; }

        public DateTime? D_CreateTime { get; set; }

        public string D_CreateUser { get; set; }

        public DateTime? D_ModifyTime { get; set; }

        public string D_ModifyUser { get; set; }

        public string D_Name { get; set; }

        public int? D_Order { get; set; }

        public string D_ParentID { get; set; }

        public string D_Remark { get; set; }

        public int? D_Type { get; set; }

        public int? D_Value { get; set; }
    }
}

