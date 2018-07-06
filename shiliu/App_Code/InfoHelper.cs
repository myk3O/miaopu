using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// InfoHelper 的摘要说明
/// </summary>
public class InfoHelper
{
    public InfoHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    //根据ID查询相关信息
    public DataTable getMang(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select ML_Info.*,ML_InfoClass.nID as tClassID,ML_InfoClass.sid0 as ClassSid,ML_InfoClass.tClassName from dbo.ML_Info inner join  dbo.ML_InfoClass on ML_Info.sid0=ML_InfoClass.nID where ML_Info.nID={0}", ID);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 添加企业信息
    /// </summary>
    /// <param name="cID"></param>
    /// <param name="tMemo"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool InfoInsert(string cID, string title, string tMemo, string num)
    {
        SqlParameter sid0 = new SqlParameter("@sid0", cID);
        SqlParameter tTitle = new SqlParameter("@tTitle", title);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter sid2 = new SqlParameter("@sid2", num);
        string dt = System.DateTime.Now.ToString();
        SqlParameter[] count = { sid0, sid2, tTitle, tMemos };
        string insert = "insert into ML_Info values(@sid0,'',@sid2,@tTitle,'',@tMemo,'','" + dt + "','" + dt + "',0)";
        bool success = her.ExecuteNonQuery(insert, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改企业信息
    public bool InfoUpdate(string ID, string cID, string title, string tMemo, string num)
    {
        SqlParameter sid0 = new SqlParameter("@sid0", cID);
        SqlParameter tTitle = new SqlParameter("@tTitle", title);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter sid2 = new SqlParameter("@sid2", num);
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", System.DateTime.Now.ToString());
        SqlParameter[] count = { sid0, sid2, tTitle, tMemos, dtPubTime };
        string update = "update dbo.ML_Info set sid0=@sid0,sid2=@sid2 ,tTitle=@tTitle,tMemo=@tMemo,dtPubTime=@dtPubTime where nID=" + ID;
        bool success = her.ExecuteNonQuery(update, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //删除企业信息
    public bool InfoDelete(string ID)
    {
        string sql = "DELETE FROM [ML_Info] WHERE nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }
    //提取字符串中的汉字
    public string strSub(string sTemp)
    {
        string aa = "";
        for (int c = 0; c < sTemp.Length; c++)
        {
            if ((int)sTemp[c] > 127)
            {
                aa += sTemp[c];
            }
        }
        return aa;
    }

    //添加企业信息分类
    public bool addInfoClass(string sid, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ML_InfoClass values (" + sid + ",0,0,@className,@paixun,0)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除企业信息分类
    public bool DelInfoClass(string nID)
    {
        string str = "delete ML_InfoClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询企业信息分类
    public DataTable SelInfoClass(string nID)
    {
        string str = "select * from ML_InfoClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改企业信息分类
    public bool updateInfoClass(string nID, string sid, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update ML_InfoClass set sid0=" + sid + ",tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
}