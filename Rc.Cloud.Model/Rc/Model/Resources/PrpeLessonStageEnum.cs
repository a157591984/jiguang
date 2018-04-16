namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum PrpeLessonStageEnum
    {
        [Description("创造阶段【编写教案】")]
        创造阶段 = 2,
        [Description("分析阶段")]
        分析阶段 = 1,
        [Description("提高阶段【二次备课，调整和修改教案】")]
        提高阶段 = 3,
        [Description("完成备课")]
        完成备课 = 5,
        [Description("准备阶段")]
        准备阶段 = 0,
        [Description("总结阶段")]
        总结阶段 = 4
    }
}

