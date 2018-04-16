namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_HWScoreLevelDict
    {
        private decimal? _addedvalue;
        private DateTime? _createtime;
        private string _createuser;
        private string _hwscoreleveldictid;
        private string _hwscorelevelname;
        private decimal? _hwscorelevelrateleft;
        private decimal? _hwscorelevelrateright;
        private string _remark;

        public decimal? AddedValue
        {
            get
            {
                return this._addedvalue;
            }
            set
            {
                this._addedvalue = value;
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

        public string HWScoreLevelDictID
        {
            get
            {
                return this._hwscoreleveldictid;
            }
            set
            {
                this._hwscoreleveldictid = value;
            }
        }

        public string HWScoreLevelName
        {
            get
            {
                return this._hwscorelevelname;
            }
            set
            {
                this._hwscorelevelname = value;
            }
        }

        public decimal? HWScoreLevelRateLeft
        {
            get
            {
                return this._hwscorelevelrateleft;
            }
            set
            {
                this._hwscorelevelrateleft = value;
            }
        }

        public decimal? HWScoreLevelRateRight
        {
            get
            {
                return this._hwscorelevelrateright;
            }
            set
            {
                this._hwscorelevelrateright = value;
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

