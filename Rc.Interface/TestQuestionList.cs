using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rc.Interface
{
    public class TestQuestionModel
    {
        public string Testid { get; set; }
        public string testType { get; set; }
        public string docBase64 { get; set; }
        public List<TQ_Score> list { get; set; }
        
    }

    public class TQ_Score
    {
        public string testIndex { get; set; }
        public string scoreText { get; set; }
        //public string testCorrectBase64 { get; set; }
        public string testCorrect { get; set; }
    }

    public class TchForMarkingModel
    {
        public List<TestQuestionModel> list { get; set; }
        public List<TQAnswerModelBig> listBig { get; set; }
    }
    public class TQAnswerModelBig
    {
        public string docBase64 { get; set; }
        public string docHtml { get; set; }
        public List<TestQuestionModel> list { get; set; }
    }

}
