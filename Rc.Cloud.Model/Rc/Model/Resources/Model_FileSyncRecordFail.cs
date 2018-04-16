namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_FileSyncRecordFail
    {
        private string _book_id;
        private string _file_type;
        private string _filesyncrecordfail_id;
        private string _fileurl;
        private string _resource_type;
        private string _resourcetoresourcefolder_id;
        private DateTime? _syncfailtime;

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

        public string File_Type
        {
            get
            {
                return this._file_type;
            }
            set
            {
                this._file_type = value;
            }
        }

        public string FileSyncRecordFail_Id
        {
            get
            {
                return this._filesyncrecordfail_id;
            }
            set
            {
                this._filesyncrecordfail_id = value;
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

        public DateTime? SyncFailTime
        {
            get
            {
                return this._syncfailtime;
            }
            set
            {
                this._syncfailtime = value;
            }
        }
    }
}

