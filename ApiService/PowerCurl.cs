using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using Senparc.Weixin.MP.Sample.WebForms;

namespace ChatGPT.API
{
    public class PowerCurl
    {
        private static string _getCurlPath()
        {
            string curlPath;

            curlPath = ConfigurationManager.AppSettings["CurlPath"].ToString();
            if (string.IsNullOrEmpty(curlPath)) { 
                curlPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "curl\\bin\\curl.exe");
            }
            if (!File.Exists(curlPath))
            {
                curlPath = System.IO.Path.Combine(Environment.SystemDirectory, "curl.exe");
            }
            return curlPath;
        }
        public static string DencodeUrl(string s)
        {
            string dencodestr = System.Web.HttpUtility.UrlDecode(s, System.Text.Encoding.UTF8);
            return dencodestr;
        }
        static string ConvertToUTF8(String str)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            Encoding utf8 = Encoding.UTF8;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                string sub = str.Substring(i, 1);
                byte[] bytes = utf8.GetBytes(sub);

                if (bytes.Length == 1) //普通英文字母或数字
                {
                    result.Append(sub);
                }
                else  //其它字符，转换成为编码
                {
                    for (int j = 0; j < bytes.Length; j++)
                    {
                        result.Append("%" + hexDigits[bytes[j] >> 4] + hexDigits[bytes[j] & 0XF]);
                    }
                }
            }
            return result.ToString();
        }
        public static string Commd(string commdstring)
        {
            //对中文参数进行编码转换
            string curlPath= _getCurlPath();
            Process myProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(curlPath);
            startInfo.Arguments = ConvertToUTF8(commdstring);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            myProcess.StartInfo = startInfo;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.Start();
            myProcess.WaitForExit();
            StreamReader sr = new StreamReader(myProcess.StandardOutput.BaseStream, System.Text.Encoding.UTF8, true);
            string myString = sr.ReadToEnd();
            myProcess.Close();
            sr.Close();
            return myString;

        }
    }
}
