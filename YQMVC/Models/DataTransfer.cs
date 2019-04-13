using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace YQMVC.Models
{
    public static class DataTransfer
    {
        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <returns></returns>
        public static int GetNonce()
        {
            Random random = new Random();
            return (int)(Math.Ceiling(random.NextDouble() * 1000.0));
        }


        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 将传输数据以 Key  Value 的形式传入
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string DictionaryOrderWithData(Dictionary<string, string> dic)
        {

            var result = "";
            var value = "";
            List<string> strs = new List<string>();
            foreach (var item in dic)
            {
                strs.Add(item.Key);
            }
            foreach (var item in strs)
            {
                if (dic[item] is null)
                {
                    value = "";
                }
                else
                {
                    value = dic[item];
                }
                result += item + value;
            }
            return result.Replace(" ", "");
        }

        /// <summary>
        /// 获得 MD5 加密的 公钥
        /// </summary>
        /// <param name="queryData">发送的数据  以 Key Value 的形式传入</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns></returns>
        public static string GetMD5Staff(Dictionary<string, string> queryData,string timestamp,string nonce)
        {
            string staffId = "^***********************************$";       // 密钥
            string data = DictionaryOrderWithData(queryData);
            string str = timestamp + nonce + staffId + data;
            string result = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToUpper();
            return result;
        }

        #region MD5加密

        // string 的扩展方法：必须声明在static类中
        /// <summary>
        /// 根据string 字符串进行 MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CalcMD5(this string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }
        /// <summary>
        /// 根据 byte 数组进行MD5 加密
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" +
                    computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }

        /// <summary>
        /// 根据 流进行MD5加密
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" +
                    computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }

        /// <summary>
        /// 生成验证码封装一个生成 n 位验证码的函数，为了避免混淆，不生成'1'、 'l'、 '0'、 'o'等字符
        /// </summary>
        /// <returns></returns>
        public static string GenerateCaptchaCode(int len)
        {
            char[] data = { 'a', 'c', 'd', 'e', 'f', 'g', 'k', 'm', 'p', 'r', 's', 't', 'w', 'x', 'y', '3', '4', '5', '7', '8' };
            StringBuilder sbCode = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                char ch = data[rand.Next(data.Length)];             //[0,data.length)
                sbCode.Append(ch);
            }
            return sbCode.ToString();
        }

        #endregion
    }
}