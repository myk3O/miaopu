using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Pruduct_ProductClassEdit : System.Web.UI.Page
{
    public string picUrl;
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
            BindDrop(DropGroup, "T_Teacher");
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
        string sql = "select nID,teacherName from " + names + " order by CreateTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drop.Items.Add(new ListItem(dt.Rows[i]["teacherName"].ToString(), dt.Rows[i]["nID"].ToString()));
        }
    }
    //界面初始化
    public void Initialization()
    {
        content1.InnerText = "";
        txtTlitle.Text = "";
        txtPubtime.Text = "";
        txtmulu.Text = "";
        imgs.Visible = false;
        txtPrice.Text = "";
        hidPurl.Value = "";
        purl = "";
        DropGroup.SelectedValue = "-1";
        txtcx.Text = "";


    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        StringBuilder sb = new StringBuilder();

        DataTable dt = pc.GetProductByID(id); ;
        if (dt.Rows.Count > 0)
        {
            purl = dt.Rows[0]["vPic"].ToString();//图片地址
            hidPurl.Value = dt.Rows[0]["vPic"].ToString();//图片地址
            // hidTou.Value = dt.Rows[0]["teacherImg"].ToString();//老师头像
            picUrl = "<img id='viewImg1' src='../../upload_Img/VideoImg/" + hidPurl.Value + "' style='width: 124px; height: 100px; float: left;'>";
            txtmulu.Text = dt.Rows[0]["vUrl"].ToString();//视频地址
            //picTou = "<img id='viewImg2' src='../../upload_Img/VideoImg/" + hidTou.Value + "' style='width: 124px; height: 100px; float: left;'>";

            txtTlitle.Text = dt.Rows[0]["vName"].ToString();                                   //标题内容
            content1.InnerText = dt.Rows[0]["vMemo"].ToString();

            txtPubtime.Text = dt.Rows[0]["addTime"].ToString();
            txtPrice.Text = StringDelHTML.PriceToStringLow(dt.Rows[0]["Price"] == null ? 0 : Convert.ToInt32(dt.Rows[0]["Price"]));
            DropGroup.SelectedValue = dt.Rows[0]["teacherID"].ToString();

            txtcx.Text = dt.Rows[0]["vdiscrib"] == null ? "" : dt.Rows[0]["vdiscrib"].ToString();




            if (dt.Rows[0]["oHide"].ToString() == "True")                                        //上架
            {
                redioUPDown.SelectedValue = "1";
            }
            else
            {
                redioUPDown.SelectedValue = "0";
            }

            if (dt.Rows[0]["oFree"].ToString() == "True")                                        //免费
            {
                radioFree.SelectedValue = "1";
            }
            else
            {
                radioFree.SelectedValue = "0";
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
        UploadPhoto(viewFiles1, hidPurl);//视频图片
        bool success = false;
        if (hidPurl.Value == "")//没有图片
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频封面')</script>");
            return;
        }



        success = pc.InsertProduct(txtTlitle.Text.Trim(), hidPurl.Value, txtmulu.Text.Trim(), content1.InnerText,
            txtPubtime.Text.Trim(), redioUPDown.SelectedItem.Value, txtPrice.Text
            , txtcx.Text.Trim(), DropGroup.SelectedItem.Value, radioFree.SelectedItem.Value);

        if (success)
        {
            Response.Redirect("ProductClassMain.aspx");
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

        success = pc.UpdateProduct(ID, txtTlitle.Text.Trim(), hidPurl.Value, txtmulu.Text.Trim(), content1.InnerText,
            txtPubtime.Text.Trim(), redioUPDown.SelectedItem.Value, txtPrice.Text
            , txtcx.Text.Trim(), DropGroup.SelectedItem.Value, radioFree.SelectedItem.Value);


        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("ProductClassMain.aspx");
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
            Response.Redirect("ProductClassMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            Response.Redirect("ProductClassMain.aspx");
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
            string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/VideoImg/";   //项目根路径   
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
            string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/VideoImg/";   //项目根路径   
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
        string sql = "select vPic from ML_VideoComment where nID=" + nid;
        string strPic = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        return strPic;
    }

}