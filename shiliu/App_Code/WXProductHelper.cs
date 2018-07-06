using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// WXProductHelper 的摘要说明
/// </summary>
public class WXProductHelper
{
    public WXProductHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    DataTable dt = new DataTable();
    //根据ID查询相关信息
    public DataTable getMang(string ID)
    {
        string sql = string.Format(@"select * from  dbo.WX_Product where nID={0}", ID);
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }

    //查询
    public DataTable SelectPro(string where)
    {
        string sql = string.Format(@"select WX_Product.*, WX_ProductClass.nID as classID,WX_ProductClass.tClassName as className 
                                        from WX_Product inner join WX_ProductClass on WX_Product.cid0 = WX_ProductClass.nID");
        sql += where;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }

    //添加案例
    public bool ProInsert(string select1, string tTlitle, string content, string imglist, string paixu)
    {
        SqlParameter cid0 = new SqlParameter("@cid0", select1);
        SqlParameter tTitle = new SqlParameter("@tTitle", tTlitle);
        SqlParameter tPicList = new SqlParameter("@tPicList", imglist);
        SqlParameter tMemo = new SqlParameter("@tMemo", content);
        SqlParameter nPaixu = new SqlParameter("@nPaiXu", paixu);
        SqlParameter dtPubTime = new SqlParameter("@dtPubTime", DateTime.Now.ToString("yyyy-MM-dd"));
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", DateTime.Now);
        SqlParameter[] count = { cid0, tTitle, tPicList, tMemo, nPaixu, dtPubTime, dtAddTime };
        string sql = "insert into WX_Product values (@cid0,0,0,0,@tTitle,@tPicList,0,@tMemo,'',0,@nPaiXu,0,0,@dtPubTime,'',@dtAddTime)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改案例
    public bool ProUpdate(string nID, string select1, string tTlitle, string content, string imglist, string paixu)
    {
        SqlParameter cid0 = new SqlParameter("@cid0", select1);
        SqlParameter tTitle = new SqlParameter("@tTitle", tTlitle);
        SqlParameter tPicList = new SqlParameter("@tPicList", imglist);
        SqlParameter tMemo = new SqlParameter("@tMemo", content);
        SqlParameter nPaixu = new SqlParameter("@nPaiXu", paixu);
        SqlParameter[] count = { cid0, tTitle, tPicList, tMemo, nPaixu };
        string sql = "update WX_Product set cid0=@cid0,tTitle=@tTitle,tPicList=@tPicList,tMemo=@tMemo,nPaiXu=@nPaiXu where nID=" + nID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改案例
    public bool ProUpdate(string nID, string select1, string tTlitle, string content, string paixu)
    {
        SqlParameter cid0 = new SqlParameter("@cid0", select1);
        SqlParameter tTitle = new SqlParameter("@tTitle", tTlitle);
        SqlParameter tMemo = new SqlParameter("@tMemo", content);
        SqlParameter nPaixu = new SqlParameter("@nPaiXu", paixu);
        SqlParameter[] count = { cid0, tTitle, tMemo, nPaixu };
        string sql = "update WX_Product set cid0=@cid0,tTitle=@tTitle,tMemo=@tMemo,nPaiXu=@nPaiXu where nID=" + nID;
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //删除案例
    public bool ProDel(string nID)
    {
        string sql = "delete WX_Product where nID=" + nID;
        bool success = her.ExecuteNonQuery(sql);
        if (success)
        {
            return true;
        }
        return false;
    }



    //添加案列分类
    public bool addProductClass(string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string sql = "insert into WX_ProductClass values (0,0,0,@className,'',@paixun,0)";
        return her.ExecuteNonQuery(sql, count);
    }
    //添加案列分类
    public bool addProductClass(string sid, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter sid0 = new SqlParameter("@sid0", sid);
        SqlParameter[] count = { sid0, names, paixus };
        string sql = "insert into WX_ProductClass values (sid0=@sid0,0,0,@className,'',@paixun,0)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除案列分类
    public bool DelProductClass(string nID)
    {
        string str = "delete WX_ProductClass where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询案列分类
    public DataTable SelProductClass(string nID)
    {
        string str = "select * from WX_ProductClass where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改案列分类
    public bool UpdProductClass(string nID, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter[] count = { names, paixus };
        string str = "update WX_ProductClass set tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    //修改案列分类
    public bool UpdProductClass(string nID, string sid, string className, string paixun)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter sid0 = new SqlParameter("@sid0", sid);
        SqlParameter[] count = { sid0, names, paixus };
        string str = "update WX_ProductClass set sid0=@sid0,tClassName=@className,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
}