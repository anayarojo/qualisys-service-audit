using QualisysLog;
using QualisysServiceAudit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace QualisysServiceAudit
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG

            ServicesListener lObjListeners = new ServicesListener();
            lObjListeners.StartListener();

            Console.WriteLine("\nPress ENTER to continue...");
            Console.ReadLine();
#else
            try
            {
                ServiceBase[] ServicesToRun = new ServiceBase[] 
                {
                    new QualisysAuditService()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception lObjException)
            {
                QsLog.WriteException(lObjException);
            }
#endif
        }
    }
}
