/**
 * Creates an anguilla command using a wrapper shorthand. Command is responsible for communicating wtih api controller
 * to get the core service api version.
 *
 * Note the ${PluginName} will get replaced by the actual plugin name.
 */
Alchemy.command("${PluginName}", "ModulesSettingsInterface", {

    /**
     * If an init function is created, this will be called from the command's constructor when a command instance
     * is created.
     */
    init: function () {
        console.log("INIT CALLED FROM ModulesSettingsInterface");
    },
    
    /**
     * Whether or not the command is enabled for the user (will usually have extensions displayed but disabled).
     * @returns {boolean}
     */
    isEnabled: function (selection) {
        return true;
    },

    /**
     * Whether or not the command is available to the user.This impacts the context menu option but not the
     ribbon bar.
     * @returns {boolean}
     */
    isAvailable: function (selection) {
        return true;
    },

    /**
     * Executes your command. You can use _execute or execute as the property name.
     */
    execute: function (selection) {
        // Sets the url of a popup window, passing through params for the ID and Title of the selected item
        var url = "${ViewsUrl}ModulesSettingsInterface.aspx";
        // Creates a popup with the above URL
        var popup = $popup.create(url, "menubar=no,location=no,resizable=no,scrollbars=no,status=no,width=700,height=450,top=10,left=10", null);
        popup.open();
    }
});