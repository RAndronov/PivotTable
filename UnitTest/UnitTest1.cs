using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrossTab;
using System.Data;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TestMethod1()
        {
            object DBNULL = DBNull.Value;
            DataTable src = new DataTable();
            DataTable dst = null;

            src.Columns.Add("City", typeof(string));
            src.Columns.Add("Product", typeof(string));
            src.Columns.Add("Value", typeof(double));
            
            src.Rows.Add("City A", "Product 1", 1.2);
            src.Rows.Add("City C", "Product 2", 0.3);
            src.Rows.Add("City A", "Product 1", 1.0);
            src.Rows.Add("City B", "Product 3", 2.2);
            src.Rows.Add("City B", "Product 1", 1.5);
            src.Rows.Add("City A", "Product 2", 0.8);
            src.Rows.Add("City A", "Product 4", 1.1);

            PivotTable test = new PivotTable(DemoAverageCalc.Average);
            dst = test.Pivot(src, "Product", "City", "Value");

            CollectionAssert.AreEqual(new object[] { "City A", 1.1,    0.8,   DBNULL, 1.1   }, dst.Rows[0].ItemArray);
            CollectionAssert.AreEqual(new object[] { "City B", 1.5,    DBNULL, 2.2,   DBNULL }, dst.Rows[1].ItemArray);
            CollectionAssert.AreEqual(new object[] { "City C", DBNULL,  0.3,   DBNULL, DBNULL }, dst.Rows[2].ItemArray);

        }
    }
}
