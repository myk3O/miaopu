using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Model;


/// <summary>
/// 分红比例配置
/// </summary>
public class ConfigModel
{
    SqlHelperModel her = new SqlHelperModel();
    /// <summary>
    /// 首次关注赠送学习币
    /// </summary>
    public double gzFirst;
    /// <summary>
    /// 推荐关注赠送学分
    /// </summary>
    public double gzPart;


    public ConfigModel()
    {
        string sql = "select * from ML_SysBiLi";
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            this.gzFirst = Convert.ToDouble(dt.Select("BiliName='gzFirst'")[0]["BiLi"]);
            this.gzPart = Convert.ToDouble(dt.Select("BiliName='gzPart'")[0]["BiLi"]);

        }
        //
        // TODO: Add constructor logic here
        //
    }
}