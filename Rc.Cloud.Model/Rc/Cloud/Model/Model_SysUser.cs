namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_SysUser
    {
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public string SysDepartment_ID { get; set; }

        public string SysRole_ID { get; set; }

        public bool? SysUser_Enable { get; set; }

        public string SysUser_ID { get; set; }

        public string SysUser_LoginName { get; set; }

        public string SysUser_Name { get; set; }

        public string SysUser_PassWord { get; set; }

        public string SysUser_Remark { get; set; }

        public string SysUser_Tel { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateUser { get; set; }
    }
}

