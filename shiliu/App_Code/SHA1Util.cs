using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace tenpayApp
{

    /// <summary>
    ///SHA1Util 的摘要说明
    /// </summary>
    public class SHA1Util
    {
        public SHA1Util()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static String getSha1(String str)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[] 
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }
    }
}