namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_ClassPool
    {
        private string _class_id;
        private string _classpool_id;
        private DateTime? _createtime;
        private int? _isenabled;
        private int? _isused;

        public string Class_Id
        {
            get
            {
                return this._class_id;
            }
            set
            {
                this._class_id = value;
            }
        }

        public string ClassPool_Id
        {
            get
            {
                return this._classpool_id;
            }
            set
            {
                this._classpool_id = value;
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

        public int? IsEnabled
        {
            get
            {
                return this._isenabled;
            }
            set
            {
                this._isenabled = value;
            }
        }

        public int? IsUsed
        {
            get
            {
                return this._isused;
            }
            set
            {
                this._isused = value;
            }
        }
    }
}

