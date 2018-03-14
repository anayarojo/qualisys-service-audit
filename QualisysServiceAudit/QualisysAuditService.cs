using QualisysLog;
using QualisysServiceAudit.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace QualisysServiceAudit
{
    partial class QualisysAuditService : ServiceBase
    {
        private Timer mObjAuditTimer;
        private ServicesListener mObjListeners;
        private bool mBolStarted = false;

        public QualisysAuditService()
        {
            InitializeComponent();
            mObjListeners = new ServicesListener();

            mObjAuditTimer = new Timer(60000);
            mObjAuditTimer.Elapsed += InternalCheckAuditService;
            mObjAuditTimer.Start();
        }

        protected override void OnStart(string[] args)
        {
            mBolStarted = true;

            try
            {
                mObjListeners.StartListener();
                QsLog.WriteInfo("Servicio iniciado.");
            }
            catch (Exception lObjException)
            {
                QsLog.WriteException(lObjException);
                mBolStarted = false;
                base.Stop();
            }
        }

        protected override void OnStop()
        {
            mBolStarted = false;

            try
            {
                mObjListeners.StopListener();
                QsLog.WriteInfo("Servicio detenido.");
            }
            catch (Exception lObjException)
            {
                QsLog.WriteException(lObjException);
                mBolStarted = true;
            }
        }

        private void InternalCheckAuditService(object pObjSender, ElapsedEventArgs pObjEventArgs)
        {
            try
            {
                if (mBolStarted && mObjListeners != null && !mObjListeners.Started)
                {
                    mObjListeners.StartListener();
                }
            }
            catch (Exception lObjException)
            {
                QsLog.WriteException(lObjException);
                mBolStarted = true;
            }
        }
    }
}
