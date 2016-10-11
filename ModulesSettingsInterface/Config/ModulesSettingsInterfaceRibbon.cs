using Alchemy4Tridion.Plugins.GUI.Configuration;

namespace ModulesSettingsInterface.Config
{
    /// <summary>
    /// Represents a ribbon tool bar
    /// </summary>
    public class ModulesSettingsInterfaceRibbon : RibbonToolbarExtension
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ModulesSettingsInterfaceRibbon()
        {
            // The id of the element (overridden b/c the ascx in the Group property contains the Id)
            AssignId = "ModulesSettingsInterfaceRibbonToolbar";

            // The filename of the ascx user control that contains the button markup/controls.
            Group = "ModulesSettingsInterfaceGroup.ascx";
            GroupId = Constants.GroupIds.HomePage.ManageGroup;
            InsertBefore = "WhereUsedBtn";

            // The name of the extension.
            Name = "Modules Settings Interface";

            // Which Page tab the extension will go on.
            PageId = Constants.PageIds.HomePage;


            // Don't forget to add a dependency to the resource group that references the command set...
            Dependencies.Add<ModulesSettingsInterfaceResourceGroup>();

            // And apply it to a view.
            Apply.ToView(Constants.Views.DashboardView, "DashboardToolbar");
        }
    }
}
