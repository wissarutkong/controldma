using controldma.App_Code;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

                DataTable dt = inl.GetDatabySQL(" SELECT dvtype_id , dvtype_name FROM tb_ctr_mm_devicetype WHERE dvtype_id<>1 ORDER BY dvtype_id ", user.UserCons_PortalDB);

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
                    _temp["Detail"] = "<button type=\"button\" id=\"" + row["description"] + "\" class=\"btn btn-block btn-info btn-sm infovalva\" value=\"" + row["description"] + "\" data-toggle=\"modal\" data-target=\"#Modal_info_valva\" ><span><i class=\"fas fa-info-circle\"></i> รายละเอียด</span></button>";
                    _temp["Edit"] = "<button type=\"button\" id=\"" + row["description"] + "\" class=\"btn btn-block btn-danger btn-sm editvalva\" value=\"" + row["description"] + "\" data-type=\"" + row["dvtype_id"] + "\" data-remote=\"" + row["remote_name"] + "\"  data-toggle=\"modal\" data-target=\"\" ><span><i class=\"fas fa-cog\"></i>ตั้งค่า</span></button>";
                    if (Convert.ToBoolean(user.UserAdmin))
                    {
                        _temp["Add"] = "<button type=\"button\" id=\"" + row["description"] + "\" class=\"btn btn-block btn-warning btn-sm addvalva\" value=\"" + row["description"] + "\" data-type=\"" + row["dvtype_id"] + "\"  data-toggle=\"modal\" data-target=\"\" ><span><i class=\"far fa-edit\"></i> กำหนดประตูน้ำ</span></button>";
                    }

                    dt_temp.Rows.Add(_temp);
                }

                var resultArr = dt_temp.Rows.Cast<DataRow>().Select(r => r.ItemArray).ToArray();

                return JsonConvert.SerializeObject(resultArr);
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetRealtimeDataCtr002()
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
                if (_remote_name != null)
                {
                    String strSQL = "EXEC sp_ctr_get_realtime_dmama2 @id_name = '" + _remote_name + "';";

                    DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                    var resultArr = dt.Rows.Cast<DataRow>().Select(r => r.ItemArray).ToArray();

                    return JsonConvert.SerializeObject(resultArr);
                }

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

        [System.Web.Services.WebMethod]
        public static string GetHistoryDataCtr002()
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
                if (_wwcode != null && _dvtype_id != null)
                {
                    String strSQL = string.Empty;
                    if (_dvtype_id == "2")
                    {
                        strSQL = "EXEC sp_ctrl_get_bvcmdlog_dmama2 @wwcode = '" + _wwcode + "',@dmacode= '" + _dmacode + "';";
                    }
                    else {
                        strSQL = "EXEC sp_ctrl_get_prvtcmdlog_dmama2 @wwcode = '" + _wwcode + "',@dmacode= '" + _dmacode + "';";
                    }

                    DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

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
                    if (row["pilot_no"] != null && row["pilot_no"] != "")
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
                    strSQL = "SELECT vt.* FROM tb_ctr_dmavalvetype vt ";
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
                DataTable dt_timeList = inl.TimerList();

                total = dt_cmdprvtdetail.Rows.Count;
                if (total == 0) { total = 1; }
                pilot_num = Convert.ToInt32(dt_pilot_num.Rows[0]["pilot_num"]);

                IndexNo = 0;
                foreach (DataRow row in dt_cmdprvtdetail.Rows)
                {
                    IndexNo += 1;
                    string pilot_no_value = IndexNo.ToString() + row["pilot_no"].ToString();
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
                    }
                }
                if (IndexNo < 7)
                {
                    var j = IndexNo + 1; for (int i = j; i <= 6; i++)
                    {
                        string idpilot = "pilot_" + i.ToString();
                        Html_2 += "<input type=\"hidden\" id=\"" + idpilot + "\" value=\"0\" />";
                    }
                }

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
                        Html_2 += "                 <option value=\"0\" " + selected_dv_0 + ">Disable</option>";
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
                        Html_2 += "             <select id=\"@selmode\" name=\"@selmode\" class=\"form-control\" onchange=\"ChangeMode(this.id);\">";
                        Html_2 += "                 <option value=\"4\">Enable</option>";
                        Html_2 += "                 <option value=\"0\">Disable</option>";
                        Html_2 += "             </select>";
                        Html_2 += "         </td>";
                        Html_2 += "         <td>";
                        Html_2 += "             <select id=\"@txttime\" name=\"@txttime\" class=\"form-control\" disabled=\"true\">";
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
                                for (int pilot = 1; pilot <= 6; pilot++)
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
                Html_2 += "<br><button type=\"button\" class=\"btn btn-primary btn-flat col-md-2\" data-toggle=\"modal\" href=\"#aboutModal_save\" onclick=\"Popup(1,'auto')\" ><i class=\"fa fa-floppy-o\"></i>" + Cs_Controlcenter.BtnSave() + "</button>";
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                Html_2 += " <input type=\"hidden\" id=\"txtpilot_num\" value=\"" + pilot_num + "\" />";
                #endregion

                #region _Realtime

                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime\" class=\"table table-striped table-bordered dt-responsive clear-center\" cellspacing=\"0\">";
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

                Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "     <div class=\"table-responsive\">";
                Html_4 += "         <table id=\"dt_grid_history\" class=\"table table-striped table-bordered dt-responsive clear-center\" cellspacing=\"0\">";
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
                Html_4 += "</div>";

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
                DataTable dt_percent_valva = new DataTable();
                string strSQL = @"SELECT vt.wwcode, vt.dmacode,vt.remote_name,vt.dvtype_id,ISNULL(hbv.percent_valve,0) percent_valve
                        FROM tb_ctr_dmavalvetype vt
                        left join (select top 1 * from tb_ctr_cmdbvhead where wwcode = '" + wwcode + "' And dmacode = '" + mainData["$_dmacode"].ToString() + "'  and control_mode = '0' order by cmdbvhead_id desc) hbv on vt.dmacode = hbv.dmacode where vt.wwcode = '" + wwcode + "' and vt.dmacode = '" + mainData["$_dmacode"].ToString() + "' ";
                DataTable dt_ctr_bvmanual = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);
                if (dt_ctr_bvmanual.Rows.Count > 0)
                {
                    percent_valve = Convert.ToInt32(dt_ctr_bvmanual.Rows[0]["percent_valve"]);
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
                Html += "       <input id=\"txtvalve\" type=\"text\" value=\"" + percent_valve + "\"  class=\"form-control\" style=\"width: 100px\" onkeypress=\"return isNumberKey_Valve(event)\" maxlength=\"3\"> ";
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
                        Html_2 += "             <option value=\"0\"  " + selected_dv_0 + " " + isdisabled + ">Disable</option>";
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
                    for (int i = 0; i < 6; i++)
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
                        Html_2 += "         <option value=\"0\" " + isdisabled + ">Disable</option>";
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
                //Html_2 += " <input type=\"hidden\" id=\"txtRow\" name=\"txtRow\" value=\"" + total + "\" /> ";
                Html_2 += "";

                #endregion

                #region _Realtime
                String Html_3 = string.Empty;

                //Html_3 += "<div style=\"width: 100%;\">";
                Html_3 += "     <div class=\"table-responsive\">";
                Html_3 += "         <table id=\"dt_grid_realtime_bv\" class=\"table table-striped table-bordered dt-responsive clear-center\" cellspacing=\"0\">";
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

                Html_4 += "<div style=\"width: 100%;\">";
                Html_4 += "     <div class=\"table-responsive\">";
                Html_4 += "         <table id=\"dt_grid_history_bv\" class=\"table table-striped table-bordered dt-responsive clear-center\" cellspacing=\"0\">";
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
                Html_4 += "</div>";

                #endregion

                var keyValues = new Dictionary<string, string>
               {
                   { "_manual", Html },
                   { "_Automatic", Html_2 },
                   { "_Realtime", Html_3 },
                   { "_History", Html_4 },
                   { "_txtRow" , total.ToString()},
                   { "_failure_mode" , cmdbvheadfailure_mode },
                   { "_step_control_delay" , cmdbvheadstep_control_delay },
                   { "_time_loop" , cmdbvheadtime_loop },
                   { "_limit_min" , cmdbvheadlimit_min },
                   { "_deadband_pressure" , cmdbvheaddeadband_pressure },
                   { "_deadband_flow" , cmdbvheaddeadband_flow }
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
        public static string GetCommandTimeOut()
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
                string dmacode = _dmacode;
                string remote_name = _remote_name;
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

                string wwcode = _wwcode.ToString();
                string dmacode = _dmacode.ToString();
                string remote_name = _remote_name.ToString();
                //string newCmdprvhead_id = string.Empty;
                //int cmdlod_id = 0;
                try
                {
                    //newCmdprvhead_id = inl.GetStringbySQL("SELECT ISNULL(max(cmdprvthead_id)+1,1) as NEW_SEQUENT FROM tb_ctr_cmdprvthead", user.UserCons_PortalDB);
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
                string wwcode = _wwcode.ToString();
                string dmacode = _dmacode.ToString();
                string remote_name = _remote_name.ToString();

                try
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
                string wwcode = _wwcode.ToString();
                string dmacode = _dmacode.ToString();
                string remote_name = _remote_name.ToString();
                try
                {
                    #region sql insert tb_ctr_cmdbvhead
                    String strSQL = string.Empty;
                    strSQL += " INSERT INTO tb_ctr_cmdbvhead (wwcode,dmacode,cmd_data_dtm,remote_name,control_mode,percent_valve,failure_mode, ";
                    strSQL += " step_control_delay,time_loop,limit_min,deadband_pressure,deadband_flow,remark,record_status,create_user,create_dtm, ";
                    strSQL += " last_upd_user,last_upd_dtm ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
                    //strSQL += "  ";
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
                string wwcode = _wwcode.ToString();
                string dmacode = _dmacode.ToString();
                string remote_name = _remote_name.ToString();

                try
                {
                    DataTable dt_cmdbvhead = (DataTable)mainData["cmdbvhead"];
                    foreach (DataRow row in dt_cmdbvhead.Rows)
                    {
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

                                    decimal pressure_value_tmp = Convert.ToDecimal(row_detail["pressure_value"]);
                                    decimal flow_value_tmp = Convert.ToDecimal(row_detail["flow_value"]);
                                    decimal valve_value_tmp = Convert.ToDecimal(row_detail["valve_value"]);

                                    string pressure_value = pressure_value_tmp.ToString("##0.00");
                                    if (pressure_value == "" || pressure_value == null)
                                        pressure_value = "0.00";
                                    string flow_value = flow_value_tmp.ToString("##0.00");
                                    if (flow_value == "" || flow_value == null)
                                        flow_value = "0.00";
                                    string valve_value = valve_value_tmp.ToString();
                                    if (valve_value == "" || valve_value == null)
                                        valve_value = "0";

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
                                    strSQL += " '" + valve_value_tmp.ToString() + "', "; //valve_value
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
                                return JsonConvert.SerializeObject(new { status = "Error:GetTimBvdetaile" });
                            }
                        }
                        else {
                            context.Response.StatusCode = 500;
                            return JsonConvert.SerializeObject(new { status = "Error:AddBvhead" });
                        }
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
                _wwcode = wwcode;
                _dmacode = mainData["$_dmacode"].ToString();

                String strSQL = @"SELECT
	                                dvt.wwcode , dvt.dmacode , l.name , dvt.dvtype_id , dvt.pilot_num , dvt.control_type , dvt.is_smartlogger ,us.firstname +' '+us.lastname as fullname , dvt.last_upd_dtm
                                FROM
	                                tb_ctr_dmavalvetype dvt
                                LEFT JOIN LINK_DMA2.dmama2.dbo.loggers l ON dvt.dmacode = l.code
                                LEFT JOIN LINK_DMA2.dmama2.dbo.users us on us.username = dvt.last_upd_user
                                WHERE dvt.wwcode = '" + wwcode + "' AND dvt.dmacode = '"+ mainData["$_dmacode"].ToString() + "'";
                DataTable dt = inl.GetDatabySQL(strSQL, user.UserCons_PortalDB);

                return JsonConvert.SerializeObject(dt);

            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }

    }
}