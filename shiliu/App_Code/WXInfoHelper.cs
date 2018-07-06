using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// WXInfoHelper 的摘要说明
/// </summary>
public class WXInfoHelper
{
    public WXInfoHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    SqlHelper her = new SqlHelper();
    //根据ID查询相关信息
    public DataTable getMang(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select WX_Info.*,WX_InfoClass.nID as tClassID,WX_InfoClass.sid0 as ClassSid,WX_InfoClass.tClassName from dbo.WX_Info inner join  dbo.WX_InfoClass on WX_Info.cid0=WX_InfoClass.nID where WX_Info.nID={0}", ID);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 添加企业信息
    /// </summary>
    /// <param name="cID"></param>
    /// <param name="tMemo"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool InfoInsert(string cID, string title, string tMemo, string tPic, string time)
    {
        SqlParameter cIDs = new SqlParameter("@cid", cID);
        SqlParameter tTitle = new SqlParameter("@tTitle", title);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter pic = new SqlParameter("@tPic", tPic);
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", time);
        SqlParameter[] count = { cIDs, tTitle, tMemos, pic, dtPubTime, dtAddTime };
        string insert = "insert into WX_Info values(@cid,'','','',@tTitle,@tPic,@tMemo,'',@dtPubTime,'',@dtAddTime,0,'')";
        bool success = her.ExecuteNonQuery(insert, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改企业信息
    public bool InfoUpdate(string ID, string title, string cID, string tMemo, string time)
    {
        SqlParameter cIDs = new SqlParameter("@cid", cID);
        SqlParameter tTitle = new SqlParameter("@tTitle", title);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", time);
        SqlParameter[] count = { cIDs, tTitle, tMemos, dtPubTime, dtAddTime };
        string update = "update dbo.WX_Info set cid0=@cid,tTitle=@tTitle,tMemo=@tMemo,dtPubTime=@dtPubTime,dtAddTime=@dtAddTime where nID=" + ID;
        bool success = her.ExecuteNonQuery(update, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改企业信息
    public bool InfoUpdate(string ID, string title, string cID, string tPic, string tMemo, string time)
    {
        SqlParameter cIDs = new SqlParameter("@cid", cID);
        SqlParameter tTitle = new SqlParameter("@tTitle", title);
        SqlParameter tMemos = new SqlParameter("@tMemo", tMemo);
        SqlParameter pic = new SqlParameter("@tPic", tPic);
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", time);
        SqlParameter[] count = { cIDs, tTitle, tMemos, pic, dtPubTime, dtAddTime };
        string update = "update dbo.WX_Info set cid0=@cid,tTitle=@tTitle,tPic=@tPic,tMemo=@tMemo,dtPubTime=@dtPubTime,dtAddTime=@dtAddTime where nID=" + ID;
        bool success = her.ExecuteNonQuery(update, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //删除企业信息
    public bool InfoDelete(string ID)
    {
        string sql = "DELETE FROM [WX_Info] WHERE nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }
    //提取字符串中的汉字
    public string strSub(string sTemp)
    {
        string aa = "";
        for (int c = 0; c < sTemp.Length; c++)
        {
            if ((int)sTemp[c] > 127)
            {
                aa += sTemp[c];
            }
        }
        return aa;
    }

    //添加企业信息分类
    public bool addInfoClass(string className, string paixun, string pic, string content)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter tPic = new SqlParameter("@tPic", pic);
        SqlParameter Content = new SqlParameter("@Content", content);
        SqlParameter[] count = { names, paixus, tPic, Content };
        string sql = "insert into WX_InfoClass values (0,0,0,@className,@tPic,'',@paixun,0,@Content)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除企业信息分类
    public bool DelInfoClass(string nID)
    {
        string str = "delete WX_InfoClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询企业信息分类
    public DataTable SelInfoClass(string nID)
    {
        string str = "select * from WX_InfoClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改企业信息分类
    public bool updateInfoClass(string nID, string className, string paixun, string pic, string content)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter tPic = new SqlParameter("@tPic", pic);
        SqlParameter Content = new SqlParameter("@Content", content);
        SqlParameter[] count = { names, paixus, tPic, Content };
        string str = "update WX_InfoClass set tClassName=@className,tPic=@tPic,content=@content,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    //修改企业信息分类
    public bool updateInfoClass(string nID, string className, string paixun, string content)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter Content = new SqlParameter("@Content", content);
        SqlParameter[] count = { names, paixus, Content };
        string str = "update WX_InfoClass set tClassName=@className,content=@content,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
}