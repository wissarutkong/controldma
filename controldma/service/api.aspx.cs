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
                        _temp["Add"] = "<button type=\"button\" id=\"" + row["description"] + "\" class=\"btn btn-block btn-warning btn-sm addvalva\" value=\"" + row["description"] + "\" data-type=\"" + row["dvtype_id"] + "\"  data-toggle=\"modal\" data-target=\"#Modal_add_valva\" ><span><i class=\"far fa-edit\"></i> กำหนดประตูน้ำ</span></button>";
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
                if (_remote_name != null) {
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
                DataTable dt_timeList = inl.TimerList();


                Html_2 += " <table class=\"table table-striped table-bordered dt-responsive clear-center\" id=\"tblBvAutomatic\" name=\"tblBvAutomatic\">";
                Html_2 += "     <thead>";
                Html_2 += "         <tr>";
                Html_2 += "             <th width=\"5%\">No.</th>";
                Html_2 += "             <th width=\"10%\">Device Mode</th>";
                Html_2 += "             <th width=\"10%\">Time</th>";
                foreach (DataRow row in dt_Solenoid.Rows)
                {
                    Html_2 += "<th>ตัวที่ " + row["pilot_num_ord"].ToString() + " Pressure " + row["pilot_pressure"].ToString() + " (bar)</th>";
                }
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
                   { "_History", Html_4 }
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
                string newCmdprvhead_id = string.Empty;
                //int cmdlod_id = 0;
                try
                {
                    newCmdprvhead_id = inl.GetStringbySQL("SELECT ISNULL(max(cmdprvthead_id)+1,1) as NEW_SEQUENT FROM tb_ctr_cmdprvthead", user.UserCons_PortalDB);
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
                    strSQL += " '" + mainData["remark"] + "', ";
                    strSQL += "  'N', ";
                    strSQL += " '" + user.UserID + "', ";
                    strSQL += "  GETDATE(), ";
                    strSQL += "  null,";
                    strSQL += "  null";
                    strSQL += " ) ";

                    int cmdprvthead_id = inl.executeSQLreturnint(strSQL, user.UserCons_PortalDB);
                    if (cmdprvthead_id != 0)
                    {
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
                        Boolean status = inl.executeSQLreturn(strSQL, user.UserCons_PortalDB);
                        return JsonConvert.SerializeObject(new { remote_name = remote_name });
                    }
                    else {
                        context.Response.StatusCode = 500;
                        return JsonConvert.SerializeObject(new { status = "fail" });
                    }
                }
                catch (Exception ex) {
                    context.Response.StatusCode = 500;
                    return JsonConvert.SerializeObject(new { status = "fail" });
                }

               
            }
            return JsonConvert.SerializeObject(new { redirec = new Cs_manageLoing().GetLoginPage() });
        }



    }
}