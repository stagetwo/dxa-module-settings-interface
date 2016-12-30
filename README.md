# DXA Module Settings Interface

This [Alchemy](https://www.alchemywebstore.com/) plugin allows the configuration of [SDL Web DXA](http://www.sdl.com/solution/digital-experience/web-experience-management/digital-experience-accelerator.html) modules in a single location. 

This will help DXA users (editors and site managers) to discover the configuration options they have for the installed modules.

The configuration is currently done in a pop-up window. Untimately, this will be configured in a new publication properties tab.

Further details can be found in the following blog post by Tanner Brine:
http://www.tridiondeveloper.com/dxa-modules-settings-interface

##Installation and configuration steps

1. Download the latest version of the source code

2. Create a new Bundle within your SDL Web (Tridion) CME and add the DXA module configuration components into this bundle

3. Configure the path to the bundle (created in Step 2) in the `bundlePath` element within the solution's `a4t.xml` file

4. Build the solution within Visual Studio and drag the `.a4t` file onto your CME's Alchemy installation screen

5. Within the CME, right click on a publication and click on the new *Module Setting Interface* option within the context menu to see the DXA module configuration settings 