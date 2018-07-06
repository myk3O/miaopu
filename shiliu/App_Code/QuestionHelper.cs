using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
///QuestionHelper 的摘要说明
/// </summary>
public class QuestionHelper
{
    public QuestionHelper()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();


    public DataTable getQu()
    {
        DataTable dt = new DataTable();
        string sql = "select nID,tClassName,dtAddTime,sid2 from ML_GongdiClass  order by dtAddTime desc";
        dt = her.ExecuteDataTable(sql);
        return dt;
    }


    public DataTable SelQu(string ID)
    {
        DataTable dt = new DataTable();
        string sql = "select nID, tClassName,dtAddTime from ML_GongdiClass where nID=" + ID + " order by dtAddTime desc ";
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 添加问卷名称信息
    /// </summary>
    /// <param name="cID"></param>
    /// <param name="tMemo"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool QusetionInsert(string title, string time)
    {
        SqlParameter tClassName = new SqlParameter("@tClassName", title);
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", time);
        SqlParameter[] count = { tClassName, dtAddTime };
        string insert = "insert into ML_GongdiClass values(0,0,0,@tClassName,0,@dtAddTime)";
        bool success = her.ExecuteNonQuery(insert, count);
        if (success)
        {
            return true;
        }
        return false;
    }


    //修改信息
    public bool QusetionUpdate(string title, string ID)
    {
        SqlParameter tClassName = new SqlParameter("@tClassName", title);
        SqlParameter[] count = { tClassName };
        string update = "update dbo.ML_GongdiClass set tClassName=@tClassName where nID=" + ID;
        bool success = her.ExecuteNonQuery(update, count);
        if (success)
        {
            return true;
        }
        return false;
    }



    //删除信息
    public bool QusetionDelete(string ID)
    {
        string sql = "DELETE FROM [ML_GongdiClass] WHERE nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }



    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 添加问卷内容
    /// </summary>
    /// <param name="cID"></param>
    /// <param name="tMemo"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool QusetionInfoInsert(string title, string q1, string q2, string q3, string q4, int state, int npaixu, string time, string id)
    {
        SqlParameter Q1 = new SqlParameter("@Q1", q1);
        SqlParameter Q2 = new SqlParameter("@Q2", q2);
        SqlParameter Q3 = new SqlParameter("@Q3", q3);
        SqlParameter Q4 = new SqlParameter("@Q4", q4);
        SqlParameter tTitlle = new SqlParameter("@tTitlle", title);
        SqlParameter oHide = new SqlParameter("@oHide", state);
        SqlParameter nPaixu = new SqlParameter("@nPaixu", npaixu);
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", time);
        SqlParameter sid0 = new SqlParameter("@sid0", id);
        SqlParameter[] count = { Q1, Q2, Q3, Q4, tTitlle, oHide, nPaixu, dtAddTime, sid0 };

        string insert = "insert into ML_Gongdi values(@Q1,@Q2,@Q3,@Q4,@dtAddTime,@oHide,@sid0,0,@nPaixu,@tTitlle)";
        bool success = her.ExecuteNonQuery(insert, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    public bool IsXuanZe(string nID)
    {
        string sql = string.Format(@"select count(nID) from ML_Gongdi where sid0={0} and oHide=0", nID);
        bool success = true;//超10个
        if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) >= 10)
        {
            success = true;
        }
        else
        {
            success = false;
        }
        return success;
    }

    public bool IsWenDa(string nID)
    {
        string sql = string.Format(@"select count(nID) from ML_Gongdi where sid0={0} and oHide=1", nID);
        bool success = true;//超10个
        if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) >= 3)
        {
            success = true;
        }
        else
        {
            success = false;
        }
        return success;
    }

    //删除信息
    public bool QusetionInfoDelete(string ID)
    {
        string sql = "DELETE FROM [ML_Gongdi] WHERE nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 修改问卷内容
    /// </summary>
    /// <param name="cID"></param>
    /// <param name="tMemo"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool QusetionInfoUpdate(string title, string q1, string q2, string q3, string q4, int npaixu, string ID)
    {
        SqlParameter Q1 = new SqlParameter("@Q1", q1);
        SqlParameter Q2 = new SqlParameter("@Q2", q2);
        SqlParameter Q3 = new SqlParameter("@Q3", q3);
        SqlParameter Q4 = new SqlParameter("@Q4", q4);
        SqlParameter tTitlle = new SqlParameter("@tTitlle", title);
        SqlParameter nPaixu = new SqlParameter("@nPaixu", npaixu);
        SqlParameter[] count = { Q1, Q2, Q3, Q4, tTitlle, nPaixu };
        string insert = "update ML_Gongdi set Q1=@Q1,Q2=@Q2,Q3=@Q3,Q4=@Q4,nPaixu=@nPaixu,tTitlle=@tTitlle where nID=" + ID;
        bool success = her.ExecuteNonQuery(insert, count);
        if (success)
        {
            return true;
        }
        return false;
    }




    public DataTable SelQuInfoClaess(string ID)
    {
        DataTable dt = new DataTable();
        string sql = "select nID,Q1,Q2,Q3,Q4,nPaixu,oHide, tTitlle from ML_Gongdi where sid0=" + ID + " order by nPaixu ";
        dt = her.ExecuteDataTable(sql);
        return dt;
    }


    public DataTable SelQuInfo(string ID)
    {
        DataTable dt = new DataTable();
        string sql = "select nID,Q1,Q2,Q3,Q4,nPaixu,oHide, tTitlle from ML_Gongdi where nID=" + ID + " order by nPaixu ";
        dt = her.ExecuteDataTable(sql);
        return dt;
    }


    public DataTable SelQuWenDa(string ID)
    {
        DataTable dt = new DataTable();
        string sql = "select nID, tTitlle from ML_Gongdi where sid0=" + ID + " and oHide=1 order by nPaixu ";
        dt = her.ExecuteDataTable(sql);
        return dt;
    }


    public DataTable SelQuWenDaResult(string sid0, string sid1)
    {
        DataTable dt = new DataTable();
        string sql = "select nID, tMemo from ML_GongdiWenDa where sid0=" + sid0 + " and sid1=" + sid1 + " order by addTime desc";
        dt = her.ExecuteDataTable(sql);
        return dt;
    }


    public string SelQuestionName(string nID)
    {
        string sql = "select tTitlle from ML_Gongdi where nID=" + nID;
        string qname = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        return qname;
    }


    public bool CheckFabu()
    {
        bool _result = false;
        string sqlCheck = "select count(*) from ML_GongdiClass where sid2=1";
        if (her.ExecuteScalar(sqlCheck) != null && Convert.ToInt32(her.ExecuteScalar(sqlCheck)) > 0)//已经有发布的问卷
        {
            _result = true;
        }
        else
        {
            _result = false;
        }

        return _result;

    }


    public bool UpdateFabu(string nID)
    {
        bool _result = false;
        string sql = "update  ML_GongdiClass set sid2=1 where nID=" + nID;
        if (her.ExecuteNonQuery(sql))//成功
        {
            _result = true;
        }
        return _result;
    }



    public bool UpdateFabuCancel(string nID)
    {
        bool _result = false;
        string sql = "update  ML_GongdiClass set sid2=0 where nID=" + nID;
        if (her.ExecuteNonQuery(sql))//成功
        {
            _result = true;
        }
        return _result;
    }
}