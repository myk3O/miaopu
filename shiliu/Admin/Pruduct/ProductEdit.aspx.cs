using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Pruduct_ProductEdit : System.Web.UI.Page
{
    public string picUrl;
    public string videoName;
    SqlHelper her = new SqlHelper();
    Video pc = new Video();

    private string purl
    {
        get
        {
            return ViewState["purl"] == null ? "" : ViewState["purl"].ToString();
        }
        set
        {
            ViewState["purl"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            //this.Session.Remove("ImgName");
            Initialization();
            DropGroup.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(DropGroup, "ML_VideoComment");
            imgs.Attributes["onclick"] = "vbscript:history.back()";
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                imgs.Visible = true;
                Interface(Request.QueryString["id"].ToString());
            }
            txtPubtime.Text = DateTime.Now.ToString();

        }
        //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('回发！')</script>");

    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop, string names)
    {
        string sql = "select nID,vName from " + names + " order by nID";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drop.Items.Add(new ListItem(dt.Rows[i]["vName"].ToString(), dt.Rows[i]["nID"].ToString()));
        }
    }
    //界面初始化
    public void Initialization()
    {
        purl = "";
        hidPurl.Value = "";
        DropGroup.SelectedValue = "-1";
        txtmulu.Text = "";
        txtTlitle.Text = "";
        txtPubtime.Text = "";
        txtpaixu.Text = "";
        imgs.Visible = false;
    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        StringBuilder sb = new StringBuilder();

        DataTable dt = pc.GetPByID(id); ;
        if (dt.Rows.Count > 0)
        {
            purl = dt.Rows[0]["tPic"].ToString();//图片地址
            hidPurl.Value = dt.Rows[0]["tPic"].ToString();//图片地址
            picUrl = "<img id='viewImg1' src='../../upload_Img/Video/" + hidPurl.Value + "' style='width: 124px; height: 100px; float: left;'>";
            txtmulu.Text = dt.Rows[0]["tVideo"].ToString();//视频地址           
            DropGroup.SelectedValue = dt.Rows[0]["sid0"].ToString();
            txtTlitle.Text = dt.Rows[0]["VideoName"].ToString();                                   //标题内容
            txtPubtime.Text = dt.Rows[0]["dtPubTime"].ToString();
            txtpaixu.Text = dt.Rows[0]["oTop"].ToString();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }
    //添加事件
    public void SubmitAdd()
    {
        UploadPhoto(viewFiles1, hidPurl);//视频图片
        bool success = false;
        if (hidPurl.Value == "")//没有图片
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频封面')</script>");
            return;
        }
        success = pc.InsertProduct(DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), hidPurl.Value, txtmulu.Text.Trim(), txtPubtime.Text.Trim(), Convert.ToInt32(txtpaixu.Text.Trim()));

        if (success)
        {
            Response.Redirect("ProductMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
        }
    }
    //修改事件
    public void SubmitUpd(string ID)
    {

        UploadPhoto(viewFiles1, hidPurl);//视频图片
        if (!hidPurl.Value.Equals(purl))//不相等就等于更新了图片，那么删除旧图片
        {
            DeletePhoto(ID);
        }
        bool success = false;
        if (hidPurl.Value == "")//没有图片
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频封面')</script>");
            return;
        }
        success = pc.UpdateProduct(ID, DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), hidPurl.Value, txtmulu.Text.Trim(), txtPubtime.Text.Trim(), Convert.ToInt32(txtpaixu.Text.Trim()));


        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("ProductMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败！')</script>");
        }
    }
    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgSub_Click(object sender, EventArgs e)
    {
        string tt = txtPubtime.Text.Trim();
        if (DropGroup.SelectedItem.Value == "-1")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择视频系列！')</script>");
            return;
        }
        if (txtTlitle.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请输入视频名称！)</script>");
            return;
        }

        if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)//新增
        {
            SubmitAdd();

        }
        else//更新
        {
            SubmitUpd(Request.QueryString["id"].ToString());

        }
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgback_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
        {
            Response.Redirect("ProductMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            Response.Redirect("ProductMain.aspx");
        }

    }



    #region 上传图片
    //上传图片
    public void UploadPhoto(FileUpload fine, HiddenField hid)
    {

        if (fine.HasFile)
        {
            FileInfo mFile = new FileInfo(fine.FileName);
            string sExt = mFile.Extension.ToLower();
            if (sExt != ".bmp" && sExt != ".jpg" && sExt != ".jpeg" && sExt != ".png" && sExt != ".gif")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您所上传的图片格式不正确！')</script>");
                return;
            }
            string filename = Guid.NewGuid().ToString() + sExt;
            // string strPath = System.Web.HttpContext.Current.Request.MapPath("../../upload_Img/VideoImg");
            string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/Video/";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
            //DeleteOldAttach(fullname);

            fine.PostedFile.SaveAs(fullname);
            hid.Value = filename;
        }
    }
    //删除文件
    private void DeleteOldAttach(string path)
    {
        try
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            if (fi.Exists)
                fi.Delete();
        }
        catch { }
    }
    //删除原有图片
    public void DeletePhoto(string ID)
    {
        var pic = GetPicList(ID);

        if (!string.IsNullOrEmpty(pic))
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/Video/";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + pic);//保存文件的路径
            DeleteOldAttach(fullname);
        }

    }
    #endregion
    private string GetPicList(string nid)
    {
        //string sql = "select tPic from ML_ServiceArea where nID=" + nid;
        //string strPic = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        //string[] arrPic = strPic.Split(';');
        //return arrPic;
        string sql = "select tPic from ML_Video where nID=" + nid;
        string strPic = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        return strPic;
    }
}