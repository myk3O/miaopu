
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

using ThoughtWorks.QRCode.Codec;

public partial class Admin_DaiLi_HomeMakMainDL : System.Web.UI.Page
{
    DaiLi makbll = new DaiLi();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["anid"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            BindDropHomeSystem(dropDailiSystem, "ML_HomeMakingClass", " where oHide = 0");
            dropDailiSystem.Items.Insert(0, new ListItem("请选择", "-1"));
            dropFatherDaili.Items.Insert(0, new ListItem("请选择", "-1"));
            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
            {
                gridField.PageIndex = int.Parse(Request.QueryString["ceid"].ToString());
            }
        }
        GridBind();
    }
    //绑定下拉框
    public void BindDropHomeSystem(DropDownList drop, string names, string where)
    {
        // IAdminBLL adminbll = BLLFactory.CreateAdminBLL();
        // DataTable dt = adminbll.AdminBind(names, where);
        drop.DataSource = makbll.HomeMakSystemBind(names, where);
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
    }
    //绑定GridView
    public void GridBind()
    {
        DataTable dt = makbll.MakSelect(Session["anid"].ToString(), keyName.Value.Trim(), dropDailiSystem.SelectedItem.Value, dropFatherDaili.SelectedItem.Value, DropState.SelectedItem.Value);
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
    }
    //绑定下拉框
    public void BindDrop(DropDownList drop, string names, string where)
    {
        ////IAdminBLL adminbll = BLLFactory.CreateAdminBLL();
        //DataTable dt = adminbll.AdminBind(names, where);
        drop.DataSource = makbll.SityBind(names, where);
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
    }
    //绑定下拉框
    public void BindDropHomeMak(DropDownList drop, string names, string where)
    {
        // IAdminBLL adminbll = BLLFactory.CreateAdminBLL();
        // DataTable dt = adminbll.AdminBind(names, where);
        drop.DataSource = makbll.HomeMakBind(names, where);
        drop.DataValueField = "nID";
        drop.DataTextField = "tRealName";
        drop.DataBind();
    }
    protected void dropDailiSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropDailiSystem.SelectedItem.Value == "-1")
        {
            dropFatherDaili.Items.Clear();
            dropFatherDaili.Items.Insert(0, new ListItem("请选择", "-1"));
        }
        else
        {
            BindDropHomeMak(dropFatherDaili, "ML_HomeMaking", string.Format(@" where cid = {0}", Convert.ToInt32(dropDailiSystem.SelectedItem.Value) - 1));
            dropFatherDaili.Items.Insert(0, new ListItem("请选择", "-1"));
        }
    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lblState = (Label)gridField.Rows[i].FindControl("lblState");
            Button button1 = (Button)gridField.Rows[i].FindControl("btnJinyong");
            Button button2 = (Button)gridField.Rows[i].FindControl("btnJiechu");
            if (lblState.Text == "True")
            {
                lblState.Text = "已启用";
                //button1.Visible = true;
               // button2.Visible = false;
            }
            else
            {
                lblState.Text = "已禁用";
               // button1.Visible = false;
                //button2.Visible = true;
            }
            Label lbgroup = (Label)gridField.Rows[i].FindControl("lblGroup");
            lbgroup.Text = makbll.HomeMakName(lbgroup.Text);
        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "jinyong")
        {
            HomeMakInfo makinfo = new HomeMakInfo();
            makinfo.nID = int.Parse(e.CommandArgument.ToString());
            makinfo.oCheck = "0";
            bool success = makbll.MakUpd(makinfo);
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "qiyong")
        {
            HomeMakInfo makinfo = new HomeMakInfo();
            makinfo.nID = int.Parse(e.CommandArgument.ToString());
            makinfo.oCheck = "1";
            bool success = makbll.MakUpd(makinfo);
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            Response.Redirect("HomeMakEdit.aspx?id=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }
        if (e.CommandName == "dingdan")
        {
            Response.Redirect("../Order/OrderMainDL.aspx?dlid=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "detail")
        {
            Response.Redirect("HomeMakByOne.aspx?id=" + e.CommandArgument.ToString());
        }


        if (e.CommandName == "AddUrl")
        {
            var url = makeUrl(e.CommandArgument.ToString());
            var picName = ImageAdd(url);
            HomeMakInfo makinfo = new HomeMakInfo();
            makinfo.nID = int.Parse(e.CommandArgument.ToString());
            makinfo.HomePic = picName;
            makinfo.CreatorName = url;
            if (makbll.MakUrl(makinfo))
            {
                GridBind();
                Pagination2.Refresh();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('生成失败')</script>");
            }
        }
    }
    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }
    protected void gridField_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("HomeMakEdit.aspx?ceid=" + gridField.PageIndex);
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                HomeMakInfo makinfo = new HomeMakInfo();
                makinfo.nID = int.Parse(gridField.DataKeys[i].Value.ToString());
                makinfo.oCheck = "0";
                bool success = makbll.MakUpd(makinfo);
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination2.Refresh();
    }

    private string makeUrl(string nID)
    {
        //string url = HttpContext.Current.Request.Url.Host;
        string url = "http://maset.com.cn/";
        string aurl = url + "/Wap/Index.aspx?agent=" + nID;
        return aurl;
    }
    private string ImageAdd(string str)
    {
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        qrCodeEncoder.QRCodeScale = 4;
        qrCodeEncoder.QRCodeVersion = 8;
        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        String data = str;
        //生成二维码
        System.Drawing.Bitmap image = qrCodeEncoder.Encode(data);
        //上传图片
        string filename = Guid.NewGuid().ToString() + ".png";
        string strPath = HttpContext.Current.Request.FilePath + "/../../Upload/pic";   //项目根路径   
        string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        //string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        //string strPath = HttpContext.Current.Request.FilePath + "/../skin/images";   //项目根路径   
        //string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        image.Save(fullname);
        return filename;
    }
}