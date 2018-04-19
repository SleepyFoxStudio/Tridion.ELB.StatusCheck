using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tridion.ContentManager.CoreService.Client;

namespace Tridion.Aws.Status
{
    public class TridionItem
    {



        internal static string GetItem(string item)
        {

            using (var coreService = new CoreServiceHelper())
            {
                var result = coreService._client.Read(item, null);
                return result.Title;
            }

        }


    }
}