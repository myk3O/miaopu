using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maliang;
using ThoughtWorks.QRCode.Codec;

public partial class Admin_Members_Member_Main : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    MemberHelper Menber = new MemberHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        //imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            btnBack.Visible = false;

            BindDrop(drpUserLevel);
            drpUserLevel.Items.Insert(0, new ListItem("请选择", "-1", true));
            if (Request.QueryString["ceid"] != null && Request.QueryString["ceid"].ToString() != "")
            {
                hid.Value = Request.QueryString["ceid"].ToString();
            }

        }
        GridBind();
        if (hid.Value != "")
        {
            gridField.PageIndex = int.Parse(hid.Value);
            hid.Value = "";
        }
    }
    //绑定下拉菜单
    public void BindDrop(DropDownList drop)
    {
        string sql = "select nID,levelName from ML_MemberLevel";
        DataTable dt = her.ExecuteDataTable(sql);
        drop.DataSource = dt;
        drop.DataValueField = "nID";
        drop.DataTextField = "levelName";
        drop.DataBind();
    }

    public DataTable GetSource()
    {
        string sql = @"  select a.*,b.levelName from  ML_Member a left join ML_MemberLevel b on a.fxslevel=b.nID  where 1=1";
        if (nickName.Text.Trim() != "")
        {
            sql += " and a.nickname like '%" + nickName.Text.Trim() + "%'";
        }
        if (txtname.Text.Trim() != "")
        {
            sql += " and a.tRealName like '%" + txtname.Text.Trim() + "%'";
        }
        //if (DropisJXS.SelectedItem.Value != "-1")
        //{
        //    sql += " and isJXS=" + DropisJXS.SelectedItem.Value + " ";
        //}
        if (drpUserLevel.SelectedItem.Value != "-1")
        {
            sql += " and a.fxslevel=" + drpUserLevel.SelectedItem.Value + "";
        }

        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
        {
            sql += " and a.FatherFXSID=" + Request.QueryString["uid"].ToString() + " ";
            btnBack.Visible = true;
        }
        sql += " order by a.dtAddTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    public void GridBind()
    {
        Pagination1.MDataTable = GetSource();
        Pagination1.MGridView = gridField;
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = Menber.MemberDelete(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination1.Refresh();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Member_Main.aspx");
    }
    protected void ImgSrs_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination1.Refresh();
    }

    protected void gridField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor=\"#F6F9FA\"");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=\"" + e.Row.Style["BACKGROUND-COLOR"] + "\"");
        }
    }

    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "apply")
        {
            Response.Redirect("MyBranchMain.aspx?uid=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }
        if (e.CommandName == "dingdan")
        {
            Response.Redirect("../Order/OrderMain.aspx?uid=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
        }

    }

    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Label lbSex = (Label)gridField.Rows[i].FindControl("lbSex");
            if (lbSex.Text == "1")
            {
                lbSex.Text = "男";
            }
            else if (lbSex.Text == "2")
            {
                lbSex.Text = "女";
            }
            else
            {
                lbSex.Text = "未确定";
            }

            Label lbAllMoney = (Label)gridField.Rows[i].FindControl("lbAllMoney");

            lbAllMoney.Text = StringDelHTML.PriceToStringLow(Convert.ToInt32(lbAllMoney.Text));



            //Label lbisJXS = (Label)gridField.Rows[i].FindControl("lbisJXS");
            //if (lbisJXS.Text.ToLower() == "true")
            //{
            //    lbisJXS.Text = "是";
            //}
            //else
            //{
            //    lbisJXS.Text = "否";
            //}



        }
    }



    private string makeUrl(string nID)
    {
        //string url = HttpContext.Current.Request.Url.Host;
        string url = wdgl.GetLocalHostUrl();
        string aurl = url + "Wap/Index.aspx?agent=" + nID;
        return aurl;
    }
    private string ImageAdd(string str)
    {
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        qrCodeEncoder.QRCodeScale = 4;
        qrCodeEncoder.QRCodeVersion = 8;
        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        String data = str;
        //生成二维码
        System.Drawing.Bitmap image = qrCodeEncoder.Encode(data);
        //上传图片
        string filename = Guid.NewGuid().ToString() + ".png";
        string strPath = HttpContext.Current.Request.FilePath + "/../../Upload/pic";   //项目根路径   
        string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        //string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        //string strPath = HttpContext.Current.Request.FilePath + "/../skin/images";   //项目根路径   
        //string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        image.Save(fullname);
        return filename;
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
        string sql = "select fxsImg from ML_Member where nID=" + ID;
        str = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../Upload/pic";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }
}
