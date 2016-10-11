using Alchemy4Tridion.Plugins.GUI.Configuration;
using Alchemy4Tridion.Plugins.GUI.Configuration.Elements;

namespace ModulesSettingsInterface.Config
{
    /// <summary>
    /// Represents the ResourceGroup element within the editor configuration that contains this plugin's files
    /// and references.
    /// </summary>
    public class ModulesSettingsInterfaceResourceGroup : ResourceGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ModulesSettingsInterfaceResourceGroup()
        {
            // When adding files you only need to specify the filename and not full path
            AddFile("ModulesSettingsInterfaceCommand.js");

            // When referencing commandsets you can just use the generic AddFile with your CommandSet as the type.
            AddFile<ModulesSettingsInterfaceCommandSet>();

            // The above is just a convenient way of doing the following...
            // AddFile(FileTypes.Reference, "Alchemy.Plugins.HelloWorld.Commands.HelloCommandSet");
            
            // If you want this resource group to contain the js proxies to call your webservice, call AddWebApiProxy()
            AddWebApiProxy();
        }
    }
}
