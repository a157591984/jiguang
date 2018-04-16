namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum UserOrder_PaytoolEnum
    {
        [Description("支付宝")]
        ALIPAY = 0,
        [Description("免费资源")]
        FREE = 2,
        [Description("内部授权")]
        NBSQ = 3,
        [Description("微信")]
        WXPAY = 1
    }
}

