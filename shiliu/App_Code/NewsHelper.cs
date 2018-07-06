using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// NewsHelper 的摘要说明
/// </summary>
public class NewsHelper
{
    public NewsHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    //删除新闻资讯
    public bool NewsDelete(string ID)
    {
        string sql = "delete ML_News where nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 新闻条数
    /// </summary>
    /// <returns></returns>
    public string NewCount(int userid)
    {
        string count = "0";
        if (userid > 0)
        {
            string sql = @"select count(b.nID)  from ML_News a left join ML_NewsClassMain b on a.nID=b.sid0
                           where b.oHide=0 and  b.sid1=" + userid;//未阅读
            if (her.ExecuteScalar(sql) != null)
            {
                count = her.ExecuteScalar(sql).ToString();
            }
        }
        return count;
    }
    //根据ID查询相关信息
    public DataTable getMang(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select * from dbo.ML_News  where nID={0}", ID);
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //添加新闻资讯
    public bool NewsInsert(string dropGroup, string tTlitle, string pic, string memo, string fromwhere, string top, string time, string isMsg)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tpic = new SqlParameter("@pic", pic);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter[] count = { cid, tlitle, tpic, tmemo, tfromwhere, pubtime, addtime };
        string sql = "insert into ML_News values (@cid0,'','','',@tlitle,@pic,@memo,'','',@fromwhere,0,0," + top + ",@pubtime,'',@addtime," + isMsg + ")";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //添加新闻资讯
    public bool NewsInsert(string dropGroup, string tTlitle, string memo, string fromwhere, string top, string time, string isMsg)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time));
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter[] count = { cid, tlitle, tmemo, tfromwhere, pubtime, addtime };
        string sql = "insert into ML_News values (@cid0,'','','',@tlitle,'',@memo,'','',@fromwhere,0,0," + top + ",@pubtime,'',@addtime," + isMsg + ")";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改新闻资讯
    public bool NewsUpdate(string ID, string dropGroup, string tTlitle, string pic, string memo, string fromwhere, string top, string time)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tpic = new SqlParameter("@pic", pic);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter tTop = new SqlParameter("@tTop", top);
        SqlParameter pubtime = new SqlParameter("@pubtime", time);
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter[] count = { cid, tlitle, tpic, tmemo, tfromwhere, tTop, pubtime, addtime };

        string sql = @"update ML_News set cid0=@cid0,tTitle=@tlitle,tPic=@pic,tMemo=@memo,tFromWhere=@fromwhere,oTop=@tTop ,dtpubtime=@pubtime,dtaddtime=@addtime where nID=" + ID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改新闻资讯(不含Pic)
    public bool NewsUpdate(string ID, string dropGroup, string tTlitle, string memo, string fromwhere, string top, string time)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter tTop = new SqlParameter("@tTop", top);
        SqlParameter pubtime = new SqlParameter("@pubtime", time);
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter[] count = { cid, tlitle, tmemo, tfromwhere, tTop, pubtime, addtime };
        string sql = @"update ML_News set cid0=@cid0,tTitle=@tlitle,tMemo=@memo,tFromWhere=@fromwhere,oTop=@tTop,dtpubtime=@pubtime,dtaddtime=@addtime where nID=" + ID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    public string NewsDelPhoto(string ID)
    {
        string sql = "select tPic from  ML_News where nID=" + ID;
        string str = (string)her.ExecuteScalar(sql);
        return str;
    }
    //添加新闻分类
    public bool addNewsClass(string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ML_NewsClass values (0,0,0,@className,'',@paixun,0)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除新闻分类
    public bool DelNewsClass(string nID)
    {
        string str = "delete ML_NewsClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询新闻分类
    public DataTable SelNewsClass(string nID)
    {
        string str = "select * from ML_NewsClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改新闻分类
    public bool updateNewsClass(string nID, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update ML_NewsClass set tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="nID"></param>
    /// <returns></returns>
    public bool updateDo(string nID)
    {
        string sql = "update ML_NewsClassMain  set sid2=1 where nID=" + nID;


        string sqlto = @"select c.tTitle,c.tMemo,b.tRealName,b.MemberPhone
                        from ML_NewsClassMain a
                        inner join  ML_Member b
                        on a.sid1=b.nID
                        inner join ML_News c 
                        on a.sid0=c.nID where a.nID=" + nID;
        DataTable dt = her.ExecuteDataTable(sqlto);
        if (dt.Rows.Count > 0)
        {
            if (AjaxAlert.SendMsg(dt.Rows[0]["tTitle"].ToString(), dt.Rows[0]["tMemo"].ToString(), dt.Rows[0]["tRealName"].ToString(), dt.Rows[0]["MemberPhone"].ToString()))
            {
                return her.ExecuteNonQuery(sql);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 判断用户是否已经阅读最新消息,false未查看
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public bool IsNewsRead(string uid)
    {
        bool flag = false;//未查看
        string sql = string.Format(@"  select COUNT(*) from ML_NewsRead   where userId={0} and newsId=(
                select top 1 nID from [ML_News] where oTop=1 order by dtAddTime desc)", uid);
        if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)
        {
            flag = true;
        }

        return flag;
    }

}