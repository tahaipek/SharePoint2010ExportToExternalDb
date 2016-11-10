using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SpAktarim.Bll.SharePoint.Model;


namespace SpAktarim.Bll.SharePoint.Helper
{
    public class SpBase
    {
        public ClientContext ClientContext;
        public SpWeb SpWeb;
        public SpList SpList;
        public SpFolder SpFolder;
        public SpFile SpFile;
        public SpBase()
        {
        }

        public SpBase(SpCredential spCredential)
        {
            ClientContext = new ClientContext(spCredential.IpAddress)
            {
                AuthenticationMode = ClientAuthenticationMode.Default,
                Credentials = new NetworkCredential(spCredential.UserName, spCredential.Password, spCredential.Domain)
            };

            int timeout = 10 * 60 * 150; // 1.5 minutes
            ClientContext.RequestTimeout = -1;
            ClientContext.PendingRequest.RequestExecutor.RequestKeepAlive = true;
            ClientContext.PendingRequest.RequestExecutor.WebRequest.KeepAlive = true;
            ClientContext.PendingRequest.RequestExecutor.WebRequest.Timeout = timeout;
            ClientContext.PendingRequest.RequestExecutor.WebRequest.ReadWriteTimeout = timeout;
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;
            System.Net.ServicePointManager.MaxServicePointIdleTime = 2000;
            System.Net.ServicePointManager.MaxServicePoints = 1000;
            System.Net.ServicePointManager.SetTcpKeepAlive(false, 0, 0);
            ServicePointManager.DnsRefreshTimeout = timeout;      


            SpWeb = new SpWeb(this);
            SpList = new SpList(this);
            SpFolder = new SpFolder(this);
            SpFile = new SpFile(this);
        }
        

    }
}
