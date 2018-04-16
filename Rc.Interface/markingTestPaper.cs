using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.Interface
{
    public class TchMKModel
    {
        /// <summary>
        /// 老师批改笔记信息
        /// </summary>
        public string testpaperMarking { get; set; }
        /// <summary>
        /// 试题列表（数组）
        /// </summary>
        public List<TchMKTQList> testList { get; set; }
        /// <summary>
        /// 综合题型（数组）
        /// </summary>
        public List<TchMKTQListBig> testListBig { get; set; }
    }

    public class TchMKTQListBig
    {
        public string docBase64 { get; set; }
        public string docHtml { get; set; }
        public List<TchMKTQList> testList { get; set; }
    }

    public class TchMKTQList
    {
        /// <summary>
        /// 试题Id
        /// </summary>
        public string Testid { get; set; }
        /// <summary>
        /// 题型 (selection选择题/clozeTest完形填空题/fill填空题/answers解答题/truefalse判断题/title标题)
        /// </summary>
        public string testType { get; set; }
        /// <summary>
        /// 批注
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 分值列表（多空数组） 只传输主观题（填空题、解答题）
        /// </summary>
        public List<TchMKTQSList> list { get; set; }
    }
    public class TchMKTQSList
    {
        /// <summary>
        /// 小题分数
        /// </summary>
        public string scoreText { get; set; }
        /// <summary>
        /// 学生得分
        /// </summary>
        public string studentScore { get; set; }
    }
}
