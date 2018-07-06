using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// StringDataTime 的摘要说明
/// </summary>
public class StringDataTime
{
    public StringDataTime()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    //获取
    public static string GetAftenoon()
    {
        if (Convert.ToInt32(DateTime.Now.Hour.ToString()) > 12)
        {
            return "下午";
        }
        else
        {
            return "上午";
        }
    }
    //获取星期
    public static string GetWeekDay(DateTime time)
    {
        int y = int.Parse(time.Year.ToString());
        int m = int.Parse(time.Month.ToString());
        int d = int.Parse(time.Day.ToString());
        if (m == 1) m = 13;
        if (m == 2) m = 14;
        int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
        string weekstr = "";
        switch (week)
        {
            case 1: weekstr = "星期一"; break;
            case 2: weekstr = "星期二"; break;
            case 3: weekstr = "星期三"; break;
            case 4: weekstr = "星期四"; break;
            case 5: weekstr = "星期五"; break;
            case 6: weekstr = "星期六"; break;
            case 7: weekstr = "星期日"; break;
        }

        return weekstr;
    }
}