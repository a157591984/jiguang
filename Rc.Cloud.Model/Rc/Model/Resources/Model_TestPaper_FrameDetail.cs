namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_TestPaper_FrameDetail
    {
        private string _complexitytext;
        private DateTime? _createtime;
        private string _createuser;
        private string _knowledgepoint;
        private string _parentid;
        private string _remark;
        private decimal? _score;
        private string _targettext;
        private string _testpaper_frame_id;
        private string _testpaper_framedetail_id;
        private string _testpaper_frametype;
        private int? _testquestions_num;
        private string _testquestions_numstr;
        private string _testquestions_type;
        private string _testquestiontype_web;

        public string ComplexityText
        {
            get
            {
                return this._complexitytext;
            }
            set
            {
                this._complexitytext = value;
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

        public string CreateUser
        {
            get
            {
                return this._createuser;
            }
            set
            {
                this._createuser = value;
            }
        }

        public string KnowledgePoint
        {
            get
            {
                return this._knowledgepoint;
            }
            set
            {
                this._knowledgepoint = value;
            }
        }

        public string ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public decimal? Score
        {
            get
            {
                return this._score;
            }
            set
            {
                this._score = value;
            }
        }

        public string TargetText
        {
            get
            {
                return this._targettext;
            }
            set
            {
                this._targettext = value;
            }
        }

        public string TestPaper_Frame_Id
        {
            get
            {
                return this._testpaper_frame_id;
            }
            set
            {
                this._testpaper_frame_id = value;
            }
        }

        public string TestPaper_FrameDetail_Id
        {
            get
            {
                return this._testpaper_framedetail_id;
            }
            set
            {
                this._testpaper_framedetail_id = value;
            }
        }

        public string TestPaper_FrameType
        {
            get
            {
                return this._testpaper_frametype;
            }
            set
            {
                this._testpaper_frametype = value;
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

        public string TestQuestionType_Web
        {
            get
            {
                return this._testquestiontype_web;
            }
            set
            {
                this._testquestiontype_web = value;
            }
        }
    }
}

