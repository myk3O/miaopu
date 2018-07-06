using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;

public partial class Admin_ImgEdit : System.Web.UI.Page
{
    WebHelper web = new WebHelper();
    SqlHelper her = new SqlHelper();

    //private int nID
    //{
    //    get { return Convert.ToInt32(ViewState["nID"]); }
    //    set { ViewState["nID"] = value; }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Session["SelectProID"] = "";
            Initialization();
            DropGroup.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(DropGroup);
            DropGroup.SelectedIndex = 1;
            imgs.Attributes["onclick"] = "vbscript:history.back()";
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)//编辑
            {
                hidSp.Value = Request.QueryString["id"].ToString();
                imgs.Visible = true;
                Interface(Request.QueryString["id"].ToString());
            }
        }
    }

    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from dbo.ML_ImageClass ";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drop.Items.Add(new ListItem(dt.Rows[i]["tClassName"].ToString(), dt.Rows[i]["nID"].ToString()));
        }
    }

    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        DataTable dt = web.SelImg(id);
        if (dt.Rows.Count > 0)
        {
            DropGroup.SelectedValue = dt.Rows[0]["cID"].ToString();                        //所属分类
            Session["SelectProID"] = dt.Rows[0]["tMemo"].ToString();
            txtPicName.Text = dt.Rows[0]["tilte"].ToString();
            txtUrl.Text = dt.Rows[0]["tMemo"].ToString();                                   //标题内容   
            txtNum.Text = dt.Rows[0]["nPaiXu"].ToString();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }
    //界面初始化
    public void Initialization()
    {
        DropGroup.SelectedValue = "-1";
        txtPicName.Text = "";
        txtUrl.Text = "";                                 //标题内容   
        txtNum.Text = "1";
        imgs.Visible = false;
    }

    /// <summary>
    /// 选择产品路径
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string jumpUrl = @"window.open('ProductSelect.aspx', 'top', 'width=1000,height=700,menubar=0,scrollbars=1, resizable=1,status=1,titlebar=0,toolbar=0,location=0')";

        ClientScript.RegisterStartupScript(GetType(), "", "<script>" + jumpUrl + "</script>");
    }

    /// <summary>
    /// 确定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgSub_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)
        {
            //if (hidSp.Value == "")
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频！')</script>");
            //    return;
            //}
            SubmitAdd();
        }
        else
        {
            SubmitUpd(Request.QueryString["id"].ToString());
        }
    }

    //添加事件
    public void SubmitAdd()
    {
        if (DropGroup.SelectedItem.Value == "-1")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择所属分类！')</script>");
            return;
        }
        if (txtPicName.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('图片名称不能为空！')</script>");
            return;
        }
        if (Session["SelectProID"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('产品链接不能为空！')</script>");
            return;
        }
        if (!FileUpload1.HasFile)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('图片不能为空！')</script>");
            return;
        }
        else
        {
            UploadPhoto();

        }
        if (web.addImg(DropGroup.SelectedItem.Value, txtPicName.Text.Trim(), txtNum.Text.Trim(), hid.Value, Session["SelectProID"].ToString()))
        {
            Response.Redirect("ImgMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
            return;
        }
    }
    #region 上传图片
    //上传图片
    public void UploadPhoto()
    {
        FileInfo mFile = new FileInfo(FileUpload1.FileName);
        string sExt = mFile.Extension.ToLower();
        if (sExt != ".bmp" && sExt != ".jpg" && sExt != ".jpeg" && sExt != ".png" && sExt != ".gif")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您所上传的图片格式不正确！')</script>");
            return;
        }
        string filename = Guid.NewGuid().ToString() + sExt;
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Logo_Img";   //项目根路径   
        string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        DeleteOldAttach(fullname);
        FileUpload1.PostedFile.SaveAs(fullname);
        hid.Value = filename;
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
        string str = "";
        DataTable dt = web.SelImg(ID);
        if (dt.Rows.Count > 0)
        {
            str = dt.Rows[0]["imgUrl"].ToString();
        }
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Logo_Img";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }
    #endregion
    protected void imgback_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
        {
            Response.Redirect("ImgMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            Response.Redirect("ImgMain.aspx");
        }

    }
    //修改事件
    public void SubmitUpd(string ID)
    {
        if (DropGroup.SelectedItem.Value == "-1")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择所属分类！')</script>");
            return;
        }
        if (txtPicName.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('图片名称不能为空！')</script>");
            return;
        }
        if (Session["SelectProID"].ToString() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('产品链接不能为空！')</script>");
            return;
        }
        bool success = false;
        if (!FileUpload1.HasFile)//无图片
        {
            success = web.updateImg(ID, DropGroup.SelectedItem.Value, txtPicName.Text.Trim(), txtNum.Text.Trim(), Session["SelectProID"].ToString());
        }
        else
        {

            DeletePhoto(ID);//删除原有图片
            UploadPhoto();//上传图片
            success = web.updateImg(ID, DropGroup.SelectedItem.Value, txtPicName.Text.Trim(), txtNum.Text.Trim(), hid.Value, Session["SelectProID"].ToString());
        }
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("ImgMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败！')</script>");
        }
    }
}