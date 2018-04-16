namespace Rc.Common.StrUtility
{
    using System;

    public sealed class GUIDUtility
    {
        public static string GetNewGUIDString()
        {
            return Guid.NewGuid().ToString();
        }

        public static string AdminID
        {
            get
            {
                return "1ebb1705-c073-41e8-b9ab-1ea594abd433";
            }
        }
    }
}

