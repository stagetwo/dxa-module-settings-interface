using Alchemy4Tridion.Plugins;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using Tridion.ContentManager.CoreService.Client;
using System.Web;
using System.ServiceModel;
using System.Threading;

namespace ModulesSettingsInterface.Controllers
{
    /// <summary>
    /// A WebAPI web service controller that can be consumed by your front end.
    /// </summary>
    /// <remarks>
    /// The following conditions apply:
    ///     1.) Must have AlchemyRoutePrefix attribute. You pass in the type of your AlchemyPlugin (the one that inherits AlchemyPluginBase).
    ///     2.) Must inherit AlchemyApiController.
    ///     3.) All Action methods must have an Http Verb attribute on it as well as a RouteAttribute (otherwise it won't generate a js proxy).
    /// </remarks>
    [AlchemyRoutePrefix("ModulesSettingsService")]
    public class ModulesSettingsServiceController : AlchemyApiController
    {
        [HttpGet]
        [Route("RetrieveSettings")]
        public string RetrieveSettings(string path)
        {
            SessionAwareCoreServiceClient client = null;
            try
            {
                // Creates a new core service client
                client = new SessionAwareCoreServiceClient("netTcp_2013");
                // Gets the current user so we can impersonate them for our client
                string username = GetUserName();
                client.Impersonate(username);
                OrganizationalItemItemsFilterData filter = new OrganizationalItemItemsFilterData();
                string componentList = string.Empty;
                foreach (XElement element in client.GetListXml(path, filter).Nodes())
                {
                    componentList += element.Attribute("ID").Value + ", ";
                }
                // We no longer need our core service client so we close it now to free resources
                client.Close();
                // Return the HTML representing the pages on which our Tridion object is used
                return componentList;
            }
            catch (Exception ex)
            {
                // proper way of ensuring that the client gets closed... we close it in our try block above,
                // then in a catch block if an exception is thrown we abort it.
                if (client != null)
                {
                    client.Abort();
                }

                // we are rethrowing the original exception and just letting webapi handle it
                throw ex;
            }
        }

        /// <summary>
        /// Borrowed from Tridion.Web.UI.Core.Utils, this gets the current username to be used in 
        /// core service impersonation
        /// </summary>
        /// <returns>
        /// String containing the username
        /// </returns>
        public string GetUserName()
        {
            string text = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
            {
                text = HttpContext.Current.User.Identity.Name;
            }
            else if (ServiceSecurityContext.Current != null && ServiceSecurityContext.Current.WindowsIdentity != null)
            {
                text = ServiceSecurityContext.Current.WindowsIdentity.Name;
            }
            if (string.IsNullOrEmpty(text))
            {
                text = Thread.CurrentPrincipal.Identity.Name;
            }
            return text;
        }
    }
}