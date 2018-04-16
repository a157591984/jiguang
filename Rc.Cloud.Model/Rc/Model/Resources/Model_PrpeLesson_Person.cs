namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_PrpeLesson_Person
    {
        private string _chargeperson;
        private DateTime? _createtime;
        private string _createuser;
        private string _prpelesson_person_id;
        private string _resourcefolder_id;

        public string ChargePerson
        {
            get
            {
                return this._chargeperson;
            }
            set
            {
                this._chargeperson = value;
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

        public string PrpeLesson_Person_Id
        {
            get
            {
                return this._prpelesson_person_id;
            }
            set
            {
                this._prpelesson_person_id = value;
            }
        }

        public string ResourceFolder_Id
        {
            get
            {
                return this._resourcefolder_id;
            }
            set
            {
                this._resourcefolder_id = value;
            }
        }
    }
}

