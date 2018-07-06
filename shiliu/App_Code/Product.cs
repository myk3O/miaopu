using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
///Product 的摘要说明
/// </summary>
public class Product
{
    public Product()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    //根据ID删除相关信息
    public bool DeleteProductByID(string ID)
    {
        string sql = "delete from ML_Video where nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }

    //根据ID查询相关信息
    public DataTable GetProductByID(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select a.*,b.tClassName from ML_Video a join ML_VideoClass b on a.sid0=b.nID where a.nID={0}", ID);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }



    //查询相关信息
    public DataTable GetProductSearch(string msg)
    {
        DataTable dt = new DataTable();
        string sql = @"select a.*,b.tClassName from ML_Video a join ML_VideoClass b on a.sid0=b.nID where a.nID={0} where 1=1 " + msg;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }



    /// <summary>
    /// 添加产品
    /// </summary>
    /// <param name="dropGroup"></param>
    /// <param name="title"></param>
    /// <param name="urlPic"></param>
    /// <param name="urlVideo"></param>
    /// <param name="tMemo"></param>
    /// <param name="oNewest"></param>
    /// <param name="oTop"></param>
    /// <param name="pbtime"></param>
    /// <param name="oHide"></param>
    /// <returns></returns>
    public bool InsertProduct(string dropGroup, string title, string urlPic, string urlVideo, string tMemo, string oNew, string oTop, string pbtime, string oHide, string pri)
    {
        SqlParameter sid0 = new SqlParameter("@sid0", dropGroup);
        SqlParameter VideoCode = new SqlParameter("@VideoCode", 0);
        SqlParameter VideoName = new SqlParameter("@VideoName", title);
        SqlParameter tPic = new SqlParameter("@tPic", urlPic);
        SqlParameter tVideo = new SqlParameter("@tVideo", urlVideo);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter oNewest = new SqlParameter("@oNewest", int.Parse(oNew));
        SqlParameter oTops = new SqlParameter("@oTop", int.Parse(oTop));
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", Convert.ToDateTime(pbtime));
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", System.DateTime.Now);
        SqlParameter oHides = new SqlParameter("@oHide", int.Parse(oHide));
        SqlParameter price = new SqlParameter("@Price", StringDelHTML.PriceToIntUp(pri));

        SqlParameter[] count = { sid0, VideoCode, VideoName, tPic, tVideo, tMemos, oNewest, oTops, dtPubTime, dtAddTime, oHides, price };
        string sql = @"insert into ML_Video values (@sid0,@VideoCode,@VideoName,@tPic,@tVideo,@tMemo,@oNewest,@oTop,@dtPubTime,@dtAddTime,@oHide,@Price)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }


    //修改产品
    public bool UpdateProduct(string ID, string dropGroup, string title, string urlPic, string urlVideo, string tMemo, string oNew, string oTop, string pbtime, string oHide,string  pri)
    {
        SqlParameter sid0 = new SqlParameter("@sid0", dropGroup);
        //SqlParameter VideoCode = new SqlParameter("@VideoCode", "0");
        SqlParameter VideoName = new SqlParameter("@VideoName", title);
        SqlParameter tPic = new SqlParameter("@tPic", urlPic);
        SqlParameter tVideo = new SqlParameter("@tVideo", urlVideo);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter oNewest = new SqlParameter("@oNewest", int.Parse(oNew));
        SqlParameter oTops = new SqlParameter("@oTop", int.Parse(oTop));
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", Convert.ToDateTime(pbtime));
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", System.DateTime.Now);
        SqlParameter oHides = new SqlParameter("@oHide", int.Parse(oHide));
        SqlParameter price = new SqlParameter("@Price", StringDelHTML.PriceToIntUp(pri));

        SqlParameter[] count = { sid0, VideoName, tPic, tVideo, tMemos, oNewest, oTops, dtPubTime, dtAddTime, oHides, price };

        string sql = @"update ML_Video set sid0=@sid0,VideoName=@VideoName,tPic=@tPic,tVideo=@tVideo,
tMemo=@tMemo,oNewest=@oNewest,oTop=@oTop,dtPubTime=@dtPubTime,dtAddTime=@dtAddTime,oHide=@oHide,Price=@Price where nID=" + ID;

        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    public DataTable GetSelect(string cid)
    {
        string sql = " select * from ML_Product  where cid0='" + cid + "' and updown=1";
        return her.ExecuteDataTable(sql);

    }
}