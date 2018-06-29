using HtmlAgilityPack;  //法國人研發的HTML擷取套件
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seminar
{
    class exchangeRate
    {
        public void refresh()
        {
            //指定來源網頁
            //抓取台灣銀行的匯率
            //只抓買入價格
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc = webClient.Load("http://rate.bot.com.tw/Pages/Static/UIP003.zh-TW.htm");
            String today = DateTime.Now.ToString("yyyy/MM/dd");

            //資料庫前置準備
            string dbHost = "localhost";//資料庫位址
            string dbPort = "3306";//資料庫的port
            string dbUser = "root";//資料庫使用者帳號
            string dbPass = "5423";//資料庫使用者密碼
            string dbName = "Seminar";//資料庫名稱

            string connStr = "server=" + dbHost + ";port=" + dbPort + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();

            //先檢查當天是否已更新過
            conn.Open(); //連接資料庫
            command.CommandText = "SELECT* FROM 匯率資料表 WHERE  日期 = '" + today + "'";
            MySqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                conn.Close();
                return; //今天已經抓過匯率了故結束Function
            }
            else
            {
                conn.Close(); //關閉連接以清除dataReader的lock
                command.CommandText = "ALTER TABLE 匯率資料表 AUTO_INCREMENT = 1;";  //流水號從最小開始用
                command.CommandText += "INSERT INTO 匯率資料表 (幣別, 日期, 匯率) VALUES('臺幣 (TWD)', '" + today + "', '1');";  //臺幣匯率
            }



            //整理各節點以擷取匯率
            
            for (int x = 1; x <= 19; x++)
            {
                string txt1 = doc.DocumentNode.SelectSingleNode(@"/html[1]/body[1]/div[1]/main[1]/div[4]/table[1]/tbody[1]/tr["+ x +"]/td[1]/div[1]/div[2]").InnerText;
                string txt2 = doc.DocumentNode.SelectSingleNode(@"/html[1]/body[1]/div[1]/main[1]/div[4]/table[1]/tbody[1]/tr[" + x + "]/td[3]").InnerText;
                if (txt2.Equals("-"))
                    txt2 = "0";
                txt1 = txt1.Replace("\r\n","");
                txt1 = txt1.Trim();
                command.CommandText += "INSERT INTO 匯率資料表 (幣別, 日期, 匯率) VALUES ('" + txt1 + "','" + today + "', '" + txt2 + "');";
            }
            conn.Open();//連接再開
            command.ExecuteNonQuery();
            conn.Close();

            doc = null;
            webClient = null;

            Console.WriteLine("Completed.");
            Console.ReadLine();

        }
    }
}
