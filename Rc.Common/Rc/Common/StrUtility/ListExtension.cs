namespace Rc.Common.StrUtility
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class ListExtension
    {
        public static string Join<T>(this List<T> items, string splitStr = ",")
        {
            if ((items == null) || (items.Count == 0))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(splitStr);
                }
                builder.Append(items[i].ToString());
            }
            return builder.ToString();
        }
    }
}

