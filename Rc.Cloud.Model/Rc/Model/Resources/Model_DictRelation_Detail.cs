namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_DictRelation_Detail
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _dict_id;
        private string _dict_type;
        private string _dictrelation_detail_id;
        private string _dictrelation_id;
        private string _parent_id;
        private string _remark;

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

        public string Dict_Id
        {
            get
            {
                return this._dict_id;
            }
            set
            {
                this._dict_id = value;
            }
        }

        public string Dict_Type
        {
            get
            {
                return this._dict_type;
            }
            set
            {
                this._dict_type = value;
            }
        }

        public string DictRelation_Detail_Id
        {
            get
            {
                return this._dictrelation_detail_id;
            }
            set
            {
                this._dictrelation_detail_id = value;
            }
        }

        public string DictRelation_Id
        {
            get
            {
                return this._dictrelation_id;
            }
            set
            {
                this._dictrelation_id = value;
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
    }
}

