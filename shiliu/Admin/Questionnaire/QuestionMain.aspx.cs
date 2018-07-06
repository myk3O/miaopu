using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
public partial class Admin_Questionnaire_QuestionMain : System.Web.UI.Page
{
    public string BindTitle;
    public string BindStr;
    private string QCid
    {
        get
        {
            return ViewState["QCid"].ToString();
        }
        set
        {
            ViewState["QCid"] = value;
        }
    }
    SqlHelper her = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["QCid"] != "" && Request.QueryString["QCid"] != null)
            {
                QCid = Request.QueryString["QCid"].ToString();
                BindSource();
                GetTitle();
            }

        }
    }
    private void GetTitle()
    {
        string sql = "select tClassName from ML_GongdiClass where nID=" + QCid;
        BindTitle = her.ExecuteScalar(sql) == null ? "问卷调查报告" : "【" + her.ExecuteScalar(sql).ToString() + "】问卷调查报告";
    }

    private void BindSource()
    {
        StringBuilder sb = new StringBuilder();
        double countA = 0.00;
        double countB = 0.00;
        double countC = 0.00;
        double countD = 0.00;
        double allCount = 0.00;

        string sql = string.Format(@"select * from ML_Gongdi  where sid0={0} and oHide={1}", QCid, 0);//某一次问卷的选择题
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            countA = 0.00;
            countB = 0.00;
            countC = 0.00;
            countD = 0.00;
            allCount = 0.00;
            var sid1 = dt.Rows[i]["nID"].ToString();

            string sql2 = string.Format(@"select XuanXiang, count(XuanXiang) point from dbo.ML_GongdiXuanZe 
 where sid0={0} and sid1={1} group by XuanXiang ", QCid, sid1); //某次某题
            DataTable dt2 = her.ExecuteDataTable(sql2);
            foreach (DataRow dr in dt2.Rows)
            {
                switch (dr["XuanXiang"].ToString())
                {
                    case "1": countA = Convert.ToInt32(dr["point"]); break;
                    case "2": countB = Convert.ToInt32(dr["point"]); break;
                    case "3": countC = Convert.ToInt32(dr["point"]); break;
                    case "4": countD = Convert.ToInt32(dr["point"]); break;
                    default: break;
                }
            }
            allCount = countA + countB + countC + countD;
            string ticount = (i + 1).ToString();

            //StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div style='line-height: 32px; font-weight: bold; font-size: 14px; margin-top: 20px;'>");
            sb.AppendLine("<span style='color: #3d81ee;'>第" + ticount + "题：</span><span style='color: #333333;'>"
                + dt.Rows[i]["tTitlle"].ToString() + "</span></div>");
            sb.AppendLine("<table width='100%' border='1' cellpadding='6' cellspacing='1'  bgcolor='#CBCBCB'");
            sb.AppendLine("style='font-size: 12px; border-collapse: collapse'>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr align='center' style='font-weight: bold; background: #f5f8fa'>");
            sb.AppendLine("<td>选项");
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='center' style='width: 10%;'>小计");
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='left' style='width: 30%;'>比例");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");
            sb.AppendLine("<tr style='background: white'>");
            sb.AppendLine("<td bgcolor='#FFFFFF' val='1'>" + dt.Rows[i]["Q1"].ToString());
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='center' bgcolor='#FFFFFF'>" + countA);
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='' bgcolor='#FFFFFF'>");
            sb.AppendLine("<div style='margin-top: 3px; float: left;'>" + PointBB(countA, allCount) + "</div>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr style='background: #eff6fb'>");
            sb.AppendLine("<td bgcolor='#FFFFFF' val='2'>" + dt.Rows[i]["Q2"].ToString());
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='center' bgcolor='#FFFFFF'>" + countB);
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='' bgcolor='#FFFFFF'>");
            sb.AppendLine("<div style='margin-top: 3px; float: left;'>" + PointBB(countB, allCount) + "</div>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr style='background: white'>");
            sb.AppendLine("<td bgcolor='#FFFFFF' val='3'>" + dt.Rows[i]["Q3"].ToString());
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='center' bgcolor='#FFFFFF'>" + countC);
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='' bgcolor='#FFFFFF'>");
            sb.AppendLine("<div style='margin-top: 3px; float: left;'>" + PointBB(countC, allCount) + "</div>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr style='background: #eff6fb'>");
            sb.AppendLine("<td bgcolor='#FFFFFF' val='4'> " + dt.Rows[i]["Q4"].ToString());
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='center' bgcolor='#FFFFFF'>" + countD);
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='' bgcolor='#FFFFFF'>");
            sb.AppendLine("<div style='margin-top: 3px; float: left;'>" + PointBB(countD, allCount) + "</div>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</tbody>");
            sb.AppendLine("<tfoot>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td bgcolor='#FFFFFF'>");
            sb.AppendLine("<b>本题有效填写人次</b>");
            sb.AppendLine("</td>");
            sb.AppendLine("<td align='center' bgcolor='#FFFFFF'>");
            sb.AppendLine("<b>" + allCount + "</b>");
            sb.AppendLine("</td>");
            sb.AppendLine("<td bgcolor='#FFFFFF'>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</tfoot>");
            sb.AppendLine("</table>");
        }

        BindStr = sb.ToString();
    }

    private string PointBB(double co1, double co2)
    {
        double doub;
        if (co2 != 0)
        {
            doub = (co1 * 100 / co2);
            doub = Math.Round(doub, 2);
        }
        else
        {
            doub = 0;
        }
        return doub.ToString() + "%";
        //return (co1 * 100 / co2).ToString("0.00") + "%";
    }
}