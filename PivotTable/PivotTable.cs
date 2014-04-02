using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CrossTab
{
    public class PivotTable
    {


        AggregationSet dataSets = new AggregationSet();

        public PivotTable(){
            dataSets = new AggregationSet();
            // by default will be summarized 
            dataSets.Aggregator = AggregateFunction.Sum;
        }

        /// <summary>
        /// Init PivotTable instance with own agreggation function.
        ///  See also: <seealso cref="AggregationFunction"/>
        /// </summary>
        /// <param name="af"></param>
        public PivotTable(AggregationFunction af) {
            dataSets = new AggregationSet();
            // set custom aggregation function if you dont need simple sum
            dataSets.Aggregator = af;
        }

        public DataTable Pivot(
            DataTable src,
            string FieldWithColumnLabels,
            string FieldWithRowLabels,
            string FieldWithValues)
        {

            DataTable dtPivot = new DataTable();
            if (src == null || src.Rows.Count == 0)
                return dtPivot;

            // find all distinct names for column and row
            ArrayList PivotColumns = new ArrayList();
            ArrayList PivotRows = new ArrayList();
            foreach (DataRow dr in src.Rows)
            {
                // find all column values
                object column = dr[FieldWithColumnLabels];
                if (!PivotColumns.Contains(column))
                    PivotColumns.Add(column);

                //find all row values
                object row = dr[FieldWithRowLabels];
                if (!PivotRows.Contains(row))
                    PivotRows.Add(row);
            }

            PivotColumns.Sort();
            PivotRows.Sort();

            //create columns
            dtPivot = new DataTable();
            dtPivot.Columns.Add(FieldWithColumnLabels + "/" + FieldWithRowLabels, src.Columns[FieldWithColumnLabels].DataType);

            Type TypeOfValues = src.Columns[FieldWithValues].DataType;
            foreach (object NewColumn in PivotColumns)
            {
                dtPivot.Columns.Add(NewColumn.ToString(), TypeOfValues);
            }

            //create destination rows
            foreach (object NewRowName in PivotRows)
            {
                DataRow NewRow = dtPivot.NewRow();
                NewRow[0] = NewRowName.ToString();
                dtPivot.Rows.Add(NewRow);
            }

            //fill out sets
            foreach (DataRow drSource in src.Rows)
            {
                dataSets.AddValue(
                    drSource[FieldWithRowLabels],
                    drSource[FieldWithColumnLabels],
                    drSource[FieldWithValues]);
            }

            //aggregate and fillout pivot table
            foreach (object row in PivotRows)
            {
                int idxRow = PivotRows.IndexOf(row);

                foreach (object column in PivotColumns)
                {
                    int idxColumn = PivotColumns.IndexOf(column);
                    dtPivot.Rows[idxRow][idxColumn + 1] = dataSets.Calculate(row, column);
                }
            }

            return dtPivot;
        }

    }
}
