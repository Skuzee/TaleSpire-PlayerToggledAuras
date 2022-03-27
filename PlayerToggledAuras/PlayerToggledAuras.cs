using BepInEx;
using BepInEx.Configuration;

using UnityEngine;

/* TODO
 *   Clear all Auras button?
 *   Add a submenu button for each aura added; for quick removal.
 *   update RadialUI code to newer format?
 *   Do all dependencies of depenencies need to be listed?
 *   Where do you put extra assests in your github? CMP uses radial menu images in plugins/CustomData/Image/Icons/ but those files are not in the github.
 *   
 *   Changelog:
 *   v1.0.1
 *      FIXED BUG: Entering an aura while no creature selected makes a massive aura that covers the whole map. ConfirmCreatureSelected() function
 *      Added RadialUI menu and config option to enable/disable it.
 *   v1.0.0
 *      init commit
 */


namespace Angst
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(LordAshes.FileAccessPlugin.Guid)]
    [BepInDependency(LordAshes.AssetDataPlugin.Guid)]
    [BepInDependency(RadialUI.RadialUIPlugin.Guid)]
    public partial class PlayerToggledAuras : BaseUnityPlugin
    {
        // Plugin info
        public const string Name = "PlayerToggledAuras Plug-In";                      // Update plugin name (and give the same name under Project | Properties)
        public const string Guid = "org.angst.plugins.playertoggledauras";       // Update user name and plugin id (usually changing 'template' to name of plugin)
        public const string Version = "1.0.1";                            // Update version as appropriate (and use same version under Project | Properties | Assembly Information)

        // Configuration
        private ConfigEntry<KeyboardShortcut> AddAuraKey { get; set; }      // Add Aura keybind
        private ConfigEntry<KeyboardShortcut> RemAuraKey { get; set; }      // Remove Aura keybind
        private ConfigEntry<bool> RadialMenuEnable { get; set; }            // Enables Radial Menu Option
        // Reference to the TaleSpire_CustomData folder used by a lot a plugins.
        // This reference is usually no longer needed if you are using the FileAccessPlugin (which is highly recommended) 
        private string dir = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("/")) + "/TaleSpire_CustomData/";

        void Awake()
        {
            // Not required but good idea to log this state for troubleshooting purpose
            UnityEngine.Debug.Log("PlayerToggledAuras Plugin: Angst PlayerToggledAuras Plugin Is Active.");

            // Read Configruation:
            AddAuraKey = Config.Bind("Hotkeys", "Add Aura Keybind", new KeyboardShortcut(KeyCode.Alpha7, KeyCode.RightControl));
            RemAuraKey = Config.Bind("Hotkeys", "Remove Aura Keybind", new KeyboardShortcut(KeyCode.Alpha8, KeyCode.RightControl));
            RadialMenuEnable = Config.Bind("Setting", "Radial Menu Buttons", true);
            //
            // (Optional) Register A Radial Main
            //

            // Ensures that the indicated menu exist at the root of the character menu
            // (This allows multiple plugins to use the same root level radial menu)

            if (RadialMenuEnable.Value)
            {
                UnityEngine.Debug.Log("PlayerToggledAuras Plugin: Radial Menu Buttons are Enabled.");

                /*
                RadialUI.RadialUIPlugin.AddCustomButtonOnCharacter(RadialUI.RadialUIPlugin.Guid + ".test", new MapMenu.ItemArgs()
                {
                    Action = MenuHandlerTest,
                    CloseMenuOnActivate = true,
                    FadeName = true,
                    Icon = LordAshes.FileAccessPlugin.Image.LoadSprite("Images/Icons/Aura.png"),
                    Title = "test"
                }
                , (a, b) => { return true; });
                */
             
                RadialUI.RadialSubmenu.EnsureMainMenuItem(RadialUI.RadialUIPlugin.Guid + ".Auras",
                                                            RadialUI.RadialSubmenu.MenuType.character, "Auras",
                                                            LordAshes.FileAccessPlugin.Image.LoadSprite("Images/Icons/Aura.png")
                                                            );
                
               // Register a radial sub-menu with a resulting action
               RadialUI.RadialSubmenu.CreateSubMenuItem(RadialUI.RadialUIPlugin.Guid + ".Auras",
                                                            "Add",
                                                             LordAshes.FileAccessPlugin.Image.LoadSprite("Images/Icons/Plus.png"),
                                                            (cid, menu, mmi) => { AddAura(cid); },
                                                            true,
                                                            null
                                                            );

                // Register a radial sub-menu with a resulting action
                RadialUI.RadialSubmenu.CreateSubMenuItem(RadialUI.RadialUIPlugin.Guid + ".Auras",
                                                            "Remove",
                                                             LordAshes.FileAccessPlugin.Image.LoadSprite("Images/Icons/Cancel.png"),
                                                            (cid, menu, mmi) => { RemAura(cid); },
                                                            true,
                                                            null
                                                            );  
            } else
            {
                UnityEngine.Debug.Log("PlayerToggledAuras Plugin: Radial Menu Buttons are Disabled.");
            }
            // Post plugin on the TaleSpire main page
            // Utility.PostOnMainPage(this.GetType());  
        }

        void Update()
        {

            if (Utility.isBoardLoaded())
            {

                if (Utility.StrictKeyCheck(AddAuraKey.Value))
                {
                    if (ConfirmCreatureSelected())
                    {
                        AddAura(LocalClient.SelectedCreatureId);
                        return;
                    }
                    UnityEngine.Debug.LogWarning("PlayerToggledAuras Plugin: No Creature Selected?");

                }

                if (Utility.StrictKeyCheck(RemAuraKey.Value))
                {
                    if (ConfirmCreatureSelected())
                    {
                        RemAura(LocalClient.SelectedCreatureId);
                        return;
                    }
                    UnityEngine.Debug.LogWarning("PlayerToggledAuras Plugin: No Creature Selected?");
                }
            }
        }
        /*
        private void MenuHandlerTest(MapMenuItem arg1, object arg2)
        {

        }
        */
    }
}
