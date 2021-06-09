using controldma.App_Code;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace controldma.service
{
    public partial class api1 : System.Web.UI.Page
    {
        public static WebManageUserData user;
        public static string _wwcode;
        public static string _dmacode;
        public static string _remote_name;
        public static string _dvtype_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null) { Hashtable userDetail = new Hashtable(); userDetail = (Hashtable)Session["USER"]; user = new WebManageUserData(userDetail); }
            else { Response.Redirect(new Cs_manageLoing().GetLoginPage()); }
        }

        [System.Web.Services.WebMethod]
        public static string GetddlTimer()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;

                DataTable dt_timeList = inl.TimerList();

                foreach (DataRow row in dt_timeList.Rows)
                {
                    string code = row[Cs_initaldata.Feild.time_objid.ToString()].ToString();
                    string name = row[Cs_initaldata.Feild.time_label_long.ToString()].ToString();

                    options += "<option value='" + code + "'>" + name + "</option>";
                }

                var keyValues = new Dictionary<string, string>
               {
                   { "option", options }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getid_khetfromwwcodeid(String wwcode_id)
        {
            /*
             * ตรวจสอบว่า User ผ่านการ Login มาหรือยัง
             */
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                if (string.IsNullOrEmpty(wwcode_id))
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }

                String districts_id = inl.GetStringbySQL("SELECT d.id FROM branches b INNER JOIN districts d on b.district_id = d.id WHERE b.id = '" + wwcode_id + "'", user.UserCons);

                var keyValues = new Dictionary<string, string>
               {
                   { "id", districts_id }
               };
                return JsonConvert.SerializeObject(keyValues);

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string m_dvtypeddl()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;

                DataTable dt = inl.GetDatabySQL(" SELECT dvtype_id , dvtype_name FROM tb_ctr_mm_devicetype WHERE dvtype_id<>1 AND dvtype_flag = 1 ORDER BY dvtype_id ", user.UserCons_PortalDB);

                options += "<option value=' '>กรุณาประเภท</option>";
                foreach (DataRow row in dt.Rows)
                {
                    string code = row[Cs_initaldata.Feild.dvtype_id.ToString()].ToString();
                    string name = row[Cs_initaldata.Feild.dvtype_name.ToString()].ToString();

                    options += "<option value='" + code + "'>" + name + "</option>";
                }

                var keyValues = new Dictionary<string, string>
               {
                   { "option", options }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string m_controltype()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;

                DataTable dt = inl.GetDatabySQL(" SELECT control_type_id , control_type_name FROM tb_ctr_dmacontroltype ORDER BY control_type_id ", user.UserCons_PortalDB);

                foreach (DataRow row in dt.Rows)
                {
                    string code = row[Cs_initaldata.Feild.control_type_id.ToString()].ToString();
                    string name = row[Cs_initaldata.Feild.control_type_name.ToString()].ToString();

                    options += "<option value='" + code + "'>" + name + "</option>";
                }

                var keyValues = new Dictionary<string, string>
               {
                   { "option", options }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string m_Templatecontrol()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;

                DataTable dt = inl.GetDatabySQL(" SELECT id as template_id , desc_template  FROM tb_ctr_cycle_counter ORDER BY id asc  ", user.UserCons_PortalDB);

                foreach (DataRow row in dt.Rows)
                {
                    string code = row[Cs_initaldata.Feild.template_id.ToString()].ToString();
                    string name = row[Cs_initaldata.Feild.desc_template.ToString()].ToString();

                    options += "<option value='" + code + "'>" + name + "</option>";
                }

                var keyValues = new Dictionary<string, string>
               {
                   { "option", options }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetddlDateOption()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;
                for (int i = 1; i <= 31; i++)
                {
                    options += "<option value=\"" + i + "\">" + i + "</option>";
                }

                var keyValues = new Dictionary<string, string>
               {
                   { "option", options }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getddlkhet(String khet)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;

                options += "<option value=' '>กรุณาเลือกเขต</option></br>";
                String strSQL = string.Empty;
                if (user.UserAccess_level.ToString() == "15" || user.UserAccess_level.ToString() == "10")
                {
                    strSQL = "SELECT id , name FROM districts WHERE id = '" + khet + "'";
                }
                else if (user.UserAccess_level.ToString() == "1")
                {
                    strSQL = "SELECT id , name FROM districts ORDER BY id";
                }
                else { strSQL = "SELECT id , name FROM districts ORDER BY id"; }



                DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons);

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

        [System.Web.Services.WebMethod]
        public static string Getddlsite(String khet_id, String wwcode_id)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);

                string options = string.Empty;

                options += "<option value=' '>กรุณาเลือกสาขา</option></br>";
                String strSQL = string.Empty;
                if (user.UserAccess_level.ToString() == "15" && wwcode_id != "")
                {
                    strSQL = "SELECT id , name FROM branches WHERE id= '" + wwcode_id.Trim() + "' ORDER BY id";
                }
                //else if (user.UserAccess_level.ToString() == "1" && khet_id.ToString() != "")
                //{
                //    strSQL = "SELECT id , name FROM branches WHERE district_id = '"+ khet_id.Trim() + "' ORDER BY id";
                //}
                else { strSQL = "SELECT id , name FROM branches WHERE district_id = '" + khet_id.Trim() + "' ORDER BY id"; }


                DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons);

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

        [System.Web.Services.WebMethod]
        public static string GetCtr002(String wwcode_id, String dmacode)
        {
            /*
             * ตรวจสอบว่า User ผ่านการ Login มาหรือยัง
             */
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode_id + "'", user.UserCons);

                String strSQL = "EXEC sp_ctrl_get_crtl002 @wwcode = '" + wwcode + "',@dmacode= '" + dmacode + "';";

                DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                DataTable dt_temp = new DataTable();

                dt_temp.Columns.Add("description", typeof(string));
                dt_temp.Columns.Add("remote_name", typeof(string));
                dt_temp.Columns.Add("dvtype_name", typeof(string));
                dt_temp.Columns.Add("control_mode", typeof(string));
                dt_temp.Columns.Add("failure_mode", typeof(string));
                dt_temp.Columns.Add("Flow", typeof(string));
                dt_temp.Columns.Add("Pressure", typeof(string));
                dt_temp.Columns.Add("LastUpdate", typeof(string));
                dt_temp.Columns.Add("Detail", typeof(string));
                dt_temp.Columns.Add("Edit", typeof(string));
                dt_temp.Columns.Add("Add", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    DataRow _temp = dt_temp.NewRow();
                    _temp["description"] = row["description"];
                    _temp["remote_name"] = row["remote_name"];
                    _temp["dvtype_name"] = row["dvtype_name"];
                    _temp["control_mode"] = row["control_mode"];
                    _temp["failure_mode"] = row["failure_mode"];
                    _temp["Flow"] = row["Flow"];
                    _temp["Pressure"] = row["Pressure"];
                    _temp["LastUpdate"] = row["LastUpdate"];
                    _temp["Detail"] = "<button type=\"button\" id=\"info_" + row["dmacode"] + "\" class=\"btn btn-block btn-info btn-sm btn-flat infovalva\" value=\"" + row["dmacode"] + "\" data-type=\"" + row["dvtype_id"] + "\" data-remote=\"" + row["remote_name"] + "\" data-toggle=\"modal\" data-target=\"#Modal_info_valva\" ><span><i class=\"fas fa-info-circle\"></i> รายละเอียด</span></button>";

                    if (Convert.ToBoolean(user.UserAdmin))
                    {
                        _temp["Add"] = "<button type=\"button\" id=\"" + row["dmacode"] + "\" class=\"btn btn-block btn-warning btn-sm btn-flat addvalva\" value=\"" + row["dmacode"] + "\" data-type=\"" + row["dvtype_id"] + "\"  data-toggle=\"modal\" data-target=\"\" ><span><i class=\"far fa-edit\"></i> กำหนดประตูน้ำ</span></button>";
                    }

                    if (row.IsNull("is_smartlogger") || row["is_smartlogger"] == "")
                        row["is_smartlogger"] = 0;

                    if ((Boolean)row["is_smartlogger"])
                    {
                        _temp["Edit"] = "<a href=\"http://smartlogger.pwa.co.th/#/redirect?pagename=prv_config&token=" + user.UserTokenAuthen + "&dmacode=" + row["dmacode"] + "\" id=\"" + row["dmacode"] + "\" class=\"btn btn-block btn-primary btn-sm btn-flat\" role=\"button\" target=\"_blank\"><i class=\"fas fa-cog\"></i>SmartLogger</a>";
                    }
                    else {
                        _temp["Edit"] = "<button type=\"button\" id=\"" + row["dmacode"] + "\" class=\"btn btn-block btn-danger btn-sm btn-flat editvalva\" value=\"" + row["dmacode"] + "\" data-type=\"" + row["dvtype_id"] + "\" data-remote=\"" + row["remote_name"] + "\" data-template=\"" + row["counter_template"] + "\"  data-toggle=\"modal\" data-target=\"\" ><span><i class=\"fas fa-cog\"></i>ตั้งค่า</span></button>";
                    }

                    dt_temp.Rows.Add(_temp);
                }

                var resultArr = dt_temp.Rows.Cast<DataRow>().Select(r => r.ItemArray).ToArray();

                return JsonConvert.SerializeObject(resultArr);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetRealtimeDataCtr002(String remote_name)
        {/*
             * ตรวจสอบว่า User ผ่านการ Login มาหรือยัง
             */
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                if (!string.IsNullOrEmpty(remote_name))
                {
                    String strSQL = "EXEC sp_ctr_get_realtime_dmama2 @id_name = '" + remote_name + "';";

                    DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                    var resultArr = dt.Rows.Cast<DataRow>().Select(r => r.ItemArray).ToArray();

                    return JsonConvert.SerializeObject(resultArr);
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetHistoryDataCtr002(String wwcode, String dmacode, String dvtype)
        {/*
             * ตรวจสอบว่า User ผ่านการ Login มาหรือยัง
             */
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dvtype) && !String.IsNullOrEmpty(dmacode))
                {
                    wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                    String strSQL = string.Empty;
                    if (dvtype == "2" || dvtype == "4")
                    {
                        strSQL = "EXEC sp_ctrl_get_bvcmdlog_dmama2 @wwcode = '" + wwcode + "',@dmacode= '" + dmacode + "';";
                    }
                    else if (dvtype == "6")
                    {
                        strSQL = "EXEC sp_ctrl_get_afvcmdlog_dmama2 @wwcode = '" + wwcode + "',@dmacode= '" + dmacode + "';";
                    }
                    else {
                        strSQL = "EXEC sp_ctrl_get_prvtcmdlog_dmama2 @wwcode = '" + wwcode + "',@dmacode= '" + dmacode + "';";
                    }

                    DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                    foreach (DataRow row in dt.Rows)
                    {
                        int index = dt.Rows.IndexOf(row);
                        //dt.Rows[index]["cmd_dtm"] = Convert.ToDateTime(row["cmd_dtm"].ToString());
                        dt.Rows[index]["cmd_desc"] = "<button type=\"button\" class=\"btn btn-block btn-info btn-sm btn-flat commandinfo\" value=\"" + row["cmd_desc"].ToString().Trim() + "\"  data-toggle=\"modal\" data-target=\"\" ><span><i class=\"fas fa-key\"></i> คลิ๊กเพื่อดู</span></button>";
                    }

                    var resultArr = dt.Rows.Cast<DataRow>().Select(r => r.ItemArray).ToArray();

                    return JsonConvert.SerializeObject(resultArr);
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetCtr002_All(String mainDataText)
        {
            /*
             * ตรวจสอบว่า User ผ่านการ Login มาหรือยัง
             */
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];

                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                String strSQL = "EXEC sp_ctrl_get_crtl002 @wwcode = '" + wwcode + "',@dmacode= '" + mainData["$_dmacode"].ToString() + "';";
                DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                return JsonConvert.SerializeObject(dt);

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getdata_prv(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                //set global variable _dvtype_id type of bv / prv 
                _dvtype_id = mainData["$_datatype"].ToString();

                #region _manual
                string strSQL = @"SELECT vt.wwcode, vt.dmacode,cf.pilot_num_ord,vt.remote_name,vt.dvtype_id,vt.pilot_num,convert(varchar,hprvt.pilot_no) as pilot_no,cf.pilot_pressure
                        FROM tb_ctr_dmavalvetype vt left join (select top 1 * from tb_ctr_cmdprvthead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "' and control_mode = '0' order by cmdprvthead_id desc) hprvt on vt.dmacode = hprvt.dmacode right join tb_ctr_dmaconfigpressure cf on cf.wwcode = vt.wwcode And cf.dmacode = vt.dmacode where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "'";
                DataTable dt_Solenoid = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                //strSQL = @"select * from tb_ctr_dmaconfigpressure where wwcode = '" + wwcode + "' and dmacode = '" + mainData["$_dmacode"].ToString() + "'";
                DataTable dt_Pressure = inl.GetConfigpressure(wwcode, mainData["$_dmacode"].ToString(), user.UserCons_PortalDB);

                String Html = string.Empty; String ischecked = string.Empty; int count = 1; String id = string.Empty;
                foreach (DataRow row in dt_Solenoid.Rows)
                {
                    if (row["pilot_no"].ToString() != null && row["pilot_no"].ToString() != "")
                    {
                        if (Convert.ToInt32(row["pilot_no"]) == count) { ischecked = "checked=\"checked\""; }
                    }
                    id = "Solenoid" + count.ToString();
                    Html += "<div class=\"list-group-item\">";
                    Html += "   <div class=\"row\">";
                    Html += "       <div class=\"col-lg-4\" style=\"margin-top:10px\">";
                    Html += "          ตัวที่ " + count + "  Pressure " + row["pilot_pressure"].ToString() + " (bar)";
                    Html += "       </div>";
                    Html += "       <div class=\"col-lg-8\">";
                    Html += "           <input id=\"" + id + "\" name=\"Solenoid\" class=\"form-control\" style=\"width: 15%\" type=\"checkbox\" value=\"" + count + "\" " + ischecked + " />";
                    Html += "           <input type=\"hidden\" id=\"txtSolenoid\" name=\"txtSolenoid\" value=\"" + count + "\" />";
                    Html += "       </div>";
                    Html += "   </div>";
                    Html += "</div>";
                    count++; ischecked = string.Empty;
                }
                Html += "<br><button type=\"button\" class=\"btn btn-primary btn-flat col-md-2\" data-toggle=\"modal\" href=\"#aboutModal_save\" onclick=\"Popup(2,'manual')\" ><i class=\"fa fa-floppy-o\"></i>" + Cs_Controlcenter.BtnSave() + "</button>";
                #endregion

                #region _Automatic
                DataTable dt_cmdprvthead = new DataTable(); DataTable dt_cmdprvtdetail = new DataTable(); DataTable dt_pilot_num = new DataTable();

                try
                {
                    strSQL = string.Empty;
                    strSQL = @"select top 1 * from tb_ctr_cmdprvthead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdprvthead_id desc";
                    dt_cmdprvthead = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    strSQL = string.Empty;
                    strSQL = "select * from tb_ctr_cmdprvtdetail where cmdprvthead_id";
                    strSQL += " in(select top 1 cmdprvthead_id from tb_ctr_cmdprvthead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdprvthead_id desc) order by order_time asc";
                    dt_cmdprvtdetail = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    strSQL = string.Empty;
                    strSQL = "SELECT vt.* , cy.counter_template FROM tb_ctr_dmavalvetype vt INNER JOIN tb_ctr_cycle_counter cy on cy.id = vt.cycle_id ";
                    strSQL += " where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "'";
                    dt_pilot_num = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }
                String Html_2 = string.Empty;
                var selected_dv_0 = "";
                var selected_dv_4 = "";
                var disablePressure = "";
                var disableFlow = "";
                var disableValve = "";
                var IndexNo = 0;
                var isDevice = true;
                var tblAutomatic = "tblAutomatic";
                string timevalue = "";
                int total = 1;
                int pilot_num = 1;
                int template_counter = 0;
                DataTable dt_timeList = inl.TimerList();

                total = dt_cmdprvtdetail.Rows.Count;
                if (total == 0) { total = 1; }
                pilot_num = Convert.ToInt32(dt_pilot_num.Rows[0]["pilot_num"]);
                template_counter = Convert.ToInt32(dt_pilot_num.Rows[0]["counter_template"]);

                IndexNo = 0;
                foreach (DataRow row in dt_cmdprvtdetail.Rows)
                {
                    IndexNo += 1;
                    string pilot_no_value = string.Empty;
                    if (row.IsNull("pilot_no") || row["pilot_no"] == "")
                        pilot_no_value = IndexNo.ToString();
                    else if (Convert.ToInt32(row["pilot_no"]) > pilot_num)
                        pilot_no_value = IndexNo.ToString();
                    else
                        pilot_no_value = IndexNo.ToString() + row["pilot_no"].ToString();

                    //int pilotnumdetail = row["pilot_no"];

                    //string pilot_no_value = IndexNo.ToString() + row["pilot_no"].ToString();
                    //if (Convert.ToInt32(row["pilot_no"]) > pilot_num)
                    //    pilot_no_value = IndexNo.ToString();


                    switch (IndexNo)
                    {
                        case 1:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_1\" value=\"" + pilot_no_value + "\" />";
                            break;
                        case 2:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_2\" value=\"" + pilot_no_value + "\" />";
                            break;
                        case 3:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_3\" value=\"" + pilot_no_value + "\" />";
                            break;
                        case 4:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_4\" value=\"" + pilot_no_value + "\" />";
                            break;
                        case 5:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_5\" value=\"" + pilot_no_value + "\" />";
                            break;
                        case 6:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_6\" value=\"" + pilot_no_value + "\" />";
                            break;
                        case 7:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_7\" value=\"" + pilot_no_value + "\" />";
                            break;
                        case 8:
                            Html_2 += "<input type=\"hidden\" id=\"pilot_8\" value=\"" + pilot_no_value + "\" />";
                            break;
                    }
                }
                if (IndexNo < (template_counter + 1))
                {
                    var j = IndexNo + 1; for (int i = j; i <= template_counter; i++)
                    {
                        string idpilot = "pilot_" + i.ToString();
                        Html_2 += "<input type=\"hidden\" id=\"" + idpilot + "\" value=\"0\" />";
                    }
                }

                Html_2 += "<div class=\"table-responsive\">";
                Html_2 += " <table class=\"table table-striped table-bordered dt-responsive clear-center\" id=\"tblPrvAutomatic\" name=\"tblPrvAutomatic\">";
                Html_2 += "     <thead>";
                Html_2 += "         <tr>";
                Html_2 += "             <th width=\"5%\">No.</th>";
                Html_2 += "             <th width=\"10%\">Device Mode</th>";
                Html_2 += "             <th width=\"10%\">Time</th>";
                foreach (DataRow row in dt_Solenoid.Rows)
                {
                    Html_2 += "<th>ตัวที่ " + row["pilot_num_ord"].ToString() + " Pressure " + row["pilot_pressure"].ToString() + " (bar)</th>";
                }

                Html_2 += "         </tr>";
                Html_2 += "     </thead>";
                Html_2 += "     <tbody>";
                if (dt_cmdprvtdetail.Rows.Count > 0)
                {
                    IndexNo = 0;
                    foreach (DataRow row in dt_cmdprvtdetail.Rows)
                    {
                        IndexNo += 1;
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        var txtValve = "txtValve" + @IndexNo;
                        isDevice = false;
                        if (IndexNo == 1)
                            isDevice = true;
                        selected_dv_0 = "";
                        selected_dv_4 = "";
                        if (Convert.ToInt32(row["failure_mode"]) == 0)
                            selected_dv_0 = "selected";
                        if (Convert.ToInt32(row["failure_mode"]) == 4)
                            selected_dv_4 = "selected";

                        disablePressure = "disabled";
                        disableFlow = "disabled";
                        disableValve = "disabled";

                        if (Convert.ToInt32(row["failure_mode"]) == 1)
                            disablePressure = "";
                        if (Convert.ToInt32(row["failure_mode"]) == 2)
                            disableFlow = "";
                        if (Convert.ToInt32(row["failure_mode"]) == 3)
                            disableValve = "";

                        Html_2 += "     <tr>";
                        Html_2 += "         <td align=\"left\">" + IndexNo + " </td>";
                        Html_2 += "         <td>";
                        Html_2 += "             <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        Html_2 += "                 <option value=\"4\" " + selected_dv_4 + ">Enable</option>";
                        Html_2 += "                 <option value=\"0\" " + selected_dv_0 + " disabled>Disable</option>";
                        Html_2 += "             </select>";
                        Html_2 += "         </td>";
                        Html_2 += "         <td>";
                        Html_2 += "             <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\">";
                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = row["time_start"].ToString();
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }
                        Html_2 += "             </select>";
                        Html_2 += "         </td>";

                        string id_pilot = string.Empty;
                        string pilotid = string.Empty;
                        ischecked = string.Empty;
                        var value = "";
                        for (int k = 1; k <= Convert.ToInt32(dt_pilot_num.Rows[0]["pilot_num"]); k++)
                        {
                            id_pilot = "pilot_" + IndexNo.ToString() + k.ToString();
                            pilotid = "pilot_" + IndexNo.ToString();
                            value = "";
                            ischecked = string.Empty;
                            if (row["pilot_no"] != null && row["pilot_no"].ToString() != "")
                            {
                                if (k.ToString() == row["pilot_no"].ToString())
                                {
                                    ischecked = "checked=\"checked\"";
                                    value = IndexNo.ToString() + k.ToString();
                                }
                            }
                            Html_2 += "     <td>";
                            Html_2 += "         <input id=\"" + id_pilot + "\" name=\"" + pilotid + "\" class=\"form-control\" type=\"checkbox\" value=\"" + IndexNo + k + "\" " + ischecked + " />";
                            Html_2 += "     </td>";

                        }
                        Html_2 += "</tr>";
                    }
                }
                else {
                    for (int i = 0; i < 2; i++)
                    {
                        IndexNo += 1;
                        if (IndexNo == 2)
                        {
                            break;
                        }
                        var delete = "btnDelete" + IndexNo;
                        var add = "btnAdd" + IndexNo;
                        var selmode = "selmode" + IndexNo;
                        var txttime = "txttime" + IndexNo;
                        var txtPressure = "txtPressure" + IndexNo;
                        var txtFlow = "txtFlow" + IndexNo;
                        var txtValve = "txtValve" + IndexNo;
                        if (IndexNo == 2)
                            isDevice = false;
                        Html_2 += "     <tr>";
                        Html_2 += "         <td align=\"left\">" + IndexNo + "</td>";
                        Html_2 += "         <td>";
                        Html_2 += "             <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        Html_2 += "                 <option value=\"4\">Enable</option>";
                        Html_2 += "                 <option value=\"0\" disabled>Disable</option>";
                        Html_2 += "             </select>";
                        Html_2 += "         </td>";
                        Html_2 += "         <td>";
                        Html_2 += "             <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" disabled=\"true\">";
                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            Html_2 += "             <option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                        }
                        Html_2 += "             </select>";
                        Html_2 += "         </td>";

                        string id_pilot = string.Empty;
                        string pilotid = string.Empty;
                        ischecked = string.Empty;
                        for (int k = 1; k <= Convert.ToInt32(dt_pilot_num.Rows[0]["pilot_num"]); k++)
                        {
                            ischecked = string.Empty;
                            id_pilot = "pilot_" + IndexNo.ToString() + k.ToString();
                            pilotid = "pilot_" + IndexNo.ToString();
                            if (k == 1)
                            {
                                //เปลี่ยนจำนวนช่วง pilot ได้
                                for (int pilot = 1; pilot <= template_counter; pilot++)
                                {
                                    Html_2 += "<input type=\"hidden\" id=\"pilot_" + pilot + "\" />";
                                }
                                //Html_2 += "<input type=\"hidden\" id=\"pilot_2\" />";
                                //Html_2 += "<input type=\"hidden\" id=\"pilot_3\" />";
                                //Html_2 += "<input type=\"hidden\" id=\"pilot_4\" />";
                                //Html_2 += "<input type=\"hidden\" id=\"pilot_5\" />";
                                //Html_2 += "<input type=\"hidden\" id=\"pilot_6\" />";
                            }
                            Html_2 += "        <td>";
                            Html_2 += "         <input id=\"" + id_pilot + "\" name=\"" + pilotid + "\" class=\"form-control\" type=\"checkbox\" value=\"" + IndexNo + k + "\" " + ischecked + " />";
                            Html_2 += "         </td>";
                        }

                        Html_2 += "     </tr>";
                        Html_2 += "";
                        Html_2 += "";
                    }
                }
                Html_2 += "     </tbody>";
                Html_2 += "</table>";
                Html_2 += " </div>";
                Html_2 += "<br><button type=\"button\" class=\"btn btn-primary btn-flat col-md-2\" data-toggle=\"modal\" href=\"#aboutModal_save\" onclick=\"Popup(1,'auto')\" ><i class=\"fa fa-floppy-o\"></i>" + Cs_Controlcenter.BtnSave() + "</button>";
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                Html_2 += " <input type=\"hidden\" id=\"txtpilot_num\" value=\"" + pilot_num + "\" />";
                #endregion

                #region _Realtime

                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime\" class=\"table table-hover table-sm table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_3 += "             <thead>";
                Html_3 += "                    <tr>";
                Html_3 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_3 += "                         <th>Remote Name</th>";
                Html_3 += "                         <th>วันเวลา</th>";
                Html_3 += "                         <th>แรงดัน (ออก)(บาร์)</th>";
                Html_3 += "                         <th>แรงดัน (เข้า)(บาร์)</th>";
                Html_3 += "                         <th>Valve (%)</th>";
                Html_3 += "                         <th>อัตราการไหล (ลบ.ม.)</th>";
                Html_3 += "                     </tr>";
                Html_3 += "             </thead>";
                Html_3 += "         </table>";
                Html_3 += "     </div>";
                //Html_3 += "</div>";

                #endregion

                #region _History

                String Html_4 = string.Empty;

                //Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "    <div class=\"table-responsive Flipped\">";
                Html_4 += "     <div class=\"Content\">";
                Html_4 += "         <table id=\"dt_grid_history\" class=\"table table-striped table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_4 += "             <thead>";
                Html_4 += "                    <tr>";
                Html_4 += "                         <th>ลำดับ</th>";
                Html_4 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_4 += "                         <th>วันที่ตั้งค่า</th>";
                Html_4 += "                         <th>รายละเอียด</th>";
                Html_4 += "                         <th>สั่งโดย</th>";
                //Html_4 += "                         <th>ไฟล์</th>";
                Html_4 += "                         <th>หมายเหตุ</th>";
                Html_4 += "                     </tr>";
                Html_4 += "             </thead>";
                Html_4 += "         </table>";
                Html_4 += "     </div>";
                Html_4 += "    </div>";
                //Html_4 += "</div>";

                #endregion

                var keyValues = new Dictionary<string, string>
               {
                   { "_manual", Html },
                   { "_Automatic", Html_2 },
                   { "_Realtime", Html_3 },
                   { "_History", Html_4 },
                   { "_txtRow" , total.ToString() }
               };
                return JsonConvert.SerializeObject(keyValues);

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getdata_bv(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                //set global variable _dvtype_id type of bv / prv 
                _dvtype_id = mainData["$_datatype"].ToString();
                String unit_percent = cs.unit_percent();
                #region _manual
                int percent_valve = 0;
                int template_counter = 0;
                DataTable dt_percent_valva = new DataTable();
                string strSQL = @"SELECT vt.wwcode, vt.dmacode,vt.remote_name,vt.dvtype_id,ISNULL(hbv.percent_valve,0) percent_valve , cy.counter_template
                        FROM tb_ctr_dmavalvetype vt
                        left join (select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '0' order by cmdbvhead_id desc) hbv on vt.dmacode = hbv.dmacode INNER JOIN tb_ctr_cycle_counter cy on cy.id = vt.cycle_id where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "' ";
                DataTable dt_ctr_bvmanual = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                if (dt_ctr_bvmanual.Rows.Count > 0)
                {
                    percent_valve = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["percent_valve"]);
                    template_counter = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["counter_template"]);
                }
                else {
                    strSQL = @" SELECT wwcode , dmacode ,remote_name, percent_valve  FROM tb_ctr_cmdbvhead WHERE wwcode = '" + wwcode + "' AND dmacode = '" + mainData["$_dmacode"].ToString() + "' AND percent_valve is not null ORDER BY cmd_data_dtm desc ";
                    dt_percent_valva = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    if (dt_percent_valva.Rows.Count > 0) percent_valve = Convert.ToInt32(dt_percent_valva.Rows[0]["percent_valve"]);
                }
                String Html = string.Empty;
                Html += "<div class=\"list-group-item\"> ";
                Html += "   <div class=\"row\">";
                Html += "       <div class=\"col-lg-1\" style=\"margin-top:10px\">";
                Html += "            Valve " + unit_percent + ": ";
                Html += "       </div>";
                Html += "       <div class=\"col-lg-10\" style=\"margin-top:10px\">";
                Html += "       <input id=\"txtvalve\" type=\"text\" value=\"" + percent_valve + "\"  class=\"form-control\" style=\"width: 100px\" onkeypress=\"return isNumberKey(event)\" maxlength=\"3\"> ";
                Html += "       </div> ";
                Html += "   </div> ";
                Html += "</div> ";
                Html += "<br><button type=\"button\" class=\"btn btn-primary btn-flat col-md-2\" data-toggle=\"modal\" href=\"#aboutModal_save\" onclick=\"Popup(2,'manual')\" ><i class=\"fa fa-floppy-o\"></i>" + Cs_Controlcenter.BtnSave() + "</button>";
                #endregion

                #region _Automatic
                DataTable dt_cmdbvhead = new DataTable(); DataTable dt_cmdbvdetail = new DataTable();
                try
                {
                    strSQL = string.Empty;
                    strSQL = @"select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc";
                    dt_cmdbvhead = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    strSQL = string.Empty;
                    strSQL = @" select * from tb_ctr_cmdbvdetail where cmdbvhead_id 
                                in(select top 1 cmdbvhead_id from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc) order by order_time asc";
                    dt_cmdbvdetail = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

                int total = 1;

                total = dt_cmdbvdetail.Rows.Count;
                if (total == 0) { total = 1; }

                String Html_2 = string.Empty;
                var selected_dv_0 = "";
                var selected_dv_1 = "";
                var selected_dv_2 = "";
                var selected_dv_3 = "";
                var disablePressure = "";
                var disableFlow = "";
                var disableValve = "";
                var IndexNo = 0;
                var isDevice = true;
                var isdisabled = "";
                var tblAutomatic = "tblAutomatic";
                string timevalue = "";
                string cmdbvheadfailure_mode = string.Empty;
                string cmdbvheadstep_control_delay = string.Empty;
                string cmdbvheadtime_loop = string.Empty;
                string cmdbvheadlimit_min = string.Empty;
                string cmdbvheaddeadband_pressure = string.Empty;
                string cmdbvheaddeadband_flow = string.Empty;
                DataTable dt_timeList = inl.TimerList();

                if (dt_cmdbvhead.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_cmdbvhead.Rows)
                    {
                        cmdbvheadfailure_mode = row["failure_mode"].ToString();
                        cmdbvheadstep_control_delay = row["step_control_delay"].ToString();
                        cmdbvheadtime_loop = row["time_loop"].ToString();
                        cmdbvheadlimit_min = row["limit_min"].ToString();
                        cmdbvheaddeadband_pressure = row["deadband_pressure"].ToString();
                        cmdbvheaddeadband_flow = row["deadband_flow"].ToString();
                    }
                }

                Html_2 += "<div class=\"table-responsive\">";
                Html_2 += "<table class=\"table table-bordered table-striped table-hover table-condensed\" id=\"tblBvAutomatic\" name=\"tblBvAutomatic\">";
                Html_2 += "     <thead>";
                Html_2 += "         <tr>";
                Html_2 += "             <th>No.</th>";
                Html_2 += "             <th>Device Mode</th>";
                Html_2 += "             <th>Time</th>";
                Html_2 += "             <th>Pressure</th>";
                Html_2 += "             <th>Flow</th>";
                Html_2 += "             <th>Valve</th>";
                Html_2 += "         </tr>";
                Html_2 += "     </thead>";
                Html_2 += "     <tbody>";
                if (dt_cmdbvdetail.Rows.Count > 0)
                {

                    IndexNo = 0;
                    foreach (DataRow row in dt_cmdbvdetail.Rows)
                    {
                        IndexNo += 1;
                        isdisabled = "";
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        var txtValve = "txtValve" + @IndexNo;
                        int failure_mode = Convert.ToInt32(row["failure_mode"]);

                        isDevice = false;
                        if (IndexNo == 1) { isDevice = true; isdisabled = "disabled"; }


                        string txtPressure_bgc = cs.GetBg_Color("p", failure_mode);
                        string txtFlow_bgc = cs.GetBg_Color("f", failure_mode);
                        string txtValve_bgc = cs.GetBg_Color("v", failure_mode);

                        selected_dv_0 = "";
                        selected_dv_1 = "";
                        selected_dv_2 = "";
                        selected_dv_3 = "";

                        if (failure_mode == 0)
                            selected_dv_0 = "selected";


                        if (failure_mode == 1)
                            selected_dv_1 = "selected";

                        if (failure_mode == 2)
                            selected_dv_2 = "selected";

                        if (failure_mode == 3)
                            selected_dv_3 = "selected";

                        disablePressure = "disabled";
                        disableFlow = "disabled";
                        disableValve = "disabled";

                        if (failure_mode == 1)
                            disablePressure = "";
                        if (failure_mode == 2)
                            disableFlow = "";
                        if (failure_mode == 3)
                            disableValve = "";



                        Html_2 += "<tr>";
                        Html_2 += "      <td align=\"center\">" + @IndexNo + "</td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "             <option value=\"0\"  " + selected_dv_0 + " " + isdisabled + ">Disable</option>";
                        Html_2 += "             <option value=\"1\" " + selected_dv_1 + ">Pressure</option>";
                        Html_2 += "             <option value=\"2\"  " + selected_dv_2 + ">Flow</option>";
                        Html_2 += "             <option value=\"3\"  " + selected_dv_3 + ">Valve</option>";
                        Html_2 += "         </select>";
                        Html_2 += "      </td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" " + isdisabled + ">";

                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = row["time_start"].ToString();
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }

                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += " <td> <input  value=\"" + row["pressure_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%; background-color:" + txtPressure_bgc + "\" maxlength=\"8\"> </td>";
                        Html_2 += " <td> <input  value=\"" + row["flow_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:" + txtFlow_bgc + "\" maxlength=\"8\" ></td>";
                        Html_2 += " <td> <input  value=\"" + row["valve_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:" + txtValve_bgc + "\" maxlength=\"8\" > </td>";
                        Html_2 += "</tr>";
                    }
                }
                else {
                    isdisabled = "disabled";
                    for (int i = 0; i < template_counter; i++)
                    {
                        IndexNo += 1;
                        if (IndexNo == 2)
                        {
                            break;
                        }

                        var delete = "btnDelete" + @IndexNo;
                        var add = "btnAdd" + @IndexNo;
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        var txtValve = "txtValve" + @IndexNo;
                        if (IndexNo == 2) { isDevice = false; isdisabled = ""; }


                        Html_2 += "<tr>";
                        Html_2 += "     <td align=\"center\">" + IndexNo + "</td>";
                        Html_2 += "     <td>";
                        Html_2 += "     <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "         <option value=\"0\" " + isdisabled + ">Disable</option>";
                        Html_2 += "         <option value=\"1\">Pressure</option>";
                        Html_2 += "         <option value=\"2\">Flow</option>";
                        Html_2 += "         <option value=\"3\">Valve</option>";
                        Html_2 += "     </select>";
                        Html_2 += "     </td>";
                        Html_2 += "     <td>";
                        Html_2 += "   <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" disabled=\"true\">";
                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                        }
                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%;\" maxlength=\"8\" value=\"\"></td>";
                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        Html_2 += "</tr>";
                    }

                }

                Html_2 += "         </tbody>";
                Html_2 += "     </table>";
                Html_2 += " </div>";
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                Html_2 += "";

                #endregion

                #region _Realtime
                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime_bv\" class=\"table table-hover table-sm table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_3 += "             <thead>";
                Html_3 += "                    <tr>";
                Html_3 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_3 += "                         <th>Remote Name</th>";
                Html_3 += "                         <th>วันเวลา</th>";
                Html_3 += "                         <th>แรงดัน (ออก)(บาร์)</th>";
                Html_3 += "                         <th>แรงดัน (เข้า)(บาร์)</th>";
                Html_3 += "                         <th>Valve (%)</th>";
                Html_3 += "                         <th>อัตราการไหล (ลบ.ม.)</th>";
                Html_3 += "                     </tr>";
                Html_3 += "             </thead>";
                Html_3 += "         </table>";
                Html_3 += "     </div>";
                //Html_3 += "</div>";

                #endregion

                #region _History
                String Html_4 = string.Empty;

                //Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "    <div class=\"table-responsive Flipped\">";
                Html_4 += "     <div class=\"Content\">";
                Html_4 += "         <table id=\"dt_grid_history_bv\" class=\"table table-striped table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_4 += "             <thead>";
                Html_4 += "                    <tr>";
                Html_4 += "                         <th>ลำดับ</th>";
                Html_4 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_4 += "                         <th>วันที่ตั้งค่า</th>";
                Html_4 += "                         <th>รายละเอียด</th>";
                Html_4 += "                         <th>สั่งโดย</th>";
                //Html_4 += "                         <th>ไฟล์</th>";
                Html_4 += "                         <th>หมายเหตุ</th>";
                Html_4 += "                     </tr>";
                Html_4 += "             </thead>";
                Html_4 += "         </table>";
                Html_4 += "     </div>";
                Html_4 += "    </div>";
                //Html_4 += "</div>";

                #endregion

                string body_footer = string.Empty;
                using (StreamReader reader = new StreamReader(context.Server.MapPath("~/template/prvstepping_footer_1.htm")))
                {
                    body_footer = reader.ReadToEnd();
                }
                body_footer = body_footer.Replace("{step_control_delay}", cmdbvheadstep_control_delay);
                body_footer = body_footer.Replace("{time_loop}", cmdbvheadtime_loop);
                body_footer = body_footer.Replace("{limit_min}", cmdbvheadlimit_min);
                body_footer = body_footer.Replace("{deadband_pressure}", cmdbvheaddeadband_pressure);
                body_footer = body_footer.Replace("{deadband_flow}", cmdbvheaddeadband_flow);

                var keyValues = new Dictionary<string, string>
               {
                   { "_manual", Html },
                   { "_Automatic", Html_2 },
                   { "_Realtime", Html_3 },
                   { "_History", Html_4 },
                   { "_txtRow" , total.ToString()},
                   { "_bodyfooter" , body_footer },
                   { "_failure_mode" , cmdbvheadfailure_mode }
                   //{ "_step_control_delay" , cmdbvheadstep_control_delay },
                   //{ "_time_loop" , cmdbvheadtime_loop },
                   //{ "_limit_min" , cmdbvheadlimit_min },
                   //{ "_deadband_pressure" , cmdbvheaddeadband_pressure },
                   //{ "_deadband_flow" , cmdbvheaddeadband_flow }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getdata_bv_new(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                //set global variable _dvtype_id type of bv / prv 
                _dvtype_id = mainData["$_datatype"].ToString();
                String unit_percent = cs.unit_percent();
                #region _manual
                int percent_valve = 0;
                int template_counter = 0;
                DataTable dt_percent_valva = new DataTable();
                string strSQL = @"SELECT vt.wwcode, vt.dmacode,vt.remote_name,vt.dvtype_id,ISNULL(hbv.percent_valve,0) percent_valve , cy.counter_template
                        FROM tb_ctr_dmavalvetype vt
                        left join (select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '0' order by cmdbvhead_id desc) hbv on vt.dmacode = hbv.dmacode INNER JOIN tb_ctr_cycle_counter cy on cy.id = vt.cycle_id where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "' ";
                DataTable dt_ctr_bvmanual = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                if (dt_ctr_bvmanual.Rows.Count > 0)
                {
                    percent_valve = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["percent_valve"]);
                    template_counter = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["counter_template"]);
                }
                else {
                    strSQL = @" SELECT wwcode , dmacode ,remote_name, percent_valve  FROM tb_ctr_cmdbvhead WHERE wwcode = '" + wwcode + "' AND dmacode = '" + mainData["$_dmacode"].ToString() + "' AND percent_valve is not null ORDER BY cmd_data_dtm desc ";
                    dt_percent_valva = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    if (dt_percent_valva.Rows.Count > 0) percent_valve = Convert.ToInt32(dt_percent_valva.Rows[0]["percent_valve"]);
                }
                String Html = string.Empty;
                Html += "<div class=\"list-group-item\"> ";
                Html += "   <div class=\"row\">";
                Html += "       <div class=\"col-lg-1\" style=\"margin-top:10px\">";
                Html += "            Valve " + unit_percent + ": ";
                Html += "       </div>";
                Html += "       <div class=\"col-lg-10\" style=\"margin-top:10px\">";
                Html += "       <input id=\"txtvalve\" type=\"text\" value=\"" + percent_valve + "\"  class=\"form-control\" style=\"width: 100px\" onkeypress=\"return isNumberKey(event)\" maxlength=\"3\"> ";
                Html += "       </div> ";
                Html += "   </div> ";
                Html += "</div> ";
                Html += "<br><button type=\"button\" class=\"btn btn-primary btn-flat col-md-2\" data-toggle=\"modal\" href=\"#aboutModal_save\" onclick=\"Popup(2,'manual')\" ><i class=\"fa fa-floppy-o\"></i>" + Cs_Controlcenter.BtnSave() + "</button>";
                #endregion

                #region _Automatic
                DataTable dt_cmdbvhead = new DataTable(); DataTable dt_cmdbvdetail = new DataTable();
                try
                {
                    strSQL = string.Empty;
                    strSQL = @"select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc";
                    dt_cmdbvhead = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    strSQL = string.Empty;
                    strSQL = @" select * from tb_ctr_cmdbvdetail where cmdbvhead_id 
                                in(select top 1 cmdbvhead_id from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc) order by order_time asc";
                    dt_cmdbvdetail = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

                int total = 1;

                total = dt_cmdbvdetail.Rows.Count;
                if (total == 0) { total = 1; }

                String Html_2 = string.Empty;
                var selected_dv_0 = "";
                var selected_dv_1 = "";
                var selected_dv_2 = "";
                var selected_dv_3 = "";
                var disablePressure = "";
                var disableFlow = "";
                var disableValve = "";
                var IndexNo = 0;
                var isDevice = true;
                var isdisabled = "";
                var tblAutomatic = "tblAutomatic";
                string timevalue = "";
                //string cmdbvheadfailure_mode = string.Empty;
                string cmdbvheadstep_control_delay = string.Empty;
                string cmdbvheadtime_loop = string.Empty;
                string cmdbvheadlimit_min = string.Empty;
                string cmdbvheaddeadband_pressure = string.Empty;
                string cmdbvheaddeadband_flow = string.Empty;
                DataTable dt_timeList = inl.TimerList();

                if (dt_cmdbvhead.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_cmdbvhead.Rows)
                    {
                        //cmdbvheadfailure_mode = row["failure_mode"].ToString();
                        cmdbvheadstep_control_delay = row["step_control_delay"].ToString();
                        cmdbvheadtime_loop = row["time_loop"].ToString();
                        cmdbvheadlimit_min = row["limit_min"].ToString();
                        cmdbvheaddeadband_pressure = row["deadband_pressure"].ToString();
                        cmdbvheaddeadband_flow = row["deadband_flow"].ToString();
                    }
                }

                Html_2 += "<div class=\"table-responsive\">";
                Html_2 += "<table class=\"table table-bordered table-striped table-hover table-condensed\" id=\"tblBvAutomatic\" name=\"tblBvAutomatic\">";
                Html_2 += "     <thead>";
                Html_2 += "         <tr>";
                Html_2 += "             <th>No.</th>";
                Html_2 += "             <th>Device Mode</th>";
                Html_2 += "             <th>Time</th>";
                Html_2 += "             <th>Pressure</th>";
                Html_2 += "             <th>Flow</th>";
                Html_2 += "             <th>Valve</th>";
                Html_2 += "         </tr>";
                Html_2 += "     </thead>";
                Html_2 += "     <tbody>";
                if (dt_cmdbvdetail.Rows.Count > 0)
                {

                    IndexNo = 0;
                    foreach (DataRow row in dt_cmdbvdetail.Rows)
                    {
                        IndexNo += 1;
                        isdisabled = "";
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        var txtValve = "txtValve" + @IndexNo;
                        int failure_mode = Convert.ToInt32(row["failure_mode"]);

                        isDevice = false;
                        if (IndexNo == 1) { isDevice = true; isdisabled = "disabled"; }


                        string txtPressure_bgc = cs.GetBg_Color("p", failure_mode);
                        string txtFlow_bgc = cs.GetBg_Color("f", failure_mode);
                        string txtValve_bgc = cs.GetBg_Color("v", failure_mode);

                        selected_dv_0 = "";
                        selected_dv_1 = "";
                        selected_dv_2 = "";
                        selected_dv_3 = "";

                        if (failure_mode == 0)
                            selected_dv_0 = "selected";


                        if (failure_mode == 1)
                            selected_dv_1 = "selected";

                        if (failure_mode == 2)
                            selected_dv_2 = "selected";

                        if (failure_mode == 3)
                            selected_dv_3 = "selected";

                        disablePressure = "disabled";
                        disableFlow = "disabled";
                        disableValve = "disabled";

                        if (failure_mode == 1)
                            disablePressure = "";
                        if (failure_mode == 2)
                            disableFlow = "";
                        if (failure_mode == 3)
                            disableValve = "";



                        Html_2 += "<tr>";
                        Html_2 += "      <td align=\"center\">" + @IndexNo + "</td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "             <option value=\"0\"  " + selected_dv_0 + " " + isdisabled + ">Disable</option>";
                        Html_2 += "             <option value=\"1\" " + selected_dv_1 + ">Pressure</option>";
                        Html_2 += "             <option value=\"2\"  " + selected_dv_2 + ">Flow</option>";
                        Html_2 += "             <option value=\"3\"  " + selected_dv_3 + ">Valve</option>";
                        Html_2 += "         </select>";
                        Html_2 += "      </td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" " + isdisabled + ">";

                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = row["time_start"].ToString();
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }

                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += " <td> <input  value=\"" + row["pressure_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%; background-color:" + txtPressure_bgc + "\" maxlength=\"8\"> </td>";
                        Html_2 += " <td> <input  value=\"" + row["flow_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:" + txtFlow_bgc + "\" maxlength=\"8\" ></td>";
                        Html_2 += " <td> <input  value=\"" + row["valve_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:" + txtValve_bgc + "\" maxlength=\"8\" > </td>";
                        Html_2 += "</tr>";
                    }
                }
                else {
                    isdisabled = "disabled";
                    for (int i = 0; i < template_counter; i++)
                    {
                        IndexNo += 1;
                        if (IndexNo == 2)
                        {
                            break;
                        }

                        var delete = "btnDelete" + @IndexNo;
                        var add = "btnAdd" + @IndexNo;
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        var txtValve = "txtValve" + @IndexNo;
                        if (IndexNo == 2) { isDevice = false; isdisabled = ""; }


                        Html_2 += "<tr>";
                        Html_2 += "     <td align=\"center\">" + IndexNo + "</td>";
                        Html_2 += "     <td>";
                        Html_2 += "     <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "         <option value=\"0\" " + isdisabled + ">Disable</option>";
                        Html_2 += "         <option value=\"1\">Pressure</option>";
                        Html_2 += "         <option value=\"2\">Flow</option>";
                        Html_2 += "         <option value=\"3\">Valve</option>";
                        Html_2 += "     </select>";
                        Html_2 += "     </td>";
                        Html_2 += "     <td>";
                        Html_2 += "   <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" disabled=\"true\">";
                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                        }
                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%;\" maxlength=\"8\" value=\"\"></td>";
                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        Html_2 += "</tr>";
                    }

                }

                Html_2 += "         </tbody>";
                Html_2 += "     </table>";
                Html_2 += " </div>";
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                Html_2 += "";

                #endregion

                #region _Realtime
                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime_bv\" class=\"table table-hover table-sm table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_3 += "             <thead>";
                Html_3 += "                    <tr>";
                Html_3 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_3 += "                         <th>Remote Name</th>";
                Html_3 += "                         <th>วันเวลา</th>";
                Html_3 += "                         <th>แรงดัน (ออก)(บาร์)</th>";
                Html_3 += "                         <th>แรงดัน (เข้า)(บาร์)</th>";
                Html_3 += "                         <th>Valve (%)</th>";
                Html_3 += "                         <th>อัตราการไหล (ลบ.ม.)</th>";
                Html_3 += "                     </tr>";
                Html_3 += "             </thead>";
                Html_3 += "         </table>";
                Html_3 += "     </div>";
                //Html_3 += "</div>";

                #endregion

                #region _History
                String Html_4 = string.Empty;

                //Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "    <div class=\"table-responsive Flipped\">";
                Html_4 += "     <div class=\"Content\">";
                Html_4 += "         <table id=\"dt_grid_history_bv\" class=\"table table-striped table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_4 += "             <thead>";
                Html_4 += "                    <tr>";
                Html_4 += "                         <th>ลำดับ</th>";
                Html_4 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_4 += "                         <th>วันที่ตั้งค่า</th>";
                Html_4 += "                         <th>รายละเอียด</th>";
                Html_4 += "                         <th>สั่งโดย</th>";
                //Html_4 += "                         <th>ไฟล์</th>";
                Html_4 += "                         <th>หมายเหตุ</th>";
                Html_4 += "                     </tr>";
                Html_4 += "             </thead>";
                Html_4 += "         </table>";
                Html_4 += "     </div>";
                Html_4 += "    </div>";
                //Html_4 += "</div>";

                #endregion

                string body_footer = string.Empty;
                using (StreamReader reader = new StreamReader(context.Server.MapPath("~/template/bv_footer_2.htm")))
                {
                    body_footer = reader.ReadToEnd();
                }
                body_footer = body_footer.Replace("{step_control_delay}", cmdbvheadstep_control_delay);
                body_footer = body_footer.Replace("{time_loop}", cmdbvheadtime_loop);
                body_footer = body_footer.Replace("{limit_min}", cmdbvheadlimit_min);
                body_footer = body_footer.Replace("{deadband_pressure}", cmdbvheaddeadband_pressure);
                body_footer = body_footer.Replace("{deadband_flow}", cmdbvheaddeadband_flow);

                var keyValues = new Dictionary<string, string>
               {
                   { "_manual", Html },
                   { "_Automatic", Html_2 },
                   { "_Realtime", Html_3 },
                   { "_History", Html_4 },
                   { "_txtRow" , total.ToString()},
                   { "_bodyfooter" , body_footer }
                   //{ "_failure_mode" , cmdbvheadfailure_mode }
                   //{ "_step_control_delay" , cmdbvheadstep_control_delay },
                   //{ "_time_loop" , cmdbvheadtime_loop },
                   //{ "_limit_min" , cmdbvheadlimit_min },
                   //{ "_deadband_pressure" , cmdbvheaddeadband_pressure },
                   //{ "_deadband_flow" , cmdbvheaddeadband_flow }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getdata_PrvStepping(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                //set global variable _dvtype_id type of bv / prv 
                _dvtype_id = mainData["$_datatype"].ToString();
                String unit_percent = cs.unit_percent();
                #region _manual
                int percent_valve = 0;
                int template_counter = 0;
                DataTable dt_percent_valva = new DataTable();
                string strSQL = @"SELECT vt.wwcode, vt.dmacode,vt.remote_name,vt.dvtype_id,ISNULL(hbv.percent_valve,0) percent_valve , cy.counter_template
                        FROM tb_ctr_dmavalvetype vt
                        left join (select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '0' order by cmdbvhead_id desc) hbv on vt.dmacode = hbv.dmacode INNER JOIN tb_ctr_cycle_counter cy on cy.id = vt.cycle_id where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "' ";
                DataTable dt_ctr_bvmanual = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                if (dt_ctr_bvmanual.Rows.Count > 0)
                {
                    percent_valve = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["percent_valve"]);
                    template_counter = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["counter_template"]);
                }
                else {
                    strSQL = @" SELECT wwcode , dmacode ,remote_name, percent_valve  FROM tb_ctr_cmdbvhead WHERE wwcode = '" + wwcode + "' AND dmacode = '" + mainData["$_dmacode"].ToString() + "' AND percent_valve is not null ORDER BY cmd_data_dtm desc ";
                    dt_percent_valva = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    if (dt_percent_valva.Rows.Count > 0) percent_valve = Convert.ToInt32(dt_percent_valva.Rows[0]["percent_valve"]);
                }
                String Html = string.Empty;
                Html += "<div class=\"list-group-item\"> ";
                Html += "   <div class=\"row\">";
                Html += "       <div class=\"col-lg-1\" style=\"margin-top:10px\">";
                Html += "            Valve " + unit_percent + ": ";
                Html += "       </div>";
                Html += "       <div class=\"col-lg-10\" style=\"margin-top:10px\">";
                Html += "       <input id=\"txtvalve\" type=\"text\" value=\"" + percent_valve + "\"  class=\"form-control\" style=\"width: 100px\" onkeypress=\"return isNumberKey(event)\" maxlength=\"3\"> ";
                Html += "       </div> ";
                Html += "   </div> ";
                Html += "</div> ";
                Html += "<br><button type=\"button\" class=\"btn btn-primary btn-flat col-md-2\" data-toggle=\"modal\" href=\"#aboutModal_save\" onclick=\"Popup(2,'manual')\" ><i class=\"fa fa-floppy-o\"></i>" + Cs_Controlcenter.BtnSave() + "</button>";
                #endregion

                #region _Automatic
                DataTable dt_cmdbvhead = new DataTable(); DataTable dt_cmdbvdetail = new DataTable();
                try
                {
                    strSQL = string.Empty;
                    strSQL = @"select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc";
                    dt_cmdbvhead = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    strSQL = string.Empty;
                    strSQL = @" select * from tb_ctr_cmdbvdetail where cmdbvhead_id 
                                in(select top 1 cmdbvhead_id from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc) order by order_time asc";
                    dt_cmdbvdetail = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

                int total = 1;

                total = dt_cmdbvdetail.Rows.Count;
                if (total == 0) { total = 1; }

                String Html_2 = string.Empty;
                var selected_dv_0 = "";
                var selected_dv_1 = "";
                var selected_dv_2 = "";
                var selected_dv_3 = "";
                var disablePressure = "";
                var disableFlow = "";
                var disableValve = "";
                var IndexNo = 0;
                var isDevice = true;
                var isdisabled = "";
                var tblAutomatic = "tblAutomatic";
                string timevalue = "";
                string cmdbvheadfailure_mode = string.Empty;
                string cmdbvheadstep_control_delay = string.Empty;
                string cmdbvheadtime_loop = string.Empty;
                string cmdbvheadlimit_min = string.Empty;
                string cmdbvheaddeadband_pressure = string.Empty;
                string cmdbvheaddeadband_flow = string.Empty;
                DataTable dt_timeList = inl.TimerList();

                if (dt_cmdbvhead.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_cmdbvhead.Rows)
                    {
                        cmdbvheadfailure_mode = row["failure_mode"].ToString();
                        cmdbvheadstep_control_delay = row["step_control_delay"].ToString();
                        cmdbvheadtime_loop = row["time_loop"].ToString();
                        cmdbvheadlimit_min = row["limit_min"].ToString();
                        cmdbvheaddeadband_pressure = row["deadband_pressure"].ToString();
                        cmdbvheaddeadband_flow = row["deadband_flow"].ToString();
                    }
                }

                Html_2 += "<div class=\"table-responsive\">";
                Html_2 += "<table class=\"table table-bordered table-striped table-hover table-condensed\" id=\"tblSteppingAutomatic\" name=\"tblSteppingAutomatic\" width=\"100%\">";
                Html_2 += "     <thead>";
                Html_2 += "         <tr>";
                Html_2 += "             <th>No.</th>";
                Html_2 += "             <th>Device Mode</th>";
                Html_2 += "             <th>Time</th>";
                Html_2 += "             <th>Pressure</th>";
                Html_2 += "             <th>Flow</th>";
                //Html_2 += "             <th>Valve</th>";
                Html_2 += "         </tr>";
                Html_2 += "     </thead>";
                Html_2 += "     <tbody>";
                if (dt_cmdbvdetail.Rows.Count > 0)
                {

                    IndexNo = 0;
                    foreach (DataRow row in dt_cmdbvdetail.Rows)
                    {
                        IndexNo += 1;
                        isdisabled = "";
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        //var txtValve = "txtValve" + @IndexNo;
                        int failure_mode = Convert.ToInt32(row["failure_mode"]);

                        isDevice = false;
                        if (IndexNo == 1) { isDevice = true; isdisabled = "disabled"; }


                        string txtPressure_bgc = cs.GetBg_Color("p", failure_mode);
                        string txtFlow_bgc = cs.GetBg_Color("f", failure_mode);
                        //string txtValve_bgc = cs.GetBg_Color("v", failure_mode);

                        selected_dv_0 = "";
                        selected_dv_1 = "";
                        selected_dv_2 = "";
                        selected_dv_3 = "";

                        if (failure_mode == 0)
                            selected_dv_0 = "selected";

                        if (failure_mode == 1)
                            selected_dv_1 = "selected";

                        if (failure_mode == 2)
                            selected_dv_2 = "selected";

                        if (failure_mode == 3)
                            selected_dv_3 = "selected";

                        disablePressure = "disabled";
                        disableFlow = "disabled";
                        disableValve = "disabled";

                        if (failure_mode == 1)
                            disablePressure = "";
                        if (failure_mode == 2)
                            disableFlow = "";
                        if (failure_mode == 3)
                            disableValve = "";



                        Html_2 += "<tr>";
                        Html_2 += "      <td align=\"center\">" + @IndexNo + "</td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "             <option value=\"0\"  " + selected_dv_0 + " " + isdisabled + ">Disable</option>";
                        Html_2 += "             <option value=\"1\" " + selected_dv_1 + ">Pressure</option>";
                        Html_2 += "             <option value=\"2\"  " + selected_dv_2 + ">Flow</option>";
                        //Html_2 += "             <option value=\"3\"  " + selected_dv_3 + ">Valve</option>";
                        Html_2 += "         </select>";
                        Html_2 += "      </td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" " + isdisabled + ">";

                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = row["time_start"].ToString();
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }

                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += " <td> <input  value=\"" + row["pressure_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%; background-color:" + txtPressure_bgc + "\" maxlength=\"8\"> </td>";
                        Html_2 += " <td> <input  value=\"" + row["flow_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:" + txtFlow_bgc + "\" maxlength=\"8\" ></td>";
                        //Html_2 += " <td> <input  value=\"" + row["valve_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:" + txtValve_bgc + "\" maxlength=\"8\" > </td>";
                        Html_2 += "</tr>";
                    }
                }
                else {
                    isdisabled = "disabled";
                    for (int i = 0; i < template_counter; i++)
                    {
                        IndexNo += 1;
                        if (IndexNo == 2)
                        {
                            break;
                        }

                        var delete = "btnDelete" + @IndexNo;
                        var add = "btnAdd" + @IndexNo;
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        //var txtValve = "txtValve" + @IndexNo;
                        if (IndexNo == 2) { isDevice = false; isdisabled = ""; }


                        Html_2 += "<tr>";
                        Html_2 += "     <td align=\"center\">" + IndexNo + "</td>";
                        Html_2 += "     <td>";
                        Html_2 += "     <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "         <option value=\"0\" " + isdisabled + ">Disable</option>";
                        Html_2 += "         <option value=\"1\">Pressure</option>";
                        Html_2 += "         <option value=\"2\">Flow</option>";
                        //Html_2 += "         <option value=\"3\">Valve</option>";
                        Html_2 += "     </select>";
                        Html_2 += "     </td>";
                        Html_2 += "     <td>";
                        Html_2 += "   <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" disabled=\"true\">";
                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = "00:00:00";
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }
                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%;\" maxlength=\"8\" value=\"\"></td>";
                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        //Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        Html_2 += "</tr>";
                    }

                }

                Html_2 += "         </tbody>";
                Html_2 += "     </table>";
                Html_2 += " </div>";
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                Html_2 += "";

                #endregion

                #region _Realtime
                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime_bv\" class=\"table table-hover table-sm table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_3 += "             <thead>";
                Html_3 += "                    <tr>";
                Html_3 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_3 += "                         <th>Remote Name</th>";
                Html_3 += "                         <th>วันเวลา</th>";
                Html_3 += "                         <th>แรงดัน (ออก)(บาร์)</th>";
                Html_3 += "                         <th>แรงดัน (เข้า)(บาร์)</th>";
                Html_3 += "                         <th>Valve (%)</th>";
                Html_3 += "                         <th>อัตราการไหล (ลบ.ม.)</th>";
                Html_3 += "                     </tr>";
                Html_3 += "             </thead>";
                Html_3 += "         </table>";
                Html_3 += "     </div>";
                //Html_3 += "</div>";

                #endregion

                #region _History
                String Html_4 = string.Empty;

                //Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "    <div class=\"table-responsive Flipped\">";
                Html_4 += "     <div class=\"Content\">";
                Html_4 += "         <table id=\"dt_grid_history_bv\" class=\"table table-striped table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_4 += "             <thead>";
                Html_4 += "                    <tr>";
                Html_4 += "                         <th>ลำดับ</th>";
                Html_4 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_4 += "                         <th>วันที่ตั้งค่า</th>";
                Html_4 += "                         <th>รายละเอียด</th>";
                Html_4 += "                         <th>สั่งโดย</th>";
                //Html_4 += "                         <th>ไฟล์</th>";
                Html_4 += "                         <th>หมายเหตุ</th>";
                Html_4 += "                     </tr>";
                Html_4 += "             </thead>";
                Html_4 += "         </table>";
                Html_4 += "     </div>";
                Html_4 += "    </div>";
                //Html_4 += "</div>";

                #endregion

                string body_footer = string.Empty;
                using (StreamReader reader = new StreamReader(context.Server.MapPath("~/template/prvstepping_footer_1.htm")))
                {
                    body_footer = reader.ReadToEnd();
                }
                body_footer = body_footer.Replace("{step_control_delay}", cmdbvheadstep_control_delay);
                body_footer = body_footer.Replace("{time_loop}", cmdbvheadtime_loop);
                body_footer = body_footer.Replace("{limit_min}", cmdbvheadlimit_min);
                body_footer = body_footer.Replace("{deadband_pressure}", cmdbvheaddeadband_pressure);
                body_footer = body_footer.Replace("{deadband_flow}", cmdbvheaddeadband_flow);

                var keyValues = new Dictionary<string, string>
               {
                   { "_manual", Html },
                   { "_Automatic", Html_2 },
                   { "_Realtime", Html_3 },
                   { "_History", Html_4 },
                   { "_txtRow" , total.ToString()},
                   { "_bodyfooter" , body_footer },
                   { "_failure_mode" , cmdbvheadfailure_mode }
                   //{ "_step_control_delay" , cmdbvheadstep_control_delay },
                   //{ "_time_loop" , cmdbvheadtime_loop },
                   //{ "_limit_min" , cmdbvheadlimit_min },
                   //{ "_deadband_pressure" , cmdbvheaddeadband_pressure },
                   //{ "_deadband_flow" , cmdbvheaddeadband_flow }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getdata_PrvStepping_new(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                //set global variable _dvtype_id type of bv / prv 
                _dvtype_id = mainData["$_datatype"].ToString();
                String unit_percent = cs.unit_percent();
                #region _manual
                int percent_valve = 0;
                int template_counter = 0;
                Boolean manual_valva_control = false;
                DataTable dt_percent_valva = new DataTable();
                string strSQL = @"SELECT vt.wwcode, vt.dmacode,vt.remote_name,vt.dvtype_id,ISNULL(hbv.percent_valve,0) percent_valve , ISNULL(hbv.manual_valva_control,0) manual_valva_control, cy.counter_template
                        FROM tb_ctr_dmavalvetype vt
                        left join (select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '0' order by cmdbvhead_id desc) hbv on vt.dmacode = hbv.dmacode INNER JOIN tb_ctr_cycle_counter cy on cy.id = vt.cycle_id where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "' ";
                DataTable dt_ctr_bvmanual = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                if (dt_ctr_bvmanual.Rows.Count > 0)
                {
                    percent_valve = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["percent_valve"]);
                    template_counter = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["counter_template"]);
                    manual_valva_control = Convert.ToBoolean(dt_ctr_bvmanual.Rows[0]["manual_valva_control"]);
                }
                else {
                    strSQL = @" SELECT wwcode , dmacode ,remote_name, manual_valva_control  FROM tb_ctr_cmdbvhead WHERE wwcode = '" + wwcode + "' AND dmacode = '" + mainData["$_dmacode"].ToString() + "' AND percent_valve is not null ORDER BY cmd_data_dtm desc ";
                    dt_percent_valva = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    if (dt_percent_valva.Rows.Count > 0) manual_valva_control = Convert.ToBoolean(dt_percent_valva.Rows[0]["manual_valva_control"]);
                }
                string status_manual_valva = string.Empty;
                if (manual_valva_control) { status_manual_valva = "checked=\"checked\""; }
                String Html = string.Empty;
                Html += "<div class=\"list-group-item\"> ";
                Html += "   <div class=\"row\">";
                Html += "       <div class=\"col-lg-2\" style=\"margin-top:10px\">";
                Html += "          ควบคุม Stepping manual : ";
                Html += "       </div>";
                Html += "       <div class=\"form-group col-lg-10\" style=\"margin-top:10px\">";
                //Html += "       <input id=\"txtvalve\" type=\"text\" value=\"" + percent_valve + "\"  class=\"form-control\" style=\"width: 100px\" onkeypress=\"return isNumberKey(event)\" maxlength=\"3\"> ";
                Html += "           <label for=\"txtvalva_ct8\">เปิด / ปิด</label><br /> ";
                Html += "               <label class=\"switch\"> ";
                Html += "               <input type=\"checkbox\" id=\"txtvalva_ct8\" " + status_manual_valva + ">";
                Html += "               <span class=\"slider round\"></span> ";
                Html += "           </label> ";
                Html += "       </div> ";
                Html += "   </div> ";
                Html += "</div> ";
                Html += "<br><button type=\"button\" class=\"btn btn-primary btn-flat col-md-2\" data-toggle=\"modal\" href=\"#aboutModal_save\" onclick=\"Popup(2,'manual')\" ><i class=\"fa fa-floppy-o\"></i>" + Cs_Controlcenter.BtnSave() + "</button>";
                #endregion

                #region _Automatic
                DataTable dt_cmdbvhead = new DataTable(); DataTable dt_cmdbvdetail = new DataTable();
                try
                {
                    strSQL = string.Empty;
                    strSQL = @"select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc";
                    dt_cmdbvhead = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    strSQL = string.Empty;
                    strSQL = @" select * from tb_ctr_cmdbvdetail where cmdbvhead_id 
                                in(select top 1 cmdbvhead_id from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdbvhead_id desc) order by order_time asc";
                    dt_cmdbvdetail = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

                int total = 1;

                total = dt_cmdbvdetail.Rows.Count;
                if (total == 0) { total = 1; }

                String Html_2 = string.Empty;
                var selected_dv_0 = "";
                var selected_dv_1 = "";
                var selected_dv_2 = "";
                var selected_dv_3 = "";
                var disablePressure = "";
                var disableFlow = "";
                var disableValve = "";
                var IndexNo = 0;
                var isDevice = true;
                var isdisabled = "";
                var tblAutomatic = "tblAutomatic";
                string timevalue = "";
                //string cmdbvheadfailure_mode = string.Empty;
                string cmdbvheadstep_control_delay = string.Empty;
                string cmdbvheadtime_loop = string.Empty;
                string cmdbvheadlimit_min = string.Empty;
                string cmdbvheadheadlost = string.Empty;
                string cmdbvheaddeadband_pressure = string.Empty;
                string cmdbvheaddeadband_flow = string.Empty;
                DataTable dt_timeList = inl.TimerList();

                if (dt_cmdbvhead.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_cmdbvhead.Rows)
                    {
                        //cmdbvheadfailure_mode = row["failure_mode"].ToString();
                        cmdbvheadstep_control_delay = row["step_control_delay"].ToString();
                        cmdbvheadtime_loop = row["time_loop"].ToString();
                        cmdbvheadlimit_min = row["limit_min"].ToString();
                        cmdbvheaddeadband_pressure = row["deadband_pressure"].ToString();
                        cmdbvheaddeadband_flow = row["deadband_flow"].ToString();
                        cmdbvheadheadlost = row["headlost"].ToString();
                    }
                }

                Html_2 += "<div class=\"table-responsive\">";
                Html_2 += "<table class=\"table table-bordered table-striped table-hover table-condensed\" id=\"tblSteppingAutomatic\" name=\"tblSteppingAutomatic\" width=\"100%\">";
                Html_2 += "     <thead>";
                Html_2 += "         <tr>";
                Html_2 += "             <th>No.</th>";
                Html_2 += "             <th>Device Mode</th>";
                Html_2 += "             <th>Time</th>";
                Html_2 += "             <th>Pressure</th>";
                Html_2 += "             <th>Flow</th>";
                //Html_2 += "             <th>Valve</th>";
                Html_2 += "         </tr>";
                Html_2 += "     </thead>";
                Html_2 += "     <tbody>";
                if (dt_cmdbvdetail.Rows.Count > 0)
                {

                    IndexNo = 0;
                    foreach (DataRow row in dt_cmdbvdetail.Rows)
                    {
                        IndexNo += 1;
                        isdisabled = "";
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        //var txtValve = "txtValve" + @IndexNo;
                        int failure_mode = Convert.ToInt32(row["failure_mode"]);

                        isDevice = false;
                        if (IndexNo == 1) { isDevice = true; isdisabled = "disabled"; }


                        string txtPressure_bgc = cs.GetBg_Color("p", failure_mode);
                        string txtFlow_bgc = cs.GetBg_Color("f", failure_mode);
                        //string txtValve_bgc = cs.GetBg_Color("v", failure_mode);

                        selected_dv_0 = "";
                        selected_dv_1 = "";
                        selected_dv_2 = "";
                        selected_dv_3 = "";

                        if (failure_mode == 0)
                            selected_dv_0 = "selected";

                        if (failure_mode == 1)
                            selected_dv_1 = "selected";

                        if (failure_mode == 2)
                            selected_dv_2 = "selected";

                        if (failure_mode == 3)
                            selected_dv_3 = "selected";

                        disablePressure = "disabled";
                        disableFlow = "disabled";
                        disableValve = "disabled";

                        if (failure_mode == 1)
                            disablePressure = "";
                        if (failure_mode == 2)
                            disableFlow = "";
                        if (failure_mode == 3)
                            disableValve = "";



                        Html_2 += "<tr>";
                        Html_2 += "      <td align=\"center\">" + @IndexNo + "</td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "             <option value=\"0\"  " + selected_dv_0 + " " + isdisabled + ">Disable</option>";
                        Html_2 += "             <option value=\"1\" " + selected_dv_1 + ">Pressure</option>";
                        Html_2 += "             <option value=\"2\"  " + selected_dv_2 + ">Flow</option>";
                        //Html_2 += "             <option value=\"3\"  " + selected_dv_3 + ">Valve</option>";
                        Html_2 += "         </select>";
                        Html_2 += "      </td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" " + isdisabled + ">";

                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = row["time_start"].ToString();
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }

                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += " <td> <input  value=\"" + row["pressure_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%; background-color:" + txtPressure_bgc + "\" maxlength=\"8\"> </td>";
                        Html_2 += " <td> <input  value=\"" + row["flow_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:" + txtFlow_bgc + "\" maxlength=\"8\" ></td>";
                        //Html_2 += " <td> <input  value=\"" + row["valve_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:" + txtValve_bgc + "\" maxlength=\"8\" > </td>";
                        Html_2 += "</tr>";
                    }
                }
                else {
                    isdisabled = "disabled";
                    for (int i = 0; i < template_counter; i++)
                    {
                        IndexNo += 1;
                        if (IndexNo == 2)
                        {
                            break;
                        }

                        var delete = "btnDelete" + @IndexNo;
                        var add = "btnAdd" + @IndexNo;
                        var selmode = "selmode" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txtPressure = "txtPressure" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        //var txtValve = "txtValve" + @IndexNo;
                        if (IndexNo == 2) { isDevice = false; isdisabled = ""; }


                        Html_2 += "<tr>";
                        Html_2 += "     <td align=\"center\">" + IndexNo + "</td>";
                        Html_2 += "     <td>";
                        Html_2 += "     <select id=\"" + selmode + "\" name=\"" + selmode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        //Html_2 += "         <option value=\"0\" " + isdisabled + ">Disable</option>";
                        Html_2 += "         <option value=\"1\">Pressure</option>";
                        Html_2 += "         <option value=\"2\">Flow</option>";
                        //Html_2 += "         <option value=\"3\">Valve</option>";
                        Html_2 += "     </select>";
                        Html_2 += "     </td>";
                        Html_2 += "     <td>";
                        Html_2 += "   <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" disabled=\"true\">";
                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = "00:00:00";
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }
                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtPressure + "\" name=\"" + txtPressure + "\" style=\"width:90%;\" maxlength=\"8\" value=\"\"></td>";
                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        //Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        Html_2 += "</tr>";
                    }

                }

                Html_2 += "         </tbody>";
                Html_2 += "     </table>";
                Html_2 += " </div>";
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                Html_2 += "";

                #endregion

                #region _Realtime
                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime_bv\" class=\"table table-hover table-sm table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_3 += "             <thead>";
                Html_3 += "                    <tr>";
                Html_3 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_3 += "                         <th>Remote Name</th>";
                Html_3 += "                         <th>วันเวลา</th>";
                Html_3 += "                         <th>แรงดัน (ออก)(บาร์)</th>";
                Html_3 += "                         <th>แรงดัน (เข้า)(บาร์)</th>";
                Html_3 += "                         <th>Valve (%)</th>";
                Html_3 += "                         <th>อัตราการไหล (ลบ.ม.)</th>";
                Html_3 += "                     </tr>";
                Html_3 += "             </thead>";
                Html_3 += "         </table>";
                Html_3 += "     </div>";
                //Html_3 += "</div>";

                #endregion

                #region _History
                String Html_4 = string.Empty;

                //Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "    <div class=\"table-responsive Flipped\">";
                Html_4 += "     <div class=\"Content\">";
                Html_4 += "         <table id=\"dt_grid_history_bv\" class=\"table table-striped table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_4 += "             <thead>";
                Html_4 += "                    <tr>";
                Html_4 += "                         <th>ลำดับ</th>";
                Html_4 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_4 += "                         <th>วันที่ตั้งค่า</th>";
                Html_4 += "                         <th>รายละเอียด</th>";
                Html_4 += "                         <th>สั่งโดย</th>";
                //Html_4 += "                         <th>ไฟล์</th>";
                Html_4 += "                         <th>หมายเหตุ</th>";
                Html_4 += "                     </tr>";
                Html_4 += "             </thead>";
                Html_4 += "         </table>";
                Html_4 += "     </div>";
                Html_4 += "    </div>";
                //Html_4 += "</div>";

                #endregion


                string body_footer = string.Empty;
                using (StreamReader reader = new StreamReader(context.Server.MapPath("~/template/prvstepping_footer_2.htm")))
                {
                    body_footer = reader.ReadToEnd();
                }
                body_footer = body_footer.Replace("{step_control_delay}", cmdbvheadstep_control_delay);
                body_footer = body_footer.Replace("{time_loop}", cmdbvheadtime_loop);
                body_footer = body_footer.Replace("{limit_min}", cmdbvheadlimit_min);
                body_footer = body_footer.Replace("{deadband_pressure}", cmdbvheaddeadband_pressure);
                body_footer = body_footer.Replace("{deadband_flow}", cmdbvheaddeadband_flow);
                body_footer = body_footer.Replace("{headlost}", cmdbvheadheadlost);



                var keyValues = new Dictionary<string, string>
               {
                   { "_manual", Html },
                   { "_Automatic", Html_2 },
                   { "_Realtime", Html_3 },
                   { "_History", Html_4 },
                   { "_txtRow" , total.ToString()},
                   { "_bodyfooter" , body_footer }
                   //{ "_failure_mode" , cmdbvheadfailure_mode }
                   //{ "_step_control_delay" , cmdbvheadstep_control_delay },
                   //{ "_time_loop" , cmdbvheadtime_loop },
                   //{ "_limit_min" , cmdbvheadlimit_min },
                   //{ "_deadband_pressure" , cmdbvheaddeadband_pressure },
                   //{ "_deadband_flow" , cmdbvheaddeadband_flow }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Getdata_Afv(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                //set global variable _dvtype_id type of bv / prv 
                _dvtype_id = mainData["$_datatype"].ToString();
                String unit_percent = cs.unit_percent();

                #region _manual
                int timeout_min = 0;
                int template_counter = 0;
                Boolean manual_valva_control = false;
                DataTable dt_timeoutmin = new DataTable();
                string strSQL = @"SELECT vt.wwcode, vt.dmacode,vt.remote_name,vt.dvtype_id,ISNULL(hbv.timeout_min,0) timeoutmin , ISNULL(hbv.manual_valva_control,0) manual_valva_control, cy.counter_template
                        FROM tb_ctr_dmavalvetype vt
                        left join (select top 1 * from tb_ctr_cmdafvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '0' order by cmdafvhead_id desc) hbv on vt.dmacode = hbv.dmacode INNER JOIN tb_ctr_cycle_counter cy on cy.id = vt.cycle_id where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "' ";
                DataTable dt_ctr_afvmanual = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                if (dt_ctr_afvmanual.Rows.Count > 0)
                {
                    timeout_min = Convert.ToInt32(dt_ctr_afvmanual.Rows[0]["timeoutmin"]);
                    template_counter = Convert.ToInt32(dt_ctr_afvmanual.Rows[0]["counter_template"]);
                    manual_valva_control = Convert.ToBoolean(dt_ctr_afvmanual.Rows[0]["manual_valva_control"]);
                }
                else {
                    strSQL = @" SELECT wwcode , dmacode ,remote_name, manual_valva_control , timeout_min  FROM tb_ctr_cmdafvhead WHERE wwcode = '" + wwcode + "' AND dmacode = '" + mainData["$_dmacode"].ToString() + "' AND timeout_min is not null AND manual_valva_control is not null ORDER BY cmd_data_dtm desc ";
                    dt_timeoutmin = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    if (dt_timeoutmin.Rows.Count > 0) manual_valva_control = Convert.ToBoolean(dt_timeoutmin.Rows[0]["manual_valva_control"]); timeout_min = Convert.ToInt32(dt_timeoutmin.Rows[0]["timeoutmin"]);
                }
                string status_manual_valva = string.Empty;
                string status_div_control = string.Empty;
                if (manual_valva_control) { status_manual_valva = "checked=\"checked\""; } else { status_div_control = "display: none;"; }
                String Html = string.Empty;

                string body_manual = string.Empty;
                using (StreamReader reader = new StreamReader(context.Server.MapPath("~/template/afv_manual.htm")))
                {
                    body_manual = reader.ReadToEnd();
                }
                body_manual = body_manual.Replace("{displaynone}", status_div_control);
                body_manual = body_manual.Replace("{checked_chk}", status_manual_valva);
                body_manual = body_manual.Replace("{txtafv_timeoutmin}", timeout_min.ToString());
                body_manual = body_manual.Replace("{btnsave}", Cs_Controlcenter.BtnSave());
                Html += body_manual;

                #endregion

                #region _Automatic
                DataTable dt_cmdafvhead = new DataTable(); DataTable dt_cmdafvdetail = new DataTable();
                try
                {
                    strSQL = string.Empty;
                    strSQL = @"select top 1 * from tb_ctr_cmdafvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdafvhead_id desc";
                    dt_cmdafvhead = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                    strSQL = string.Empty;
                    strSQL = @" select * from tb_ctr_cmdafvdetail where cmdafvhead_id 
                                in(select top 1 cmdafvhead_id from tb_ctr_cmdafvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '1' order by cmdafvhead_id desc) order by order_time asc";
                    dt_cmdafvdetail = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

                int total = 1;
                total = dt_cmdafvdetail.Rows.Count;
                if (total == 0) { total = 1; }

                String Html_2 = string.Empty;
                var selected_dv_0 = "";
                var selected_dv_1 = "";
                var selected_dv_2 = "";
                var IndexNo = 0;
                //var isDevice = true;
                var isdisabled = "";
                //var tblAutomatic = "tblAutomatic";
                //string timevalue = "";
                ////string cmdbvheadfailure_mode = string.Empty;
                string cmdafvheadpipe_size = string.Empty;
                string cmdafvheadtimeout_min = string.Empty;

                //string cmdbvheadlimit_min = string.Empty;
                //string cmdbvheadheadlost = string.Empty;
                //string cmdbvheaddeadband_pressure = string.Empty;
                //string cmdbvheaddeadband_flow = string.Empty;
                DataTable dt_timeList = inl.TimerList();

                if (dt_cmdafvhead.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_cmdafvhead.Rows)
                    {
                        cmdafvheadpipe_size = row["pipe_size"].ToString();
                        cmdafvheadtimeout_min = row["timeout_min"].ToString();
                    }
                }

                Html_2 += "<div class=\"table-responsive\">";
                Html_2 += "<table class=\"table table-bordered table-striped table-hover table-condensed\" id=\"tblAfvAutomatic\" name=\"tblAfvAutomatic\" width=\"100%\">";
                Html_2 += "     <thead>";
                Html_2 += "         <tr>";
                Html_2 += "             <th>No.</th>";
                Html_2 += "             <th>Date</th>";
                Html_2 += "             <th>Time</th>";
                Html_2 += "             <th>Mode</th>";
                Html_2 += "             <th>Minutes</th>";
                Html_2 += "             <th>Flow</th>";
                Html_2 += "         </tr>";
                Html_2 += "     </thead>";
                Html_2 += "     <tbody>";
                if (dt_cmdafvdetail.Rows.Count > 0)
                {

                    IndexNo = 0;
                    foreach (DataRow row in dt_cmdafvdetail.Rows)
                    {
                        IndexNo += 1;
                        //isdisabled = "";
                        var Mode = "txtMode" + @IndexNo;
                        var txtDate = "txtdate" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txttimer = "txttimer" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;
                        int failure_mode = Convert.ToInt32(row["failure_mode"]);

                        //        isDevice = false;
                        //        if (IndexNo == 1) { isDevice = true; isdisabled = "disabled"; }


                        string txttimer_bgc = cs.GetBg_Color("p", failure_mode);
                        string txtFlow_bgc = cs.GetBg_Color("f", failure_mode);

                        selected_dv_1 = "";
                        selected_dv_2 = "";

                        if (failure_mode == 1)
                            selected_dv_1 = "selected";

                        if (failure_mode == 2)
                            selected_dv_2 = "selected";

                        //        if (failure_mode == 3)
                        //            selected_dv_3 = "selected";



                        Html_2 += "<tr>";
                        Html_2 += "      <td align=\"center\">" + @IndexNo + "</td>";

                        Html_2 += "     <td>";
                        Html_2 += "         <select id=\"" + txtDate + "\" name=\"" + txtDate + "\" class=\"form-control\">";
                        for (int i = 1; i <= 31; i++)
                        {
                            if ((Int32)row["date_worker"] == i)
                            {
                                Html_2 += "             <option value=\"" + i + "\" selected>" + i + "</option>";
                            }
                            else {
                                Html_2 += "             <option value=\"" + i + "\">" + i + "</option>";
                            }
                        }
                        Html_2 += "         </select>";
                        Html_2 += "     </td>";
                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\" >";

                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = row["time_start"].ToString();
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";

                            }
                        }

                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + Mode + "\" name=\"" + Mode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        Html_2 += "             <option value=\"1\" " + selected_dv_1 + ">Timer</option>";
                        Html_2 += "             <option value=\"2\"  " + selected_dv_2 + ">Flow</option>";
                        Html_2 += "         </select>";
                        Html_2 += "      </td>";

                        Html_2 += " <td> <input  value=\"" + row["time_worker"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txttimer + "\" name=\"" + txttimer + "\" style=\"width:90%; background-color:" + txttimer_bgc + "\" maxlength=\"8\"> </td>";
                        Html_2 += " <td> <input  value=\"" + row["flow_worker"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:" + txtFlow_bgc + "\" maxlength=\"8\" ></td>";
                        //        //Html_2 += " <td> <input  value=\"" + row["valve_value"] + "\" class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:" + txtValve_bgc + "\" maxlength=\"8\" > </td>";
                        Html_2 += "</tr>";
                    }
                }
                else {
                    //    isdisabled = "disabled";
                    for (int i = 0; i < template_counter; i++)
                    {
                        IndexNo += 1;
                        if (IndexNo == 2)
                        {
                            break;
                        }

                        var Mode = "txtMode" + @IndexNo;
                        var txtDate = "txtdate" + @IndexNo;
                        var txttime = "txttime" + @IndexNo;
                        var txttimer = "txttimer" + @IndexNo;
                        var txtFlow = "txtFlow" + @IndexNo;

                        //        var delete = "btnDelete" + @IndexNo;
                        //        var add = "btnAdd" + @IndexNo;
                        //        var selmode = "selmode" + @IndexNo;
                        //        var txttime = "txttime" + @IndexNo;
                        //        var txtPressure = "txtPressure" + @IndexNo;
                        //        var txtFlow = "txtFlow" + @IndexNo;
                        //        //var txtValve = "txtValve" + @IndexNo;

                        //        if (IndexNo == 2) { isDevice = false; isdisabled = ""; }


                        Html_2 += "<tr>";
                        Html_2 += "     <td align=\"center\">" + IndexNo + "</td>";
                        Html_2 += "     <td>";
                        Html_2 += "         <select id=\"" + txtDate + "\" name=\"" + txtDate + "\" class=\"form-control\">";
                        for (int j = 1; j <= 31; j++)
                        {
                            Html_2 += "             <option value=\"" + j + "\">" + j + "</option>";
                        }
                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += "     <td>";
                        Html_2 += "   <select id=\"" + txttime + "\" name=\"" + txttime + "\" class=\"form-control\">";
                        foreach (DataRow time in dt_timeList.Rows)
                        {
                            string time_start = "00:00:00";
                            TimeSpan ts = TimeSpan.Parse(time_start);
                            if (time["time_objid"].ToString() == ts.ToString(@"hh\:mm"))
                            {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\" selected>" + time["time_label_long"] + "</option>";
                            }
                            else {
                                Html_2 += "<option value=\"" + time["time_objid"] + "\">" + time["time_label_long"] + "</option>";
                            }
                        }
                        Html_2 += "         </select>";
                        Html_2 += "     </td>";

                        Html_2 += "      <td>";
                        Html_2 += "         <select id=\"" + Mode + "\" name=\"" + Mode + "\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        Html_2 += "             <option value=\"1\" selected>Timer</option>";
                        Html_2 += "             <option value=\"2\">Flow</option>";
                        Html_2 += "         </select>";
                        Html_2 += "      </td>";


                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txttimer + "\" name=\"" + txttimer + "\" style=\"width:90%;\" maxlength=\"8\" value=\"\"></td>";
                        Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtFlow + "\" name=\"" + txtFlow + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        //        //Html_2 += "<td> <input class=\"form-control\" onkeypress=\"return isNumberKey(event)\" type=\"text\" id=\"" + txtValve + "\" name=\"" + txtValve + "\" style=\"width:90%; background-color:#CCCCCC\" maxlength=\"8\" ></td>";
                        Html_2 += "</tr>";
                    }

                }

                Html_2 += "         </tbody>";
                Html_2 += "     </table>";
                Html_2 += " </div>";
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                //Html_2 += "";

                #endregion

                #region _Realtime
                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime_afv\" class=\"table table-hover table-sm table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_3 += "             <thead>";
                Html_3 += "                    <tr>";
                Html_3 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_3 += "                         <th>Remote Name</th>";
                Html_3 += "                         <th>วันเวลา</th>";
                Html_3 += "                         <th>แรงดัน (ออก)(บาร์)</th>";
                Html_3 += "                         <th>แรงดัน (เข้า)(บาร์)</th>";
                Html_3 += "                         <th>Valve (%)</th>";
                Html_3 += "                         <th>อัตราการไหล (ลบ.ม.)</th>";
                Html_3 += "                     </tr>";
                Html_3 += "             </thead>";
                Html_3 += "         </table>";
                Html_3 += "     </div>";
                //Html_3 += "</div>";

                #endregion

                #region _History
                String Html_4 = string.Empty;

                //Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "    <div class=\"table-responsive Flipped\">";
                Html_4 += "     <div class=\"Content\">";
                Html_4 += "         <table id=\"dt_grid_history_afv\" class=\"table table-striped table-bordered dt-responsive clear-center nowrap\" cellspacing=\"0\" style=\"width: 100%\">";
                Html_4 += "             <thead>";
                Html_4 += "                    <tr>";
                Html_4 += "                         <th>ลำดับ</th>";
                Html_4 += "                         <th>พื้นที่เฝ้าระวัง</th>";
                Html_4 += "                         <th>วันที่ตั้งค่า</th>";
                Html_4 += "                         <th>รายละเอียด</th>";
                Html_4 += "                         <th>สั่งโดย</th>";
                //Html_4 += "                         <th>ไฟล์</th>";
                Html_4 += "                         <th>หมายเหตุ</th>";
                Html_4 += "                     </tr>";
                Html_4 += "             </thead>";
                Html_4 += "         </table>";
                Html_4 += "     </div>";
                Html_4 += "    </div>";
                //Html_4 += "</div>";

                #endregion


                string body_footer = string.Empty;
                using (StreamReader reader = new StreamReader(context.Server.MapPath("~/template/afv_footer.htm")))
                {
                    body_footer = reader.ReadToEnd();
                }
                body_footer = body_footer.Replace("{pipe_size}", cmdafvheadpipe_size);
                body_footer = body_footer.Replace("{time_out_min_afv}", cmdafvheadtimeout_min);


                var keyValues = new Dictionary<string, string>
               {
                   { "_manual", Html },
                   { "_Automatic", Html_2 },
                   { "_Realtime", Html_3 },
                   { "_History", Html_4 },
                   { "_txtRow" , total.ToString()},
                   { "_bodyfooter" , body_footer }
                   //{ "_failure_mode" , cmdbvheadfailure_mode }
                   //{ "_step_control_delay" , cmdbvheadstep_control_delay },
                   //{ "_time_loop" , cmdbvheadtime_loop },
                   //{ "_limit_min" , cmdbvheadlimit_min },
                   //{ "_deadband_pressure" , cmdbvheaddeadband_pressure },
                   //{ "_deadband_flow" , cmdbvheaddeadband_flow }
               };
                return JsonConvert.SerializeObject(keyValues);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string Get_Automatic_prv(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });

        }

        [System.Web.Services.WebMethod]
        public static string SetDataModal(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                _wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);
                _dmacode = mainData["$_dmacode"].ToString();
                _remote_name = mainData["$_remote_name"].ToString();

                return null;
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetCommandTimeOut(String remote_name)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                List<SelectItem> resultList = new List<SelectItem>();
                string error = "";
                try
                {
                    DataTable dt = inl.GetDatabySQL(@"SELECT convert(varchar,180-DATEDIFF(second,[DataDateTime], SYSDATETIME())) as value
                              ,'' as label
                          FROM [RTU].[dbo].[commandqueue]
                          where RemoteName='" + remote_name + @"' and (statusName = 'work=NO' or statusName is null or statusName='')
                          and DATEDIFF(second,[DataDateTime], SYSDATETIME()) < 180", user.UserCons_PortalDB);

                    foreach (DataRow item in dt.Rows)
                    {
                        SelectItem selectItem = new SelectItem();
                        selectItem.Check = false;
                        if (Convert.ToInt32(item["VALUE"]) > 180)
                            item["VALUE"] = "180";
                        selectItem.Value = item["VALUE"].ToString();
                        selectItem.Label = "";
                        resultList.Add(selectItem);
                    }

                    return JsonConvert.SerializeObject(resultList);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetDataCommandqueue(String remote_name)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                try
                {
                    DataTable dt = inl.GetDatabySQL(@"SELECT StatusName from [RTU].[dbo].[commandqueue] WHERE RemoteName = '" + remote_name + @"'", user.UserCons_PortalDB);
                    return JsonConvert.SerializeObject(dt);
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddManualPrvt(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);

                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);

                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        #region sql insert tb_ctr_cmdprvthead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdprvthead (wwcode,dmacode,cmd_data_dtm,remote_name ";
                        strSQL += " ,control_mode ";
                        strSQL += " ,pilot_no ";
                        strSQL += " ,pilot_pressure ";
                        strSQL += " ,remark ";
                        strSQL += " ,record_status ";
                        strSQL += " ,create_user ";
                        strSQL += " ,create_dtm ";
                        strSQL += " ,last_upd_user ";
                        strSQL += " ,last_upd_dtm ";
                        strSQL += " ) output INSERTED.cmdprvthead_id ";
                        strSQL += " VALUES ( ";
                        //strSQL += " '" + newCmdprvhead_id + "', ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += "  GETDATE(),";
                        strSQL += " '" + remote_name + "', ";
                        strSQL += " '0', ";
                        strSQL += " " + Convert.ToInt32(mainData["solenoid"]) + ", ";
                        strSQL += " 0, ";
                        strSQL += " N'" + mainData["remark"].ToString() + "', ";
                        strSQL += "  'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += "  GETDATE(), ";
                        strSQL += "  null,";
                        strSQL += "  null";
                        strSQL += " ) ";
                        #endregion

                        int cmdprvthead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdprvthead_id != 0)
                        {
                            #region sql insert tb_ctr_cmdlog
                            string cmd_desc = "Mode=3," + remote_name + "," + mainData["solenoid"] + ",";
                            strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                            strSQL += " wwcode, ";
                            strSQL += " dmacode, ";
                            strSQL += " cmd_dtm, ";
                            strSQL += " cmd_dvtypeid, ";
                            strSQL += " cmd_headid, ";
                            strSQL += " cmd_desc, ";
                            strSQL += " record_status, ";
                            strSQL += " create_user, ";
                            strSQL += " create_dtm ";
                            strSQL += " ) VALUES ";
                            strSQL += " ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += " GETDATE(), ";
                            strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                            strSQL += " " + cmdprvthead_id + ", ";
                            strSQL += " '" + cmd_desc + "', ";
                            strSQL += " 'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += " GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                            if (status)
                            {
                                return JsonConvert.SerializeObject(new { dmacode = dmacode });
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:Addprvdetailfail" });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "Error:Addprvheadfail" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }


            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddAutoPrvt(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();
                string error = "";

                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        #region tb_ctr_cmdprvthead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdprvthead (wwcode,dmacode,cmd_data_dtm,remote_name ";
                        strSQL += " ,control_mode ";
                        strSQL += " ,pilot_no ";
                        strSQL += " ,pilot_pressure ";
                        strSQL += " ,remark ";
                        strSQL += " ,record_status ";
                        strSQL += " ,create_user ";
                        strSQL += " ,create_dtm ";
                        strSQL += " ,last_upd_user ";
                        strSQL += " ,last_upd_dtm ";
                        strSQL += " ) output INSERTED.cmdprvthead_id ";
                        strSQL += " VALUES ( ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += "  GETDATE(),";
                        strSQL += " '" + remote_name + "', ";
                        strSQL += " '1', ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " N'" + mainData["remark"].ToString() + "', ";
                        strSQL += "  'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += "  GETDATE(), ";
                        strSQL += "  null,";
                        strSQL += "  null";
                        strSQL += " ) ";

                        #endregion

                        int cmdprvthead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdprvthead_id != 0)
                        {
                            string cmd_desc = "Sync=1," + remote_name + ","
                               + "0,"
                               + "4,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,";

                            DataTable dt_arrtime = (DataTable)mainData["cmdprvtdetail"];

                            int index = 1;
                            Hashtable arrTime = new Hashtable();
                            arrTime = cs.GetTimPrvtdetaile(dt_arrtime);
                            if (arrTime.Count > 0)
                            {
                                //DateTime create_dtm = DateTime.Now;
                                foreach (DataRow row in dt_arrtime.Rows)
                                {
                                    object[] Time = new object[2];
                                    Time = (object[])arrTime[index];
                                    string pressure_value = "0.00";
                                    string flow_value = "0.00";
                                    string valve_value = "0";
                                    TimeSpan time_start = TimeSpan.Parse(Time[0].ToString());
                                    TimeSpan time_end = TimeSpan.Parse(Time[1].ToString());

                                    cmd_desc += row["failure_mode"].ToString() + "/" +
                                        time_start.ToString(@"hhmm") + "/" +
                                        time_end.ToString(@"hhmm") + "/" +
                                        pressure_value + "/" +
                                        flow_value + "/" +
                                        valve_value + "/" +
                                        row["pilot_no"].ToString() + ",";

                                    if (row.IsNull("pilot_no") || row["pilot_no"] == "")
                                        row["pilot_no"] = "null";

                                    #region sql insert to tb_ctr_cmdprvtdetail
                                    strSQL = string.Empty;
                                    strSQL += " INSERT INTO tb_ctr_cmdprvtdetail ( ";
                                    strSQL += " cmdprvthead_id, ";
                                    strSQL += " dmacode, ";
                                    strSQL += " cmd_data_dtm, ";
                                    strSQL += " order_time, ";
                                    strSQL += " failure_mode, ";
                                    strSQL += " time_end, ";
                                    strSQL += " time_start, ";
                                    strSQL += " pilot_no, ";
                                    strSQL += " pilot_pressure, ";
                                    strSQL += " record_status, ";
                                    strSQL += " create_user, ";
                                    strSQL += " create_dtm, ";
                                    strSQL += " last_upd_user, ";
                                    strSQL += " last_upd_dtm ";
                                    strSQL += " ) VALUES  ";
                                    strSQL += " ( ";
                                    strSQL += " '" + cmdprvthead_id + "', "; //cmdprvthead_id
                                    strSQL += " '" + dmacode + "', ";
                                    strSQL += " GETDATE(), ";
                                    strSQL += " " + Convert.ToInt32(row["order_time"]) + ", ";
                                    strSQL += " " + Convert.ToInt32(row["failure_mode"]) + ", ";
                                    strSQL += " '" + time_end + "', ";
                                    strSQL += " '" + time_start + "', ";
                                    strSQL += " " + row["pilot_no"] + ", ";
                                    strSQL += " 0, ";
                                    strSQL += " 'N', ";
                                    strSQL += " '" + user.UserID + "', ";
                                    strSQL += " GETDATE(), ";
                                    strSQL += " null, ";
                                    strSQL += " null ";
                                    strSQL += " ) ";
                                    #endregion

                                    error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                    index += 1;
                                }

                                if (error == "")
                                {
                                    if (arrTime.Count != 6)
                                    {
                                        for (int i = 0; i < 6 - arrTime.Count; i++)
                                        {
                                            cmd_desc += "0" + "/" + "0000" + "/" + "0000" + "/0/0/0/0/,";
                                        }
                                    }

                                    #region tb_ctr_cmdlog

                                    strSQL = string.Empty;
                                    strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                                    strSQL += " wwcode, ";
                                    strSQL += " dmacode, ";
                                    strSQL += " cmd_dtm, ";
                                    strSQL += " cmd_dvtypeid, ";
                                    strSQL += " cmd_headid, ";
                                    strSQL += " cmd_desc, ";
                                    strSQL += " record_status, ";
                                    strSQL += " create_user, ";
                                    strSQL += " create_dtm ";
                                    strSQL += " ) VALUES ";
                                    strSQL += " ( ";
                                    strSQL += " '" + wwcode + "', ";
                                    strSQL += " '" + dmacode + "', ";
                                    strSQL += " GETDATE(), ";
                                    strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                                    strSQL += " " + cmdprvthead_id + ", ";
                                    strSQL += " '" + cmd_desc + "', ";
                                    strSQL += " 'N', ";
                                    strSQL += " '" + user.UserID + "', ";
                                    strSQL += " GETDATE() ";
                                    strSQL += " ) ";
                                    #endregion

                                    error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                    return JsonConvert.SerializeObject(new { dmacode = dmacode });
                                }
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:GetTimBvdetaile" });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "Error:AddBvhead" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

                return null;
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddManualBv(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);

                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        #region sql insert tb_ctr_cmdbvhead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,percent_valve,failure_mode, ";
                        strSQL += " step_control_delay,time_loop,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm, ";
                        strSQL += " last_upd_user,last_upd_dtm ";
                        strSQL += " ) output INSERTED.cmdbvhead_id ";
                        strSQL += " VALUES ( ";
                        //strSQL += " '" + newCmdprvhead_id + "', ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += "  GETDATE(),";
                        strSQL += " '" + remote_name + "', ";
                        strSQL += " '0', ";
                        strSQL += " " + Convert.ToInt32(mainData["valve"]) + ", ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " 0, ";
                        strSQL += " 0, ";
                        strSQL += " N'" + mainData["remark"].ToString() + "', ";
                        strSQL += "  'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += "  GETDATE(), ";
                        strSQL += "  null,";
                        strSQL += "  null";
                        strSQL += " ) ";
                        #endregion

                        int cmdbvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdbvhead_id != 0)
                        {
                            #region sql insert tb_ctr_cmdlog
                            string cmd_desc = "Mode=0," + remote_name + "," + mainData["valve"].ToString() + ",";
                            strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                            strSQL += " wwcode, ";
                            strSQL += " dmacode, ";
                            strSQL += " cmd_dtm, ";
                            strSQL += " cmd_dvtypeid, ";
                            strSQL += " cmd_headid, ";
                            strSQL += " cmd_desc, ";
                            strSQL += " record_status, ";
                            strSQL += " create_user, ";
                            strSQL += " create_dtm ";
                            strSQL += " ) VALUES ";
                            strSQL += " ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += " GETDATE(), ";
                            strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                            strSQL += " " + cmdbvhead_id + ", ";
                            strSQL += " '" + cmd_desc + "', ";
                            strSQL += " 'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += " GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                            if (status)
                            {
                                return JsonConvert.SerializeObject(new { dmacode = dmacode });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "fail" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddAutoBv(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();

                string error = "";

                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        DataTable dt_cmdbvhead = (DataTable)mainData["cmdbvhead"];
                        foreach (DataRow row in dt_cmdbvhead.Rows)
                        {
                            if (row.IsNull("failure_mode") || row["failure_mode"] == "")
                                row["failure_mode"] = 0;
                            if (row.IsNull("step_control_delay") || row["step_control_delay"] == "")
                                row["step_control_delay"] = 0;
                            if (row.IsNull("time_loop") || row["time_loop"] == "")
                                row["time_loop"] = 0;
                            if (row.IsNull("limit_min") || row["limit_min"] == "")
                                row["limit_min"] = 0;
                            if (row.IsNull("deadband_pressure") || row["deadband_pressure"] == "")
                                row["deadband_pressure"] = 0;
                            if (row.IsNull("deadband_flow") || row["deadband_flow"] == "")
                                row["deadband_flow"] = 0;

                            #region insert into tb_ctr_cmdbvhead
                            String strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,percent_valve,failure_mode, ";
                            strSQL += " step_control_delay,time_loop,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm, ";
                            strSQL += " last_upd_user,last_upd_dtm ";
                            strSQL += " ) output INSERTED.cmdbvhead_id ";
                            strSQL += " VALUES ( ";
                            //strSQL += " '" + newCmdprvhead_id + "', ";
                            strSQL += " '" + wwcode + "', "; //wwcode
                            strSQL += " '" + dmacode + "', "; //dmacode
                            strSQL += "  GETDATE(),"; //cmd_data_dtm
                            strSQL += " '" + remote_name + "', "; //remote_name
                            strSQL += " '1', "; //control_mode
                            strSQL += " null, "; //percent_valve
                            strSQL += " " + row["failure_mode"] + ", "; //failure_mode
                            strSQL += " " + row["step_control_delay"] + ", "; //step_control_delay
                            strSQL += " " + row["time_loop"] + ", "; //time_loop
                            strSQL += " " + row["limit_min"] + ", "; //limit_min
                            strSQL += " " + row["deadband_pressure"] + ", "; //deadband_pressure
                            strSQL += " " + row["deadband_flow"] + ", "; //deadband_flow
                            strSQL += " N'" + mainData["remark"].ToString() + "', "; //remark
                            strSQL += "  'N', "; //record_status
                            strSQL += " '" + user.UserID + "', "; //create_user
                            strSQL += "  GETDATE(), "; //create_dtm
                            strSQL += "  null,"; //last_upd_user
                            strSQL += "  null"; //last_upd_dtm
                            strSQL += " ) ";
                            #endregion
                            int cmdbvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                            if (cmdbvhead_id != 0)
                            {
                                decimal step_control_delay = Convert.ToDecimal(row["step_control_delay"]);
                                decimal limit_min = Convert.ToDecimal(row["limit_min"]);
                                decimal deadband_pressure = Convert.ToDecimal(row["deadband_pressure"]);
                                decimal deadband_flow = Convert.ToDecimal(row["deadband_flow"]);
                                string cmd_desc = "Sync=1," + remote_name + ","
                                + "0,"
                                + row["failure_mode"].ToString() + ","
                                + step_control_delay.ToString("##0.00") + ","
                                + "0,"
                                + "0,"
                                + row["time_loop"].ToString() + ","
                                + limit_min.ToString() + ","
                                + deadband_pressure.ToString("##0.00") + ","
                                + deadband_flow.ToString("##0.00") + ",";

                                DataTable dt_arrtime = (DataTable)mainData["cmdbvdetail"];

                                int index = 1;
                                Hashtable arrTime = new Hashtable();
                                arrTime = cs.GetTimPrvtdetaile(dt_arrtime);
                                if (arrTime.Count > 0)
                                {
                                    foreach (DataRow row_detail in dt_arrtime.Rows)
                                    {
                                        object[] Time = new object[2];
                                        Time = (object[])arrTime[index];
                                        TimeSpan time_start = TimeSpan.Parse(Time[0].ToString());
                                        TimeSpan time_end = TimeSpan.Parse(Time[1].ToString());

                                        if (row_detail.IsNull("pressure_value") || row_detail["pressure_value"] == "")
                                            row_detail["pressure_value"] = 0;
                                        if (row_detail.IsNull("flow_value") || row_detail["flow_value"] == "")
                                            row_detail["flow_value"] = 0;
                                        if (row_detail.IsNull("valve_value") || row_detail["valve_value"] == "")
                                            row_detail["valve_value"] = 0;

                                        decimal pressure_value_tmp = Convert.ToDecimal(row_detail["pressure_value"]);
                                        decimal flow_value_tmp = Convert.ToDecimal(row_detail["flow_value"]);
                                        decimal valve_value_tmp = Convert.ToDecimal(row_detail["valve_value"]);

                                        string pressure_value = pressure_value_tmp.ToString("##0.00");
                                        string flow_value = flow_value_tmp.ToString("##0.00");
                                        string valve_value = valve_value_tmp.ToString("##0.00");

                                        //string extension_pump_tmp = row_detail[""].ToString();
                                        //string extension_pump = "0";
                                        //if (objdetail.extension_pump != null)
                                        //{
                                        //    extension_pump = objdetail.extension_pump.ToString();
                                        //}

                                        cmd_desc += row_detail["failure_mode"].ToString() + "/"
                                                    + time_start.ToString(@"hhmm") + "/"
                                                    + time_end.ToString(@"hhmm")
                                                    + "/" + pressure_value + "/"
                                                    + flow_value + "/"
                                                    + valve_value + "/0/,";

                                        #region insert into tb_ctr_cmdbvdetail
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdbvdetail ( ";
                                        strSQL += " cmdbvhead_id, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_data_dtm, ";
                                        strSQL += " order_time, ";
                                        strSQL += " failure_mode, ";
                                        strSQL += " time_start, ";
                                        strSQL += " time_end, ";
                                        strSQL += " pressure_value, ";
                                        strSQL += " flow_value, ";
                                        strSQL += " valve_value, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm, ";
                                        strSQL += " last_upd_user, ";
                                        strSQL += " last_upd_dtm, ";
                                        strSQL += " extension_pump ";
                                        strSQL += " ) ";
                                        strSQL += " VALUES ( ";
                                        strSQL += " '" + cmdbvhead_id + "', "; //cmdbvhead_id
                                        strSQL += " '" + dmacode + "', "; //dmacode
                                        strSQL += " GETDATE(), "; //cmd_data_dtm
                                        strSQL += " " + Convert.ToInt32(row_detail["order_time"]) + ", "; //order_time
                                        strSQL += " " + Convert.ToInt32(row_detail["failure_mode"]) + ", "; //failure_mode
                                        strSQL += " '" + time_start + "', "; //time_start
                                        strSQL += " '" + time_end + "', "; //time_end
                                        strSQL += " '" + pressure_value_tmp.ToString("##0.00") + "', "; //pressure_value
                                        strSQL += " '" + flow_value_tmp.ToString("##0.00") + "', "; //flow_value
                                        strSQL += " '" + valve_value_tmp.ToString("##0.00") + "', "; //valve_value
                                        strSQL += " 'N', "; //record_status
                                        strSQL += " '" + user.UserID + "', "; //create_user
                                        strSQL += " GETDATE(), "; //create_dtm
                                        strSQL += " null, "; //last_upd_user
                                        strSQL += " null, "; //last_upd_dtm
                                        strSQL += " null "; //extension_pump
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        index += 1;
                                    }

                                    if (error == "")
                                    {
                                        if (arrTime.Count != 6)
                                        {
                                            for (int i = 0; i < 6 - arrTime.Count; i++)
                                            {
                                                cmd_desc += "0" + "/" + "0000" + "/" + "0000" + "/0/0/0/0/,";
                                            }
                                        }

                                        #region tb_ctr_cmdlog
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                                        strSQL += " wwcode, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_dtm, ";
                                        strSQL += " cmd_dvtypeid, ";
                                        strSQL += " cmd_headid, ";
                                        strSQL += " cmd_desc, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm ";
                                        strSQL += " ) VALUES ";
                                        strSQL += " ( ";
                                        strSQL += " '" + wwcode + "', ";
                                        strSQL += " '" + dmacode + "', ";
                                        strSQL += " GETDATE(), ";
                                        strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                                        strSQL += " " + cmdbvhead_id + ", ";
                                        strSQL += " '" + cmd_desc + "', ";
                                        strSQL += " 'N', ";
                                        strSQL += " '" + user.UserID + "', ";
                                        strSQL += " GETDATE() ";
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        return JsonConvert.SerializeObject(new { dmacode = dmacode });
                                    }
                                }
                                else {
                                    context.Response.StatusCode = 500;
                                    return JsonConvert.SerializeObject(new { status = "Error:GetTimsteppingdetaile" });
                                }
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:Addsteppinghead" });
                            }
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddManualBv_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);

                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        #region sql insert tb_ctr_cmdbvhead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,percent_valve,failure_mode, ";
                        strSQL += " step_control_delay,time_loop,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm, ";
                        strSQL += " last_upd_user,last_upd_dtm ";
                        strSQL += " ) output INSERTED.cmdbvhead_id ";
                        strSQL += " VALUES ( ";
                        //strSQL += " '" + newCmdprvhead_id + "', ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += "  GETDATE(),";
                        strSQL += " '" + remote_name + "', ";
                        strSQL += " '0', ";
                        strSQL += " " + Convert.ToInt32(mainData["valve"]) + ", ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " 0, ";
                        strSQL += " 0, ";
                        strSQL += " N'" + mainData["remark"].ToString() + "', ";
                        strSQL += "  'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += "  GETDATE(), ";
                        strSQL += "  null,";
                        strSQL += "  null";
                        strSQL += " ) ";
                        #endregion

                        int cmdbvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdbvhead_id != 0)
                        {
                            #region sql insert tb_ctr_cmdlog
                            string cmd_desc = "manual=2," + remote_name + "," + mainData["valve"].ToString() + ",0,0,0,0,0";
                            strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                            strSQL += " wwcode, ";
                            strSQL += " dmacode, ";
                            strSQL += " cmd_dtm, ";
                            strSQL += " cmd_dvtypeid, ";
                            strSQL += " cmd_headid, ";
                            strSQL += " cmd_desc, ";
                            strSQL += " record_status, ";
                            strSQL += " create_user, ";
                            strSQL += " create_dtm ";
                            strSQL += " ) VALUES ";
                            strSQL += " ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += " GETDATE(), ";
                            strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                            strSQL += " " + cmdbvhead_id + ", ";
                            strSQL += " '" + cmd_desc + "', ";
                            strSQL += " 'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += " GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                            if (status)
                            {
                                return JsonConvert.SerializeObject(new { dmacode = dmacode });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "fail" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddAutoBv_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();

                string error = "";

                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        DataTable dt_cmdbvhead = (DataTable)mainData["cmdbvhead"];
                        foreach (DataRow row in dt_cmdbvhead.Rows)
                        {
                            //if (row.IsNull("failure_mode") || row["failure_mode"] == "")
                            //    row["failure_mode"] = 0;
                            if (row.IsNull("step_control_delay") || row["step_control_delay"] == "")
                                row["step_control_delay"] = 0;
                            if (row.IsNull("time_loop") || row["time_loop"] == "")
                                row["time_loop"] = 0;
                            if (row.IsNull("limit_min") || row["limit_min"] == "")
                                row["limit_min"] = 0;
                            if (row.IsNull("deadband_pressure") || row["deadband_pressure"] == "")
                                row["deadband_pressure"] = 0;
                            if (row.IsNull("deadband_flow") || row["deadband_flow"] == "")
                                row["deadband_flow"] = 0;

                            #region insert into tb_ctr_cmdbvhead
                            String strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,percent_valve,failure_mode, ";
                            strSQL += " step_control_delay,time_loop,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm, ";
                            strSQL += " last_upd_user,last_upd_dtm ";
                            strSQL += " ) output INSERTED.cmdbvhead_id ";
                            strSQL += " VALUES ( ";
                            //strSQL += " '" + newCmdprvhead_id + "', ";
                            strSQL += " '" + wwcode + "', "; //wwcode
                            strSQL += " '" + dmacode + "', "; //dmacode
                            strSQL += "  GETDATE(),"; //cmd_data_dtm
                            strSQL += " '" + remote_name + "', "; //remote_name
                            strSQL += " '1', "; //control_mode
                            strSQL += " null, "; //percent_valve
                            strSQL += " null, "; //failure_mode
                            strSQL += " " + row["step_control_delay"] + ", "; //step_control_delay
                            strSQL += " " + row["time_loop"] + ", "; //time_loop
                            strSQL += " " + row["limit_min"] + ", "; //limit_min
                            strSQL += " " + row["deadband_pressure"] + ", "; //deadband_pressure
                            strSQL += " " + row["deadband_flow"] + ", "; //deadband_flow
                            strSQL += " N'" + mainData["remark"].ToString() + "', "; //remark
                            strSQL += "  'N', "; //record_status
                            strSQL += " '" + user.UserID + "', "; //create_user
                            strSQL += "  GETDATE(), "; //create_dtm
                            strSQL += "  null,"; //last_upd_user
                            strSQL += "  null"; //last_upd_dtm
                            strSQL += " ) ";
                            #endregion
                            int cmdbvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                            if (cmdbvhead_id != 0)
                            {
                                decimal step_control_delay = Convert.ToDecimal(row["step_control_delay"]);
                                decimal limit_min = Convert.ToDecimal(row["limit_min"]);
                                decimal deadband_pressure = Convert.ToDecimal(row["deadband_pressure"]);
                                decimal deadband_flow = Convert.ToDecimal(row["deadband_flow"]);
                                int template_cycle = Convert.ToInt32(row["template"]);

                                //string cmd_desc = "Sync=1," + remote_name + ","
                                //+ "0,"
                                //+ row["failure_mode"].ToString() + ","
                                //+ step_control_delay.ToString("##0.00") + ","
                                //+ "0,"
                                //+ "0,"
                                //+ row["time_loop"].ToString() + ","
                                //+ limit_min.ToString() + ","
                                //+ deadband_pressure.ToString("##0.00") + ","
                                //+ deadband_flow.ToString("##0.00") + ",";

                                string cmd_desc = "auto=2," + remote_name + ","
                                + row["time_loop"].ToString() + ","
                                + step_control_delay.ToString("##0.00") + ","
                                + limit_min.ToString() + ","
                                + deadband_pressure.ToString("##0.00") + ","
                                + deadband_flow.ToString("##0.00") + ","
                                + "0,"
                                + "0,"
                                + "0,"
                                + "0,";

                                DataTable dt_arrtime = (DataTable)mainData["cmdbvdetail"];

                                int index = 1;
                                Hashtable arrTime = new Hashtable();
                                arrTime = cs.GetTimPrvtdetaile(dt_arrtime);
                                if (arrTime.Count > 0)
                                {
                                    foreach (DataRow row_detail in dt_arrtime.Rows)
                                    {
                                        object[] Time = new object[2];
                                        Time = (object[])arrTime[index];
                                        TimeSpan time_start = TimeSpan.Parse(Time[0].ToString());
                                        TimeSpan time_end = TimeSpan.Parse(Time[1].ToString());

                                        if (row_detail.IsNull("pressure_value") || row_detail["pressure_value"] == "")
                                            row_detail["pressure_value"] = 0;
                                        if (row_detail.IsNull("flow_value") || row_detail["flow_value"] == "")
                                            row_detail["flow_value"] = 0;
                                        if (row_detail.IsNull("valve_value") || row_detail["valve_value"] == "")
                                            row_detail["valve_value"] = 0;

                                        decimal pressure_value_tmp = Convert.ToDecimal(row_detail["pressure_value"]);
                                        decimal flow_value_tmp = Convert.ToDecimal(row_detail["flow_value"]);
                                        decimal valve_value_tmp = Convert.ToDecimal(row_detail["valve_value"]);

                                        string pressure_value = pressure_value_tmp.ToString("##0.00");
                                        string flow_value = flow_value_tmp.ToString("##0.00");
                                        string valve_value = valve_value_tmp.ToString("##0.00");

                                        //string extension_pump_tmp = row_detail[""].ToString();
                                        //string extension_pump = "0";
                                        //if (objdetail.extension_pump != null)
                                        //{
                                        //    extension_pump = objdetail.extension_pump.ToString();
                                        //}

                                        cmd_desc += row_detail["failure_mode"].ToString() + "/"
                                                    + time_start.ToString(@"hhmm") + "/"
                                                    //+ time_end.ToString(@"hhmm")
                                                    + pressure_value + "/"
                                                    + flow_value + "/"
                                                    + valve_value + "/0/,";

                                        #region insert into tb_ctr_cmdbvdetail
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdbvdetail ( ";
                                        strSQL += " cmdbvhead_id, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_data_dtm, ";
                                        strSQL += " order_time, ";
                                        strSQL += " failure_mode, ";
                                        strSQL += " time_start, ";
                                        strSQL += " time_end, ";
                                        strSQL += " pressure_value, ";
                                        strSQL += " flow_value, ";
                                        strSQL += " valve_value, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm, ";
                                        strSQL += " last_upd_user, ";
                                        strSQL += " last_upd_dtm, ";
                                        strSQL += " extension_pump ";
                                        strSQL += " ) ";
                                        strSQL += " VALUES ( ";
                                        strSQL += " '" + cmdbvhead_id + "', "; //cmdbvhead_id
                                        strSQL += " '" + dmacode + "', "; //dmacode
                                        strSQL += " GETDATE(), "; //cmd_data_dtm
                                        strSQL += " " + Convert.ToInt32(row_detail["order_time"]) + ", "; //order_time
                                        strSQL += " " + Convert.ToInt32(row_detail["failure_mode"]) + ", "; //failure_mode
                                        strSQL += " '" + time_start + "', "; //time_start
                                        strSQL += " '" + time_end + "', "; //time_end
                                        strSQL += " '" + pressure_value_tmp.ToString("##0.00") + "', "; //pressure_value
                                        strSQL += " '" + flow_value_tmp.ToString("##0.00") + "', "; //flow_value
                                        strSQL += " '" + valve_value_tmp.ToString("##0.00") + "', "; //valve_value
                                        strSQL += " 'N', "; //record_status
                                        strSQL += " '" + user.UserID + "', "; //create_user
                                        strSQL += " GETDATE(), "; //create_dtm
                                        strSQL += " null, "; //last_upd_user
                                        strSQL += " null, "; //last_upd_dtm
                                        strSQL += " null "; //extension_pump
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        index += 1;
                                    }

                                    if (error == "")
                                    {
                                        if (arrTime.Count != template_cycle)
                                        {
                                            for (int i = 0; i < template_cycle - arrTime.Count; i++)
                                            {
                                                cmd_desc += "0" + "/" + "0000" + "/0/0/0/0/,";
                                            }
                                        }

                                        #region tb_ctr_cmdlog
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                                        strSQL += " wwcode, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_dtm, ";
                                        strSQL += " cmd_dvtypeid, ";
                                        strSQL += " cmd_headid, ";
                                        strSQL += " cmd_desc, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm ";
                                        strSQL += " ) VALUES ";
                                        strSQL += " ( ";
                                        strSQL += " '" + wwcode + "', ";
                                        strSQL += " '" + dmacode + "', ";
                                        strSQL += " GETDATE(), ";
                                        strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                                        strSQL += " " + cmdbvhead_id + ", ";
                                        strSQL += " '" + cmd_desc + "', ";
                                        strSQL += " 'N', ";
                                        strSQL += " '" + user.UserID + "', ";
                                        strSQL += " GETDATE() ";
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        return JsonConvert.SerializeObject(new { dmacode = dmacode });
                                    }
                                }
                                else {
                                    context.Response.StatusCode = 500;
                                    return JsonConvert.SerializeObject(new { status = "Error:GetTimsteppingdetaile" });
                                }
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:Addsteppinghead" });
                            }
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddManualStepping_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);

                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        #region sql insert tb_ctr_cmdbvhead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,manual_valva_control,failure_mode, ";
                        strSQL += " step_control_delay,time_loop,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm, ";
                        strSQL += " last_upd_user,last_upd_dtm ";
                        strSQL += " ) output INSERTED.cmdbvhead_id ";
                        strSQL += " VALUES ( ";
                        //strSQL += " '" + newCmdprvhead_id + "', ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += "  GETDATE(),";
                        strSQL += " '" + remote_name + "', ";
                        strSQL += " '0', ";
                        strSQL += " '" + mainData["valve"] + "', ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " 0, ";
                        strSQL += " 0, ";
                        strSQL += " N'" + mainData["remark"].ToString() + "', ";
                        strSQL += "  'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += "  GETDATE(), ";
                        strSQL += "  null,";
                        strSQL += "  null";
                        strSQL += " ) ";
                        #endregion

                        int cmdbvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdbvhead_id != 0)
                        {
                            #region sql insert tb_ctr_cmdlog
                            string cmd_desc = "manual=4," + remote_name + "," + ((Boolean)mainData["valve"] ? "1" : "0") + ",0,0,0,0,0";
                            strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                            strSQL += " wwcode, ";
                            strSQL += " dmacode, ";
                            strSQL += " cmd_dtm, ";
                            strSQL += " cmd_dvtypeid, ";
                            strSQL += " cmd_headid, ";
                            strSQL += " cmd_desc, ";
                            strSQL += " record_status, ";
                            strSQL += " create_user, ";
                            strSQL += " create_dtm ";
                            strSQL += " ) VALUES ";
                            strSQL += " ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += " GETDATE(), ";
                            strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                            strSQL += " " + cmdbvhead_id + ", ";
                            strSQL += " '" + cmd_desc + "', ";
                            strSQL += " 'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += " GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                            if (status)
                            {
                                return JsonConvert.SerializeObject(new { dmacode = dmacode });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "fail" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddAutoStepping_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();

                string error = "";

                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        DataTable dt_cmdbvhead = (DataTable)mainData["cmdbvhead"];
                        foreach (DataRow row in dt_cmdbvhead.Rows)
                        {
                            //if (row.IsNull("failure_mode") || row["failure_mode"] == "")
                            //    row["failure_mode"] = 0;
                            if (row.IsNull("step_control_delay") || row["step_control_delay"] == "")
                                row["step_control_delay"] = 0;
                            if (row.IsNull("time_loop") || row["time_loop"] == "")
                                row["time_loop"] = 0;
                            if (row.IsNull("limit_min") || row["limit_min"] == "")
                                row["limit_min"] = 0;
                            if (row.IsNull("headlost") || row["headlost"] == "")
                                row["headlost"] = 0;
                            if (row.IsNull("deadband_pressure") || row["deadband_pressure"] == "")
                                row["deadband_pressure"] = 0;
                            if (row.IsNull("deadband_flow") || row["deadband_flow"] == "")
                                row["deadband_flow"] = 0;

                            #region insert into tb_ctr_cmdbvhead
                            String strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,percent_valve,failure_mode, ";
                            strSQL += " step_control_delay,time_loop,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm, ";
                            strSQL += " last_upd_user,last_upd_dtm,headlost ";
                            strSQL += " ) output INSERTED.cmdbvhead_id ";
                            strSQL += " VALUES ( ";
                            //strSQL += " '" + newCmdprvhead_id + "', ";
                            strSQL += " '" + wwcode + "', "; //wwcode
                            strSQL += " '" + dmacode + "', "; //dmacode
                            strSQL += "  GETDATE(),"; //cmd_data_dtm
                            strSQL += " '" + remote_name + "', "; //remote_name
                            strSQL += " '1', "; //control_mode
                            strSQL += " null, "; //percent_valve
                            strSQL += " null, "; //failure_mode
                            strSQL += " " + row["step_control_delay"] + ", "; //step_control_delay
                            strSQL += " " + row["time_loop"] + ", "; //time_loop
                            strSQL += " " + row["limit_min"] + ", "; //limit_min
                            strSQL += " " + row["deadband_pressure"] + ", "; //deadband_pressure
                            strSQL += " " + row["deadband_flow"] + ", "; //deadband_flow
                            strSQL += " N'" + mainData["remark"].ToString() + "', "; //remark
                            strSQL += "  'N', "; //record_status
                            strSQL += " '" + user.UserID + "', "; //create_user
                            strSQL += "  GETDATE(), "; //create_dtm
                            strSQL += "  null,"; //last_upd_user
                            strSQL += "  null,"; //last_upd_dtm
                            strSQL += "  " + row["headlost"] + ""; //headlost
                            strSQL += " ) ";
                            #endregion
                            int cmdbvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                            if (cmdbvhead_id != 0)
                            {
                                decimal step_control_delay = Convert.ToDecimal(row["step_control_delay"]);
                                decimal limit_min = Convert.ToDecimal(row["limit_min"]);
                                decimal deadband_pressure = Convert.ToDecimal(row["deadband_pressure"]);
                                decimal deadband_flow = Convert.ToDecimal(row["deadband_flow"]);
                                decimal headlost = Convert.ToDecimal(row["headlost"]);
                                int template_cycle = Convert.ToInt32(row["template"]);

                                //string cmd_desc = "Sync=1," + remote_name + ","
                                //+ "0,"
                                //+ row["failure_mode"].ToString() + ","
                                //+ step_control_delay.ToString("##0.00") + ","
                                //+ "0,"
                                //+ "0,"
                                //+ row["time_loop"].ToString() + ","
                                //+ limit_min.ToString() + ","
                                //+ deadband_pressure.ToString("##0.00") + ","
                                //+ deadband_flow.ToString("##0.00") + ",";

                                string cmd_desc = "auto=4," + remote_name + ","
                                + row["time_loop"].ToString() + ","
                                + step_control_delay.ToString("##0.00") + ","
                                + limit_min.ToString() + ","
                                + headlost.ToString("##0.00") + ","
                                + deadband_pressure.ToString("##0.00") + ","
                                + deadband_flow.ToString("##0.00") + ","
                                + "0,"
                                + "0,"
                                + "0,";

                                DataTable dt_arrtime = (DataTable)mainData["cmdbvdetail"];

                                int index = 1;
                                Hashtable arrTime = new Hashtable();
                                arrTime = cs.GetTimPrvtdetaile(dt_arrtime);
                                if (arrTime.Count > 0)
                                {
                                    foreach (DataRow row_detail in dt_arrtime.Rows)
                                    {
                                        object[] Time = new object[2];
                                        Time = (object[])arrTime[index];
                                        TimeSpan time_start = TimeSpan.Parse(Time[0].ToString());
                                        TimeSpan time_end = TimeSpan.Parse(Time[1].ToString());

                                        if (row_detail.IsNull("pressure_value") || row_detail["pressure_value"] == "")
                                            row_detail["pressure_value"] = 0;
                                        if (row_detail.IsNull("flow_value") || row_detail["flow_value"] == "")
                                            row_detail["flow_value"] = 0;
                                        if (row_detail.IsNull("valve_value") || row_detail["valve_value"] == "")
                                            row_detail["valve_value"] = 0;

                                        decimal pressure_value_tmp = Convert.ToDecimal(row_detail["pressure_value"]);
                                        decimal flow_value_tmp = Convert.ToDecimal(row_detail["flow_value"]);
                                        int valve_value_tmp = 0;
                                        //decimal valve_value_tmp = Convert.ToDecimal(row_detail["valve_value"]);

                                        string pressure_value = pressure_value_tmp.ToString("##0.00");
                                        string flow_value = flow_value_tmp.ToString("##0.00");
                                        string valve_value = valve_value_tmp.ToString();

                                        //string extension_pump_tmp = row_detail[""].ToString();
                                        //string extension_pump = "0";
                                        //if (objdetail.extension_pump != null)
                                        //{
                                        //    extension_pump = objdetail.extension_pump.ToString();
                                        //}

                                        cmd_desc += row_detail["failure_mode"].ToString() + "/"
                                                    + time_start.ToString(@"hhmm") + "/"
                                                    //+ time_end.ToString(@"hhmm")
                                                    + pressure_value + "/"
                                                    + flow_value + "/"
                                                    + valve_value + "/0/,";

                                        #region insert into tb_ctr_cmdbvdetail
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdbvdetail ( ";
                                        strSQL += " cmdbvhead_id, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_data_dtm, ";
                                        strSQL += " order_time, ";
                                        strSQL += " failure_mode, ";
                                        strSQL += " time_start, ";
                                        strSQL += " time_end, ";
                                        strSQL += " pressure_value, ";
                                        strSQL += " flow_value, ";
                                        strSQL += " valve_value, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm, ";
                                        strSQL += " last_upd_user, ";
                                        strSQL += " last_upd_dtm, ";
                                        strSQL += " extension_pump ";
                                        strSQL += " ) ";
                                        strSQL += " VALUES ( ";
                                        strSQL += " '" + cmdbvhead_id + "', "; //cmdbvhead_id
                                        strSQL += " '" + dmacode + "', "; //dmacode
                                        strSQL += " GETDATE(), "; //cmd_data_dtm
                                        strSQL += " " + Convert.ToInt32(row_detail["order_time"]) + ", "; //order_time
                                        strSQL += " " + Convert.ToInt32(row_detail["failure_mode"]) + ", "; //failure_mode
                                        strSQL += " '" + time_start + "', "; //time_start
                                        strSQL += " '" + time_end + "', "; //time_end
                                        strSQL += " '" + pressure_value_tmp.ToString("##0.00") + "', "; //pressure_value
                                        strSQL += " '" + flow_value_tmp.ToString("##0.00") + "', "; //flow_value
                                        strSQL += " '" + valve_value_tmp.ToString("##0.00") + "', "; //valve_value
                                        strSQL += " 'N', "; //record_status
                                        strSQL += " '" + user.UserID + "', "; //create_user
                                        strSQL += " GETDATE(), "; //create_dtm
                                        strSQL += " null, "; //last_upd_user
                                        strSQL += " null, "; //last_upd_dtm
                                        strSQL += " null "; //extension_pump
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        index += 1;
                                    }

                                    if (error == "")
                                    {
                                        if (arrTime.Count != template_cycle)
                                        {
                                            for (int i = 0; i < template_cycle - arrTime.Count; i++)
                                            {
                                                //cmd_desc += "0" + "/" + "0000" + "/" + "0000" + "/0/0/0/0/,";
                                                cmd_desc += "0" + "/" + "0000" + "/0/0/0/0/,";
                                            }
                                        }

                                        #region tb_ctr_cmdlog
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                                        strSQL += " wwcode, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_dtm, ";
                                        strSQL += " cmd_dvtypeid, ";
                                        strSQL += " cmd_headid, ";
                                        strSQL += " cmd_desc, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm ";
                                        strSQL += " ) VALUES ";
                                        strSQL += " ( ";
                                        strSQL += " '" + wwcode + "', ";
                                        strSQL += " '" + dmacode + "', ";
                                        strSQL += " GETDATE(), ";
                                        strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                                        strSQL += " " + cmdbvhead_id + ", ";
                                        strSQL += " '" + cmd_desc + "', ";
                                        strSQL += " 'N', ";
                                        strSQL += " '" + user.UserID + "', ";
                                        strSQL += " GETDATE() ";
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        return JsonConvert.SerializeObject(new { dmacode = dmacode });
                                    }
                                }
                                else {
                                    context.Response.StatusCode = 500;
                                    return JsonConvert.SerializeObject(new { status = "Error:GetTimsteppingdetaile" });
                                }
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:Addsteppinghead" });
                            }
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddManualPrvt_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);

                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);

                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        #region sql insert tb_ctr_cmdprvthead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdprvthead (wwcode,dmacode,cmd_data_dtm,remote_name ";
                        strSQL += " ,control_mode ";
                        strSQL += " ,pilot_no ";
                        strSQL += " ,pilot_pressure ";
                        strSQL += " ,remark ";
                        strSQL += " ,record_status ";
                        strSQL += " ,create_user ";
                        strSQL += " ,create_dtm ";
                        strSQL += " ,last_upd_user ";
                        strSQL += " ,last_upd_dtm ";
                        strSQL += " ) output INSERTED.cmdprvthead_id ";
                        strSQL += " VALUES ( ";
                        //strSQL += " '" + newCmdprvhead_id + "', ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += "  GETDATE(),";
                        strSQL += " '" + remote_name + "', ";
                        strSQL += " '0', ";
                        strSQL += " " + Convert.ToInt32(mainData["solenoid"]) + ", ";
                        strSQL += " 0, ";
                        strSQL += " N'" + mainData["remark"].ToString() + "', ";
                        strSQL += "  'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += "  GETDATE(), ";
                        strSQL += "  null,";
                        strSQL += "  null";
                        strSQL += " ) ";
                        #endregion

                        int cmdprvthead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdprvthead_id != 0)
                        {
                            #region sql insert tb_ctr_cmdlog
                            string cmd_desc = "manual=3," + remote_name + "," + mainData["solenoid"] + ",0,0,0,0,0";
                            strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                            strSQL += " wwcode, ";
                            strSQL += " dmacode, ";
                            strSQL += " cmd_dtm, ";
                            strSQL += " cmd_dvtypeid, ";
                            strSQL += " cmd_headid, ";
                            strSQL += " cmd_desc, ";
                            strSQL += " record_status, ";
                            strSQL += " create_user, ";
                            strSQL += " create_dtm ";
                            strSQL += " ) VALUES ";
                            strSQL += " ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += " GETDATE(), ";
                            strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                            strSQL += " " + cmdprvthead_id + ", ";
                            strSQL += " '" + cmd_desc + "', ";
                            strSQL += " 'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += " GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                            if (status)
                            {
                                return JsonConvert.SerializeObject(new { dmacode = dmacode });
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:Addprvdetailfail" });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "Error:Addprvheadfail" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }


            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddAutoPrvt_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();
                string error = "";

                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        #region tb_ctr_cmdprvthead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdprvthead (wwcode,dmacode,cmd_data_dtm,remote_name ";
                        strSQL += " ,control_mode ";
                        strSQL += " ,pilot_no ";
                        strSQL += " ,pilot_pressure ";
                        strSQL += " ,remark ";
                        strSQL += " ,record_status ";
                        strSQL += " ,create_user ";
                        strSQL += " ,create_dtm ";
                        strSQL += " ,last_upd_user ";
                        strSQL += " ,last_upd_dtm ";
                        strSQL += " ) output INSERTED.cmdprvthead_id ";
                        strSQL += " VALUES ( ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += "  GETDATE(),";
                        strSQL += " '" + remote_name + "', ";
                        strSQL += " '1', ";
                        strSQL += " null, ";
                        strSQL += " 0, ";
                        strSQL += " N'" + mainData["remark"].ToString() + "', ";
                        strSQL += "  'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += "  GETDATE(), ";
                        strSQL += "  null,";
                        strSQL += "  null";
                        strSQL += " ) ";

                        #endregion

                        int cmdprvthead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdprvthead_id != 0)
                        {
                            int template_cycle = Convert.ToInt32(mainData["template"]);
                            string cmd_desc = "auto=3," + remote_name + ","
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,"
                               + "0,";

                            DataTable dt_arrtime = (DataTable)mainData["cmdprvtdetail"];

                            int index = 1;
                            Hashtable arrTime = new Hashtable();
                            arrTime = cs.GetTimPrvtdetaile(dt_arrtime);
                            if (arrTime.Count > 0)
                            {
                                //DateTime create_dtm = DateTime.Now;
                                foreach (DataRow row in dt_arrtime.Rows)
                                {
                                    object[] Time = new object[2];
                                    Time = (object[])arrTime[index];
                                    string pressure_value = "0";
                                    string flow_value = "0";
                                    string valve_value = "0";
                                    TimeSpan time_start = TimeSpan.Parse(Time[0].ToString());
                                    TimeSpan time_end = TimeSpan.Parse(Time[1].ToString()); //ไม่จำเป็นต้องใช้

                                    cmd_desc += row["failure_mode"].ToString() + "/" +
                                        time_start.ToString(@"hhmm") + "/" +
                                        //time_end.ToString(@"hhmm") + "/" +
                                        row["pilot_no"].ToString() + "/" +
                                        pressure_value + "/" +
                                        flow_value + "/" +
                                        valve_value + "/,";

                                    if (row.IsNull("pilot_no") || row["pilot_no"] == "")
                                        row["pilot_no"] = "null";

                                    #region sql insert to tb_ctr_cmdprvtdetail
                                    strSQL = string.Empty;
                                    strSQL += " INSERT INTO tb_ctr_cmdprvtdetail ( ";
                                    strSQL += " cmdprvthead_id, ";
                                    strSQL += " dmacode, ";
                                    strSQL += " cmd_data_dtm, ";
                                    strSQL += " order_time, ";
                                    strSQL += " failure_mode, ";
                                    strSQL += " time_end, ";
                                    strSQL += " time_start, ";
                                    strSQL += " pilot_no, ";
                                    strSQL += " pilot_pressure, ";
                                    strSQL += " record_status, ";
                                    strSQL += " create_user, ";
                                    strSQL += " create_dtm, ";
                                    strSQL += " last_upd_user, ";
                                    strSQL += " last_upd_dtm ";
                                    strSQL += " ) VALUES  ";
                                    strSQL += " ( ";
                                    strSQL += " '" + cmdprvthead_id + "', "; //cmdprvthead_id
                                    strSQL += " '" + dmacode + "', ";
                                    strSQL += " GETDATE(), ";
                                    strSQL += " " + Convert.ToInt32(row["order_time"]) + ", ";
                                    strSQL += " " + Convert.ToInt32(row["failure_mode"]) + ", ";
                                    strSQL += " '" + time_end + "', ";
                                    strSQL += " '" + time_start + "', ";
                                    strSQL += " " + row["pilot_no"] + ", ";
                                    strSQL += " 0, ";
                                    strSQL += " 'N', ";
                                    strSQL += " '" + user.UserID + "', ";
                                    strSQL += " GETDATE(), ";
                                    strSQL += " null, ";
                                    strSQL += " null ";
                                    strSQL += " ) ";
                                    #endregion

                                    error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                    index += 1;
                                }

                                if (error == "")
                                {
                                    if (arrTime.Count != template_cycle)
                                    {
                                        for (int i = 0; i < template_cycle - arrTime.Count; i++)
                                        {
                                            cmd_desc += "0" + "/" + "0000" + "/0/0/0/0/,";
                                        }
                                    }

                                    #region tb_ctr_cmdlog

                                    strSQL = string.Empty;
                                    strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                                    strSQL += " wwcode, ";
                                    strSQL += " dmacode, ";
                                    strSQL += " cmd_dtm, ";
                                    strSQL += " cmd_dvtypeid, ";
                                    strSQL += " cmd_headid, ";
                                    strSQL += " cmd_desc, ";
                                    strSQL += " record_status, ";
                                    strSQL += " create_user, ";
                                    strSQL += " create_dtm ";
                                    strSQL += " ) VALUES ";
                                    strSQL += " ( ";
                                    strSQL += " '" + wwcode + "', ";
                                    strSQL += " '" + dmacode + "', ";
                                    strSQL += " GETDATE(), ";
                                    strSQL += " " + Convert.ToInt32(mainData["dvtypeid"]) + ", ";
                                    strSQL += " " + cmdprvthead_id + ", ";
                                    strSQL += " '" + cmd_desc + "', ";
                                    strSQL += " 'N', ";
                                    strSQL += " '" + user.UserID + "', ";
                                    strSQL += " GETDATE() ";
                                    strSQL += " ) ";
                                    #endregion

                                    error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                    return JsonConvert.SerializeObject(new { dmacode = dmacode });
                                }
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:GetTimBvdetaile" });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "Error:AddBvhead" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

                return null;
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddManualAfv_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);

                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                string dvtypeid = mainData["dvtypeid"].ToString();
                int myTimeoutmin = 0;
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        if ((Boolean)mainData["valve"])
                        {
                            myTimeoutmin = Convert.ToInt32(mainData["timeout_min"]);
                        }
                        #region sql insert tb_ctr_cmdbvhead
                        String strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdafvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,manual_valva_control,timeout_min, ";
                        strSQL += " remark,record_status,create_user,create_dtm ";
                        strSQL += " ) output INSERTED.cmdafvhead_id ";
                        strSQL += " VALUES ( ";
                        strSQL += " '" + wwcode + "', "; // wwcode
                        strSQL += " '" + dmacode + "', "; // dmacode
                        strSQL += "  GETDATE(),"; // cmd_date_dtm
                        strSQL += " '" + remote_name + "', "; //remote_name 
                        strSQL += " '0', "; //control_mode
                        strSQL += " '" + mainData["valve"] + "', "; //manual_valva_control
                        strSQL += " '" + myTimeoutmin + "', "; //timeout_min
                        strSQL += " N'" + mainData["remark"].ToString() + "', "; //remark
                        strSQL += "  'N', "; //record_status 
                        strSQL += " '" + user.UserID + "', "; //create_user
                        strSQL += "  GETDATE() "; //create_dtm
                        strSQL += " ) ";
                        #endregion

                        int cmdafvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                        if (cmdafvhead_id != 0)
                        {
                            #region sql insert tb_ctr_cmdlog
                            string cmd_desc = "manual=" + dvtypeid + "," + remote_name + "," + ((Boolean)mainData["valve"] ? "1" : "0") + "," + myTimeoutmin + ",0,0,0,0";
                            strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                            strSQL += " wwcode, ";
                            strSQL += " dmacode, ";
                            strSQL += " cmd_dtm, ";
                            strSQL += " cmd_dvtypeid, ";
                            strSQL += " cmd_headid, ";
                            strSQL += " cmd_desc, ";
                            strSQL += " record_status, ";
                            strSQL += " create_user, ";
                            strSQL += " create_dtm ";
                            strSQL += " ) VALUES ";
                            strSQL += " ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += " GETDATE(), ";
                            strSQL += " " + Convert.ToInt32(dvtypeid) + ", ";
                            strSQL += " " + cmdafvhead_id + ", ";
                            strSQL += " '" + cmd_desc + "', ";
                            strSQL += " 'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += " GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                            if (status)
                            {
                                return JsonConvert.SerializeObject(new { dmacode = dmacode });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "fail" });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddAutoAfv_Template(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                Cs_Controlcenter cs = new Cs_Controlcenter();

                string error = "";
                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                string dvtypeid = mainData["dvtypeid"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    if (!string.IsNullOrEmpty(wwcode) && !string.IsNullOrEmpty(dmacode) && !string.IsNullOrEmpty(remote_name))
                    {
                        DataTable dt_cmdafvhead = (DataTable)mainData["cmdafvhead"];
                        foreach (DataRow row in dt_cmdafvhead.Rows)
                        {
                            if (row.IsNull("pipe_size") || row["pipe_size"] == "")
                                row["pipe_size"] = 0;
                            if (row.IsNull("time_out_min_afv") || row["time_out_min_afv"] == "")
                                row["time_out_min_afv"] = 0;

                            #region insert into tb_ctr_cmdafvhead
                            String strSQL = string.Empty;
                            strSQL += " INSERT INTO tb_ctr_cmdafvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,pipe_size,timeout_min, ";
                            strSQL += " remark,record_status,create_user,create_dtm ";
                            strSQL += " ) output INSERTED.cmdafvhead_id ";
                            strSQL += " VALUES ( ";
                            strSQL += " '" + wwcode + "', "; // wwcode
                            strSQL += " '" + dmacode + "', "; // dmacode
                            strSQL += "  GETDATE(),"; // cmd_date_dtm
                            strSQL += " '" + remote_name + "', "; //remote_name 
                            strSQL += " '1', "; //control_mode
                            strSQL += " " + row["pipe_size"] + ", "; //pipe_size
                            strSQL += " " + row["time_out_min_afv"] + ", "; //timeout_min
                            strSQL += " N'" + mainData["remark"].ToString() + "', "; //remark
                            strSQL += "  'N', "; //record_status 
                            strSQL += " '" + user.UserID + "', "; //create_user
                            strSQL += "  GETDATE() "; //create_dtm
                            strSQL += " ) ";
                            #endregion
                            int cmdafvhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                            if (cmdafvhead_id != 0)
                            {
                                int template_cycle = Convert.ToInt32(row["template"]);

                                string cmd_desc = "auto=" + dvtypeid + "," + remote_name + ","
                                + row["pipe_size"].ToString() + ","
                                + row["time_out_min_afv"].ToString() + ","
                                + "0,"
                                + "0,"
                                + "0,"
                                + "0,"
                                + "0,"
                                + "0,"
                                + "0,";

                                DataTable dt_arrtime = (DataTable)mainData["cmdafvdetail"];

                                int index = 1;
                                Hashtable arrTime = new Hashtable();
                                arrTime = cs.GetTimPrvtdetaile(dt_arrtime);
                                if (arrTime.Count > 0)
                                {
                                    foreach (DataRow row_detail in dt_arrtime.Rows)
                                    {
                                        object[] Time = new object[2];
                                        Time = (object[])arrTime[index];
                                        TimeSpan time_start = TimeSpan.Parse(Time[0].ToString());
                                        TimeSpan time_end = TimeSpan.Parse(Time[1].ToString());

                                        if (row_detail.IsNull("date_worker") || row_detail["date_worker"] == "")
                                            row_detail["date_worker"] = 1;
                                        if (row_detail.IsNull("time_worker") || row_detail["time_worker"] == "")
                                            row_detail["time_worker"] = 0;
                                        if (row_detail.IsNull("flow_worker") || row_detail["flow_worker"] == "")
                                            row_detail["flow_worker"] = 0;

                                        decimal time_worker_tmp = Convert.ToDecimal(row_detail["time_worker"]);
                                        decimal flow_worker_tmp = Convert.ToDecimal(row_detail["flow_worker"]);
                                        //int valve_value_tmp = 0;

                                        string time_worker = time_worker_tmp.ToString("##0.00");
                                        string flow_worker = flow_worker_tmp.ToString("##0.00");
                                        //string valve_value = valve_value_tmp.ToString();

                                        cmd_desc += row_detail["txtMode"].ToString() + "/"
                                                    + time_start.ToString(@"hhmm") + "/"
                                                    + row_detail["date_worker"].ToString() + "/"
                                                    + time_worker + "/"
                                                    + flow_worker + "/0/,";

                                        #region insert into tb_ctr_cmdafvdetail
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdafvdetail ( ";
                                        strSQL += " cmdafvhead_id, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_data_dtm, ";
                                        strSQL += " order_time, ";
                                        strSQL += " failure_mode, ";
                                        strSQL += " time_start, ";
                                        strSQL += " time_end, ";
                                        strSQL += " date_worker, ";
                                        strSQL += " time_worker, ";
                                        strSQL += " flow_worker, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm ";
                                        strSQL += " ) ";
                                        strSQL += " VALUES ( ";
                                        strSQL += " '" + cmdafvhead_id + "', "; //cmdbvhead_id
                                        strSQL += " '" + dmacode + "', "; //dmacode
                                        strSQL += " GETDATE(), "; //cmd_data_dtm
                                        strSQL += " " + Convert.ToInt32(row_detail["order_time"]) + ", "; //order_time
                                        strSQL += " " + Convert.ToInt32(row_detail["txtMode"]) + ", "; //txtMode
                                        strSQL += " '" + time_start + "', "; //time_start
                                        strSQL += " '" + time_end + "', "; //time_end
                                        strSQL += " " + row_detail["date_worker"] + ", "; //date_worker
                                        strSQL += " " + row_detail["time_worker"] + ", "; //time_worker
                                        strSQL += " " + row_detail["flow_worker"] + ", "; //flow_worker
                                        strSQL += " 'N', "; //record_status
                                        strSQL += " '" + user.UserID + "', "; //create_user
                                        strSQL += " GETDATE() "; //create_dtm
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        index += 1;
                                    }

                                    if (error == "")
                                    {
                                        if (arrTime.Count != template_cycle)
                                        {
                                            for (int i = 0; i < template_cycle - arrTime.Count; i++)
                                            {
                                                //cmd_desc += "0" + "/" + "0000" + "/" + "0000" + "/0/0/0/0/,";
                                                cmd_desc += "0" + "/" + "0000" + "/0/0/0/0/,";
                                            }
                                        }

                                        #region tb_ctr_cmdlog
                                        strSQL = string.Empty;
                                        strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                                        strSQL += " wwcode, ";
                                        strSQL += " dmacode, ";
                                        strSQL += " cmd_dtm, ";
                                        strSQL += " cmd_dvtypeid, ";
                                        strSQL += " cmd_headid, ";
                                        strSQL += " cmd_desc, ";
                                        strSQL += " record_status, ";
                                        strSQL += " create_user, ";
                                        strSQL += " create_dtm ";
                                        strSQL += " ) VALUES ";
                                        strSQL += " ( ";
                                        strSQL += " '" + wwcode + "', ";
                                        strSQL += " '" + dmacode + "', ";
                                        strSQL += " GETDATE(), ";
                                        strSQL += " " + Convert.ToInt32(dvtypeid) + ", ";
                                        strSQL += " " + cmdafvhead_id + ", ";
                                        strSQL += " '" + cmd_desc + "', ";
                                        strSQL += " 'N', ";
                                        strSQL += " '" + user.UserID + "', ";
                                        strSQL += " GETDATE() ";
                                        strSQL += " ) ";
                                        #endregion

                                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);

                                        return JsonConvert.SerializeObject(new { dmacode = dmacode });
                                    }
                                }
                                else {
                                    context.Response.StatusCode = 500;
                                    return JsonConvert.SerializeObject(new { status = "Error:GetTimsteppingdetaile" });
                                }
                            }
                            else {
                                context.Response.StatusCode = 500;
                                return JsonConvert.SerializeObject(new { status = "Error:Addsteppinghead" });
                            }
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "Error:ไม่พบตำแหน่งจุดติดตั้งกรุณาลองใหม่" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string AddCloseLoggerAll(String mainDataText)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);

                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);
                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                Cs_initaldata inl = new Cs_initaldata(user);
                string wwcode = mainData["wwcode"].ToString();
                string dmacode = mainData["dmacode"].ToString();
                string remote_name = mainData["remote_name"].ToString();
                string dvtypeid = mainData["dvtypeid"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                try
                {
                    String strSQL = string.Empty;
                    string cmd_desc = "manual=1," + remote_name + ",0,0,0,0,0,0";
                    switch (dvtypeid)
                    {
                        case "2":
                            #region sql insert tb_ctr_cmdbvhead
                            strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode, ";
                            strSQL += " step_control_delay,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm ";
                            strSQL += " ) output INSERTED.cmdbvhead_id ";
                            strSQL += " VALUES ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += "  GETDATE(),";
                            strSQL += " '" + remote_name + "', ";
                            strSQL += " '3', "; //control_mode
                            strSQL += " 0, "; //step_control_delay
                            strSQL += " 0, "; //limit_min
                            strSQL += " 0, "; //deadband_pressure
                            strSQL += " 0, "; //deadband_flow
                            strSQL += " N'" + mainData["remark"].ToString() + "', ";
                            strSQL += "  'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += "  GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            break;
                        case "3":
                            #region sql insert tb_ctr_cmdprvthead
                            strSQL += " INSERT INTO tb_ctr_cmdprvthead (wwcode,dmacode,cmd_data_dtm,remote_name ";
                            strSQL += " ,control_mode ";
                            strSQL += " ,pilot_pressure ";
                            strSQL += " ,remark ";
                            strSQL += " ,record_status ";
                            strSQL += " ,create_user ";
                            strSQL += " ,create_dtm ";
                            strSQL += " ) output INSERTED.cmdprvthead_id ";
                            strSQL += " VALUES ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += "  GETDATE(),";
                            strSQL += " '" + remote_name + "', ";
                            strSQL += " '3', "; //control_mode
                            strSQL += " 0, "; //pilot_pressure
                            strSQL += " N'" + mainData["remark"].ToString() + "', "; //remark
                            strSQL += "  'N', "; //record_status
                            strSQL += " '" + user.UserID + "', "; //create_user
                            strSQL += "  GETDATE() "; //create_dtm
                            strSQL += " ) ";
                            #endregion
                            break;
                        case "4":
                            #region sql insert tb_ctr_cmdbvhead
                            strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode, ";
                            strSQL += " step_control_delay,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm ";
                            strSQL += " ) output INSERTED.cmdbvhead_id ";
                            strSQL += " VALUES ( ";
                            strSQL += " '" + wwcode + "', ";
                            strSQL += " '" + dmacode + "', ";
                            strSQL += "  GETDATE(),";
                            strSQL += " '" + remote_name + "', ";
                            strSQL += " '3', "; //control_mode
                            strSQL += " 0, "; //step_control_delay
                            strSQL += " 0, "; //limit_min
                            strSQL += " 0, "; //deadband_pressure
                            strSQL += " 0, "; //deadband_flow
                            strSQL += " N'" + mainData["remark"].ToString() + "', ";
                            strSQL += "  'N', ";
                            strSQL += " '" + user.UserID + "', ";
                            strSQL += "  GETDATE() ";
                            strSQL += " ) ";
                            #endregion
                            break;
                        case "6":
                            #region sql insert tb_ctr_cmdafvhead
                            strSQL += " INSERT INTO tb_ctr_cmdafvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode, ";
                            strSQL += " remark,record_status,create_user,create_dtm ";
                            strSQL += " ) output INSERTED.cmdafvhead_id ";
                            strSQL += " VALUES ( ";
                            strSQL += " '" + wwcode + "', "; // wwcode
                            strSQL += " '" + dmacode + "', "; // dmacode
                            strSQL += "  GETDATE(),"; // cmd_date_dtm
                            strSQL += " '" + remote_name + "', "; //remote_name 
                            strSQL += " '3', "; //control_mode
                            strSQL += " N'" + mainData["remark"].ToString() + "', "; //remark
                            strSQL += "  'N', "; //record_status 
                            strSQL += " '" + user.UserID + "', "; //create_user
                            strSQL += "  GETDATE() "; //create_dtm
                            strSQL += " ) ";
                            #endregion
                            break;
                        default:
                            break;
                    }
                    int cmdhead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                    if (cmdhead_id != 0)
                    {
                        #region sql insert tb_ctr_cmdlog                       
                        strSQL = string.Empty;
                        strSQL += " INSERT INTO tb_ctr_cmdlog ( ";
                        strSQL += " wwcode, ";
                        strSQL += " dmacode, ";
                        strSQL += " cmd_dtm, ";
                        strSQL += " cmd_dvtypeid, ";
                        strSQL += " cmd_headid, ";
                        strSQL += " cmd_desc, ";
                        strSQL += " record_status, ";
                        strSQL += " create_user, ";
                        strSQL += " create_dtm ";
                        strSQL += " ) VALUES ";
                        strSQL += " ( ";
                        strSQL += " '" + wwcode + "', ";
                        strSQL += " '" + dmacode + "', ";
                        strSQL += " GETDATE(), ";
                        strSQL += " " + Convert.ToInt32(dvtypeid) + ", ";
                        strSQL += " " + cmdhead_id + ", ";
                        strSQL += " '" + cmd_desc + "', ";
                        strSQL += " 'N', ";
                        strSQL += " '" + user.UserID + "', ";
                        strSQL += " GETDATE() ";
                        strSQL += " ) ";
                        #endregion
                        Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                        if (status)
                        {
                            return JsonConvert.SerializeObject(new { dmacode = dmacode });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "fail" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetCtr003_All(String mainDataText)
        {
            /*
             * ตรวจสอบว่า User ผ่านการ Login มาหรือยัง
             */
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];

                String wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + mainData["$_wwcode"].ToString() + "'", user.UserCons);

                String strSQL = @"SELECT
	                                dvt.wwcode , dvt.dmacode , l.name , dvt.dvtype_id , dvt.pilot_num , dvt.control_type , dvt.is_smartlogger , dvt.cycle_id ,us.firstname +' '+us.lastname as fullname , dvt.last_upd_dtm
                                FROM
	                                tb_ctr_dmavalvetype dvt
                                LEFT JOIN LINK_DMA2.dmama2.dbo.loggers l ON dvt.dmacode = l.code
                                LEFT JOIN LINK_DMA2.dmama2.dbo.users us on us.username = dvt.last_upd_user
                                WHERE dvt.wwcode = '" + wwcode + "' AND dvt.dmacode = '" + mainData["$_dmacode"].ToString() + "'";


                DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                strSQL = @"SELECT vt.wwcode, vt.dmacode,cf.pilot_num_ord,vt.remote_name,vt.dvtype_id,vt.pilot_num,convert(varchar,hprvt.pilot_no) as pilot_no,cf.pilot_pressure
                        FROM tb_ctr_dmavalvetype vt left join (select top 1 * from tb_ctr_cmdprvthead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "' and control_mode = '0' order by cmdprvthead_id desc) hprvt on vt.dmacode = hprvt.dmacode right join tb_ctr_dmaconfigpressure cf on cf.wwcode = vt.wwcode And cf.dmacode = vt.dmacode where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "' ORDER BY cf.pilot_num_ord asc";

                DataTable dt_Solenoid = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                foreach (DataRow row in dt_Solenoid.Rows)
                {
                    int index = dt_Solenoid.Rows.IndexOf(row);
                    index++;
                    dt.Columns.Add("pilot_pressure" + index, typeof(double));
                    dt.Rows[0]["pilot_pressure" + index] = row["pilot_pressure"];
                }


                return JsonConvert.SerializeObject(dt);

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string UpdateDmavalvatype(String mainDataText)
        {
            /*
             * ตรวจสอบว่า User ผ่านการ Login มาหรือยัง
             */
            HttpContext context = HttpContext.Current;
            if (context.Session["USER"] != null)
            {
                Hashtable userDetail = new Hashtable();
                userDetail = (Hashtable)context.Session["USER"];
                user = new WebManageUserData(userDetail);
                Cs_initaldata inl = new Cs_initaldata(user);
                var tempMainData = JsonConvert.DeserializeObject<DataTable>(mainDataText);

                if (tempMainData.Rows.Count == 0)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }
                var mainData = tempMainData.Rows[0];
                string error = "";
                string wwcode = mainData["m_wwcode"].ToString();
                wwcode = inl.GetStringbySQL("SELECT code FROM branches WHERE id ='" + wwcode + "'", user.UserCons);
                string dmacode = mainData["m_dmacode"].ToString();
                try
                {
                    if (wwcode != "" && dmacode != "")
                    {
                        String strSQL = string.Empty;
                        strSQL += "UPDATE tb_ctr_dmavalvetype SET dvtype_id = " + Convert.ToInt32(mainData["m_dvtypeddl"]) + " , control_type = " + Convert.ToInt32(mainData["m_controltype"]) + " , is_smartlogger = '" + mainData["m_smartlogger"] + "' , cycle_id = '" + mainData["m_Templatecontrol"] + "'  ";
                        if (Convert.ToInt32(mainData["m_dvtypeddl"]) == 3)
                        {
                            strSQL += " , pilot_num = " + Convert.ToInt32(mainData["m_totlepilot"]) + " ";
                        }
                        else {
                            strSQL += " , pilot_num = null ";
                        }
                        strSQL += " , last_upd_user = '" + user.UserID.ToString() + "' , last_upd_dtm = GETDATE() ";
                        strSQL += " WHERE wwcode = '" + wwcode + "' AND dmacode = '" + dmacode + "' ";

                        error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);
                        if (error == "")
                        {
                            if (Convert.ToInt32(mainData["m_dvtypeddl"]) == 3)
                            {
                                DataTable dt_Pressure = (DataTable)mainData["dmaconfigpressure"];
                                strSQL = string.Empty;
                                strSQL = "select * from tb_ctr_dmaconfigpressure where wwcode = '" + wwcode + "' and dmacode = '" + dmacode + "'";
                                DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                                if (dt.Rows.Count > 0)
                                {
                                    strSQL = string.Empty;
                                    strSQL += "DELETE FROM tb_ctr_dmaconfigpressure WHERE wwcode = '" + wwcode + "' AND dmacode = '" + dmacode + "'";
                                    error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);
                                }

                                foreach (DataRow row in dt_Pressure.Rows)
                                {
                                    if (row.IsNull("pilot_pressure") || row["pilot_pressure"] == "")
                                        row["pilot_pressure"] = 0;

                                    #region Sql insert tb_ctr_dmaconfigpressure
                                    strSQL = string.Empty;
                                    strSQL += " INSERT INTO tb_ctr_dmaconfigpressure (wwcode, ";
                                    strSQL += " dmacode, ";
                                    strSQL += " pilot_num_ord, ";
                                    strSQL += " pilot_pressure, ";
                                    strSQL += " record_status, ";
                                    strSQL += " create_user, ";
                                    strSQL += " create_dtm, ";
                                    strSQL += " last_upd_user, ";
                                    strSQL += " last_upd_dtm ";
                                    strSQL += " ) VALUES  ";
                                    strSQL += " ( ";
                                    strSQL += " '" + wwcode + "',";
                                    strSQL += " '" + dmacode + "',";
                                    strSQL += " " + row["pilot_num_ord"] + ",";
                                    strSQL += " " + row["pilot_pressure"] + ",";
                                    strSQL += " 'N',";
                                    strSQL += " '" + user.UserID + "', ";
                                    strSQL += " GETDATE(), ";
                                    strSQL += " null, ";
                                    strSQL += " null ";
                                    strSQL += " ) ";
                                    #endregion
                                    error = inl.executeSQLreturnerror(strSQL, user.UserCons_PortalDB);
                                }

                            }
                            return JsonConvert.SerializeObject(new { wwcode = wwcode });
                        }
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "ไม่พบรหัสสาขาหรือรหัสอุปกรณ์" });
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = ex.Message.ToString() });
                }
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }


    }
}