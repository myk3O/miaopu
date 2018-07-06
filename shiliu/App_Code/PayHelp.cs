using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Maliang;
using WeiPay;
using System.Data;

/// <summary>
/// Summary description for PayHelp
/// </summary>
public class PayHelp
{
    SqlHelper her = new SqlHelper();
    public PayHelp()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public bool InsertOrder(string uid, string fid, string ordercode, int price, string pid, string octype)
    {
        bool Result = false;
        if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(fid) && !string.IsNullOrEmpty(ordercode))
        {
            DateTime timeNow = System.DateTime.Now;
            SqlParameter[] count = 
                {  
                    new SqlParameter("@Auntid",Convert.ToInt32(fid)),//代理商id
                    new SqlParameter("@OrderUserid",Convert.ToInt32(uid)), 
                    new SqlParameter("@OcType",octype),
                    new SqlParameter("@OcID",Convert.ToInt32(pid)),
                    new SqlParameter("@OrderState",Convert.ToInt32(10)), 
                    new SqlParameter("@OrderCode", ordercode),                 
                    new SqlParameter("@CreateTime", timeNow.ToString()),
                    new SqlParameter("@Lianxiren",uid), 
                    new SqlParameter("@PhoneNumber", ""),
                    new SqlParameter("@Address",""), 
                    new SqlParameter("@tComment", ""),
                    new SqlParameter("@worktime",""), //快递名称
                    new SqlParameter("@workComment", ""),//快递单号
                    new SqlParameter("@OrderPrice", Convert.ToInt32(price)), 
                    new SqlParameter("@OPing", Convert.ToInt32(0)),//是否评论
                    new SqlParameter("@OspecialStr",""),                                       
                    new SqlParameter("@DelState", Convert.ToInt32(0)),//删除状态
                    new SqlParameter("@MoneyState",Convert.ToInt32(0)), //提现状态1未申请，2已申请，3已打款                    
                    new SqlParameter("@MoneyOrderID",Convert.ToInt32(0)),     
                    new SqlParameter("@orderTime", timeNow.ToString()),
                };
            string sql = @"insert into ML_Order values (@Auntid,@OrderUserid,@OcType,@OcID,@OrderState,@OrderCode,@CreateTime,@Lianxiren
,@PhoneNumber,@Address,@tComment,@worktime,@workComment,@OrderPrice,@OPing,@OspecialStr,@DelState,@MoneyState,@MoneyOrderID,@orderTime)";
            bool success = false;
            try
            {
                success = her.ExecuteNonQuery(sql, count);
                if (success)
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }

            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(ex.Message);
                Result = false;

            }

        }
        else
        {
            Result = false;

        }
        return Result;
    }



    /// <summary>
    /// 获取用户是否会员
    /// </summary>
    /// <param name="context"></param>
    public bool Memberisfxs(string uid)
    {
        bool isjxs = false; //不是会员
        if (!string.IsNullOrEmpty(uid))
        {
            string sql = "select isJXS  from ML_Member where nID=" + uid;
            string jsx = her.ExecuteScalar(sql) == null ? "False" : her.ExecuteScalar(sql).ToString();
            if (jsx == "True")
            {
                isjxs = true;
            }
        }
        return isjxs;
    }

    /// <summary>
    /// 判断是否已经插入，默认不存在
    /// </summary>
    /// <param name="ordercode"></param>
    /// <returns></returns>
    public bool IsOrder(string ordercode)
    {
        bool flag = false;
        if (!string.IsNullOrEmpty(ordercode))
        {
            string sql = "select *  from ML_Order where OrderCode='" + ordercode + "'";
            DataTable dt = her.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                flag = true;
            }
        }
        return flag;
    }


    public bool UpdateXuexb(string uID, int pri)
    {
        string sql = string.Format(@"update ML_Member set MemberState=MemberState+{0} where nID={1}", pri, uID);
        return her.ExecuteNonQuery(sql);
    }
}