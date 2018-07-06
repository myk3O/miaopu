using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_News_NewsEdit : System.Web.UI.Page
{
    NewsHelper newshelper = new NewsHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        // if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Initialization();
            DropGroup.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(DropGroup);
            imgs.Attributes["onclick"] = "vbscript:history.back()";
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                imgs.Visible = true;
                Interface(Request.QueryString["id"].ToString());
            }
            // txtPubtime.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from dbo.ML_NewsClass";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drop.Items.Add(new ListItem(dt.Rows[i]["tClassName"].ToString(), dt.Rows[i]["nID"].ToString()));
        }
    }
    //界面初始化
    public void Initialization()
    {
        DropGroup.SelectedValue = "-1";
        txtTlitle.Text = "";
        txtFromWhere.Text = "";
        // txtPubtime.Value = "";
        content1.InnerText = "";
        //ftb.Text = "";
        imgs.Visible = false;
    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        DataTable dt = newshelper.getMang(id);
        if (dt.Rows.Count > 0)
        {
            //DropGroup.SelectedValue = dt.Rows[0]["tClassID"].ToString();                        //所属分类
            txtTlitle.Text = dt.Rows[0]["tTitle"].ToString();                                   //标题内容
            txtFromWhere.Text = dt.Rows[0]["tFromWhere"].ToString();                            //来至何处
            content1.InnerText = dt.Rows[0]["tMemo"].ToString();
            //ftb.Text = dt.Rows[0]["tMemo"].ToString();                                          //内容
            // txtPubtime.Value = dt.Rows[0]["dtPubTime"].ToString();                               //发布时间
            if (dt.Rows[0]["oTop"].ToString() == "True")                                        //是否置顶
            {
                RadioState.SelectedValue = "1";
            }
            else
            {
                RadioState.SelectedValue = "0";
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }
    //添加事件
    public void SubmitAdd()
    {
        string strTime = System.DateTime.Now.ToString();
        bool success = false;
        if (!FileUpload1.HasFile)
        {
            success = newshelper.NewsInsert("0", txtTlitle.Text.Trim(), content1.InnerText, txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value, strTime, "0");
        }
        else
        {
            UploadPhoto();
            success = newshelper.NewsInsert("0", txtTlitle.Text.Trim(), hid.Value, content1.InnerText, txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value, strTime, "0");
        }
        if (success)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加完成！')</script>");
            //Initialization();
            Response.Redirect("NewsMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
        }
    }
    //修改事件
    public void SubmitUpd(string ID)
    {
        string strTime = System.DateTime.Now.ToString();
        bool success = false;
        if (!FileUpload1.HasFile)//是否需要上传图片
        {
            success = newshelper.NewsUpdate(ID, DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), content1.InnerText, txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value, strTime);
        }
        else
        {
            DeletePhoto(ID);//删除原有图片
            UploadPhoto();//上传图片
            success = newshelper.NewsUpdate(ID, DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), hid.Value, content1.InnerText, txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value, strTime);
        }
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("NewsMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败！')</script>");
        }
    }
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
        //如果目录不存在就创建目录
        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath(HttpContext.Current.Request.FilePath + "../upload_Img/"));
        //if (!dir.Exists)
        //{
        //    dir.Create();
        //}
        string filename = Guid.NewGuid().ToString() + sExt;
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/News";   //项目根路径   
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
        string str = newshelper.NewsDelPhoto(ID);
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/News";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }
    protected void imgSub_Click(object sender, EventArgs e)
    {

        if (txtTlitle.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择资讯标题！')</script>");
            return;
        }
        //if (txtPubtime.Value == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择发布时间！')</script>");
        //    return;
        //}
        if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)
        {
            SubmitAdd();
        }
        else
        {
            SubmitUpd(Request.QueryString["id"].ToString());
        }
    }
    protected void imgback_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
        {
            Response.Redirect("NewsMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            Response.Redirect("NewsMain.aspx");
        }
    }
}