namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_F_User_GradeTerm
    {
        private string _gradeterm_id;
        private string _userid;

        public string GradeTerm_ID
        {
            get
            {
                return this._gradeterm_id;
            }
            set
            {
                this._gradeterm_id = value;
            }
        }

        public string UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }
    }
}

