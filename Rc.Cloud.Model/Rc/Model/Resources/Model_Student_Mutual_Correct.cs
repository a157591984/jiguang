﻿namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Student_Mutual_Correct
    {
        private string _correct_guid;
        private DateTime? _createtime;
        private string _homework_id;
        private string _remark;
        private string _student_id;
        private string _student_mutual_correct_id;

        public string Correct_Guid
        {
            get
            {
                return this._correct_guid;
            }
            set
            {
                this._correct_guid = value;
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

        public string Student_Mutual_Correct_Id
        {
            get
            {
                return this._student_mutual_correct_id;
            }
            set
            {
                this._student_mutual_correct_id = value;
            }
        }
    }
}

