<%@ WebHandler Language="C#" Class="UserAuth" %>

using System;
using System.Web;
using WeiPay;
using Newtonsoft.Json;
using System.Web.SessionState;
using Maliang;

public class UserAuth : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public string UserOpenId = ""; //微信用户openid；
    public string headimgurl = "Img/dpgl_19.png";//头像

    public string nickname = "共学";//昵称
    SqlHelper her = new SqlHelper();
    ServerAuth sa = new ServerAuth();
    MemberHelper mb = new MemberHelper();
    public void ProcessRequest(HttpContext context)
    {

        /// <summary>
        /// 获取当前用户的微信 OpenId，如果知道用户的OpenId请不要使用该函数
        /// </summary>

        LogUtil.WriteLog(" ============ 开始 获取微信用户相关信息 =====================");
        try
        {
            LogUtil.WriteLog("agent:[" + context.Request.QueryString["agent"] + "]");
            string code = context.Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                LogUtil.WriteLog(" ============ 开始 获取微信用户相关信息11 =====================");
                Random ra = new Random();
                string state = "STATE" + ra.Next(0, 1000).ToString();
                //Server.UrlEncode(PayConfig.SendUrl);
                string backUrl = HttpUtility.UrlEncode(PayConfig.SendUrl_SQ + "?agent=" + context.Request.QueryString["agent"]);
                string code_url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", PayConfig.AppId, backUrl, state);
                context.Response.Redirect(code_url, false);
            }
            else
            {
                //为了解决40029错误，微信推送两次回发地址，保证code有效，只能不同时去取code，但这样还是有1/5概率相同
                //Random ran = new Random();
                //int RandKey = ran.Next(1000, 2000);
                //LogUtil.WriteLog(RandKey + "毫秒定时开始");
                //System.Threading.Thread.Sleep(RandKey);

                LogUtil.WriteLog(" ============ 开始 获取微信用户相关信息____" + code + " =====================");
                LogUtil.WriteLog("agent:[" + context.Request.QueryString["agent"] + "]");
                #region 获取支付用户 OpenID================
                string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", PayConfig.AppId, PayConfig.AppSecret, code);
                string returnStr = HttpUtil.Send("", url);
                LogUtil.WriteLog("通过code换取网页授权,返回值包含(access_token，openid)：" + returnStr);

                OpenModel obj = new OpenModel();
                try
                {
                    obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);
                }
                catch (Exception e)
                {
                    LogUtil.WriteLog("obj转换错误(UserAuth通过code换取网页授权)" + e.Message + ";  returnStr:" + returnStr);
                }

                //刷新access_token（如果需要）
                //url = string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", PayConfig.AppId, obj.refresh_token);
                //returnStr = HttpUtil.Send("", url);
                //obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);
                if (string.IsNullOrEmpty(obj.openid))
                {
                    LogUtil.WriteLog("停止授权");
                    return;
                    //context.Response.Redirect("Login.aspx");
                    //LogUtil.WriteLog("第一步未获取到openid，5秒定时开始");
                    //System.Threading.Thread.Sleep(5000);//如果失败了，这次就不执行了，让对的执行，吗的对的还是不跳转了！
                    //LogUtil.WriteLog("第一步未获取到openid，5秒定时结束");
                }
                else
                {
                    LogUtil.WriteLog("access_token：" + obj.access_token);
                    LogUtil.WriteLog("openid=" + obj.openid);
                    //第四步：拉取用户信息(需scope为 snsapi_userinfo)
                    url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", obj.access_token, obj.openid);
                    returnStr = HttpUtil.Send("", url);
                    obj = JsonConvert.DeserializeObject<OpenModel>(returnStr);
                    LogUtil.WriteLog("第四步：拉取用户信息：" + returnStr);

                    this.UserOpenId = obj.openid;
                    this.headimgurl = obj.headimgurl;
                    this.nickname = obj.nickname;
                    LogUtil.WriteLog(" ============ 结束 获取微信用户相关信息 =====================");
                    if (!string.IsNullOrEmpty(this.UserOpenId))
                    {
                        LogUtil.WriteLog("详细信息里取到openid:[" + UserOpenId + "]");
                        string userid = string.Empty;
                        if (!mb.Verification(UserOpenId))
                        {
                            LogUtil.WriteLog("注册用户的openid是" + UserOpenId + "；其他参数：" + nickname + "_" + obj.sex + "_" + headimgurl);
                            var fid = "0";
                            if (context.Request.QueryString["agent"] != null && context.Request.QueryString["agent"].ToString() != "")
                            {
                                fid = context.Request.QueryString["agent"].ToString();
                            }
                            LogUtil.WriteLog("agent:[" + fid + "]");
                            if (mb.MemberInsert(UserOpenId, nickname, obj.sex, headimgurl, fid))
                            {
                                LogUtil.WriteLog("注册成功");
                                userid = mb.GetMemberID(this.UserOpenId);
                            }
                            else
                            {
                                LogUtil.WriteLog("注册失败");
                            }
                        }
                        else
                        {
                            LogUtil.WriteLog("已经注册，直接修改信息并登录");

                            userid = mb.GetMemberID(this.UserOpenId);
                            DeletePhoto(context, userid);
                            var touurl = headimgurl.ToString();
                            if (string.IsNullOrEmpty(touurl))
                            {
                                touurl = "http://tv.gongxue168.com/upload_img/getheadimg.jpg";
                            }
                            string pathtou = saveimage(context, touurl);
                            string erma = sa.GetQrcode(userid);
                            string pathMs = context.Server.MapPath("../upload_Img/maseters.jpg");
                            string picName = Guid.NewGuid().ToString() + ".png";
                            string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Pic";   //项目根路径   
                            string SavePath = context.Server.MapPath(strPath + "/" + picName);//保存文件的路径
                            string text = nickname.ToString();

                            CombinImage(pathMs, saveQrcode(context, erma), pathtou, text).Save(SavePath);

                            //已经注册,修改一下
                            //一旦用户的父级不是 总部，则该用户下的订单都属于 同一个父级
                            string sql = "select FatherFXSID from ML_Member where nID=" + userid;
                            var fatherID = "0";
                            if (Convert.ToInt32(her.ExecuteScalar(sql)) > 0)//已经有父级，就用之前的父级别
                            {
                                fatherID = her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
                            }
                            else
                            {
                                if (context.Request.QueryString["agent"] != null && context.Request.QueryString["agent"].ToString() != "")
                                {
                                    fatherID = context.Request.QueryString["agent"].ToString();
                                }
                            }

                            if (mb.MemberUpdate(UserOpenId, nickname, obj.sex, headimgurl, picName, fatherID))
                            {
                                LogUtil.WriteLog("修改成功");
                            }
                            else
                            {
                                LogUtil.WriteLog("修改失败");
                            }
                        }

                        LogUtil.WriteLog("返回的用户Id:" + userid);
                        context.Response.Redirect("Login.aspx?mid=" + userid, false);

                    }
                    else
                    {
                        LogUtil.WriteLog("未获取到openid");
                        //context.Response.Redirect(PayConfig.SendUrl_SQ);
                    }
                }
                #endregion

            }
        }
        catch (Exception ex) { LogUtil.WriteLog("错误日志:" + ex.Message); }




    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// 调用此函数后使此两种图片合并，类似相册，有个  
    /// 背景图，中间贴自己的目标图片  
    /// </summary>
    /// <param name="imgBack">背景模板图片地址</param>
    /// <param name="erma">二维码图片</param>
    /// <param name="tou">头像图片本地路径</param>
    /// <param name="text">文字描述</param>
    /// <returns>最终合成图片</returns>
    public System.Drawing.Image CombinImage(string imgBack, string rm, string tou, string text)
    {
        System.Drawing.Image maseter = System.Drawing.Image.FromFile(imgBack);//背景图
        //System.Drawing.Image img = System.Drawing.Image.FromFile(tou);        //照片图片   
        System.Drawing.Image erma = System.Drawing.Image.FromFile(rm);        //公众号图片   
        if (erma.Height != 270 || erma.Width != 270)
        {
            erma = KiResizeImage(erma, 270, 270, 0);
        }
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(maseter);


        g.DrawImage(maseter, 0, 0, maseter.Width, maseter.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   


        //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  


        //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  

        //g.DrawImage(erma, maseter.Width / 2 - erma.Width / 2, 300, erma.Width, erma.Height);
        g.DrawImage(erma, maseter.Width / 2 - erma.Width / 2 - 25, 200, erma.Width, erma.Height);
        //g.DrawImage(img, 45, 60, img.Width, img.Height);

        //加文字
        float fontSize = 30.0f;    //字体大小  
        float textWidth = text.Length * fontSize;  //文本的长度  
        //下面定义一个矩形区域，以后在这个矩形里画上白底黑字  
        //float rectX = img.Width + 130;
        float rectX = 380;
        float rectY = 55;
        float rectWidth = text.Length * (fontSize + 8);
        float rectHeight = fontSize + 8;
        //声明矩形域  
        System.Drawing.RectangleF textArea = new System.Drawing.RectangleF(rectX, rectY, rectWidth, rectHeight);

        System.Drawing.Font font = new System.Drawing.Font("宋体", fontSize, System.Drawing.FontStyle.Bold);   //定义字体  
        System.Drawing.Brush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, System.Drawing.Color.White));   //白笔刷，画背景用   
        System.Drawing.Brush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);   //黑笔刷，画文字用   

        g.FillRectangle(whiteBrush, rectX, rectY, rectWidth, rectHeight);

        g.DrawString(text, font, blackBrush, textArea);
        GC.Collect();

        return maseter;

    }


    /// <summary>  
    /// Resize图片  
    /// </summary>  
    /// <param name="bmp">原始Bitmap</param>  
    /// <param name="newW">新的宽度</param>  
    /// <param name="newH">新的高度</param>  
    /// <param name="Mode">保留着，暂时未用</param>  
    /// <returns>处理以后的图片</returns>  
    public System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
    {
        try
        {
            System.Drawing.Image b = new System.Drawing.Bitmap(newW, newH);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);

            // 插值算法的质量  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;


            g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, newW, newH), new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            //bmp.Save(Server.MapPath("/Images/tou.jpg"));
            return b;
        }
        catch
        {
            return null;
        }

    }

    public string saveQrcode(HttpContext context, string url)
    {
        System.Net.WebClient mywebclient = new System.Net.WebClient();

        string newfilename = Guid.NewGuid().ToString() + ".jpg";
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Qcode";   //项目根路径   
        string filepath = context.Server.MapPath(strPath + "/" + newfilename);//保存文件的路径

        try
        {
            mywebclient.DownloadFile(url, filepath);
            //filename = newfilename;
        }
        catch (Exception ex)
        {
            // MessageBox.Show(ex.ToString());
        }
        return filepath;
    }

    public string saveimage(HttpContext context, string url)
    {
        System.Net.WebClient mywebclient = new System.Net.WebClient();
        // string url = "http://wx.qlogo.cn/mmopen/ajNVdqHZLLD4M3icnlzMp7RIG6TiaG9ILLpBH118BibOWa2BR2cdicrTjfEP92gvMjMjEq6iaxaAicCozPaGKleELryA/0";
        // string newfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".jpg";

        string newfilename = Guid.NewGuid().ToString() + ".jpg";
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/tou";   //项目根路径   
        string filepath = context.Server.MapPath(strPath + "/" + newfilename);//保存文件的路径

        // string filepath = Server.MapPath("/Images/" + newfilename);
        try
        {
            mywebclient.DownloadFile(url, filepath);
            //filename = newfilename;
        }
        catch (Exception ex)
        {
            // MessageBox.Show(ex.ToString());
        }
        return filepath;
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
    public void DeletePhoto(HttpContext context, string ID)
    {
        string str = "";
        string sql = "select fxsImg from ML_Member where nID=" + ID;
        str = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Pic";   //项目根路径   
            string fullname = context.Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }

}