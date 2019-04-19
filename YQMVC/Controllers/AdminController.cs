using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using YQMVC.Helper;
using YQMVC.Models;
using YQMVC.Filter;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 管理员表
    /// </summary>
    public class AdminController : Controller
    {
        //主页显示
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        //登录方法
        [HttpPost]
        [AllowAnonymous]
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
                Session["Login"] = admin;
                return Redirect("/Admin/Index");
            }
            else
            {
                return Content("<script>alert('密码或用户名错误！');location.reload()</script>");
            }
        }

    }
}