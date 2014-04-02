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
            dynamic ret = 0;

            foreach (dynamic t in set)
            {
                ret = ret + t;
            }

            return (object)ret; 
        }

        public static object Average(ArrayList set)
        {
            dynamic d = Sum(set);
            return (object) (d / set.Count);
        }

        public static object Count(ArrayList set) {
            return (object)set.Count;
        }

    }
}
