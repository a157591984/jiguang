namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TestQuestions_Option
    {
        private DateTime? _createtime;
        private string _testquestions_id;
        private string _testquestions_option_content;
        private string _testquestions_option_id;
        private int? _testquestions_option_ordernum;
        private int? _testquestions_optionparent_ordernum;
        private string _testquestions_score_id;

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

        public string TestQuestions_Option_Content
        {
            get
            {
                return this._testquestions_option_content;
            }
            set
            {
                this._testquestions_option_content = value;
            }
        }

        public string TestQuestions_Option_Id
        {
            get
            {
                return this._testquestions_option_id;
            }
            set
            {
                this._testquestions_option_id = value;
            }
        }

        public int? TestQuestions_Option_OrderNum
        {
            get
            {
                return this._testquestions_option_ordernum;
            }
            set
            {
                this._testquestions_option_ordernum = value;
            }
        }

        public int? TestQuestions_OptionParent_OrderNum
        {
            get
            {
                return this._testquestions_optionparent_ordernum;
            }
            set
            {
                this._testquestions_optionparent_ordernum = value;
            }
        }

        public string TestQuestions_Score_ID
        {
            get
            {
                return this._testquestions_score_id;
            }
            set
            {
                this._testquestions_score_id = value;
            }
        }
    }
}

