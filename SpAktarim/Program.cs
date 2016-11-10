using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpAktarim.Helper;

namespace SpAktarim
{

    class Program
    {


        static void Main(string[] args)
        {
            ConsoleHelper.Maximize();

            ConsoleHelper.ShowMessage("Uygulama başlatıldı.", ConsoleColor.Green);

            if (!SpinAnimation.IsRunning)
                SpinAnimation.Start(50);

            Console.WriteLine();

            var p = new Program();
            Sync s = new Sync();

            Console.WriteLine();
            if (SpinAnimation.IsRunning)
                SpinAnimation.Stop();


            Console.ReadKey();
        }


    }
}
