namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_HomeworkToTQ
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _homework_id;
        private string _homeworktotq_id;
        private string _resourcetoresourcefolder_id;
        private string _rtrfid_old;
        private int? _sort;
        private string _testquestions_id;
        private string _topicnumber;
        private string _usergroup_id;

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

        public string HomeworkToTQ_Id
        {
            get
            {
                return this._homeworktotq_id;
            }
            set
            {
                this._homeworktotq_id = value;
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

        public string rtrfId_Old
        {
            get
            {
                return this._rtrfid_old;
            }
            set
            {
                this._rtrfid_old = value;
            }
        }

        public int? Sort
        {
            get
            {
                return this._sort;
            }
            set
            {
                this._sort = value;
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

        public string topicNumber
        {
            get
            {
                return this._topicnumber;
            }
            set
            {
                this._topicnumber = value;
            }
        }

        public string UserGroup_Id
        {
            get
            {
                return this._usergroup_id;
            }
            set
            {
                this._usergroup_id = value;
            }
        }
    }
}

