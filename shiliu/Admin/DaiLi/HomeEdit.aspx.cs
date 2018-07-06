
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Model;


using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.Text;

public partial class Admin_DaiLi_HomeEdit : System.Web.UI.Page
{
    DaiLi makbll = new DaiLi();
    // IHomeMakBLL makbll = BLLFactory.CreateHomeMakBLL();
    //Transformation tran = new Transformation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            BindDrop(dropSheng, "ML_CityClass", " where sid0 = 0");
            BindDropHomeSystem(dropDailiSystem, "ML_HomeMakingClass", " where oHide = 0");
            dropDailiSystem.Items.Insert(0, new ListItem("请选择", "-1"));
            dropFatherDaili.Items.Insert(0, new ListItem("请选择", "-1"));
            dropSheng.Items.Insert(0, new ListItem("请选择", "-1"));
            dropShi.Items.Insert(0, new ListItem("请选择", "-1"));
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                GridBind(Request.QueryString["id"].ToString());
            }
        }
    }
    //绑定GridView
    public void GridBind(string id)
    {
        DataTable dt = makbll.MakSelectID(id);
        if (dt.Rows.Count > 0)
        {
            dropSheng.SelectedValue = makbll.CitynID(dt.Rows[0]["HomePro"].ToString(), " and sid0=0").ToString();
            if (dropSheng.SelectedItem.Value != "-1")
            {
                BindDrop(dropShi, "ML_CityClass", string.Format(@" where sid0 = {0}", dropSheng.SelectedItem.Value));
                dropShi.Items.Insert(0, new ListItem("请选择", "-1"));
            }
            dropShi.SelectedValue = makbll.CitynID(dt.Rows[0]["HomeCity"].ToString(), " and sid0<>0").ToString();

            dropDailiSystem.SelectedValue = dt.Rows[0]["cid"].ToString();
            BindDropHomeMak(dropFatherDaili, "ML_HomeMaking", string.Format(@" where cid = {0}", Convert.ToInt32(dt.Rows[0]["cid"].ToString()) - 1));
            dropFatherDaili.SelectedValue = dt.Rows[0]["nLogNum"].ToString();
            txtCode.Value = dt.Rows[0]["HomeCode"].ToString();
            txtYouBian.Value = dt.Rows[0]["CreatorCode"].ToString();
            txtName.Value = dt.Rows[0]["HomeName"].ToString();
            txtPass.Attributes.Add("value", dt.Rows[0]["HomePass"].ToString());
            //txtPassAgan.Attributes.Add("value", dt.Rows[0]["HomePass"].ToString());
            txtCompany.Value = dt.Rows[0]["tRealName"].ToString();
            txtPhone.Value = dt.Rows[0]["HomePhone"].ToString();
            txtMebile.Value = dt.Rows[0]["HomeMobile"].ToString();
            txtEmail.Value = dt.Rows[0]["HomeEmail"].ToString();
            txtAddress.Value = dt.Rows[0]["HomeIntro"].ToString();
            if (dt.Rows[0]["oCheck"].ToString() == "True") { ckb.Checked = true; } else { ckb.Checked = false; }
            hid.Value = dt.Rows[0]["nID"].ToString();
            txtlxr.Value = dt.Rows[0]["HomeIntegral"].ToString();//联系人
            hid1.Value = dt.Rows[0]["HomePass"].ToString();
            hid2.Value = dt.Rows[0]["HomeName"].ToString();
            content1.InnerText = dt.Rows[0]["tMemo"].ToString();  //内容
            hideLng.Value = dt.Rows[0]["Lng"].ToString();
            hideLat.Value = dt.Rows[0]["Lat"].ToString();
            hid3.Value = dt.Rows[0]["tPic"].ToString();
        }
    }
    //初始化界面
    public void Initialization()
    {
        dropSheng.SelectedValue = "-1";
        dropShi.SelectedValue = "-1";
        txtCode.Value = "";
        txtName.Value = "";
        txtPass.Attributes.Add("value", "");
        txtPassAgan.Attributes.Add("value", "");
        txtCompany.Value = "";
        txtPhone.Value = "";
        txtMebile.Value = "";
        txtEmail.Value = "";
        txtAddress.Value = "";
        content1.InnerText = "";
        ckb.Checked = false;
        content1.InnerText = "";  //内容
        hideLng.Value = "";
        hideLat.Value = "";
        hid3.Value = "";
    }
    //绑定下拉框
    public void BindDrop(DropDownList drop, string names, string where)
    {
        // IAdminBLL adminbll = BLLFactory.CreateAdminBLL();
        // DataTable dt = adminbll.AdminBind(names, where);
        drop.DataSource = makbll.SityBind(names, where);
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
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
        string strPath = HttpContext.Current.Request.FilePath + "/../../Upload";   //项目根路径   
        string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        DeleteOldAttach(fullname);
        FileUpload1.PostedFile.SaveAs(fullname);
        hid3.Value = filename;
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
        DataTable dt = makbll.MakSelectID(ID);
        if (dt.Rows.Count > 0)
        {
            str = dt.Rows[0]["tPic"].ToString();
        }
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../Upload/Making";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }
    protected void btnSub_Click(object sender, EventArgs e)
    {
        if (txtName.Value == "")
        {
            return;
        }
        if (txtPass.Text.Trim() == "")
        {
            return;
        }
        if (hid.Value == "")//nID
        {
            Addmak();
        }
        else
        {
            updatemak(hid.Value);
        }
    }
    public void Addmak()
    {
        //if (hideLng.Value == "" || hideLat.Value == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('未获取位置信息')</script>");
        //    return;
        //}
        bool cuss = makbll.SelMakName(" where HomeName='" + txtName.Value.Trim() + "'");
        if (cuss)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('登录名已存在')</script>");
            return;
        }
        if (FileUpload1.HasFile)
        {
            UploadPhoto();//图片不是必填
        }
        HomeMakInfo homemak = new HomeMakInfo();
        homemak.HomePro = dropSheng.SelectedItem.Text == "请选择" ? "" : dropSheng.SelectedItem.Text;
        homemak.HomeCity = dropShi.SelectedItem.Text == "请选择" ? "" : dropShi.SelectedItem.Text;
        homemak.HomeCode = txtCode.Value.Trim();
        homemak.CreatorCode = txtYouBian.Value.Trim();//youbian

        homemak.TPic = hid3.Value;
        homemak.TMemo = content1.InnerText;
        homemak.Lng = Convert.ToDecimal(hideLng.Value == "" ? "0" : hideLng.Value);
        homemak.Lat = Convert.ToDecimal(hideLat.Value == "" ? "0" : hideLat.Value);
        homemak.HomeName = txtName.Value.Trim();
        homemak.HomePass = txtPass.Text.Trim();
        homemak.tRealName = txtCompany.Value.Trim();
        homemak.HomePhone = txtPhone.Value.Trim();
        homemak.HomeMobile = txtMebile.Value.Trim();
        homemak.HomeMobileYz = "0";
        homemak.HomeEmail = txtEmail.Value.Trim();
        homemak.HomePic = "";
        homemak.HomeIntro = txtAddress.Value.Trim();
        homemak.HomeIntegral = txtlxr.Value.Trim();//联系人;
        homemak.CreatorName = "";
        //homemak.CreatorCode = "";
        homemak.nLogNum = Convert.ToInt32(dropFatherDaili.SelectedItem.Value); //上级代理id
        if (ckb.Checked) { homemak.oCheck = "1"; }
        else { homemak.oCheck = "0"; }
        homemak.oHide = "1"; // 不是总部
        homemak.tLastIP = "";
        homemak.cid = dropDailiSystem.SelectedItem.Value;//代理等级
        homemak.dtAddTime = DateTime.Now.ToString();
        homemak.dtLastTime = DateTime.Now.ToString();
        // homemak.TPic=
        bool success = makbll.MakInsert(homemak);
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加完成')</script>");
            Initialization();
        }
    }
    public void updatemak(string id)
    {
        //if (hideLng.Value == "" || hideLat.Value == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('未获取位置信息')</script>");
        //    return;
        //}
        if (hid2.Value != txtName.Value.Trim())
        {
            bool cuss = makbll.SelMakName(" where HomeName='" + txtName.Value.Trim() + "'");
            if (cuss)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('登录名已存在')</script>");
                return;
            }
        }
        if (FileUpload1.HasFile)
        {
            UploadPhoto();
            DeletePhoto(hid.Value);
        }
        HomeMakInfo homemak = new HomeMakInfo();
        homemak.nID = int.Parse(id);
        homemak.TPic = hid3.Value;
        homemak.TMemo = content1.InnerText;
        homemak.Lng = Convert.ToDecimal(hideLng.Value == "" ? "0" : hideLng.Value);
        homemak.Lat = Convert.ToDecimal(hideLat.Value == "" ? "0" : hideLat.Value);

        homemak.HomePro = dropSheng.SelectedItem.Text == "请选择" ? "" : dropSheng.SelectedItem.Text;
        homemak.HomeCity = dropShi.SelectedItem.Text == "请选择" ? "" : dropShi.SelectedItem.Text;
        homemak.HomeCode = txtCode.Value.Trim();

        homemak.HomeName = txtName.Value.Trim();
        if (hid1.Value == txtPass.Text.Trim())
        { homemak.HomePass = txtPass.Text.Trim(); }
        else { homemak.HomePass = txtPass.Text.Trim(); }
        homemak.tRealName = txtCompany.Value.Trim();
        homemak.HomePhone = txtPhone.Value.Trim();
        homemak.HomeMobile = txtMebile.Value.Trim();
        homemak.HomeEmail = txtEmail.Value.Trim();
        homemak.HomeIntro = txtAddress.Value.Trim();
        if (ckb.Checked) { homemak.oCheck = "1"; }
        else { homemak.oCheck = "0"; }

        homemak.HomeIntegral = txtlxr.Value.Trim();//联系人;
        homemak.cid = "3";//代理等级
        homemak.nLogNum = 1; //上级代理id
        homemak.CreatorCode = txtYouBian.Value.Trim();//youbian
        bool success = makbll.MakUpdate(homemak);
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成')</script>");
            //Response.Redirect("HomeMakMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Response.Redirect("HomeMakMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
    }
    protected void dropSheng_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropSheng.SelectedItem.Value == "-1")
        {
            dropShi.Items.Clear();
            dropShi.Items.Insert(0, new ListItem("请选择", "-1"));
        }
        else
        {
            BindDrop(dropShi, "ML_CityClass", string.Format(@" where sid0 = {0}", dropSheng.SelectedItem.Value));
            dropShi.Items.Insert(0, new ListItem("请选择", "-1"));
            if (hid.Value == "")
            {
                DataTable dt = makbll.MakSelHomeCode(dropSheng.SelectedItem.Text, "请选择");
                if (dt.Rows.Count > 0)
                {
                    txtCode.Value = (int.Parse(dt.Rows[0]["HomeCode"].ToString()) + 1).ToString();
                }
                else
                {
                    //IAdminBLL adminbll = BLLFactory.CreateAdminBLL();
                    DataTable dts = makbll.SityBind("ML_CityClass", string.Format(@" where sid0 =0 and tClassName='{0}'", dropSheng.SelectedItem.Text));
                    if (dts.Rows.Count > 0)
                    {
                        txtCode.Value = dts.Rows[0]["AreaCode"].ToString() + "0001";
                    }
                }
            }
        }
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
}