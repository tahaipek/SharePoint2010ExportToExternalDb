using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SpAktarim.Bll.SharePoint.Caml;
using SpAktarim.Data.Enum;

namespace SpAktarim.Bll.SharePoint.Helper
{
    public class SpFolder
    {
        private readonly SpBase _spBase;

        public SpFolder(SpBase spBase)
        {
            if (spBase == null)
                throw new Exception();

            _spBase = spBase;
        }

    }
}
