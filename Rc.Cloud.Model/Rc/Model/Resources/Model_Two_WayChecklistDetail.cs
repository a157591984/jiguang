namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Two_WayChecklistDetail
    {
        private string _complexitytext;
        private DateTime? _createtime;
        private string _createuser;
        private string _knowledgepoint;
        private string _parentid;
        private string _remark;
        private decimal? _score;
        private string _targettext;
        private string _testpaper_framedetail_id;
        private int? _testquestions_num;
        private string _testquestions_numstr;
        private string _testquestions_type;
        private string _two_waychecklist_id;
        private string _two_waychecklistdetail_id;
        private string _two_waychecklisttype;

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

        public string Two_WayChecklist_Id
        {
            get
            {
                return this._two_waychecklist_id;
            }
            set
            {
                this._two_waychecklist_id = value;
            }
        }

        public string Two_WayChecklistDetail_Id
        {
            get
            {
                return this._two_waychecklistdetail_id;
            }
            set
            {
                this._two_waychecklistdetail_id = value;
            }
        }

        public string Two_WayChecklistType
        {
            get
            {
                return this._two_waychecklisttype;
            }
            set
            {
                this._two_waychecklisttype = value;
            }
        }
    }
}

