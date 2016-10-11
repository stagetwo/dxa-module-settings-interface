<%@ Page Language="C#" Inherits="Tridion.Web.UI.Editors.CME.Views.Page" %>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Modules Settings Interface</title>
        <cc:tridionmanager runat="server" editor="CME">
            <dependencies runat="server"> 
                <dependency runat="server">Tridion.Web.UI.Editors.CME</dependency>
                <dependency runat="server">Tridion.Web.UI.Editors.CME.commands</dependency>
                <dependency runat="server">Alchemy.Resources.Libs.Jquery</dependency>
                <dependency runat="server">Alchemy.Plugins.${PluginName}.Resources.ModulesSettingsInterfacePopupResourceGroup</dependency>
            </dependencies>
        </cc:tridionmanager>
		<link rel='shortcut icon' type='image/x-icon' href='${ImgUrl}favicon.png' />
    </head>
    <body>
    </body>
</html>