using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web;

/// <summary>
/// SendEmail 发送邮件
/// </summary>
public class SendEmail
{
    public SendEmail()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 邮件发送
    /// </summary>
    /// <param name="Femail">发件人的邮箱</param>
    /// <param name="Fname">发件人的名称</param>
    /// <param name="Fpass">发件人的密码</param>
    /// <param name="email">收件人的邮箱</param>
    /// <param name="body">邮件内容</param>
    /// <param name="subject">邮件标题</param>
    /// <returns></returns>
    public static bool SendEmails(string Femail, string Fname, string Fpass, string email, string body, string subject)
    {
        jmail.Message jmailmessage = new jmail.Message();
        DateTime t = DateTime.Now;
        string FromEmail = Femail;//你的email地址
        string ToEmail = email;//发送到对方的邮箱
        //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false 
        jmailmessage.Silent = true;
        //Jmail创建的日志，前提loging属性设置为true
        jmailmessage.Logging = false;
        //字符集，缺省为"US-ASCII"
        jmailmessage.Charset = "UTF-8";
        //添加收件人
        jmailmessage.AddRecipient(ToEmail, "", "");
        //设置自己的邮箱
        jmailmessage.From = FromEmail;
        //给对方看的昵称
        jmailmessage.FromName = Fname;
        //发件人邮件用户名
        jmailmessage.MailServerUserName = FromEmail;//FromEmail.Substring(0, FromEmail.IndexOf('@'));
        //发件人邮件密码
        jmailmessage.MailServerPassWord = Fpass;
        //设置邮件标题
        jmailmessage.Subject = subject;
        //设置邮件优先级
        jmailmessage.Priority = 1;//1--5  越小优先级越高
        //邮件内容
        //jmailmessage.Body = body;
        jmailmessage.HTMLBody = body;
        //Jmail发送的方法(mailServer:pop3.163.com/smtp.163.com)
        //bool temp = jmailmessage.Send("smtp.163.com", false); //使用163邮箱做为发件人
        bool temp = jmailmessage.Send("smtp.exmail.qq.com", false);//使用QQ邮箱做为发件人
        jmailmessage.ClearAttachments();
        jmailmessage.ClearRecipients();
        jmailmessage.Close();
        if (temp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    static string strHost = String.Empty;
    static string strAccount = String.Empty;
    static string strPwd = String.Empty;
    static string strFrom = String.Empty;
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="Femail">发件人的邮箱</param>
    /// <param name="Fname">发件人的名称</param>
    /// <param name="Fpass">发件人的密码</param>
    /// <param name="to">接收方邮件地址</param>
    /// <param name="title">邮件标题</param>
    /// <param name="content">邮件正文内容</param>
    public static bool sendmail(string Femail, string Fname, string Fpass, string to, string content, string title, string stmps)
    {
        //strHost = "smtp.webjx.com";   //STMP服务器地址
        //strAccount = "abc@webjx.com";       //SMTP服务帐号
        //strPwd = "password";       //SMTP服务密码
        //strFrom = "chenjun@webjx.com";  //发送方邮件地址
        strHost = stmps;   //STMP服务器地址
        strAccount = Femail;       //SMTP服务帐号
        strPwd = Fpass;       //SMTP服务密码
        strFrom = Femail;  //发送方邮件地址

        SmtpClient _smtpClient = new SmtpClient();
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
        _smtpClient.Host = strHost; ;//指定SMTP服务器
        _smtpClient.Credentials = new System.Net.NetworkCredential(strAccount, strPwd);//用户名和密码
        MailMessage _mailMessage = new MailMessage(strFrom, to);
        //_mailMessage.From = new MailAddress(strFrom);
        _mailMessage.Subject = title;//主题
        _mailMessage.Body = content;//内容
        _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;//正文编码
        _mailMessage.IsBodyHtml = true;//设置为HTML格式
        _mailMessage.Priority = MailPriority.High;//优先级

        try
        {
            _smtpClient.Send(_mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }
}