using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using AndroidWebApp.WebReference;
using System.IO;
using System.Text;
namespace Android_Security
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Variables
        protected string username = string.Empty;
        protected string password = string.Empty;
        AndroidSecurityWS _reference;
        public static string session_reg_key = "session_reg_key";
        public static string session_device_id = "session_device_id";

        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetHintText();
            }
        }

        #endregion

        #region Control Events
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            username = txtUserName.Text.ToUpper();
            password = txtPassWord.Text.ToUpper();
            bool isValid = false;
            string registration_key = string.Empty;
            string device_id = string.Empty;
            isValid =  isUserValid(username, password);

            if (isValid == true)
            {
                Response.Write("<script>alert('Valid User!');</script>");
                device_id = _reference.GetDeviceId(username);
                registration_key = GetRegKey(device_id);
                Session[session_device_id] = device_id;
                Session[session_reg_key] = registration_key;
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Write("<script>alert('Invalid User!');</script>");
            }
        }

        #endregion

        #region Internal Functionality
        protected bool isUserValid(string username, string password)
        {
            _reference = new AndroidSecurityWS();            
            bool isValid = false;
            isValid = _reference.checkUser(username, password);

            return isValid;
        }

        protected string GetRegKey(string device_id)
        {
            _reference = new AndroidSecurityWS();
            string registration_key = string.Empty;
            registration_key = _reference.GetRegistrationKey(device_id);

            return registration_key;
        }

        protected void SetHintText()
        {
            txtUserName.Text = "Please Enter Your Email ID";
            txtPassWord.Text = "Please Enter Your Password";
            txtUserName.Attributes.Add("onFocus", "clearEmail(this)");
            txtPassWord.Attributes.Add("onFocus", "clearEmail(this)");
        }

        #endregion
    }
}
