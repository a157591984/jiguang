namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum MsgTypeEumn
    {
        [Description("仅个人可见（消息接收者）")]
        Private = 1,
        [Description("于Msg相关的人可见，逻辑可自定义")]
        Protect = 2,
        [Description("所有人可见")]
        Public = 0
    }
}

