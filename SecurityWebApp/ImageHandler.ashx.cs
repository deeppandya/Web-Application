using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AndroidWebApp.WebReference;
using System.Web.Services; 
namespace AndroidWebApp
{
  /// <summary>
  /// Summary description for $codebehindclassname$
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class ImageHandler : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      string regKey = "APA91bHAs4_4tQbLOvCuoLANN5NVrJvVbauKkeBfBrr3ehesvmSrlmCfud3fcI5HE1KviOC18qa3lvDK9Xb7gKvlGbPi7vwUCFoLIg3In7Wd1O3S_nzTJk3Tf9fZC7eV-E3K2Yny8GPBmxh8B_lkPuJUbOYaM64KKg";
      AndroidSecurityWS wef = new AndroidSecurityWS();
      byte[] bytes = wef.getImageByteArray(regKey);
      context.Response.ContentType = "image/jpeg";
      context.Response.OutputStream.Write(bytes, 0, bytes.Length);
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
