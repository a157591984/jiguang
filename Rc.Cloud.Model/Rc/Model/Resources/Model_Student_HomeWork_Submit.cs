namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Student_HomeWork_Submit
    {
        private DateTime? _opentime;
        private DateTime? _student_answer_time;
        private string _student_homework_id;
        private int? _student_homework_status;
        private string _studentip;

        public DateTime? OpenTime
        {
            get
            {
                return this._opentime;
            }
            set
            {
                this._opentime = value;
            }
        }

        public DateTime? Student_Answer_Time
        {
            get
            {
                return this._student_answer_time;
            }
            set
            {
                this._student_answer_time = value;
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

        public int? Student_HomeWork_Status
        {
            get
            {
                return this._student_homework_status;
            }
            set
            {
                this._student_homework_status = value;
            }
        }

        public string StudentIP
        {
            get
            {
                return this._studentip;
            }
            set
            {
                this._studentip = value;
            }
        }
    }
}

