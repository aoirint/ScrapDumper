using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace ScrapDumper;

[BepInPlugin("com.aoirint.scrapdumper", "Scrap Dumper", "0.1.0")]
[BepInProcess("Lethal Company.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo("Plugin com.aoirint.scrapdumper is loaded!");

        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(StartOfRound), "Awake")]
    [HarmonyPostfix]
    static void StartOfRoundAwakePostfix() {
        var allItemsList = StartOfRound.Instance.allItemsList;

        var itemsList = allItemsList.itemsList;
        foreach (var item in itemsList) {
            Logger.LogInfo($"{item.itemName},{item.creditsWorth},{item.minValue},{item.maxValue},{item.weight}");
        }
    }

    [HarmonyPatch(typeof(RoundManager), "Awake")]
    [HarmonyPostfix]
    static void RoundManagerAwakePostfix() {
        var scrapValueMultiplier = RoundManager.Instance.scrapValueMultiplier;
        Logger.LogInfo($"scrapValueMultiplier: {scrapValueMultiplier}");
    }
}
