using System;
using System.Collections;
using System.Text;

namespace UnitTest
{
    public class DemoAverageCalc
    {

        // my inner type is double

        public static object Sum(ArrayList set)
        {
            double ret = 0.0;
            foreach (object t in set)
            {
                if(t is double)
                    ret = ret + (double)t;
            }

            return ret;
        }

        /// <summary>
        /// Demo implemetaion of custom average. It will process the same types as contains value column
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static object Average(ArrayList set)
        {

            return (double)Sum(set) / (double)set.Count;
        }

    }
}
