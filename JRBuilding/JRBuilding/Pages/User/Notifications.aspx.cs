using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JRBuilding.Pages.User
{
    public partial class Notifications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "webguard.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("rental@jrbuildings.com", "20JRBuildings!4");

                MailMessage mm = new MailMessage("rental@jrbuildings.com", txtto.Text, txtsubject.Text, txtbody.Text);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.Send(mm);
                lblmsg.InnerHtml = "Message successfully sent...";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}