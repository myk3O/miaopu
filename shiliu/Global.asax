<%@ Application Language="C#" %>
<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {

        //定义定时器功能
        //1.缓存access_token
        //2.全球分红
        //3.会员等级升级

        System.Timers.Timer myTimer = new System.Timers.Timer(1800000);//access_token7200000=2个小时过期，这里设成半个小时就刷新一次

        myTimer.Elapsed += new System.Timers.ElapsedEventHandler(myTimer_Elapsed);

        myTimer.Enabled = true;

        myTimer.AutoReset = true;

        myTimer_Elapsed(null, null);

        myTimer.Start();

    }


    void myTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            //1.缓存access_token
            DateTime dt = System.DateTime.Now;
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
            YourTask();
            WeiPay.LogUtil.WriteGlobalLog(time);
            //3.每月排行
            MonthPaiHang mp = new MonthPaiHang();
            if (HttpRuntime.Cache["MonthRanking"] == null)
            {
                HttpRuntime.Cache.Insert("MonthRanking", mp.getMonthRadnking());
            }
            else
            {
                string str = mp.getMonthRadnking();//这个时间耗费长，保证查找完毕再进行清空
                HttpRuntime.Cache.Remove("MonthRanking");
                HttpRuntime.Cache.Insert("MonthRanking", str);
            }
            WeiPay.LogUtil.WriteGlobalLog("每月排行缓存完成");
            //4.我的排名
            if (HttpRuntime.Cache["Ranking"] == null)
            {
                HttpRuntime.Cache.Insert("Ranking", mp.getRadnking());
            }
            else
            {
                Dictionary<string, UserInfo> dicPri = mp.getRadnking();//这个时间耗费长，保证查找完毕再进行清空
                HttpRuntime.Cache.Remove("Ranking");
                HttpRuntime.Cache.Insert("Ranking", dicPri);
            }
            WeiPay.LogUtil.WriteGlobalLog("我的排名缓存完成");
            //2.全球分红

            //if (dt.Hour > 12 && dt.Hour < 13)//每天12点以后执行（小于13是为了保证性能，减少判断）
            if (false)
            {
                tongji tj = new tongji();
                if (tj.InsertFenHongLastDay(tj.GetUserByLevel(3), 3, 0, false))
                {
                    WeiPay.LogUtil.WriteGlobalLog("班长全球分红完成");
                }
                else
                {
                    WeiPay.LogUtil.WriteGlobalLog("班长全球分红：无用户或已分红");
                }
                if (tj.InsertFenHongLastDay(tj.GetUserByLevel(4), 4, 0, false))
                {
                    WeiPay.LogUtil.WriteGlobalLog("班主任全球分红完成");
                }
                else
                {
                    WeiPay.LogUtil.WriteGlobalLog("班主任全球分红：无用户或已分红");
                }
                if (tj.InsertFenHongLastDay(tj.GetUserByLevel(5), 5, 0, false))
                {
                    WeiPay.LogUtil.WriteGlobalLog("校长全球分红完成");
                }
                else
                {
                    WeiPay.LogUtil.WriteGlobalLog("校长全球分红：无用户或已分红");
                }
            }
        }
        catch (Exception ee)
        {
            // Log.SaveException(ee);
            WeiPay.LogUtil.WriteGlobalLog("Exception:" + ee.Message);
        }

    }

    string Get_access_token()
    {
        string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", WeiPay.PayConfig.AppId, WeiPay.PayConfig.AppSecret);
        string returnStr = WeiPay.HttpUtil.Send("", url);
        WeiPay.OpenModel obj = new WeiPay.OpenModel();
        try
        {
            obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiPay.OpenModel>(returnStr);
        }
        catch (Exception e)
        {
            WeiPay.LogUtil.WriteGlobalLog("obj转换错误" + e.Message);
        }
        if (HttpRuntime.Cache["access_token"] == null)
        {
            HttpRuntime.Cache.Insert("access_token", obj.access_token);
            //HttpRuntime.Cache与HttpContext.Current.Cache是同一对象,建议使用HttpRuntime.Cache                
        }
        else
        {
            HttpRuntime.Cache.Remove("access_token");
            HttpRuntime.Cache.Insert("access_token", obj.access_token);
        }
        WeiPay.LogUtil.WriteGlobalLog("一般access_token：" + HttpRuntime.Cache["access_token"].ToString());
        return HttpRuntime.Cache["access_token"].ToString();
    }

    void Get_jsapi_ticket(string access_token)
    {
        string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
        string returnStr = WeiPay.HttpUtil.Send("", url);
        WeiPay.jsapiModel obj = new WeiPay.jsapiModel();
        try
        {
            obj = Newtonsoft.Json.JsonConvert.DeserializeObject<WeiPay.jsapiModel>(returnStr);
        }
        catch (Exception e)
        {
            WeiPay.LogUtil.WriteGlobalLog("obj转换错误" + e.Message);
        }
        if (HttpRuntime.Cache["jsapi_ticket"] == null)
        {
            HttpRuntime.Cache.Insert("jsapi_ticket", obj.ticket);
            //HttpRuntime.Cache与HttpContext.Current.Cache是同一对象,建议使用HttpRuntime.Cache                
        }
        else
        {
            HttpRuntime.Cache.Remove("jsapi_ticket");
            HttpRuntime.Cache.Insert("jsapi_ticket", obj.ticket);
        }
        WeiPay.LogUtil.WriteGlobalLog("jsapi_ticket：" + HttpRuntime.Cache["jsapi_ticket"].ToString());

    }

    /// <summary>
    /// 主动获取，一般access_token
    /// </summary>
    void YourTask()
    {
        //在这里写你需要执行的任务          
        //DateTime dtNow = System.DateTime.Now.AddDays(-3);
        //string sql = "update  ML_Order set Oping=1 where OrderState=1 and CreateTime<'" + dtNow + "'";
        //Maliang.SqlHelper her = new Maliang.SqlHelper();
        //her.ExecuteNonQuery(sql);
        Get_jsapi_ticket(Get_access_token());


    }
    void Application_End(object sender, EventArgs e)
    {

        //Log.SaveNote(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":Application End!");  

        //下面的代码是关键，可解决IIS应用程序池自动回收的问题  

        System.Threading.Thread.Sleep(1000);

        //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start  

        //string url = "http://jm.nowwin.cn/";

        string url = "http://tv.gongxue168.com/Wap/index.aspx";
        System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

        System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();

        System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流  

    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        /* Fix for the Flash Player Cookie bug in Non-IE browsers.
         * Since Flash Player always sends the IE cookies even in FireFox
         * we have to bypass the cookies by sending the values as part of the POST or GET
         * and overwrite the cookies with the passed in values.
         * 
         * The theory is that at this point (BeginRequest) the cookies have not been read by
         * the Session and Authentication logic and if we update the cookies here we'll get our
         * Session and Authentication restored correctly
         */

        try
        {
            string session_param_name = "ASPSESSID";
            string session_cookie_name = "ASP.NET_SESSIONID";

            if (HttpContext.Current.Request.Form[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
            }
        }
        catch (Exception)
        {
            Response.StatusCode = 500;
            Response.Write("Error Initializing Session");
        }

        try
        {
            string auth_param_name = "AUTHID";
            string auth_cookie_name = FormsAuthentication.FormsCookieName;

            if (HttpContext.Current.Request.Form[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
            }

        }
        catch (Exception)
        {
            Response.StatusCode = 500;
            Response.Write("Error Initializing Forms Authentication");
        }
    }
    void UpdateCookie(string cookie_name, string cookie_value)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
        if (cookie == null)
        {
            cookie = new HttpCookie(cookie_name);
            HttpContext.Current.Request.Cookies.Add(cookie);
        }
        cookie.Value = cookie_value;
        HttpContext.Current.Request.Cookies.Set(cookie);
    }
    
   
   　 
		   
</script>
