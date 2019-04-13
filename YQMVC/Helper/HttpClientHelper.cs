using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace YQMVC.Helper
{
    public class HttpClientHelper
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="url">访问的路径</param>
       /// <param name="method">访问的方法</param>
       /// <param name="timestamp">时间戳</param>
       /// <param name="nonce">随机数</param>
       /// <param name="signature">公钥</param>
       /// <param name="data">传输的数据</param>
       /// <returns></returns>
        public static string SendRequest(string url, string method, string timestamp, string nonce, string signature, string data = "")
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:54830/"); //设置http请求的地址
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//设置请求的数据传输格式
          
            #region 自定义请求头
            {
                client.DefaultRequestHeaders.Add("timestamp", timestamp);
                client.DefaultRequestHeaders.Add("nonce", nonce);
                client.DefaultRequestHeaders.Add("signature", signature);
            }
            #endregion

            HttpContent content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");//设置发送的数据格式
            


            var strVal = "";

            HttpResponseMessage response = null;

            switch (method.Trim().ToLower())
            {
                case "get":
                    response = client.GetAsync(url).Result;
                    break;
                case "post":
                    //接收http请求返回的结果信息
                    response = client.PostAsync(url, content).Result;
                    break;
                case "put":
                    response = client.PutAsync(url, content).Result;
                    break;
                case "delete":
                    response = client.DeleteAsync(url).Result;
                    break;
            }
            //接收http请求返回的结果信息

            if (response.IsSuccessStatusCode)
            {
                //将文本数据流转为传输的json数据格式
                strVal = response.Content.ReadAsStringAsync().Result;
            }
            //释放掉http请求对象
            client.Dispose();

            return strVal;
        }

    }
}