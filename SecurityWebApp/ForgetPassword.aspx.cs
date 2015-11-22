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
using System.IO;
using System.Net;
using System.Text;
using System.Drawing;
using AndroidWebApp.WebReference;
using System.Collections.Generic;
using System.Threading;
using System.Net.Mail;

namespace Android_Security
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        #region Variables
        protected string user_id = string.Empty;
        protected string question = string.Empty;
        AndroidSecurityWS _reference;
        protected string answer = string.Empty;
        #endregion

        #region Constants
        protected const string SUBJECT = "Password Notification";
        protected const string MAIL_SUCCESSFUL = "Your User Id and Password has been successfully mailed to your Email Id.";
        protected const string MAIL_FAIL = "Answer to your security question does not match";
        protected const string EMAIL_ERROR = "Please Check Your Email/User Id again.";
        protected const string USER_ID = "USERID";
        protected const string PASSWORD = "PASSWORD";
        protected const string QUESTION = "QUESTION";
        protected const string ANSWER = "ANSWER";
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
            _reference = new AndroidSecurityWS();
            user_id = txtEmailId.Text.ToUpper();
            DataTable dt = new DataTable();
            dt = _reference.getUserLoginInfo(user_id);
            string dt_user_id = string.Empty;
            string dt_answer = string.Empty;
            string dt_email = string.Empty;
            bool isDelivered = false;

            if (dt != null && dt.Rows.Count > 0)
            {
                dt_user_id = dt.Rows[0][USER_ID].ToString();
                dt_answer = dt.Rows[0][ANSWER].ToString();
                if (txtAnswer.Visible == false && txtQuestion.Visible == false)
                {
                    question = dt.Rows[0][QUESTION].ToString().ToUpper();
                    txtQuestion.Text = question;
                    txtEmailId.Enabled = false;
                    txtEmailId.Font.Bold = true;
                    QuestionPanel.Visible = true;
                }

                else
                {
                    answer = txtAnswer.Text.ToUpper();
                    if (user_id == dt_user_id && answer == dt_answer)
                    {
                        string password = dt.Rows[0][PASSWORD].ToString();
                        string body = "Hello! Your User-Id is" + " " + user_id + " and Password is " + password;                        
                       isDelivered = _reference.sendMail(string.Empty, string.Empty, dt_user_id, SUBJECT, body, false);
                    }
                    HidePanels();
                    if (isDelivered == true)
                    {
                        string text = MAIL_SUCCESSFUL;
                        ShowMsg(text);
                    }
                    else
                    {
                        string text = MAIL_FAIL;
                        ShowError(text);
                    }
                }
            }

            else
            {
                HidePanels();
                string text = EMAIL_ERROR;
                ShowError(text);
            }
        }
        #endregion

        #region Internal Functionality
        protected void HidePanels()
        {
            forgetPanel.Visible = false;
            QuestionPanel.Visible = false;
            btnPanel.Visible = false;
        }

        protected void ShowMsg(string text)
        {
            msgPanel.Visible = true;
            msgLabel.Text = text;
            msgLabel.ForeColor = System.Drawing.Color.Green;
            msgLabel.Font.Bold = true;
        }

        protected void ShowError(string text)
        {
            msgPanel.Visible = true;
            msgLabel.Text = text;
            msgLabel.ForeColor = System.Drawing.Color.Red;
            msgLabel.Font.Bold = true;
            msgLabel.Font.Size = 11;
            msgLabel.BackColor = System.Drawing.Color.Transparent;
        }

        protected void SetHintText()
        {
            txtEmailId.Text = "Please Enter Your Email ID";
            txtAnswer.Text = "Please Enter Your Security Answer";
            //txtEmailId.Attributes.Add("onLoad", "setText(this)");
            txtEmailId.Attributes.Add("onFocus", "clearEmail(this)");
            txtAnswer.Attributes.Add("onFocus", "clearEmail(this)");
        }

#endregion
    }
}
