using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web;

/// <summary>
/// 该类用于登陆信息验证
/// </summary>
public class LoginVerification
{
    public LoginVerification()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }

    //获取登陆者的信息
    public DataTable GetLoginBynID(string nid)
    {
        SqlHelper her = new SqlHelper();
        DataTable dt = new DataTable();
        string sql = "select * from ML_HomeMaking where nID=" + nid;
        return her.ExecuteDataTable(sql);
    }

    //登陆验证
    public bool Login(string name, string password)
    {
        SqlHelper her = new SqlHelper();
        SqlParameter txtname = new SqlParameter("@name", name);
        SqlParameter txtpass = new SqlParameter("@pass", password);
        SqlParameter[] Collection = { txtname, txtpass };
        string sql = "select count(*) from ML_Admin where tAdminName=@name and tAdminPass=@pass";
        int nun = (int)her.ExecuteScalar(sql, Collection);
        if (nun > 0)
        {
            return true;
        }
        return false;
    }

    //代理登陆验证
    public bool ALogin(string name, string password)
    {
        SqlHelper her = new SqlHelper();
        SqlParameter txtname = new SqlParameter("@HomeName", name);
        SqlParameter txtpass = new SqlParameter("@HomePass", password);
        SqlParameter[] Collection = { txtname, txtpass };
        string sql = "select count(*) from ML_HomeMaking where HomeName=@HomeName and HomePass=@HomePass";
        int nun = (int)her.ExecuteScalar(sql, Collection);
        if (nun > 0)
        {
            return true;
        }
        return false;
    }
    //获取登陆者的信息
    public DataTable loginmanige(string name, string password)
    {
        SqlHelper her = new SqlHelper();
        DataTable dt = new DataTable();
        SqlParameter txtname = new SqlParameter("@name", name);
        SqlParameter txtpass = new SqlParameter("@pass", password);
        SqlParameter[] Collection = { txtname, txtpass };
        string sql = "select * from ML_Admin where tAdminName=@name and tAdminPass=@pass";
        dt = her.ExecuteDataTable(sql, Collection);
        if (dt.Rows.Count > 0)
        {
            int lognum = Convert.ToInt32(dt.Rows[0]["nLogNum"]) + 1;
            string sql1 = "Update [ML_Admin] Set nLogNum='" + lognum + "',dtLastTime='" + DateTime.Now + "',tLastIP='" + getIp() + "' Where nID='" + dt.Rows[0]["nID"] + "'";
            her.ExecuteNonQuery(sql1);
        }
        return dt;
    }

    public DataTable GetnIDcID()
    {
        string nID = string.Empty;
        SqlHelper her = new SqlHelper();
        DataTable dt = new DataTable();
        string sql = "select nID,cid from ML_HomeMaking where cid=3 and  oHide=0";//总部的唯一标示cid=3  类型为总部
        return her.ExecuteDataTable(sql);
    }

    //获取代理商的信息
    public DataTable Aloginmanige(string name, string password)
    {
        SqlHelper her = new SqlHelper();
        DataTable dt = new DataTable();
        SqlParameter txtname = new SqlParameter("@HomeName", name);
        SqlParameter txtpass = new SqlParameter("@HomePass", password);
        SqlParameter[] Collection = { txtname, txtpass };
        string sql = "select * from ML_HomeMaking where HomeName=@HomeName and HomePass=@HomePass";
        dt = her.ExecuteDataTable(sql, Collection);
        if (dt.Rows.Count > 0)
        {
            //int lognum = Convert.ToInt32(dt.Rows[0]["nLogNum"]) + 1;
            //string sql1 = "Update [ML_Admin] Set nLogNum='" + lognum + "',dtLastTime='" + DateTime.Now + "',tLastIP='" + getIp() + "' Where nID='" + dt.Rows[0]["nID"] + "'";
            //her.ExecuteNonQuery(sql1);
        }
        return dt;
    }
    //更改代理商的信息
    public void Aupdatelogin(string name, string password)
    {
        SqlHelper her = new SqlHelper();
        DataTable dt = new DataTable();
        SqlParameter txtname = new SqlParameter("@name", name);
        SqlParameter txtpass = new SqlParameter("@pass", password);
        SqlParameter[] Collection = { txtname, txtpass };
        string sql = "select * from ML_Admin where tAdminName=@name and tAdminPass=@pass";
        dt = her.ExecuteDataTable(sql, Collection);
        if (dt.Rows.Count > 0)
        {
            int lognum = Convert.ToInt32(dt.Rows[0]["nLogNum"]) + 1;
            string sql1 = "Update [ML_Admin] Set nLogNum='" + lognum + "',dtLastTime='" + DateTime.Now + "',tLastIP='" + getIp() + "' Where nID='" + dt.Rows[0]["nID"] + "'";
            her.ExecuteNonQuery(sql1);
        }
    }
    //更改登陆者的信息
    public void updatelogin(string name, string password)
    {
        SqlHelper her = new SqlHelper();
        DataTable dt = new DataTable();
        SqlParameter txtname = new SqlParameter("@name", name);
        SqlParameter txtpass = new SqlParameter("@pass", password);
        SqlParameter[] Collection = { txtname, txtpass };
        string sql = "select * from ML_Admin where tAdminName=@name and tAdminPass=@pass";
        dt = her.ExecuteDataTable(sql, Collection);
        if (dt.Rows.Count > 0)
        {
            int lognum = Convert.ToInt32(dt.Rows[0]["nLogNum"]) + 1;
            string sql1 = "Update [ML_Admin] Set nLogNum='" + lognum + "',dtLastTime='" + DateTime.Now + "',tLastIP='" + getIp() + "' Where nID='" + dt.Rows[0]["nID"] + "'";
            her.ExecuteNonQuery(sql1);
        }
    }
    //获取IP
    public string getIp()
    {
        string str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (str != null && str != string.Empty)
        {
            string[] strs = str.Split(new char[] { ',', ';' });
            if (strs.Length > 0)
            {
                return strs[0];
            }
        }
        str = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        if (str != null && str != string.Empty) return str;
        else return "";
    }

}