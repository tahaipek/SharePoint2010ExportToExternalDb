using System;
using System.Diagnostics;
using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpAktarim.Bll.SharePoint.Caml;
using SpAktarim.Bll.SharePoint.Helper;
using SpAktarim.Bll.SharePoint.Model;
using SpAktarim.Data.Enum;

namespace SpAktarim.Test
{
    [TestClass]
    public class SharePointTest
    {
        private readonly SpBase _sp;

        public SharePointTest()
        {
            _sp = new SpBase(new SpCredential()
            {
                IpAddress = Settings.SharePoint.ServerIpAddress,
                Domain = Settings.SharePoint.ServerDomain,
                UserName = Settings.SharePoint.ServerUserName,
                Password = Settings.SharePoint.ServerPassword
            });
        }


        [TestMethod]
        public void GetLists()
        {
            var listCollection = _sp.SpList.GetLists();
            foreach (List listItem in listCollection)
            {
                Debug.WriteLine("Id: {0} Title: {1}", listItem.Id, listItem.Title);
                var list = _sp.SpList.GetListByTitle(listItem.Title, includeRootFolder: true);
                if (list.RootFolder != null)
                    Debug.WriteLine("|t RootFolder: {0} ", list.RootFolder.ServerRelativeUrl);
            }
        }

        [TestMethod]
        public void GetListItems()
        {
            var camlQuery = new CamlQuery
            {
                ViewXml = ""
            };
            var listItems = _sp.SpList.GetListItems("Document Library01", camlQuery);
            foreach (ListItem listItem in listItems)
                Debug.WriteLine("Id: {0} Title: {1}", listItem.Id, listItem["FileRef"]);
        }

        [TestMethod]
        public void GetFolderByPath()
        {
            var folderPath = "/Document Library01/TestFolder";
            var listItems = _sp.SpList.GetListItems("Document Library01", CamlHelper.CamlQueryBuilder(folderPath, DocumentType.File));
            foreach (ListItem listItem in listItems)
            {
                Debug.WriteLine("Id: {0} Title: {1}, FileRef: {2}, FolderCount: {3}, ItemCount: {4}", listItem.Id, listItem["Title"], listItem["FileRef"], listItem["FolderChildCount"], listItem["ItemChildCount"]);
            }
        }


    }
}
