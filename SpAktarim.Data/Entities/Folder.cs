using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming
namespace SpAktarim.Data.Entities
{
    public class Folder
    {
        public long ID { get; set; } //(long, not null)
        public long SiteID { get; set; } //(long, not null)
        public long DocumentLibraryID { get; set; } //(long, not null)
        public long ParentFolderID { get; set; } //(long, not null)
        public long RemoteID { get; set; } //(long, not null)
        public string FileLeafRef { get; set; } //(nvarchar(max), null)
        public string FileRef { get; set; } //(nvarchar(max), null)
        public string Title { get; set; } //(nvarchar(max), null)
        public DateTime Created { get; set; } //(datetime, null)
        public string Author { get; set; } //(nvarchar(max), null)
        public DateTime Modified { get; set; } //(datetime, null)
        public string Editor { get; set; } //(nvarchar(max), null)
        public string CopySource { get; set; } //(nvarchar(max), null)
        public long ItemChildCount { get; set; } //(long, not null)
        public long FolderChildCount { get; set; } //(long, not null)
    }

}
