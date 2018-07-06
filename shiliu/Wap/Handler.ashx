<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using Maliang;
using WeiPay;

public class Handler : IHttpHandler, IRequiresSessionState
{
    Config cf = new Config();
    Order or = new Order();
    SqlHelper her = new SqlHelper();
    JavaScriptSerializer js = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {

        if (context.Request["Method"] != null)
        {
            //获取执行方法的名称
            string methodName = context.Request["Method"].ToString();
            //反射执行指定方法
            MethodInfo minfo = this.GetType().GetMethod(methodName);
            if (minfo != null)
            {
                minfo.Invoke(this, new object[] { context });
            }
        }
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        //context.Response.CacheControl = "public";
        //context.Response.Expires = 1; //1分钟
    }



    /// <summary>
    /// 修改用户信息  
    /// </summary>
    /// <param name="context"></param>
    public void UpdateMemberBynID(HttpContext context)
    {
        bool success = false;
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        string name = context.Request["name"] == null ? "" : context.Request["name"].ToString();
        string idcard = context.Request["idcard"] == null ? "" : context.Request["idcard"].ToString();
        string phone = context.Request["phone"] == null ? "" : context.Request["phone"].ToString();
        //string addr = context.Request["addr"] == null ? "" : context.Request["addr"].ToString();
        string qq = context.Request["qq"] == null ? "" : context.Request["qq"].ToString();
        ////
        // string aprovince = context.Request["aprovince"] == null ? "" : context.Request["aprovince"].ToString();
        // string acity = context.Request["acity"] == null ? "" : context.Request["acity"].ToString();
        // string aarea = context.Request["aarea"] == null ? "" : context.Request["aarea"].ToString();
        //string tujing = context.Request["tujing"] == null ? "" : context.Request["tujing"].ToString();
        //string job = context.Request["job"] == null ? "" : context.Request["job"].ToString();



        if (!string.IsNullOrEmpty(userID))
        {
            SqlParameter sname = new SqlParameter("@tRealName", name);
            SqlParameter sphone = new SqlParameter("@MemberPhone", phone);
            SqlParameter sqq = new SqlParameter("@MemberEmail", qq);
            SqlParameter card = new SqlParameter("@MemberPass", idcard);
            SqlParameter[] count = { sname, sphone, sqq, card };
            string update = @"update ML_Member set tRealName=@tRealName 
 ,MemberPhone=@MemberPhone ,MemberEmail=@MemberEmail, MemberPass=@MemberPass where nID=" + userID;
            if (her.ExecuteNonQuery(update, count))
            {
                success = true;
                //if (UserInfo(userID, aprovince, acity, aarea, addr, tujing, job))
                //{
                //    success = true;
                //}
            }


        }
        ResponseData(context, js.Serialize(success));
    }

    public bool UserInfo(string uid, string pro, string city, string area, string addr, string tuijian, string job)
    {
        bool flag = false;
        SqlParameter[] count = 
                {  
                    new SqlParameter("@UserID", uid),
                    new SqlParameter("@Sheng", pro),
                    new SqlParameter("@Shi", city),
                    new SqlParameter("@area", area),
                    new SqlParameter("@YouBian", addr),                    
                    new SqlParameter("@PeopleName", tuijian),
                    new SqlParameter("@Phone", job),                    
                    new SqlParameter("@CreateTime", System.DateTime.Now.ToString()), 
                };

        string sql = "select count(*) from ML_UserAddress where UserID=" + uid;
        if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)//有地址
        {
            string sql1 = @"update ML_UserAddress set Sheng=@Sheng,Shi=@Shi,area=@area,
                           YouBian=@YouBian,PeopleName=@PeopleName,Phone=@Phone where UserID=@UserID";
            flag = her.ExecuteNonQuery(sql1, count);
        }
        else
        {
            string sql2 = "insert into ML_UserAddress values (@UserID,@Sheng,@Shi,@area,'',@YouBian,@PeopleName,@Phone,@CreateTime,'')";
            flag = her.ExecuteNonQuery(sql2, count);

        }
        return flag;

    }



    /// <summary>
    /// 获取用户对象 
    /// </summary>
    /// <param name="context"></param>
    public void GetMemberObjectBynID(HttpContext context)
    {
        MemberHelper mb = new MemberHelper();
        MemberInfo mi = new MemberInfo();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            DataTable dt = mb.GetMember(userID);
            foreach (DataRow dr in dt.Rows)
            {
                mi.nID = dr["nID"] == null ? "" : dr["nID"].ToString();
                mi.openid = dr["openid"] == null ? "" : dr["openid"].ToString();
                mi.nickname = dr["nickname"] == null ? "" : dr["nickname"].ToString();
                mi.tRealName = dr["tRealName"] == null ? "" : dr["tRealName"].ToString();
                mi.MemberSex = dr["MemberSex"].ToString() == "1" ? "男" : "女";
                mi.MemberPhone = dr["MemberPhone"] == null ? "" : dr["MemberPhone"].ToString();
                mi.MemberEmail = dr["MemberEmail"] == null ? "" : dr["MemberEmail"].ToString();
                mi.MemberCode = dr["MemberCode"] == null ? "" : dr["MemberCode"].ToString();
                mi.MemberPass = dr["MemberPass"] == null ? "" : dr["MemberPass"].ToString();
                mi.dtAddTime = dr["dtAddTime"] == null ? "" : dr["dtAddTime"].ToString();
                mi.headimgurl = dr["headimgurl"] == null ? "" : dr["headimgurl"].ToString();
                mi.isJXS = Convert.ToBoolean(dr["isJXS"]);
                mi.MemberState = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["MemberState"].ToString()));
                mi.isUseful = Convert.ToBoolean(dr["isUseful"].ToString());
                mi.age = dr["fxslevel"] == null ? "" : dr["fxslevel"].ToString() == "1" ? "" : dr["fxslevel"].ToString();
            }
            ResponseData(context, js.Serialize(mi));
        }
    }
    public class MemberInfo
    {
        public string nID { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public string tRealName { get; set; }
        public string MemberSex { get; set; }
        public string MemberPhone { get; set; }
        public string MemberEmail { get; set; }
        public string MemberCode { get; set; }
        public string MemberPass { get; set; }
        public string dtAddTime { get; set; }
        public string headimgurl { get; set; }
        public bool isJXS { get; set; }
        public string MemberState { get; set; }
        public bool isUseful { get; set; }
        public string age { get; set; }

    }
    /// <summary>
    /// 获取用户  
    /// </summary>
    /// <param name="context"></param>
    public void GetMemberBynID(HttpContext context)
    {
        MemberHelper mb = new MemberHelper();
        StringBuilder sb = new StringBuilder();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            DataTable dt = mb.GetMember(userID);
            foreach (DataRow dr in dt.Rows)
            {
                string img = dr["headimgurl"].ToString() == "" ? "img/Styl_01.png" : dr["headimgurl"].ToString();
                string nickname = dr["nickname"].ToString() == "" ? "共学用户" : StringDelHTML.Centers(dr["nickname"].ToString(), 8);
                string code = dr["MemberCode"] == null ? "" : dr["MemberCode"].ToString();

                string levelName = dr["levelName"].ToString();

                string sql2 = "select nickname from ML_Member where nID=" + dr["FatherFXSID"].ToString();
                string fname = her.ExecuteScalar(sql2) == null ? "" : StringDelHTML.Centers(her.ExecuteScalar(sql2).ToString(), 8);

                sb.AppendLine("<img src='" + img + "'/><h1>昵称：" + nickname + "</h1>");
                //sb.AppendLine("<h2>关注时间：" + Convert.ToDateTime(dr["fxsTimeBegin"]).ToString("yyyy-MM-dd") + "</h2>");
                //sb.AppendLine("<h3>石榴学员</h3>");

                sb.AppendLine("<h2>我的编号：" + code + "</h2>");
                if (!string.IsNullOrEmpty(fname))
                {
                    sb.AppendLine("<h3>我的好友：" + fname + "</h3>");
                }

                sb.AppendLine("<h3>" + levelName + "</h3>");

            }
            ResponseData(context, js.Serialize(sb.ToString()));
        }
    }
    /// <summary>
    /// 获取用户  
    /// </summary>
    /// <param name="context"></param>
    public void GetMemberByMyProfile(HttpContext context)
    {
        MemberHelper mb = new MemberHelper();
        StringBuilder sb = new StringBuilder();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            //string sql = "select * from ML_UserAddress where UserID=" + userID;
            //DataTable dta = her.ExecuteDataTable(sql);
            //string tujing = "", job = "";

            //if (dta.Rows.Count > 0)
            //{
            //    tujing = dta.Rows[0]["PeopleName"].ToString();
            //    job = dta.Rows[0]["Phone"].ToString();
            //}

            DataTable dt = mb.GetMember(userID);
            foreach (DataRow dr in dt.Rows)
            {

                string name = dr["tRealName"] == null ? "" : dr["tRealName"].ToString();
                string email = dr["MemberEmail"] == null ? "" : dr["MemberEmail"].ToString();//qq号
                string phone = dr["MemberPhone"] == null ? "" : dr["MemberPhone"].ToString();
                string card = dr["MemberPass"] == null ? "" : dr["MemberPass"].ToString();//身份证号
                // string age = dr["fxslevel"] == null ? "" : dr["fxslevel"].ToString() == "1" ? "" : dr["fxslevel"].ToString();//年龄
                string sex = dr["MemberSex"].ToString() == "1" ? "男" : dr["MemberSex"].ToString() == "2" ? "女" : "未填写";
                string code = dr["MemberCode"] == null ? "" : dr["MemberCode"].ToString();

                string img = dr["headimgurl"].ToString() == "" ? "img/Styl_01.png" : dr["headimgurl"].ToString();
                string nickname = dr["nickname"].ToString() == "" ? "共学用户" : dr["nickname"].ToString();
                string levelName = dr["levelName"].ToString();

                string sql2 = "select nickname from ML_Member where nID=" + dr["FatherFXSID"].ToString();
                string fname = her.ExecuteScalar(sql2) == null ? "" : her.ExecuteScalar(sql2).ToString();

                sb.AppendLine("<div class='StylistT'>");
                sb.AppendLine("<img src='" + img + "' /><h1>" + nickname + "</h1>");
                sb.AppendLine("<h2>我的编号：" + code + "</h2>");
                if (!string.IsNullOrEmpty(fname))
                {
                    sb.AppendLine("<h3>我的好友：" + fname + "</h3>");
                }

                sb.AppendLine("<h3>" + levelName + "</h3>");
                //sb.AppendLine("<h2>关注时间：" + Convert.ToDateTime(dr["fxsTimeBegin"]).ToString("yyyy-MM-dd") + "</h2>");
                sb.AppendLine("</div>");

                sb.AppendLine("<dl class='MyProfile'>");
                sb.AppendLine("<dd><span>姓 名：</span>" + name + "</dd>");
                sb.AppendLine("<dd><span>性 别：</span>" + sex + "</dd>");
                //sb.AppendLine("<dd><span>年 龄：</span>" + age + "</dd>");
                //sb.AppendLine("<dd><span>地 区：</span>" + addr + "</dd>");
                sb.AppendLine("<dd><span>证 件：</span>" + card + "</dd>");
                sb.AppendLine("<dd><span>手 机：</span>" + phone + "</dd>");
                //sb.AppendLine("<dd><span>Q  Q：</span>" + email + "</dd>");
                //sb.AppendLine("<dd><span>途 径：</span>" + tujing + "</dd>");
                //sb.AppendLine("<dd><span>职 业：</span>" + job + "</dd>");
                sb.AppendLine("<dt><input type=button  onclick=jump() value=修改信息 /></dt>");
                sb.AppendLine("</dl>");

            }
            ResponseData(context, js.Serialize(sb.ToString()));
        }
    }
    /// <summary>
    /// 获取用户  
    /// </summary>
    /// <param name="context"></param>
    public void GetMemberBynIDList(HttpContext context)
    {
        MemberHelper mb = new MemberHelper();
        StringBuilder sb = new StringBuilder();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            //当前可提现资金=总金-已提现
            DataTable dt = mb.GetMember(userID);
            foreach (DataRow dr in dt.Rows)
            {
                string img = dr["headimgurl"].ToString() == "" ? "img/Styl_01.png" : dr["headimgurl"].ToString();
                string nickname = dr["nickname"].ToString() == "" ? "共学用户" : dr["nickname"].ToString();
                sb.AppendLine("<img src='" + img + "' /><h1>昵称：" + nickname + "</h1>");
                //sb.AppendLine("<h2>当前可提现资金：0.00元</h2>");
                string sex = string.Empty;
                switch (Convert.ToInt32(dr["MemberSex"]))
                {
                    case 1: sex = "男"; break;
                    case 2: sex = "女"; break;
                    default: sex = "未填写"; break;
                }
                //sb.AppendLine("<p>性别：" + sex + "</p>");
                //sb.AppendLine("<a onclick='LoginOut()'>注销</a>");
                //sb.AppendLine("<a href='Editor.aspx'>编辑</a>");
            }
            ResponseData(context, js.Serialize(sb.ToString()));
        }
    }
    /// <summary>
    /// 获取用户是否经销商  
    /// </summary>
    /// <param name="context"></param>
    public void Memberisfxs(HttpContext context)
    {
        bool isjxs = false;
        StringBuilder sb = new StringBuilder();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = "select isJXS  from ML_Member where nID=" + userID;
            string jsx = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
            if (jsx.ToLower() == "true")
            {
                isjxs = true;
            }
            else
            {
                isjxs = false;
            }

            ResponseData(context, js.Serialize(isjxs));
        }
    }
    /// <summary>
    /// 判断用户是否购买了这个视频 
    /// </summary>
    /// <param name="context"></param>
    public void IsForFree(HttpContext context)
    {
        bool flag = false;

        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        string proID = context.Request["proID"] == null ? "" : context.Request["proID"].ToString();
        if (!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(proID))
        {
            string sql1 = "select count(1) from ML_VideoComment where nID=" + proID + " and oFree=1";
            if (her.ExecuteScalar(sql1) != null && Convert.ToInt32(her.ExecuteScalar(sql1)) > 0)
            {
                flag = true;//免费视频
            }
            else
            {
                //string sql2 = "select OcID from ML_Order where OrderUserid=" + userID;
                //DataTable dt = her.ExecuteDataTable(sql2);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (proID.Equals(dt.Rows[i]["OcID"].ToString()))
                //    {
                //        flag = true;
                //        break;
                //    }
                //}
                string sql2 = string.Format(@"select count(1) from ML_Order where OrderUserid={0} and OcID={1}", userID, proID);
                if (her.ExecuteScalar(sql2) != null && Convert.ToInt32(her.ExecuteScalar(sql2)) > 0)
                {
                    flag = true;//免费视频
                }

            }

            ResponseData(context, js.Serialize(flag));
        }
    }
    /// <summary>
    /// 获取用户是否经销商(二维码)
    /// </summary>
    /// <param name="context"></param>
    public void MemberisfxsErMa(HttpContext context)
    {

        StringBuilder sb = new StringBuilder();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = "select isJXS,fxsImg  from ML_Member where nID=" + userID;
            DataTable dt = her.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {

                string jsx = dt.Rows[0]["isJXS"] == null ? "false" : dt.Rows[0]["isJXS"].ToString();
                if (jsx.ToLower() == "true")
                {
                    string img = dt.Rows[0]["fxsImg"] == null ? "" : dt.Rows[0]["fxsImg"].ToString();
                    sb.Append("<img  src='../upload_Img/Pic/" + img + "' width='100%'>");

                }
                else
                {
                    sb.Append("<img src='../upload_Img/erma.png' width='100%'/>");
                }
            }

            ResponseData(context, js.Serialize(sb.ToString()));
        }
    }

    /// <summary>
    /// 获取用户绑定银行卡 
    /// </summary>
    /// <param name="context"></param>
    public void GetBankCardByUid(HttpContext context)
    {
        bankCard bc = new bankCard();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = "select * from Bank where Cid=" + userID;
            DataTable dt = her.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                bc.Cid = dr["Cid"] == null ? "" : dr["Cid"].ToString();
                bc.TypeName = dr["TypeName"] == null ? "" : dr["TypeName"].ToString();
                bc.Zhanghao = dr["Zhanghao"] == null ? "" : dr["Zhanghao"].ToString();
                bc.Huming = dr["Huming"] == null ? "" : dr["Huming"].ToString();
                bc.Kaihu = dr["Kaihu"] == null ? "" : dr["Kaihu"].ToString();
                bc.AddTime = dr["AddTime"] == null ? "" : dr["AddTime"].ToString();
                bc.Code = dr["code"] == null ? "" : dr["code"].ToString();
                bc.Address = dr["address"] == null ? "" : dr["address"].ToString();
            }
            ResponseData(context, js.Serialize(bc));
        }
    }
    public class bankCard
    {
        public string Cid { get; set; }
        public string TypeName { get; set; }
        public string Zhanghao { get; set; }
        public string Huming { get; set; }
        public string Kaihu { get; set; }
        public string AddTime { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
    }

    /// <summary>
    /// 修改银行卡信息  
    /// </summary>
    /// <param name="context"></param>
    public void UpdateBankCartByCid(HttpContext context)
    {
        bool success = false;
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        string TypeName = context.Request["TypeName"] == null ? "" : context.Request["TypeName"].ToString();
        string Huming = context.Request["Huming"] == null ? "" : context.Request["Huming"].ToString();
        string Zhanghao = context.Request["Zhanghao"] == null ? "" : context.Request["Zhanghao"].ToString();
        string Kaihu = context.Request["Kaihu"] == null ? "" : context.Request["Kaihu"].ToString();
        string code = context.Request["Code"] == null ? "" : context.Request["Code"].ToString();
        string dizhi = context.Request["Address"] == null ? "" : context.Request["Address"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = "select count(*) from Bank where Cid=" + userID;
            SqlParameter cid = new SqlParameter("@Cid", userID);
            SqlParameter tn = new SqlParameter("@TypeName", TypeName);
            SqlParameter hm = new SqlParameter("@Huming", Huming);
            SqlParameter zh = new SqlParameter("@Zhanghao", Zhanghao);
            SqlParameter kh = new SqlParameter("@Kaihu", Kaihu);
            SqlParameter time = new SqlParameter("@AddTime", System.DateTime.Now);
            SqlParameter co = new SqlParameter("@code", code);
            SqlParameter ad = new SqlParameter("@address", dizhi);
            SqlParameter[] count = { cid, tn, hm, zh, kh, time, co, ad };
            if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)//如果已经存在
            {
                string update = @"update Bank set TypeName=@TypeName ,Huming=@Huming 
 ,Zhanghao=@Zhanghao   ,Kaihu=@Kaihu  ,AddTime=@AddTime  ,code=@code ,address=@address where Cid=@Cid ";
                success = her.ExecuteNonQuery(update, count);
            }
            else
            {
                string insert = @"insert into Bank values (@Cid,@TypeName,@Zhanghao,@Huming,@Kaihu,@AddTime,@code,@address)";
                success = her.ExecuteNonQuery(insert, count);
            }
        }
        ResponseData(context, js.Serialize(success));
    }


    PayHelp ph = new PayHelp();
    MemberHelper Menber = new MemberHelper();
    tongji tj = new tongji();
    Video vd = new Video();
    /// <summary>
    /// 生成订单（学习币支付）
    /// </summary>
    /// <param name="context"></param>
    public void MakeOrderXuexb(HttpContext context)
    {
        string fatherID = "0";
        LogUtil.WriteLog("MakeOrder");
        bool falg = false;
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();//登录用户
        // string Auntid = context.Request["agentID"] == null ? "0" : context.Request["agentID"].ToString() == "" ? "0" : context.Request["agentID"].ToString();//代理商id
        string price = context.Request.Params["Price"] == null ? "" : context.Request.Params["Price"].ToString();//应该根据产品id，重新查价格
        string proid = context.Request.Params["proID"] == null ? "" : context.Request.Params["proID"].ToString();
        string ordercode = context.Request["OrderCode"] == null ? "" : context.Request["OrderCode"].ToString();

        if (!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(ordercode) && !string.IsNullOrEmpty(price) && !string.IsNullOrEmpty(proid))
        {
            LogUtil.WriteLog("数据未丢失");
            if (ph.IsOrder(ordercode) == false)//已经存在就不要入库了
            {
                //一旦用户的父级不是 总部，则该用户下的订单都属于 同一个父级
                string sql = "select FatherFXSID from ML_Member where nID=" + userID;
                if (Convert.ToInt32(her.ExecuteScalar(sql)) > 0)//已经有父级，就用之前的父级别
                {
                    fatherID = her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
                }
                if (!string.IsNullOrEmpty(vd.GetVideoPrice(proid)))//查不到才用传过来的
                {
                    price = vd.GetVideoPrice(proid);
                }
                //判断是否已经是会员
                if (ph.Memberisfxs(userID))
                {
                    //再次购买，产品打1折
                    price = (Convert.ToInt32(price) * 0.1).ToString();

                }
                //double learnpt = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["learnPart"]);//学习币比例
                //下面这些是求 “可使用学习币”
                int PayMoneyNO = sh.getPayMoneyNO(userID);//申请中
                int FenXiangYJ = tj.GetFenXiangYJ(userID);//分享佣金
                int PayMoneys = sh.getPayMoney(userID);//已提现
                int jiangxj = tj.GetSumMoneyByUser(userID);//累计奖学金,分红
                int jintie = tj.GetJx(userID);//绩效，津贴
                //总金=分享佣金+全球分红+绩效+邀请关注(2016/3/30)
                double allmakemoney = FenXiangYJ + jiangxj + jintie + tj.GetAttentionXF(userID);
                //当前剩余资金=总金-已提现-已申请
                // double allpri = allmakemoney - PayMoneys - PayMoneyNO;
                //总学习币=总金的一定比例
                double allxxb = tj.Round((allmakemoney) * cf.learnPart, 0);
                //可用学习币=总学习币-已使用学习币+转入学习币+首次关注赠送(2016/3/30)
                double canxxb = allxxb - sh.getUsedXueXb(userID) + tj.GetXueXibi(userID) + tj.GetAttentionXXB(userID);
                if (Convert.ToInt32(price) <= Convert.ToInt32(canxxb)) //判断学习币 是否够支付 产品
                {
                    int pri = Convert.ToInt32(price);//测试结束，要注销掉* 10000
                    //插入订单
                    falg = ph.InsertOrder(userID, fatherID, ordercode, pri, proid, "学习币支付");
                    if (falg)
                    {
                        //1.更新已使用学习币
                        ph.UpdateXuexb(userID, Convert.ToInt32(price));
                        //判断是否已经是会员
                        if (ph.Memberisfxs(userID) == false)
                        {
                            if (or.UpdateUserState(userID))
                            {
                                LogUtil.WriteLog("会员成功");
                            }
                            else
                            {
                                LogUtil.WriteLog("会员失败");
                            }
                        }

                        //3.会员等级升级
                        UpdateLevel(userID, fatherID, pri);
                    }

                    LogUtil.WriteLog("插入订单是否成功：" + falg.ToString());

                }
            }


        }
        ResponseData(context, js.Serialize(falg));
    }

    private void UpdateLevel(string userID, string fatherID, int pri)
    {
        LogUtil.WriteLog("佣金分配");
        tj.insetTop1User(Convert.ToInt32(userID), Convert.ToInt32(fatherID), pri);
        tj.insetTop2User(Convert.ToInt32(userID), Convert.ToInt32(fatherID), pri);
        tj.insetTop3User(Convert.ToInt32(userID), Convert.ToInt32(fatherID), pri);
        LogUtil.WriteLog("绩效分配");
        tj.InsertJxBZ(Convert.ToInt32(userID), pri);
        tj.InsertJxBZE(Convert.ToInt32(userID), pri);
        tj.InsertJxXZ(Convert.ToInt32(userID), pri);
        DateTime dt = System.DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
        if (tj.UpdateLevel1())
        {
            WeiPay.LogUtil.WriteGlobalLog("班长查询，更新成功!" + time);
        }
        if (tj.UpdateLevel2())
        {
            WeiPay.LogUtil.WriteGlobalLog("班主任查询，更新成功!" + time);
        }
        if (tj.UpdateLevel3())
        {
            WeiPay.LogUtil.WriteGlobalLog("校长查询，更新成功!" + time);
        }
    }

    /// <summary>
    /// 生成订单（微信支付）
    /// </summary>
    /// <param name="context"></param>
    public void MakeOrder(HttpContext context)
    {
        string fatherID = "0";
        LogUtil.WriteLog("MakeOrder");
        bool falg = true; //微信支付已经处理了，这里只是防止那边出错，默认是成功的
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();//登录用户
        // string Auntid = context.Request["agentID"] == null ? "0" : context.Request["agentID"].ToString() == "" ? "0" : context.Request["agentID"].ToString();//代理商id
        string price = context.Request.Params["Price"] == null ? "" : context.Request.Params["Price"].ToString();//应该根据产品id，重新查价格
        string proid = context.Request.Params["proID"] == null ? "" : context.Request.Params["proID"].ToString();
        string ordercode = context.Request["OrderCode"] == null ? "" : context.Request["OrderCode"].ToString();

        if (!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(ordercode) && !string.IsNullOrEmpty(price) && !string.IsNullOrEmpty(proid))
        {
            LogUtil.WriteLog("数据未丢失");
            if (ph.IsOrder(ordercode) == false)//已经存在就不要入库了
            {
                //一旦用户的父级不是 总部，则该用户下的订单都属于 同一个父级
                string sql = "select FatherFXSID from ML_Member where nID=" + userID;
                if (Convert.ToInt32(her.ExecuteScalar(sql)) > 0)//已经有父级，就用之前的父级别
                {
                    fatherID = her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
                }
                if (!string.IsNullOrEmpty(vd.GetVideoPrice(proid)))//查不到才用传过来的
                {
                    price = vd.GetVideoPrice(proid);
                }
                //判断是否已经是会员
                if (ph.Memberisfxs(userID))
                {
                    //再次购买，产品打1折
                    price = (Convert.ToInt32(price) * 0.1).ToString();

                }
                int pri = Convert.ToInt32(price);//测试结束，要注销掉* 10000
                //插入订单
                falg = ph.InsertOrder(userID, fatherID, ordercode, pri, proid, "微信支付");
                LogUtil.WriteLog(falg.ToString() + "订单号：" + ordercode + "__用户ID：" + userID);
                //判断是否已经是会员
                if (ph.Memberisfxs(userID) == false)
                {
                    LogUtil.WriteLog("不是会员要更新");
                    if (or.UpdateUserState(userID))
                    {
                        LogUtil.WriteLog("会员成功");
                    }
                    else
                    {
                        LogUtil.WriteLog("会员失败");
                    }
                }

                //3.会员等级升级
                UpdateLevel(userID, fatherID, pri);
            }


        }
        ResponseData(context, js.Serialize(falg));
    }



    /// <summary>
    /// 购买列表
    /// </summary>
    /// <param name="context"></param>
    public void GetOrderList(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = @"select b.nID, b.vName,b.vPic,b.Price from  ML_Order a join ML_VideoComment b on a.OcID=b.nID 
                            where a.OrderUserid=" + userID + " order by a.CreateTime desc";
            DataTable dt = her.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                var price = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["Price"].ToString()));
                sb.AppendLine("<dd><a href='VideoShow.aspx?pid=" + dr["nID"].ToString() + "'>");
                sb.AppendLine("<img src='../upload_Img/VideoImg/" + dr["vPic"].ToString() + "' />");
                sb.AppendLine("<h1>" + dr["vName"].ToString() + "</h1>");
                sb.AppendLine("<p><span>￥" + price + "</span></p>");
                sb.AppendLine("</a></dd>");
            }
            if (dt.Rows.Count > 0)
            {
                sb.AppendLine(" <dd><a href='#'></a></dd>");
            }
        }
        ResponseData(context, js.Serialize(sb.ToString()));
    }

    /// <summary>
    /// 产品列表
    /// </summary>
    /// <param name="context"></param>
    public void GetVideoByType(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        string type = context.Request["type"] == null ? "" : context.Request["type"].ToString().Trim();
        string sql = string.Empty;
        if (!string.IsNullOrEmpty(type))
        {
            switch (type)
            {
                case "3": sql = "select * from ML_Video where oHide=1 and sid0=3"; break;
                case "4": sql = "select * from ML_Video where oHide=1 and sid0=4"; break;
                case "5": sql = "select * from ML_Video where oHide=1 and sid0=5"; break;
                default: sql = "select * from ML_Video where oHide=1"; break;
            }

        }
        else
        {
            sql = "select * from ML_VideoComment where oHide=1 and oFree=0 order by addTime desc";
        }
        DataTable dt = her.ExecuteDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            var vname = StringDelHTML.Centers(dr["vName"].ToString(), 7);
            var price = StringDelHTML.PriceToStringLow(Convert.ToInt32(dr["Price"].ToString()));
            sb.AppendLine("<dd><a href='VideoShow.aspx?pid=" + dr["nID"].ToString() + "'>");
            sb.AppendLine("<img src='../upload_Img/VideoImg/" + dr["vPic"].ToString() + "' />");
            sb.AppendLine("<h1>" + vname + "</h1>");
            if (dr["oFree"].ToString() == "True")
            {
                sb.AppendLine("<p><span>免费体验</span></p>");
            }
            else
            {
                sb.AppendLine("<p><span>￥" + price + "</span></p>");
            }
            sb.AppendLine("</a></dd>");
        }


        ResponseData(context, js.Serialize(sb.ToString()));
    }

    StylistHelp sh = new StylistHelp();
    /// <summary>
    /// 提现申请
    /// </summary>
    /// <param name="context"></param>
    public void MoneyApplyByUser(HttpContext context)
    {
        bool success = false;
        string UserID = context.Request["UserID"] == null ? "" : context.Request["UserID"].ToString();

        if (!string.IsNullOrEmpty(UserID))
        {
            double learnpt = cf.learnPart;//学习币比例
            double shuifei = Convert.ToDouble(cf.shuifei);
            int PayMoneyNO = sh.getPayMoneyNO(UserID);//申请中
            int FenXiangYJ = tj.GetFenXiangYJ(UserID);//分享佣金
            int PayMoney = sh.getPayMoney(UserID);//已提现
            int jiangxj = tj.GetSumMoneyByUser(UserID);//累计奖学金,分红
            int jintie = tj.GetJx(UserID);//绩效，津贴
            //总金=分享佣金+全球分红+绩效+邀请关注(2016/3/30)
            double allmakemoney = FenXiangYJ + jiangxj + jintie + tj.GetAttentionXF(UserID);
            //当前剩余资金=总金-已提现-已申请-转学习币
            double allpri = allmakemoney - PayMoney - PayMoneyNO - tj.GetMoneyToXueBi(UserID);
            //总学习币=总金的一定比例
            double allxxb = tj.Round((allmakemoney) * learnpt, 0);
            //可用学习币=总学习币-已使用学习币+转入学习币+首次关注赠送(2016/3/30)
            double canxxb = allxxb - sh.getUsedXueXb(UserID) + tj.GetXueXibi(UserID) + tj.GetAttentionXXB(UserID);
            //可提现资金=当前剩余资金-总学习币
            double canpri = allpri - allxxb;
            double koushui = tj.Round(canpri * shuifei, 0);//扣税

            //CanGetPri = StringDelHTML.DoublePriceToString(canmoney);
            // int money = StringDelHTML.PriceToIntUp(CanGetPri);
            // int ordermoney = StringDelHTML.PriceToIntUp(oMoney);
            if (canpri >= 5000)//可提取资金必须不小于50
            {
                //double shuilv = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["shuifei"]);

                //添加提现申请记录
                if (sh.MoneyOrderInsert(UserID, Convert.ToInt32(canpri - koushui), shuifei.ToString(), Convert.ToInt32(canpri), koushui.ToString()))
                {
                    success = true;
                }
            }
        }

        ResponseData(context, js.Serialize(success));

    }
    /// <summary>
    /// 获取用户当周是否已经有过提现
    /// </summary>
    /// <param name="context"></param>
    public void IsMemberMoneyWeek(HttpContext context)
    {
        bool flag = true;//有提现
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = string.Format(@"  select COUNT(*) from MoneyOrder where AnID={0} and datediff(WEEK,CreateTime,getdate())=0 ", userID);

            if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)
            {
                flag = true;//填了信息
            }
            else
            {
                flag = false;
            }

            ResponseData(context, js.Serialize(flag));
        }
    }
    /// <summary>
    /// 获取用户信息是否完善
    /// </summary>
    /// <param name="context"></param>
    public void IsMemberMsg(HttpContext context)
    {
        bool flag = false;//没有填写信息
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = string.Format(@"  select COUNT(*) from ML_Member where nID={0} and  tRealName is not null and tRealName!='' 
  and MemberPhone!=''  and MemberPhone is not null", userID);

            if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)
            {
                flag = true;//填了信息
            }

            ResponseData(context, js.Serialize(flag));
        }
    }

    /// <summary>
    /// 获取用户银行卡号是否填写
    /// </summary>
    /// <param name="context"></param>
    public void IsMemberBank(HttpContext context)
    {
        bool flag = false;//没有填写信息
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = " select COUNT(*) from Bank where Cid=" + userID;

            if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)
            {
                flag = true;//填了信息
            }

            ResponseData(context, js.Serialize(flag));
        }
    }


    /// <summary>
    /// 获取免责声明
    /// </summary>
    /// <param name="context"></param>
    public void GetShengMing(HttpContext context)
    {
        string sql = "select selectmail from ML_SysConfig";
        string mianze = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        ResponseData(context, js.Serialize(mianze));
    }

    /// <summary>
    /// 获取省
    /// </summary>
    /// <param name="context"></param>
    public void GetProvince(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        //string result = string.Empty;
        string sql = "select * from province";
        DataTable dt = her.ExecuteDataTable(sql);
        sb.AppendLine("<option value=''>省</option>");
        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendLine("<option value='" + dr["pcode"].ToString() + "'>" + dr["pname"].ToString() + "</option>");
        }
        ResponseData(context, js.Serialize(sb.ToString()));
    }

    /// <summary>
    /// 获取市
    /// </summary>
    /// <param name="context"></param>
    public void GetCity(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        //string result = string.Empty;
        string region_id = context.Request["region_id"] == null ? "" : context.Request["region_id"].ToString();
        if (!string.IsNullOrEmpty(region_id))
        {
            string sql = "select * from city where provincecode='" + region_id + "'";
            DataTable dt = her.ExecuteDataTable(sql);
            //sb.AppendLine("<option value=''>请选择</option>");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendLine("<option value='" + dr["ccode"].ToString() + "'>" + dr["cname"].ToString() + "</option>");
            }
        }
        ResponseData(context, js.Serialize(sb.ToString()));
    }
    /// <summary>
    /// 获取区域
    /// </summary>
    /// <param name="context"></param>
    public void GetArea(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        //string result = string.Empty;
        string region_id = context.Request["region_id"] == null ? "" : context.Request["region_id"].ToString();
        if (!string.IsNullOrEmpty(region_id))
        {
            string sql = "select * from area where citycode='" + region_id + "'";
            DataTable dt = her.ExecuteDataTable(sql);
            //sb.AppendLine("<option value=''>请选择</option>");
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendLine("<option value='" + dr["acode"].ToString() + "'>" + dr["aname"].ToString() + "</option>");
            }
        }
        ResponseData(context, js.Serialize(sb.ToString()));
    }

    /// <summary>
    /// 获取收货地址详细信息
    /// </summary>
    /// <param name="context"></param>
    public void GetAddrDetail(HttpContext context)
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        string userID = context.Request.Params["userID"] == null ? "" : context.Request.Params["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            string sql = @"select * from ML_UserAddress where UserID=" + userID;
            dt = her.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                Address addr = new Address()
                {
                    Sheng = dr["Sheng"].ToString(),
                    Shi = dr["Shi"].ToString(),
                    area = dr["area"].ToString(),
                    YouBian = dr["YouBian"].ToString(),
                    PeopleName = dr["PeopleName"].ToString(),
                    Phone = dr["Phone"].ToString(),
                };

                ResponseData(context, js.Serialize(addr));
            }

        }

    }

    public class Address
    {
        public string Sheng { get; set; }
        public string Shi { get; set; }
        public string area { get; set; }
        public string YouBian { get; set; }
        public string PeopleName { get; set; }
        public string Phone { get; set; }
    }


    /// <summary>
    /// 根据code判断，用户是否存在
    /// </summary>
    /// <param name="context"></param>
    public void IsMemberExitByCode(HttpContext context)
    {
        bool flag = false;//没有
        string MemberCode = context.Request["MemberCode"] == null ? "" : context.Request["MemberCode"].ToString();
        if (!string.IsNullOrEmpty(MemberCode))
        {
            string sql = " select COUNT(*) from ML_Member where MemberCode='" + MemberCode + "'";

            if (her.ExecuteScalar(sql) != null && Convert.ToInt32(her.ExecuteScalar(sql)) > 0)
            {
                flag = true;//有
            }

            ResponseData(context, js.Serialize(flag));
        }
    }
    /// <summary>
    /// 根据code判断，找用户
    /// </summary>
    /// <param name="context"></param>
    public void GetMemberByCode(HttpContext context)
    {
        MemberHelper mb = new MemberHelper();
        StringBuilder sb = new StringBuilder();
        string code = context.Request["code"] == null ? "" : context.Request["code"].ToString();
        if (!string.IsNullOrEmpty(code))
        {
            DataTable dt = mb.GetMemberByCode(code);
            foreach (DataRow dr in dt.Rows)
            {
                string img = dr["headimgurl"].ToString() == "" ? "img/Styl_01.png" : dr["headimgurl"].ToString();
                //string nickname = dr["nickname"].ToString() == "" ? "共学用户" : StringDelHTML.Centers(dr["nickname"].ToString(), 8);
                string nickname = dr["nickname"].ToString() == "" ? "共学用户" : dr["nickname"].ToString();
                sb.AppendLine("<img src='" + img + "'/>");
                sb.AppendLine("<h1>" + nickname + "</h1>");
                sb.AppendLine("<p>" + code + "</p>");
                sb.AppendLine("<p id='ToUserID' style='display:none'>" + dr["nID"].ToString() + "</p>");
            }
            ResponseData(context, js.Serialize(sb.ToString()));
        }
    }
    /// <summary>
    /// 发起转币
    /// </summary>
    /// <param name="context"></param>
    public void ApplyZhuanBi(HttpContext context)
    {
        bool success = false;
        string fromUser = context.Request["fromUid"] == null ? "" : context.Request["fromUid"].ToString();
        string toUser = context.Request["toUid"] == null ? "" : context.Request["toUid"].ToString();
        string zMoney = context.Request["zMoney"] == null ? "" : context.Request["zMoney"].ToString();
        string tmemo = context.Request["tMemo"] == null ? "" : context.Request["tMemo"].ToString();
        if (!string.IsNullOrEmpty(fromUser) && !string.IsNullOrEmpty(toUser) && !string.IsNullOrEmpty(zMoney))
        {
            double learnpt = cf.learnPart;//学习币比例
            double shuifei = Convert.ToDouble(cf.shuifei);
            int PayMoneyNO = sh.getPayMoneyNO(fromUser);//申请中
            int FenXiangYJ = tj.GetFenXiangYJ(fromUser);//分享佣金
            int PayMoney = sh.getPayMoney(fromUser);//已提现
            int jiangxj = tj.GetSumMoneyByUser(fromUser);//累计奖学金,分红
            int jintie = tj.GetJx(fromUser);//绩效，津贴
            //总金=分享佣金+全球分红+绩效+邀请关注(2016/3/30)
            double allmakemoney = FenXiangYJ + jiangxj + jintie + tj.GetAttentionXF(fromUser);
            //当前剩余资金=总金-已提现-已申请-转学习币
            double allpri = allmakemoney - PayMoney - PayMoneyNO - tj.GetMoneyToXueBi(fromUser);
            //总学习币=总金的一定比例
            double allxxb = tj.Round((allmakemoney) * learnpt, 0);
            //可用学习币=总学习币-已使用学习币+转入学习币+首次关注赠送(2016/3/30)
            double canxxb = allxxb - sh.getUsedXueXb(fromUser) + tj.GetXueXibi(fromUser) + tj.GetAttentionXXB(fromUser);
            //可提现资金=当前剩余资金-总学习币
            double canpri = allpri - allxxb;

            if (Convert.ToDouble(zMoney) <= canpri)//想转小于等于可转
            {
                //double shuilv = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["shuifei"]);

                //添加转币记录
                if (tj.InsertZhuanBi(Convert.ToInt32(fromUser), Convert.ToInt32(toUser), Convert.ToInt32(zMoney), tmemo))
                {
                    success = true;
                }
            }
        }

        ResponseData(context, js.Serialize(success));

    }

    NewsHelper nh = new NewsHelper();
    /// <summary>
    /// 发起转币
    /// </summary>
    /// <param name="context"></param>
    public void IsNewsRead(HttpContext context)
    {
        bool flag = false;//未读
        string userID = context.Request["userID"] == null ? "" : context.Request["userID"].ToString();
        if (!string.IsNullOrEmpty(userID))
        {
            flag = nh.IsNewsRead(userID);
        }

        ResponseData(context, js.Serialize(flag));

    }
    /// <summary>
    /// 内容输出
    /// </summary>
    /// <param name="context"></param>
    /// <param name="jsonData"></param>
    public void ResponseData(HttpContext context, string jsonData)
    {
        //context.Response.Cache.SetNoStore();
        context.Response.Clear();
        // context.Response.Charset="utf-8";
        context.Response.ContentType = "text/plain";
        context.Response.Write(jsonData);
        context.Response.End();
        // context.Response.Cache.SetNoStore();
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



} 