namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum TestQuestions_Type
    {
        [Description("解答题")]
        answers = 4,
        [Description("完形填空")]
        clozeTest = 1,
        [Description("综合题")]
        complex = 5,
        [Description("填空题")]
        fill = 3,
        [Description("选择题")]
        selection = 0,
        [Description("判断题")]
        truefalse = 2
    }
}

