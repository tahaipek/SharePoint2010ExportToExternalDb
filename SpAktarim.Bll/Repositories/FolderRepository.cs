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
    public class FolderRepository : RepositoryBase, IFolderRepository
    {
        public FolderRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        private static class SqlCommand
        {
            public static string Add => @"INSERT INTO Folder (SiteID, DocumentLibraryID, ParentFolderID, RemoteID, FileLeafRef, FileRef, Title, Created, Author, Modified, Editor, CopySource, ItemChildCount, FolderChildCount) 
                                                    VALUES (@SiteID, @DocumentLibraryID, @ParentFolderID, @RemoteID, @FileLeafRef, @FileRef, @Title, @Created, @Author, @Modified, @Editor, @CopySource, @ItemChildCount, @FolderChildCount); SELECT SCOPE_IDENTITY();";
            public static string All => "SELECT * FROM Folder";
            public static string FindById => "SELECT * FROM Folder WHERE ID = @Id";
            public static string Remove => "DELETE FROM Folder WHERE ID = @Id";
            public static string Update => @"UPDATE Site SET SiteID = @SiteID, DocumentLibraryID = @DocumentLibraryID, ParentFolderID = @ParentFolderID, RemoteID = @RemoteID, FileLeafRef = @FileLeafRef, FileRef = @FileRef, Title = @Title, Created = @Created, Author = @Author, Modified = @Modified, Editor = @Editor, CopySource = @CopySource, ItemChildCount = @ItemChildCount, FolderChildCount = @FolderChildCount 
                                                    WHERE ID = @Id";
        }

        public void Add(Folder entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(Folder).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Add
                    , param: new
                    {
                        SiteID = entity.SiteID,
                        DocumentLibraryID = entity.DocumentLibraryID,
                        ParentFolderID = entity.ParentFolderID,
                        RemoteID = entity.RemoteID,
                        FileLeafRef = entity.FileLeafRef,
                        FileRef = entity.FileRef,
                        Title = entity.Title,
                        Created = entity.Created,
                        Author = entity.Author,
                        Modified = entity.Modified,
                        Editor = entity.Editor,
                        CopySource = entity.CopySource,
                        ItemChildCount = entity.ItemChildCount,
                        FolderChildCount = entity.FolderChildCount
                    }, transaction: Transaction);
        }

        public IEnumerable<Folder> All()
        {
            return Connection.Query<Folder>(SqlCommand.All, transaction: Transaction);
        }

        public Folder FindById(long id)
        {
            return Connection.Query<Folder>(SqlCommand.FindById,
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

        public void Remove(Folder entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(Folder).Name} Entity is null");

            Remove(entity.ID);
        }

        public void Update(Folder entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(Folder).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Update
                    , param: new
                    {
                        Id = entity.ID,
                        SiteID = entity.SiteID,
                        DocumentLibraryID = entity.DocumentLibraryID,
                        ParentFolderID = entity.ParentFolderID,
                        RemoteID = entity.RemoteID,
                        FileLeafRef = entity.FileLeafRef,
                        FileRef = entity.FileRef,
                        Title = entity.Title,
                        Created = entity.Created,
                        Author = entity.Author,
                        Modified = entity.Modified,
                        Editor = entity.Editor,
                        CopySource = entity.CopySource,
                        ItemChildCount = entity.ItemChildCount,
                        FolderChildCount = entity.FolderChildCount
                    }, transaction: Transaction);
        }


    }
}
