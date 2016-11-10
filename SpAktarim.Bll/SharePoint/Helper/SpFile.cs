using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace SpAktarim.Bll.SharePoint.Helper
{
    public class SpFile
    {
       private readonly SpBase _spBase;
        public SpFile(SpBase spBase)
        {
            if (spBase == null)
                throw new Exception();

            _spBase = spBase;
        }
    }
}
