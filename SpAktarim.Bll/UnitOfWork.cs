using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpAktarim.Bll.Repositories;

namespace SpAktarim.Bll
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();

                ResetRepositories();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #region [Repositories]

        private ISiteRepository _siteRepository;
        public ISiteRepository SiteRepository => _siteRepository ?? (_siteRepository = new SiteRepository(_transaction));

        private IDocumentLibraryRepository _documentLibraryRepository;
        public IDocumentLibraryRepository DocumentLibraryRepository => _documentLibraryRepository ?? (_documentLibraryRepository = new DocumentLibraryRepository(_transaction));

        private IFolderRepository _folderRepository;
        public IFolderRepository FolderRepository => _folderRepository ?? (_folderRepository = new FolderRepository(_transaction));

        private IFileRepository _fileRepository;
        public IFileRepository FileRepository => _fileRepository ?? (_fileRepository = new FileRepository(_transaction));

        private void ResetRepositories()
        {
            _siteRepository = null;
            _documentLibraryRepository = null;
            _folderRepository = null;
            _fileRepository = null;
        }

        #endregion
    }
}
