using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// PoliciesHelper 的摘要说明
/// </summary>
public class PoliciesHelper
{
    public PoliciesHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    //删除新闻资讯
    public bool PoliciesDelete(string ID)
    {
        string sql = "delete ML_News where nID=" + ID;
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
        string sql = string.Format(@"select ML_Policies.*,ML_PoliciesClass.nID as tClassID,ML_PoliciesClass.tClassName from dbo.ML_Policies inner join ML_PoliciesClass on ML_Policies.cid0=ML_PoliciesClass.nID where ML_Policies.nID={0}", ID);
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //添加新闻资讯
    public bool PoliciesInsert(string dropGroup, string tTlitle, string pic, string memo, string MemoList, string fromwhere, string top, string time)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tpic = new SqlParameter("@pic", pic);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tMemoPicList = new SqlParameter("@tMemoPicList", MemoList);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter[] count = { cid, tlitle, tpic, tmemo, tMemoPicList, tfromwhere, pubtime, addtime };
        string sql = "insert into ML_Policies values (@cid0,'','','',@tlitle,@pic,@memo,@tMemoPicList,'',@fromwhere,0,0," + top + ",@pubtime,'',@addtime,0)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false; 
    }
    //添加新闻资讯
    public bool PoliciesInsert(string dropGroup, string tTlitle, string memo, string MemoList, string fromwhere, string top, string time)
    {
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tMemoPicList = new SqlParameter("@tMemoPicList", MemoList);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter pubtime = new SqlParameter("@pubtime", Convert.ToDateTime(time).ToString("yyyy-MM-dd"));
        SqlParameter addtime = new SqlParameter("@addtime", time);
        SqlParameter[] count = { cid, tlitle, tmemo, tMemoPicList, tfromwhere, pubtime, addtime };
        string sql = "insert into ML_Policies values (@cid0,'','','',@tlitle,'',@memo,@tMemoPicList,'',@fromwhere,0,0," + top + ",@pubtime,'',@addtime,0)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改新闻资讯
    public bool PoliciesUpdate(string ID, string dropGroup, string tTlitle, string pic, string memo, string MemoList, string fromwhere, string top)
    {
        SqlParameter tMemoPicList = new SqlParameter("@tMemoPicList", MemoList);
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tpic = new SqlParameter("@pic", pic);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter tTop = new SqlParameter("@tTop", top);
        SqlParameter[] count = { cid, tlitle, tpic, tmemo, tMemoPicList, tfromwhere, tTop };
        string sql = "";
        if (MemoList == "")
        {
            sql = "update ML_Policies set cid0=@cid0,tTitle=@tlitle,tPic=@pic,tMemo=@memo,tFromWhere=@fromwhere,oTop=@tTop where nID=" + ID;
        }
        else
        {
            sql = "update ML_Policies set cid0=@cid0,tTitle=@tlitle,tPic=@pic,tMemo=@memo,tMemoPicList=@tMemoPicList,tFromWhere=@fromwhere,oTop=@tTop where nID=" + ID;
        }
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改新闻资讯(不含Pic)
    public bool PoliciesUpdate(string ID, string dropGroup, string tTlitle, string memo, string MemoList, string fromwhere, string top)
    {
        SqlParameter tMemoPicList = new SqlParameter("@tMemoPicList", MemoList);
        SqlParameter cid = new SqlParameter("@cid0", dropGroup);
        SqlParameter tlitle = new SqlParameter("@tlitle", tTlitle);
        SqlParameter tmemo = new SqlParameter("@memo", memo);
        SqlParameter tfromwhere = new SqlParameter("@fromwhere", fromwhere);
        SqlParameter tTop = new SqlParameter("@tTop", top);
        SqlParameter[] count = { cid, tlitle, tmemo, tMemoPicList, tfromwhere, tTop };
        string sql = "";
        if (MemoList == "")
        {
            sql = "update ML_Policies set cid0=@cid0,tTitle=@tlitle,tMemo=@memo,tFromWhere=@fromwhere,oTop=@tTop where nID=" + ID;
        }
        else
        {
            sql = "update ML_Policies set cid0=@cid0,tTitle=@tlitle,tMemo=@memo,tMemoPicList=@tMemoPicList,tFromWhere=@fromwhere,oTop=@tTop where nID=" + ID;
        }
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    public string PoliciesDelPhoto(string ID)
    {
        string sql = "select tPic from  ML_Policies where nID=" + ID;
        string str = (string)her.ExecuteScalar(sql);
        return str;
    }
    //添加新闻分类
    public bool addPoliciesClass(string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into ML_PoliciesClass values (0,0,0,@className,'',@paixun,0)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除新闻分类
    public bool DelPoliciesClass(string nID)
    {
        string str = "delete ML_PoliciesClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询新闻分类
    public DataTable SelPoliciesClass(string nID)
    {
        string str = "select * from ML_PoliciesClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改新闻分类
    public bool updatePoliciesClass(string nID, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update ML_PoliciesClass set tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
}