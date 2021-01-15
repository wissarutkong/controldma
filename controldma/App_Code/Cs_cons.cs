using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using EDCryptor = EnCrypt_DeCrypt.Cryptor;


namespace controldma.App_Code
{
    public class Cs_cons
    {

        public enum Database { dma , dma2 , PortalDB };
        public Cs_cons()
        {

        }

        public String GetConnectionString(Database NAME)
        {
            StringBuilder ConStr = new StringBuilder();
            StringBuilder RtnStr = new StringBuilder();

            string s = "Data Source=164.115.22.222;Initial Catalog=RTU;User ID=sense;Password=sense";
            string sss = EDCryptor.Crypting(s.ToString(), iMode.Encode);

            ConStr.Remove(0, ConStr.Length);
            switch (NAME)
            {
                case Database.dma2:
                    ConStr.Append("yYdhbOWZlYW0rS7ObFDtiPpcB7278othpYDGfNHPtYp7SeOHGqUA67gEd0/882Os+sXZzDDa8n7nUIkkvtFbg3TosTYJ55aueR5TwUNxHV/nbIuv2t+UWQ==");
                    break;
                case Database.PortalDB:
                    ConStr.Append("yYdhbOWZlYV56gcZojntQHo6ihdxo08UwfWoij5/6S0bV5odjO6VfTycdDL7FoPk7U1l5hbvANzCK81w5UmMjqN+wXMscZuUm8kDgcohbgQ=");
                    break;
            }

            RtnStr.Remove(0, RtnStr.Length);
            try
            {
                RtnStr.Append(EDCryptor.Crypting(ConStr.ToString(), iMode.Decode));
            }
            catch (Exception ex)
            {
                RtnStr.Append(string.Empty);
            }
            return RtnStr.ToString();
        }

        //Example
        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }


    }
}