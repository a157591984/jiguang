namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum FileSyncType
    {
        [Description("同步运营")]
        SyncOperate = 0,
        [Description("同步学校")]
        SyncSchool = 1
    }
}

