using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YQMVC.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class UserInfos
    {
        public int UserInfo_Id { get; set; }//用户信息编号
        public string UserInfo_Name { get; set; }//姓名
        public int UserInfo_Sex { get; set; }//性别
        public string ID_Num{ get; set; }//身份证号
        public string Phone_Num { get; set; }//手机号
        public string Address { get; set; }//当前位置
        public string HeadImg { get; set; }//用户头像

        public int orderid { get; set; }
        public int state { get; set; }
    }
}