using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YQMVC.Models
{
    /// <summary>
    /// 管理员表
    /// </summary>
    public class Admin
    {
        public int Admin_Id { get; set; }//管理员编号
        public string Admin_Name { get; set; }//管理员名称
        public string Admin_Pwd { get; set; }//管理员密码
        public string Role_Id { get; set; }//角色编号
    }
}