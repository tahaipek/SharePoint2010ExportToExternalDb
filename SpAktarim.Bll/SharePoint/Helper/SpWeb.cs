using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace SpAktarim.Bll.SharePoint.Helper
{
    public class SpWeb
    {
        private readonly SpBase _spBase;

        public SpWeb(SpBase spBase)
        {
            if (spBase == null)
                throw new Exception();

            _spBase = spBase;
        }

        /// <summary>
        /// Web Site'ta bulunan listeleri döndürür.
        /// </summary>
        public Web GetWeb()
        {
            var web = _spBase.ClientContext.Web;
            _spBase.ClientContext.Load(web);
            _spBase.ClientContext.ExecuteQuery();

            return web;
        }
    }
}
