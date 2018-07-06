using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiPay
{   /// <summary>
    /// 公众号二维码
    /// </summary>
    public class Qrcode
    {
        public string ticket { get; set; }
        public string expire_seconds { get; set; }
        public string url { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
