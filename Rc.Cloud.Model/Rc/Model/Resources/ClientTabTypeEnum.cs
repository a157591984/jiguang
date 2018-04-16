namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum ClientTabTypeEnum
    {
        [Description("管理员")]
        A = 0,
        [Description("学生")]
        S = 2,
        [Description("老师")]
        T = 1
    }
}

