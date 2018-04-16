namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Student_HomeWorkAnswer
    {
        private string _comment;
        private DateTime? _createtime;
        private string _homework_id;
        private int? _isread;
        private string _student_answer;
        private string _student_answer_status;
        private string _student_homework_id;
        private string _student_homeworkanswer_id;
        private string _student_id;
        private decimal? _student_score;
        private int? _testquestions_detail_ordernum;
        private string _testquestions_id;
        private int? _testquestions_num;
        private string _testquestions_numstr;
        private string _testquestions_score_id;

        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

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

        public string HomeWork_Id
        {
            get
            {
                return this._homework_id;
            }
            set
            {
                this._homework_id = value;
            }
        }

        public int? isRead
        {
            get
            {
                return this._isread;
            }
            set
            {
                this._isread = value;
            }
        }

        public string Student_Answer
        {
            get
            {
                return this._student_answer;
            }
            set
            {
                this._student_answer = value;
            }
        }

        public string Student_Answer_Status
        {
            get
            {
                return this._student_answer_status;
            }
            set
            {
                this._student_answer_status = value;
            }
        }

        public string Student_HomeWork_Id
        {
            get
            {
                return this._student_homework_id;
            }
            set
            {
                this._student_homework_id = value;
            }
        }

        public string Student_HomeWorkAnswer_Id
        {
            get
            {
                return this._student_homeworkanswer_id;
            }
            set
            {
                this._student_homeworkanswer_id = value;
            }
        }

        public string Student_Id
        {
            get
            {
                return this._student_id;
            }
            set
            {
                this._student_id = value;
            }
        }

        public decimal? Student_Score
        {
            get
            {
                return this._student_score;
            }
            set
            {
                this._student_score = value;
            }
        }

        public int? TestQuestions_Detail_OrderNum
        {
            get
            {
                return this._testquestions_detail_ordernum;
            }
            set
            {
                this._testquestions_detail_ordernum = value;
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

        public string TestQuestions_NumStr
        {
            get
            {
                return this._testquestions_numstr;
            }
            set
            {
                this._testquestions_numstr = value;
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

