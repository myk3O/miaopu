using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// AdminManagHelper 管理员功能支持类
/// </summary>
public class AdminManagHelper
{
    SqlHelper her = new SqlHelper();
    public AdminManagHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    //根据ID查询相关信息
    public DataTable getMang(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select * from dbo.ML_Admin 
                                      where nID={0}", ID);
        //string sql = "select * from dbo.ML_Admin where nID=" + ID;
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 添加管理员信息
    /// </summary>
    /// <param name="name">用户名</param>
    /// <param name="pass">密码</param>
    /// <param name="cid">管理员级别</param>
    /// <param name="Realname">真实姓名</param>
    /// <param name="addtime">添加时间</param>
    /// <param name="check">是否已通过审核</param>
    public bool AdminInsert(string name, string pass, string cid, string Realname, string addtime, int check)
    {
        SqlParameter names = new SqlParameter("@name", name);
        SqlParameter password = new SqlParameter("@pass", pass);
        SqlParameter Realnames = new SqlParameter("@Realname", Realname);
        SqlParameter addtimes = new SqlParameter("@addtime", addtime);
        SqlParameter[] count = { names, password, Realnames, addtimes };
        string insert = "insert into [ML_Admin](tAdminCode,[tAdminName] ,[tAdminPass] ,[cid] ,[tRealName] ,[nLogNum],[dtLastTime] ,[dtAddTime],[tLastIP],[oCheck],[oHide]) " +
        " values('',@name,@pass,'" + cid + "',@Realname,0,'',@addtime,''," + check + ",0)";
        bool success = her.ExecuteNonQuery(insert, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //修改管理员信息
    public bool AdminUpdate(string ID, string name, string pass, string cid, string Realname, string addtime, int check)
    {
        SqlParameter names = new SqlParameter("@name", name);
        SqlParameter password = new SqlParameter("@pass", pass);
        SqlParameter Realnames = new SqlParameter("@Realname", Realname);
        SqlParameter addtimes = new SqlParameter("@addtime", addtime);
        SqlParameter[] count = { names, password, Realnames, addtimes };
        string update = "update [ML_Admin] set [tAdminName]=@name ,[tAdminPass]=@pass ,[cid]='" + cid + "' ,[tRealName]=@Realname ,[oCheck]=" + check + " where nID=" + ID;
        bool success = her.ExecuteNonQuery(update, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //查询管理员密码
    public string AdminSelPass(string ID)
    {
        string sql = "select tAdminPass from dbo.ML_Admin WHERE nID=" + ID;
        return (string)her.ExecuteScalar(sql);
    }
    //删除管理员信息
    public bool AdminDelete(string ID)
    {
        string sql = "DELETE FROM [ML_Admin] WHERE nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }
    //验证用户名是否重复
    public bool Verification(string name)
    {
        SqlParameter username = new SqlParameter("@name", name);
        SqlParameter[] count = { username };
        string sql = "select count(*) from [ML_Admin] where [tAdminName]=@name";
        int num = (int)her.ExecuteScalar(sql, count);
        if (num > 0)
        {
            return true;
        }
        return false;
    }
    //查询网站基本信息
    public DataTable WebsiteInformation()
    {
        string sql = "select top 1 * from dbo.ML_SysConfig order by nID asc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //修改网站基本信息
    public bool WebInforUpd(string nID, string WebURLs, string Webtitles, string Webdescriptions, string Webkeywords, string smtp)
    {
        SqlParameter WebURL = new SqlParameter("@WebURL", WebURLs);
        SqlParameter Webtitle = new SqlParameter("@Webtitle", Webtitles);
        SqlParameter Webdescription = new SqlParameter("@Webdescription", Webdescriptions);
        SqlParameter Webkeyword = new SqlParameter("@Webkeyword", Webkeywords);
        SqlParameter selectmail = new SqlParameter("@selectmail", smtp);
        SqlParameter[] count = { WebURL, Webtitle, Webdescription, Webkeyword, selectmail };
        string sql = "update ML_SysConfig set WebURL=@WebURL,Webtitle=@Webtitle,Webdescription=@Webdescription,Webkeyword=@Webkeyword,selectmail=@selectmail  where nID='" + nID + "'";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //插入网站基本信息
    public bool WebInfroInsert(string WebURLs, string Webtitles, string Webdescriptions, string Webkeywords, string smtp)
    {
        SqlParameter WebURL = new SqlParameter("@WebURL", WebURLs);
        SqlParameter Webtitle = new SqlParameter("@Webtitle", Webtitles);
        SqlParameter Webdescription = new SqlParameter("@Webdescription", Webdescriptions);
        SqlParameter Webkeyword = new SqlParameter("@Webkeyword", Webkeywords);
        SqlParameter selectmail = new SqlParameter("@selectmail", smtp);
        SqlParameter[] count = { WebURL, Webtitle, Webdescription, Webkeyword, selectmail };
        string sql = "insert into ML_SysConfig values(@WebURL,@Webtitle,@Webdescription,@Webkeyword,@selectmail)";

        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }

    public string SysDelPhoto(string ID)
    {
        string sql = "select smtp from  ML_SysConfig where nID=" + ID;
        string str = (string)her.ExecuteScalar(sql);
        return str;
    }
    public string SysDelPhotoOne()
    {
        string sql = "select top 1 smtp from  ML_SysConfig";
        string str = (string)her.ExecuteScalar(sql);
        return str;
    }

    #region 发送邮件
    //修改
    public bool EmailUpdate(string nID, string EFemail, string EFpass, string EFname, string ESemail, string Ename, string stmp)
    {
        SqlParameter Femail = new SqlParameter("@EFemail", EFemail);
        SqlParameter Fpass = new SqlParameter("@EFpass", EFpass);
        SqlParameter Fname = new SqlParameter("@EFname", EFname);
        SqlParameter Semail = new SqlParameter("@ESemail", ESemail);
        SqlParameter name = new SqlParameter("@Ename", Ename);
        SqlParameter Estmp = new SqlParameter("@Estmp", stmp);
        SqlParameter[] count = { Femail, Fpass, Fname, Semail, name, Estmp };
        string sql = "update ML_SysConfig set EFemail=@EFemail,EFpass=@EFpass,EFname=@EFname,ESemail=@ESemail,Ename=@Ename,Estmp=@Estmp where nID='" + nID + "'";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //插入
    public bool EmailInsert(string EFemail, string EFpass, string EFname, string ESemail, string Ename, string stmp)
    {
        SqlParameter Femail = new SqlParameter("@EFemail", EFemail);
        SqlParameter Fpass = new SqlParameter("@EFpass", EFpass);
        SqlParameter Fname = new SqlParameter("@EFname", EFname);
        SqlParameter Semail = new SqlParameter("@ESemail", ESemail);
        SqlParameter name = new SqlParameter("@Ename", Ename);
        SqlParameter Estmp = new SqlParameter("@Estmp", stmp);
        SqlParameter[] count = { Femail, Fpass, Fname, Semail, name, Estmp };
        string sql = "insert into ML_SysConfig values('','','','','','','','','','','','','','','','','','','','','','',@EFemail,@EFpass,@EFname,@ESemail,@Ename,@Estmp)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    #endregion
}