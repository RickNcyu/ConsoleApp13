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
using System.Threading;

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
        public class Global
        {
            public static string name = "";
        }
        public static double PointCheck(Stack myCollection)
        {

            string setPath = @"C:/down/down.txt";
            string[] row, test, Poil, Name;
            //type 付款方式 cost 總金額 count 總公升 Oil 哪種油品
            string type = "", Oil = "";
            int cost = 0;
            double count = 0, setpoint = 0, Point = 0;
            int ans = 0;



            //點數設定值
            row = File.ReadAllLines(setPath, Encoding.Default);
            /*Console.WriteLine("row[0]"+row[0]);
            Console.WriteLine("row[1]" + row[1]);
            Console.WriteLine("row[2]" + row[2]);
            Console.WriteLine("row[3]" + row[3]);
            Console.WriteLine("row[4]" + row[4]);
            Console.WriteLine("row[5]" + row[5]);
            Console.WriteLine("row[6]" + row[6]);
            Console.WriteLine("row[7]" + row[7]);
            Console.WriteLine("row[8]" + row[8]);
            Console.WriteLine("row[9]" + row[9]);
            Console.WriteLine("row[10]" + row[10]);
            Console.WriteLine("row[11]" + row[11]);
            Console.WriteLine("row[12]" + row[12]);
            Console.WriteLine("row[13]" + row[13]);
            Console.WriteLine("row[14]" + row[14]);*/

            //站名
            Name = row[15].Split('=');
            Global.name = Name[1];

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
                else if (str.Contains("tran_detl_tmp"))
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
            //Console.WriteLine("付款方式"+type + " 油品" + Oil + " 總金額" + cost + " 數量(公升)" + count);


            //next判斷是汽油還是柴油 by Oil
            //汽油 92 95 98 
            if (Oil == "113F 12092005" || Oil == "113F 12095001" || Oil == "113F 12098008")
            {

                //判斷是用哪種付款方式 決定金額要除的設定值
                if (type == "901")
                {
                    ans = 3;
                }
                else if (type == "931")
                {
                    ans = 4;
                }
                else if (type == "903")
                {
                    ans = 5;
                }
                else if (type == "905")
                {
                    ans = 6;
                }
                else if (type == "939")
                {
                    ans = 7;
                }

            }
            //柴油
            else if (Oil == "113F 51001007")
            {
                if (type == "901")
                {
                    ans = 10;
                }
                else if (type == "931")
                {
                    ans = 11;
                }
                else if (type == "903")
                {
                    ans = 12;
                }
                else if (type == "905")
                {
                    ans = 13;
                }
                else if (type == "939")
                {
                    ans = 14;
                }
            }
            else
            {
                System.Environment.Exit(0);
            }
            Poil = row[ans].Split('=');
            //Poil[1]為設定值
            //Console.WriteLine(Poil[1]);
            //沒有設定值表示不集點 強制結束程式
            if (Poil[1] == "")
            {
                //Console.WriteLine("設定值為空");
                System.Environment.Exit(0);

            }



            //以金額/集點值
            if (row[0] == "以現金集點")
            {
                setpoint = cost / (Double.Parse(Poil[1]));
            }


            else if (row[0] == "以公升集點")
            {
                setpoint = count * (Double.Parse(Poil[1]));
            }

            //Point = (int)setpoint;
            Point = Math.Round(setpoint, 0, MidpointRounding.AwayFromZero);
            //Console.WriteLine(setpoint);
            //Console.WriteLine(Point);



            return Point;
        }
        //static string connectionString = @"Server=localhost;Database=postgres;User ID=postgres;Password=1234;";
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();


            DateTime date = DateTime.Today;
            var taiwanCalendar = new System.Globalization.TaiwanCalendar();
            var datetime = string.Format("{0}{1}{2}", taiwanCalendar.GetYear(date), date.Month.ToString("00"), date.Day.ToString("00"));
            string path = @"C:\PosSystem\data\" + datetime + "_bkp_c#.sql";
            string pathdes = @"C:/down/need.txt";

            string pathlast = @"C:/down/last.txt";
            //確認stack是不是空的
            int top = 0;

            /*
                while (true)
                {
                using (var file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {

                    var reader = new StreamReader(file);
                    var line = reader.ReadToEnd();
                    Console.WriteLine(line);
                    using (var wrfile = new FileStream(pathdes, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {


                        var writer = new StreamWriter(wrfile);
                        writer.Write(line);
                        writer.Flush();
                    }
                }

                    Thread.Sleep(2000);
                }
           */ 
            while (true)
            {
                //sql如果不存在==>12點過後還沒有產生最新一筆交易
                if (File.Exists(path))
                { 

                    /*//沒有交易 空白的
                    if (File.ReadAllText(pathdes) == "") {
                        
                        System.Environment.Exit(0);
                    }*/

                    //讀取集點設定值
                    //File.Copy(path, pathdes, true);
                    //Console.WriteLine(File.ReadAllText(path));
                    
                    using (var file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                        {

                            var reader = new StreamReader(file);
                            var line = reader.ReadToEnd();
                            Console.WriteLine(line);
                            using (var wrfile = new FileStream(pathdes, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                            {


                                var writer = new StreamWriter(wrfile);
                                writer.Write(line);
                                writer.Flush();
                            }
                           
                        }
                        

                    Stack temp = new Stack();
                    Stack last = new Stack();
               
                   
                    
     
                    //判斷有沒有last
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/down/last.txt", true, System.Text.Encoding.Default))
                    {
                        last.Push("null");
                        file.Close();
                    };
                    //上一筆最新資料
                    foreach (var line in File.ReadLines(pathlast).Reverse())
                    {
                        last.Push(line);
                    }
                    //最新資料比對
                    foreach (var line in File.ReadLines(pathdes).Reverse())
                    {
                       
                        if (line.Contains("tran_detl_tmp"))
                        {
                            temp.Push(line);
                            top++;
                        }

                        if (line.Contains("tran_tmp"))
                        {
                            temp.Push(line);
                            top++;
                            break;
                            
                        }
                    }

                  
                    using (StreamWriter svw = new StreamWriter(pathlast))
                    {
                        foreach (string str in temp)
                        {
                            svw.WriteLine(str);
                        }
                    }
                    if(top==0)
                    { //Console.WriteLine("空的"); 
                      System.Environment.Exit(0);
                    }
                    //Console.Read();
                    string ls1, ls2;
                    ls1 = temp.Peek().ToString();
                    Console.WriteLine(ls1);
                    ls2 = last.Peek().ToString();
                    Console.WriteLine(ls2);
                    Console.WriteLine(string.Compare(ls1, ls2));

                    //與上一筆資料不同 有新資料
                    if (string.Compare(ls1, ls2) != 0)
                    {
                        Console.WriteLine("yes");


                        //Stack存放最新一筆資料
                        //PrintValues(temp);
                        double Ans = 0;
                        Ans = PointCheck(temp);
                        //Console.WriteLine(Ans);

                        Console.WriteLine(Global.name);

                        /*
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
                        TSCLIB_DLL.windowsfont(0, 0, 40, 0, 0, 0, "Arial", Global.name);
                        TSCLIB_DLL.windowsfont(10, 40, 30, 0, 0, 0, "Arial", DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss"));

                        //TSCLIB_DLL.windowsfontUnicode(40, 550, 48, 0, 0, 0, "Arial", result_unicode);
                        //TSCLIB_DLL.sendcommand("PUTPCX 40,40,\"UL.PCX\"");
                        //TSCLIB_DLL.sendBinaryData(result_utf8, result_utf8.Length);
                        TSCLIB_DLL.barcode("10", "70", "128", "35", "0", "0", "1", "1", B1);
                        TSCLIB_DLL.windowsfont(10, 110, 50, 0, 0, 0, "Arial", "點數" + Ans.ToString() + "點");
                        //TSCLIB_DLL.printerfont("20", "40", "0", "0", "15", "15", WT1);
                        TSCLIB_DLL.printlabel("1", "1");
                        TSCLIB_DLL.closeport();
                        */
                        sw.Stop();
                        TimeSpan ts2 = sw.Elapsed;
                        Console.WriteLine("Stopwatch總共花費{0}ms.", ts2.TotalMilliseconds);


                    }
                    //Thread.Sleep(2000);
                }
            }
            //Console.Read();

        }
    }
}
