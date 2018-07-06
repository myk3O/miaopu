using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SqlBulkCopyUtils
/// </summary>
public class SqlBulkCopyUtils
{
    public SqlBulkCopyUtils()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void BulkToDB(DataTable dt)
    {
        SqlConnection sqlConn = new SqlConnection(
        ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
        SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
        bulkCopy.DestinationTableName = "BulkTestTable";
        bulkCopy.BatchSize = dt.Rows.Count;

        try
        {
            sqlConn.Open();
            if (dt != null && dt.Rows.Count != 0)
                bulkCopy.WriteToServer(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            sqlConn.Close();
            if (bulkCopy != null)
                bulkCopy.Close();
        }
    }
    public static DataTable GetTableSchema()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[]{ 
            new DataColumn("Id",typeof(int)), 
            new DataColumn("UserName",typeof(string)), 
            new DataColumn("Pwd",typeof(string))
        });

        return dt;
    }

    public void CopyInsert()
    {
       // Stopwatch sw = new Stopwatch();
        //for (int multiply = 0; multiply < 10; multiply++)
        //{
        //    DataTable dt = Bulk.GetTableSchema();
        //    for (int count = multiply * 100000; count < (multiply + 1) * 100000; count++)
        //    {
        //        DataRow r = dt.NewRow();
        //        r[0] = count;
        //        r[1] = string.Format("User-{0}", count * multiply);
        //        r[2] = string.Format("Pwd-{0}", count * multiply);
        //        dt.Rows.Add(r);
        //    }
        //    //sw.Start();
        //    Bulk.BulkToDB(dt);
        //    //sw.Stop();
        //}
    }
}