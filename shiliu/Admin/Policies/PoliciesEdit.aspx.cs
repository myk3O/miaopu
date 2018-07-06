using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Policies_PoliciesEdit : System.Web.UI.Page
{
    PoliciesHelper policies = new PoliciesHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
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
            txtPubtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from dbo.ML_PoliciesClass order by nPaiXu asc";
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
        txtPubtime.Text = "";
        content1.InnerText = "";
        //ftb.Text = "";
        txtPubtime.Text = "";
        imgs.Visible = false;
    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        DataTable dt = policies.getMang(id);
        if (dt.Rows.Count > 0)
        {
            DropGroup.SelectedValue = dt.Rows[0]["tClassID"].ToString();                        //所属分类
            txtTlitle.Text = dt.Rows[0]["tTitle"].ToString();                                   //标题内容
            txtFromWhere.Text = dt.Rows[0]["tFromWhere"].ToString();                            //来至何处
            content1.InnerText = dt.Rows[0]["tMemo"].ToString();
            //ftb.Text = dt.Rows[0]["tMemo"].ToString();                                          //内容
            txtPubtime.Text = dt.Rows[0]["dtPubTime"].ToString();                               //发布时间
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
        bool success = false;
        if (!FileUpload1.HasFile)
        {
            success = policies.PoliciesInsert(DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), content1.InnerText, hidSp.Value.Trim(), txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value, txtPubtime.Text.Trim());
        }
        else
        {
            UploadPhoto();
            success = policies.PoliciesInsert(DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), hid.Value, content1.InnerText, hidSp.Value.Trim(), txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value, txtPubtime.Text.Trim());
        }
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加完成！')</script>");
            //Initialization();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
        }
    }
    //修改事件
    public void SubmitUpd(string ID)
    {
        if (hidSp.Value.ToString() != "")
        {
            DeleteMoves(ID);//删除原有视频
        }
        bool success = false;
        if (!FileUpload1.HasFile)//是否需要上传图片
        {
            success = policies.PoliciesUpdate(ID, DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), content1.InnerText, hidSp.Value.Trim(), txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value);
        }
        else
        {
            DeletePhoto(ID);//删除原有图片
            UploadPhoto();//上传图片
            success = policies.PoliciesUpdate(ID, DropGroup.SelectedItem.Value, txtTlitle.Text.Trim(), hid.Value, content1.InnerText, hidSp.Value.Trim(), txtFromWhere.Text.Trim(), RadioState.SelectedItem.Value);
        }
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("PoliciesMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
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
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/MovesImg";   //项目根路径   
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
        SqlHelper her = new SqlHelper();
        string sql = "select tPic from  ML_Policies where nID=" + ID;
        string str = (string)her.ExecuteScalar(sql);
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/MovesImg";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }
    //删除原有视频
    public void DeleteMoves(string ID)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select tMemoPicList from  ML_Policies where nID=" + ID;
        string str = (string)her.ExecuteScalar(sql);
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../Upload/Moves";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }

    protected void imgSub_Click(object sender, EventArgs e)
    {
        if (DropGroup.SelectedItem.Value == "-1")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择所属分类！')</script>");
            return;
        }
        if (txtTlitle.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择视频标题！')</script>");
            return;
        }
        if (txtPubtime.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择发布时间！')</script>");
            return;
        }

        if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)
        {
            if (hidSp.Value == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频！')</script>");
                return;
            }
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
            Response.Redirect("PoliciesMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
    }
    //视频上传
    protected void Upload_Click(object sender, EventArgs e)
    {
        this.uploadPath.Text = "上传中，请勿作其他操作！";
        string FileName = this.AttachFile.FileName;//获取上传文件文件名 
        string ExtenName = System.IO.Path.GetExtension(FileName);//获取扩展名 
        // string SaveFileName = System.IO.Path.Combine(Request.PhysicalApplicationPath + "upload/file/", DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName);//合并两个路径为上传到服务器上的全路径
        string newName = DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName;
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../Upload/file/"), newName);//合并两个路径为上传到服务器上的全路径
        //string SaveFileName ="~/Upload/file/"+DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName;
        float FileSize = (float)System.Math.Round((float)AttachFile.ContentLength / 1024000, 1); //获取文件大小并保留小数点后一位,单位是M 
        //this.savePath.Text = "upload/file/" + newName;
        //this.theFileExt.Text = ExtenName;
        //this.filesize.Text = FileSize.ToString() + "M";
        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('上传成功:" + newName + "   大小：" + FileSize.ToString() + "M')</script>");
        hidSp.Value = newName;
        if (this.AttachFile.ContentLength > 0)
        {
            try
            {
                this.AttachFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        this.uploadPath.Text = "上传成功！";
    }
}