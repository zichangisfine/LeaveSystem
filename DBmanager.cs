using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Globalization;


namespace 請假系統.Models
{
    public class DBmanager
    {
        private readonly string ConnStr = "server=localhost;port=3306;database=LeaveSystem;user=root;password=jc314159265jc;allow zero datetime=true;allow User Variables=True";
        
        public List<LeaveTable> GetLeaveTable()
        {
            List<LeaveTable> leaveTables = new List<LeaveTable>();
            MySqlConnection mySqlConnection = new MySqlConnection(ConnStr);
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM leavetable ORDER BY 序號");
            mySqlCommand.Connection = mySqlConnection;
            mySqlConnection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();


            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    LeaveTable leave = new LeaveTable
                    {
                        
                        序號 = reader.GetInt32(reader.GetOrdinal("序號")),
                        員編 = reader.GetString(reader.GetOrdinal("員編")),
                        開始日期 = reader.GetString(reader.GetOrdinal("開始日期")).Split(' ')[0],
                        開始時間 = reader.GetString(reader.GetOrdinal("開始時間")),
                        結束日期 = reader.GetString(reader.GetOrdinal("結束日期")).Split(' ')[0],
                        結束時間 = reader.GetString(reader.GetOrdinal("結束時間")),
                        假別 = reader.GetString(reader.GetOrdinal("假別")),
                        假別子項目 = reader.GetString(reader.GetOrdinal("假別子項目")) ?? Convert.ToString(' '),
                        送審主管 = reader.GetString(reader.GetOrdinal("送審主管")),
                    };
                    leaveTables.Add(leave);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            mySqlConnection.Close();
            return leaveTables;
        }
        public void NewLeave(LeaveTable leave)  
        {
            MySqlConnection mySqlConnection = new MySqlConnection(ConnStr);
            MySqlCommand mySqlCommand = new MySqlCommand (
                @"INSERT INTO leavetable (員編,開始日期,開始時間,結束日期,結束時間,假別,假別子項目,送審主管)
                    VALUES (@員編,@開始日期,@開始時間,@結束日期,@結束時間,@假別,@假別子項目,@送審主管)");
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.Parameters.Add(new MySqlParameter("@員編", leave.員編));
            mySqlCommand.Parameters.Add(new MySqlParameter("@開始日期", leave.開始日期));
            mySqlCommand.Parameters.Add(new MySqlParameter("@開始時間", leave.開始時間));
            mySqlCommand.Parameters.Add(new MySqlParameter("@結束日期", leave.結束日期));
            mySqlCommand.Parameters.Add(new MySqlParameter("@結束時間", leave.結束時間));
            mySqlCommand.Parameters.Add(new MySqlParameter("@假別", leave.假別));
            if (leave.假別子項目 != null)
            {
                mySqlCommand.Parameters.Add(new MySqlParameter("@假別子項目", leave.假別子項目));
            }
            else
            {
                mySqlCommand.Parameters.Add(new MySqlParameter("@假別子項目", " "));
            }
            mySqlCommand.Parameters.Add(new MySqlParameter("@送審主管", leave.送審主管));
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public LeaveTable GetLeaveById(int 序號)
        {
            LeaveTable leave = new LeaveTable();
            MySqlConnection mySqlConnection = new MySqlConnection(ConnStr);
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM leavetable WHERE 序號 = @序號");
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.Parameters.Add(new MySqlParameter("@序號",序號));
            mySqlConnection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    leave = new LeaveTable
                    {
                        序號 = reader.GetInt32(reader.GetOrdinal("序號")),
                        員編 = reader.GetString(reader.GetOrdinal("員編")),
                        開始日期 = reader.GetString(reader.GetOrdinal("開始日期")).Split(' ')[0],
                        開始時間 = reader.GetString(reader.GetOrdinal("開始時間")),
                        結束日期 = reader.GetString(reader.GetOrdinal("結束日期")).Split(' ')[0],
                        結束時間 = reader.GetString(reader.GetOrdinal("結束時間")),
                        假別 = reader.GetString(reader.GetOrdinal("假別")),
                        假別子項目 = reader.GetString(reader.GetOrdinal("假別子項目")) ?? Convert.ToString(' '),
                        送審主管 = reader.GetString(reader.GetOrdinal("送審主管")),
                    };
            }
            }
            else
            {
                leave.假別 = ("未找到");
            }
            mySqlConnection.Close();
            return leave;
        }

        public void UpdateLeave(LeaveTable leave)
        {
                MySqlConnection mySqlConnection = new MySqlConnection(ConnStr);
                MySqlCommand mySqlCommand = new MySqlCommand(
                    @"UPDATE leavetable SET 員編 = @員編,開始日期 = @開始日期,開始時間 = @開始時間,結束日期 = @結束日期,結束時間 = @結束時間,假別 = @假別,假別子項目 = @假別子項目,送審主管 = @送審主管 WHERE 序號 = @序號");
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.Parameters.Add(new MySqlParameter("@序號", leave.序號));
                mySqlCommand.Parameters.Add(new MySqlParameter("@員編", leave.員編));
                mySqlCommand.Parameters.Add(new MySqlParameter("@開始日期", leave.開始日期));
                mySqlCommand.Parameters.Add(new MySqlParameter("@開始時間", leave.開始時間));
                mySqlCommand.Parameters.Add(new MySqlParameter("@結束日期", leave.結束日期));
                mySqlCommand.Parameters.Add(new MySqlParameter("@結束時間", leave.結束時間));
                mySqlCommand.Parameters.Add(new MySqlParameter("@假別", leave.假別));
                if (leave.假別子項目 != null)
                {
                    mySqlCommand.Parameters.Add(new MySqlParameter("@假別子項目", leave.假別子項目));
                }
                else
                {
                    mySqlCommand.Parameters.Add(new MySqlParameter("@假別子項目", " "));
                }
                mySqlCommand.Parameters.Add(new MySqlParameter("@送審主管", leave.送審主管));
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
        }

        public void DeleteLeaveById(int 序號)
        {
            MySqlConnection mySqlConnection = new MySqlConnection(ConnStr);
            MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM leavetable WHERE 序號 = @序號");
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.Parameters.Add(new MySqlParameter("@序號", 序號));
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
        public List<LeaveTable> SearchLeaveById(string 員編)
        {
            List<LeaveTable> leaveTables = new List<LeaveTable>();
            MySqlConnection mySqlConnection = new MySqlConnection(ConnStr);
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM leavetable WHERE 員編 = @員編");
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.Parameters.Add(new MySqlParameter("@員編", 員編));
            mySqlConnection.Open();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    LeaveTable leave = new LeaveTable
                    {
                        序號 = reader.GetInt32(reader.GetOrdinal("序號")),
                        員編 = reader.GetString(reader.GetOrdinal("員編")),
                        開始日期 = reader.GetString(reader.GetOrdinal("開始日期")).Split(' ')[0],
                        開始時間 = reader.GetString(reader.GetOrdinal("開始時間")),
                        結束日期 = reader.GetString(reader.GetOrdinal("結束日期")).Split(' ')[0],
                        結束時間 = reader.GetString(reader.GetOrdinal("結束時間")),
                        假別 = reader.GetString(reader.GetOrdinal("假別")),
                        假別子項目 = reader.GetString(reader.GetOrdinal("假別子項目")) ?? Convert.ToString(' '),
                        送審主管 = reader.GetString(reader.GetOrdinal("送審主管")),
                    };
                    leaveTables.Add(leave);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            mySqlConnection.Close();
            return leaveTables;

        }
    }

}