using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SpAktarim.Data.Entities;

namespace SpAktarim.Bll.Repositories
{
    public class SiteRepository : RepositoryBase, ISiteRepository
    {
        private static class SqlCommand
        {
            public static string Add => "INSERT INTO Site (Url, Title) VALUES (@Url, @Title); SELECT SCOPE_IDENTITY();";
            public static string All => "SELECT * FROM Site";
            public static string FindById => "SELECT * FROM Site WHERE ID = @Id";
            public static string Remove => "DELETE FROM Site WHERE ID = @Id";
            public static string Update => "UPDATE Site SET Url = @Url, Title = @Title WHERE ID = @Id";
        }

        public SiteRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(Site entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(Site).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Add
                    , param: new
                    {
                        Url = entity.Url,
                        Title = entity.Title
                    }, transaction: Transaction);
        }

        public IEnumerable<Site> All()
        {
            return Connection.Query<Site>(SqlCommand.All, transaction: Transaction);
        }

        public Site FindById(long id)
        {
            return Connection.Query<Site>(SqlCommand.FindById,
                param: new { Id = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Remove(long id)
        {
            Connection.Execute(SqlCommand.Remove,
                param: new { Id = id },
                transaction: Transaction);
        }

        public void Remove(Site entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(Site).Name} Entity is null");

            Remove(entity.ID);
        }

        public void Update(Site entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(Site).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Update
                    , param: new
                    {
                        Id = entity.ID,
                        Url = entity.Url,
                        Title = entity.Title
                    }, transaction: Transaction);
        }
    }
}