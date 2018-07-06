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

public partial class Main_Right : System.Web.UI.Page
{
    private string Anid
    {
        get
        {
            return ViewState["Anid"].ToString();
        }
        set
        {
            ViewState["Anid"] = value;
        }
    }
    public string strDaiLiList;
    public string strQr;
    public string strTongJ;
    public string result;
    private DateTime _statisticsBeginDate;
    private DateTime _statisticsEndDate;
    public int _statisticsTimeLength;//0代表按日查询,1代表按周查询,2按月查询,3.代表按年查询
    DaiLi dl = new DaiLi();
    Order or = new Order();
    SqlHelper her = new SqlHelper();
    private Dictionary<string, int> SchWebsiteRank = new Dictionary<string, int>();
    MemberStatHelper members = new MemberStatHelper();

    //该键值对存放会员
    private Dictionary<string, int> DateStatisitcs = new Dictionary<string, int>();
    //该键值对存放设计师
    private Dictionary<string, int> DesignMember = new Dictionary<string, int>();
    private HighCharts _lineCharts = new HighCharts();//画图
    public HighChartsHelper _highChartsHelper = new HighChartsHelper();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); } else { Anid = Session["anid"].ToString(); }
        if (!IsPostBack)
        {
            InitPage();
            DataTableShow();
            TongJi();
        }
    }

    private void TongJi()
    {
        DataTable dtdl = dl.MemberSelectTongJi();//代理商申请
        DataTable dtor = or.GetOrderTongji();//待发货
        StringBuilder sb = new StringBuilder();

        StringBuilder sb1 = new StringBuilder();

        StringBuilder sb2 = new StringBuilder();
        sb.AppendLine("<li><i><a href='../Members/MemberMain.aspx'>用户总数：</a></i>" + dl.MakSelectTongJi() + "人</li>");
        sb.AppendLine("<li><i><a href='../Pruduct/ServceMain.aspx'>会员总数：</a></i>" + dl.ProductSelectTongJi() + "人</li>");
        sb.AppendLine("<li><i><a href='../Order/OrderMain.aspx'>视频个数：</a></i>" + or.videoCount() + "部</li>");
        sb.AppendLine("<li><i><a href='../Order/OrderMainFaHuo.aspx'>总成交额：</a></i>" + or.momeycount() + "元</li>");
        sb.AppendLine("<li><i><a href='../Order/OrderMain.aspx'>日成交额：</a></i>" + or.momeycountDate() + "元</li>");
        sb.AppendLine("<li><i><a href='../Order/OrderMainTuiHuo.aspx'>总订单量：</a></i>" + or.OrderCount() + "个</li>");
        strTongJ = sb.ToString();
        foreach (DataRow dr in dtdl.Rows)
        {
            string time = Convert.ToDateTime(dr["dtAddTime"]).ToString("yyyy-MM-dd HH:mm:ss");
            string nicname = StringDelHTML.Centers(dr["nickname"].ToString(), 15);
            sb1.AppendLine("<li><a href='../Members/Member_Main.aspx'>" + nicname + "</a><b>" + time + "</b></li>");
        }
        foreach (DataRow dr in dtor.Rows)
        {
            string time = Convert.ToDateTime(dr["CreateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
            sb2.AppendLine("<li><a href='../Order/OrderMain.aspx'>" + dr["OrderCode"].ToString() + "</a><b>" + time + "</b></li>");
        }

        strDaiLiList = sb1.ToString();
        strQr = sb2.ToString();
    }
    private void InitPage()
    {
        _statisticsBeginDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-30)));
        _statisticsEndDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", DateTime.Now));
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
    ///画图图标 
    /// </summary>
    public void ModifyTheChart()
    {
        InitData();
        //折线图
        InitDataLineTable();
        if (string.IsNullOrEmpty(_lineCharts.SeriesData))
        {
            //noData.InnerText = "没有数据";
            return;
        }
        else
        {

        }
        result = _highChartsHelper.OutputLine(_lineCharts);
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
        dt = members.EverydayAdd(startTime, endTime, ockecked);//阿姨每日新增量
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
        dt = members.SamedayAdd(startTime, endTime, ockecked);//阿姨每日新增量
        foreach (DataRow dr in dt.Rows)
        {
            dic.Add(dr["dtAddTime"].ToString(), Convert.IsDBNull(dr["MemberNum"]) == true ? 0 : Convert.ToInt32(dr["MemberNum"]));
        }
        return dic;
    }
    /// <summary>
    /// 初始化普通会员折线图数据
    /// </summary>
    public void InitDataLineTable()
    {
        _lineCharts.Title = "用户数量统计";
        StringBuilder xAxis = new StringBuilder();
        _lineCharts.YAxis = "人数";
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
    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <returns></returns>
    public void InitData()
    {
        _statisticsBeginDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-30)));
        _statisticsEndDate = DateTime.Parse(string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-1)));
    }
}