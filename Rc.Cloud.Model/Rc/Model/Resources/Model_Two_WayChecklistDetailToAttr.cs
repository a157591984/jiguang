namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_Two_WayChecklistDetailToAttr
    {
        private string _attr_type;
        private string _attr_value;
        private DateTime? _createtime;
        private string _createuser;
        private string _two_waychecklist_id;
        private string _two_waychecklistdetail_id;
        private string _two_waychecklistdetailtoattr_id;

        public string Attr_Type
        {
            get
            {
                return this._attr_type;
            }
            set
            {
                this._attr_type = value;
            }
        }

        public string Attr_Value
        {
            get
            {
                return this._attr_value;
            }
            set
            {
                this._attr_value = value;
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

        public string Two_WayChecklist_Id
        {
            get
            {
                return this._two_waychecklist_id;
            }
            set
            {
                this._two_waychecklist_id = value;
            }
        }

        public string Two_WayChecklistDetail_Id
        {
            get
            {
                return this._two_waychecklistdetail_id;
            }
            set
            {
                this._two_waychecklistdetail_id = value;
            }
        }

        public string Two_WayChecklistDetailToAttr_Id
        {
            get
            {
                return this._two_waychecklistdetailtoattr_id;
            }
            set
            {
                this._two_waychecklistdetailtoattr_id = value;
            }
        }
    }
}

