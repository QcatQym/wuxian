using HarmonyLib;

namespace wuxian
{
  public static class WirelessAutomationPatches
  {

    [HarmonyPatch(typeof(Game))]
    [HarmonyPatch("OnPrefabInit")]
    public static class Game_OnPrefabInit_Patch
    {
      public static void Postfix(PauseScreen __instance)
      {
        WirelessAutomationManager.ResetEmittersList();
        WirelessAutomationManager.ResetReceiversList();
      }
    }

    [HarmonyPatch(typeof(Game))]
    [HarmonyPatch("OnLoadLevel")]
    public static class Game_OnLoadLevel_Patch
    {
      public static void Postfix(PauseScreen __instance)
      {
        WirelessAutomationManager.ResetEmittersList();
        WirelessAutomationManager.ResetReceiversList();
      }
    }

    [HarmonyPatch(typeof(GeneratedBuildings))]
    [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
    public class GeneratedBuildings_LoadGeneratedBuildings_Patch
    {
      public static void Prefix()
      {
        Utils.AddBuildingStrings(WirelessSignaTemEmitterConfig.Id, WirelessSignaTemEmitterConfig.DisplayName, "wireless LED ", "wireless LED ");
        Utils.AddBuildingStrings(WirelessSignalPressureEmitterConfig.Id, WirelessSignalPressureEmitterConfig.DisplayName, "wireless LED ", "wireless LED ");

        Utils.AddBuildingStrings(WirelessSignalReceiver100Config.Id, WirelessSignalReceiver100Config.DisplayName, "wireless LED ", "wireless LED ");
        Utils.AddBuildingStrings(WirelessSignalReceiver10Config.Id, WirelessSignalReceiver10Config.DisplayName, "wireless LED ", "wireless LED ");
        Utils.AddBuildingStrings(WirelessSignalReceiver1Config.Id, WirelessSignalReceiver1Config.DisplayName, "wireless LED ", "wireless LED ");
        Utils.AddBuildingStrings(WirelessSignalReceiver1000Config.Id, WirelessSignalReceiver1000Config.DisplayName, "wireless LED ", "wireless LED ");
        Utils.AddBuildingStrings(WirelessSignalReceiver10000Config.Id, WirelessSignalReceiver10000Config.DisplayName, "wireless LED ", "wireless LED ");

        Utils.AddBuildingToPlanScreen("WirelessSignalReceiver1", "Automation");
        Utils.AddBuildingToPlanScreen("WirelessSignalReceiver10", "Automation");
        Utils.AddBuildingToPlanScreen("WirelessSignalReceiver100", "Automation");
        Utils.AddBuildingToPlanScreen("WirelessSignalReceiver1000", "Automation");
        Utils.AddBuildingToPlanScreen("WirelessSignalReceiver10000", "Automation");
        Utils.AddBuildingToPlanScreen("WirelessSignalTempertureEmitter", "Automation");
        Utils.AddBuildingToPlanScreen("WirelessSignalPressureEmitter", "Automation");

      }

    }

  }
}
