namespace Rc.Cloud.Model
{
    using System;

    [Serializable]
    public class Model_SysRoleParameter
    {
        private Model_SysRole _MODEL_SysRole = new Model_SysRole();

        public Model_SysRole MODEL_SysRole
        {
            get
            {
                return this._MODEL_SysRole;
            }
            set
            {
                this._MODEL_SysRole = value;
            }
        }
    }
}

