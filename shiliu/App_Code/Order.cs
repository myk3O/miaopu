using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Maliang;
using System.Data.SqlClient;

/// <summary>
///Order 的摘要说明
/// </summary>
public class Order
{
    SqlHelper hp = new SqlHelper();
    DataTable dt = new DataTable();
    public Order()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 订单统计
    /// </summary>
    /// <returns></returns>
    public string OrderTongji(int state)
    {
        string ct = "0";
        string sql = string.Format(@"
select count(*) from ML_Order where  OrderState={0} and DelState=0 order by CreateTime desc", state);

        try { ct = hp.ExecuteScalar(sql) == null ? "0" : hp.ExecuteScalar(sql).ToString(); }
        catch { }
        return ct;
    }
    public string videoCount()
    {
        string sql = "select count(*) from ML_VideoComment";
        return hp.ExecuteScalar(sql) == null ? "0" : hp.ExecuteScalar(sql).ToString();

    }
    public string momeycount()
    {
        string sql = "  select isnull( sum(OrderPrice),0) OrderPrice from [ML_Order]";
        return hp.ExecuteScalar(sql) == DBNull.Value ? "0" : StringDelHTML.PriceToStringLow(Convert.ToInt32(hp.ExecuteScalar(sql)));

    }

    public string OrderCount()
    {
        string sql = "  select count(*) from [ML_Order]";
        return hp.ExecuteScalar(sql) == null ? "0" : hp.ExecuteScalar(sql).ToString();

    }
    public string momeycountDate()
    {
        DateTime dt = DateTime.Now;
        string dateb = dt.ToString("yyyy-MM-dd");
        string datee = dt.AddDays(1).ToString("yyyy-MM-dd");
        string sql = string.Format(@"  select ISNULL(sum(OrderPrice),0) from [ML_Order] where CreateTime>'{0}' and CreateTime<='{1}' ", dateb, datee);
        return hp.ExecuteScalar(sql) == DBNull.Value ? "0" : StringDelHTML.PriceToStringLow(Convert.ToInt32(hp.ExecuteScalar(sql)));

    }

    /// <summary>
    /// 订单统计
    /// </summary>
    /// <returns></returns>
    public DataTable GetOrderTongji()
    {
        string sql = @"select top 5 * from ML_Order  order by CreateTime desc";

        try { dt = hp.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    /// <summary>
    /// 订单
    /// </summary>
    /// <returns></returns>
    public DataTable GetOrder(string where)
    {
        string sql = @"
select a.*,b.MemberPhone,b.nickname,c.tRealName,e.StateName,f.name dlName from ML_Order a 
left join ML_Member b on a.OrderUserid=b.nID
left join ML_HomeMaking c on a.Auntid=c.nID
left join ML_OrderState e on a.OrderState=e.nID 
left join DB_ApplyShop f on b.nID=f.userID " + where;
        try { dt = hp.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }


    /// <summary>
    /// 获取订单详细
    /// </summary>
    /// <returns></returns>
    public DataTable GetOrderOne(string nID)
    {
        string sql = @"select a.*,b.MemberPhone,c.tRealName,e.StateName from ML_Order a 
left join ML_Member b on a.OrderUserid=b.nID
left join ML_HomeMaking c on a.Auntid=c.nID
left join ML_OrderState e on a.OrderState=e.nID where a.nID=" + nID;

        DataTable dt = hp.ExecuteDataTable(sql);
        return dt;
    }

    //获取订单状态
    public DataTable GetOrderState(string where)
    {
        string str = "select * from " + where;
        return hp.ExecuteDataTable(str);
    }
    //订单支付，更新状态和支付方式
    public bool UpdateOrderPay(string nID, string type, int state)
    {
        string str = string.Format("update  ML_Order set OrderState={0},OcType='{1}' where nID={2}", state, type, nID);
        return hp.ExecuteNonQuery(str);
    }
    /// <summary>
    /// 开通会员
    /// </summary>
    /// <param name="openid"></param>
    /// <returns></returns>
    public bool UpdateUserState(string nID)
    {
        string str = string.Format("update ML_Member set isJXS=1 ,fxslevel=2 where nID='{0}'", nID);
        return hp.ExecuteNonQuery(str);
    }

    //更新状态
    public bool DelOrder(string nID, int state)
    {
        string str = string.Format("update  ML_Order set OrderState={0} where nID={1}", state, nID);
        return hp.ExecuteNonQuery(str);
    }
    //更新状态
    public bool updateOrderFH(string nID, int state)
    {
        string str = string.Format("update  ML_Order set OrderState={0},CreateTime='{1}' where nID={2}", state, System.DateTime.Now.ToString(), nID);
        return hp.ExecuteNonQuery(str);
    }
    /// <summary>
    /// 增加快递信息
    /// </summary>
    /// <param name="nID"></param>
    /// <param name="TypeName"></param>
    /// <param name="strCode"></param>
    /// <returns></returns>
    public bool OrderKuaiDi(string nID, string TypeName, string strCode)
    {
        SqlParameter worktime = new SqlParameter("@worktime", TypeName);
        SqlParameter workComment = new SqlParameter("@workComment", strCode);
        SqlParameter[] count = { worktime, workComment };
        string str = "update  ML_Order set worktime=@worktime,workComment=@workComment where niD=" + nID;
        bool success = hp.ExecuteNonQuery(str, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 获取产品
    /// </summary>
    /// <param name="nid"></param>
    /// <returns></returns>
    public DataTable GetOrderProduct(string nid)
    {
        string sql = "select a.*,b.tPic,b.tTitle,b.price from ML_OrderProduct a join ML_ServiceArea b on a.proID=b.nID where a.orderID=" + nid;
        DataTable dt = hp.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 删除订单
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public bool DelOrder(string nID)
    {
        string str = "update  ML_Order set DelState=1  where nID=" + nID;
        return hp.ExecuteNonQuery(str);
    }

    public string GetOrderDaiLiID(string orderid)
    {
        string sql = "select Auntid from ML_Order where nID=" + orderid;
        return hp.ExecuteScalar(sql) == null ? "" : hp.ExecuteScalar(sql).ToString();
    }

    /// <summary>
    /// 改变库存
    /// </summary>
    /// <param name="orderid">订单id</param>
    /// <param name="mark">true=减少</param>
    /// <returns></returns>
    public bool ChengKuCun(string orderid, bool mark)
    {
        string sql = "select proID,probyCount from ML_OrderProduct where orderID=" + orderid;
        DataTable dtProID = hp.ExecuteDataTable(sql);
        string sql2 = string.Empty;
        foreach (DataRow dr in dtProID.Rows)
        {
            if (mark)  //true=减少
            {
                sql2 = string.Format(@"update ML_Product set kucun=kucun-{0} 
where nID={1}", int.Parse(dr["probyCount"].ToString()), int.Parse(dr["proID"].ToString()));
            }
            else
            {
                sql2 = string.Format(@"update ML_Product set kucun=kucun+{0} 
where nID={1}", int.Parse(dr["probyCount"].ToString()), int.Parse(dr["proID"].ToString()));
            }
            hp.ExecuteNonQuery(sql2);
        }
        return true;
    }
}