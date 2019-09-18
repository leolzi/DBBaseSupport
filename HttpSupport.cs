using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DBBase
{
    public class HttpSupport
    {
        /// <summary>
        /// 发送请求到web服务器，并接受消息，T 是接受的class结构，T2是发送的class结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="obj"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T CallWebServerPost<T, T2>(T2 obj, string url)
        {
            string poststr = JsonSerializer(obj);  //对象转化为字符串发送
            string backmsg = FinalCallWebServerPost(poststr, url);  //发送请求返回相应的数据
            T js = Deserialize<T>(backmsg);  //返回数据进行反序列化
            return js;
        }

        public static string FinalCallWebServerPost(string postDataStr, string url)
        {
            Uri myuri = new Uri(url);
            var request = WebRequest.Create(myuri) as HttpWebRequest;

            request.Method = "POST";  //post
            request.ContentType = "application/json";
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.UTF8);
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();
            var Response = request.GetResponse() as HttpWebResponse;
            Stream myResponseStream = Response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
    }
}
