namespace Rc.Cloud.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SysRoleModel
    {
        private List<SysUserRoleRelationModel> _SysUserRoleList = new List<SysUserRoleRelationModel>();

        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }

        public bool? SysRole_Enable { get; set; }

        public string SysRole_ID { get; set; }

        public string SysRole_Name { get; set; }

        public int? SysRole_Order { get; set; }

        public string SysRole_Remark { get; set; }

        public List<SysUserRoleRelationModel> SysUserRoleList
        {
            get
            {
                return this._SysUserRoleList;
            }
            set
            {
                value = this._SysUserRoleList;
            }
        }

        public DateTime? UpdateTime { get; set; }

        public string UpdateUser { get; set; }
    }
}

