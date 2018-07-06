using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Servce_ServceEdit : System.Web.UI.Page
{
    public string picUrl;
    public string videoName;
    SqlHelper her = new SqlHelper();
    Product pc = new Product();

    private string Vid
    {
        get
        {
            return ViewState["Vid"] == null ? "" : ViewState["Vid"].ToString();
        }
        set
        {
            ViewState["Vid"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            Session["vrul"] = "";
            //this.Session.Remove("ImgName");
            Initialization();
            DropGroup.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(DropGroup, "ML_VideoClass");
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
        string sql = "select nID,tClassName from " + names + " order by npaixu";
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
        content1.InnerText = "";
        txtTlitle.Text = "";
        txtPubtime.Text = "";
        imgs.Visible = false;
        txtPrice.Text = "";
    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("<img id='viewImg" + i + "' src='../../upload_Img/" + arrPic[i].ToString() + "' style='width: 80px; height: 80px; float: left;'>");


        DataTable dt = pc.GetProductByID(id); ;
        if (dt.Rows.Count > 0)
        {
            hidPurl.Value = dt.Rows[0]["tPic"].ToString();//图片地址
            picUrl = "<img id='viewImg' src='../../upload_Img/VideoImg/" + hidPurl.Value + "' style='width: 124px; height: 100px; float: left;'>";
            hidVurl.Value = dt.Rows[0]["tVideo"].ToString();//视频地址
            //videoName = hidVurl.Value;
            videoname.Text = hidVurl.Value;


            DropGroup.SelectedValue = dt.Rows[0]["sid0"].ToString();

            txtTlitle.Text = dt.Rows[0]["VideoName"].ToString();                                   //标题内容
            content1.InnerText = dt.Rows[0]["tMemo"].ToString();

            txtPubtime.Text = dt.Rows[0]["dtPubTime"].ToString();
            txtPrice.Text = StringDelHTML.PriceToStringLow(dt.Rows[0]["Price"] == null ? 0 : Convert.ToInt32(dt.Rows[0]["Price"]));

            if (dt.Rows[0]["oNewest"].ToString() == "1")                                        //日韩
            {
                RadioState.SelectedValue = "1";
            }
            else
            {
                RadioState.SelectedValue = "0";
            }

            if (dt.Rows[0]["oTop"].ToString() == "1")                                        //选购
            {
                RadioTh.SelectedValue = "1";
            }
            else
            {
                RadioTh.SelectedValue = "0";
            }
            if (dt.Rows[0]["oHide"].ToString() == "1")                                        //上架
            {
                redioUPDown.SelectedValue = "1";
            }
            else
            {
                redioUPDown.SelectedValue = "0";
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
        UploadPhoto();

        bool success = false;
        if (hidPurl.Value == "")//没有图片
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频封面')</script>");
            return;
        }

        if (hidVurl.Value == "")//没有视频
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频')</script>");
            return;
        }


        success = pc.InsertProduct(DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), hidPurl.Value, hidVurl.Value, content1.InnerText,
            RadioState.SelectedItem.Value, RadioTh.SelectedItem.Value, txtPubtime.Text.Trim(), redioUPDown.SelectedItem.Value, txtPrice.Text);

        if (success)
        {
            Response.Redirect("ServceMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
        }
    }
    //修改事件
    public void SubmitUpd(string ID)
    {
        UploadPhoto();
        bool success = false;

        success = pc.UpdateProduct(ID, DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), hidPurl.Value, hidVurl.Value, content1.InnerText,
              RadioState.SelectedItem.Value, RadioTh.SelectedItem.Value, txtPubtime.Text.Trim(), redioUPDown.SelectedItem.Value, txtPrice.Text);


        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("ServceMain.aspx");
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
            Response.Redirect("ServceMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            Response.Redirect("ServceMain.aspx");
        }

    }

    /// <summary>
    /// 视频上传
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void upload_Click(object sender, EventArgs e)
    {
        if (InputFile.HasFile)
        {
            string FileName = this.InputFile.FileName;//获取上传文件的文件名,包括后缀  
            string ExtenName = System.IO.Path.GetExtension(FileName).ToLower();//获取扩展名 
            if (ExtenName != ".mp4")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "警告", "alert('请上传视频文件！');", true);
                return;
            }

            string SaveFileName = System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath("../../upload_Img/Video"), DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName);
            //合并两个路径为上传到服务器上的全路径  
            if (this.InputFile.ContentLength > 0)
            {
                try
                {
                    this.InputFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "警告", "alert('选择要上传的文件为空！');", true);
            }
            string url = "Video/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ExtenName; //文件保存的路径  
            hidVurl.Value = url;
            videoname.Text = url;
            float FileSize = (float)System.Math.Round((float)InputFile.ContentLength / 1024000, 1); //获取文件大小并保留小数点后一位,单位是M  
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "警告", "alert('请选择要上传的文件！');", true);
        }
    }

    #region 上传图片
    //上传图片
    public void UploadPhoto()
    {

        if (viewFiles1.HasFile)
        {
            FileInfo mFile = new FileInfo(viewFiles1.FileName);
            string sExt = mFile.Extension.ToLower();
            if (sExt != ".bmp" && sExt != ".jpg" && sExt != ".jpeg" && sExt != ".png" && sExt != ".gif")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('您所上传的图片格式不正确！')</script>");
                return;
            }
            string filename = Guid.NewGuid().ToString() + sExt;
            // string strPath = System.Web.HttpContext.Current.Request.MapPath("../../upload_Img/VideoImg");
            string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/VideoImg/";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
            //DeleteOldAttach(fullname);
            viewFiles1.PostedFile.SaveAs(fullname);
            hidPurl.Value = filename;
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
        string[] arrPic = GetPicList(ID);
        for (int i = 0; i < arrPic.Length - 1; i++)
        {
            if (!string.IsNullOrEmpty(arrPic[i].ToString()))
            {
                string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/VideoImg/";   //项目根路径   
                string fullname = Server.MapPath(strPath + "/" + arrPic[i].ToString());//保存文件的路径
                DeleteOldAttach(fullname);
            }
        }
    }
    #endregion

    private string[] GetPicList(string nid)
    {
        string sql = "select tPic from ML_ServiceArea where nID=" + nid;
        string strPic = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        string[] arrPic = strPic.Split(';');
        return arrPic;
    }


}