
using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

public partial class Admin_HighChart : System.Web.UI.Page
{
    public string result;
    private DateTime _statisticsBeginDate;
    private DateTime _statisticsEndDate;
    public int _statisticsTimeLength;//0代表按日查询,1代表按周查询,2按月查询,3.代表按年查询
    bool isFirstLoad = true;

    SqlHelper her = new SqlHelper();
    MemberStatHelper members = new MemberStatHelper();
    private Dictionary<string, int> SchWebsiteRank = new Dictionary<string, int>();

    //该键值对存放会员
    private Dictionary<string, int> DateStatisitcs = new Dictionary<string, int>();
    //该键值对存放设计师
    private Dictionary<string, int> DesignMember = new Dictionary<string, int>();

    private HighCharts _lineCharts = new HighCharts();//画图
    public HighChartsHelper _highChartsHelper = new HighChartsHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            DataTableShow();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void InitPage()
    {
        if (isFirstLoad)
        {
            txtStatisticsStartTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-30));
            txtStatisticsEndTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            //LbtnLastMonth.CssClass = "SelectColor";
        }
        //LookDnsStatisitcsType.Items[2].Selected = true;
        _statisticsBeginDate = DateTime.Parse(txtStatisticsStartTime.Text);
        _statisticsEndDate = DateTime.Parse(txtStatisticsEndTime.Text);
        _statisticsTimeLength = 0;
    }
    //获取table表格的数据
    public void DataTableShow()
    {
        int DateType = _statisticsTimeLength;
        switch (DateType)
        {
            case 0:
                GetDesignByDateAndLengthDay();//根据数据库中的日月年为0,1,2.所以把week设置为3
                GetStatisicsDetailByDateAndLengthDay();
                break;
        }
        ModifyTheChart();
    }
    /// <summary>
    /// 每日新增量
    /// </summary>
    public void GetDesignByDateAndLengthDay()
    {
        Dictionary<string, int> date = DataDael(_statisticsBeginDate, _statisticsEndDate, "ML_Member");//普通会员
        DesignMember = date;
    }
    /// <summary>
    /// 当日所有量
    /// </summary>
    public void GetStatisicsDetailByDateAndLengthDay()
    {
        Dictionary<string, int> date = DataDaels(_statisticsBeginDate, _statisticsEndDate, "ML_Member");//普通会员
        DateStatisitcs = date;
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <returns></returns>
    public void InitData()
    {
        _statisticsBeginDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", txtStatisticsStartTime.Text));
        _statisticsEndDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", txtStatisticsEndTime.Text));

    }

    /// <summary>
    /// 数据处理-每日新增量
    /// </summary>
    /// <param name="i">时间</param>
    /// <param name="scopeType">搜索类型</param>
    /// <param name="dateType">时间类型</param>
    /// <returns></returns>
    private Dictionary<string, int> DataDael(DateTime startTime, DateTime endTime, string ockecked)
    {
        SchWebsiteRank.Clear();
        Dictionary<string, int> dic = new Dictionary<string, int>();
        DataTable dt = new DataTable();
        dt = members.EverydayAdd(startTime, endTime, ockecked);//每日新增量
        foreach (DataRow dr in dt.Rows)
        {
            dic.Add(dr["dtAddTime"].ToString(), Convert.IsDBNull(dr["MemberNum"]) == true ? 0 : Convert.ToInt32(dr["MemberNum"]));
        }
        return dic;
    }
    /// <summary>
    /// 数据处理-当日所有量
    /// </summary>
    /// <param name="i">时间</param>
    /// <param name="scopeType">搜索类型</param>
    /// <param name="dateType">时间类型</param>
    /// <returns></returns>
    private Dictionary<string, int> DataDaels(DateTime startTime, DateTime endTime, string ockecked)
    {
        SchWebsiteRank.Clear();
        Dictionary<string, int> dic = new Dictionary<string, int>();
        DataTable dt = new DataTable();
        dt = members.SamedayAdd(startTime, endTime, ockecked);//总量
        foreach (DataRow dr in dt.Rows)
        {
            dic.Add(dr["dtAddTime"].ToString(), Convert.IsDBNull(dr["MemberNum"]) == true ? 0 : Convert.ToInt32(dr["MemberNum"]));
        }
        return dic;
    }

    /// <summary>
    ///画图图标 
    /// </summary>
    public void ModifyTheChart()
    {
        InitData();
        //折线图
        InitDataLineTable();
        if (string.IsNullOrEmpty(_lineCharts.SeriesData))
        {
            noData.InnerText = "没有数据";
            return;
        }
        else
        {

        }
        result = _highChartsHelper.OutputLine(_lineCharts);
    }

    /// <summary>
    /// 初始化普通会员折线图数据
    /// </summary>
    public void InitDataLineTable()
    {
        _lineCharts.Title = "用户数量统计";
        StringBuilder xAxis = new StringBuilder();
        _lineCharts.YAxis = "个数";
        _lineCharts.SeriesData = "" + GetSeriesData(out xAxis) + "";
        _lineCharts.XAxis = "" + xAxis + "";
    }

    /// <summary>
    /// 获取普通会员折线图数据
    /// </summary>
    /// <param name="xAxis"></param>
    /// <returns></returns>
    private StringBuilder GetSeriesData(out StringBuilder xAxis)
    {
        StringBuilder seriesData = new StringBuilder();
        int DateType = _statisticsTimeLength;
        xAxis = _highChartsHelper.InitWebSiteArrayXcategories(DesignMember);

        Dictionary<string, int> avgData = GetAvgData(DesignMember);
        seriesData = _highChartsHelper.InitStatisticsSeriesData("每日新增量", avgData, "line");
        if (seriesData != null)
        {
            seriesData.Append(",");
            seriesData.Append(_highChartsHelper.InitWithArrayStatisticsSeriesData("当前所有量", DateStatisitcs));
        }
        return seriesData;
    }
    /// <summary>
    /// 获取平均值  更改为设计师
    /// </summary>
    /// <param name="dnsStatisticsNodes"></param>
    /// <returns></returns>
    public Dictionary<string, int> GetAvgData(Dictionary<string, int> DesignMember)
    {
        //普通会员
        return DesignMember;
    }
    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        _statisticsBeginDate = DateTime.Parse(txtStatisticsStartTime.Text);
        _statisticsEndDate = DateTime.Parse(txtStatisticsEndTime.Text);
        Refreshpage();
    }
    /// <summary>
    /// 刷新页面
    /// </summary>
    private void Refreshpage()
    {
        InitData();
        noData.Visible = false;
        DivChart.Visible = true;
        DataTableShow();
    }
}