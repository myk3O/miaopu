using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// MemberHelper 的摘要说明
/// </summary>
public class MemberHelper
{
    private static readonly object locker = new object();
    public MemberHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    //根据ID查询相关信息
    public DataTable GetMember(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select a.*,b.levelName from ML_Member a join ML_MemberLevel b on a.fxslevel=b.nID where a.nID={0}", ID);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //根据code查询用户
    public DataTable GetMemberByCode(string code)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select a.*,b.levelName from ML_Member a join ML_MemberLevel b on a.fxslevel=b.nID where a.MemberCode='{0}'", code);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }

    //根据openid查询账户
    public string GetMemberID(string openid)
    {
        string nID = string.Empty;
        string sql = string.Format(@"select nID from ML_Member where openid='{0}'", openid);
        nID = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        return nID;
    }
    /// <summary>
    /// 用户注册
    /// </summary>
    public bool MemberInsert(string openid, string nickname, int sex, string imgurl, string fid)
    {
        string faid = fid == null ? "0" : fid == "null" ? "0" : fid;
        string code = string.Empty;
        lock (locker)
        {
            string sql1 = "select usercode from UserMaxCode";
            code = her.ExecuteScalar(sql1) == null ? "" : her.ExecuteScalar(sql1).ToString();
            string sql2 = "update UserMaxCode set usercode=usercode+1";
            her.ExecuteNonQuery(sql2);

            bool success = false;
            if (!string.IsNullOrEmpty(code))
            {
                string time = System.DateTime.Now.ToString();
                SqlParameter[] count = 
                {  
                    new SqlParameter("@openid",openid),//代理商id
                    new SqlParameter("@nickname",nickname), 
                    new SqlParameter("@MemberCode",code), 
                    new SqlParameter("@MemberSex",sex),   
                    new SqlParameter("@FatherFXSID",faid),                       
                    new SqlParameter("@dtAddTime",time ),         
                    new SqlParameter("@headimgurl", imgurl ),         
                    new SqlParameter("@isJXS",Convert.ToInt32(0)),         
                    new SqlParameter("@MemberState", Convert.ToInt32(0)),
                    new SqlParameter("@isUseful", Convert.ToInt32(1)),
                    new SqlParameter("@fxsTimeBegin",  time),   
                    new SqlParameter("@fxsTimeEnd",  time), 
                    new SqlParameter("@fxslevel",  Convert.ToInt32(1)),   
                    new SqlParameter("@AllMoney",  Convert.ToInt32(0)),
                    new SqlParameter("@level3Time",  time),   
                };

                string sql = @"insert into ML_Member values (@openid,@nickname,'',@MemberCode,@MemberSex,'','',@FatherFXSID,'',
@dtAddTime,@headimgurl,@isJXS,@MemberState,@isUseful,@fxsTimeBegin,@fxsTimeEnd,@fxslevel,'','',@AllMoney,@level3Time)";

                success = her.ExecuteNonQuery(sql, count);

            }
            if (success)
            {
                return true;
            }
        }
        return false;
    }
    //修改会员信息
    public bool MemberUpdate(string openid, string nickname, int sex, string imgurl, string fxsImg, string fid)
    {
        bool success = false;
        SqlParameter[] count = 
            {  
                new SqlParameter("@openid",openid),//代理商id
                new SqlParameter("@nickname",nickname), 
                new SqlParameter("@MemberSex",sex),
                new SqlParameter("@FatherFXSID",fid),                   
                new SqlParameter("@headimgurl", imgurl ),   
                new SqlParameter("@fxsImg", fxsImg ),      
            };

        string update = @"update [ML_Member] set nickname=@nickname,MemberSex=@MemberSex,FatherFXSID=@FatherFXSID,
                headimgurl=@headimgurl,fxsImg=@fxsImg where openid=@openid";
        success = her.ExecuteNonQuery(update, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //删除会员信息
    public bool MemberDelete(string ID)
    {
        string sql = "DELETE FROM [ML_Member] WHERE nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }
    //用户，禁用，启用
    public bool MemberUpdateState(string ID, int state)
    {
        string sql = "update  [ML_Member] set isUseful=" + state + " WHERE nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }
    ////分销商，撤店
    //public bool MemberUpdateFxs(string ID, int fxs)
    //{
    //    string sql = "update  [ML_Member] set isJXS=" + fxs + " WHERE nID=" + ID;
    //    if (her.ExecuteNonQuery(sql))
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    //分销商，开店、撤店
    public bool MemberUpdateFxs(string ID, int fxs, string url, string imgurl)
    {
        bool success = false;
        SqlParameter[] count = 
            {  
                new SqlParameter("@nID", ID),            
                new SqlParameter("@fxsurl", url), 
                new SqlParameter("@fxsImg",imgurl),
                new SqlParameter("@isJXS", fxs),
                //new SqlParameter("@FatherFXSID", fid),
                new SqlParameter("@fxslevel", Convert.ToInt32(2))
            };
        string sql = string.Format(@"update ML_Member set fxsurl=@fxsurl ,fxsImg=@fxsImg ,isJXS=@isJXS ,
                                fxslevel=@fxslevel where nID=@nID");
        try { success = her.ExecuteNonQuery(sql, count); }
        catch { }


        return success;
    }
    //验证用户名是否重复
    public bool Verification(string openid)
    {
        openid = string.IsNullOrEmpty(openid) ? "" : openid;
        //SqlParameter openID = new SqlParameter("@openid", openid);
        //SqlParameter[] count = { openID };

        string sql = "select count(*) from ML_Member where openid='" + openid + "'";
        //int num = her.ExecuteScalar(sql) == DBNull.Value ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
        int num = Convert.ToInt32(her.ExecuteScalar(sql));
        if (num > 0)
        {
            return true;
        }
        return false;
    }
    //验证用户名是否重复
    public bool VerificationEdit(string phone, string ID)
    {
        SqlParameter username = new SqlParameter("@phone", phone);
        SqlParameter[] count = { username };
        string sql = "select count(*) from [ML_Member] where [MemberPhone]=@phone and nID!=" + ID;//除了自己
        int num = her.ExecuteScalar(sql, count) == DBNull.Value ? 0 : (int)her.ExecuteScalar(sql, count);
        if (num > 0)
        {
            return true;
        }
        return false;
    }
    //查询会员密码
    public string MemberSelPass(string ID)
    {
        string sql = "select MemberPass from dbo.ML_Member WHERE nID=" + ID;
        return (string)her.ExecuteScalar(sql);
    }

    //查询用户留言
    public DataTable MemberSelLiuyan(string ID)
    {
        string sql = "select * from dbo.ML_MemberMessage where nID=" + ID;
        return her.ExecuteDataTable(sql);
    }

    //插入回复
    public bool MemberHFinsert(string sid, string tMemo)
    {
        SqlParameter sids = new SqlParameter("@sid", sid);
        SqlParameter Memo = new SqlParameter("@tMemo", tMemo);
        SqlParameter dttime = new SqlParameter("@dttime", DateTime.Now);
        SqlParameter[] count = { sids, Memo, dttime };
        string sql = "insert into ML_MemberMessageR values (@sid,0,0,'',@tMemo,@dttime,@dttime,1)";
        return her.ExecuteNonQuery(sql, count);
    }
    public bool MemberHFdel(string sid)
    {
        string str = "delete ML_MemberMessageR where nID=" + sid;
        bool success = her.ExecuteNonQuery(str);
        return success;
    }
}