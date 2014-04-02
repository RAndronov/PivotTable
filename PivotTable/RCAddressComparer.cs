using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CrossTab
{
    class RCAddressComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}
