namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_AudioVideoBook
    {
        private string _audiovideobookid;
        private string _bookname;
        private DateTime? _createtime;
        private string _createuser;
        private string _gradeterm;
        private int? _particularyear;
        private string _resource_version;
        private string _subject;

        public string AudioVideoBookId
        {
            get
            {
                return this._audiovideobookid;
            }
            set
            {
                this._audiovideobookid = value;
            }
        }

        public string BookName
        {
            get
            {
                return this._bookname;
            }
            set
            {
                this._bookname = value;
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

        public string GradeTerm
        {
            get
            {
                return this._gradeterm;
            }
            set
            {
                this._gradeterm = value;
            }
        }

        public int? ParticularYear
        {
            get
            {
                return this._particularyear;
            }
            set
            {
                this._particularyear = value;
            }
        }

        public string Resource_Version
        {
            get
            {
                return this._resource_version;
            }
            set
            {
                this._resource_version = value;
            }
        }

        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }
    }
}

