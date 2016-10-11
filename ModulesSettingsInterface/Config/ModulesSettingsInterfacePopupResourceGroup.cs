using Alchemy4Tridion.Plugins.GUI.Configuration;
using Alchemy4Tridion.Plugins.GUI.Configuration.Elements;

namespace ModulesSettingsInterface.Config
{
    /// <summary>
    /// Represents the ResourceGroup element within the editor configuration that contains this plugin's files
    /// and references.
    /// </summary>
    public class ModulesSettingsInterfacePopupResourceGroup : ResourceGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ModulesSettingsInterfacePopupResourceGroup()
        {
            // When adding files you only need to specify the filename and not full path
            AddFile("ModulesSettingsInterfacePopup.js");
            // The above is just a convenient way of doing the following...
            // AddFile(FileTypes.Reference, "Alchemy.Plugins.HelloWorld.Commands.HelloCommandSet");

            // Since Alchemy comes with several libraries I can reference JQuery this way and avoid having
            // to add it myself
            Dependencies.AddLibraryJQuery();
            
            // If you want this resource group to contain the js proxies to call your webservice, call AddWebApiProxy()
            AddWebApiProxy();
        }
    }
}
