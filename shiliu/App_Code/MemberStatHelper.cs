using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class MemberStatHelper
{
    public MemberStatHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    SqlHelper her = new SqlHelper();
    /// <summary>
    /// 每日新增量
    /// </summary>
    /// <param name="dd">初始日期</param>
    /// <param name="dti">结束日期</param>
    /// <param name="tab">所要查询的表名如：ML_HomeAunt</param>
    /// <returns></returns>
    public DataTable EverydayAdd(DateTime dd, DateTime dti, string tab)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("dtAddTime");
        dt.Columns.Add("MemberNum");
        string sql = string.Format(@"select CONVERT(varchar(12),DATEADD(day,number,'{0}'),23) as dtAddTime,
                                    (select count(*) from {1} where CONVERT(varchar(12),dtAddTime,23)=CONVERT(varchar(12),DATEADD(day,number,'{2}'),23))as MemberNum
                                    from master..spt_values where type = 'P' and '{3}'>= DATEADD(day,number,'{4}')", dd, tab, dd, dti, dd);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
    /// <summary>
    /// 当日所有量
    /// </summary>
    /// <param name="dd">初始日期</param>
    /// <param name="dti">结束日期</param>
    /// <param name="tab">所要查询的表名如：ML_HomeAunt</param>
    /// <returns></returns>
    public DataTable SamedayAdd(DateTime dd, DateTime dti, string tab)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("dtAddTime");
        dt.Columns.Add("MemberNum");
        string sql = string.Format(@"select CONVERT(varchar(12),DATEADD(day,number,'{0}'),23) as dtAddTime,
                                    (select count(*) from {1} where CONVERT(varchar(12),dtAddTime,23)<=CONVERT(varchar(12),DATEADD(day,number,'{2}'),23))as MemberNum
                                    from master..spt_values where type = 'P' and '{3}'>= DATEADD(day,number,'{4}')", dd, tab, dd, dti, dd);
        dt = her.ExecuteDataTable(sql);
        return dt;
    }
}

