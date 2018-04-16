namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_SysDepartment
    {
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public bool? SysDepartment_Enable { get; set; }

        public string SysDepartment_ID { get; set; }

        public string SysDepartment_Name { get; set; }

        public string SysDepartment_ParentID { get; set; }

        public string SysDepartment_Remark { get; set; }

        public string SysDepartment_Tel { get; set; }

        public string SysUser_ID { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateUser { get; set; }
    }
}

