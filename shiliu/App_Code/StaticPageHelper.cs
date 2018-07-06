using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// StaticPageHelper 的摘要说明
/// </summary>
public class StaticPageHelper
{
    public StaticPageHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();

    /// <summary>
    /// NewsLite插入将要生成的静态页地址
    /// </summary>
    public void insertNewsLitetWriter()
    {
        string sel = "select * from  ML_NewsClass";
        DataTable dts = her.ExecuteDataTable(sel);
        for (int i = 0; i < dts.Rows.Count; i++)
        {
            string sql = "select * from  ML_News where cid0 = " + dts.Rows[i]["nID"].ToString();
            DataTable dt = her.ExecuteDataTable(sql);
            string tWriter = "";
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                tWriter = "NewsLite_List" + dts.Rows[i]["nID"].ToString() + "_" + dt.Rows[a]["nID"].ToString();
                string str = string.Format(@"update ML_News set tWriter='{0}' where nID={1}", tWriter, dt.Rows[a]["nID"].ToString());
                her.ExecuteNonQuery(str);
            }
        }
    }
    /// <summary>
    /// News插入将要生成的静态页地址
    /// </summary>
    public void insertNewstPic()
    {
        string sel = "select * from  ML_NewsClass";
        DataTable dts = her.ExecuteDataTable(sel);
        for (int i = 0; i < dts.Rows.Count; i++)
        {
            string tPic = "News_" + dts.Rows[i]["nID"].ToString();
            string str = string.Format(@"update ML_NewsClass set tPic='{0}' where nID={1}", tPic, dts.Rows[i]["nID"].ToString());
            her.ExecuteNonQuery(str);
        }
    }
}