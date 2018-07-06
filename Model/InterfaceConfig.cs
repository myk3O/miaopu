using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Model
{
    public class InterfaceConfig
    {
        /// <summary>
        /// ERP为卖家分配的帐号
        /// </summary>
        public static string SellerID = "dev5";

        /// <summary>
        /// ERP为外部接口分配的帐号
        /// </summary>
        public static string InterfaceID = "jiaaotest";

        /// <summary>
        /// KEY
        /// </summary>
        public static string key = "12345";


        /// <summary>
        /// 仓库编号
        /// </summary>
        public static string WarehouseNO = "jiaaotest";

        /// <summary>
        /// 测试环境地址
        /// </summary>
        public static string PostUrl = "http://121.199.38.85/openapi/interface.php";

        /// <summary>
        /// 店铺名称
        /// </summary>
        public static string ShopName = "测试店铺";
    }
}
