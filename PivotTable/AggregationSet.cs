using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CrossTab
{
    /// <summary>
    /// Delegate calculates aggregated value of set
    /// </summary>
    /// <param name="set">ArrayList contains objects of any type</param>
    /// <returns></returns>
    public delegate object AggregationFunction(ArrayList set);

    public class AggregationSet
    {

        static protected Hashtable hValues = new Hashtable(new RCAddressComparer());

        private AggregationFunction aggregationHandler;

        private object DefaultEmptyValue = DBNull.Value;

        /// <summary>
        /// Set custom aggregator if you need more than sum.
        /// <remarks> Setter only!</remarks>
        /// </summary>
        public AggregationFunction Aggregator
        {
            set { aggregationHandler = value; }
        }

        public void AddValue(object RowName, object ColumnName, object Value)
        {

            if (Value is DBNull)
                return;

            ArrayList v;
            RCAddress address = new RCAddress(RowName, ColumnName);

            if (hValues.ContainsKey(address))
            {
                v = (ArrayList)hValues[address];
            }
            else
            {
                v = new ArrayList();
                hValues[address] = v;
            }

            v.Add(Value);
        }

        public object Calculate(object RowName, object ColumnName)
        {
            RCAddress address = new RCAddress(RowName, ColumnName);
            if (hValues.ContainsKey(address))
            {

                if (this.aggregationHandler == null)
                {
                    this.aggregationHandler = AggregateFunction.Sum;
                }

                ArrayList set = (ArrayList)hValues[address];

                return this.aggregationHandler(set);
            }

            return DefaultEmptyValue;
        }
    }
}
