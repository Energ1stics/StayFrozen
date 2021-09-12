using Verse;
using HarmonyLib;

namespace StayFrozen
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("energistics.itemtemperature");
            harmony.PatchAll();
        }
    }
}
