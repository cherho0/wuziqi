using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestSend
{
    class Program
    {
        static void Main(string[] args)
        {
            CookieWebClient wc = new CookieWebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Cookies.Add(new Cookie("__RequestVerificationToken",
                "qNmFBgpQQFU7NEixZP_hgdBB1rw90J4LEeWnKMn3-gb9XEyQmWYNkjyibziBw_qoKaocyPZ3QlZMHbNtg6qwtUh6OMpoT5nbLk9ADwGPbEY1", "/", "etuanjian.com"));
            string regUrl = "http://etuanjian.com:8080/SendSMS/SendSMSReg/";
            byte[] post = Encoding.UTF8.GetBytes("data={\"mobileNumber\":\"15151551892\"}");
            byte[] data = wc.UploadData(regUrl,"post" ,post);
            string html = Encoding.UTF8.GetString(data);
        }
    }

    public class CookieWebClient : WebClient
    {
        // Cookie 容器  
        public CookieContainer Cookies;

        /// <summary>  
        /// 创建一个新的 CookieWebClient 实例。  
        /// </summary>  
        public CookieWebClient()
        {
            this.Cookies = new CookieContainer();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = Cookies;
            }
            return request;
        }
    }
}
