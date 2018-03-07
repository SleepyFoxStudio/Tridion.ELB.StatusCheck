using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Web.Configuration;


namespace Tridion.Aws.Status
{
    class Services
    {
        internal Dictionary<string, ServiceController> GetListOfServices()
        {
            var rx = new Regex(WebConfigurationManager.AppSettings["regexOfServicesToCheckIfRunning"], RegexOptions.IgnoreCase);
            var services = ServiceController.GetServices().Where(s => rx.IsMatch(s.DisplayName) ).ToArray();
            return services.ToDictionary(s => s.ServiceName, s => s);
        }
    }

}