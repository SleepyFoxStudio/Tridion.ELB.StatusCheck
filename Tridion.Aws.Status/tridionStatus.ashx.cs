using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Web.Configuration;

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


            bool activeMode;
            if (!bool.TryParse(WebConfigurationManager.AppSettings["active"], out activeMode))
            {


                context.Response.StatusCode = 500;
            }

            if (!activeMode)
            {
                context.Response.Write("*************************************************************************\r\n");
                context.Response.Write("*                                                                       *\r\n");
                context.Response.Write("*                                                                       *\r\n");
                context.Response.Write("*                      Services are not monitored                       *\r\n");
                context.Response.Write("*                      active mode is set false in config               *\r\n");
                context.Response.Write("*                                                                       *\r\n");
                context.Response.Write("*                                                                       *\r\n");
                context.Response.Write("*************************************************************************\r\n");
                context.Response.StatusCode = 200;
            }

            var svcList = s.GetListOfServices();
            var hasError = false;
            foreach (var service in svcList)
            {

                if (activeMode && service.Value.Status != ServiceControllerStatus.Running &&
                    service.Value.StartType == ServiceStartMode.Automatic)
                {
                    hasError = true;
                }
                context.Response.Write(service.Value.DisplayName + ":" + service.Value.Status + Environment.NewLine);
            }
            context.Response.Write(hasError ? "Error" : "OK");
            context.Response.StatusCode = hasError ? 500 : 200;

        }

        public bool IsReusable => false;
    }
}