using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiPay;

public partial class Wap_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ServerAuth sa = new ServerAuth();
        var touurl = "http://wx.qlogo.cn/mmopen/icE1QGMQwDGFjFdfUk8X1Ueiczt2xPgcQyCPUnj2TJdbCib672lrLJqNdQ9cJec73giasN7v4bodia7CgDyJ9UwarEHONyqLJus66/0";
        string pathtou = saveimage(touurl);
        string erma = sa.GetQrcode("74");
        string pathMs = Server.MapPath("../upload_Img/maseter.jpg");
        string picName = Guid.NewGuid().ToString() + ".png";
        string strPath = HttpContext.Current.Request.FilePath + "/../../upload_Img/Pic";   //项目根路径   
        string SavePath = Server.MapPath(strPath + "/" + picName);//保存文件的路径
        string text = "我是谁";

        CombinImage(pathMs, saveQrcode(erma), pathtou, text).Save(SavePath);
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
        System.Drawing.Image img = System.Drawing.Image.FromFile(tou);        //照片图片   
        System.Drawing.Image erma = System.Drawing.Image.FromFile(rm);        //公众号图片   
        if (img.Height != 90 || img.Width != 90)
        {
            img = KiResizeImage(img, 90, 90, 0);
        }
        Graphics g = Graphics.FromImage(maseter);


        g.DrawImage(maseter, 0, 0, maseter.Width, maseter.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   


        //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  


        //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  

        g.DrawImage(erma, maseter.Width / 2 - erma.Width / 2, 300, erma.Width, erma.Height);

        g.DrawImage(img, 45, 60, img.Width, img.Height);

        //加文字
        float fontSize = 25.0f;    //字体大小  
        float textWidth = text.Length * fontSize;  //文本的长度  
        //下面定义一个矩形区域，以后在这个矩形里画上白底黑字  
        float rectX = img.Width + 130;
        float rectY = 65;
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
}