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
        public static void PointCheck(Stack myCollection)
        {
            string setPath = @"D:/集點設定/集點.txt";
            string[] row;
            //點數設定值
            row = File.ReadAllLines(setPath, Encoding.Default);
            Console.WriteLine(row[0]);
            Console.WriteLine(row[1]);
            Console.WriteLine(row[2]);
            Console.WriteLine(row[3]);
            Console.WriteLine(row[4]);
            foreach (string str in myCollection)
            {
                if(str.Contains("tran_tmp")
                Console.Write(str + "\n");
            }


            //Console.WriteLine();
        }*/
        //static string connectionString = @"Server=localhost;Database=postgres;User ID=postgres;Password=1234;";
        static void Main(string[] args)
        {
            /*string fileName = @"D:/集點設定/集點.txt";
            string fileName2 = @"D:/集點設定/金額.txt";
            string temp = File.ReadLines(fileName, Encoding.Default).Last();
            string[] arrTemp;
            Console.WriteLine(temp);
            arrTemp = temp.Split('值');
            Console.WriteLine(arrTemp[1]);
            string tempcost = File.ReadLines(fileName2, Encoding.Default).Last();*/
            
            DateTime date = DateTime.Today;
            var taiwanCalendar = new System.Globalization.TaiwanCalendar();
            var datetime = string.Format("{0}{1}{2}",taiwanCalendar.GetYear(date),date.Month.ToString("00"),date.Day.ToString("00"));
            string path = @"D:/data/" + datetime + "_bkp_c#.sql";
            //Console.WriteLine(datetime);
            //Console.WriteLine(path);
            string[] arrTemp,test;
            string type;

            //讀取集點設定值
            string setPath = @"D:/集點設定/集點.txt";
            string[] row = File.ReadAllLines(setPath, Encoding.Default); ;

            //Stack temp=new Stack();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (var line in File.ReadLines(path).Reverse())
            {
                if (line.Contains("tran_detl_tmp"))
                {
                    //temp.Push(line);
                    Console.WriteLine(line);
                    
                }

                if(line.Contains("tran_tmp"))
                {
                    //temp.Push(line);
                    Console.WriteLine(line);
                    test = line.Split(',');
                    type = test[69].Trim('\'');
                    Console.WriteLine(type);
                    break;
                }

            }
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("Stopwatch總共花費{0}ms.", ts2.TotalMilliseconds);

            //Queue存放最新一筆資料
            //PrintValues(temp);

            //PointCheck(temp);


            

            
            /*
            //金額/集點設定值
            //int cost=Int32.Parse(tempcost);
            //int divide = Int32.Parse(arrTemp[1]);
            //int Point = Int32.Parse(tempcost) / Int32.Parse(arrTemp[1]);
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
            Console.Read();
               
        }
    }
}
