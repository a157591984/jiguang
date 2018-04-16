namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum UOrderStatus
    {
        [Description("待付款")]
        待付款 = 1,
        [Description("付款成功")]
        付款成功 = 2,
        [Description("完成")]
        完成 = 3,
        [Description("已取消")]
        已取消 = 4
    }
}

