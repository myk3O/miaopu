using System;
using System.Data;
using System.Data.SqlClient;
using Maliang;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Pagination : System.Web.UI.UserControl
{
    private GridView gridView;
    private DataTable mDataTable;
    private string txtpagetext;

    /// <summary>
    /// 要绑定的GridView控件
    /// </summary>
    public GridView MGridView
    {
        get { return gridView; }
        set { gridView = value; }
    }

    /// <summary>
    /// 要绑定的GridView控件
    /// </summary>
    public DataTable MDataTable
    {
        get { return mDataTable; }
        set { mDataTable = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) return;
        if (!IsPostBack)
        {
            showData();
            setButton();
        }
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    public void Refresh()
    {
        showData();
        setButton();
    }

    /// <summary>
    /// 获取当前页数
    /// </summary>
    public string pagetext()
    {
        return txtGotoPage.Text;
    }

    /// <summary>
    /// 分页
    /// </summary>
    private void showData()
    {
        if (gridView == null) return;
        if (mDataTable == null) mDataTable = new DataTable();
        gridView.DataSource = mDataTable;
        gridView.DataBind();

        txtGotoPage.Text = (gridView.PageIndex + 1).ToString();
        txtpagetext = (gridView.PageIndex + 1).ToString();
        lblPageCount.Text = "共<span style='font-weight:bold;'>" + gridView.PageCount.ToString() + "</span>页";
        lblRecordCount.Text = "共<span style='font-weight:bold;'>" + mDataTable.DefaultView.Count.ToString() + "</span>条记录";
        lblRecordPerPage.Text = "<span style='font-weight:bold;'>" + gridView.PageSize.ToString() + "</span>条/页";
    }

    /// <summary>
    /// 这是用来显示导行栏中每个元素的状态
    /// </summary>
    void setButton()
    {
        if (gridView == null) return;
        lbtBack.Enabled = lbtTop.Enabled = gridView.PageIndex == 0 ? false : true;
        if (gridView.PageCount == 0)
        {
            lbtEnd.Enabled = lbtNext.Enabled = false;
        }
        else
        {
            lbtEnd.Enabled = lbtNext.Enabled = gridView.PageIndex == gridView.PageCount - 1 ? false : true;
        }
    }

    public void lbtTop_Click(object sender, System.EventArgs e)
    {
        gridView.PageIndex = 0;
        gridView.EditIndex = -1;
        showData();
        setButton();
    }

    public void lbtBack_Click(object sender, System.EventArgs e)
    {
        gridView.PageIndex--;
        gridView.EditIndex = -1;
        showData();
        setButton();
    }

    public void lbtNext_Click(object sender, System.EventArgs e)
    {
        gridView.PageIndex++;
        gridView.EditIndex = -1;
        showData();
        setButton();
    }

    public void lbtEnd_Click(object sender, System.EventArgs e)
    {
        gridView.PageIndex = gridView.PageCount - 1;
        gridView.EditIndex = -1;
        showData();
        setButton();
    }

    protected void imgbtnGo_Click(object sender, ImageClickEventArgs e)
    {
        int index;
        if ((gridView.PageIndex + 1).ToString() != txtGotoPage.Text.Trim())
        {
            gridView.EditIndex = -1;
        }
        if (!int.TryParse(txtGotoPage.Text.Trim(), out index))
        {
            txtGotoPage.Text = (gridView.PageIndex + 1).ToString();
        }
        if (gridView == null) return;
        if (index > 0) index--;
        if (index > gridView.PageCount)
        {
            index = gridView.PageCount - 1;
        }
        gridView.PageIndex = index;
        showData();
        setButton();
    }


}
