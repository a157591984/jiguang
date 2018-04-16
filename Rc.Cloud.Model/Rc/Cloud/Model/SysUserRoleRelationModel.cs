namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SysUserRoleRelationModel
    {
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public string SysRole_ID { get; set; }

        public string SysUser_ID { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateUser { get; set; }
    }
}

