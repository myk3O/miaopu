using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Configuration;

public partial class Admin_SetConfig : System.Web.UI.Page
{
    AdminManagHelper adminMH = new AdminManagHelper();
    NewsHelper newshelper = new NewsHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../Error.aspx"); }
        if (!IsPostBack)
        {
            Bindding();
        }
    }
    //初始化
    public void Bindding()
    {
        DataTable dt = adminMH.WebsiteInformation();
        if (dt.Rows.Count > 0)
        {
            hid.Value = dt.Rows[0]["nID"].ToString();
            txtAdress.Text = dt.Rows[0]["WebURL"].ToString();
            txtName.Text = dt.Rows[0]["Webtitle"].ToString();
            content1.InnerText = dt.Rows[0]["selectmail"].ToString();
            txtCont.Text = dt.Rows[0]["Webdescription"].ToString();
            txtKey.Text = dt.Rows[0]["Webkeyword"].ToString();
            //txttel.Text = dt.Rows[0]["smtp"].ToString();
        }
        else
        {
            hid.Value = "";
        }
    }

    //上传图片
    public void UploadPhoto()
    {
        //if (!FileUpload1.HasFile)
        //{ return; }
        //FileInfo mFile = new FileInfo(FileUpload1.FileName);
        //string sExt = mFile.Extension.ToLower();
        //if (sExt != ".bmp" && sExt != ".jpg" && sExt != ".jpeg" && sExt != ".png" && sExt != ".gif")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您所上传的图片格式不正确！')</script>");
        //    return;
        //}
        ////如果目录不存在就创建目录
        ////DirectoryInfo dir = new DirectoryInfo(Server.MapPath(HttpContext.Current.Request.FilePath + "../upload_Img/"));
        ////if (!dir.Exists)
        ////{
        ////    dir.Create();
        ////}
        //string filename = Guid.NewGuid().ToString() + sExt;
        //string strPath = HttpContext.Current.Request.FilePath + "/../upload_Img/imgLogo";   //项目根路径   
        //string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        //DeleteOldAttach(fullname);
        //FileUpload1.PostedFile.SaveAs(fullname);
        //hid2.Value = filename;
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
        //if (!FileUpload1.HasFile)
        //{ return; }
        //string str = adminMH.SysDelPhoto(ID);
        //if (str != "")
        //{
        //    string strPath = HttpContext.Current.Request.FilePath + "/../upload_Img/imgLogo";   //项目根路径   
        //    string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
        //    DeleteOldAttach(fullname);
        //}
    }
    protected void btnSub_Click(object sender, EventArgs e)
    {
        // UpdateConfig();
        if (hid.Value != "")
        {
            UploadPhoto();//上传图片
            DeletePhoto(hid.Value);//删除原有图片
            bool success = adminMH.WebInforUpd(hid.Value, txtAdress.Text.Trim(), txtName.Text.Trim(), txtCont.Text.Trim(), txtKey.Text.Trim(), content1.InnerText

               );
            if (success)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('成功！')</script>");
                Bindding();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误，更新失败2！')</script>");
            }
        }
        else
        {
            UploadPhoto();
            bool success = adminMH.WebInfroInsert(txtAdress.Text.Trim(), txtName.Text.Trim(), txtCont.Text.Trim(), txtKey.Text.Trim(), content1.InnerText);
            if (success)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('成功！')</script>");
                Bindding();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误，更新失败1！')</script>");
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }


    /// <summary>  
    /// 更新web.config中pagesize设置。  
    /// </summary>  
    protected void UpdateConfig()
    {
        Configuration objConfig = WebConfigurationManager.OpenWebConfiguration("~");
        AppSettingsSection objAppSettings = (AppSettingsSection)objConfig.GetSection("appSettings");
        if (objAppSettings != null)
        {
            //objAppSettings.Settings["tel"].Value = txttel.Text.Trim();
            //objConfig.Save();
        }

    }
}