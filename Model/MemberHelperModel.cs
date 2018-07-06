using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Model
{

    /// <summary>
    /// MemberHelper 的摘要说明
    /// </summary>
    public class MemberHelperModel
    {
        private static readonly object lockerModel = new object();
        public MemberHelperModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        SqlHelperModel her = new SqlHelperModel();


        //根据openid查询账户
        public DataTable GetMemberByOpenID(string openid)
        {
            string nID = string.Empty;
            string sql = string.Format(@"select nID,MemberCode from ML_Member where openid='{0}'", openid);

            return her.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        public bool MemberInsert(string openid, string nickname, int sex, string imgurl, string fid)
        {
            bool success = false;
            string faid = fid == null ? "0" : fid == "null" ? "0" : fid;
            string code = string.Empty;
            lock (lockerModel)
            {
                string sql1 = "select usercode from UserMaxCode";
                code = her.ExecuteScalar(sql1) == null ? "" : her.ExecuteScalar(sql1).ToString();
                string sql2 = "update UserMaxCode set usercode=usercode+1";
                her.ExecuteNonQuery(sql2);

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
            }
            return success;
        }



        //修改会员信息
        public bool MemberUpdate(string openid, string nickname, int sex, string imgurl)
        {
            bool success = false;
            SqlParameter[] count = 
            {  
                new SqlParameter("@openid",openid),//代理商id
                new SqlParameter("@nickname",nickname), 
                new SqlParameter("@MemberSex",sex),                               
                new SqlParameter("@headimgurl", imgurl ), 
            };

            string update = @"update [ML_Member] set nickname=@nickname ,MemberSex=@MemberSex,headimgurl=@headimgurl 
             where openid=@openid";
            success = her.ExecuteNonQuery(update, count);
            if (success)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否已经注册
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public bool Verification(string openid)
        {
            openid = string.IsNullOrEmpty(openid) ? "" : openid;
            string sql = "select count(*) from ML_Member where openid='" + openid + "'";
            int num = Convert.ToInt32(her.ExecuteScalar(sql));
            if (num > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否有上级用户
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public bool VerificationFather(string openid)
        {
            openid = string.IsNullOrEmpty(openid) ? "" : openid;
            string sql = "select FatherFXSID from ML_Member where openid='" + openid + "'";
            int fid = her.ExecuteScalar(sql) == null ? 0 : Convert.ToInt32(her.ExecuteScalar(sql));
            if (fid > 0)
            {
                return true;
            }
            return false;
        }
    }

}
