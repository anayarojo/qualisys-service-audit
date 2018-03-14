using QualisysServiceAudit.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace QualisysServiceAudit.Services
{
    public class ServicesListener
    {
        private List<ServiceListener> mLstObjServices = null;

        public bool Started
        {
            get { return mLstObjServices.Select(x=> x.Started).Count() > 0; }
        }

        public ServicesListener()
        {
            List<ServiceModel> lLstObjServices = ConfigurationManager.GetSection("Services") as List<ServiceModel>;
            mLstObjServices = lLstObjServices.Select(x => new ServiceListener(x)).ToList();
        }

        public void StartListener()
        {
            if(mLstObjServices != null && mLstObjServices.Count > 0)
            {
                foreach (ServiceListener lObjService in mLstObjServices)
                {
                    lObjService.StartListener();
                }
            }
        }

        public void StopListener()
        {
            if (mLstObjServices != null && mLstObjServices.Count > 0)
            {
                foreach (ServiceListener lObjService in mLstObjServices)
                {
                    lObjService.StopListener();
                }
            }
        }
    }
}
