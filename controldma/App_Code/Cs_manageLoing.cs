using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace controldma.App_Code
{

    public class Cs_manageLoing
    {
        public String AlertType, txtAlert;
        public String UserID, UserName, Userposition , UserAccess_level, User_Admin , _region , _zone , _wwcode ;
        public Cs_manageLoing()
        {
            //
            //
            //string mySalt = BCrypt.Net.BCrypt.GenerateSalt();

            //string myHash = BCrypt.Net.BCrypt.HashPassword(txtPassword);

            //string test = BCrypt.Net.BCrypt.HashString(txtPassword);

            //String json = cls.GET("https://intranet.pwa.co.th/login/app_gis.php?u=" + txtUsername + "&p=" + cls.MD5Hash(txtPassword) + "");
            //json = json.Replace("(", "");
            //json = json.Replace(")", "");
            //json = json.Replace(";", "");
            //var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);
        }

        public Boolean ManageLoginC(String sUsername, String sPassword, String sCon)
        {
            Boolean bLogin = false;
            SqlConnection cons = new SqlConnection(sCon);
            try
            {
                cons.Open();
                DataTable dtUser = ChkuserData(sUsername, cons);
                if (dtUser.Rows.Count > 0)
                {
                    if (DoesPasswordMatch(dtUser.Rows[0]["password"].ToString(), sPassword.ToString()))
                    {
                        if ((bool)dtUser.Rows[0]["is_controllable"])
                        {
                            DataTable dtRole = ChkRole(sUsername, cons);
                            if (dtRole.Rows.Count > 0) {
                                bLogin = true;
                                UserID = dtUser.Rows[0]["username"].ToString();
                                UserName = dtUser.Rows[0]["fullname"].ToString();
                                Userposition = dtUser.Rows[0]["position"].ToString();
                                UserAccess_level = dtRole.Rows[0]["access_level_id"].ToString();
                                User_Admin = dtRole.Rows[0]["is_admin"].ToString();
                                _region = dtUser.Rows[0]["sector_id"].ToString() != "" ? dtUser.Rows[0]["sector_id"].ToString() : "";
                                _zone = dtUser.Rows[0]["district_id"].ToString() != "" ? dtUser.Rows[0]["district_id"].ToString() : "";
                                _wwcode = dtUser.Rows[0]["branch_id"].ToString() != "" ? dtUser.Rows[0]["branch_id"].ToString() : "";
                            }                          
                        }
                        else {
                            bLogin = false; txtAlert = "คุณไม่มีสิทธิ์เข้าใช้งานระบบ Control โปรดติดต่อผู้ดูแลระบบ"; AlertType = "warning";
                        }
                    }
                    else {
                        bLogin = false; txtAlert = "รหัสผ่านไม่ถูกต้อง"; AlertType = "warning";
                    }

                }
                else { bLogin = false; txtAlert = "ชื่อผู้ใช้งานไม่มีในระบบ"; AlertType = "warning"; }


                cons.Close();
            }
            catch (Exception ex)
            {
                bLogin = false; txtAlert = "ไม่สามารถเข้าใช้งานระบบได้กรุณาติดต่อผุ้ดูแลระบบ"; AlertType = "error";
            }

            return bLogin;
        }

        private bool DoesPasswordMatch(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userEnteredPassword, hashedPwdFromDatabase);
        }

        public string GetLoginPage()
        {
            return "login.aspx";
        }

        private DataTable ChkuserData(String sUser, SqlConnection sCon)
        {
            DataTable dt = new DataTable();

            string sql = "SELECT * , concat(u.firstname , ' ' , u.lastname) as fullname  ";
            sql += "FROM users u WHERE u.is_enable<>0 and u.username = '" + sUser.ToString() + "'";

            SqlCommand command = new SqlCommand(sql, sCon);

            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }

            return dt;
        }

        private DataTable ChkRole(String sUser, SqlConnection sCon)
        {
            DataTable dt = new DataTable();

            string sql = " SELECT ";
            sql += " 	users.name , roles.name , roles.access_level as access_level_id , roles.is_admin, ";
            sql += " 	CASE roles.access_level  ";
            sql += " 			WHEN 1 THEN N'ประเทศ' ";
            sql += " 			WHEN 5 THEN N'ภาค' ";
            sql += " 			WHEN 10 THEN N'เขต' ";
            sql += " 			WHEN 15 THEN N'สาขา' ";
            sql += " 			end as access_level, ";
            sql += " 			branches.name as branch, ";
            sql += " 			districts.name as district, ";
            sql += " 			sectors.name as sector	 ";
            sql += " FROM ";
            sql += " 	users ";
            sql += " INNER JOIN ( ";
            sql += " 	SELECT ";
            sql += " 		* ";
            sql += " 	FROM ";
            sql += " 		model_has_roles ";
            sql += " 	WHERE ";
            sql += " 		model_type = 'user' ";
            sql += " ) AS model_has_roles ON model_has_roles.model_id = users.id ";
            sql += "  INNER JOIN roles on roles.id = model_has_roles.role_id  ";
            sql += " LEFT JOIN (SELECT id,name,code FROM branches) as branches on branches.id = users.branch_id ";
            sql += " LEFT JOIN (SELECT id,name,code FROM districts) as districts on districts.id = users.district_id ";
            sql += " LEFT JOIN (SELECT id,name,code FROM sectors) as sectors on sectors.id = users.sector_id ";
            sql += " WHERE users.username = '"+ sUser.ToString() + "' ";

            SqlCommand command = new SqlCommand(sql, sCon);

            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }

            return dt;
        }

        }
}