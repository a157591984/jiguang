namespace Rc.Cloud.Model
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Model_Struct_Func
    {
        public bool page;
        public bool Add;
        public bool Edit;
        public bool Delete;
        public bool Select;
        public bool Check;
        public bool Input;
        public bool Output;
        public bool Synchronization;
        public bool Move;
        public bool Copy;
    }
}

