namespace Rc.Model.Resources
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class Model_StudentToParent
    {
        private DateTime? _createtime;
        private string _parent_id;
        private string _student_id;
        private string _studenttoparent_id;

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

        public string Email { get; set; }

        public string Parent_ID
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

        public string Student_ID
        {
            get
            {
                return this._student_id;
            }
            set
            {
                this._student_id = value;
            }
        }

        public string StudentToParent_ID
        {
            get
            {
                return this._studenttoparent_id;
            }
            set
            {
                this._studenttoparent_id = value;
            }
        }

        public string UserName { get; set; }
    }
}

