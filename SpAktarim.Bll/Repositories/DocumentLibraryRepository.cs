using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SpAktarim.Data.Entities;

namespace SpAktarim.Bll.Repositories
{
    public class DocumentLibraryRepository : RepositoryBase, IDocumentLibraryRepository
    {
        private static class SqlCommand
        {
            public static string Add      => "INSERT INTO DocumentLibrary (SiteID, Url, Title, ItemCount) VALUES (@SiteId, @Url, @Title, @ItemCount); SELECT SCOPE_IDENTITY();";
            public static string All      => "SELECT * FROM DocumentLibrary";
            public static string FindById => "SELECT * FROM DocumentLibrary WHERE ID = @Id";
            public static string Remove   => "DELETE FROM DocumentLibrary WHERE ID = @Id";
            public static string Update   => "UPDATE DocumentLibrary SET SiteID = @SiteId, Url = @Url, Title = @Title, ItemCount = @ItemCount WHERE ID = @Id";
        }

        public DocumentLibraryRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(DocumentLibrary entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(DocumentLibrary).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Add
                    , param: new
                    {
                        SiteId = entity.SiteID,
                        Url = entity.URL,
                        Title = entity.Title,
                        ItemCount = entity.ItemCount
                    }, transaction: Transaction);
        }

        public IEnumerable<DocumentLibrary> All()
        {
            return Connection.Query<DocumentLibrary>(SqlCommand.All, transaction: Transaction);
        }

        public DocumentLibrary FindById(long id)
        {
            return Connection.Query<DocumentLibrary>(SqlCommand.FindById,
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

        public void Remove(DocumentLibrary entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(DocumentLibrary).Name} Entity is null");

            Remove(entity.ID);
        }

        public void Update(DocumentLibrary entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(DocumentLibrary).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Update
                    , param: new
                    {
                        Id = entity.ID,
                        SiteId = entity.SiteID,
                        Url = entity.URL,
                        Title = entity.Title,
                        ItemCount = entity.ItemCount
                    }, transaction: Transaction);
        }


    }
}
