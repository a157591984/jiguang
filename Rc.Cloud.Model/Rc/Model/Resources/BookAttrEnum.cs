namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum BookAttrEnum
    {
        [Description("复制")]
        Copy = 2,
        [Description("打印")]
        Print = 0,
        [Description("存盘")]
        Save = 1
    }
}

