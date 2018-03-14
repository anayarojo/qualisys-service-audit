using QualisysServiceAudit.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace QualisysServiceAudit.Sections
{
    public class ServiceSection : IConfigurationSectionHandler
    {
        public object Create(object pObjParent, object pObjConfigContext, XmlNode pObjSection)
        {
            List<ServiceModel> lLstObjSections = new List<ServiceModel>();

            foreach (XmlNode lObjChildNode in pObjSection.ChildNodes)
            {
                lLstObjSections.Add(new ServiceModel()
                {
                    Index = Convert.ToInt32(lObjChildNode.Attributes["Index"].Value.ToString()),
                    DisplayName = lObjChildNode.Attributes["DisplayName"].Value.ToString(),
                    Name = lObjChildNode.Attributes["Name"].Value.ToString()
                });
            }
            return lLstObjSections;
        }
    }
}
