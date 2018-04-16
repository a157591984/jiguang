namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ChapterAssembly_TQ
    {
        private string _chapterassembly_tq_id;
        private string _complexitytext;
        private string _identifier_id;
        private string _parent_id;
        private string _resourcetoresourcefolder_id;
        private string _testquestions_id;
        private int? _tq_order;
        private string _tq_type;
        private string _type;

        public string ChapterAssembly_TQ_Id
        {
            get
            {
                return this._chapterassembly_tq_id;
            }
            set
            {
                this._chapterassembly_tq_id = value;
            }
        }

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

        public string Identifier_Id
        {
            get
            {
                return this._identifier_id;
            }
            set
            {
                this._identifier_id = value;
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

        public int? TQ_Order
        {
            get
            {
                return this._tq_order;
            }
            set
            {
                this._tq_order = value;
            }
        }

        public string TQ_Type
        {
            get
            {
                return this._tq_type;
            }
            set
            {
                this._tq_type = value;
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

