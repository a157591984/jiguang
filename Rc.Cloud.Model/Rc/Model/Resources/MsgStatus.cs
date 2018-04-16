namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum MsgStatus
    {
        [Description("已删除")]
        Deleted = 2,
        [Description("已读")]
        Read = 0,
        [Description("未读")]
        Unread = 1
    }
}

