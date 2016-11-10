using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace SpAktarim.Bll.SharePoint.Helper
{
    public class SpList
    {
        private readonly SpBase _spBase;
        public SpList(SpBase spBase)
        {
            if (spBase == null)
                throw new Exception();

            _spBase = spBase;
        }

        /// <summary>
        /// Web Site'ta bulunan listeleri döndürür.
        /// </summary>
        public ListCollection GetLists()
        {
            var web = _spBase.ClientContext.Web;
            ListCollection listCollection = web.Lists;
            _spBase.ClientContext.Load(listCollection);
            _spBase.ClientContext.ExecuteQuery();

            return listCollection;
        }

        /// <summary>
        /// Title'a göre List tipinde liste özelliklerini döndürür.
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="includeRootFolder"></param>
        /// <returns></returns>
        public List GetListByTitle(string listName, bool includeRootFolder = false)
        {
            var list = _spBase.ClientContext.Web.Lists.GetByTitle(listName);
            _spBase.ClientContext.Load(list);

            if (includeRootFolder)
                _spBase.ClientContext.Load(list.RootFolder);

            _spBase.ClientContext.ExecuteQuery();
            return list;
        }

        /// <summary>
        /// Parametrelere göre listedeki verileri döndürür.
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public ListItemCollection GetListItems(string listName, CamlQuery query)
        {
            var list = _spBase.ClientContext.Web.Lists.GetByTitle(listName);
            ListItemCollection listItems = list.GetItems(query);
            _spBase.ClientContext.Load(listItems);
            _spBase.ClientContext.ExecuteQuery();

            return listItems;
        }

        public ListItemCollection GetItems(List spList, CamlQuery query)
        {
            ListItemCollection listItems = spList.GetItems(query);
            _spBase.ClientContext.Load(listItems);
            _spBase.ClientContext.ExecuteQuery();

            return listItems;
        }
    }
}
