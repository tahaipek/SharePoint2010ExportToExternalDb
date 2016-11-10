using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SpAktarim.Data.Enum;

namespace SpAktarim.Bll.SharePoint.Caml
{
    public static class CamlHelper
    {
        public static CamlQuery CamlQueryBuilder(string folderPath, DocumentType docType)
        {
            var query = new CamlQuery
            {
                FolderServerRelativeUrl = folderPath,
                ViewXml = "<View>"
                          //+ "<ViewFields>"
                          //+ "   <FieldRef Name='Editor' />"
                          //+ "   <FieldRef Name='Modified' />"
                          //+ "</ViewFields > "
                          + "<Query>"
                          + "   <Where>"
                          + "      <Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>" + (int) docType + "</Value></Eq>"
                          + "   </Where>"
                          + "</Query>"
                          + "</View>"
            };

            return query;
        }
    }
}
