namespace Rc.Model.Resources
{
    using System;
    using System.ComponentModel;

    public enum KnowledgePointAttrEnum
    {
        [Description("重要程度")]
        attrKnowledgeImportant = 0,
        [Description("中考分值")]
        attrKnowledgeScore = 1
    }
}

