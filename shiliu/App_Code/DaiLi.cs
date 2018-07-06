using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maliang;
using System.Data.SqlClient;
using System.Data;
using Model;

/// <summary>
///DaiLi 的摘要说明
/// </summary>
public class DaiLi
{
    SqlHelper her = new SqlHelper();
    public DaiLi()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    DataTable dt = new DataTable();

    public string MakSelectTongJi()
    {
        string sql = "select count(*) from ML_Member ";

        string ct = her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
        return ct;
    }
    public DataTable MemberSelectTongJi()
    {
        string sql = "select top 5  *  from  ML_Member  order by dtAddTime desc";

        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    public string ProductSelectTongJi()
    {
        string sql = "select count(*) from ML_Member where  isJXS=1";
        string ct = her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
        return ct;
    }
    /// <summary>
    /// 查询家政公司
    /// </summary>
    /// <returns></returns>
    public DataTable MakSelect(string nid, string keyName, string dropsheng, string dropshi, string state)
    {
        string where = " where 1=1";
        where += keyName == "" ? "" : string.Format(@" and (a.tRealName like '%{0}%' or a.HomeMobile like '%{1}%' or a.HomePhone like '%{2}%' or a.HomeIntegral like '%{3}%')", keyName, keyName, keyName, keyName);
        where += dropsheng == "-1" ? "" : string.Format(@" and a.cid='{0}'", dropsheng);
        where += dropshi == "-1" ? "" : string.Format(@" and a.nLogNum='{0}'", dropshi);
        where += state == "-1" ? "" : string.Format(@" and a.oCheck={0}", state);

        if (nid != "1")
        {
            where += " and a.nLogNum=" + nid;//不是总部，只能查看下级代理
        }

        where += " and a.nID!=" + nid; //不可以看到自己。
        ////
        string sql = "select a.*,b.tClassName from ML_HomeMaking a join ML_HomeMakingClass b on a.cid=b.nID ";
        sql += where;
        sql += " order by dtAddTime desc";
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 查询家政公司(根据ID)
    /// </summary>
    /// <returns></returns>
    public DataTable MakSelectID(string id)
    {
        string sql = string.Format(@"select * from ML_HomeMaking where nID={0}  order by dtAddTime desc", id);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 查询家政公司最大code
    /// </summary>
    /// <returns></returns>
    public DataTable MakSelHomeCode(string where)
    {
        string sql = "select top 1 HomeCode from ML_HomeMaking";
        sql += where;
        sql += " order by HomeCode desc";
        return her.ExecuteDataTable(sql);
    }
    /// <summary>
    /// 修改家政公司
    /// </summary>
    /// <returns></returns>
    public bool MakUpdate(HomeMakInfo makinfo)
    {
        SqlParameter[] count = 
            {  
                new SqlParameter("@nID", makinfo.nID),
                new SqlParameter("@HomeCode", makinfo.HomeCode), 
                new SqlParameter("@HomeName",makinfo.HomeName), 
                new SqlParameter("@HomePass", makinfo.HomePass), 
                new SqlParameter("@tRealName", makinfo.tRealName), 
                new SqlParameter("@HomeMobile", makinfo.HomeMobile), 
                new SqlParameter("@HomePhone", makinfo.HomePhone), 
                new SqlParameter("@HomeEmail", makinfo.HomeEmail), 
                new SqlParameter("@HomeCity", makinfo.HomeCity), 
                new SqlParameter("@HomePro", makinfo.HomePro), 
                new SqlParameter("@HomeIntro", makinfo.HomeIntro), 
                new SqlParameter("@oCheck", makinfo.oCheck),
                new SqlParameter("@tPic", makinfo.TPic), 
                new SqlParameter("@Lng", makinfo.Lng), 
                new SqlParameter("@Lat", makinfo.Lat), 
                new SqlParameter("@tMemo", makinfo.TMemo), 
                new SqlParameter("@HomeIntegral", makinfo.HomeIntegral), 
                new SqlParameter("@cid", makinfo.cid), 
                new SqlParameter("@nLogNum", makinfo.nLogNum),
                new SqlParameter("@CreatorCode", makinfo.CreatorCode), 
            };
        string sql = string.Format(@"update ML_HomeMaking set HomeCode=@HomeCode,HomeName=@HomeName,HomePass=@HomePass,tRealName=@tRealName,HomeMobile=@HomeMobile,HomePhone=@HomePhone,HomeEmail=@HomeEmail,HomeCity=@HomeCity,HomePro=@HomePro,HomeIntro=@HomeIntro,oCheck=@oCheck,tPic=@tPic,Lng=@Lng,Lat=@Lat,tMemo=@tMemo,HomeIntegral=@HomeIntegral,cid=@cid,nLogNum=@nLogNum,CreatorCode=@CreatorCode where nID=@nID");
        bool success = false;
        try { success = her.ExecuteNonQuery(sql, count); }
        catch { }
        return success;
    }

    /// <summary>
    /// 生成商城地址
    /// </summary>
    /// <returns></returns>
    public bool MakUrl(HomeMakInfo makinfo)
    {
        SqlParameter[] count = 
            {  
                new SqlParameter("@nID", makinfo.nID),
                new SqlParameter("@HomePic", makinfo.HomePic), 
                new SqlParameter("@CreatorName",makinfo.CreatorName)
            };
        string sql = string.Format(@"update ML_HomeMaking set HomePic=@HomePic,CreatorName=@CreatorName where nID=@nID");
        bool success = false;
        try { success = her.ExecuteNonQuery(sql, count); }
        catch { }
        return success;
    }

    /// <summary>
    /// 添加家政公司
    /// </summary>
    /// <returns></returns>
    public bool MakInsert(HomeMakInfo makinfo)
    {
        SqlParameter[] count = 
            {  
                new SqlParameter("@HomeCode", makinfo.HomeCode), 
                new SqlParameter("@HomeName",makinfo.HomeName), //账号
                new SqlParameter("@HomePass", makinfo.HomePass), //密码
                new SqlParameter("@cid", makinfo.cid), //代理级别
                new SqlParameter("@tRealName", makinfo.tRealName),
                new SqlParameter("@HomeMobile", makinfo.HomeMobile), 
                new SqlParameter("@HomeMobileYz", makinfo.HomeMobileYz), 
                new SqlParameter("@HomePhone", makinfo.HomePhone), 
                new SqlParameter("@HomeEmail", makinfo.HomeEmail), 
                new SqlParameter("@HomeCity", makinfo.HomeCity), 
                new SqlParameter("@HomePro", makinfo.HomePro), 
                new SqlParameter("@HomeIntro", makinfo.HomeIntro), 
                new SqlParameter("@HomeIntegral", makinfo.HomeIntegral),
                new SqlParameter("@HomePic", makinfo.HomePic),
                new SqlParameter("@CreatorName", makinfo.CreatorName),
                new SqlParameter("@CreatorCode", makinfo.CreatorCode),
                new SqlParameter("@nLogNum", makinfo.nLogNum), 
                new SqlParameter("@dtLastTime", makinfo.dtLastTime), 
                new SqlParameter("@dtAddTime", makinfo.dtAddTime), 
                new SqlParameter("@tLastIP", makinfo.tLastIP), 
                new SqlParameter("@oCheck", makinfo.oCheck), 
                new SqlParameter("@oHide", makinfo.oHide), 
                new SqlParameter("@tPic", makinfo.TPic), 
                new SqlParameter("@Lng", makinfo.Lng), 
                new SqlParameter("@Lat", makinfo.Lat), 
                new SqlParameter("@tMemo", makinfo.TMemo), 
             new SqlParameter("@MoneyAll", makinfo.MoneyAll), 
            };
        string sql = string.Format(@"insert into ML_HomeMaking values (@HomeCode, @HomeName, @HomePass, @cid, @tRealName, @HomeMobile, @HomeMobileYz, @HomePhone,@HomeEmail, @HomePic, @HomePro, @HomeCity, @HomeIntro, @HomeIntegral, @CreatorName, @CreatorCode, @nLogNum, @dtLastTime, @dtAddTime, @tLastIP, @oCheck, @oHide,@tPic,@Lng,@Lat,@tMemo,@MoneyAll)");
        bool success = false;
        try { success = her.ExecuteNonQuery(sql, count); }
        catch { }
        return success;
    }
    /// <summary>
    /// 禁用账号
    /// </summary>
    /// <returns></returns>
    public bool MakUpd(HomeMakInfo makinfo)
    {
        SqlParameter[] count = 
            {  
                new SqlParameter("@nID", makinfo.nID),
                new SqlParameter("@oCheck", makinfo.oCheck)
            };
        string sql = string.Format(@"update ML_HomeMaking set oCheck=@oCheck where nID=@nID");
        bool success = false;
        try { success = her.ExecuteNonQuery(sql, count); }
        catch { }
        return success;
    }

    /// <summary>
    /// 删除账号
    /// </summary>
    /// <returns></returns>
    public bool MakDel(HomeMakInfo makinfo)
    {
        SqlParameter[] count = 
            {  
                new SqlParameter("@nID", makinfo.nID)
            };
        string sql = string.Format(@"delete from  ML_HomeMaking where nID=@nID");
        bool success = false;
        try { success = her.ExecuteNonQuery(sql, count); }
        catch { }
        return success;
    }

    /// <summary>
    /// 登录名是否重复
    /// </summary>
    /// <returns></returns>
    public bool SelMakName(string where)
    {
        string sql = string.Format(@"select count(*) from ML_HomeMaking {0}", where);
        int num = 0;
        try { num = (int)her.ExecuteScalar(sql); }
        catch { }
        if (num > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 代理级别
    /// </summary>
    /// <returns></returns>
    public DataTable HomeMakSystemBind(string tableName, string where)
    {
        string sql = string.Format(@"select * from {0} {1} ", tableName, where);
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    /// <summary>
    /// 获取上级代理名称
    /// </summary>
    /// <param name="nid"></param>
    /// <returns></returns>
    public string HomeMakName(string nid)
    {
        string sql = "select tRealName from ML_HomeMaking where nID=" + nid;
        return her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
    }

    /// <summary>
    /// 所有代理
    /// </summary>
    /// <returns></returns>
    public DataTable HomeMakBind(string tableName, string where)
    {
        string sql = string.Format(@"select nID,tRealName from {0} {1} ", tableName, where);
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }

    /// <summary>
    /// 自己和下级代理
    /// </summary>
    /// <returns></returns>
    public DataTable HomeMakBindDL(string tableName, string nID)
    {
        string sql = string.Format(@"select nID,tRealName from {0} where nID={1} or nLogNum={1} ", tableName, nID);
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }

    /// <summary>
    /// c城市查询
    /// </summary>
    /// <returns></returns>
    public DataTable SityBind(string tableName, string where)
    {
        string sql = string.Format(@"select * from {0} {1} order by nPaiXu asc", tableName, where);
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }


    /// <summary>
    /// 将城市名称转化为城市编号
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public int CitynID(string str, string where)
    {
        int returnString = 0;
        string sql = string.Format(@"select nID from ML_CityClass where tClassName = '{0}' {1}", str, where);
        try { returnString = (int)her.ExecuteScalar(sql); }
        catch { }
        return returnString;
    }

    /// <summary>
    /// 查询家政公司最大code
    /// </summary>
    /// <returns></returns>
    public DataTable MakSelHomeCode(string dropsheng, string dropshi)
    {
        string where = " where 1=1";
        where += dropsheng == "请选择" ? "" : string.Format(@" and HomePro='{0}'", dropsheng);
        where += dropshi == "请选择" ? "" : string.Format(@" and HomeCity='{0}'", dropshi);

        string sql = "select top 1 HomeCode from ML_HomeMaking";
        sql += where;
        sql += " order by HomeCode desc";
        return her.ExecuteDataTable(sql);
    }
}