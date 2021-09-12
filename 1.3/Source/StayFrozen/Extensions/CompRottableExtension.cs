using RimWorld;
using System.Collections.Generic;

namespace StayFrozen.Extensions
{
    public static class CompRottableExtension
    {
        private static Dictionary<CompRottable, float> tempDict = new Dictionary<CompRottable, float>();

        public static float GetTemperature(this CompRottable comp)
        {
            if (!tempDict.ContainsKey(comp))
            {
                tempDict.Add(comp, comp.parent.AmbientTemperature);
            }
            return tempDict[comp];
        }

        public static void SetTemperature(this CompRottable comp, float temp)
        {
            tempDict[comp] = temp;
        }
    }
}
