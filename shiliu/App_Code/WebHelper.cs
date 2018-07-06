using Maliang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// WebHelper 的摘要说明
/// </summary>
public class WebHelper
{
    public WebHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    DataTable dt = new DataTable();
    InfoHelper info = new InfoHelper();
    SqlHelper her = new SqlHelper();


    //在线留言
    public bool InsertMember(string titel, string tmemo, string RealName, string Email, string phone)
    {
        SqlParameter tTilte = new SqlParameter("@tTilte", titel);
        SqlParameter tMemo = new SqlParameter("@tMemo", tmemo);
        SqlParameter tRealName = new SqlParameter("@tRealName", RealName);
        SqlParameter tEmail = new SqlParameter("@tEmail", Email);
        SqlParameter tPhone = new SqlParameter("@tPhone", phone);
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", DateTime.Now);
        SqlParameter[] cont = { tTilte, tMemo, tRealName, tEmail, tPhone, dtAddTime };
        string sql = @"insert into dbo.ML_MemberMessage values (0,0,0,@tTilte,@tMemo,@tRealName,@tEmail,@tPhone,@dtAddTime,@dtAddTime,1)";
        if (her.ExecuteNonQuery(sql, cont))
        {
            return true;
        }
        return false;
    }
    //在线留言信息
    public DataTable SelMemMessage(string where)
    {
        string sql = @"select * from dbo.ML_MemberMessage ";
        sql += where;
        sql += " order by dtAddTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //删除在线留言
    public bool DelMessage(string where)
    {
        string sql = @"delete dbo.ML_MemberMessage where nID =" + where;
        bool succe = her.ExecuteNonQuery(sql);
        return succe;
    }


    #region News
    //新闻
    public DataTable Newsclass(string where)
    {
        string sql = @"select * from dbo.ML_NewsClass ";
        sql += where;
        sql += " order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //新闻
    public DataTable News(string where)
    {
        string sql = @"select ML_News.*, ML_NewsClass.nID as classID,ML_NewsClass.tClassName as className
                            from dbo.ML_News inner join dbo.ML_NewsClass on ML_News.cid0=ML_NewsClass.nID";
        sql += where;
        sql += " order by oNewest desc,dtAddTime desc";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    //新闻
    public int New(string where)
    {
        string sql = @"select ML_NewsClass.nID from dbo.ML_News inner join dbo.ML_NewsClass on ML_News.cid0=ML_NewsClass.nID";
        sql += where;
        sql += " order by oNewest desc,dtAddTime desc";
        int dt = (int)her.ExecuteScalar(sql);
        return dt;
    }



    //修改点击量
    public void UpdateNews(string ID, int hit)
    {
        string sql = "update ML_News set nHit=" + hit + " where nID=" + ID;
        her.ExecuteNonQuery(sql);
    }
    //修改点击量
    public void UpdatePolicy(string ID, int hit)
    {
        string sql = "update ML_Policies set nHit=" + hit + " where nID=" + ID;
        her.ExecuteNonQuery(sql);
    }
    //上一篇or下一篇
    public DataTable NewsShang(string where)
    {
        //string sql = "select top 1 * from ML_NewsSel";
        string sql = "select row_number() over(order by num desc) as row, * from ML_NewsSel " + where + "  order by oTop DESC";
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
    public int newsID(string id)
    {
        string sql = "select cid0 from ML_News where nID=" + id;
        int cid = (int)her.ExecuteScalar(sql);
        return cid;
    }
    public int PolicyID(string id)
    {
        string sql = "select cid0 from ML_Policies where nID=" + id;
        int cid = (int)her.ExecuteScalar(sql);
        return cid;
    }
    #endregion


    #region 动态图片
    //添加图片
    public bool addImg(string cID, string className, string paixun, string pic, string strUrl)
    {
        SqlParameter cid = new SqlParameter("@cID", cID);
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter strPic = new SqlParameter("@strPic", pic);
        SqlParameter url = new SqlParameter("@strUrl", strUrl);
        SqlParameter pubtime = new SqlParameter("@PubTime", System.DateTime.Now);
        SqlParameter[] count = { cid, names, paixus, strPic, url, pubtime };
        string sql = "insert into ML_Image values (@cID,@className,@strPic,@paixun,@strUrl,@PubTime)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除图片
    public bool DelImg(string nID)
    {
        string str = "delete ML_Image where id=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询图片
    public DataTable SelImg(string nID)
    {
        string str = "select * from ML_Image where id=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //查询图片
    public DataTable SelImg()
    {
        string str = "select a.*,b.tClassName from ML_Image  a join ML_ImageClass b on a.cID=b.nID order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //查询图片
    public DataTable SelImg(int cID)
    {
        string str = "select * from ML_Image where cID=" + cID + " order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改图片
    public bool updateImg(string nID, string cID, string className, string paixun, string strPic, string url)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter pic = new SqlParameter("@strPic", strPic);
        SqlParameter strurl = new SqlParameter("@strUrl", url);
        SqlParameter[] count = { names, paixus, pic, strurl };
        string str = "update ML_Image set tilte=@className,nPaiXu=@paixun,imgUrl=@strPic,tMemo=@strUrl where id=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    //修改图片
    public bool updateImg(string nID, string cID, string className, string paixun, string url)
    {
        SqlParameter names = new SqlParameter("@className", className);
        SqlParameter paixus = new SqlParameter("@paixun", paixun);
        SqlParameter strurl = new SqlParameter("@strUrl", url);
        SqlParameter[] count = { names, paixus, strurl };
        string str = "update ML_Image set tilte=@className,nPaiXu=@paixun,tMemo=@strUrl where id=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    #endregion


    #region 客服中心

    //添加
    public bool AddCustomer(string tilte, string paixun, string memo)
    {
        SqlParameter tiltes = new SqlParameter("@tilte", tilte);
        SqlParameter paixuns = new SqlParameter("@paixun", paixun);
        SqlParameter memos = new SqlParameter("@memo", memo);
        SqlParameter[] count = { tiltes, paixuns, memos };
        string sql = "insert into ML_CustomerConfig values (0,0,0,@tilte,'',@memo,@paixun,0)";
        return her.ExecuteNonQuery(sql, count);
    }
    //删除
    public bool DelCustomer(string nID)
    {
        string str = "delete ML_CustomerConfig where nID=" + nID;
        return her.ExecuteNonQuery(str);
    }
    //查询
    public DataTable SelCustomer(string nID)
    {
        string str = "select * from ML_CustomerConfig where nID=" + nID;
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //查询
    public DataTable SelCustomer()
    {
        string str = "select * from ML_CustomerConfig order by nPaiXu asc";
        DataTable dt = her.ExecuteDataTable(str);
        return dt;
    }
    //修改
    public bool updateCustomer(string nID, string tilte, string paixun, string memo)
    {
        SqlParameter tiltes = new SqlParameter("@tilte", tilte);
        SqlParameter paixuns = new SqlParameter("@paixun", paixun);
        SqlParameter memos = new SqlParameter("@memo", memo);
        SqlParameter[] count = { tiltes, paixuns, memos };
        string str = "update ML_CustomerConfig set tTilte=@tilte,tMemo=@memo,nPaiXu=@paixun where nID=" + nID;
        return her.ExecuteNonQuery(str, count);
    }
    #endregion


    #region 预约量房

    //插入预约量房
    public bool AppointMent(string name, string tel, string title, string area, string budget, string sytle)
    {
        bool success = false;
        SqlParameter tUserName = new SqlParameter("@tUserName", name);
        SqlParameter tTel = new SqlParameter("@tTel", tel);
        SqlParameter tTitle = new SqlParameter("@tTitle", title);
        SqlParameter tArea = new SqlParameter("@tArea", area);
        SqlParameter tBudget = new SqlParameter("@tBudget", budget);
        SqlParameter tStyle = new SqlParameter("@tStyle", sytle);
        SqlParameter dtAddTime = new SqlParameter("@dtAddTime", DateTime.Now);
        SqlParameter[] count = { tUserName, tTel, tTitle, tArea, tBudget, tStyle, dtAddTime };
        string sql = @"insert into dbo.ML_Message (cid0,tUserName,tTitle,tSex,tTel,tTelTime,tFromWhere,tDistrict,tBudget,tStyle,tHouseStyle0,tHouseStyle1,tHouseStyle2,tArea,tMemo,oHide,dtEditTime,dtAddTime)
                        values(0,@tUserName,@tTitle,'',@tTel,'','','',@tBudget,@tStyle,'','','',@tArea,'',0,@dtAddTime,@dtAddTime)";
        success = her.ExecuteNonQuery(sql, count);
        if (success)
        {
            return true;
        }
        return false;
    }
    //发送预约量房邮件
    public void AppointMentSendEamil(string name, string tel, string title, string area, string budget, string sytle)
    {
        AdminManagHelper admin = new AdminManagHelper();
        string shengri = "预约信息：<br />";
        shengri += "<div style=\"float:left\">姓  名：" + name + "</div><div style=\"margin-left:2em;float:left\">电  话：" + tel + "</div><div style=\"margin-left:2em;float:left\">楼盘名称：" + title + "</div><div style=\"margin-left:2em;float:left\">面　积：" + area + "</div><div style=\"margin-left:2em;float:left\">预算：" + budget + "</div><div style=\"margin-left:2em;float:left\">风格：" + sytle + "</div><br />";
        DataTable dt = admin.WebsiteInformation();
        if (dt.Rows.Count > 0)
        {
            //SendEmail.SendEmails(dt.Rows[0]["EFemail"].ToString(), dt.Rows[0]["EFname"].ToString(), dt.Rows[0]["EFpass"].ToString(), dt.Rows[0]["ESemail"].ToString(), shengri, dt.Rows[0]["Ename"].ToString());
            SendEmail.sendmail(dt.Rows[0]["EFemail"].ToString(), dt.Rows[0]["EFname"].ToString(), dt.Rows[0]["EFpass"].ToString(), dt.Rows[0]["ESemail"].ToString(), shengri, dt.Rows[0]["Ename"].ToString(), dt.Rows[0]["Estmp"].ToString());

        }
    }


    #endregion


    #region 视频

    //作品展示列表
    public DataTable PoliSelect(string where)
    {
        string sql = "select * from dbo.ML_PoliciesClass";
        sql += where;
        sql += " order by nPaixu desc";
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    //作品展示列表
    public DataTable PoliSelectMemo(string where)
    {
        string sql = "select * from dbo.ML_Policies inner join dbo.ML_PoliciesClass on ML_Policies.cid0 = ML_PoliciesClass.nID";
        sql += where;
        sql += " order by ML_PoliciesClass.nPaixu desc";
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    //作品展示查询
    public DataTable PoliciesSelect(string where)
    {
        string sql = "select * from dbo.ML_Policies";
        sql += where;
        sql += " order by oTop desc,oNewest desc, nHit desc,dtAddTime desc";
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }



    //培训课程列表
    public DataTable SerSelect(string where)
    {
        string sql = "select * from dbo.ML_ServiceAreaClass";
        sql += where;
        sql += " order by nPaixu desc";
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    //作品展示列表
    public DataTable SerSelectMemo(string where)
    {
        string sql = "select * from dbo.ML_ServiceArea inner join dbo.ML_ServiceAreaClass on ML_ServiceArea.cid0 = ML_ServiceAreaClass.nID";
        sql += where;
        sql += " order by ML_ServiceAreaClass.nPaixu desc,ML_ServiceArea.oTop desc";
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    //培训课程查询
    public DataTable ServceSelect(string where)
    {
        string sql = "select * from dbo.ML_ServiceArea";
        sql += where;
        sql += " order by oTop desc,oNewest desc, nHit desc,dtAddTime desc";
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }

    #endregion


    #region 企业信息
    //获取第一条企业信息
    public int GetInfoClass(string where)
    {
        int num = 0;
        string sql = string.Format(@"select top 1 ML_InfoClass.nID from dbo.ML_InfoClass inner join dbo.ML_InfoClassMain on ML_InfoClass.sid0 = ML_InfoClassMain.nID where ML_InfoClassMain.tClassName ='{0}' order by ML_InfoClass.nPaiXu asc", where);
        try { num = (int)her.ExecuteScalar(sql); }
        catch { }
        return num;
    }
    //导航
    public DataTable InfoTilte(string where)
    {
        string sql = string.Format(@"select * from dbo.ML_InfoClass inner join ML_InfoClassMain on ML_InfoClass.sid0 = ML_InfoClassMain.nID");
        sql += where;
        sql += " order by ML_InfoClassMain.nPaiXu asc, ML_InfoClass.nPaiXu asc";
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    //加载信息
    public DataTable SelInfoClass(string where)
    {
        string sql = string.Format(@"select ML_Info.*, ML_InfoClass.nID as classID,ML_InfoClass.tClassName as className
                       from dbo.ML_Info inner join ML_InfoClass on ML_Info.cid0=ML_InfoClass.nID  {0} order by ML_InfoClass.nPaiXu asc", where);
        try { dt = her.ExecuteDataTable(sql); }
        catch { }
        return dt;
    }
    #endregion


    //内容修饰
    public string Centers(string center, int num)
    {
        center = info.strSub(center);
        if (center.Length > num)
        {
            center = center.Substring(0, num) + "...";
        }
        return center;
    }
    //企业信息
    public DataTable SysConfig(string where)
    {
        string sql = @"select top 1 * from dbo.ML_SysConfig";
        sql += where;
        DataTable dt = her.ExecuteDataTable(sql);
        return dt;
    }
}