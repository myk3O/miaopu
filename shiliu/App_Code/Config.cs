using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maliang;
using System.Data;


/// <summary>
/// 分红比例配置
/// </summary>
public class Config
{
    SqlHelper her = new SqlHelper();
    /// <summary>
    /// 一班佣金比例
    /// </summary>
    public double leave1;
    /// <summary>
    /// 二班佣金比例
    /// </summary>
    public double leave2;
    /// <summary>
    /// 三班佣金比例
    /// </summary>
    public double leave3;//
    /// <summary>
    /// 班长，全球分红比例
    /// </summary>
    public double leave_1;//
    /// <summary>
    /// 班主任，全球分红比例
    /// </summary>
    public double leave_2;//
    /// <summary>
    /// 校长，全球分红比例
    /// </summary>
    public double leave_3;//
    /// <summary>
    /// 一班店铺个数
    /// </summary>
    public int level1Count;//
    /// <summary>
    /// 二班店铺个数
    /// </summary>
    public int level2Count;//
    /// <summary>
    /// 三班店铺个数
    /// </summary>
    public int level3Count;//
    /// <summary>
    /// 班长绩效比例
    /// </summary>
    public double jixiao3;//
    /// <summary>
    /// 班主任绩效比例
    /// </summary>
    public double jixiao1;//
    /// <summary>
    /// 校长绩效比例
    /// </summary>
    public double jixiao2;//
    /// <summary>
    /// 购物币比例
    /// </summary>
    public double learnPart;

    /// <summary>
    /// 税费
    /// </summary>
    public double shuifei;
    /// <summary>
    /// 学霸排名
    /// </summary>
    public int xuebaCount;
    /// <summary>
    /// 首次关注赠送学习币
    /// </summary>
    public double gzFirst;
    /// <summary>
    /// 推荐关注赠送学分
    /// </summary>
    public double gzPart;

    public Config()
    {
        string sql = "select * from ML_SysBiLi";
        DataTable dt = her.ExecuteDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            this.leave1 = Convert.ToDouble(dt.Select("BiliName='leave1'")[0]["BiLi"]);
            this.leave2 = Convert.ToDouble(dt.Select("BiliName='leave2'")[0]["BiLi"]);
            this.leave3 = Convert.ToDouble(dt.Select("BiliName='leave3'")[0]["BiLi"]);
            this.leave_1 = Convert.ToDouble(dt.Select("BiliName='leave_1'")[0]["BiLi"]);
            this.leave_2 = Convert.ToDouble(dt.Select("BiliName='leave_2'")[0]["BiLi"]);
            this.leave_3 = Convert.ToDouble(dt.Select("BiliName='leave_3'")[0]["BiLi"]);
            this.level1Count = Convert.ToInt32(dt.Select("BiliName='leave1count'")[0]["BiLi"]);
            this.level2Count = Convert.ToInt32(dt.Select("BiliName='leave2count'")[0]["BiLi"]);
            this.level3Count = Convert.ToInt32(dt.Select("BiliName='leave3count'")[0]["BiLi"]);
            this.jixiao3 = Convert.ToDouble(dt.Select("BiliName='jixiao3'")[0]["BiLi"]);
            this.jixiao1 = Convert.ToDouble(dt.Select("BiliName='jixiao1'")[0]["BiLi"]);
            this.jixiao2 = Convert.ToDouble(dt.Select("BiliName='jixiao2'")[0]["BiLi"]);
            this.learnPart = Convert.ToDouble(dt.Select("BiliName='learnPart'")[0]["BiLi"]);
            this.shuifei = Convert.ToDouble(dt.Select("BiliName='shuifei'")[0]["BiLi"]);
            this.xuebaCount = Convert.ToInt32(dt.Select("BiliName='xuebaCount'")[0]["BiLi"]);
            //
            this.gzFirst = Convert.ToDouble(dt.Select("BiliName='gzFirst'")[0]["BiLi"]);
            this.gzPart = Convert.ToDouble(dt.Select("BiliName='gzPart'")[0]["BiLi"]);
        }
        //
        // TODO: Add constructor logic here
        //
    }
}