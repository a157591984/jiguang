namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ResourceToResourceFolder_img
    {
        private DateTime? _createtime;
        private string _resourcetoresourcefolder_id;
        private string _resourcetoresourcefolder_img_id;
        private int? _resourcetoresourcefolderimg_order;
        private string _resourcetoresourcefolderimg_url;

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

        public string ResourceToResourceFolder_id
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

        public string ResourceToResourceFolder_img_id
        {
            get
            {
                return this._resourcetoresourcefolder_img_id;
            }
            set
            {
                this._resourcetoresourcefolder_img_id = value;
            }
        }

        public int? ResourceToResourceFolderImg_Order
        {
            get
            {
                return this._resourcetoresourcefolderimg_order;
            }
            set
            {
                this._resourcetoresourcefolderimg_order = value;
            }
        }

        public string ResourceToResourceFolderImg_Url
        {
            get
            {
                return this._resourcetoresourcefolderimg_url;
            }
            set
            {
                this._resourcetoresourcefolderimg_url = value;
            }
        }
    }
}

