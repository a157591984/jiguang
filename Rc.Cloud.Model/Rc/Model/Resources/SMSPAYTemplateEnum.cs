namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum SMSPAYTemplateEnum
    {
        [Description("支付宝支付")]
        ALIPAY = 2,
        [Description("邮箱")]
        Mail = 1,
        [Description("短信")]
        SMS = 0,
        [Description("微信支付")]
        WXPAY = 3
    }
}

