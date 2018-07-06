using Maliang;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// 统计类
/// </summary>
public class StylistHelp
{
    SqlHelper her = new SqlHelper();
    public StylistHelp()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //添加提现申请
    public bool MoneyOrderInsert(string anid, int Money, string shuilv, int tixian, string koushui)
    {
        SqlParameter aid = new SqlParameter("@AnID", Convert.ToInt32(anid));
        SqlParameter oprice = new SqlParameter("@AllPrice", Money);
        SqlParameter ostate = new SqlParameter("@OrderState", Convert.ToInt32(0));
        SqlParameter addtime = new SqlParameter("@CreateTime", System.DateTime.Now.ToString());
        SqlParameter sl = new SqlParameter("@BankName", shuilv);
        SqlParameter tx = new SqlParameter("@Tixian", tixian);
        SqlParameter ks = new SqlParameter("@Huming", koushui);
        SqlParameter uptime = new SqlParameter("@UpdateTime", System.DateTime.Now.ToString());
        SqlParameter[] count = { aid, oprice, ostate, addtime, sl, tx, ks, uptime };
        string sql = @"insert into MoneyOrder values (@AnID,@AllPrice,@OrderState,@CreateTime,@BankName,@Tixian,@Huming,@UpdateTime)";
        bool success = her.ExecuteNonQuery(sql, count);
        return success;
    }
    /// <summary>
    /// 个人提现中金额
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public int getPayMoneyNO(string nID)
    {
        string sql = "select ISNULL(SUM(convert(int,Tixian)),0) from MoneyOrder where  AnID=" + nID + " and OrderState=0";

        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
    }
    /// <summary>
    /// 个人已提取金额
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public int getPayMoney(string nID)
    {
        string sql = "select AllMoney from ML_Member where nID=" + nID;

        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
    }
    /// <summary>
    /// 个人已使用学习币
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public int getUsedXueXb(string nID)
    {
        string sql = "select MemberState from ML_Member where nID=" + nID;

        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
    }

    /// <summary>
    /// 一级代理人数
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getOneLeaveUser(string nID)
    {
        string sql = "select count(*) from ML_Member where FatherFXSID=" + nID + " and fxslevel>1";

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 二级代理人数
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getTwoLeaveUser(string nID)
    {
        string sql = string.Format(@"  select COUNT(*) from ML_Member a join 
                          (select nID from ML_Member where FatherFXSID={0}) b
                          on a.FatherFXSID=b.nID where a.fxslevel>1", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 三级代理人数
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getThreeLeaveUser(string nID)
    {
        string sql = string.Format(@"    select COUNT(*) from  ML_Member d  join 
                              ( select a.nID from ML_Member a join 
                              (select nID from ML_Member where FatherFXSID={0}) b
                              on a.FatherFXSID=b.nID )c on d.FatherFXSID=c.nID where d.fxslevel>1", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }


    /// <summary>
    /// 一级代理佣金
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getOneLeavePrice(string nID)
    {
        string sql = string.Format(@"  select ISNULL(SUM(oo.OrderPrice),0) from ML_Order  oo  join 
  
        (select nID from ML_Member where FatherFXSID={0}) mm on oo.OrderUserid=mm.nID", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }

    /// <summary>
    /// 当月一级代理佣金
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getOneLeavePriceOneMonth(string nID)
    {
        string sql = string.Format(@"  select ISNULL(SUM(price),0) from T_TopUserMoney 
                                    where fatherid={0} and mark=1 and datediff(Month,CreateTime,getdate())=0 ", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }


    /// <summary>
    /// 每周一班佣金
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getOneLeavePriceOneWeek(string nID)
    {
        DateTime dt = DateTime.Now;
        int dd = Convert.ToInt32(dt.DayOfWeek.ToString("d"));
        if (dd == 0)
        {
            dd = 7;
        }
        DateTime startWeek = dt.AddDays(1 - dd);  //本周周一  
        DateTime endWeek = startWeek.AddDays(7);  //本周周日  
        //datediff(Week,oo.orderTime,getdate())=0
        string sql = string.Format(@"  select ISNULL(SUM(oo.OrderPrice),0) from ML_Order  oo  join 
      
            (select nID from ML_Member where FatherFXSID={0}) mm on oo.OrderUserid=mm.nID 
            where oo.orderTime>'{1}' and oo.orderTime<='{2}' ", nID, startWeek.ToString("yyyy-MM-dd"), endWeek.ToString("yyyy-MM-dd"));

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }


    /// <summary>
    /// 某周一班佣金
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getOneLeavePriceOneWeek(string nID,DateTime dt)
    {
        int dd = Convert.ToInt32(dt.DayOfWeek.ToString("d"));
        if (dd == 0)
        {
            dd = 7;
        }
        DateTime startWeek = dt.AddDays(1 - dd);  //本周周一  
        DateTime endWeek = startWeek.AddDays(7);  //本周周日  
        //datediff(Week,oo.orderTime,getdate())=0
        string sql = string.Format(@"  select ISNULL(SUM(oo.OrderPrice),0) from ML_Order  oo  join 
      
            (select nID from ML_Member where FatherFXSID={0}) mm on oo.OrderUserid=mm.nID 
            where oo.orderTime>'{1}' and oo.orderTime<='{2}' ", nID, startWeek.ToString("yyyy-MM-dd"), endWeek.ToString("yyyy-MM-dd"));

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }

    /// <summary>
    /// 二级代理佣金
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getTwoLeavePrice(string nID)
    {
        string sql = string.Format(@"  select ISNULL(SUM(oo.OrderPrice),0) from ML_Order  oo  join 
        ( 
						select a.nID from ML_Member a join 
                          (select nID from ML_Member where FatherFXSID={0}) b
                          on a.FatherFXSID=b.nID
        ) mm on oo.OrderUserid=mm.nID", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 三级代理佣金
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getThreeLeavePrice(string nID)
    {
        string sql = string.Format(@"    select ISNULL(SUM(oo.OrderPrice),0) from ML_Order  oo  join 
        ( 
						 select d.nID from  ML_Member d  join 
                              ( select a.nID from ML_Member a join 
                              (select nID from ML_Member where FatherFXSID={0}) b
                              on a.FatherFXSID=b.nID )c on d.FatherFXSID=c.nID
        ) mm on oo.OrderUserid=mm.nID", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }



    /// <summary>
    /// 一级代理订单量
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getOneLeaveOrderCount(string nID)
    {
        string sql = string.Format(@"  select count(*) from ML_Order  oo  join 
  
        (select nID from ML_Member where FatherFXSID={0}) mm on oo.OrderUserid=mm.nID", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 二级代理订单量
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getTwoLeaveOrderCount(string nID)
    {
        string sql = string.Format(@"  select count(*) from ML_Order  oo  join 
        ( 
						select a.nID from ML_Member a join 
                          (select nID from ML_Member where FatherFXSID={0}) b
                          on a.FatherFXSID=b.nID
        ) mm on oo.OrderUserid=mm.nID", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }
    /// <summary>
    /// 三级代理订单量
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string getThreeLeaveOrderCount(string nID)
    {
        string sql = string.Format(@"    select count(*) from ML_Order  oo  join 
        ( 
						 select d.nID from  ML_Member d  join 
                              ( select a.nID from ML_Member a join 
                              (select nID from ML_Member where FatherFXSID={0}) b
                              on a.FatherFXSID=b.nID )c on d.FatherFXSID=c.nID
        ) mm on oo.OrderUserid=mm.nID", nID);

        return her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
    }


}