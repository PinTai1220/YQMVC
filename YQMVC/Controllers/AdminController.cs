using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using YQMVC.Helper;
using YQMVC.Models;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 管理员表
    /// </summary>
    public class AdminController : Controller
    {
        //登录
        // GET: Admin
        //public ActionResult Login()
        //{
        //    Dictionary<string, string> datas = new Dictionary<string, string>();
        //    string signature = DataTransfer.DataTransfer.GetMD5Staff(datas);       // 获得公钥
        //    int nonce = DataTransfer.DataTransfer.GetNonce();            // 获取随机数
        //    string timestamp = DataTransfer.DataTransfer.GetTimeStamp();        // 获取时间戳
        //    HttpClientHelper.SendRequest("", "", timestamp, nonce.ToString(), signature, "");

        //    return View();
        //}

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string pwd)
        {
            // 去掉前后空格
            userName = userName.TrimStart(' ').TrimEnd(' ');
            pwd = pwd.TrimStart(' ').TrimEnd(' ');


            #region Api 验证必填信息
            // 传入数据
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");


            string nonce = DataTransfer.GetNonce().ToString();            // 获取随机数
            string timestamp = DataTransfer.GetTimeStamp();        // 获取时间戳
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);       // 获得公钥
            #endregion

            string result = HttpClientHelper.SendRequest("api/Admin/Show", "get", timestamp, nonce, signature, "");
            List<Admin> admins = JsonConvert.DeserializeObject<List<Admin>>(result);

            Admin admin = admins.Where(c => c.Admin_Name.Equals(userName) && c.Admin_Pwd.Equals(pwd)).FirstOrDefault();

            if (admin != null)
            {
                return Redirect("/Admin/Index");
            }
            else
            {
                return Content("<script>alert('密码或用户名错误！');location.reload()</script>");
            }
        }

    }
}