using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for Video
/// </summary>
public class Video
{
    public Video()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    SqlHelper her = new SqlHelper();
    //根据ID删除相关信息
    public bool DeleteProductByID(string ID)
    {
        string sql = "delete from ML_VideoComment where nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            string sql3 = "delete from ML_Video where sid0=" + ID;
            her.ExecuteNonQuery(sql3);
            return true;


        }
        return false;
    }
    /// <summary>
    /// 获取视频价格
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public string GetVideoPrice(string ID)
    {
        string sql = string.Format(@"select Price from ML_VideoComment where nID={0}", ID);
        return her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();

    }
    //根据ID查询视频系列
    public DataTable GetProductByID(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select * from ML_VideoComment where nID={0}", ID);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //根据ID查询视频系列
    public DataTable GetPByID(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select * from ML_Video where nID={0}", ID);
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
    /// <param name="urlVideo"></param>
    /// <param name="pbtime"></param>
    /// <returns></returns>
    public bool InsertProduct(string dropGroup, string title, string tpic, string urlVideo, string pbtime, int top)
    {
        SqlParameter sid0 = new SqlParameter("@sid0", dropGroup);
        SqlParameter VideoCode = new SqlParameter("@VideoCode", Convert.ToInt32(0));
        SqlParameter VideoName = new SqlParameter("@VideoName", title);
        SqlParameter tPic = new SqlParameter("@tPic", tpic);
        SqlParameter tVideo = new SqlParameter("@tVideo", urlVideo);
        SqlParameter oTop = new SqlParameter("@oTop", top);
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", Convert.ToDateTime(pbtime));
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", System.DateTime.Now);

        SqlParameter[] count = { sid0, VideoCode, VideoName, tPic, tVideo, oTop, dtPubTime, dtAddTime };
        string sql = @"insert into ML_Video values (@sid0,@VideoCode,@VideoName,@tPic,@tVideo,'',0,@oTop,@dtPubTime,@dtAddTime,1,0)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    //修改产品
    public bool UpdateProduct(string ID, string dropGroup, string title, string tpic, string urlVideo, string pbtime, int top)
    {

        SqlParameter sid0 = new SqlParameter("@sid0", dropGroup);
        //SqlParameter VideoCode = new SqlParameter("@VideoCode", 0);
        SqlParameter VideoName = new SqlParameter("@VideoName", title);
        SqlParameter tPic = new SqlParameter("@tPic", tpic);
        SqlParameter tVideo = new SqlParameter("@tVideo", urlVideo);
        SqlParameter oTop = new SqlParameter("@oTop", top);
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", Convert.ToDateTime(pbtime));
        //SqlParameter dtAddTime = new SqlParameter("@dtAddTime", System.DateTime.Now);

        SqlParameter[] count = { sid0, VideoName, tPic, tVideo, oTop, dtPubTime };

        string sql = @"update ML_Video set sid0=@sid0,VideoName=@VideoName,tPic=@tPic,tVideo=@tVideo,oTop=@oTop,dtPubTime=@dtPubTime where nID=" + ID;

        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 添加产品系列
    /// </summary>
    /// <param name="title"></param>
    /// <param name="urlPic"></param>
    /// <param name="urlVideo"></param>
    /// <param name="tMemo"></param>
    /// <param name="pbtime"></param>
    /// <param name="oHide"></param>
    /// <param name="pri"></param>
    /// <returns></returns>
    public bool InsertProduct(string title, string urlPic, string urlVideo, string tMemo, string pbtime, string oHide, string pri
        , string vd, string tid, string oFree)
    {
        SqlParameter sid = new SqlParameter("@sid0", Convert.ToInt32(0));
        SqlParameter VideoName = new SqlParameter("@vName", title);
        SqlParameter tPic = new SqlParameter("@vPic", urlPic);
        SqlParameter tVideo = new SqlParameter("@vUrl", "");
        SqlParameter tMemos = new SqlParameter("@vMemo", tMemo);
        SqlParameter dtAddTime = new SqlParameter("@addTime", Convert.ToDateTime(pbtime));
        SqlParameter oHides = new SqlParameter("@oHide", int.Parse(oHide));
        SqlParameter price = new SqlParameter("@Price", StringDelHTML.PriceToIntUp(pri));
        SqlParameter vdiscrib = new SqlParameter("@vdiscrib", vd);
        SqlParameter teacherID = new SqlParameter("@teacherID", tid);
        SqlParameter ofree = new SqlParameter("@oFree", int.Parse(oFree));
        SqlParameter[] count = { sid, VideoName, tPic, tVideo, tMemos, dtAddTime, oHides, price, 
                                   vdiscrib, teacherID ,ofree};
        string sql = @"insert into ML_VideoComment values (@sid0,@vMemo,@Price,@vName,@vPic,@vUrl,@oHide,@addTime
                    ,@vdiscrib,@teacherID,@oFree)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }


    //修改产品系列
    public bool UpdateProduct(string ID, string title, string urlPic, string urlVideo, string tMemo, string pbtime, string oHide, string pri
           , string vd, string tid, string oFree)
    {
        SqlParameter sid = new SqlParameter("@sid0", Convert.ToInt32(0));
        SqlParameter VideoName = new SqlParameter("@vName", title);
        SqlParameter tPic = new SqlParameter("@vPic", urlPic);
        SqlParameter tVideo = new SqlParameter("@vUrl", urlVideo);
        SqlParameter tMemos = new SqlParameter("@vMemo", tMemo);
        SqlParameter dtAddTime = new SqlParameter("@addTime", Convert.ToDateTime(pbtime));
        SqlParameter oHides = new SqlParameter("@oHide", int.Parse(oHide));
        SqlParameter price = new SqlParameter("@Price", StringDelHTML.PriceToIntUp(pri));
        SqlParameter vdiscrib = new SqlParameter("@vdiscrib", vd);
        SqlParameter teacherID = new SqlParameter("@teacherID", tid);
        SqlParameter ofree = new SqlParameter("@oFree", int.Parse(oFree));
        SqlParameter[] count = { sid, VideoName, tPic, tVideo, tMemos, dtAddTime, oHides, price,
                                   vdiscrib, teacherID,ofree};

        string sql = @"update ML_VideoComment set sid0=@sid0,vMemo=@vMemo,Price=@Price,vName=@vName,
vPic=@vPic,vUrl=@vUrl,oHide=@oHide,addTime=@addTime,vdiscrib=@vdiscrib,teacherID=@teacherID,oFree=@oFree where nID=" + ID;

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