<%@ Application Language="C#" %>
<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {

        //���嶨ʱ������
        //1.����access_token
        //2.ȫ��ֺ�
        //3.��Ա�ȼ�����

        System.Timers.Timer myTimer = new System.Timers.Timer(1800000);//access_token7200000=2��Сʱ���ڣ�������ɰ��Сʱ��ˢ��һ��

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
            //1.����access_token
            DateTime dt = System.DateTime.Now;
            string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
            YourTask();
            WeiPay.LogUtil.WriteGlobalLog(time);
            //3.ÿ������
            MonthPaiHang mp = new MonthPaiHang();
            if (HttpRuntime.Cache["MonthRanking"] == null)
            {
                HttpRuntime.Cache.Insert("MonthRanking", mp.getMonthRadnking());
            }
            else
            {
                string str = mp.getMonthRadnking();//���ʱ��ķѳ�����֤��������ٽ������
                HttpRuntime.Cache.Remove("MonthRanking");
                HttpRuntime.Cache.Insert("MonthRanking", str);
            }
            WeiPay.LogUtil.WriteGlobalLog("ÿ�����л������");
            //4.�ҵ�����
            if (HttpRuntime.Cache["Ranking"] == null)
            {
                HttpRuntime.Cache.Insert("Ranking", mp.getRadnking());
            }
            else
            {
                Dictionary<string, UserInfo> dicPri = mp.getRadnking();//���ʱ��ķѳ�����֤��������ٽ������
                HttpRuntime.Cache.Remove("Ranking");
                HttpRuntime.Cache.Insert("Ranking", dicPri);
            }
            WeiPay.LogUtil.WriteGlobalLog("�ҵ������������");
            //2.ȫ��ֺ�

            //if (dt.Hour > 12 && dt.Hour < 13)//ÿ��12���Ժ�ִ�У�С��13��Ϊ�˱�֤���ܣ������жϣ�
            if (false)
            {
                tongji tj = new tongji();
                if (tj.InsertFenHongLastDay(tj.GetUserByLevel(3), 3, 0, false))
                {
                    WeiPay.LogUtil.WriteGlobalLog("�೤ȫ��ֺ����");
                }
                else
                {
                    WeiPay.LogUtil.WriteGlobalLog("�೤ȫ��ֺ죺���û����ѷֺ�");
                }
                if (tj.InsertFenHongLastDay(tj.GetUserByLevel(4), 4, 0, false))
                {
                    WeiPay.LogUtil.WriteGlobalLog("������ȫ��ֺ����");
                }
                else
                {
                    WeiPay.LogUtil.WriteGlobalLog("������ȫ��ֺ죺���û����ѷֺ�");
                }
                if (tj.InsertFenHongLastDay(tj.GetUserByLevel(5), 5, 0, false))
                {
                    WeiPay.LogUtil.WriteGlobalLog("У��ȫ��ֺ����");
                }
                else
                {
                    WeiPay.LogUtil.WriteGlobalLog("У��ȫ��ֺ죺���û����ѷֺ�");
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
            WeiPay.LogUtil.WriteGlobalLog("objת������" + e.Message);
        }
        if (HttpRuntime.Cache["access_token"] == null)
        {
            HttpRuntime.Cache.Insert("access_token", obj.access_token);
            //HttpRuntime.Cache��HttpContext.Current.Cache��ͬһ����,����ʹ��HttpRuntime.Cache                
        }
        else
        {
            HttpRuntime.Cache.Remove("access_token");
            HttpRuntime.Cache.Insert("access_token", obj.access_token);
        }
        WeiPay.LogUtil.WriteGlobalLog("һ��access_token��" + HttpRuntime.Cache["access_token"].ToString());
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
            WeiPay.LogUtil.WriteGlobalLog("objת������" + e.Message);
        }
        if (HttpRuntime.Cache["jsapi_ticket"] == null)
        {
            HttpRuntime.Cache.Insert("jsapi_ticket", obj.ticket);
            //HttpRuntime.Cache��HttpContext.Current.Cache��ͬһ����,����ʹ��HttpRuntime.Cache                
        }
        else
        {
            HttpRuntime.Cache.Remove("jsapi_ticket");
            HttpRuntime.Cache.Insert("jsapi_ticket", obj.ticket);
        }
        WeiPay.LogUtil.WriteGlobalLog("jsapi_ticket��" + HttpRuntime.Cache["jsapi_ticket"].ToString());

    }

    /// <summary>
    /// ������ȡ��һ��access_token
    /// </summary>
    void YourTask()
    {
        //������д����Ҫִ�е�����          
        //DateTime dtNow = System.DateTime.Now.AddDays(-3);
        //string sql = "update  ML_Order set Oping=1 where OrderState=1 and CreateTime<'" + dtNow + "'";
        //Maliang.SqlHelper her = new Maliang.SqlHelper();
        //her.ExecuteNonQuery(sql);
        Get_jsapi_ticket(Get_access_token());


    }
    void Application_End(object sender, EventArgs e)
    {

        //Log.SaveNote(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":Application End!");  

        //����Ĵ����ǹؼ����ɽ��IISӦ�ó�����Զ����յ�����  

        System.Threading.Thread.Sleep(1000);

        //�����������web��ַ���������ָ���������һ��aspxҳ�����������ڵ�ҳ�棬Ŀ����Ҫ����Application_Start  

        //string url = "http://jm.nowwin.cn/";

        string url = "http://tv.gongxue168.com/Wap/index.aspx";
        System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

        System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();

        System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//�õ���д���ֽ���  

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
    
   
   �� 
		   
</script>
