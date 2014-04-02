using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CrossTab
{
    public class RCAddress
    {

        private static ArrayList list;

        private object oRowName;
        private object oColumnName;

        static RCAddress() { list = new ArrayList(); }

        public RCAddress(object RowName, object ColumnName)
        {
            this.oRowName = RowName;
            this.oColumnName = ColumnName;
        }

        public override int GetHashCode()
        {
            return (this.oRowName.GetHashCode() ^ this.oColumnName.GetHashCode()) & 0x7FFFFFFF;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            RCAddress o = (RCAddress)obj;

            return (this.oRowName == o.oRowName && this.oColumnName == o.oColumnName);
        }

    }
}
