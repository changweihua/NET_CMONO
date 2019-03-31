using System;
using System.Collections.Generic;

namespace NET_CMONO.Framework.Util
{
    public sealed class EnumUtil
    {
        public static IEnumerable<T> GetEnumValuesFromFlagsEnum<T>(Int32 value) where T : struct
        {
            T[] values = (T[])Enum.GetValues(typeof(T));

            foreach (var itemValue in values)
            {
                if ((value & Convert.ToInt32(itemValue)) != 0)
                {
                    yield return itemValue;
                }
            }
        }
    }
}