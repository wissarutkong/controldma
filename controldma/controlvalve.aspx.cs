using controldma.App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace controldma
{
    public partial class controlvalve : System.Web.UI.Page
    {
        public WebManageUserData user;
        public string unit_percent;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable(); userDetail = (Hashtable)Session["USER"]; user = new WebManageUserData(userDetail);
                Cs_Controlcenter cs = new Cs_Controlcenter(); unit_percent = cs.unit_percent();
            }
            else { Response.Redirect(new Cs_manageLoing().GetLoginPage()); }
        }
    }
}