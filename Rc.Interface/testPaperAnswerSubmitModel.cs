using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rc.Interface
{
    public class testPaperAnswerSubmitModel
    {
        public List<TestPaperAnswerModel> answerJson { get; set; }
        public List<TestPaperAnswerModelBig> listBig { get; set; }
        public string booksCode { get; set; }
        public string booksUnitCode { get; set; }
        public string testPaperName { get; set; }
        public string userId { get; set; }

    }
    public class TestPaperAnswerModelBig
    {
        public string docBase64 { get; set; }
        public string docHtml { get; set; }
        public List<TestPaperAnswerModel> list { get; set; }
    }
    public class TestPaperAnswerModel
    {
        public string Testid { get; set; }
        public string topicNumber { get; set; }
        public List<TestPaperAnswerSubModel> list { get; set; }

    }
    public class TestPaperAnswerSubModel
    {
        public string isHTML { get; set; }
        public string answerHTML { get; set; }
        /// <summary>
        /// // 答题内容的Image Base64（目前只对解答里有效）
        /// </summary>
        public string answerImageBase64 { get; set; }
        /// <summary>
        /// // 答题内容的文档 Base64（目前只对填空题有效）
        /// </summary>
        public string answerDocBase64 { get; set; }
        public string answerChooses { get; set; }
        public string isRight { get; set; }
        public string studentScore { get; set; }
    }

    public class StuAnswerForMarkingModel
    {
        public List<TestPaperAnswerModel> list { get; set; }
        public List<TestPaperAnswerModelBig> listBig { get; set; }

    } 

}
