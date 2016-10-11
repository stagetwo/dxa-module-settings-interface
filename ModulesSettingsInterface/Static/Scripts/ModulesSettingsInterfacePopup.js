/**
 * Handles all functionality of my popup window, including retrieving the where used and using functionality
 * and manipulating the tabs.
 *
 * Note the self executing function wrapping the JS. This is to limit the scope of my variables and avoid
 * conflicts with other scripts.
 */
!(function () {
    // Alchemy comes with jQuery and several other libraries already pre-installed so assigning
    // it a variable here eliminates the redundancy of loading my own copy, and avoids any conflicts over
    // the $ character
    $j = Alchemy.library("jQuery");
    var qwest = Alchemy.library("qwest");

    Alchemy.Plugins["${PluginName}"].Api.getSettings().success(function (success) {
        qwest.get('/Alchemy/Plugins/Modules_Settings/api/ModulesSettingsService/RetrieveSettings?path=' + success.bundlePath).success(function (success) {
            console.log(success)
        })
    })
})();