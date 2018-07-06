using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Admin_Teacher_TeacherEdit : System.Web.UI.Page
{
    public string picTou;
    SqlHelper her = new SqlHelper();
    Teacher tc = new Teacher();

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
            //DropGroup.Items.Add(new ListItem("请选择", "-1"));
            // BindDrop(DropGroup, "ML_VideoClass");
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

        txtPubtime.Text = "";

        imgs.Visible = false;

        txtname.Text = "";
        txtjob.Text = "";
        txttMemo.InnerText = "";
        hidTou.Value = "";
    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        StringBuilder sb = new StringBuilder();

        DataTable dt = tc.GetTeacherByID(id);
        if (dt.Rows.Count > 0)
        {
            hidTou.Value = dt.Rows[0]["teacherImg"].ToString();//老师头像
            purl = dt.Rows[0]["teacherImg"].ToString();//图片地址
            picTou = "<img id='viewImg2' src='../../upload_Img/TeacherImg/" + hidTou.Value + "' style='width: 124px; height: 100px; float: left;'>";


            txtPubtime.Text = dt.Rows[0]["CreateTime"].ToString();

            txtname.Text = dt.Rows[0]["teacherName"] == null ? "" : dt.Rows[0]["teacherName"].ToString();
            txtjob.Text = dt.Rows[0]["teacherdiscrib"] == null ? "" : dt.Rows[0]["teacherdiscrib"].ToString();
            txttMemo.InnerText = dt.Rows[0]["teacherMemo"] == null ? "" : dt.Rows[0]["teacherMemo"].ToString();


        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }
    //添加事件
    public void SubmitAdd()
    {
        UploadPhoto(viewFiles2, hidTou);
        bool success = false;
        if (hidTou.Value == "")//没有图片
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传视频封面')</script>");
            return;
        }
        success = tc.InsertTeacher(txtPubtime.Text.Trim(), txtname.Text.Trim(), hidTou.Value, txtjob.Text.Trim(), txttMemo.InnerText);

        if (success)
        {
            Response.Redirect("TeacherMain.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加失败！')</script>");
        }
    }
    //修改事件
    public void SubmitUpd(string ID)
    {
        //DeletePhoto(ID);
        UploadPhoto(viewFiles2, hidTou);
        bool success = false;
        if (hidTou.Value == "")//没有图片
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请上传头像')</script>");
            return;
        }
        else
        {
            if (!hidTou.Equals(purl))//不相等就等于更新了图片，那么删除旧图片
            {
                DeletePhoto(ID);
            }
        }
        success = tc.UpdateTeacher(ID, txtPubtime.Text.Trim(), txtname.Text.Trim(), hidTou.Value, txtjob.Text.Trim(), txttMemo.InnerText);


        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("TeacherMain.aspx");
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
            Response.Redirect("TeacherMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            Response.Redirect("TeacherMain.aspx");
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
            string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/TeacherImg/";   //项目根路径   
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
            string strPath = HttpContext.Current.Request.FilePath + "/../../../upload_Img/TeacherImg/";   //项目根路径   
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
        string sql = "select teacherImg from T_Teacher where nID=" + nid;
        string strPic = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        return strPic;
    }
}