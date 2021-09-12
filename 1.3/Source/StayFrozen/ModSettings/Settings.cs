using Verse;

namespace StayFrozen.ModSettings
{
    public class Settings : Verse.ModSettings
    {
        public static int timeToBalance = 40000;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref timeToBalance, nameof(timeToBalance), 40000);
            base.ExposeData();
        }
    }
}
