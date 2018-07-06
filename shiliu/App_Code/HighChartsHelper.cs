using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Model;

public class HighChartsHelper
{
    public HighChartsHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    string chartPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
    /// <summary>
    /// 显示折线图
    /// </summary>
    /// <returns></returns>
    public string OutputLine(HighCharts highCharts)
    {
        string result = string.Empty;
        string urlLine = chartPath + "js\\template\\line.js";
        using (StreamReader reader = new StreamReader(urlLine, Encoding.UTF8))
        {
            StringBuilder buffer = new StringBuilder(reader.ReadToEnd());
            buffer.Replace("{#title#}", highCharts.Title);
            buffer.Replace("{#yAxis#}", highCharts.YAxis);
            buffer.Replace("#categries#", highCharts.XAxis);
            buffer.Replace("{#series#}", highCharts.SeriesData);
            buffer.Replace("#tooltip#", highCharts.ToolTip);
            if (highCharts.Title == "注册人数")
            {
                buffer.Replace("#unit#", "个");
                //buffer.Replace("#Yunit#", "千个");
                buffer.Replace("#Yunit#", "个");
            }
            else
            {
                buffer.Replace("#Yunit#", "人");
                buffer.Replace("#unit#", "人");
            }
            result = buffer.ToString();
        }
        return result;
    }

    /// <summary>
    /// 初始化会员访问
    /// </summary>
    /// <param name="number"></param>
    public StringBuilder InitWebSiteArrayXcategories(Dictionary<string, int> dataInfo)
    {
        StringBuilder xcategories = new StringBuilder();
        xcategories.Append("");
        foreach (var item in dataInfo)
        {
            xcategories.Append("'" + item.Key + "',");
        }
        if (xcategories.Length > 1)
            xcategories.Remove(xcategories.Length - 1, 1);
        return xcategories;
    }
    /// <summary>
    /// 初始化图标数据 (每年每月每周每天) 
    /// </summary>
    /// <param name="websiteVisitedTopInfoNodes"></param>
    /// <returns></returns>
    public StringBuilder InitStatisticsSeriesData(string name, Dictionary<string, int> websiteVisitedTopInfoNodes, string type)
    {
        if (IsAllNull(websiteVisitedTopInfoNodes))
            return null;
        StringBuilder seriesData = new StringBuilder();
        if (type != null)
            seriesData.Append("{name:'" + name + "',type:'" + type + "',data: [");
        else
            seriesData.Append("{name:'" + name + "',data: [");
        for (int i = 0; i < websiteVisitedTopInfoNodes.Count; i++)
        {
            seriesData.Append(websiteVisitedTopInfoNodes.ElementAt(i).Value + ",");
        }
        if (seriesData.Length > 20)
        {
            seriesData.Remove(seriesData.Length - 1, 1);
            if (type != null)
            {
                seriesData.Append("],marker: {enabled: false},dashStyle: 'shortdot'}");
                return seriesData;
            }
            else
            {
                seriesData.Append("]}");
                return seriesData;
            }
        }
        else
            return null;
    }
    /// <summary>
    /// 判断是否全是0
    /// </summary>
    /// <param name="websiteVisitedTopInfoNodes"></param>
    /// <returns></returns>
    public bool IsAllNull(Dictionary<string, int> websiteVisitedTopInfoNodes)
    {
        for (int i = 0; i < websiteVisitedTopInfoNodes.Count; i++)
        {
            if (websiteVisitedTopInfoNodes.ElementAt(i).Value != 0)
                return false;
            else
                continue;
        }
        return true;
    }

    /// <summary>
    /// 初始化图标数据 重载+1
    /// </summary>
    /// <param name="name"></param>
    /// <param name="websiteVisitedTopInfoNodes"></param>
    /// <returns></returns>
    public StringBuilder InitWithArrayStatisticsSeriesData(string name, Dictionary<string, int> websiteVisitedTopInfoNodes)
    {
        StringBuilder seriesData = new StringBuilder();
        seriesData.Append("{name:'" + name + "',data: [");
        for (int i = 0; i < websiteVisitedTopInfoNodes.Count; i++)
        {
            seriesData.Append(websiteVisitedTopInfoNodes.ElementAt(i).Value + ",");
        }
        if (seriesData.Length > 20)
        {
            seriesData.Remove(seriesData.Length - 1, 1);
            seriesData.Append("]}");
            return seriesData;
        }
        else
            return null;
    }
    /// <summary>
    /// 四舍五入舍去小数位数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public double GetIntData(double data)
    {
        string dateString = string.Format("{0:####}", data);
        if (!string.IsNullOrEmpty(dateString))
            return double.Parse(dateString);
        return 0;
    }
}

