using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ChatGPT.API
{
    public class Service
    {
        private readonly string _Service_Url = ConfigurationManager.AppSettings["ChatGPT_Api_Url"].ToString();
        private readonly string _Authorization_Key = ConfigurationManager.AppSettings["ChatGPT_Api_Key"].ToString();
        private readonly string _Proxy_Host = ConfigurationManager.AppSettings["Proxy_Host"].ToString();
        private readonly int _Proxy_Port = Int32.Parse( ConfigurationManager.AppSettings["Proxy_Port"]);
        private readonly string _Proxy_Username = ConfigurationManager.AppSettings["Proxy_Username"].ToString();
        private readonly string _Proxy_Password = ConfigurationManager.AppSettings["Proxy_Password"].ToString();

        #region 公共底层接口 
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        private string _GetAuthorization()
        {
            return "Bearer" + " " + _Authorization_Key;
        }
        /// <summary>
        /// CURL方式调用
        /// </summary>
        /// <param name="lstReq"></param>
        /// <returns></returns>
        public string Question2(string question)
        {

            string path = "";
            string url = _Service_Url + string.Format(path);
            var lstReq = new QuestionEntity(question);
            var answer = this._QueryCurlService<QuestionEntity, AnswerEntity>(url, lstReq);
            string result = answer.choices.FirstOrDefault().text;

            //中文回答开头乱码处理（貌似问题较短，被AI自动补全）
            Regex reg = new Regex("^(\n)*(%[a-zA-Z0-9]{2})");
            if (reg.IsMatch(result))
            {
                result = question + PowerCurl.DencodeUrl(result);
            }

            Regex reg1 = new Regex("^(\n)*");
            result = reg1.Replace(result, "");//去除开头换行
            //result = result.Replace("\n", "\r\n"); //换行处理
            return result;
        }

        private V _QueryCurlService<K, V>(string strUrl,  K lstReq)
        {
            try
            {
                //C# 过滤字段值为null的字段
                var jsonSetting = new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.None
                };
                string Req = JsonConvert.SerializeObject(lstReq,jsonSetting);
                Req = Req.Replace("\"", "\\\"");
                //加入文件头及参数
                var curlCommd = string.Format("{0} -H \"Content-Type:application/json\" -H \"charset=utf-8\" -H \"Authorization:{1}\" -d \"{2}\"", _Service_Url, _GetAuthorization(), Req);
                //代理访问

                var ret=PowerCurl.Commd(curlCommd);
                var errRes= JsonConvert.DeserializeObject<ErrorRespone>(ret);
                if (errRes.error != null)
                {
                    throw (new Exception("返回失败:" + ret));
                }
                var lstRes = JsonConvert.DeserializeObject<V>(ret);
                return lstRes;
            }
            catch (Exception ex)
            {
                throw (new Exception("调用接口失败:" + ex.Message));
            }
        }
        /// <summary>
        /// 新增提问
        /// </summary>
        /// <param name="lstReq"></param>
        /// <returns></returns>
        public string Question(string question)
        {
            string path = "";
            string url = _Service_Url + string.Format(path);
            var Method = "POST";
            var lstReq = new QuestionEntity(question);
            var answer=this._QueryRecruitService<QuestionEntity, AnswerEntity>(url, Method, lstReq);
            return answer.choices.FirstOrDefault().text.Replace("\n", "\r\n");
        }
        /// <summary>
        /// 调用API
        /// </summary>
        /// <typeparam name="K">传入参数类型</typeparam>
        /// <typeparam name="V">响应参数类型</typeparam>
        /// <param name="strUrl">接口地址</param>
        /// <param name="Method">调用方式</param>
        /// <param name="lstReq">传入信息</param>
        private V _QueryRecruitService<K, V>(string strUrl, string Method, K lstReq)
        {
            try
            {


                HttpWebRequest WebReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);

                WebReq.Method = Method;
                WebReq.ContentType = "application/json";
                WebReq.Headers.Set("Authorization", this._GetAuthorization());

                //代理访问
                WebReq.Proxy = _MyProxy;

                //C# 过滤字段值为null的字段
                var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                string Req = JsonConvert.SerializeObject(lstReq, Formatting.Indented, jsonSetting);
                byte[] postBytes = string.IsNullOrEmpty(Req) ? new byte[0] : Encoding.UTF8.GetBytes(Req);
                WebReq.ContentLength = postBytes.Length;

                Stream newStream = WebReq.GetRequestStream();
                newStream.Write(postBytes, 0, postBytes.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)WebReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                var ret = sr.ReadToEnd();
                var lstRes = JsonConvert.DeserializeObject<V>(ret);
                sr.Close();
                response.Close();
                newStream.Close();

                return lstRes;
            }
            catch (Exception ex)
            {
                throw (new Exception("调用接口失败:" + ex.Message));
            }
        }
        /// <summary>
        /// 代理服务器
        /// </summary>
        private WebProxy _MyProxy
        {
            get {
                return new WebProxy(this._Proxy_Host.ToString(), this._Proxy_Port) { Credentials = new NetworkCredential(this._Proxy_Username, this._Proxy_Password) };
            }
        }
        #endregion
    }
}
