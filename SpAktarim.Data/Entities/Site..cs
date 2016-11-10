using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming
namespace SpAktarim.Data.Entities
{
    public class Site
    {
        public long ID { get; set; } //(bigint, not null)
        public string Url { get; set; } //(nvarchar(max), not null)
        public string Title { get; set; } //(nvarchar(max), not null)
    }

}
