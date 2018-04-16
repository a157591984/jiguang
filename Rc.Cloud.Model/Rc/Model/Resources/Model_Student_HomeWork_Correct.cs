namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Student_HomeWork_Correct
    {
        private string _correctmode;
        private DateTime? _correcttime;
        private string _correctuser;
        private int? _student_homework_correctstatus;
        private string _student_homework_id;

        public string CorrectMode
        {
            get
            {
                return this._correctmode;
            }
            set
            {
                this._correctmode = value;
            }
        }

        public DateTime? CorrectTime
        {
            get
            {
                return this._correcttime;
            }
            set
            {
                this._correcttime = value;
            }
        }

        public string CorrectUser
        {
            get
            {
                return this._correctuser;
            }
            set
            {
                this._correctuser = value;
            }
        }

        public int? Student_HomeWork_CorrectStatus
        {
            get
            {
                return this._student_homework_correctstatus;
            }
            set
            {
                this._student_homework_correctstatus = value;
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
    }
}

