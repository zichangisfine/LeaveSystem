using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Runtime;

namespace 請假系統.Models
{
    public class LeaveTable
    {
        public int 序號 { get; set; }
        public string 員編 { get; set; }
        public string 開始日期 { get; set; }
        public string 開始時間 { get; set; }
        public string 結束日期 { get; set; }
        public string 結束時間 { get; set; }
        public string 假別 { get; set; }
        public string 假別子項目 { get; set; }
        public string 送審主管 { get; set; }
        
    }
}