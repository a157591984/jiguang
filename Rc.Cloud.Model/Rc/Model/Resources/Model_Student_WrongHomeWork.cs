namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Student_WrongHomeWork
    {
        private DateTime? _createtime;
        private string _student_homeworkanswer_id;
        private string _student_wronghomework_id;

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

        public string Student_HomeWorkAnswer_Id
        {
            get
            {
                return this._student_homeworkanswer_id;
            }
            set
            {
                this._student_homeworkanswer_id = value;
            }
        }

        public string Student_WrongHomeWork_Id
        {
            get
            {
                return this._student_wronghomework_id;
            }
            set
            {
                this._student_wronghomework_id = value;
            }
        }
    }
}

