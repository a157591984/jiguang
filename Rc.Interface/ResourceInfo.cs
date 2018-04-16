using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rc.Interface
{
    /// <summary>
    /// 接口名称: testLibrarySubmit 提交题库
    /// </summary>
    public class ResourceInfo
    {
        public List<AnswerJson> list { get; set; }
        public List<AnswerJsonBig> listBig { get; set; }
        public string testPaperFileBase64 { get; set; }
        public string booksCode { get; set; }
        public string booksUnitCode { get; set; }
        public string guidDoc { get; set; }
        public string testPaperName { get; set; }
        public string paperHeaderDoc { get; set; }
        public string paperHeaderHtml { get; set; }

    }
    public class AnswerJsonBig
    {
        /// <summary>
        /// 类型 (simple普通题型/complex综合题型)
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 题序 (目前WEB端不用，WEB端使用数组的顺序记录）
        /// </summary>
        public string topicNumber { get; set; }
        /// <summary>
        /// 本题内容（Base64）
        /// </summary>
        public string docBase64 { get; set; }
        /// <summary>
        /// 本题内容（HTML）
        /// </summary>
        public string docHtml { get; set; }
        /// <summary>
        /// 一个答案对应一套双向细目
        /// </summary>
        public List<AnswerJson> list { get; set; }
    }
    public class AnswerJson
    {
        /// <summary>
        /// 试题的GUID(),新建的试卷为空值 ； 从客户端任务窗格中打开时，从接口传入
        /// </summary>
        public string Testid { get; set; }
        /// <summary>
        /// 题型 (selection选择题/clozeTest完形填空题/fill填空题/answers解答题/truefalse判断题/title标题)
        /// </summary>
        public string testType { get; set; }
        /// <summary>
        /// 题序 (目前WEB端不用，WEB端使用数组的顺序记录）
        /// </summary>
        public string topicNumber { get; set; }
        /// <summary>
        /// 本题内容（Base64）
        /// </summary>
        public string docBase64 { get; set; }
        /// <summary>
        /// 本题内容（HTML）
        /// </summary>
        public string docHtml { get; set; }
        /// <summary>
        /// 一个答案对应一套双向细目
        /// </summary>
        public List<AnswerScoreJson> list { get; set; }
    }
    public class AnswerScoreJson
    {
        /// <summary>
        /// "bodySubHtml": "子题题干Html",   // 子题题干，目前第一题的子题题干包括主题体感
        /// </summary>
        public string bodySubHtml { get; set; }
        public string analyzeHyperlink { get; set; }
        public string analyzeText { get; set; }
        public string analyzeHyperlinkData { get; set; }
        public string analyzeHyperlinkHtml { get; set; }
        public string complexityHyperlink { get; set; }
        public string complexityText { get; set; }
        public string contentHyperlink { get; set; }
        public string contentText { get; set; }
        public string scoreHyperlink { get; set; }
        public string targetHyperlink { get; set; }
        public string targetText { get; set; }
        public string trainHyperlink { get; set; }
        public string trainText { get; set; }
        public string trainHyperlinkData { get; set; }
        public string trainHyperlinkHtml { get; set; }
        public string areaHyperlink { get; set; }
        public string areaText { get; set; }
        public string typeHyperlink { get; set; }
        public string typeText { get; set; }
        public string scoreText { get; set; }
        /// <summary>
        /// /* 填空题、解答题专用、判断题专用 */
        /// </summary>
        public string testCorrectHTML { get; set; }
        /// <summary>
        /// /* 选择题专用 */
        /// </summary>
        public string testCorrect { get; set; }
        public List<TestSelections> testSelections { get; set; }
        public string kaofaText { get; set; }
        public string testIndex { get; set; }
    }
    public class TestSelections
    {
        public string selectionHTML { get; set; }
        public string selectionText { get; set; }
    }
}
