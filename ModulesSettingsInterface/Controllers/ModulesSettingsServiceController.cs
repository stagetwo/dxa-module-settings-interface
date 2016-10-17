using Alchemy4Tridion.Plugins;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using System.Linq;
using Tridion.ContentManager.CoreService.Client;
using System.Web;
using System.ServiceModel;
using System.Threading;
using System.Collections.Generic;

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
        public string RetrieveSettings(string path, string modules)
        {
            SessionAwareCoreServiceClient client = null;
            try
            {
                Dictionary<string, string> componentsContents = new Dictionary<string, string>();
                // Creates a new core service client
                client = new SessionAwareCoreServiceClient("netTcp_2013");
                // Gets the current user so we can impersonate them for our client
                string username = GetUserName();
                client.Impersonate(username);
                OrganizationalItemItemsFilterData filter = new OrganizationalItemItemsFilterData();
                foreach (XElement element in client.GetListXml(path, filter).Nodes())
                {
                    string componentContents = String.Empty;
                    var componentId = element.Attribute("ID").Value;
                    var componentXmlData = (ComponentData)client.Read(componentId, new ReadOptions());
                    XDocument doc = XDocument.Parse(componentXmlData.Content);
                    XElement xmlData = doc.Root;
                    SchemaFieldsData fields = client.ReadSchemaFields(componentXmlData.Schema.IdRef, false, null);
                    componentContents += "<table style=\"border: 1px solid black;\">";
                    foreach (var field in fields.Fields)
                    {
                        if ((field is SingleLineTextFieldDefinitionData) || field is NumberFieldDefinitionData)
                        {
                            var ns = xmlData.GetDefaultNamespace();
                            if (xmlData.Descendants(ns + field.Name).Any())
                            {
                                foreach (var descendant in xmlData.Elements(ns + field.Name))
                                {
                                    componentContents += "<tr><td>" + field.Name + "</td><td><input type=\"text\" value=\"" + descendant.Value + "\"></td></tr>";
                                }
                            }
                            else
                            {
                                componentContents += "<tr><td>" + field.Name + "</td><td></td></tr>";
                            }
                        }
                    }
                    componentContents += "</table>";
                    var compPath = componentXmlData.LocationInfo.Path;
                    foreach (var module in modules.Split(','))
                    {
                        var moduleProcessed = module.ToLower().Trim().Replace(" ", "");
                        if(compPath.ToLower().Contains(moduleProcessed)){
                            if (!componentsContents.ContainsKey(module))
                            {
                                componentsContents.Add(module, "<h2>" + module + "</h2>" + componentContents);
                            }
                            else
                            {
                                componentsContents[module] = componentsContents[module] + componentContents;
                            }
                        }
                    }
                }
                // We no longer need our core service client so we close it now to free resources
                client.Close();
                // Return the HTML representing the pages on which our Tridion object is used
                return string.Join(Environment.NewLine, componentsContents.Select(kvp => kvp.Value.ToString()));
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