using System;

using System.Data;

using System.Configuration;

using System.Collections;

using System.Web;

using System.Web.Security;

using System.Web.SessionState;

using System.Timers;

using System.Net;

using System.IO;

using System.Text;

using System.Threading;

using Maliang;

/// <summary>
///Global 的摘要说明
/// </summary>
public class Global : System.Web.HttpApplication
{
    SqlHelper her = new SqlHelper();
    public Global()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    protected void Application_Start(object sender, EventArgs e)
    {

        //定义定时器  

        System.Timers.Timer myTimer = new System.Timers.Timer(5000);

        myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);

        myTimer.Enabled = true;

        myTimer.AutoReset = true;

    }

    void myTimer_Elapsed(object source, ElapsedEventArgs e)
    {

        try
        {

            //  Log.SaveNote(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":AutoTask is Working!");

            YourTask();

        }

        catch (Exception ee)
        {

            // Log.SaveException(ee);

        }

    }


    void YourTask()
    {
        DateTime dtNow = System.DateTime.Now.AddDays(-3);
        string sql = "update  ML_Order set Oping=1 where OrderState=1 and CreateTime<'" + dtNow + "'";
        her.ExecuteNonQuery(sql);

        //在这里写你需要执行的任务  

    }


    protected void Application_End(object sender, EventArgs e)
    {

        //Log.SaveNote(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":Application End!");  

        //下面的代码是关键，可解决IIS应用程序池自动回收的问题  

        Thread.Sleep(1000);

        //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start  

        string url = "http://jm.nowwin.cn/";

        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

        Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流  

    }
}