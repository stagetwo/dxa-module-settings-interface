using Alchemy4Tridion.Plugins.GUI.Configuration;

namespace ModulesSettingsInterface.Config
{
    /// <summary>
    /// Represents an extension element in the editor configuration for creating a context menu extension.
    /// </summary>
    public class ModulesSettingsInterfaceContextMenu : ContextMenuExtension
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ModulesSettingsInterfaceContextMenu()
        {
            // This is the id which gets put on the html element for this menu (to target with css/js).
            AssignId = "ModulesSettingsInterface"; 
            // The name of the extension menu
            Name = "ModulesSettingsInterfaceMenu";
            // Where to add the new menu in the current context menu.
            InsertBefore = "cm_refresh";

            // Generate all of the context menu items...
            AddItem("cm_modulessettingsinterface", "Modules Settings Interface", "ModulesSettingsInterface");

            // We need to addd our resource group as a dependency to this extension
            Dependencies.Add<ModulesSettingsInterfaceResourceGroup>();

            // Actually apply our extension to a particular view.  You can have multiple.
            Apply.ToView(Constants.Views.DashboardView);
        }
    }
}
