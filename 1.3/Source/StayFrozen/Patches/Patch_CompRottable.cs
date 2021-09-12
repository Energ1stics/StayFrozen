using HarmonyLib;
using RimWorld;
using StayFrozen.Extensions;
using StayFrozen.Formulas;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace StayFrozen
{
    [HarmonyPatch(typeof(CompRottable))]
    static class Patch_CompRottable
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CompRottable.TicksUntilRotAtCurrentTemp), MethodType.Getter)]
        private static IEnumerable<CodeInstruction> Transpiler_TicksUntilRotAtCurrentTemp(IEnumerable<CodeInstruction> instructions)
        {
            IEnumerable<CodeInstruction> codeInstructions = SwapTempAccesses(instructions);
            return codeInstructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch("Tick")]
        private static IEnumerable<CodeInstruction> Transpiler_Tick(IEnumerable<CodeInstruction> instructions)
        {
            IEnumerable<CodeInstruction> codeInstructions = SwapTempAccesses(instructions);
            return codeInstructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CompRottable.CompInspectStringExtra))]
        private static IEnumerable<CodeInstruction> Transpiler_CompInsprectStringExtra(IEnumerable<CodeInstruction> instructions)
        {
            IEnumerable<CodeInstruction> codeInstructions = SwapTempAccesses(instructions);
            return codeInstructions;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompRottable.CompInspectStringExtra))]
        private static void Postfix_CompInspectStringExtra(CompRottable __instance, ref string __result)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(__result);
            stringBuilder.AppendLine();
            stringBuilder.Append("CurrentItemTemperature".Translate(__instance.GetTemperature().ToStringTemperature("F0")));
            __result = stringBuilder.ToString();
        }

        [HarmonyPrefix]
        [HarmonyPatch("Tick")]
        private static void Prefix_Tick(CompRottable __instance, int interval)
        {
            if (!__instance.Active)
            {
                return;
            }
            // Update temp
            float temp = __instance.GetTemperature();
            float ambientTemp = __instance.parent.AmbientTemperature;
            temp = Physics.ThermalBalance(ambientTemp, temp, 40000f, interval);
            __instance.SetTemperature(temp);
        }

        private static IEnumerable<CodeInstruction> SwapTempAccesses(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codeInstructions = instructions.ToList();
            for (int i = 0; i < codeInstructions.Count; i++)
            {
                CodeInstruction instruction = codeInstructions[i];
                if (codeInstructions[i + 0].LoadsField(typeof(ThingComp).GetField(nameof(ThingComp.parent))) &&
                    codeInstructions[i + 1].Calls(typeof(Thing).GetProperty(nameof(Thing.AmbientTemperature)).GetMethod))
                {
                    yield return CodeInstruction.Call(typeof(CompRottableExtension), nameof(CompRottableExtension.GetTemperature));
                    i++;
                }
                else
                {
                    yield return codeInstructions[i];
                }
            }
        }
    }
}
