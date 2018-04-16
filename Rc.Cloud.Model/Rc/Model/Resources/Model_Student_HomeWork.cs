namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Student_HomeWork
    {
        private DateTime? _createtime;
        private string _homework_id;
        private string _student_homework_id;
        private string _student_id;

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

        public string Student_HomeWork_Id
        {
            get
            {
                return this._student_homework_id;
            }
            set
            {
                this._student_homework_id = value;
            }
        }

        public string Student_Id
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
    }
}

