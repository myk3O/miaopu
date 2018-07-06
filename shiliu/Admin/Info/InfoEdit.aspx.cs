using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Info_InfoEdit : System.Web.UI.Page
{
     InfoHelper info = new InfoHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            DropGroup.Items.Add(new ListItem("请选择", "-1"));
            BindDrop(DropGroup, "ML_InfoClass");
           // DropClass.Items.Add(new ListItem("请选择", "-1"));
            Initialization();
            BindDrop(DropClass, "ML_InfoClassMain");
            imgback.Attributes["onclick"] = "vbscript:history.back()";
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                imgback.Visible = true;
                Interface(Request.QueryString["id"].ToString());
            } 
        }
       
    }

    //绑定下拉菜单
    public void BindDrop(DropDownList drop, string names)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from " + names;
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
        txtTitle.Text = "";
        content1.InnerText = "";
        //ftb.Text = "";
        txtnum.Text = "1";
        imgback.Visible = false;
    }
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        SqlHelper her = new SqlHelper();
        DataTable dt = info.getMang(id);
        if (dt.Rows.Count > 0)
        {
            DropClass.SelectedValue = dt.Rows[0]["ClassSid"].ToString();
            content1.InnerText = dt.Rows[0]["tMemo"].ToString();
            txtTitle.Text = dt.Rows[0]["tTitle"].ToString();
            string sql = "select * from dbo.ML_InfoClass where sid0=" + dt.Rows[0]["ClassSid"].ToString();
            DataTable dts = her.ExecuteDataTable(sql);
            DropGroup.Items.Clear();
            for (int i = 0; i < dts.Rows.Count; i++)
            {
                DropGroup.Items.Add(new ListItem(dts.Rows[i]["tClassName"].ToString(), dts.Rows[i]["nID"].ToString()));
            }
            DropGroup.SelectedValue = dt.Rows[0]["tClassID"].ToString();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }
    //添加事件
    public void SubmitAdd()
    {
        bool success = info.InfoInsert(DropGroup.SelectedItem.Value,txtTitle.Text.Trim(), content1.InnerText, txtnum.Text.Trim());
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
        bool success = info.InfoUpdate(ID, DropGroup.SelectedItem.Value,txtTitle.Text.Trim(), content1.InnerText, txtnum.Text.Trim());
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("InfoMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改失败！')</script>");
        }
    }
    protected void imgSub_Click(object sender, EventArgs e)
    {
        if (DropGroup.SelectedItem.Value == "-1")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择所属分类！')</script>");
            return;
        }
        if (txtTitle.Text == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('请选择内容标题script>");
            return;
        }
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
        Response.Redirect("InfoMain.aspx");
    }
    protected void DropClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select * from dbo.ML_InfoClass where sid0=" + DropClass.SelectedItem.Value;
        DataTable dt = her.ExecuteDataTable(sql);
        DropGroup.Items.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DropGroup.Items.Add(new ListItem(dt.Rows[i]["tClassName"].ToString(), dt.Rows[i]["nID"].ToString()));
        }
    }
}