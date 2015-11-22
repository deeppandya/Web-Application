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

namespace Android_Security
{
    public partial class Home : System.Web.UI.Page
    {
        #region Variables
        protected string registration_key = string.Empty;
        string authorization_key = System.Configuration.ConfigurationManager.AppSettings["AuthorizationKey"].ToString();
        string session_reg_key = string.Empty;
        AndroidSecurityWS _reference;

        public static string Start_Tracking = "Start_Tracking";
        public static string Stop_Tracking = "Stop_Tracking";
        public static string Take_Picture = "Send_Photo";
        public static string Profile_Silent = "Change_To_Silent";
        public static string Profile_Vibrate = "Change_To_Vibrate";
        public static string Profile_Loud = "Change_To_Loud";
        public static string Sound_Alarm = "Play";
        public static string Send_Message = "Send_Location";

        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            mapPanel.Visible = false;
            imagePanel.Visible = false;
            locationHistoryPanel.Visible = false;            
            LoadingPanel.Visible = false;
            if (!IsPostBack)
            {
                ShowDeviceInfo();
            }
        }
        #endregion

        #region Control Events
        protected void locationGrid_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = locationGrid.Rows[index];

                string latitude = string.Empty;
                string longitude = string.Empty;

                latitude = row.Cells[1].Text.ToString();
                longitude = row.Cells[2].Text.ToString();
                //latitude = "23.066466";
                //longitude = "72.520752";

                ShowGridLocationOnMap(latitude, longitude);
            }

        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            foreach (GridViewRow row in locationGrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Attributes["onmouseover"] =
                       "this.style.cursor='hand';this.style.textDecoration='underline';";
                    row.Attributes["onmouseout"] =
                       "this.style.textDecoration='none';";
                    // Set the last parameter to True 
                    // to register for event validation. 
                    row.Attributes["onclick"] =
                     ClientScript.GetPostBackClientHyperlink(locationGrid,
                        "Select$" + row.DataItemIndex, true);
                }
            }
            base.Render(writer);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SendCommandToPhone("Stop_Tracking");
            Thread.Sleep(2000);
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        protected void Timer2_Tick(object sender, EventArgs e)
        {
            DataTable LocationDt = GetLocationCoordinates();
            if (LocationDt != null && LocationDt.Rows.Count > 0)
            {
                locationHistoryPanel.Visible = true;
                locationGrid.DataSource = LocationDt;
                locationGrid.DataBind();
            }

            else
            {
                locationHistoryPanel.Visible = false;
            }
        }

        protected void SendMessage(object sender, EventArgs e)
        {
            locationHistoryPanel.Visible = false;
            SendCommandToPhone(Send_Message);
        }

        protected void StartTracking(object sender, EventArgs e)
        {
            SendCommandToPhone(Start_Tracking);
            Thread.Sleep(10000);
            BuildScript();
        }

        protected void StopTracking(object sender, EventArgs e)
        {
            SendCommandToPhone(Stop_Tracking);
            //Thread.Sleep(2000);
            mapPanel.Visible = false;
            locationHistoryPanel.Visible = false;
            imagePanel.Visible = false;
        }

        protected void SendPhoto(object sender, EventArgs e)
        {
            locationHistoryPanel.Visible = false;
            imagePanel.Visible = false;
            _reference = new AndroidSecurityWS();
            _reference.deleteImage(Session[_Default.session_device_id].ToString());
            SendCommandToPhone(Take_Picture);
            mapPanel.Visible = false;
            GenerateImageControl();
        }

        protected void Silent(object sender, EventArgs e)
        {
            mapPanel.Visible = false;
            imagePanel.Visible = false;
            locationHistoryPanel.Visible = false;
            SendCommandToPhone(Profile_Silent);
        }

        protected void Vibrate(object sender, EventArgs e)
        {
            mapPanel.Visible = false;
            imagePanel.Visible = false;
            locationHistoryPanel.Visible = false;
            SendCommandToPhone(Profile_Vibrate);
        }

        protected void Loud(object sender, EventArgs e)
        {
            mapPanel.Visible = false;
            imagePanel.Visible = false;
            locationHistoryPanel.Visible = false;
            SendCommandToPhone(Profile_Loud);
        }

        protected void PlayAlarm(object sender, EventArgs e)
        {
            mapPanel.Visible = false;
            imagePanel.Visible = false;
            locationHistoryPanel.Visible = false;
            SendCommandToPhone(Sound_Alarm);
        }

        protected void LocationHistory_Click(object sender, EventArgs e)
        {
            BuildScript();

            DataTable LocationDt = GetLocationCoordinates();
            if (LocationDt != null && LocationDt.Rows.Count > 0)
            {
                locationHistoryPanel.Visible = true;
                locationGrid.DataSource = LocationDt;
                locationGrid.DataBind();
            }
        }

        #endregion

        #region Internal Functionality
        

        private void BuildScript()
        {
            mapPanel.Visible = true;
            String Locations = "";
            string latitude = string.Empty;
            string longitude = string.Empty;
            string map_script = string.Empty;
            DataTable LocationDt = GetLocationCoordinates();
            if (LocationDt != null && LocationDt.Rows.Count > 0)
            {
                locationHistoryPanel.Visible = true;
                locationGrid.DataSource = LocationDt;
                locationGrid.DataBind();
                latitude = LocationDt.Rows[0]["LATITUDE"].ToString();
                longitude = LocationDt.Rows[0]["LONGITUDE"].ToString();
                map_script = ShowMap(latitude, longitude,true );

                js.Text = map_script;
            }

            else
            {
                mapPanel.Visible = false;
                locationHistoryPanel.Visible = false;
            }
        }

        public string ShowMap(string latitude, string longitude,bool isWindowOnload)
        {
            String Locations = "";
            if (!String.IsNullOrEmpty(latitude) && !String.IsNullOrEmpty(longitude))
            {
                Locations += Environment.NewLine + "(" + latitude + "," + longitude + ")";
            }
            string map_script = string.Empty;
            map_script = @"<script type ='text/javascript'>
                            var map;
                            var isWinLoad = '" + isWindowOnload + @"' ;
                            function initialize() {
                                
                                var latlng = new google.maps.LatLng" + Locations + @"; 
                                var myOptions = {
                                    zoom: 17,
                                    center: latlng,
                                    mapTypeId: google.maps.MapTypeId.ROADMAP
                                };
                                map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
                                var marker = new google.maps.Marker
                                (
                                    {
                                        position: latlng,
                                        map: map
                                    }
                                );
                                var circle = {
                                        strokeColor: '#006DFC',
                                        strokeOpacity: 0.4,
                                        strokeWeight: 3,
                                        fillColor: '#006DFC',
                                        fillOpacity: 0.15,
                                        map: map,
                                        center: marker.getPosition(),
                                        radius: 50 // in meters

                                    };
                                var cityCircle = new google.maps.Circle(circle);                 
                                cityCircle.bindTo('center', marker, 'position'); 
                                var infowindow = new google.maps.InfoWindow({
                                    content: 'Location info:<br/>Country Name:<br/>LatLng:'
                                });
                                google.maps.event.addListener(marker, 'click', function () {
                                    // Calling the open method of the infoWindow 
                                    infowindow.open(map, marker);
                                });
                            }
                            if(isWinLoad == 'True')
                            window.onload = initialize;
                            else
                              initialize();
                        </script>";

            return map_script;
        }

        public string ShowMap1()
        {
          string map_script = string.Empty;
          map_script = @"<script type ='text/javascript'>
                     initialize();
                        function initialize() {
                            alert('inside javascript');
                          }
                        </script>";

          return map_script;
        }


        private void ShowGridLocationOnMap(string latitude, string longitude)
        {
            string map_script = string.Empty;
            mapPanel.Visible = false;
            locationHistoryPanel.Visible = true;
           map_script = ShowMap(latitude, longitude,false );
            //map_script = ShowMap1();

            mapPanel.Visible = true;
            js.Text = map_script;
         
            ScriptManager.RegisterClientScriptBlock(mapPanel, this.GetType(), "alert", map_script, false );
        }

        protected DataTable GetLocationCoordinates()
        {
            _reference = new AndroidSecurityWS();
            DataTable dt = new DataTable();

            dt = _reference.GetLocation(Session[_Default.session_device_id].ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtNew = dt.Clone();
                decimal count = Convert.ToDecimal(dt.Rows.Count.ToString());
                if (count > 5)
                {
                    count = 5;
                }

                for (int i = 0; i < count; i++)
                {
                    dtNew.ImportRow(dt.Rows[i]);
                }

                return dtNew;
            }
            else
            {
                return dt;
            }
        }

        protected DataTable GetDeviceInfo()
        {
            _reference = new AndroidSecurityWS();
            DataTable dt = new DataTable();

            if (Session[_Default.session_device_id] != null)
            {
                dt = _reference.getUserInformation(Session[_Default.session_device_id].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return dt;
                }
            }
            else
            {
                return dt;
            }

        }

        protected void GenerateImageControl()
        {

            mapPanel.Visible = false;
            System.Web.UI.WebControls.Image _imageControl = new System.Web.UI.WebControls.Image();
            _imageControl.Controls.Clear();
            LoadingPanel.Visible = true;
            Thread.Sleep(20000);
            LoadingPanel.Visible = false;
            _imageControl.ImageUrl = "~/Handler1.ashx";
            _imageControl.Visible = true;
            _imageControl.Width = 300;
            _imageControl.Height = 300;

            imagePanel.Controls.Add(_imageControl);
            imagePanel.Visible = true;
        }

        protected void ShowDeviceInfo()
        {
            DataTable dt = GetDeviceInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                txtModelNo.Text = dt.Rows[0]["DEVICE_NAME"].ToString();
                txtIMEI.Text = Session[_Default.session_device_id].ToString();
                txtUserId.Text = " " + dt.Rows[0]["USERID"].ToString();
                txtUserId.BackColor = System.Drawing.Color.Transparent;
                if (dt.Rows[0]["PHONE_NO"].ToString() != string.Empty)
                {
                    txtPhoneNumber.Text = dt.Rows[0]["PHONE_NO"].ToString();
                    txtPhoneNumber.BackColor = System.Drawing.Color.Transparent;
                }
                else
                {
                    lblPhoneNumber.Visible = false;
                    txtPhoneNumber.Visible = false;

                }
                if (dt.Rows[0]["SERVICE_PROVIDER"].ToString() != string.Empty)
                {
                    txtServiceProvider.Text = dt.Rows[0]["SERVICE_PROVIDER"].ToString();
                    txtServiceProvider.BackColor = System.Drawing.Color.Transparent;
                }
                else
                {
                    lblServiceProvider.Visible = false;
                    txtServiceProvider.Visible = false;
                }

            }

        }

        #endregion

        #region External Functionality

        public void SendCommandToPhone(string command)
        {
            String Registration_Key = Session[_Default.session_reg_key].ToString();
            string geo_tracking_interval = System.Configuration.ConfigurationManager.AppSettings["GeoTrackingInterval"].ToString();
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/x-www-form-urlencoded";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", authorization_key));

            String collaspeKey = Guid.NewGuid().ToString("n");
            //String postData=string.Format("registration_id={0}&data.payload={1}&collapse_key={2}", DeviceID, "Pickup Message", collaspeKey);
            String postData = string.Format("registration_id={0}&data1={1}&data2={2}", Registration_Key, command, geo_tracking_interval);

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }
        #endregion
    }
}
