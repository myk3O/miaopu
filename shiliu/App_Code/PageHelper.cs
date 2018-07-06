using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;


public class PageHelper
{

    /// <summary>
    /// 导出Excel（使用HtmlTextWriter + GridView）
    /// </summary>        
    /// <param name="fileName">文件名</param>
    internal static void ExportExcel(DataTable source, string fileName)
    {
        GridView gv = new GridView();
        gv.DataSource = source;
        gv.DataBind();
        DataTableToExcel(gv, fileName);
    }

    /// <summary>
    /// 导出文件
    /// 张岳鹏 2014-4-18 
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="fileName"></param>
    /// <param name="DataSourceIsTable"></param>
    internal static void GridViewToExcel(GridView gv, string fileName)
    {
        foreach (GridViewRow row in gv.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");//数字格式
                    if (i == row.Cells.Count - 1)
                    {
                        row.Cells[i].Visible = false;
                    }
                }
            }
            //移除操作栏的标题  不执行啊！！！！！
            if (row.RowType == DataControlRowType.Header)
            {
                row.Cells[row.Cells.Count - 1].Visible = false;
            }
        }
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.BufferOutput = true;
        //设定输出的字符集
        //HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.Charset = "UTF-8";
        fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString();
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        // HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        ////设置导出文件的格式
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter stringWriter = new System.IO.StringWriter(cultureInfo);
        System.Web.UI.HtmlTextWriter textWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
        gv.RenderControl(textWriter);

        //在头上输这么一段据说是解决乱码问题的终级方案…………啊啊啊，头疼
        HttpContext.Current.Response.Write("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
        HttpContext.Current.Response.Write(stringWriter.ToString());
        HttpContext.Current.Response.Write("</body></html>");
        //HttpContext.Current.Response.End();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
        HttpContext.Current.Response.End();
    }
    /// <summary>
    /// 导出文件
    /// 张岳鹏 2014-4-18 
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="fileName"></param>
    /// <param name="DataSourceIsTable"></param>
    public static void DataTableToExcel(GridView gv, string fileName)
    {
        foreach (GridViewRow row in gv.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    //var ss = row.Cells[i].Text;
                    if (i == row.Cells.Count - 1)
                    {
                        row.Cells[i].BackColor = System.Drawing.Color.Red;
                    }
                    row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");//数字格式                   
                }
            }

        }
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.BufferOutput = true;
        //设定输出的字符集
        //HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.Charset = "UTF-8";
        fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString();
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        // HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        ////设置导出文件的格式
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter stringWriter = new System.IO.StringWriter(cultureInfo);
        System.Web.UI.HtmlTextWriter textWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
        gv.RenderControl(textWriter);

        //在头上输这么一段据说是解决乱码问题的终级方案…………啊啊啊，头疼
        HttpContext.Current.Response.Write("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
        HttpContext.Current.Response.Write(stringWriter.ToString());
        HttpContext.Current.Response.Write("</body></html>");
        //HttpContext.Current.Response.End();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
        HttpContext.Current.Response.End();
    }
}
