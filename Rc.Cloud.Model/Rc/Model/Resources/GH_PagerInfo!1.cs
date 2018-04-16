namespace Rc.Model.Resources
{
    using System;
    using System.Runtime.CompilerServices;

    public class GH_PagerInfo<T>
    {
        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public T PageData { get; set; }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }
    }
}

