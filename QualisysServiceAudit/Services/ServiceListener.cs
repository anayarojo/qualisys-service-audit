using QualisysLog;
using QualisysServiceAudit.Models;
using System;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;

namespace QualisysServiceAudit.Services
{
    public class ServiceListener
    {
        private bool mBolStopListener = true;
        private bool mBolWorking = false;
        private ServiceModel mObjModel;
        private Thread mObjListenerThread;

        public bool Started
        {
            get { return !mBolStopListener; }
        }

        public ServiceListener(ServiceModel pObjModel)
        {
            mObjModel = pObjModel;
        }

        public void StartListener()
        {
            mObjListenerThread = new Thread(new ThreadStart(StartThreadListen));
            mObjListenerThread.Priority = ThreadPriority.Lowest;
            mObjListenerThread.Start();
        }

        public void StopListener()
        {
            mBolStopListener = true;

            if (mObjListenerThread != null)
            {
                mObjListenerThread.Join(1000);

                if (mObjListenerThread.IsAlive)
                {
                    mObjListenerThread.Abort();
                }

                mObjListenerThread = null;
            }
        }

        private void StartThreadListen()
        {
            mBolStopListener = false;

            if (GetServiceController() != null)
            {
                while (!mBolStopListener)
                {
                    if (!mBolWorking)
                    {
                        CheckServiceStatus(GetServiceController());
                    }

                    Thread.Sleep(10000);
                }
            }
        }

        private void CheckServiceStatus(ServiceController pObjServiceController)
        {
            if (pObjServiceController != null)
            {
                try
                {
                    if (pObjServiceController.Status == ServiceControllerStatus.Stopped)
                    {
                        QsLog.WriteWarning(string.Format("Servicio {0} detenido.", mObjModel.DisplayName));
                        QsLog.WriteSuccess(string.Format("Iniciando servicio {0}...", mObjModel.DisplayName));

                        mBolWorking = true;

                        pObjServiceController.Start();
                        pObjServiceController.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));

                        if (pObjServiceController.Status == ServiceControllerStatus.Running)
                        {
                            QsLog.WriteInfo("Servicio iniciado.");
                        }
                        else
                        {
                            QsLog.WriteError("No se pudo iniciar el servicio.");
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                    if (!IsAdministrator())
                    {
                        QsLog.WriteError("Favor de iniciar la aplicación como Administrador.");
                    }
                    else
                    {
                        QsLog.WriteError("No se pudo iniciar el servicio.");
                    }
                }
                catch (Exception e)
                {
                    QsLog.WriteException(e);
                    QsLog.WriteError("No se pudo iniciar el servicio.");
                }
                finally
                {
                    mBolWorking = false;
                }
            }
        }

        private ServiceController GetServiceController()
        {
            ServiceController lObjServiceController = null;

            try
            {
                lObjServiceController = new ServiceController();
                lObjServiceController.ServiceName = mObjModel.Name;
            }
            catch (Exception lObjException)
            {
                QsLog.WriteException(lObjException);
                lObjServiceController = null;
            }

            return lObjServiceController;
        }

        private bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
