using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// ServceHelper 的摘要说明
/// </summary>
public class ServceHelper
{

    public static string ImgName = string.Empty;
    public ServceHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    //根据ID删除相关信息
    public bool ServiceDelete(string ID)
    {
        string sql = "delete ML_ServiceArea where nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }

    //根据ID删除相关信息
    public bool ServicePaikeDelete(string ID)
    {
        string sql = "delete ML_Policies where nID=" + ID;
        //string strSql = "delete ML_PoliciesClass where cid0=" + ID;
        //if (her.ExecuteNonQuery(strSql))
        //{
        //}
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }
    //根据ID查询相关信息
    public DataTable getMang(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select a.*,b.tClassName ptclass,b.nID classID1,c.nID classID2,d.nID classID3,e.nID classID4 from dbo.ML_ServiceArea a 
left join ML_ServiceMainClass b on a.cid0=b.nID
left join ML_ServiceAreaClass c on a.cid1=c.nID
left join ML_ServiceAreaClass2 d  on a.cid2=d.nID
left join ML_ServiceAreaClass1 e  on a.cid3=e.nID where a.nID={0}", ID);
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //根据ID查询相关信息
    public string getKucunDL(string homeMakid, string proid)
    {
        string sql = string.Format(@"select kuCunCount from ML_KunCun where homeMakID={0}and proID={1}", homeMakid, proid);
        string kucun = her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
        return kucun;
    }

    //更新代理库存
    public bool UpdateKucunDL(string homeMakid, string proid, string Kucun)
    {
        SqlParameter kc = new SqlParameter("@kuCunCount", Kucun);

        SqlParameter[] count = { kc };
        string sql = string.Format(@"update ML_KunCun set kuCunCount=@kuCunCount where homeMakID={0} and proID={1}", homeMakid, proid);
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //新增代理库存
    public bool InsertKucunDL(string homeMakid, string proid, string Kucun)
    {
        SqlParameter mid = new SqlParameter("@homeMakID", homeMakid);
        SqlParameter pid = new SqlParameter("@proID", proid);
        SqlParameter kc = new SqlParameter("@kuCunCount", Kucun);
        SqlParameter dtime = new SqlParameter("@CreateTime", System.DateTime.Now);

        SqlParameter[] count = { mid, pid, kc, dtime };
        string sql = @"insert into ML_KunCun values (@homeMakID,@proID,@kuCunCount,@CreateTime)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //判断是否有该产品库存
    public bool isAsistKucunDL(string homeMakid, string proid)
    {
        bool result = false;
        string sql = string.Format(@"select COUNT(kuCunCount) from ML_KunCun where homeMakID={0}and proID={1}", homeMakid, proid);
        if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)
        {
            //有记录 
            result = true;
        }
        return result;
    }
    //查询相关信息
    public DataTable GetProduct()
    {
        DataTable dt = new DataTable();
        string sql = @"select a.*,b.nID classID1,c.nID classID2,d.nID classID3,e.nID classID4 from dbo.ML_ServiceArea a 
left join ML_ServiceMainClass b on a.cid0=b.nID
left join ML_ServiceAreaClass c on a.cid1=c.nID
left join ML_ServiceAreaClass2 d  on a.cid2=d.nID
left join ML_ServiceAreaClass1 e  on a.cid3=e.nID ";
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }

    //查询相关信息
    public DataTable GetProduct(string typeId)
    {
        DataTable dt = new DataTable();
        string sql = @"select a.*,b.nID classID1,c.nID classID2,d.nID classID3,e.nID classID4 from dbo.ML_ServiceArea a 
left join ML_ServiceMainClass b on a.cid0=b.nID
left join ML_ServiceAreaClass c on a.cid1=c.nID
left join ML_ServiceAreaClass2 d  on a.cid2=d.nID
left join ML_ServiceAreaClass1 e  on a.cid3=e.nID where b.nID=" + typeId;
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }

    //查询相关信息
    public DataTable GetProductSearch(string msg)
    {
        DataTable dt = new DataTable();
        string sql = @"select a.*,b.nID classID1,c.nID classID2,d.nID classID3,e.nID classID4 from dbo.ML_ServiceArea a 
left join ML_ServiceMainClass b on a.cid0=b.nID
left join ML_ServiceAreaClass c on a.cid1=c.nID
left join ML_ServiceAreaClass2 d  on a.cid2=d.nID
left join ML_ServiceAreaClass1 e  on a.cid3=e.nID where a.tTitle like '%" + msg + "%'";
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }


    //根据ID查询排课信息
    public DataTable getPaike(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select b.tTitle ,a.nID,a.tTitle as tName,a.tPic,a.tMemoPicList,a.tWriter,a.tFromWhere,a.nHit,
                       a.dtEditTime,a.dtAddTime,a.tMemo
                        from  ML_Policies  a
                        join ML_ServiceArea b on a.cid0=b.nID where a.nID={0}", ID);
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //添加产品
    public bool ServiceInsert(string dropGroup, string dg1, string dg2, string dg3, string tTlitle, string pic, string memo, string top, string time, string jg, string kc, string cd, string sj)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter cid1 = new SqlParameter("@cid1", dg1);//佣金
        SqlParameter cid2 = new SqlParameter("@cid2", dg2);//新品
        SqlParameter cid3 = new SqlParameter("@cid3", dg3);//特惠
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tpic = new SqlParameter("@pic", pic);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter oHide = new SqlParameter("@oHide", top);//精选
        SqlParameter price = new SqlParameter("@price", jg);
        SqlParameter kucun = new SqlParameter("@kucun", kc);
        SqlParameter chandi = new SqlParameter("@chandi", cd);
        SqlParameter updown = new SqlParameter("@updown", sj);//上下架

        SqlParameter[] count = { cid, cid1, cid2, cid3, tlitle, tpic, tmemo, pubtime, addtime, oHide, price, kucun, chandi, updown };
        string sql = @"insert into ML_ServiceArea values 
(@cid0,@cid1,@cid2,@cid3,@tlitle,@pic,@memo,'',@pubtime,@addtime,@addtime,@oHide,@price,@kucun,@chandi,@updown)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    //添加排课
    public bool ServicePaiKe(string ID, string type, string Strtime1, string Strtime2, string timeB, string timeE, string KeShi, string teacher, string JiaoShi, string tMemo)
    {
        SqlParameter cid = new SqlParameter("@cid0", ID);
        SqlParameter tlitle = new SqlParameter("@tTitle", type);
        SqlParameter strtime1 = new SqlParameter("@tPic", Strtime1);
        SqlParameter strtime2 = new SqlParameter("@tMemoPicList", Strtime2);
        SqlParameter keShi = new SqlParameter("@tWriter", KeShi);
        SqlParameter teachers = new SqlParameter("@tFromWhere", teacher);
        SqlParameter jiaoShi = new SqlParameter("@nHit", JiaoShi);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter pubtime = new SqlParameter("@dtPubTime", System.DateTime.Now);
        SqlParameter edittime = new SqlParameter("@dtEditTime", timeB);
        SqlParameter addtime = new SqlParameter("@dtAddTime", timeE);
        SqlParameter[] count = { cid, tlitle, strtime1, tMemos, strtime2, keShi, teachers, jiaoShi, pubtime, edittime, addtime };
        string sql = @"insert into ML_Policies values 
(@cid0,0,0,0,@tTitle,@tPic,@tMemo,@tMemoPicList,@tWriter,@tFromWhere,@nHit,'','',@dtPubTime,@dtEditTime,@dtAddTime,0)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    //添加新闻资讯
    public bool ServiceInsert(string dropGroup, string tTlitle, string memo, string MemoList, string fromwhere, string top, string time)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tMemoPicList = new SqlParameter("@tMemoPicList", MemoList);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter[] count = { cid, tlitle, tmemo, tMemoPicList, tfromwhere, pubtime, addtime };
        string sql = "insert into ML_ServiceArea values (@cid0,'','','',@tlitle,'',@memo,@tMemoPicList,'',@fromwhere,0,0," + top + ",@pubtime,'',@addtime,0)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }



    //修改排课
    public bool ServicePaiKeUp(string ID, string timeB, string timeE, string KeShi, string teacher, string JiaoShi, string tMemo)
    {
        //SqlParameter cid = new SqlParameter("@cid0", ID);
        //SqlParameter tlitle = new SqlParameter("@tTitle", type);
        SqlParameter keShi = new SqlParameter("@tWriter", KeShi);
        SqlParameter teachers = new SqlParameter("@tFromWhere", teacher);
        SqlParameter jiaoShi = new SqlParameter("@nHit", JiaoShi);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        //SqlParameter pubtime = new SqlParameter("@dtPubTime", System.DateTime.Now);
        SqlParameter edittime = new SqlParameter("@dtEditTime", timeB);
        SqlParameter addtime = new SqlParameter("@dtAddTime", timeE);
        SqlParameter[] count = { tMemos, keShi, teachers, jiaoShi, edittime, addtime };
        string sql = @"update  ML_Policies set 
                    tMemo=@tMemo,tWriter=@tWriter,tFromWhere=@tFromWhere,nHit=@nHit,
                    dtEditTime=@dtEditTime,dtAddTime=@dtAddTime where nID=" + ID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    //修改产品(不含Pic)
    public bool ServiceUpdate(string ID, string dropGroup, string dg1, string dg2, string dg3, string tTlitle, string memo, string top, string jg, string kc, string cd, string sj)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter cid1 = new SqlParameter("@cid1", dg1);
        SqlParameter cid2 = new SqlParameter("@cid2", dg2);
        SqlParameter cid3 = new SqlParameter("@cid3", dg3);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        //SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter tTop = new SqlParameter("@oHide", top);
        SqlParameter price = new SqlParameter("@price", jg);
        SqlParameter kucun = new SqlParameter("@kucun", kc);
        SqlParameter chandi = new SqlParameter("@chandi", cd);
        SqlParameter updown = new SqlParameter("@updown", sj);
        SqlParameter[] count = { cid, cid1, cid2, cid3, tlitle, tmemo, tTop, price, kucun, chandi, updown };
        string sql = @"update ML_ServiceArea set cid0=@cid0,cid1=@cid1,cid2=@cid2,cid3=@cid3,tTitle=@tlitle,
tMemo=@memo,oHide=@oHide ,price=@price,kucun=@kucun,chandi=@chandi,updown=@updown where nID=" + ID;

        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    //修改产品
    public bool ServiceUpdate(string ID, string dropGroup, string dg1, string dg2, string dg3, string tTlitle, string pic, string memo, string top, string jg, string kc, string cd, string sj)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter cid1 = new SqlParameter("@cid1", dg1);
        SqlParameter cid2 = new SqlParameter("@cid2", dg2);
        SqlParameter cid3 = new SqlParameter("@cid3", dg3);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tpic = new SqlParameter("@pic", pic);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        //SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter tTop = new SqlParameter("@oHide", top);
        SqlParameter price = new SqlParameter("@price", jg);
        SqlParameter kucun = new SqlParameter("@kucun", kc);
        SqlParameter chandi = new SqlParameter("@chandi", cd);
        SqlParameter updown = new SqlParameter("@updown", sj);
        SqlParameter[] count = { cid, cid1, cid2, cid3, tlitle, tpic, tmemo, tTop, price, kucun, chandi, updown };
        string sql = @"update ML_ServiceArea set cid0=@cid0,cid1=@cid1,cid2=@cid2,cid3=@cid3,
tTitle=@tlitle,tPic=@pic,tMemo=@memo,oHide=@oHide ,price=@price,kucun=@kucun,chandi=@chandi,updown=@updown where nID=" + ID;

        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    ////修改
    //public bool ServiceUpdate(string ID, string sid, string tMemo, string time)
    //{
    //    SqlParameter sid0 = new SqlParameter("@sid0", sid);
    //    SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
    //    SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
    //    SqlParameter addtime = new SqlParameter("@addtime", time);
    //    SqlParameter[] count = { sid0, tMemos, pubtime, addtime };
    //    string sql = "update ML_ServiceArea set cid0=@sid0,tMemo=@tMemo,dtPubTime=@pubtime,dtAddTime=@addtime where nID=" + ID;
    //    bool success = her.ExecuteNonQuery(sql, count);
    //    if (success)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    //添加课程分类2
    public bool addServiceClass2(string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ML_ServiceAreaClass2 values (0,0,0,@className,@paixun,0,'')";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除课程分类2
    public bool DelServiceClass2(string nID)
    {
        string str = "delete ML_ServiceAreaClass2 where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询课程分类2
    public DataTable SelServiceClass2(string nID)
    {
        string str = "select * from ML_ServiceAreaClass2 where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改课程分类2
    public bool updateServiceClass2(string nID, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update ML_ServiceAreaClass2 set tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    //添加课程子分类
    public bool addServiceClass(string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ML_ServiceAreaClass values (0,0,0,@className,@paixun,0,'')";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除课程分类
    public bool DelServiceClass(string nID)
    {
        string str = "delete ML_ServiceAreaClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询课程分类
    public DataTable SelServiceClass(string nID)
    {
        string str = "select * from ML_ServiceAreaClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改课程分类
    public bool updateServiceClass(string nID, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update ML_ServiceAreaClass set tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    //添加总分类
    public bool addMainClass(string className, string paixu)
    {
        SqlParameter names = new SqlParameter("@tClassName", className);
        SqlParameter paixus = new SqlParameter("@nPaiXu", paixu);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ML_VideoClass values (0,0,0,@tClassName,@nPaiXu,1)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除总分类
    public bool DelMainClass(string nID)
    {
        string str = "delete ML_VideoClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }


    //查询总分类
    public DataTable SelMainClass()
    {
        string str = "select * from  ML_VideoClass order by nPaiXu";
        return her.ExecuteDataTable(str);
    }
    //查询总分类
    public DataTable SelMainClass(string nID)
    {
        string str = "select * from ML_VideoClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改总分类
    public bool updateMainClass(string nID, string className, string paixu)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixu", paixu);
        SqlParameter[] count = { names, paixus };
        string str = "update ML_VideoClass set tClassName=@className,nPaiXu=@paixu where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    /// <summary>
    /// 添加分类图片
    /// </summary>
    /// <param name="cid"></param>
    /// <param name="fname"></param>
    /// <returns></returns>
    public bool InsertClassImg(string cid, string iname, int px, string url)
    {
        SqlParameter cid0 = new SqlParameter("@cid0", cid);
        SqlParameter fname = new SqlParameter("@fname", iname);
        SqlParameter nPaixu = new SqlParameter("@nPaixu", px);
        SqlParameter Jurl = new SqlParameter("@Jurl", url);
        SqlParameter[] count = { cid0, fname, nPaixu, Jurl };
        string sql = "insert into ML_ServiceMainImg values (@cid0,@fname,@nPaixu,@Jurl)";
        return her.ExecuteNonQuery(sql, count);
    }
    /// <summary>
    /// 修改分类图片
    /// </summary>
    /// <param name="cid"></param>
    /// <param name="fname"></param>
    /// <returns></returns>
    public bool UpdateClassImg(string cid, string iname, int px, string url)
    {
        SqlParameter fname = new SqlParameter("@fname", iname);
        SqlParameter Jurl = new SqlParameter("@Jurl", url);
        SqlParameter[] count = { fname, Jurl };
        string sql = "update  ML_ServiceMainImg set fname=@fname,Jurl=@Jurl where cid0=" + cid + " and nPaixu=" + px;
        return her.ExecuteNonQuery(sql, count);
    }
    //添加酒精含量
    public bool addServiceClass1(string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ML_ServiceAreaClass1 values (0,0,0,@className,@paixun,0,'')";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除酒精含量
    public bool DelServiceClass1(string nID)
    {
        string str = "delete ML_ServiceAreaClass1 where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询酒精含量
    public DataTable SelServiceClass1(string nID)
    {
        string str = "select * from ML_ServiceAreaClass1 where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改酒精含量
    public bool updateServiceClass1(string nID, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update ML_ServiceAreaClass1 set tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    /// <summary>
    /// 获取总部库存
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string GetKucun(string nID)
    {
        string sql = "select kucun from ML_ServiceArea where nID=" + nID;
        return her.ExecuteScalar(sql).ToString();
    }

    /// <summary>
    /// 获取售货量
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string GetSoldCount(string proid, string anid)
    {
        string soldCount = "0";
        string sql = string.Format(@"  
   select sum(b.probyCount) soldCount, d.nID from ML_Order a  join ML_OrderProduct b on a.nID=b.orderID
  join ML_Product c on c.nID =b.proID join ML_ServiceArea d on c.cid0=d.nID
  where d.nID={0} and (a.orderState=6 or a.orderState=10) 
  group by d.nID", proid);
        if (her.ExecuteScalar(sql) != null && her.ExecuteScalar(sql).ToString() != "")
        {
            soldCount = her.ExecuteScalar(sql).ToString();
        }
        return soldCount;
    }
    /// <summary>
    /// 获取代理库存
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string GetKucunDL(string nID, string agentID)
    {
        string count = string.Empty;
        string sql = string.Format(@"select kuCunCount from ML_KunCun where homeMakID={0} and  proID={1}", agentID, nID);
        //return (her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString()) == "" ? "0" : her.ExecuteScalar(sql).ToString();
        if (her.ExecuteScalar(sql) != null && her.ExecuteScalar(sql).ToString() != "")
        {
            count = her.ExecuteScalar(sql).ToString();
        }
        else
        {
            count = "0";
        }
        return count;
    }

    /// <summary>
    /// 获取代理库存
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public DataTable GetMainClassImage(string cid)
    {
        string count = string.Empty;
        string sql = @" select a.*, isnull( b.price,0) pri,b.nID cid from ML_ServiceMainImg a left join ML_ServiceArea b 
  on a.Jurl=b.nID where a.cid0=" + cid;

        return her.ExecuteDataTable(sql);
    }
    /// <summary>
    /// 获取产品图片
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public string GetServiceAreaImg(string nid)
    {
        string picStr = string.Empty;
        string sql = "select tPic from ML_ServiceArea where nID=" + nid;
        picStr = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        return picStr;
    }
}