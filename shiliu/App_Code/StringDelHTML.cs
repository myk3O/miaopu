using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// StringDelHTML 的摘要说明
/// </summary>
public class StringDelHTML
{
    public StringDelHTML()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static string DelHTML(string Htmlstring)//将HTML去除
    {
        #region
        //删除脚本
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //删除HTML
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<A>.*</A>","");
        //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<[a-zA-Z]*=\.[a-zA-Z]*\?[a-zA-Z]+=\d&\w=%[a-zA-Z]*|[A-Z0-9]","");
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(\d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");
        //Htmlstring=HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
        #endregion
        return Htmlstring;
    }
    //内容修饰
    public static string Centers(string center, int num)
    {
        if (center.Length > num)
        {
            center = center.Substring(0, num) + "...";
        }
        return center;
    }
    /// <summary>
    /// 截取自定义长度
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string SubStringY(string str, int length)
    {
        var subStr = str;
        if (str.Length > length)
        {
            subStr = str.Substring(0, length) + "...";
        }
        return subStr;
    }


    /// <summary>
    /// 截取
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SubString(string str)
    {
        var subStr = str;
        if (str.Length > 4)
        {
            subStr = str.Substring(0, 4);
        }
        return subStr;
    }

    /// <summary>
    /// string*100  to int 
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static int PriceToIntUp(string price)
    {
        double p = Convert.ToDouble(price) * 100;
        return Convert.ToInt32(p);
    }
    /// <summary>
    /// int/100  to string 
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static string PriceToStringLow(int price)
    {
        double p = Convert.ToDouble(price) / 100;
        return (p).ToString("0.00");
    }
    /// <summary>
    /// double/ 100 to string 
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static string DoublePriceToString(double price)
    {
        double p = price / 100;
        return (p).ToString("0.00");
    }
}