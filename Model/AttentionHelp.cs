using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class AttentionHelp
    {
        SqlHelperModel her = new SqlHelperModel();

        /// <summary>
        /// 要邀请关注，送学分（邀请人）
        /// </summary>
        public bool AttentionInsert(string uid, string fuid, int money)
        {
            bool success = false;
            SqlParameter[] count = 
            {  
                new SqlParameter("@AtTypeId",Convert.ToInt32(1)),//代理商id
                new SqlParameter("@AtTypeName","邀请赠送学分"), 
                new SqlParameter("@userID",Convert.ToInt32(uid)), 
                new SqlParameter("@FuserID",Convert.ToInt32(fuid)),   
                new SqlParameter("@money",money),       
                new SqlParameter("@CreateTime",System.DateTime.Now.ToString())
            };

            string sql = @"insert into T_Attention values (@AtTypeId,@AtTypeName,@userID,@FuserID,@money,@CreateTime)";

            success = her.ExecuteNonQuery(sql, count);

            return success;
        }


        /// <summary>
        /// 被邀请人（首次关注送学习币）
        /// </summary>
        public bool AttentionInserted(string uid, string fuid, int money)
        {
            bool success = false;
            SqlParameter[] count = 
            {  
                new SqlParameter("@AtTypeId",Convert.ToInt32(2)),//代理商id
                new SqlParameter("@AtTypeName","首次关注送学习币"), 
                new SqlParameter("@userID",Convert.ToInt32(uid)), 
                new SqlParameter("@FuserID",Convert.ToInt32(fuid)),   
                new SqlParameter("@money",money),       
                new SqlParameter("@CreateTime",System.DateTime.Now.ToString())
            };

            string sql = @"insert into T_Attention values (@AtTypeId,@AtTypeName,@userID,@FuserID,@money,@CreateTime)";

            success = her.ExecuteNonQuery(sql, count);

            return success;
        }
    }
}
