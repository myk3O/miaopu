using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Maliang;
using System.Configuration;
using System.Text;
using WeiPay;
/// <summary>
/// Summary description for tongji
/// </summary>
public class tongji
{
    //public static double leave1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave1"]);//一班佣金比例
    //public static double leave2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave2"]);//二班佣金比例
    //public static double leave3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave3"]);//三班佣金比例
    //public static double leave_1 = Convert.ToDouble(ConfigurationManager.AppSettings["leave_1"]);//班长，全球分红比例
    //public static double leave_2 = Convert.ToDouble(ConfigurationManager.AppSettings["leave_2"]);//班主任，全球分红比例
    //public static double leave_3 = Convert.ToDouble(ConfigurationManager.AppSettings["leave_3"]);//校长，全球分红比例
    //public static int level1Count = Convert.ToInt32(ConfigurationManager.AppSettings["leave1count"]);//一班店铺个数
    //public static int level2Count = Convert.ToInt32(ConfigurationManager.AppSettings["leave2count"]);//二班店铺个数
    //public static int level3Count = Convert.ToInt32(ConfigurationManager.AppSettings["leave3count"]);//三班店铺个数

    //public static double jixiao1 = Convert.ToDouble(ConfigurationManager.AppSettings["jixiao1"]);//班长绩效比例
    //public static double jixiao2 = Convert.ToDouble(ConfigurationManager.AppSettings["jixiao2"]);//校长绩效比例
    Config cf = new Config();
    private SqlHelper her = new SqlHelper();
    public tongji()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// 全球分红，昨天 0点到昨天24点订单收益
    /// </summary>
    public int AllMoneyToday()
    {
        //DateTime dt = DateTime.Now;
        //DateTime dtt = dt.AddDays(-1);
        //string timelast = dt.Year + "-" + dt.Month + "-" + dtt.Day;
        //string timenow = dt.Year + "-" + dt.Month + "-" + dt.Day;
        //DateTime lastday = Convert.ToDateTime(timelast).AddHours(6);//昨天10点
        //DateTime today = Convert.ToDateTime(timenow).AddHours(6);//今天10点
        //        string sql = string.Format(@"select ISNULL(SUM(OrderPrice),0) from ML_Order
        //where orderTime>'{0}' and orderTime<='{1}' ", lastday, today);
        string sql = "select ISNULL(SUM(OrderPrice),0) from ML_Order where datediff(day,CreateTime,getdate()-1)=0 ";

        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
    }
    /// <summary>
    /// 整个平台等级在  《学员》以上用户(班长，班主任，校长)
    /// </summary>
    /// <returns></returns>
    public int AllUser()
    {
        string sql = "select count(nID) from  ML_Member where isJXS=1 and fxslevel>2";
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
    }


    /// 
    /// 实现数据的四舍五入法
    /// 
    /// 要进行处理的数据
    /// 保留的小数位数
    /// 四舍五入后的结果
    public double Round(double v, int x)
    {
        bool isNegative = false;
        //如果是负数
        if (v < 0)
        {
            isNegative = true;
            v = -v;
        }

        int IValue = 1;
        for (int i = 1; i <= x; i++)
        {
            IValue = IValue * 10;
        }
        double Int = Math.Round(v * IValue + 0.5, 0);
        v = Int / IValue;

        if (isNegative)
        {
            v = -v;
        }

        return v;
    }

    public DataTable GetUserByLevel(int level)
    {
        DataTable dt = new DataTable();
        string sql = string.Empty;
        switch (level)
        {
            case 3: sql = @"select a.*,b.levelName from  ML_Member a left join ML_MemberLevel b on a.fxslevel=b.nID  
                    where datediff(day,a.fxsTimeBegin,getdate()-1)>=0 and  a.fxslevel=3 or 
                    datediff(day,a.fxsTimeEnd,getdate())=0 and a.fxslevel=4 or
                    datediff(day,a.level3Time,getdate())=0 and a.fxslevel=5
                     order by a.fxsTimeBegin desc"; break;
            case 4: sql = @"select a.*,b.levelName from  ML_Member a left join ML_MemberLevel b on a.fxslevel=b.nID  
                    where datediff(day,a.fxsTimeEnd,getdate()-1)>=0 and  a.fxslevel=4 or 
                    datediff(day,a.level3Time,getdate())=0 and a.fxslevel=5
                     order by a.fxsTimeBegin desc"; break;
            case 5: sql = @"select a.*,b.levelName from  ML_Member a left join ML_MemberLevel b on a.fxslevel=b.nID
                    where datediff(day,a.level3Time,getdate()-1)>=0 and  a.fxslevel=5 order by a.level3Time asc"; break;
            case 6: sql = @"select * from ML_Member where nID=656"; break;
            default: break;
        }
        if (!string.IsNullOrEmpty(sql))
        {
            dt = her.ExecuteDataTable(sql);
        }
        return dt;
    }

    /// <summary>
    /// 插入上一日的全球分红
    /// </summary>
    /// <param name="dtuser"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public bool InsertFenHongLastDay(DataTable dtuser, int level, int rmoney, bool shoudong)
    {
        bool flag = false;
        int allmoney = AllMoneyToday();
        int ucount = dtuser.Rows.Count;
        int avg = 0;
        double levellv = 0.0;
        switch (level)
        {
            case 3: levellv = cf.leave_1; break;
            case 4: levellv = cf.leave_2; break;
            case 5: levellv = cf.leave_3; break;
        }
        if (ucount > 0)//有人分，才插入
        {
            if (shoudong)//是否手动分红
            {
                avg = rmoney;
            }
            else
            {
                avg = Convert.ToInt32(Round(Convert.ToDouble(allmoney * levellv / ucount), 0));//四舍五入取2位小数
            }
            //判断今日是否已经进行过分红操作,未分红，插入
            if (!isFengHongToday(level))
            {
                InsertAllMoneyToday(level, ucount, allmoney, avg);
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into  T_AvgMoneyLastDay (userID,userlevel,AvgMoney,CreateTime) values ");
                DateTime dt = System.DateTime.Now;
                string dtDate = dt.ToLongDateString().ToString();
                TemplateMsg tm = new TemplateMsg();
                for (int i = 0; i < ucount; i++)
                {
                    int uid = Convert.ToInt32(dtuser.Rows[i]["nID"]);
                    string opendid = dtuser.Rows[i]["openid"].ToString();
                    string url = "http://tv.gongxue168.com/wap/Global.aspx?uid=" + uid;
                    if (i == ucount - 1)//最后一个
                    {
                        sql.AppendLine("(" + uid + "," + level + "," + avg + ",'" + dt.ToString() + "')");
                    }
                    else
                    {
                        sql.AppendLine("(" + uid + "," + level + "," + avg + ",'" + dt.ToString() + "'),");
                    }

                    //微信模板，每日分红
                    tm.CommissionRemind(opendid, url, StringDelHTML.PriceToStringLow(avg), dtDate);

                }
                flag = her.ExecuteNonQuery(sql.ToString());
            }//昨日已分红
        }//没人分
        return flag;
    }


    /// <summary>
    /// 判断今日是否已经插入过全球分红
    /// </summary>
    /// <returns>true 有数据今日不插入了</returns>
    public bool isFengHongToday(int level)
    {
        bool flag = false;
        //控制一天只能插入一条数据
        string sql = "select count(*) from T_AvgMoneyDay where datediff(day,CreateTime,getdate())=0 and userlevel=" + level;
        if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 插入总体全球分红
    /// </summary>
    /// <returns> true 插入成功；false 未插入</returns>
    public bool InsertAllMoneyToday(int userlevel, int ucount, int allmoney, int avg)
    {
        bool flag = false;
        SqlParameter[] count = 
            {  
                new SqlParameter("@AvgMoney",avg),
                new SqlParameter("@MoneyDay",allmoney), 
                new SqlParameter("@UserCount",ucount), 
                new SqlParameter("@CreateTime",System.DateTime.Now.ToString()),
                new SqlParameter("@userlevel",userlevel), 
            };

        string sqlinsert = @"insert into T_AvgMoneyDay values (@AvgMoney,@MoneyDay,@UserCount,@CreateTime,@userlevel)";
        flag = her.ExecuteNonQuery(sqlinsert, count);

        return flag;

    }


    /// <summary>
    /// 获取总全球分红
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public int GetSumMoneyByUser(string uid)
    {
        int money = 0;
        string sql = "  select ISNULL(SUM(AvgMoney),0) money from T_AvgMoneyLastDay  where userID=" + uid;
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            money = her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));

        }
        return money;
    }
    /// <summary>
    /// 获取每日全球分红列表
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public DataTable GetAvgMoneyListByUser(string uid)
    {
        string sql = "select * from T_AvgMoneyLastDay where userID=" + uid + " order by CreateTime desc";
        DataTable dt = her.ExecuteDataTable(sql);

        return dt;
    }

    /// <summary>
    /// 满足班长条件
    /// </summary>
    /// <returns></returns>
    public bool UpdateLevel1()
    {
        bool flag = false;
        string sql = string.Format(@"update ML_Member set fxslevel=3,fxsTimeBegin='{0}' from 
                         (
                            select FatherFXSID, COUNT(FatherFXSID) ct from ML_Member 
                            where FatherFXSID!=0 and isJXS=1
                            group by FatherFXSID 
                          ) a inner join ML_Member on a.FatherFXSID=ML_Member.nID
                          where ML_Member.fxslevel=2 and a.ct>={1} ", System.DateTime.Now.ToString(), cf.level1Count);
        flag = her.ExecuteNonQuery(sql);
        return flag;

    }
    /// <summary>
    /// 满足班主任条件
    /// </summary>
    /// <returns></returns>
    public bool UpdateLevel2()
    {
        bool flag = false;
        string sql = string.Format(@"update ML_Member set fxslevel=4,fxsTimeEnd='{0}' from 
                         (
                              select b.FatherFXSID ,COUNT(b.FatherFXSID) ct from ML_Member a  join 
                              (select nID,FatherFXSID  from ML_Member) b
                              on a.FatherFXSID=b.nID 
                              where  b.FatherFXSID!=0 and a.isJXS=1
                              group by b.FatherFXSID 
                          ) tt inner join ML_Member on tt.FatherFXSID=ML_Member.nID
                          where ML_Member.fxslevel=3 and tt.ct>={1} ", System.DateTime.Now.ToString(), cf.level2Count);
        flag = her.ExecuteNonQuery(sql);
        return flag;

    }

    /// <summary>
    /// 满足校长条件
    /// </summary>
    /// <returns></returns>
    public bool UpdateLevel3()
    {
        bool flag = false;
        string sql = string.Format(@"update ML_Member set fxslevel=5,level3Time='{0}' from 
                         (
                            select cc.FatherFXSID,COUNT(cc.FatherFXSID) ct from ML_Member ff join
                              (
                                  select a.nID,b.FatherFXSID from ML_Member a  join 
                                  (select nID,FatherFXSID  from ML_Member) b
                                  on a.FatherFXSID=b.nID 
                                  where  b.FatherFXSID!=0  
                              )cc on ff.FatherFXSID=cc.nID
                              where  cc.FatherFXSID!=0 and ff.isJXS=1
                              group by cc.FatherFXSID 
                          ) tt inner join ML_Member on tt.FatherFXSID=ML_Member.nID
                          where ML_Member.fxslevel=4 and  tt.ct>={1} ", System.DateTime.Now.ToString(), cf.level3Count);
        flag = her.ExecuteNonQuery(sql);
        return flag;

    }

    /// <summary>
    /// 上级 佣金
    /// </summary>
    /// <param name="fid"></param>
    /// <param name="price"></param>
    public void insetTop1User(int uid, int fid, int price)
    {
        DateTime dt = System.DateTime.Now;
        int pri = Convert.ToInt32(Round(price * cf.leave1, 0));
        string part = (cf.leave1 * 100).ToString();
        SqlParameter[] count = 
            {   
                new SqlParameter("@uid",uid),//
                new SqlParameter("@fatherid",fid),
                new SqlParameter("@allpri",price),
                new SqlParameter("@price",pri), 
                new SqlParameter("@part",part), 
                new SqlParameter("@mark",Convert.ToInt32(1)),                        
                new SqlParameter("@CreateTime",dt.ToString()),      
            };

        string sql = @"insert into T_TopUserMoney values (@uid,@fatherid,@allpri,@price,@part,@mark,@CreateTime)";
        her.ExecuteNonQuery(sql, count);
        YongJinMouban(uid, fid, pri, dt);
    }
    /// <summary>
    /// 上上级 佣金
    /// </summary>
    /// <param name="fid"></param>
    /// <param name="price"></param
    /// <param name="part"></param>
    public void insetTop2User(int uid, int fid, int price)
    {
        DateTime dt = System.DateTime.Now;
        int pri = Convert.ToInt32(Round(price * cf.leave2, 0));
        string part = (cf.leave2 * 100).ToString();
        if (fid != 0)
        {
            string sqlg = "select FatherFXSID from ML_Member where nID=" + fid;
            if (her.ExecuteScalar(sqlg) != null && Convert.ToInt32(her.ExecuteScalar(sqlg)) > 0)
            {
                fid = Convert.ToInt32(her.ExecuteScalar(sqlg));
            }
            else
            {
                fid = 0;
            }
        }
        SqlParameter[] count = 
            {  
                new SqlParameter("@uid",uid),//
                new SqlParameter("@fatherid",fid),
                new SqlParameter("@allpri",price),
                new SqlParameter("@price",pri), 
                new SqlParameter("@part",part), 
                new SqlParameter("@mark",Convert.ToInt32(2)),                        
                new SqlParameter("@CreateTime",dt.ToString()),      
            };

        string sql = @"insert into T_TopUserMoney values (@uid,@fatherid,@allpri,@price,@part,@mark,@CreateTime)";
        her.ExecuteNonQuery(sql, count);
        YongJinMouban(uid, fid, pri, dt);
    }

    /// <summary>
    /// 上上上级 佣金
    /// </summary>
    /// <param name="fid"></param>
    /// <param name="price"></param>
    public void insetTop3User(int uid, int fid, int price)
    {
        DateTime dt = System.DateTime.Now;
        int pri = Convert.ToInt32(Round(price * cf.leave3, 0));
        string part = (cf.leave3 * 100).ToString();
        if (fid != 0)
        {
            string sqlg = @"   select FatherFXSID from ML_Member where nID=(
                                select FatherFXSID from ML_Member where nID=" + fid + ")";
            if (her.ExecuteScalar(sqlg) != null && Convert.ToInt32(her.ExecuteScalar(sqlg)) > 0)
            {
                fid = Convert.ToInt32(her.ExecuteScalar(sqlg));
            }
            else
            {
                fid = 0;
            }
        }
        SqlParameter[] count = 
            {  
                new SqlParameter("@uid",uid),//
                new SqlParameter("@fatherid",fid),
                new SqlParameter("@allpri",price),
                new SqlParameter("@price",pri), 
                new SqlParameter("@part",part), 
                new SqlParameter("@mark",Convert.ToInt32(3)),                        
                new SqlParameter("@CreateTime",dt.ToString()),      
            };

        string sql = @"insert into T_TopUserMoney values (@uid,@fatherid,@allpri,@price,@part,@mark,@CreateTime)";
        her.ExecuteNonQuery(sql, count);
        YongJinMouban(uid, fid, pri, dt);
    }

    /// <summary>
    /// 推荐奖励提醒
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="fid"></param>
    /// <param name="money"></param>
    /// <param name="time"></param>
    public void YongJinMouban(int uid, int fid, int money, DateTime time)
    {
        TemplateMsg tm = new TemplateMsg();
        string url = "http://tv.gongxue168.com/wap/Sales.aspx?uid=" + fid;
        string sql1 = "select nickname from ML_Member where nID=" + uid;
        string nickname = her.ExecuteScalar(sql1) == null ? "" : her.ExecuteScalar(sql1).ToString();
        string sql2 = "select openid from ML_Member where nID=" + fid;
        string openid = her.ExecuteScalar(sql2) == null ? "" : her.ExecuteScalar(sql2).ToString();
        string longdate = time.ToLongDateString().ToString();
        string shorttime = time.ToShortTimeString().ToString();
        string my = StringDelHTML.PriceToStringLow(money);
        tm.NominateReward(openid, nickname, url, my, longdate + " " + shorttime);
    }


    /// <summary>
    /// 获取销售提成记录
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public DataTable GetFenXiangJL(string uid)
    {
        string sql = @"select a.nickname,b.* from ML_Member a join T_TopUserMoney b on a.nID=b.uid 
                        where b.fatherid=" + uid + " order by b.CreateTime desc";
        return her.ExecuteDataTable(sql);
    }
    /// <summary>
    /// 获取个人销售提成
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public int GetFenXiangYJ(string uid)
    {
        string sql = @"select ISNULL(SUM(price),0) from T_TopUserMoney where fatherid=" + uid;
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
    }

    /// <summary>
    /// 获取个人销售总计
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public int GetFenXiangYJZJ(string uid)
    {
        string sql = @"select ISNULL(SUM(allpri),0) from T_TopUserMoney where fatherid=" + uid;
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
    }
    /// <summary>
    /// 插入班长的绩效奖金
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public bool InsertJxBZ(int uid, int price)
    {
        bool flag = false;
        //int pri = Convert.ToInt32(Round(price * cf.jixiao1, 0));
        int pri = Convert.ToInt32(price * cf.jixiao3);
        string part = cf.jixiao3.ToString();
        int bid = GetJxXZ(uid, 3);
        if (bid > 0)//如果树上有班长
        {
            flag = InsertJiXiao(uid, bid, price, pri, part);
        }
        return flag;
    }
    /// <summary>
    /// 插入班主任的绩效奖金
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public bool InsertJxBZE(int uid, int price)
    {
        bool flag = false;
        //int pri = Convert.ToInt32(Round(price * cf.jixiao1, 0));
        int pri = Convert.ToInt32(price * cf.jixiao1);
        string part = cf.jixiao1.ToString();
        int bid = GetJxXZ(uid, 4);
        if (bid > 0)//如果树上有班主任
        {
            flag = InsertJiXiao(uid, bid, price, pri, part);
        }
        return flag;
    }
    /// <summary>
    /// 插入校长的绩效奖金
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public bool InsertJxXZ(int uid, int price)
    {
        bool flag = false;
        //int pri = Convert.ToInt32(Round(price * cf.jixiao2, 0));
        int pri = Convert.ToInt32(price * cf.jixiao2);
        string part = cf.jixiao2.ToString();
        int bid = GetJxXZ(uid, 5);
        if (bid > 0)//如果树上有校长
        {
            flag = InsertJiXiao(uid, bid, price, pri, part);
        }
        return flag;
    }
    /// <summary>
    /// 插入绩效
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="bid"></param>
    /// <param name="price"></param>
    /// <param name="pri"></param>
    /// <param name="part"></param>
    /// <returns></returns>
    private bool InsertJiXiao(int uid, int bid, int price, int pri, string part)
    {
        SqlParameter[] count = 
            {  
                new SqlParameter("@uid",uid),
                new SqlParameter("@bid",bid),
                new SqlParameter("@allpri",price),
                new SqlParameter("@price",pri), 
                new SqlParameter("@part",part), 
                new SqlParameter("@mark",Convert.ToInt32(0)),                        
                new SqlParameter("@CreateTime",System.DateTime.Now.ToString()),      
            };

        string sql = @"insert into T_UserJiXiao values (@uid,@bid,@allpri,@price,@part,@mark,@CreateTime)";

        return her.ExecuteNonQuery(sql, count);

    }
    /// <summary>
    /// 获取当前用户的第一个，lv=班长，班主任，校长
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    private int GetJxXZ(int uid, int lv)
    {
        //哎，本来写到存储过程里面是最好的
        string sql = "select fxslevel,nID from ML_Member where nID=(select FatherFXSID from ML_Member where nID=" + uid + ")";
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count <= 0)
        {
            return 0;
        }
        else
        {
            int level = Convert.ToInt32(dt.Rows[0]["fxslevel"]);
            if (level == lv)//3班长，4班主任，5校长
            {
                return Convert.ToInt32(dt.Rows[0]["nID"]);// 结束，返回当前等级id
            }
            else
            {
                return GetJxXZ(Convert.ToInt32(dt.Rows[0]["nID"]), lv);
            }
        }
    }

    /// <summary>
    /// 获取个人绩效部分
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public int GetJx(string uid)
    {
        string sql = @"select ISNULL(SUM(price),0) from T_UserJiXiao where bid=" + uid;
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));

    }
    /// <summary>
    /// 获取个人绩效记录
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public DataTable GetJxList(string uid)
    {
        string sql = @"select a.nickname,b.* from ML_Member a join T_UserJiXiao b on a.nID=b.uid 
                        where b.bid=" + uid + " order by b.CreateTime desc";
        return her.ExecuteDataTable(sql);

    }


    /// <summary>
    /// 获取个人订单记录
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public DataTable GetMoneyOrderList(string uid)
    {
        string sql = @"select * from MoneyOrder where AnID=" + uid + " order by  CreateTime desc";
        return her.ExecuteDataTable(sql);

    }



    /// <summary>
    /// 学分转学习币
    /// </summary>
    /// <param name="fuid"></param>
    /// <param name="tuid"></param>
    /// <param name="money"></param>
    /// <param name="tmemo"></param>
    /// <returns></returns>
    public bool InsertZhuanBi(int fuid, int tuid, int money, string tmemo)
    {
        bool flag = false;
        DateTime timeNow = System.DateTime.Now;
        string orderCode = fuid + timeNow.ToString("yyyyMMddhhmmss") + tuid;
        SqlParameter[] count = 
            {  
                new SqlParameter("@zrCode",orderCode),
                new SqlParameter("@fromUser",fuid),
                new SqlParameter("@toUser",tuid),
                new SqlParameter("@zrMoney",money), 
                new SqlParameter("@CreateTime",timeNow.ToString()), 
                new SqlParameter("@zrState",Convert.ToInt32(1)),                        
                new SqlParameter("@tMemo",tmemo),      
            };

        string sql = @"insert into ML_ZhuanBi values (@zrCode,@fromUser,@toUser,@zrMoney,@CreateTime,@zrState,@tMemo)";

        flag = her.ExecuteNonQuery(sql, count);

        return flag;
    }

    /// <summary>
    /// 查询转币记录
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="state">true 转出，false 转入</param>
    /// <returns></returns>
    //public DataTable GetZhuanBiList(string uid, bool state)
    //{

    //}


    /// <summary>
    /// 获取个人学分转学习币总额
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public int GetMoneyToXueBi(string uid)
    {
        string sql = @"select ISNULL(SUM(zrMoney),0) from ML_ZhuanBi where fromUser=" + uid;
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));

    }

    /// <summary>
    /// 获取个人转入的学习币
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public int GetXueXibi(string uid)
    {
        string sql = @"select ISNULL(SUM(zrMoney),0) from ML_ZhuanBi where toUser=" + uid;
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));

    }
    /// <summary>
    /// 获取首次关注赠送的学习币
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public double GetAttentionXXB(string uid)
    {
        string sql = @"select money  from T_Attention where userID=" + uid + " and AtTypeId=2 ";
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToDouble(her.ExecuteScalar(sql)) * 100;

    }


    /// <summary>
    /// 获取个人，邀请关注，的奖励
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public double GetAttentionXF(string uid)
    {
        string sql = @"select ISNULL(SUM(money),0) from T_Attention where FuserID=" + uid + " and AtTypeId=1 ";
        return her.ExecuteScalar(sql) == null ? 0 : Convert.ToDouble(her.ExecuteScalar(sql)) * 100;

    }


    /// <summary>
    /// 获取个人，邀请关注，的奖励记录
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public DataTable GetAttentionXFList(string uid)
    {
        string sql = @"select a.nickname,b.* from ML_Member a join T_Attention b on a.nID=b.userID 
                        where b.FuserID=" + uid + "  and b.AtTypeId=1 order by b.CreateTime desc";
        return her.ExecuteDataTable(sql);
    }
}