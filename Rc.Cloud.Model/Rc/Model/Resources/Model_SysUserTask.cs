namespace Rc.Model.Resources
{
    using System;

    [Serializable]
    public class Model_SysUserTask
    {
        private DateTime? _createtime;
        private string _createuser;
        private string _sysuser_id;
        private string _sysusertask_id;
        private string _taskid;
        private string _taskname;
        private string _tasktype;

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

        public string SysUser_ID
        {
            get
            {
                return this._sysuser_id;
            }
            set
            {
                this._sysuser_id = value;
            }
        }

        public string SysUserTask_id
        {
            get
            {
                return this._sysusertask_id;
            }
            set
            {
                this._sysusertask_id = value;
            }
        }

        public string TaskID
        {
            get
            {
                return this._taskid;
            }
            set
            {
                this._taskid = value;
            }
        }

        public string TaskName
        {
            get
            {
                return this._taskname;
            }
            set
            {
                this._taskname = value;
            }
        }

        public string TaskType
        {
            get
            {
                return this._tasktype;
            }
            set
            {
                this._tasktype = value;
            }
        }
    }
}

