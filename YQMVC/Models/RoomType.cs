using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YQMVC.Models
{
    /// <summary>
    /// 房间类型表
    /// </summary>
    public class RoomType
    {
        public int RoomType_Id { get; set; }//房间类型编号
        public string RoomType_Name { get; set; }//房间类型名称
        public decimal RoomType_Price { get; set; }//房间价格
    }
}