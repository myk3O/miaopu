using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Admin_Servce_ServceClass : System.Web.UI.Page
{
    public string img1;
    public string img2;
    public string img3;
    public string img4;
    SqlHelper her = new SqlHelper();
    ServceHelper servce = new ServceHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Session["SelectProID1"] = "";
            Session["SelectProID2"] = "";
            Session["SelectProID3"] = "";
            Session["SelectProID4"] = "";
            tab.Visible = false;
            GridBind();
        }
        img1 = " <img id='viewImg1' src='' style='width: 80px; height: 80px; float: left;'>";
        img2 = " <img id='viewImg2' src='' style='width: 80px; height: 80px; float: left;'>";
        img3 = " <img id='viewImg3' src='' style='width: 80px; height: 80px; float: left;'>";
        img4 = " <img id='viewImg4' src='' style='width: 80px; height: 80px; float: left;'>";
    }
    public void GridBind()
    {
        string sql = "select * from ML_VideoClass order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(sql);
        gridField.DataSource = dt;
        gridField.DataBind();
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            if (servce.DelMainClass(e.CommandArgument.ToString()))
            {
                string sql = "delete from ML_VideoClass where nID=" + e.CommandArgument.ToString();
                her.ExecuteNonQuery(sql);
                GridBind();
            }
        }
        if (e.CommandName == "update")
        {
            DataTable dt = servce.SelMainClass(e.CommandArgument.ToString());
            if (dt.Rows.Count > 0)
            {
                txtfenleiName.Text = dt.Rows[0]["tClassName"].ToString();
                txtnum.Text = dt.Rows[0]["nPaiXu"].ToString();
                hid.Value = dt.Rows[0]["nID"].ToString();

                tab.Visible = true;
                imgAdd.Visible = false;
                imgSub.Visible = true;

                btnAdd.Visible = false;
               
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

    //添加 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tab.Visible = true;
        imgAdd.Visible = true;
        imgSub.Visible = false;
        txtfenleiName.Text = "";
        txtnum.Text = "";
        btnAdd.Visible = false;
    }
    //添加 内容
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入分类名称！')</script>");
            return;
        }
        bool success = servce.addMainClass(txtfenleiName.Text.Trim(), txtnum.Text.Trim());
        if (success)
        {

            GridBind();
            tab.Visible = false;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
            return;
        }
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgSub_Click(object sender, EventArgs e)
    {
        if (txtfenleiName.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入分类名称！')</script>");
            return;
        }
        if (servce.updateMainClass(hid.Value, txtfenleiName.Text.Trim(), txtnum.Text.Trim()))
        {

            tab.Visible = false;
            GridBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('失败！')</script>");
            return;
        }
    }

    //取消添加
    protected void imgback_Click(object sender, EventArgs e)
    {
        tab.Visible = false;
        btnAdd.Visible = true;

    }
    protected void gridField_DataBound(object sender, EventArgs e)
    {

    }



    /// <summary>
    /// 选择产品路径
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string jumpUrl = @"window.open('../ImgConfig/ProductSelect.aspx', 'top', 'width=1000,height=700,menubar=0,scrollbars=1, resizable=1,status=1,titlebar=0,toolbar=0,location=0')";

        ClientScript.RegisterStartupScript(GetType(), "", "<script>" + jumpUrl + "</script>");
    }


    #region 上传图片
    //上传图片
    public void UploadPhoto(HtmlInputFile file, HiddenField hid, int paixu)
    {
        string uploadName = file.Value;//获取待上传图片的完整路径，包括文件名

        //string uploadName = InputFile.PostedFile.FileName;
        string pictureName = "";//上传后的图片名，以GUID命名文件名，确保文件名没有重复
        if (file.Value != "")
        {
            int idx = uploadName.LastIndexOf(".");
            string sExt = uploadName.Substring(idx);//获得上传的图片的后缀名
            if (sExt != ".bmp" && sExt != ".jpg" && sExt != ".jpeg" && sExt != ".png" && sExt != ".gif")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您所上传的图片格式不正确！')</script>");
                return;
            }
            pictureName = Guid.NewGuid().ToString() + sExt;
        }    //对上传文件的大小进行检测，限定文件最大不超过8M
        if (file.Size > 8192000)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您所上传的图片太大，请重新选择！')</script>");
            return;
        }
        try
        {
            if (uploadName != "")
            {
                string path = Server.MapPath("~/upload_Img/Pruduct/");
                DeletePhoto(path, paixu);
                file.PostedFile.SaveAs(path + pictureName);
                hid.Value = pictureName;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }

    }
    //删除文件
    private void DeleteOldAttach(string filename)
    {
        try
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filename);
            if (fi.Exists)
                fi.Delete();
        }
        catch { }
    }
    //删除原有图片
    public void DeletePhoto(string path, int px)
    {
        string sql = "select fname from ML_ServiceMainImg where cid0=" + hid.Value + " and nPaixu=" + px;
        if (her.ExecuteScalar(sql) != null && her.ExecuteScalar(sql).ToString() != "")
        {
            DeleteOldAttach(path + her.ExecuteScalar(sql).ToString());
        }
    }
    #endregion
}