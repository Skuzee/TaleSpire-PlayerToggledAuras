using BepInEx;

namespace Angst
{
    public partial class PlayerToggledAuras : BaseUnityPlugin
    {
        private bool ConfirmCreatureSelected()
        {
            CreatureBoardAsset asset = null;
            CreaturePresenter.TryGetAsset(LocalClient.SelectedCreatureId, out asset);
            return asset != null;
        }

        private void AddAura(CreatureGuid cid)
        {

            SystemMessage.AskForTextInput("Player Toggled Auras", "Enter the name of the aura to Add.", "OK",
                                (auraName) =>
                                {                 
                                    LordAshes.AssetDataPlugin.SetInfo(cid.ToString(),
                                    "org.lordashes.plugins.extraassetsregistration.Aura." + auraName,
                                    LordAshes.ExtraAssetsRegistrationPlugin.AssetHandler.FindAssetId(auraName).ToString());
                                }, null,
                                "Cancel", () =>
                                {
        },
                                "");
        }

        private void RemAura(CreatureGuid cid)
        {

            SystemMessage.AskForTextInput("Player Toggled Auras", "Enter the name of the aura to Remove.", "OK",
                                (auraName) =>
                                { // LocalClient.SelectedCreatureId.ToString()
                                    LordAshes.AssetDataPlugin.ClearInfo(cid.ToString(),
                                    "org.lordashes.plugins.extraassetsregistration.Aura." + auraName);
                                }, null,
                                "Cancel", () =>
                                {

                                },
                                "");
        }


        /// <summary>
        /// Handler for Radial Menu selections
        /// </summary>
        /// <param name="cid"></param>
        private void SetRequest(CreatureGuid cid)
        {
        }
    }
}
