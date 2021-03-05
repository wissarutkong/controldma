using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace controldma.App_Code
{
    public class WebManageUserData
    {
        public string UserID;
        public string UserNAME;
        public string Userposition;
        public string UserAccess_level;
        public string UserAdmin;
        public string UserRegion;
        public string UserZone;
        public string UserBranch_defualt;
        public string UserCons;
        public string UserCons_PortalDB;
        public string UserTokenAuthen;

        public WebManageUserData(Hashtable data)
        {
            UserID = data["UserID"] != null ? data["UserID"].ToString().Trim() : "";

            UserNAME = data["UserName"] != null ? data["UserName"].ToString() : "";

            Userposition = data["Userposition"] != null ? data["Userposition"].ToString().Trim() : "";

            UserCons = data["UserCons"] != null ? data["UserCons"].ToString().Trim() : "";

            UserCons_PortalDB = data["UserCons_PortalDB"] != null ? data["UserCons_PortalDB"].ToString().Trim() : "";

            UserAccess_level = data["UserAccess_level"] != null ? data["UserAccess_level"].ToString().Trim() : "";

            UserAdmin = data["UserAdmin"] != null ? data["UserAdmin"].ToString().Trim() : "";

            UserRegion = data["UserRegion"] != null ? data["UserRegion"].ToString().Trim() : "";

            UserZone = data["UserZone"] != null ? data["UserZone"].ToString().Trim() : "";

            UserBranch_defualt = data["UserBranch_defualt"] != null ? data["UserBranch_defualt"].ToString().Trim() : "";

            UserTokenAuthen = data["UserTokenAuthen"] != null ? data["UserTokenAuthen"].ToString().Trim() : "";
        }
    }
}