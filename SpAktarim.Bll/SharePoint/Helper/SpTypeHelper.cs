using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace SpAktarim.Bll.SharePoint.Helper
{
    public static class SpTypeHelper
    {
        public static int GetIntValue(object param)
        {
            int ret = 0;
            if (param != null)
            {
                int newValue;
                if (int.TryParse(param.ToString(), out newValue))
                {
                    ret = newValue;
                }
            }
            return ret;
        }

        public static string GetStringValue(object param)
        {
            string ret = string.Empty;
            if (param != null)
            {
                ret = param.ToString();
            }
            return ret;
        }

        public static string GetLookupValue(object param)
        {
            string ret = string.Empty;
            if (param != null)
            {
                var field = param as FieldLookupValue;
                if (field != null)
                    ret = field.LookupValue;
            }
            return ret;
        }

        public static string GetFieldUserValue(object param)
        {
            string ret = string.Empty;
            if (param != null)
            {
                var field = param as FieldUserValue;
                if (field != null)
                    ret = field.LookupValue;
            }
            return ret;
        }

        public static DateTime GetDateTimeValue(object param)
        {
            DateTime ret = new DateTime();
            if (param != null)
            {
                DateTime dt;
                bool field = DateTime.TryParse(param.ToString(), out dt);
                if (field)
                    ret = dt;
            }
            return ret;
        }

        public static string SpDateTimeToSqlDateTime(object param)
        {
            string ret = string.Empty;
            if (param != null)
            {
                DateTime dt;
                bool field = DateTime.TryParse(param.ToString(), out dt);
                if (field)
                    ret = dt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            return ret;
        }

        public static string SpDateTimeToSqlDateTimeUtc(object param)
        {
            string ret = string.Empty;
            if (param != null)
            {
                DateTime dt;
                bool field = DateTime.TryParse(param.ToString(), out dt);
                if (field)
                    ret = dt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            return ret;
        }
    }
}
