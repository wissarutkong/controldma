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
    public partial class master : System.Web.UI.MasterPage
    {
        public WebManageUserData user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable(); userDetail = (Hashtable)Session["USER"]; user = new WebManageUserData(userDetail);
                txtUsername.InnerText = user.UserNAME.ToString(); 
            }
            else { Response.Redirect(new Cs_manageLoing().GetLoginPage()); }
        }

        private void InitialDoctorAutoSuggestion()
        {
            _khet.Attributes.Add("data-live-search", "true");
            _khet.Attributes.Add("data-width", "100%");
            _khet.Attributes.Add("data-size", "12");
            Cs_initaldata inl = new Cs_initaldata(user);

            DataTable dailyDepartments = inl.GetDatabySQL(" SELECT id , name FROM districts ORDER BY id ", user.UserCons);

            if (dailyDepartments.Rows.Count == 0)
            {
                DataRow tempRow = dailyDepartments.NewRow();

                tempRow[Cs_initaldata.Feild.NAME.ToString()] = "ไม่พบหน่วยงาน";
                tempRow[Cs_initaldata.Feild.ID.ToString()] = "";

                dailyDepartments.Rows.Add(tempRow);
            }
            else
            {
                DataRow tempRow = dailyDepartments.NewRow();

                tempRow[Cs_initaldata.Feild.NAME.ToString()] = "กรุณาเลือกเขต";
                tempRow[Cs_initaldata.Feild.ID.ToString()] = "0";

                dailyDepartments.Rows.InsertAt(tempRow, 0);
            }

            /* 
             * Generate ข้อมูลลง dropdown list
             */
            _khet.DataSource = dailyDepartments;
            _khet.DataTextField = Cs_initaldata.Feild.NAME.ToString();
            _khet.DataValueField = Cs_initaldata.Feild.ID.ToString();
            _khet.DataBind();

        }
    }
}