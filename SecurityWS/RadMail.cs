using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.IO;
using Radiqal;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;

namespace Radiqal.Common
{
  public class RadMail
  {
    public bool  sendMail(string deviceId, string byteStream, string ToUser,string subject,string mailText,bool isAttachment)
    {
      SmtpClient client = new SmtpClient();
      //client.Port = 587;
      //client.Host = "smtp.gmail.com";
      client.EnableSsl = true;
      client.Timeout = 2000000;
      //client.DeliveryMethod = SmtpDeliveryMethod.Network;
      //client.UseDefaultCredentials = false;
      //client.Credentials = new System.Net.NetworkCredential("dev.radiqal@gmail.com", "Radiqal110");
      MailMessage mm = new MailMessage("dev.radiqal@gmail.com", ToUser ,subject ,mailText );
      mm.BodyEncoding = UTF8Encoding.UTF8;
      mm.IsBodyHtml = true;
      mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
     //MemoryStream ms =  getImageBytes(deviceId );
      if (isAttachment)
      {
        byte[] byteArrayIn = System.Convert.FromBase64String(byteStream);
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Attachment at = new Attachment(ms, "AndroidSecurityImage.jpg", "image/jpeg");
        mm.Attachments.Add(at);
      }
      try
      {
        client.Send(mm);
        Logging.WriteMessage("Mail Sent Successfully");  
        return true;
      }
      catch (Exception e)
      {
        Logging.WriteError("Error in sending mail " + e.Message); 
        return false;
      }
    
    }

    private MemoryStream getImageBytes(string deviceId)
    {

      byte[] byteArrayIn = new Radiqal.AndroidSecurityWS().getImageByteArray(deviceId);
      MemoryStream ms = new MemoryStream(byteArrayIn);
     // Image returnImage = Image.FromStream(ms);
     //return returnImage;
      return ms;
    }
  }
}
