using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace controldma.App_Code
{
    public class Cs_initaldata
    {
        public WebManageUserData user;
        public enum Feild { ID , NAME }
        public Cs_initaldata(WebManageUserData user) {
            this.user = user;
        }

        public DataTable GetDatabySQL(String sStringSQL, String sCon)
        {
            DataTable dt = new DataTable();
            SqlConnection cons = new SqlConnection(sCon);
            cons.Open();
            String strSQL = string.Empty;
            strSQL += sStringSQL;
            SqlCommand command = new SqlCommand(strSQL, cons);
            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }
            cons.Close();
            return dt;
        }

        public String GetStringbySQL(String sStringSQL, String sCon)
        {
            DataTable dt = new DataTable();
            SqlConnection cons = new SqlConnection(sCon);
            cons.Open();
            String strSQL = string.Empty; String strResult = string.Empty;
            strSQL += sStringSQL;
            SqlCommand command = new SqlCommand(strSQL, cons);
            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }
            cons.Close();
            if (dt.Rows.Count > 0) { strResult = dt.Rows[0][0].ToString(); }
            return strResult;
        }

        public Boolean GetboolbySQL(String sStringSQL, String sCon)
        {
            DataTable dt = new DataTable();
            Boolean sResult = false;
            SqlConnection cons = new SqlConnection(sCon);
            cons.Open();
            String strSQL = string.Empty;
            strSQL += sStringSQL;
            SqlCommand command = new SqlCommand(strSQL, cons);
            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }
            cons.Close();
            if (dt.Rows.Count > 0) { sResult = true; }
            return sResult;
        }

        public int executeSQLreturnint(String sStringSQL, String sCon)
        {
            DataTable dt = new DataTable();
            Boolean sResult = false;
            SqlConnection cons = new SqlConnection(sCon);
            String strSQL = string.Empty;
            strSQL += sStringSQL;
            int modified = 0;
            using (SqlCommand cmd = new SqlCommand(strSQL, cons))
            {
                cons.Open();
                try
                {
                    modified = (int)cmd.ExecuteScalar();

                    //cmd.ExecuteNonQuery();
                    sResult = true;
                }
                catch (Exception ex) { sResult = false; }              
                cons.Close();
            }
            return modified;
        }

        public Boolean executeSQLreturn(String sStringSQL, String sCon)
        {
            DataTable dt = new DataTable();
            Boolean sResult = false;
            SqlConnection cons = new SqlConnection(sCon);
            String strSQL = string.Empty;
            strSQL += sStringSQL;
            //int modified = 0;
            using (SqlCommand cmd = new SqlCommand(strSQL, cons))
            {
                cons.Open();
                try
                {
                    //modified = (int)cmd.ExecuteScalar();

                    cmd.ExecuteNonQuery();
                    sResult = true;
                }
                catch (Exception ex) { sResult = false; }
                cons.Close();
            }
            return sResult;
        }

        public DataTable GetConfigpressure(String wwcode , String dmacode, String sCon)
        {
            DataTable dt = new DataTable();
            SqlConnection cons = new SqlConnection(sCon);
            cons.Open();
            String strSQL = string.Empty;
            strSQL += "select * from tb_ctr_dmaconfigpressure where wwcode = '" + wwcode + "' and dmacode = '" + dmacode + "'";
            SqlCommand command = new SqlCommand(strSQL, cons);
            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }
            cons.Close();
            return dt;
        }

        public DataTable TimerList() {
            DataTable dt = new DataTable();
            SqlConnection cons = new SqlConnection(user.UserCons_PortalDB);
            cons.Open();
            String strSQL = "exec sp_wpt_get_time_dmama2";
            SqlCommand command = new SqlCommand(strSQL, cons);
            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }
            cons.Close();
            return dt;
        }

        public DataTable GetDmaList(string wwcode) {
            DataTable dt = new DataTable();
            SqlConnection cons = new SqlConnection(user.UserCons_PortalDB);
            cons.Open();
            String strSQL = string.Empty;
            strSQL += " SELECT code as name , name as description , is_enable , is_controllable , meter_type ";
            strSQL += " FROM loggers WHERE is_enable = 1 AND is_controllable = 1 AND meter_type in (1,2) ";
            strSQL += " AND code like '%"+ wwcode + "%' ";
            strSQL += " ORDER BY meter_type desc, [order] ";
            SqlCommand command = new SqlCommand(strSQL, cons);
            try
            {
                using (SqlDataAdapter dtAdapter = new SqlDataAdapter(command)) dtAdapter.Fill(dt);
            }
            catch (Exception ex) { throw; }
            cons.Close();
            return dt;

        }
    }
}