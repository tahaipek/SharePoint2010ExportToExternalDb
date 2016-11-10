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
    public class FileRepository : RepositoryBase, IFileRepository
    {

        private static class SqlCommand
        {
            public static string Add => @"INSERT INTO [File] (SiteID, DocumentLibraryID, FolderID, RemoteID, FileLeafRef, FileRef, FileDirRef, Title, Created, Author, Modified, Editor, CopySource, FileType, FileSize, Aktarildimi) 
                                                    VALUES (@SiteID, @DocumentLibraryID, @FolderID, @RemoteID, @FileLeafRef, @FileRef, @FileDirRef, @Title, @Created, @Author, @Modified, @Editor, @CopySource, @FileType, @FileSize, @Aktarildimi); SELECT SCOPE_IDENTITY();";
            public static string All => "SELECT * FROM [File]";
            public static string FindById => "SELECT * FROM [File] WHERE ID = @Id";
            public static string Remove => "DELETE FROM [File] WHERE ID = @Id";
            public static string Update => @"UPDATE [File] SET SiteID = @SiteID, DocumentLibraryID = @DocumentLibraryID, FolderID = @FolderID, RemoteID = @RemoteID, FileLeafRef = @FileLeafRef, FileRef = @FileRef, FileDirRef = @FileDirRef, Title = @Title, Created = @Created, Author = @Author, Modified = @Modified, Editor = @Editor, CopySource = @CopySource, FileType = @FileType, FileSize = @FileSize, Aktarildimi = @Aktarildimi 
                                                    WHERE ID = @Id";
        }

        public FileRepository(IDbTransaction transaction) : base(transaction)
        {
        }


        public void Add(File entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(File).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Add
                    , param: new
                    {
                        SiteID = entity.SiteID,
                        DocumentLibraryID = entity.DocumentLibraryID,
                        FolderID = entity.FolderID,
                        RemoteID = entity.RemoteID,
                        FileLeafRef = entity.FileLeafRef,
                        FileRef = entity.FileRef,
                        FileDirRef = entity.FileDirRef,
                        Title = entity.Title,
                        Created = entity.Created,
                        Author = entity.Author,
                        Modified = entity.Modified,
                        Editor = entity.Editor,
                        CopySource = entity.CopySource,
                        FileType = entity.FileType,
                        FileSize = entity.FileSize,
                        Aktarildimi = entity.Aktarildimi
                    }, transaction: Transaction);
        }

        public IEnumerable<File> All()
        {
            return Connection.Query<File>(SqlCommand.All, transaction: Transaction);
        }

        public File FindById(long id)
        {
            return Connection.Query<File>(SqlCommand.FindById,
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

        public void Remove(File entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(File).Name} Entity is null");

            Remove(entity.ID);
        }

        public void Update(File entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(Folder).Name} Entity is null");

            entity.ID = Connection.ExecuteScalar<long>(SqlCommand.Update
                    , param: new
                    {
                        Id = entity.ID,
                        SiteID = entity.SiteID,
                        DocumentLibraryID = entity.DocumentLibraryID,
                        FolderID = entity.FolderID,
                        RemoteID = entity.RemoteID,
                        FileLeafRef = entity.FileLeafRef,
                        FileRef = entity.FileRef,
                        FileDirRef = entity.FileDirRef,
                        Title = entity.Title,
                        Created = entity.Created,
                        Author = entity.Author,
                        Modified = entity.Modified,
                        Editor = entity.Editor,
                        CopySource = entity.CopySource,
                        FileType = entity.FileType,
                        FileSize = entity.FileSize,
                        Aktarildimi = entity.Aktarildimi
                    }, transaction: Transaction);
        }
    }
}
