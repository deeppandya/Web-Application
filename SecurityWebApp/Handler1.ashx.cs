using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using AndroidWebApp.WebReference;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.OracleClient;
using System.Configuration;
using System.Web.SessionState;

namespace Android_Security
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler1 : IHttpHandler, IReadOnlySessionState
    {

        //string session_reg_key = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            AndroidWebApp.WebReference.AndroidSecurityWS _reference = new AndroidSecurityWS();
            byte[] byteArray1 = null;
            byteArray1 = getImage();
            context.Response.ContentType = "image/jpeg";
            if (byteArray1 != null)
            {
                context.Response.OutputStream.Write(byteArray1, 0, byteArray1.Length);
            }

            else
            { }          
              
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }      

        public byte [] getImage()
        {
            //string regKey = "APA91bHAs4_4tQbLOvCuoLANN5NVrJvVbauKkeBfBrr3ehesvmSrlmCfud3fcI5HE1KviOC18qa3lvDK9Xb7gKvlGbPi7vwUCFoLIg3In7Wd1O3S_nzTJk3Tf9fZC7eV-E3K2Yny8GPBmxh8B_lkPuJUbOYaM64KKg";
            string device_id = HttpContext.Current.Session[_Default.session_device_id].ToString();
            AndroidWebApp.WebReference.AndroidSecurityWS   webService = new AndroidWebApp.WebReference.AndroidSecurityWS ();
            return webService.getImageByteArray(device_id);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
