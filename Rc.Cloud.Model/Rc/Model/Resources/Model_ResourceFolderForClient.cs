namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ResourceFolderForClient
    {
        private string _isstore_files;
        private string _resourcefolder_id;
        private string _resourcefolder_level;
        private string _resourcefolder_name;
        private string _resourcefolder_order;
        private string _resourcefolder_parentid;

        public string isStore_Files
        {
            get
            {
                return this._isstore_files;
            }
            set
            {
                this._isstore_files = value;
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

        public string ResourceFolder_Level
        {
            get
            {
                return this._resourcefolder_level;
            }
            set
            {
                this._resourcefolder_level = value;
            }
        }

        public string ResourceFolder_Name
        {
            get
            {
                return this._resourcefolder_name;
            }
            set
            {
                this._resourcefolder_name = value;
            }
        }

        public string ResourceFolder_Order
        {
            get
            {
                return this._resourcefolder_order;
            }
            set
            {
                this._resourcefolder_order = value;
            }
        }

        public string ResourceFolder_ParentId
        {
            get
            {
                return this._resourcefolder_parentid;
            }
            set
            {
                this._resourcefolder_parentid = value;
            }
        }
    }
}

