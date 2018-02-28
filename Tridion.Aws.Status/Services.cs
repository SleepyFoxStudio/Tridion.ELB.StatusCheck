using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;


namespace Tridion.Aws.Status
{
    class Services
    {
        internal Dictionary<string, ServiceController> GetListOfServices()
        {
            var servicesList = new List<Process>();
            ServiceController[] services = ServiceController.GetServices().Where(s => s.DisplayName.ToLower().StartsWith("sdl ") || s.DisplayName.ToLower().StartsWith("tridion ")).ToArray();
            var servicesStatus = services.ToDictionary(s => s.ServiceName, s => s);
            return servicesStatus;
        }
    }

}