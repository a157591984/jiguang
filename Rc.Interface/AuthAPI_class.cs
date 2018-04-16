using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.Interface
{

    #region 获取测评结果 类
    public class AnswerResultModel
    {
        public List<object> SingleChoice { get; set; }
        public List<object> MultipleChoice { get; set; }
        public List<object> BlankFilling { get; set; }
        public List<object> AnswerQuestions { get; set; }
        public List<object> JudgmentQuestions { get; set; }
    }
    #endregion

    #region 客户端标签枚举 类
    /// <summary>
    /// 客户端标签枚举
    /// </summary>
    public enum EnumTabindex
    {
        /// <summary>
        /// 管理员scienceword云教案
        /// </summary>
        MgrScienceWordCloudTeachingPlan,
        /// <summary>
        /// 管理员scienceword云习题集
        /// </summary>
        MgrScienceWordCloudSkill,
        /// <summary>
        /// 管理员class云教案
        /// </summary>
        MgrClassCloudTeachingPlan,
        /// <summary>
        /// 管理员class微课件
        /// </summary>
        MgrClassCloudMicroClass,
        /// <summary>
        /// 老师scienceword云教案
        /// </summary>
        TeacherScienceWordCloudTeachingPlan,
        /// <summary>
        /// 老师scienceword自有教案
        /// </summary>
        TeacherScienceWordOwnTeachingPlan,
        /// <summary>
        /// 老师scienceword云习题集
        /// </summary>
        TeacherScienceWordCloudSkill,
        /// <summary>
        /// 老师scienceword自有习题集
        /// </summary>
        TeacherScienceWordOwnSkill,
        /// <summary>
        /// 老师scienceword集体备课
        /// </summary>
        TeacherScienceWordCollectiveLessonPreparation,

        /// <summary>
        /// 老师class云教案
        /// </summary>
        TeacherClassCloudTeachingPlan,
        /// <summary>
        /// 老师class讲评
        /// </summary>
        TeacherClassComment,
        /// <summary>
        /// 老师class自有教案
        /// </summary>
        TeacherClassOwnTeachingPlan,
        /// <summary>
        /// 老师class集体备课
        /// </summary>
        TeacherClassCollectiveLessonPreparation,

        /// <summary>
        /// 学生skill最新作业
        /// </summary>
        StudentSkillNew,
        /// <summary>
        /// 学生skill已提交作业
        /// </summary>
        StudentSkillSubminted,
        /// <summary>
        /// 学生skill错题集
        /// </summary>
        StudentSkillWrong,
        /// <summary>
        /// 学生class微课件
        /// </summary>
        StudentClassMicroClass,
        /// <summary>
        /// 学生class云教案
        /// </summary>
        StudentClassCloudTeachingPlan
    }
    #endregion
    public class CommonDictModel
    {
        /// <summary>
        /// 字典标识
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string D_Name { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public string D_Type { get; set; }
    }
    /// <summary>
    /// 资源属性 类
    /// </summary>
    public class BookAttrModel
    {
        /// <summary>
        /// 是否允许打印
        /// </summary>
        public bool IsPrint { get; set; }
        /// <summary>
        /// 是否允许存盘
        /// </summary>
        public bool IsSave { get; set; }
        /// <summary>
        /// 是否允许复制
        /// </summary>
        public bool IsCopy { get; set; }
    }
}
