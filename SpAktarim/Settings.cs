using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpAktarim
{
    public static class Settings
    {
        public static class SharePoint
        {
            public static string ServerIpAddress => "http://10.1.1.1";
            public static string ServerDomain => "SP";
            public static string ServerUserName => "spadmin";
            public static string ServerPassword => "Passw0rd";
            public static string GetFullUserName => "${ServerDomain}\\$(ServerUserName)";

            /// <summary>
            /// Aktarılması beklenen kök dizinler. Bu klasör dışındakiler aktarılmayacak.
            /// </summary>
            public static List<string> RootFolders = new List<string>
            {
                "Document Library01",
                "Document Library02",
                "Document Library03",
                "Document Library04",
                "Document Library05",
                "Document Library06",
            };
        }

        public static class MsSql
        {
            public static string ConnectionString => ConfigurationManager.ConnectionStrings["LocalDb"].ToString();
        }
    }
}
