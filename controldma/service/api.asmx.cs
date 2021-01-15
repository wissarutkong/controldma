using controldma.App_Code;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace controldma.service
{
    /// <summary>
    /// Summary description for api
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class api : System.Web.Services.WebService
    {

        public WebManageUserData user;

        public api(WebManageUserData user)
        {
            this.user = user;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        [WebMethod]
        public string Getddlkhet()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;

                options += "<option value=' '>กรุณาเลือก</option></br>";

                DataTable dt = inl.GetDatabySQL(" SELECT id , name FROM districts ORDER BY id ", user.UserCons );

                foreach (DataRow row in dt.Rows)
                {
                    string code = row[Cs_initaldata.Feild.ID.ToString()].ToString();
                    string name = row[Cs_initaldata.Feild.NAME.ToString()].ToString();

                    options += "<option value='" + code + "'>" + name + "</option></br>";
                }

                var keyValues = new Dictionary<string, string>
               {
                   { "option", options }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }
    }
}
