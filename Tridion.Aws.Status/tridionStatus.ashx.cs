using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;

namespace Tridion.Aws.Status
{
    /// <summary>
    /// Summary description for status
    /// </summary>
    public class TridionStatus : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var s = new Services();


            context.Response.ContentType = "text/plain";
            context.Response.Write(Environment.MachineName + "\r\n");
            var svcList = s.GetListOfServices();
            bool hasError = false;
            foreach (var service in svcList)
            {

                if (service.Value.Status != ServiceControllerStatus.Running &&
                    service.Value.StartType != ServiceStartMode.Disabled)
                {
                    hasError = true;
                }
                context.Response.Write(service.Value.DisplayName + ":" + service.Value.Status + Environment.NewLine);

            }
            context.Response.StatusCode = hasError ? 500 : 200;

        }

        public bool IsReusable => false;
    }
}