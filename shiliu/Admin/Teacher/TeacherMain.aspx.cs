﻿using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Teacher_TeacherMain : System.Web.UI.Page
{
    Teacher tc = new Teacher();
    protected void Page_Load(object sender, EventArgs e)
    {
        imgdelete.Attributes.Add("onclick", "return confirm('请谨慎操作，你确认删除本记录?执行本操作将是不可逆的!')");
        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            //DropGroup.Items.Add(new ListItem("请选择", "-1"));
            //BindDrop(DropGroup, "ML_VideoClass"); ;
            if (Request.QueryString["ceid"] != "" && Request.QueryString["ceid"] != null)
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
    public void BindDrop(DropDownList drop, string names)
    {
        SqlHelper her = new SqlHelper();
        string sql = "select nID,tClassName from " + names + " order by npaixu";
        DataTable dt = her.ExecuteDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            drop.Items.Add(new ListItem(dt.Rows[i]["tClassName"].ToString(), dt.Rows[i]["nID"].ToString()));
        }
    }

    public DataTable GetSource()
    {
        SqlHelper her = new SqlHelper();
        string sql = @"select * from T_Teacher where 1=1 ";
        if (keyName.Value.Trim() != "") { sql += " and teacherName like '%" + keyName.Value.Trim() + "%' "; }

        sql += " order by CreateTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }

    public void GridBind()
    {

        Pagination2.MDataTable = GetSource();
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
    protected void gridField_DataBound(object sender, EventArgs e)
    {

        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            Image img = gridField.Rows[i].FindControl("img1") as Image;
            img.ImageUrl = "../../upload_Img/TeacherImg/" + img.ImageUrl;

            Label lbtMemo = gridField.Rows[i].FindControl("lbvMemo") as Label;
            lbtMemo.Text = StringDelHTML.Centers(StringDelHTML.DelHTML(lbtMemo.Text), 30);

        }
    }
    protected void gridField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            bool success = tc.DeleteTeacherByID(e.CommandArgument.ToString());
            if (success)
            {
                GridBind();
                Pagination2.Refresh();
            }
        }
        if (e.CommandName == "update")
        {
            Response.Redirect("TeacherEdit.aspx?id=" + e.CommandArgument.ToString() + "&ceid=" + gridField.PageIndex);
        }

    }
    protected void btnSearh_Click(object sender, EventArgs e)
    {
        GridBind();
        Pagination2.Refresh();
    }
    protected void imgdelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridField.Rows.Count; i++)
        {
            CheckBox ckb = (CheckBox)gridField.Rows[i].FindControl("CheckSel");
            if (ckb.Checked)
            {
                bool success = tc.DeleteTeacherByID(gridField.DataKeys[i].Value.ToString());
                if (!success)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('发生未知错误！请重试')</script>");
                }
            }
        }
        GridBind();
        Pagination2.Refresh();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherEdit.aspx?ceid=" + gridField.PageIndex);
    }

    protected string ConvertName(bool state)
    {
        if (state.ToString() == bool.TrueString)
        {
            return "是";
        }
        else
        {
            return "否";
        }
    }

    /// <summary>
    /// 重写了这个，什么都不用干，就能解决导出时 类型“GridView”的控件 必须放在具有 runat。。。的错误
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btn_Expot_Click(object sender, EventArgs e)
    {

    }

}