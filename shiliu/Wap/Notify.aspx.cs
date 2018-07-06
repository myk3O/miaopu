using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using WeiPay;
using ThoughtWorks.QRCode.Codec;
using Maliang;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;

/**
 * 
 * 作用：支付完成以后通知页面，该页面实现数据库的更新操作，比如更新订单状态等等
 * 作者：张岳鹏
 * 编写日期：2014-12-25
 * 备注：请注意更新代码的填写位置
 * 
 * */
public partial class Wap_Notify : System.Web.UI.Page
{
    ServerAuth sa = new ServerAuth();
    PayHelp ph = new PayHelp();
    SqlHelper her = new SqlHelper();
    MemberHelper Menber = new MemberHelper();
    Order or = new Order();
    protected void Page_Load(object sender, EventArgs e)
    {
        //创建ResponseHandler实例
        ResponseHandler resHandler = new ResponseHandler(Context);
        resHandler.setKey(PayConfig.AppKey);
        LogUtil.WriteLog("Notify 页面  支付成功");

        //判断签名
        try
        {
            string error = "";
            if (resHandler.isWXsign(out error))
            {
                #region 协议参数=====================================
                //--------------协议参数--------------------------------------------------------
                //SUCCESS/FAIL此字段是通信标识，非交易标识，交易是否成功需要查
                string return_code = resHandler.getParameter("return_code");
                //返回信息，如非空，为错误原因签名失败参数格式校验错误
                string return_msg = resHandler.getParameter("return_msg");
                //微信分配的公众账号 ID
                string appid = resHandler.getParameter("appid");

                //以下字段在 return_code 为 SUCCESS 的时候有返回--------------------------------
                //微信支付分配的商户号
                string mch_id = resHandler.getParameter("mch_id");
                //微信支付分配的终端设备号
                string device_info = resHandler.getParameter("device_info");
                //微信分配的公众账号 ID
                string nonce_str = resHandler.getParameter("nonce_str");
                //业务结果 SUCCESS/FAIL
                string result_code = resHandler.getParameter("result_code");
                //错误代码 
                string err_code = resHandler.getParameter("err_code");
                //结果信息描述
                string err_code_des = resHandler.getParameter("err_code_des");

                //以下字段在 return_code 和 result_code 都为 SUCCESS 的时候有返回---------------
                //-------------业务参数---------------------------------------------------------
                //用户在商户 appid 下的唯一标识
                string openid = resHandler.getParameter("openid");
                //用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
                string is_subscribe = resHandler.getParameter("is_subscribe");
                //JSAPI、NATIVE、MICROPAY、APP
                string trade_type = resHandler.getParameter("trade_type");
                //银行类型，采用字符串类型的银行标识
                string bank_type = resHandler.getParameter("bank_type");
                //订单总金额，单位为分
                string total_fee = resHandler.getParameter("total_fee");
                //货币类型，符合 ISO 4217 标准的三位字母代码，默认人民币：CNY
                string fee_type = resHandler.getParameter("fee_type");
                //微信支付订单号
                string transaction_id = resHandler.getParameter("transaction_id");
                //商户系统的订单号，与请求一致。
                string out_trade_no = resHandler.getParameter("out_trade_no");
                //商家数据包，原样返回
                string attach = resHandler.getParameter("attach");
                //支 付 完 成 时 间 ， 格 式 为yyyyMMddhhmmss，如 2009 年12 月27日 9点 10分 10 秒表示为 20091227091010。时区为 GMT+8 beijing。该时间取自微信支付服务器
                string time_end = resHandler.getParameter("time_end");

                #endregion

                //支付成功
                // LogUtil.WriteLog("return_code" + return_code);
                string[] arr = attach.Split(',');
                var uid = arr[0];
                var fid = arr[1] == null ? "0" : arr[1].ToString() == "" ? "0" : arr[1].ToString();
                var price = arr[2];
                var ordercode = arr[3];
                var pid = arr[4];
                // LogUtil.WriteLog(uid + ";" + fid + ";" + price + ";" + ordercode + ";" + pid);


                Video vd = new Video();
                if (!string.IsNullOrEmpty(vd.GetVideoPrice(pid)))//查不到才用传过来的
                {
                    price = vd.GetVideoPrice(pid);
                }
                //判断是否已经是会员
                if (ph.Memberisfxs(uid))
                {
                    //再次购买，产品打1折
                    price = (Convert.ToDouble(price) * 0.1).ToString();

                }
                else
                {
                    //生成二维码,用户ID，FatherFXSID，
                    DeletePhoto(uid);//删除原有图片

                    string sql = "select headimgurl,nickname from ML_Member where nID=" + uid;
                    DataTable dt = her.ExecuteDataTable(sql);
                    var url = makeUrl(uid);
                    var picName = "";
                    if (dt.Rows.Count > 0)
                    {
                        var touurl = dt.Rows[0]["headimgurl"].ToString();
                        if (string.IsNullOrEmpty(touurl))
                        {
                            touurl = "http://tv.gongxue168.com/upload_img/getheadimg.jpg";
                        }
                        string pathtou = saveimage(touurl);
                        string erma = sa.GetQrcode(uid);
                        string pathMs = Server.MapPath("../upload_Img/maseters.jpg");
                        picName = Guid.NewGuid().ToString() + ".png";
                        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Pic";   //项目根路径   
                        string SavePath = Server.MapPath(strPath + "/" + picName);//保存文件的路径
                        string text = dt.Rows[0]["nickname"].ToString();

                        CombinImage(pathMs, saveQrcode(erma), pathtou, text).Save(SavePath);
                    }
                    //else
                    //{
                    //    picName = ImageAdd(url);
                    //}



                    bool success = Menber.MemberUpdateFxs(uid, 1, url, picName);//支付的时候就不改变，上级来源
                    if (success)
                    {
                        LogUtil.WriteLog("微店成功");
                    }
                    else
                    {
                        LogUtil.WriteLog("微店失败");
                    }
                }



                //插入订单
                LogUtil.WriteLog("notify:插入订单");
                if (ph.IsOrder(ordercode) == false)//已经存在就不要入库了
                {
                    bool falg = false;
                    tongji tj = new tongji();

                    string fatherID = "0";
                    //一旦用户的父级不是 总部，则该用户下的订单都属于 同一个父级
                    string sql = "select FatherFXSID from ML_Member where nID=" + uid;
                    if (Convert.ToInt32(her.ExecuteScalar(sql)) > 0)//已经有父级，就用之前的父级别
                    {
                        fatherID = her.ExecuteScalar(sql) == null ? "0" : her.ExecuteScalar(sql).ToString();
                    }


                    int pri = Convert.ToInt32(price);//测试结束，要注销掉* 10000
                    //插入订单
                    falg = ph.InsertOrder(uid, fatherID, ordercode, pri, pid, "微信支付");
                    LogUtil.WriteLog("notify:" + falg.ToString() + "订单号：" + ordercode + "__用户ID：" + uid);
                    //判断是否已经是会员
                    if (ph.Memberisfxs(uid) == false)
                    {

                        if (or.UpdateUserState(uid))
                        {
                            LogUtil.WriteLog("notify:会员成功");
                        }
                        else
                        {
                            LogUtil.WriteLog("notify:会员失败");
                        }
                    }
                    else
                    {
                        LogUtil.WriteLog("notify:已经是会员不更新");
                    }
                    LogUtil.WriteLog("佣金分配");
                    tj.insetTop1User(Convert.ToInt32(uid), Convert.ToInt32(fatherID), pri);
                    tj.insetTop2User(Convert.ToInt32(uid), Convert.ToInt32(fatherID), pri);
                    tj.insetTop3User(Convert.ToInt32(uid), Convert.ToInt32(fatherID), pri);
                    LogUtil.WriteLog("绩效分配");
                    tj.InsertJxBZ(Convert.ToInt32(uid), pri);
                    tj.InsertJxBZE(Convert.ToInt32(uid), pri);
                    tj.InsertJxXZ(Convert.ToInt32(uid), pri);
                    //3.会员等级升级
                    DateTime dt = System.DateTime.Now;
                    string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    if (tj.UpdateLevel1())
                    {
                        WeiPay.LogUtil.WriteLog("班长查询，更新成功!" + time);
                    }
                    if (tj.UpdateLevel2())
                    {
                        WeiPay.LogUtil.WriteLog("班主任查询，更新成功!" + time);
                    }
                    if (tj.UpdateLevel3())
                    {
                        WeiPay.LogUtil.WriteLog("校长查询，更新成功!" + time);
                    }
                }




                if (!out_trade_no.Equals("") && return_code.Equals("SUCCESS") && result_code.Equals("SUCCESS"))
                {
                    //LogUtil.WriteLog("Notify 页面  支付成功，支付信息：商家订单号：" + out_trade_no + "、支付金额(分)：" + total_fee + "、自定义参数：" + attach);

                    /**
                     *  这里输入用户逻辑操作，比如更新订单的支付状态**/
                    //or.UpdateUserState(attach);

                    /** * **/
                    //插入订单

                    //LogUtil.WriteLog("============ 单次支付结束 ===============");
                    Response.Write("success");
                    return;
                }
                else
                {
                    LogUtil.WriteLog("Notify 页面  支付失败，支付信息   total_fee= " + total_fee + "、err_code_des=" + err_code_des + "、result_code=" + result_code);
                }
            }
            else
            {
                LogUtil.WriteLog("Notify 页面  isWXsign= false ，错误信息：" + error);
            }


        }
        catch (Exception ee)
        {
            LogUtil.WriteLog("Notify 页面  发送异常错误：" + ee.Message);
        }

        Response.End();
    }


    private string makeUrl(string nID)
    {
        //string url = HttpContext.Current.Request.Url.Host;
        string url = "http://tv.gongxue168.com/";
        string aurl = url + "Wap/Index.aspx?agent=" + nID;
        return aurl;
    }


    /// <summary>  
    /// 生成二维码.  
    /// </summary>  
    /// <param name="data">需要添加进去的文本</param>  
    /// <returns></returns>  
    public System.Drawing.Image GCode(String data)
    {
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;


        qrCodeEncoder.QRCodeScale = 5;
        qrCodeEncoder.QRCodeVersion = 7;


        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
        var pbImg = qrCodeEncoder.Encode(data, System.Text.Encoding.UTF8);
        var width = pbImg.Width / 10;
        var dwidth = width * 2;
        Bitmap bmp = new Bitmap(pbImg.Width + dwidth, pbImg.Height + dwidth);
        Graphics g = Graphics.FromImage(bmp);
        var c = System.Drawing.Color.White;
        g.FillRectangle(new SolidBrush(c), 0, 0, pbImg.Width + dwidth, pbImg.Height + dwidth);
        g.DrawImage(pbImg, width, width);
        g.Dispose();

        string filename = Guid.NewGuid().ToString() + ".png";
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Pic";   //项目根路径   
        string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径

        //bmp.Save(fullname);
        return bmp;
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
        // System.Drawing.Image img = System.Drawing.Image.FromFile(tou);        //照片图片   
        System.Drawing.Image erma = System.Drawing.Image.FromFile(rm);        //公众号图片   
        if (erma.Height != 270 || erma.Width != 270)
        {
            erma = KiResizeImage(erma, 270, 270, 0);
        }
        Graphics g = Graphics.FromImage(maseter);


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
        RectangleF textArea = new RectangleF(rectX, rectY, rectWidth, rectHeight);

        Font font = new Font("宋体", fontSize, FontStyle.Bold);   //定义字体  
        Brush whiteBrush = new SolidBrush(Color.FromArgb(0, Color.White));   //白笔刷，画背景用   
        Brush blackBrush = new SolidBrush(Color.Red);   //黑笔刷，画文字用   

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
            System.Drawing.Image b = new Bitmap(newW, newH);
            Graphics g = Graphics.FromImage(b);

            // 插值算法的质量  
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;


            g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.Dispose();

            //bmp.Save(Server.MapPath("/Images/tou.jpg"));
            return b;
        }
        catch
        {
            return null;
        }

    }

    public string saveQrcode(string url)
    {
        WebClient mywebclient = new WebClient();

        string newfilename = Guid.NewGuid().ToString() + ".jpg";
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Qcode";   //项目根路径   
        string filepath = Server.MapPath(strPath + "/" + newfilename);//保存文件的路径

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

    public string saveimage(string url)
    {
        WebClient mywebclient = new WebClient();
        // string url = "http://wx.qlogo.cn/mmopen/ajNVdqHZLLD4M3icnlzMp7RIG6TiaG9ILLpBH118BibOWa2BR2cdicrTjfEP92gvMjMjEq6iaxaAicCozPaGKleELryA/0";
        // string newfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".jpg";

        string newfilename = Guid.NewGuid().ToString() + ".jpg";
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/tou";   //项目根路径   
        string filepath = Server.MapPath(strPath + "/" + newfilename);//保存文件的路径

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


    private string ImageAdd(string str)
    {
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        qrCodeEncoder.QRCodeScale = 4;
        qrCodeEncoder.QRCodeVersion = 8;
        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        String data = str;
        //生成二维码
        System.Drawing.Bitmap image = qrCodeEncoder.Encode(data);
        //上传图片
        string filename = Guid.NewGuid().ToString() + ".png";
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Pic";   //项目根路径   
        string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        //string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        //string strPath = HttpContext.Current.Request.FilePath + "/../skin/images";   //项目根路径   
        //string fullname = Server.MapPath(strPath + "/" + filename);//保存文件的路径
        image.Save(fullname);
        return filename;
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
    public void DeletePhoto(string ID)
    {
        string str = "";
        string sql = "select fxsImg from ML_Member where nID=" + ID;
        str = her.ExecuteScalar(sql) == null ? "" : her.ExecuteScalar(sql).ToString();
        if (str != "")
        {
            string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Pic";   //项目根路径   
            string fullname = Server.MapPath(strPath + "/" + str);//保存文件的路径
            DeleteOldAttach(fullname);
        }
    }
}
