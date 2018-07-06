using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;

public partial class Admin_Member_MemberEdit : System.Web.UI.Page
{
    MemberHelper Member = new MemberHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["AdminName"] == null) { Response.Redirect("../../Error.aspx"); }
        if (!IsPostBack)
        {
            //U_ShowDivPic(this.Page, "正在导入中…");
            Initialization();
            //BindDrop(dropGroup, "ML_MemberClass");
            imgs.Attributes["onclick"] = "vbscript:history.back()";
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                imgs.Visible = true;
                lbltishi3.Text = "";
                Interface(Request.QueryString["id"].ToString());
            }
        }
    }
    //界面初始化
    public void Initialization()
    {
        txtRealname.Text = "";
        txtPhone.Text = "";
        txtStudy.Text = "";
        txtName.Text = "";
        txtClass.Text = "";
        txtMark.Text = "";
        txtpass.Attributes.Add("value", "123456");
        txtpassagion.Attributes.Add("value", "123456");
        ckb.Checked = true;
        lbltishi3.Text = "默认密码：123456";
        imgs.Visible = false;
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
    //根据ID查询信息并加载到界面
    public void Interface(string id)
    {
        DataTable dt = new DataTable();
        if (dt.Rows.Count > 0)
        {
            //  dropGroup.SelectedValue = dt.Rows[0]["className"].ToString();
            txtPhone.Text = dt.Rows[0]["MemberPhone"].ToString();
            txtStudy.Text = dt.Rows[0]["MCompany"].ToString();
            txtClass.Text = dt.Rows[0]["MBusiness"].ToString();
            txtName.Text = dt.Rows[0]["MemberName"].ToString();
            txtpass.Text = dt.Rows[0]["MemberPass"].ToString();
            txtpassagion.Text = dt.Rows[0]["MemberPass"].ToString();
            txtRealname.Text = dt.Rows[0]["tRealName"].ToString();
            txtMark.Text = dt.Rows[0]["tRealName"].ToString();
            if (dt.Rows[0]["oCheck"].ToString() == "True") { ckb.Checked = true; } else { ckb.Checked = false; }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('获取数据失败！')</script>");
        }
    }
    //添加事件
    public void SubmitAdd()
    {
        if (txtRealname.Text.Trim() == "")
        {
            lbRealname.Text = "姓名不能为空！";
            return;
        }
        else
        {
            lbRealname.Text = "";
        }
        if (txtPhone.Text.Trim() == "")
        {
            lbPhone.Text = "手机号不能为空！";
            return;
        }
        else
        {
            lbPhone.Text = "";
        }
        if (Member.Verification(txtPhone.Text.Trim()))
        {
            lbPhone.Text = "手机号已被注册，请重新输入！";
            return;
        }
        else
        {
            lbPhone.Text = "";
        }
        int check = 0;
        if (ckb.Checked) { check = 1; }
        bool success = false;
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
        if (txtRealname.Text.Trim() == "")
        {
            lbRealname.Text = "姓名不能为空！";
            return;
        }
        else
        {
            lbRealname.Text = "";
        }
        if (txtPhone.Text.Trim() == "")
        {
            lbPhone.Text = "手机号不能为空！";
            return;
        }
        else
        {
            lbPhone.Text = "";
        }
        if (Member.Verification(txtPhone.Text.Trim()))
        {
            lbPhone.Text = "手机号已被注册，请重新输入！";
            return;
        }
        else
        {
            lbPhone.Text = "";
        }
        int check = 0; bool success = false;
        if (ckb.Checked) { check = 1; }
        if (Member.MemberSelPass(ID) == txtpass.Text.Trim())
        {
            //success = Member.MemberUpdate(ID, txtName.Text.Trim(), txtStudy.Text.Trim(), txtClass.Text.Trim(),
           // txtPhone.Text.Trim(), txtpass.Text.Trim(), txtRealname.Text.Trim(), check, txtMark.Text.Trim(), txt_feiyong.Text.Trim());
        }
        else
        {
            //success = Member.MemberUpdate(ID, txtName.Text.Trim(), txtStudy.Text.Trim(), txtClass.Text.Trim(),
            //txtPhone.Text.Trim(), EntityUtils.StringToMD5(txtpass.Text.Trim(), 32), txtRealname.Text.Trim(), check, txtMark.Text.Trim(), txt_feiyong.Text.Trim());
        }
        if (success)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('修改完成！')</script>");
            Response.Redirect("MemberMain.aspx?ceid=" + Request.QueryString["ceid"].ToString());
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
    protected void Submit_Click(object sender, ImageClickEventArgs e)
    {
        if (txtRealname.Text == "" || txtRealname.Text == null) { return; }
        if (txtpass.Text == "" || txtpassagion.Text == "" || txtpass.Text != txtpassagion.Text) { return; }
        if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)
        {
            SubmitAdd();
        }
        else
        {
            SubmitUpd(Request.QueryString["id"].ToString());
        }
    }

    #region  批量导入用户
    SqlHelper hp = new SqlHelper();
    int allCount = 0, errorCount = 0, successCount = 0, missCount = 0;

    protected void btnUpload_Click(object sender, EventArgs e)
    {

        //确保已经选择了待导入的文件，首先上传，然后在服务器端完成导入
        if (this.fuOpen.PostedFile.FileName != "")
        {
            //确保文件是excel格式
            //Response.Write(this.fuOpen.FileName.Substring(this.fuOpen.FileName.LastIndexOf('.')));
            if (this.fuOpen.FileName.Substring(this.fuOpen.FileName.LastIndexOf('.') + 1) == "xls")
            {
                Random rd = new Random(1);

                string filename = DateTime.Now.Date.ToString("yyyymmdd") + DateTime.Now.ToLongTimeString().Replace(":", "") + rd.Next(9999).ToString() + ".xls";


                try
                {
                    this.fuOpen.PostedFile.SaveAs(@Server.MapPath("../Upload/file/") + filename);
                }
                catch (HttpException he)
                {
                    Response.Write("文件上传不成功，请检查文件是否过大，是否有写权限！");
                    return;
                }
                #region --------读取文件内容到服务器内存----------
                string conn = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source =" + Server.MapPath("../Upload/file") + "/" + filename + ";Extended Properties=Excel 8.0";
                OleDbConnection thisconnection = new OleDbConnection(conn);
                thisconnection.Open();
                //要保证字段名和excel表中的字段名相同
                string Sql = @"select 姓名,学校,年级,班级,联系电话,家长姓名,备注 from [Sheet1$]";
                OleDbDataAdapter mycommand = new OleDbDataAdapter(Sql, thisconnection);
                DataSet ds = new DataSet();
                mycommand.Fill(ds, "[Sheet1$]");
                thisconnection.Close();

                #endregion


                allCount = ds.Tables[0].Rows.Count;
                U_ShowDivPic(this.Page, "正在导入中…");
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    string exception = "";
                //    if (string.IsNullOrEmpty(dr["联系电话"].ToString()))
                //    {
                //        missCount++;
                //    }
                //    else
                //    {
                //        // string sql = "select count(*) from ML_Member where MemberPhone=" + dr["手机号码"].ToString();
                //        // if (hp.ExecuteScalar(sql) != null && Convert.ToInt32(hp.ExecuteScalar(sql)) > 0)//已经注册的用户不需要再添
                //        if (Member.Verification(dr["联系电话"].ToString()))
                //        {
                //            exception = "该号码已被注册";
                //            missCount++;
                //            continue;
                //        }
                //        else
                //        {
                //            //导入用户
                //            InsertPerson(dr, out exception);//exception可以考虑写入日志
                //        }
                //    }
                //}

                this.Page.Response.Write("<script language=javascript>;");
                this.Page.Response.Write("HideWait();</script>");
                ClientScript.RegisterStartupScript(GetType(), "", string.Format("<script>alert('总共{0}条数据，成功{1}，失败{2}，过滤{3}')</script>", allCount, successCount, errorCount, missCount));

            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "导入文件的格式不正确！");
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "您还没有选择要导入的文件！");
        }

    }


    /// <summary>
    /// 插入用户信息
    /// </summary>
    /// <param name="context"></param>
    public void InsertPerson(DataRow dr, out string result)
    {
        if (string.IsNullOrEmpty(dr["联系电话"].ToString()))
        {
            result = "空数据";
        }
        else
        {
            try
            {
                if (false)
                {
                    successCount++;
                    result = "注册成功";
                }
                else
                {
                    errorCount++;
                    result = "注册失败";
                }

            }
            catch (Exception ex)
            {
                errorCount++;
                result = ex.Message;
            }
        }
    }
    #endregion

    /// <summary>
    /// 显示处理等待层-图片
    /// </summary>
    /// <param name="show_str"></param>
    /// <returns></returns>
    public void U_ShowDivPic(System.Web.UI.Page page, string show_str)
    {
        page.Response.Write("<STYLE type=text/css>");
        page.Response.Write("..p9 {FONT-FAMILY: 宋体; FONT-SIZE: 9pt; COLOR: #ffffff; TEXT-DECORATION: none}");
        page.Response.Write("..content01 {COLOR: #444444; FONT-SIZE: 12px; LINE-HEIGHT: 20px; TEXT-DECORATION: none}");
        page.Response.Write("A:hover { color:#666666; TEXT-DECORATION: underline}");
        page.Response.Write("</STYLE>");

        page.Response.Write("<div id='mydiv' style='DISPLAY: inline; Z-INDEX: 102; POSITION: relative;TOP: 80px' ms_positioning='FlowLayout'>");
        page.Response.Write("<br>");
        page.Response.Write("<table ALIGN=CENTER BORDER='0' WIDTH='100%' CELLSPACING='0' CELLPADDING='0' style='z-index: 1111; position: absolute; margin:0 auto;left:50%'>");

        page.Response.Write("<tr><td align='center' class='content01'><IMG SRC='../Upload/wait.gif' ></td></tr>");
        page.Response.Write("<tr><td align='center' class='content01'><b><font color='#ccc'>" + show_str + "</font></b></td></tr>"); // 字符串参数
        page.Response.Write("</table>");
        page.Response.Write("");
        page.Response.Write("</div>");

        page.Response.Write("<script language=javascript>;");
        page.Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; }");
        page.Response.Write("function HideWait(){mydiv.style.visibility = 'hidden';}");
        page.Response.Write("StartShowWait();</script>");
        page.Response.Flush();
    }
}