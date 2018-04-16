namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ExeccuteDataAnalysis
    {
        private string _execcutedataanalysisid;
        private string _execcutelenght;
        private DateTime? _execcutetime;
        private string _execcuteuserid;
        private string _execcuteusername;
        private string _remark;

        public string ExeccuteDataAnalysisID
        {
            get
            {
                return this._execcutedataanalysisid;
            }
            set
            {
                this._execcutedataanalysisid = value;
            }
        }

        public string ExeccuteLenght
        {
            get
            {
                return this._execcutelenght;
            }
            set
            {
                this._execcutelenght = value;
            }
        }

        public DateTime? ExeccuteTime
        {
            get
            {
                return this._execcutetime;
            }
            set
            {
                this._execcutetime = value;
            }
        }

        public string ExeccuteUserId
        {
            get
            {
                return this._execcuteuserid;
            }
            set
            {
                this._execcuteuserid = value;
            }
        }

        public string ExeccuteUserName
        {
            get
            {
                return this._execcuteusername;
            }
            set
            {
                this._execcuteusername = value;
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

