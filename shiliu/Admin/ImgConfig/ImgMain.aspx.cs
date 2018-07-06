using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maliang;

public partial class Admin_ImgConfig_ImgMain : System.Web.UI.Page
{
    SqlHelper her = new SqlHelper();
    WebHelper web = new WebHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (!IsPostBack)
        {
            BindDrop(DropGroup);
            DropGroup.Items.Insert(0, new ListItem("请选择", "-1"));
            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
            {
                hid.Value = Request.QueryString["ceid"].ToString();
                string aa = Request.QueryString["ceid"].ToString();
            }
        }
        GridBind();
        if (hid.Value != "")
        {
            gridField.PageIndex = int.Parse(hid.Value);
            hid.Value = "";
        }

    }

    //绑定下拉框
    public void BindDrop(DropDownList drop)
    {
        string sql = "select * from ML_ImageClass";
        drop.DataSource = her.ExecuteDataTable(sql);
        drop.DataValueField = "nID";
        drop.DataTextField = "tClassName";
        drop.DataBind();
    }
    protected void DropState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropGroup.SelectedItem.Value == "-1")
        {
            DropGroup.Items.Clear();
            DropGroup.Items.Insert(0, new ListItem("请选择", "-1"));
        }
        else
        {
            string sql = "select * from ML_ImageClass where nID=" + DropGroup.SelectedItem.Value;
            DropGroup.DataSource = her.ExecuteDataTable(sql);
            DropGroup.DataValueField = "nID";
            DropGroup.DataTextField = "tClassName";
            DropGroup.DataBind();
            DropGroup.Items.Insert(0, new ListItem("请选择", "-1"));
        }
    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    public void GridBind()
    {
        DataTable dt = new DataTable();
        if (DropGroup.SelectedItem.Value == "-1")
        {
            dt = web.SelImg();
        }
        else
        {
            dt = web.SelImg(DropGroup.SelectedItem.Value);
        }
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (dt.Rows[i]["dtAddTime"].ToString() != "" && dt.Rows[i]["dtAddTime"] != null)
        //    {
        //        dt.Rows[i]["dtAddTime"] = Convert.ToDateTime(dt.Rows[i]["dtAddTime"]).ToString("yyyy-MM-dd");
        //    }
        //}
        Pagination2.MDataTable = dt;
        Pagination2.MGridView = gridField;
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
        if (e.CommandName == "del")
        {
            DeletePhoto(e.CommandArgument.ToString());
            bool success = web.DelImg(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            int aa = gridField.PageIndex;
            Response.Redirect("ImgEdit.aspx?id=" + e.CommandArgument.ToString() + "&&ceid=" + gridField.PageIndex);
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ImgEdit.aspx?ceid=" + gridField.PageIndex);
    }

    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = web.DelImg(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination2.Refresh();
    }

    protected void gridField_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Image img = gridField.Rows[i].FindControl("img1") as Image;
            img.ImageUrl = "../upload_Img/Logo_Img/" + img.ImageUrl;
        }
    }
}