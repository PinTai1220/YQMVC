using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YQMVC.Models
{
    /// <summary>
    /// 金额表
    /// </summary>
    public class Money
    {
        public int Money_Id { get; set; }//金额编号
        public decimal Money_Num { get; set; }//金额数目
        public DateTime Recharge_Time { get; set; }//充值时间
        public int User_Id { get; set; }//用户编号
    }
}