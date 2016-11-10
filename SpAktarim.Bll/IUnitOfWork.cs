using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpAktarim.Bll.Repositories;

namespace SpAktarim.Bll
{
    public interface IUnitOfWork : IDisposable
    {
        ISiteRepository SiteRepository { get; }

        void Commit();
    }
}
