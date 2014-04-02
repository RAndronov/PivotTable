using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CrossTab
{
    public class AggregateFunction
    {

        public static object Sum(ArrayList set)
        {
            double ret = 0.0;

            foreach (object t in set)
            {
                ret = ret + Convert.ToDouble(t);
            }

            return (object)ret; 
        }

        public static object Average(ArrayList set)
        {
            double d = (double)Sum(set);
            return (object) (d / set.Count);
        }

        public static object Count(ArrayList set) {
            return (object)set.Count;
        }

    }
}
