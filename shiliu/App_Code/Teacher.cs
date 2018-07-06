using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
/// <summary>
/// Summary description for Teacher
/// </summary>
public class Teacher
{
    SqlHelper her = new SqlHelper();
    public Teacher()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //根据ID删除相关信息
    public bool DeleteTeacherByID(string ID)
    {
        string sql = "delete from T_Teacher where nID=" + ID;
        if (her.ExecuteNonQuery(sql))
        {
            return true;
        }
        return false;
    }

    //根据ID查询相关信息
    public DataTable GetTeacherByID(string ID)
    {
        DataTable dt = new DataTable();
        string sql = string.Format(@"select * from T_Teacher where nID={0}", ID);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 添加讲师
    /// </summary>
    /// <param name="pbtime"></param>
    /// <param name="tn"></param>
    /// <param name="timg"></param>
    /// <param name="td"></param>
    /// <param name="tm"></param>
    /// <returns></returns>
    public bool InsertTeacher(string pbtime, string tn, string timg, string td, string tm)
    {
        SqlParameter teacherName = new SqlParameter("@teacherName", tn);
        SqlParameter teacherImg = new SqlParameter("@teacherImg", timg);
        SqlParameter teacherdiscrib = new SqlParameter("@teacherdiscrib", td);
        SqlParameter teacherMemo = new SqlParameter("@teacherMemo", tm);
        SqlParameter dtAddTime = new SqlParameter("@CreateTime", Convert.ToDateTime(pbtime));

        SqlParameter[] count = { teacherName, teacherImg, teacherdiscrib, teacherMemo, dtAddTime };
        string sql = @"insert into T_Teacher values (@teacherName,@teacherImg,@teacherdiscrib,@teacherMemo,@CreateTime)";
        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// 修改讲师
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="pbtime"></param>
    /// <param name="tn"></param>
    /// <param name="timg"></param>
    /// <param name="td"></param>
    /// <param name="tm"></param>
    /// <returns></returns>
    public bool UpdateTeacher(string ID, string pbtime, string tn, string timg, string td, string tm)
    {

        SqlParameter teacherName = new SqlParameter("@teacherName", tn);
        SqlParameter teacherImg = new SqlParameter("@teacherImg", timg);
        SqlParameter teacherdiscrib = new SqlParameter("@teacherdiscrib", td);
        SqlParameter teacherMemo = new SqlParameter("@teacherMemo", tm);
        SqlParameter dtAddTime = new SqlParameter("@CreateTime", Convert.ToDateTime(pbtime));
        SqlParameter[] count = { teacherName, teacherImg, teacherdiscrib, teacherMemo, dtAddTime };

        string sql = @"update T_Teacher set teacherName=@teacherName,teacherImg=@teacherImg,teacherdiscrib=@teacherdiscrib,
teacherMemo=@teacherMemo,CreateTime=@CreateTime where nID=" + ID;

        bool success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
}