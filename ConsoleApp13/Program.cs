using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Globalization;
using Npgsql;
using System.Data;
using System.IO;
using System.Collections;
using System.Diagnostics;

public class TSCLIB_DLL
{
    [DllImport("TSCLIB.dll", EntryPoint = "about")]
    public static extern int about();

    [DllImport("TSCLIB.dll", EntryPoint = "openport")]
    public static extern int openport(string printername);

    [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
    public static extern int barcode(string x, string y, string type,
                string height, string readable, string rotation,
                string narrow, string wide, string code);

    [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
    public static extern int clearbuffer();

    [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
    public static extern int closeport();

    [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
    public static extern int downloadpcx(string filename, string image_name);

    [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
    public static extern int formfeed();

    [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
    public static extern int nobackfeed();

    [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
    public static extern int printerfont(string x, string y, string fonttype,
                    string rotation, string xmul, string ymul,
                    string text);

    [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
    public static extern int printlabel(string set, string copy);

    [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
    public static extern int sendcommand(string printercommand);

    [DllImport("TSCLIB.dll", EntryPoint = "setup")]
    public static extern int setup(string width, string height,
              string speed, string density,
              string sensor, string vertical,
              string offset);

    [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
    public static extern int windowsfont(int x, int y, int fontheight,
                    int rotation, int fontstyle, int fontunderline,
                    string szFaceName, string content);
    [DllImport("TSCLIB.dll", EntryPoint = "windowsfontUnicode")]
    public static extern int windowsfontUnicode(int x, int y, int fontheight,
                     int rotation, int fontstyle, int fontunderline,
                     string szFaceName, byte[] content);

    [DllImport("TSCLIB.dll", EntryPoint = "sendBinaryData")]
    public static extern int sendBinaryData(byte[] content, int length);

    [DllImport("TSCLIB.dll", EntryPoint = "usbportqueryprinter")]
    public static extern byte usbportqueryprinter();
}
namespace ConsoleApp13
{
    class Program
    {
        /*
         //Queue檢查
         public static void PrintValues(IEnumerable myCollection)
          {
            foreach (Object obj in myCollection)
                Console.Write("{0}\n", obj);
            //Console.WriteLine();
           }
           */
        public static void PointCheck(Stack myCollection)
        {
            string setPath = @"D:/集點設定/集點.txt";
            string[] row,test;
            //type 付款方式 cost 總金額 count 總公升 Oil 哪種油品
            string type="",Oil="";
            int cost=0;
            double count=0;

            
            //點數設定值
            row = File.ReadAllLines(setPath, Encoding.Default);
            Console.WriteLine(row[0]);
            Console.WriteLine(row[1]);
            Console.WriteLine(row[2]);
            Console.WriteLine(row[3]);
            Console.WriteLine(row[4]);
            Console.WriteLine(row[5]);
            Console.WriteLine(row[6]);
            Console.WriteLine(row[7]);
            Console.WriteLine(row[8]);
            Console.WriteLine(row[9]);
            Console.WriteLine(row[10]);
            Console.WriteLine(row[11]);
            Console.WriteLine(row[12]);
            Console.WriteLine(row[13]);
            Console.WriteLine(row[14]);


            bool Firstdetl = true;
                foreach (string str in myCollection)
                {
                    test = str.Split(',');
                    //type表示付款方式EX 901現金

                    if (str.Contains("tran_tmp"))
                    {
                        //tran_tmp抓哪種付款方式 type
                        type = test[69].Trim('\'');
                        //Console.Write(type + "\n");
                        
                    }
                    else if(str.Contains("tran_detl_tmp"))
                    {
                      //抓第一筆資料 因為第二筆為簽帳不需考量
                      if (Firstdetl == true)
                      {
                        //Oil 代表哪種油品 92 95 98 
                        Oil = test[17].Trim('\'');
                        //Console.Write(Oil + "\n");

                        cost = Int32.Parse(test[20].Trim('\''));
                        //Console.WriteLine(cost);
                        count = Double.Parse(test[21].Trim('\''));
                        //Console.WriteLine(count);
                        
                        Firstdetl = false;
                      }
                    }
                    
                    
                }

            //迴圈取得 type(付款方式) Oil(油品) cost(總金額) count(數量.公升)
            Console.WriteLine("付款方式"+type + " 油品" + Oil + " 總金額" + cost + " 數量(公升)" + count);
            
            //待續

            //next判斷是汽油還是柴油 by Oil
            if (Oil == "113F 51001007") { }
            //柴油 92 95 98 
            else if (Oil == "" || Oil == "" || Oil == "") { }
            //剩下的都是車隊的delt 嗎?
            //在分類
            if (Oil == "113F 51001007") { }
                //柴油 92 95 98 
                else if (Oil == "" || Oil == "" || Oil == "") { }
                //剩下的都是車隊的delt 嗎?


                //判斷是用哪種付款方式與加哪種油 決定金額要除的設定值
                if (type == "901")
                { }
                else if (type == "931")
                { }
                else if (type == "903")
                { }
                else if (type == "905")
                { }
                else if (type == "939")
                { }


            //用總金額除上對應的油品(柴油或汽油)->付款方式的集點設定值
            /*string f = "32.9";
            Console.WriteLine(double.Parse(f));*/
            //以現金集點 多少元1點
            if (row[0] == "以現金集點")
            {
            }
            //以公升


            //Console.WriteLine();
        }
        //static string connectionString = @"Server=localhost;Database=postgres;User ID=postgres;Password=1234;";
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            

            DateTime date = DateTime.Today;
            var taiwanCalendar = new System.Globalization.TaiwanCalendar();
            var datetime = string.Format("{0}{1}{2}",taiwanCalendar.GetYear(date),date.Month.ToString("00"),date.Day.ToString("00"));
            string path = @"D:/data/" + datetime + "_bkp_c#.sql";
            //Console.WriteLine(datetime);
            //Console.WriteLine(path);

            //讀取集點設定值
            Stack temp=new Stack();

           
            foreach (var line in File.ReadLines(path).Reverse())
            {
                if (line.Contains("tran_detl_tmp"))
                {
                    temp.Push(line);
                    Console.WriteLine(line);
                    
                }

                if(line.Contains("tran_tmp"))
                {
                    temp.Push(line);
                    Console.WriteLine(line);
                    //Console.WriteLine(type);
                    break;
                }

            }
           

            //Queue存放最新一筆資料
            //PrintValues(temp);

            PointCheck(temp);





            
            //金額/集點設定值
            /*
            int Point = 10;
            Console.WriteLine(Point);

                string WT1 = "Test Print";
                string B1 = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss", CultureInfo.InvariantCulture);
                byte[] result_unicode = System.Text.Encoding.GetEncoding("utf-16").GetBytes("unicode test");
                byte[] result_utf8 = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,\"ARIAL.TTF\",0,12,12,\"utf8 test Wörter auf Deutsch\"");
                System.Diagnostics.Debug.WriteLine(B1);
                //TSCLIB_DLL.about();
                byte status = TSCLIB_DLL.usbportqueryprinter();//0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
                TSCLIB_DLL.openport("TSC TTP-243E Pro");
                TSCLIB_DLL.sendcommand("SIZE 40 mm, 20 mm");
                TSCLIB_DLL.sendcommand("SPEED 4");
                TSCLIB_DLL.sendcommand("DENSITY 12");
                TSCLIB_DLL.sendcommand("DIRECTION 1");
                TSCLIB_DLL.sendcommand("SET TEAR ON");
                TSCLIB_DLL.sendcommand("CODEPAGE UTF-8");

                TSCLIB_DLL.clearbuffer();
                //TSCLIB_DLL.downloadpcx("UL.PCX", "UL.PCX");
                TSCLIB_DLL.windowsfont(0, 0, 40, 0, 0, 0, "Arial", "東誠加油站 JiH002");
                TSCLIB_DLL.windowsfont(10, 40, 30, 0, 0, 0, "Arial", DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss"));

                //TSCLIB_DLL.windowsfontUnicode(40, 550, 48, 0, 0, 0, "Arial", result_unicode);
                //TSCLIB_DLL.sendcommand("PUTPCX 40,40,\"UL.PCX\"");
                //TSCLIB_DLL.sendBinaryData(result_utf8, result_utf8.Length);
                TSCLIB_DLL.barcode("10", "70", "128", "35", "0", "0", "1", "1", B1);
                TSCLIB_DLL.windowsfont(10, 110, 50, 0, 0, 0, "Arial", "點數"+Point.ToString()+"點");
                //TSCLIB_DLL.printerfont("20", "40", "0", "0", "15", "15", WT1);
                TSCLIB_DLL.printlabel("1", "1");
                TSCLIB_DLL.closeport();
                */
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch總共花費{0}ms.", ts2.TotalMilliseconds);

            Console.Read();
               

        }
    }
}
