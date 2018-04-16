namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum MsgEnum
    {
        [Description("加入班级")]
        ApplyToClass = 0,
        [Description("加入年级")]
        ApplyToGrade = 3,
        [Description("加入学校")]
        ApplyToSchool = 6,
        [Description("邀请加入班级")]
        InvitationToClass = 1,
        [Description("邀请加入年级")]
        InvitationToGrade = 4,
        [Description("邀请加入学校")]
        InvitationToSchool = 7,
        [Description("通知")]
        Notice = 9,
        [Description("退出班级")]
        QuitClass = 2,
        [Description("退出年级")]
        QuitGrade = 5,
        [Description("退出学校")]
        QuitSchool = 8
    }
}

