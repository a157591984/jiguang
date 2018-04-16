namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_DictRelation
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _dictrelation_id;
        private string _headdict_id;
        private string _remark;
        private string _sondict_id;

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

        public string HeadDict_Id
        {
            get
            {
                return this._headdict_id;
            }
            set
            {
                this._headdict_id = value;
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

        public string SonDict_Id
        {
            get
            {
                return this._sondict_id;
            }
            set
            {
                this._sondict_id = value;
            }
        }
    }
}

