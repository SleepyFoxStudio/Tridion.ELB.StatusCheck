using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tridion.ContentManager.CoreService.Client;
using System.ServiceModel;
using System.Xml;
using System.Configuration;
using System.Web.Configuration;

namespace Tridion.Aws.Status
{
    public class CoreServiceHelper : IDisposable
    {
        public readonly SessionAwareCoreServiceClient _client;

        public CoreServiceHelper()
        {
            _client = GetNetTcpClient();
        }

        private static SessionAwareCoreServiceClient GetNetTcpClient()
        {
            try
            {
                var netTcpBinding = new NetTcpBinding
                {
                    MaxReceivedMessageSize = int.MaxValue,
                    ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxStringContentLength = int.MaxValue,
                        MaxArrayLength = int.MaxValue
                    },
                };
                var remoteAddress = new EndpointAddress(WebConfigurationManager.AppSettings["CoreServiceNetTcpBinding"]);
                var client = new SessionAwareCoreServiceClient(netTcpBinding, remoteAddress);
                return client;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Close()
        {
            if (_client != null)
            {
                if (_client.State == CommunicationState.Faulted)
                {
                    _client.Abort();
                }
                else
                {
                    if (_client.State != CommunicationState.Opened)
                    {
                        return;
                    }

                    try
                    {
                        _client.Close();
                    }
                    catch (CommunicationException ex)
                    {
                        _client.Abort();
                    }
                    catch (TimeoutException ex)
                    {
                        _client.Abort();
                    }
                    catch (Exception ex)
                    {
                        _client.Abort();
                    }
                }
            }
        }

        #endregion
    }

}