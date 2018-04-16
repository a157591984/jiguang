namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TestQuestions
    {
        private DateTime? _createtime;
        private string _parent_id;
        private string _resourcetoresourcefolder_id;
        private string _testquestions_answer;
        private string _testquestions_content;
        private string _testquestions_id;
        private int? _testquestions_num;
        private decimal? _testquestions_sumscore;
        private string _testquestions_type;
        private string _topicnumber;
        private string _type = "simple";

        public DateTime? CreateTime
        {
            get
            {
                return this._createtime;
            }
            set
            {
                this._createtime = value;
            }
        }

        public string Parent_Id
        {
            get
            {
                return this._parent_id;
            }
            set
            {
                this._parent_id = value;
            }
        }

        public string ResourceToResourceFolder_Id
        {
            get
            {
                return this._resourcetoresourcefolder_id;
            }
            set
            {
                this._resourcetoresourcefolder_id = value;
            }
        }

        public string TestQuestions_Answer
        {
            get
            {
                return this._testquestions_answer;
            }
            set
            {
                this._testquestions_answer = value;
            }
        }

        public string TestQuestions_Content
        {
            get
            {
                return this._testquestions_content;
            }
            set
            {
                this._testquestions_content = value;
            }
        }

        public string TestQuestions_Id
        {
            get
            {
                return this._testquestions_id;
            }
            set
            {
                this._testquestions_id = value;
            }
        }

        public int? TestQuestions_Num
        {
            get
            {
                return this._testquestions_num;
            }
            set
            {
                this._testquestions_num = value;
            }
        }

        public decimal? TestQuestions_SumScore
        {
            get
            {
                return this._testquestions_sumscore;
            }
            set
            {
                this._testquestions_sumscore = value;
            }
        }

        public string TestQuestions_Type
        {
            get
            {
                return this._testquestions_type;
            }
            set
            {
                this._testquestions_type = value;
            }
        }

        public string topicNumber
        {
            get
            {
                return this._topicnumber;
            }
            set
            {
                this._topicnumber = value;
            }
        }

        public string type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

