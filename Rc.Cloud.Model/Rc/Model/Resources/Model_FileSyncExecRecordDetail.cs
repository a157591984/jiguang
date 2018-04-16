namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_FileSyncExecRecordDetail
    {
        private string _book_id;
        private string _detail_remark;
        private string _detail_status;
        private DateTime? _detail_timeend;
        private DateTime? _detail_timestart;
        private string _filesyncexecrecord_id;
        private string _filesyncexecrecorddetail_id;
        private string _fileurl;
        private string _resource_type;
        private string _resourcetoresourcefolder_id;
        private string _testquestions_id;

        public string Book_Id
        {
            get
            {
                return this._book_id;
            }
            set
            {
                this._book_id = value;
            }
        }

        public string Detail_Remark
        {
            get
            {
                return this._detail_remark;
            }
            set
            {
                this._detail_remark = value;
            }
        }

        public string Detail_Status
        {
            get
            {
                return this._detail_status;
            }
            set
            {
                this._detail_status = value;
            }
        }

        public DateTime? Detail_TimeEnd
        {
            get
            {
                return this._detail_timeend;
            }
            set
            {
                this._detail_timeend = value;
            }
        }

        public DateTime? Detail_TimeStart
        {
            get
            {
                return this._detail_timestart;
            }
            set
            {
                this._detail_timestart = value;
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

        public string FileSyncExecRecordDetail_id
        {
            get
            {
                return this._filesyncexecrecorddetail_id;
            }
            set
            {
                this._filesyncexecrecorddetail_id = value;
            }
        }

        public string FileUrl
        {
            get
            {
                return this._fileurl;
            }
            set
            {
                this._fileurl = value;
            }
        }

        public string Resource_Type
        {
            get
            {
                return this._resource_type;
            }
            set
            {
                this._resource_type = value;
            }
        }

        public string ResourceToResourceFolder_Id
        {
            get
            {
                return this._resourcetoresourcefolder_id;
            }
            set
            {
                this._resourcetoresourcefolder_id = value;
            }
        }

        public string TestQuestions_Id
        {
            get
            {
                return this._testquestions_id;
            }
            set
            {
                this._testquestions_id = value;
            }
        }
    }
}

