using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Maliang;

/// <summary>
///Bank 的摘要说明
/// </summary>
public class Bank
{
    private SqlHelper her = new SqlHelper();
    public Bank()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 某一个账户银行
    /// </summary>
    /// <returns></returns>
    public DataTable GetBankByAgent(string anid)
    {
        DataTable dt = new DataTable();
        string sql = "select * from Bank where Cid=" + anid;
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }

    /// <summary>
    /// 获取数据库中所有的值
    /// </summary>
    /// <returns></returns>
    public DataTable GetBank(string cid, string where)
    {
        string sql = "select *  from Bank where Cid=" + cid + " " + where;
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 获取数据库中所有的值
    /// </summary>
    /// <returns></returns>
    public DataTable GetBankOne(string nID)
    {
        string sql = "select *  from Bank where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }

    //添加活动
    public bool BankInsert(string Cid, string TypeName, string Zhanghao, string Huming, string Kaihu)
    {
        SqlParameter cid = new SqlParameter("@Cid", Cid);
        SqlParameter name = new SqlParameter("@TypeName", TypeName);
        SqlParameter zhanghao = new SqlParameter("@Zhanghao", Zhanghao);
        SqlParameter huming = new SqlParameter("@Huming", Huming);
        SqlParameter kaihu = new SqlParameter("@Kaihu", Kaihu);
        SqlParameter addtime = new SqlParameter("@AddTime", System.DateTime.Now.ToString());
        SqlParameter[] count = { cid, name, zhanghao, huming, kaihu, addtime };
        string sql = @"insert into Bank values (@Cid,@TypeName,@Zhanghao,@Huming,@Kaihu,@AddTime)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }



    //修改活动(不含Pic)
    public bool BankUpdate(string ID, string TypeName, string Zhanghao, string Huming, string Kaihu)
    {
        SqlParameter name = new SqlParameter("@TypeName", TypeName);
        SqlParameter zhanghao = new SqlParameter("@Zhanghao", Zhanghao);
        SqlParameter huming = new SqlParameter("@Huming", Huming);
        SqlParameter kaihu = new SqlParameter("@Kaihu", Kaihu);
        SqlParameter[] count = { name, zhanghao, huming, kaihu };
        string sql = @"update Bank set TypeName=@TypeName,Zhanghao=@Zhanghao,
        Huming=@Huming,Kaihu=@Kaihu where nID=" + ID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }


    //删除分类
    public bool DelBank(string nID)
    {
        string str = "delete Bank where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }


    /// <summary>
    /// 获取可提现，申请中 部分
    /// </summary>
    /// <param name="nid"></param>
    /// <param name="mstate"></param>
    /// <returns></returns>
    public string GetMoneyOrder(string nid, int mstate)
    {
        string sql = string.Format(@"  select b.proPrice,b.yongjin from ML_Order a 
  join ML_OrderProduct b on a.nID=b.orderID  
  where a.Auntid={0}  and DelState=0 and a.MoneyState={1} and  a.OrderState=10", nid, mstate);
        double dd = 0.00;
        foreach (DataRow dr in her.ExecuteDataTable(sql).Rows)
        {
            dd += Convert.ToInt32(dr["proPrice"]) * Convert.ToInt32(dr["yongjin"]);
        }
        string yj = (dd / 10000).ToString("0.00");
        return yj;
    }


    /// <summary>
    /// 获取待不可提现部分
    /// </summary>
    /// <param name="str"></param>
    /// <param name="nid"></param>
    /// <param name="mstate"></param>
    /// <returns></returns>
    public string GetMoneyOrderNoCan(string nid)
    {
        string sql = string.Format(@"  select b.proPrice,b.yongjin from ML_Order a 
  join ML_OrderProduct b on a.nID=b.orderID  
  where a.Auntid={0}  and DelState=0 and a.MoneyState=0 and (a.OrderState=2 or a.OrderState=3)", nid);
        double dd = 0.00;
        foreach (DataRow dr in her.ExecuteDataTable(sql).Rows)
        {
            dd += Convert.ToInt32(dr["proPrice"]) * Convert.ToInt32(dr["yongjin"]);
        }
        string yj = (dd / 10000).ToString("0.00");
        return yj;

    }


    /// <summary>
    /// 获取总利润
    /// </summary>
    /// <param name="nid"></param>
    /// <returns></returns>
    public string GetMoneyAll(string nid)
    {
        string sql = "select AllMoney from ML_Member where nID=" + nid;
        return her.ExecuteScalar(sql) == null ? "0.00" : StringDelHTML.PriceToStringLow(Convert.ToInt32(her.ExecuteScalar(sql)));
    }




    //添加提现申请
    public bool MoneyOrderInsert(string anid, string Money,string bkname,string bkcard,string bkhuming)
    {
        SqlParameter aid = new SqlParameter("@AnID", Convert.ToInt32(anid));
        SqlParameter oprice = new SqlParameter("@AllPrice", Money);
        SqlParameter ostate = new SqlParameter("@OrderState", Convert.ToInt32(0));
        SqlParameter addtime = new SqlParameter("@CreateTime", System.DateTime.Now.ToString());
        SqlParameter bname = new SqlParameter("@BankName", bkname);
        SqlParameter bcard = new SqlParameter("@BankCard", bkcard);
        SqlParameter bhuming = new SqlParameter("@Huming", bkhuming);
        SqlParameter[] count = { aid, oprice, ostate, addtime, bname, bcard, bhuming };
        string sql = @"insert into MoneyOrder values (@AnID,@AllPrice,@OrderState,@CreateTime,@BankName,@BankCard,@Huming)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
}