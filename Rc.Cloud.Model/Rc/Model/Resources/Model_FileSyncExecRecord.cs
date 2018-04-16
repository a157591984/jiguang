namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_FileSyncExecRecord
    {
        private string _createuser;
        private string _filesyncexecrecord_condition;
        private string _filesyncexecrecord_id;
        private string _filesyncexecrecord_remark;
        private string _filesyncexecrecord_status;
        private DateTime? _filesyncexecrecord_timeend;
        private DateTime? _filesyncexecrecord_timestart;
        private string _filesyncexecrecord_type;

        public string createUser
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

        public string FileSyncExecRecord_Condition
        {
            get
            {
                return this._filesyncexecrecord_condition;
            }
            set
            {
                this._filesyncexecrecord_condition = value;
            }
        }

        public string FileSyncExecRecord_id
        {
            get
            {
                return this._filesyncexecrecord_id;
            }
            set
            {
                this._filesyncexecrecord_id = value;
            }
        }

        public string FileSyncExecRecord_Remark
        {
            get
            {
                return this._filesyncexecrecord_remark;
            }
            set
            {
                this._filesyncexecrecord_remark = value;
            }
        }

        public string FileSyncExecRecord_Status
        {
            get
            {
                return this._filesyncexecrecord_status;
            }
            set
            {
                this._filesyncexecrecord_status = value;
            }
        }

        public DateTime? FileSyncExecRecord_TimeEnd
        {
            get
            {
                return this._filesyncexecrecord_timeend;
            }
            set
            {
                this._filesyncexecrecord_timeend = value;
            }
        }

        public DateTime? FileSyncExecRecord_TimeStart
        {
            get
            {
                return this._filesyncexecrecord_timestart;
            }
            set
            {
                this._filesyncexecrecord_timestart = value;
            }
        }

        public string FileSyncExecRecord_Type
        {
            get
            {
                return this._filesyncexecrecord_type;
            }
            set
            {
                this._filesyncexecrecord_type = value;
            }
        }
    }
}

