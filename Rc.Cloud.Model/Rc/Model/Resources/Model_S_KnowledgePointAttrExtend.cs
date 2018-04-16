namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_S_KnowledgePointAttrExtend
    {
        private DateTime? _createtime;
        private decimal? _maxvalue;
        private decimal? _minvalue;
        private string _s_knowledgepointattrenum;
        private string _s_knowledgepointattrextend_id;
        private string _s_knowledgepointattrname;
        private decimal? _s_knowledgepointattrvalue;
        private string _s_knowledgepointbasic_id;

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

        public decimal? MaxValue
        {
            get
            {
                return this._maxvalue;
            }
            set
            {
                this._maxvalue = value;
            }
        }

        public decimal? MinValue
        {
            get
            {
                return this._minvalue;
            }
            set
            {
                this._minvalue = value;
            }
        }

        public string S_KnowledgePointAttrEnum
        {
            get
            {
                return this._s_knowledgepointattrenum;
            }
            set
            {
                this._s_knowledgepointattrenum = value;
            }
        }

        public string S_KnowledgePointAttrExtend_Id
        {
            get
            {
                return this._s_knowledgepointattrextend_id;
            }
            set
            {
                this._s_knowledgepointattrextend_id = value;
            }
        }

        public string S_KnowledgePointAttrName
        {
            get
            {
                return this._s_knowledgepointattrname;
            }
            set
            {
                this._s_knowledgepointattrname = value;
            }
        }

        public decimal? S_KnowledgePointAttrValue
        {
            get
            {
                return this._s_knowledgepointattrvalue;
            }
            set
            {
                this._s_knowledgepointattrvalue = value;
            }
        }

        public string S_KnowledgePointBasic_Id
        {
            get
            {
                return this._s_knowledgepointbasic_id;
            }
            set
            {
                this._s_knowledgepointbasic_id = value;
            }
        }
    }
}

