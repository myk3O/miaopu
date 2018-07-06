using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Maliang;

/// <summary>
/// ActiveHelp 的摘要说明
/// </summary>
public class ActiveHelp
{
    private SqlHelper her = new SqlHelper();

    public ActiveHelp()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public bool NewsDelete(string ID)
    {
        string sql = "delete Active where nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }

    public DataTable getMang(string ID)
    {
        DataTable dt = new DataTable();
        string sql =
            string.Format(
                @"select Active.*,ActiveClass.nID as tClassID,ActiveClass.tClassName from dbo.Active inner join ActiveClass on Active.tClassName=ActiveClass.nID where Active.nID={0}",
                ID);
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 获取数据库中所有的值
    /// </summary>
    /// <returns></returns>
    public DataTable GetActive()
    {
        string sql = "select nID,TclassName,tMemo,tMemoList,dtActMethods,dtActCost,dtPubTime,dtAddtime from Active";
       // string sql = "select top 5 nID,tTitle,dtAddTime from dbo.ML_News";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }

    //添加活动
    public bool NewsInsert(string dropGroup, string tTlitle, string memo, string dtActCost, string time)
    {
        SqlParameter sid0 = new SqlParameter("@sid0", dropGroup);
        SqlParameter memos = new SqlParameter("@tMemo", tTlitle);
     //   SqlParameter tMemoList = new SqlParameter("@tMemoList", pic);
        SqlParameter dtActMethodes = new SqlParameter("@dtActMethod", memo);
        SqlParameter dtActCosts = new SqlParameter("@dtActCost", dtActCost);
        // SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter addtime = new SqlParameter("@dtAddtime", time);
        SqlParameter[] count = { sid0, memos, dtActMethodes, dtActCosts, addtime };
        string sql =
            "insert into Active values ('@sid0','','','','','','',@tMemo,'',@dtActMethod,@dtActCost,'','','',@dtAddtime,0)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    //添加活动
    public bool NewsInsert(string dropGroup, string memo, string pic, string dtActMethods, string dtActCost, string time)
    {
        SqlParameter sid0 = new SqlParameter("@sid0", dropGroup);
        SqlParameter memos = new SqlParameter("@tMemo", memo);
        SqlParameter tMemoList = new SqlParameter("@tMemoList", pic);
        SqlParameter dtActMethod = new SqlParameter("@dtActMethods", dtActMethods);
        SqlParameter dtActCosts = new SqlParameter("@dtActCost", dtActCost);
       // SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter addtime = new SqlParameter("@dtAddtime", time);
        SqlParameter[] count = { sid0, memos, tMemoList, dtActMethod, dtActCosts, addtime };
        string sql = "insert into Active values ('@sid0','','','','','','',@tMemo,@tMemoList,@dtActMethods,@dtActCost,'','','',@dtAddtime,0)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    //修改活动(不含Pic)
    public bool NewsUpdate(string ID, string dropGroup, string memo, string dtActMethods, string dtActCost)
    {
        SqlParameter cid = new SqlParameter("@sid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tMemo", memo);
        SqlParameter tmemo = new SqlParameter("@dtActMethods", dtActMethods);
        SqlParameter tfromwhere = new SqlParameter("@dtActCost", dtActCost);
      //SqlParameter tTop = new SqlParameter("@tTop", top);
        SqlParameter[] count = { cid, tlitle, tmemo, tfromwhere};
        string sql = "update Active set sid0=@sid0,tMemo=@tMemo,dtActMethods=@dtActMethods,dtActCost=@dtActCost where nID=" + ID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 修改活动
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="dropGroup"></param>
    /// <param name="tTlitle"></param>
    /// <param name="pic"></param>
    /// <param name="memo"></param>
    /// <param name="fromwhere"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public bool NewsUpdate(string ID, string dropGroup, string memo, string tMemoList, string dtActMethods, string dtActCost)
    {
        SqlParameter cid = new SqlParameter("@sid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tMemo", memo);
        SqlParameter tpic = new SqlParameter("@tMemoList", tMemoList);
        SqlParameter tmemo = new SqlParameter("@dtActMethods", dtActMethods);
        SqlParameter tfromwhere = new SqlParameter("@dtActCost", dtActCost);
      //  SqlParameter tTop = new SqlParameter("@tTop", top);
        SqlParameter[] count = { cid, tlitle, tpic, tmemo, tfromwhere};

        string sql = "update Active set sid0=@sid0,tMemo=@tMemo,dtActMethods=@dtActMethods,dtActCost=@dtActCost  where nID=" + ID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    public string NewsDelPhoto(string ID)
    {
        string sql = "select tPic from  Active where nID=" + ID;
        string str = (string)her.ExecuteScalar(sql);
        return str;
    }
    //删除分类
    public bool DelNewsClass(string nID)
    {
        string str = "delete ActiveClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询分类
    public DataTable SelNewsClass(string nID)
    {
        string str = "select * from ActiveClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    public bool addNewsClass(string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@tClassName", className);
        SqlParameter paixus = new SqlParameter("@nPaiXu", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ActiveClass values (0,0,0,@tClassName,@nPaiXu,0)";
        return her.ExecuteNonQuery(sql, count);
    }

    public bool updateNewsClass(string nID, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@tClassName", className);
        SqlParameter paixus = new SqlParameter("@nPaiXu", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update ActiveClass set tClassName=@tClassName,nPaiXu=@nPaiXu where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
}