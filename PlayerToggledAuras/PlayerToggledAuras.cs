using BepInEx;
using BepInEx.Configuration;

using UnityEngine;

namespace Angst
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(LordAshes.FileAccessPlugin.Guid)]
    [BepInDependency(LordAshes.AssetDataPlugin.Guid)]
    //[BepInDependency(RadialUI.RadialUIPlugin.Guid)]
    public partial class PlayerToggledAuras : BaseUnityPlugin
    {
        // Plugin info
        public const string Name = "PlayerToggledAuras Plug-In";                      // Update plugin name (and give the same name under Project | Properties)
        public const string Guid = "org.angst.plugins.playertoggledauras";       // Update user name and plugin id (usually changing 'template' to name of plugin)
        public const string Version = "1.0.0";                            // Update version as appropriate (and use same version under Project | Properties | Assembly Information)

        // Configuration
        private ConfigEntry<KeyboardShortcut> triggerKey { get; set; }      // Sample configuration for triggering a plugin via keyboard

        // Reference to the TaleSpire_CustomData folder used by a lot a plugins.
        // This reference is usually no longer needed if you are using the FileAccessPlugin (which is highly recommended) 
        private string dir = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("/")) + "/TaleSpire_CustomData/";

        void Awake()
        {
            // Not required but good idea to log this state for troubleshooting purpose
            UnityEngine.Debug.Log("PlayerToggledAuras Plugin: Angst PlayerToggledAuras Plugin Is Active.");

            // Read Configruation:
            triggerKey = Config.Bind("Hotkeys", "States Activation", new KeyboardShortcut(KeyCode.Alpha7, KeyCode.RightControl));

            //
            // (Optional) Register A Radial Main
            //

            // Ensures that the indicated menu exist at the root of the character menu
            // (This allows multiple plugins to use the same root level radial menu)
            RadialUI.RadialSubmenu.EnsureMainMenuItem(  RadialUI.RadialUIPlugin.Guid + ".Auras",
                                                        RadialUI.RadialSubmenu.MenuType.character, "Auras",
                                                        LordAshes.FileAccessPlugin.Image.LoadSprite("Images/Icons/Aura.png")
                                                        );

            // Register a radial sub-menu with a resulting action
            RadialUI.RadialSubmenu.CreateSubMenuItem(   RadialUI.RadialUIPlugin.Guid + ".Auras",
                                                        "Toggle",
                                                         LordAshes.FileAccessPlugin.Image.LoadSprite("Images/Icons/Plus.png"),
                                                        (cid, menu, mmi) => { SetRequest(cid); },
                                                        false,
                                                        null
                                                        );

            // Register a radial sub-menu with a resulting action
            RadialUI.RadialSubmenu.CreateSubMenuItem(RadialUI.RadialUIPlugin.Guid + ".Auras",
                                                        "Clear All",
                                                         LordAshes.FileAccessPlugin.Image.LoadSprite("Images/Icons/Cancel.png"),
                                                        (cid, menu, mmi) => { SetRequest(cid); },
                                                        false,
                                                        null
                                                        );

            // Post plugin on the TaleSpire main page
            // Utility.PostOnMainPage(this.GetType());  
        }


        void Update()
        {

            if (Utility.isBoardLoaded())
            {

                if (Utility.StrictKeyCheck(triggerKey.Value))
                {
                    SystemMessage.AskForTextInput("Player Toggled Auras", "Enter the name of the aura to toggle.", "OK",
                                                    (auraName) =>
                                                    {
                                                        // Need a way to toggle the aura, or cancel chosen, or cancel all.
                                                        // Consider config option for radialUI menu button to open prompt.
                                                        // Do all dependencies need to be listed?
                                                        // 
                                                        LordAshes.AssetDataPlugin.SetInfo(LocalClient.SelectedCreatureId.ToString(), 
                                                        "org.lordashes.plugins.extraassetsregistration.Aura." + auraName,
                                                        LordAshes.ExtraAssetsRegistrationPlugin.AssetHandler.FindAssetId(auraName).ToString());
                                                    }, null,
                                                    "Cancel", () =>
                                                    {
                                                        // LordAshes.AssetDataPlugin.ClearInfo(LocalClient.SelectedCreatureId.ToString(),
                                                        // "org.lordashes.plugins.extraassetsregistration.Aura.");
                                                    },
                                                    "");
                }
            }
        }
    }
}
