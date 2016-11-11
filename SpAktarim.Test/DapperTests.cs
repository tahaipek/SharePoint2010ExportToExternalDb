using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpAktarim.Bll;
using Ent = SpAktarim.Data.Entities;

namespace SpAktarim.Test
{
    [TestClass]
    public class DapperTests
    {
        [TestClass]
        public class DapperUnitOfWorkTest
        {
            [TestMethod]
            public void AddSite()
            {
                using (var uow = new UnitOfWork(Settings.MsSql.ConnectionString))
                {
                    var site = new Ent::Site()
                    {
                        Title = "Test",
                        Url = "http://10.1.1.1/"
                    };
                    uow.SiteRepository.Add(site);
                    uow.Commit();
                    Debug.WriteLine(site.ID.ToString());

                    var allSiteList = uow.SiteRepository.All();
                    foreach (var s in allSiteList)
                    {
                        Debug.WriteLine(s.Title);
                    }
                }
            }

        }
        [TestMethod]
        public void DapperSelectTest()
        {
            using (IDbConnection connection = new SqlConnection(Settings.MsSql.ConnectionString))
            {
                const string query = "SELECT * FROM Site";
                var siteList = connection.Query<Ent::File>(query);
                Debug.WriteLine(siteList.Count());
            }
        }


    }
}
