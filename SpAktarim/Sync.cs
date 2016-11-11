using System;
using Microsoft.SharePoint.Client;
using SpAktarim.Bll;
using SpAktarim.Bll.SharePoint.Helper;
using SpAktarim.Bll.SharePoint.Model;
using Ent = SpAktarim.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpAktarim.Bll.SharePoint.Caml;
using SpAktarim.Data.Enum;
using SpAktarim.Helper;

namespace SpAktarim
{
    public class Sync
    {
        private SpBase _sp;
        private UnitOfWork _unitOfWork;

        public Sync()
        {
            try
            {
                Initialize();
                SyncWeb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeObject();

                ConsoleHelper.ShowMessage("SharePoint DB'ye hiyerarşik bir biçimde aktarımı tamamlandı...", ConsoleColor.Green);

            }
        }

        private void Initialize()
        {
            DisposeObject();

            _unitOfWork = new UnitOfWork(Settings.MsSql.ConnectionString);
            _sp = new SpBase(new SpCredential
            {
                IpAddress = Settings.SharePoint.ServerIpAddress,
                Domain = Settings.SharePoint.ServerDomain,
                UserName = Settings.SharePoint.ServerUserName,
                Password = Settings.SharePoint.ServerPassword
            });
        }

        private void DisposeObject()
        {
            _sp?.Dispose();
            _unitOfWork?.Dispose();
        }

        private void SyncWeb()
        {
            var web = _sp.SpWeb.GetWeb();
            if (web != null)
            {
                var site = new Ent::Site()
                {
                    Title = web.Title,
                    Url = $"{Settings.SharePoint.ServerIpAddress}"
                };
                _unitOfWork.SiteRepository.Add(site);
                _unitOfWork.Commit();

                SyncDocumentLibrary(site);
            }
        }

        private void SyncDocumentLibrary(Ent::Site site)
        {
            var siteDocumentLibrary = new Dictionary<Ent::DocumentLibrary, List>();

            #region [DocumentLibrary CommitDb]

            foreach (var d in Settings.SharePoint.RootFolders)
            {
                //Elimizde yalnızca aktarılacak klasörler mevcut. Bu nedenle; bu klasörlerin liste özellikleri gerekli.
                var list = _sp.SpList.GetListByTitle(d, includeRootFolder: true);
                if (list == null)
                    throw new ArgumentNullException(nameof(list));

                var documentLibrary = new Ent::DocumentLibrary()
                {
                    SiteID = site.ID,
                    Title = list.Title,
                    URL = list.RootFolder.ServerRelativeUrl,
                    ItemCount = list.ItemCount
                };
                _unitOfWork.DocumentLibraryRepository.Add(documentLibrary);
                siteDocumentLibrary.Add(documentLibrary, list);
            }
            _unitOfWork.Commit();

            #endregion

            foreach (var dl in siteDocumentLibrary)
            {
                SyncListItem(site, dl);
            }

            //Parallel.ForEach(siteDocumentLibrary, new ParallelOptions { MaxDegreeOfParallelism = 4 }, keyValuePair =>
            //  {
            //      SyncListItem(site, keyValuePair);
            //  });
        }

        private void SyncListItem(Ent::Site site, KeyValuePair<Ent::DocumentLibrary, List> library)
        {
            var documentLibrary = library.Key;
            var folderPath = documentLibrary.URL;

            var commitedFolders = CommitFolders(site, documentLibrary, folderPath, folder: null);
            CommitFiles(site, documentLibrary, folderPath, folder: null);

            if (commitedFolders != null && commitedFolders.Any())
            {
                // Doküman kütüphanesinin en root dizini artık burada.
                // Recursive bu kırılımda yapılacak.
                foreach (var commitedFolder in commitedFolders)
                {
                    RecursiveCommitItems(site, documentLibrary, commitedFolder);
                }
            }
        }

        private void RecursiveCommitItems(Ent::Site site, Ent::DocumentLibrary documentLibrary, Ent::Folder commitedFolder)
        {
            var commitedFolders = new List<Ent::Folder>();
            //{ documentLibrary.URL}
            string folderPath = $"{commitedFolder.FileRef}";
            if (commitedFolder.FolderChildCount > 0)
            {
                commitedFolders = CommitFolders(site, documentLibrary, folderPath, commitedFolder);
            }

            if (commitedFolder.ItemChildCount > 0)
            {
                CommitFiles(site, documentLibrary, folderPath, commitedFolder);
            }

            if (commitedFolders != null && commitedFolders.Any())
            {
                foreach (var folder in commitedFolders)
                {
                    RecursiveCommitItems(site, documentLibrary, folder);
                }
            }
        }


        #region [Folder & Files]

        private List<Ent::Folder> CommitFolders(Ent::Site site, Ent::DocumentLibrary documentLibrary, string folderPath, Ent::Folder folder)
        {
            var commitedFolders = new List<Ent::Folder>();
            try
            {

                var folderListItems = _sp.SpList.GetListItems(documentLibrary.Title, CamlHelper.CamlQueryBuilder(folderPath, DocumentType.Folder));
                if (folderListItems != null)
                {
                    foreach (ListItem listItem in folderListItems)
                    {
                        var folderItem = new Ent::Folder()
                        {
                            SiteID = site.ID,
                            DocumentLibraryID = documentLibrary.ID,
                            ParentFolderID = folder?.ID ?? 0,
                            RemoteID = listItem.Id,
                            FileLeafRef = SpTypeHelper.GetStringValue(listItem["FileLeafRef"]),
                            FileRef = SpTypeHelper.GetStringValue(listItem["FileRef"]),
                            Title = SpTypeHelper.GetStringValue(listItem["Title"]),
                            Created = SpTypeHelper.GetDateTimeValue(listItem["Created"]),
                            Author = SpTypeHelper.GetFieldUserValue(listItem["Author"]),
                            Modified = SpTypeHelper.GetDateTimeValue(listItem["Modified"]),
                            Editor = SpTypeHelper.GetFieldUserValue(listItem["Editor"]),
                            CopySource = String.Empty,
                            ItemChildCount = SpTypeHelper.GetIntValue(listItem["ItemChildCount"]),
                            FolderChildCount = SpTypeHelper.GetIntValue(listItem["FolderChildCount"])
                        };
                        _unitOfWork.FolderRepository.Add(folderItem);
                        commitedFolders.Add(folderItem);
                    }
                    _unitOfWork.Commit();
                }
            }
            catch
            {
                Thread.Sleep(new TimeSpan(0, 0, 1, 0));
                Initialize();
                commitedFolders = CommitFolders(site, documentLibrary, folderPath, folder);
            }
            return commitedFolders;


        }

        private void CommitFiles(Ent::Site site, Ent::DocumentLibrary documentLibrary, string folderPath, Ent::Folder folder)
        {
            try
            {
                var fileListItems = _sp.SpList.GetListItems(documentLibrary.Title, CamlHelper.CamlQueryBuilder(folderPath, DocumentType.File));
                if (fileListItems != null)
                {
                    foreach (ListItem listItem in fileListItems)
                    {
                        var file = new Ent::File()
                        {
                            SiteID = site.ID,
                            DocumentLibraryID = documentLibrary.ID,
                            FolderID = folder?.ID ?? 0,
                            RemoteID = listItem.Id,
                            FileLeafRef = SpTypeHelper.GetStringValue(listItem["FileLeafRef"]),
                            FileRef = SpTypeHelper.GetStringValue(listItem["FileRef"]),
                            FileDirRef = SpTypeHelper.GetStringValue(listItem["FileDirRef"]),
                            Title = SpTypeHelper.GetStringValue(listItem["Title"]),
                            Created = SpTypeHelper.GetDateTimeValue(listItem["Created"]),
                            Author = SpTypeHelper.GetFieldUserValue(listItem["Author"]),
                            Modified = SpTypeHelper.GetDateTimeValue(listItem["Modified"]),
                            Editor = SpTypeHelper.GetFieldUserValue(listItem["Editor"]),
                            CopySource = String.Empty,
                            FileType = SpTypeHelper.GetStringValue(listItem["File_x0020_Type"]),
                            FileSize = SpTypeHelper.GetIntValue(listItem["File_x0020_Size"]),
                            Aktarildimi = false
                        };
                        _unitOfWork.FileRepository.Add(file);
                    }
                    _unitOfWork.Commit();
                }
            }
            catch
            {
                Thread.Sleep(new TimeSpan(0, 0, 1, 0));
                Initialize();
                CommitFiles(site, documentLibrary, folderPath, folder);
            }
        }


        #endregion
    }
}