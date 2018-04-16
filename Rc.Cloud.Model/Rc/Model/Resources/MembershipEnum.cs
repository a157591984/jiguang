namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum MembershipEnum
    {
        [Description("班级")]
        classrc = 7,
        [Description("教务主任")]
        Dean = 2,
        [Description("年级")]
        grade = 4,
        [Description("年级组长")]
        gradedirector = 5,
        [Description("备课组长")]
        GroupLeader = 6,
        [Description("班主任")]
        headmaster = 8,
        [Description("校长")]
        principal = 0,
        [Description("学生")]
        student = 10,
        [Description("代课老师")]
        teacher = 9,
        [Description("教研组长")]
        TeachingLeader = 3,
        [Description("副校长")]
        vice_principal = 1
    }
}

