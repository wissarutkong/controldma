using controldma.App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using Newtonsoft.Json;

namespace controldma
{
    public partial class login : System.Web.UI.Page
    {
        Cs_Controlcenter cls = new Cs_Controlcenter();
        Cs_cons clo = new Cs_cons();
        Cs_manageLoing Mgs = new Cs_manageLoing();
        protected void Page_Load(object sender, EventArgs e)
        {
            litMsg.Text = "";
            //cls.GetIPAddress();
            Context.ApplicationInstance.CompleteRequest();
            if (!this.IsPostBack)
            {
                if (this.UserName != null)
                {
                    l_username.Value = this.UserName;
                    l_pass.Value = this.Password;
                    flaglogin = true;
                    btn_Login_Click(null, null);
                }
                else { flaglogin = false; }
                
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            String txtUsername = cls.toHtmlTag(l_username.Value.ToString());
            String txtPassword = cls.toHtmlTag(l_pass.Value.ToString());
            if (String.IsNullOrEmpty(txtUsername) || String.IsNullOrEmpty(txtPassword))
            {
                Alert("ชื่อผู้ใช้งาน หรือ รหัสผ่่านยังไม่ได้กรอก", "warning");
            }
            else
            {
                String sCon = clo.GetConnectionString(Cs_cons.Database.dma2);
                String sCon_portaldb = clo.GetConnectionString(Cs_cons.Database.PortalDB);
                Boolean slogin = Mgs.ManageLoginC(txtUsername, txtPassword, sCon , flaglogin);
                if (slogin)
                {
                    HttpCookie cookie_khet = new HttpCookie("_zone", Mgs._zone);
                    HttpCookie cookie_wwcode = new HttpCookie("_wwcode" , Mgs._wwcode);
                    HttpCookie cookie_level = new HttpCookie("_level" , Mgs.UserAccess_level);
                    cookie_khet.Expires = DateTime.Now.AddMinutes(30d);
                    cookie_wwcode.Expires = DateTime.Now.AddMinutes(30d);
                    cookie_level.Expires = DateTime.Now.AddMinutes(30d);
                    Response.Cookies.Add(cookie_khet);
                    Response.Cookies.Add(cookie_wwcode);
                    Response.Cookies.Add(cookie_level);

                    //cookie["_region"] = Mgs._region;
                    //cookie_khet["_zone"] = Mgs._zone;
                    //cookie_wwcode["_wwcode"] = Mgs._wwcode;
                    //cookie["_level"] = Mgs.UserAccess_level;
                    //Response.Cookies.Add(cookie);
                    //cookie.Value = DateTime.Now.ToString();
                    //Response.Cookies.Add(cookie);

                    Hashtable htbUser = new Hashtable();
                    htbUser.Add("UserID", Mgs.UserID);
                    htbUser.Add("UserName", Mgs.UserName);
                    htbUser.Add("Userposition", Mgs.Userposition);
                    htbUser.Add("UserAccess_level", Mgs.UserAccess_level);
                    htbUser.Add("UserAdmin", Mgs.User_Admin);
                    htbUser.Add("UserRegion", Mgs._region);
                    htbUser.Add("UserZone", Mgs._zone);
                    htbUser.Add("UserBranch_defualt", Mgs._wwcode);
                    htbUser.Add("UserCons", sCon);
                    htbUser.Add("UserCons_PortalDB", sCon_portaldb);
                    Session["USER"] = htbUser;

                    //Server.Transfer("index.aspx");
                    Response.Redirect("controlvalve.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else { Alert(Mgs.txtAlert, Mgs.AlertType); }
            }
        }

        protected void Alert(string strMassage, string alertType)
        {
            litMsg.Text = "<script type=\"text/javascript\" language=\"javascript\">";
            litMsg.Text += "   swalAlert('" + strMassage + ".','" + alertType + "') ";
            litMsg.Text += "</script>";
        }

        [System.Web.Services.WebMethod]
        public static string Logout()
        {
            //String Path = HttpContext.Current.Request.Url.AbsoluteUri;
            //String[] sPathArry = Path.Split('/');
            //HttpContext.Current.Session.Abandon();
            //string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            if (HttpContext.Current.Request.Cookies["_zone"] != null || HttpContext.Current.Request.Cookies["_wwcode"] != null 
                || HttpContext.Current.Request.Cookies["_level"] != null)
            {
                HttpContext.Current.Response.Cookies["_zone"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies["_wwcode"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies["_level"].Expires = DateTime.Now.AddDays(-1);
            }

            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        protected bool flaglogin { get; set; }

        protected string UserName
        {
            get
            {
                return (Request.QueryString["username"]);
            }
        }

        protected string Password
        {
            get
            {
                return (Request.QueryString["pass"]);
            }
        }

        protected string wwcode
        {
            get
            {
                return (Request.QueryString["dmawwcode"]);
            }
        }

        protected string dmacode
        {
            get
            {
                return (Request.QueryString["devicecode"]);
            }
        }
    }
}