using UnityEngine;
using Verse;
using SettingsHelper;
using StayFrozen.Extensions;
using StayFrozen.Utility;
using System;

namespace StayFrozen.ModSettings
{
    public class Mod : Verse.Mod
    {
        private Settings settings;

        public Mod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            float timeToBalanceAsFloat = Settings.timeToBalance;
            listing.AddLabeledSlider("HeatBalanceTimeLabel".Translate() + ": " + Utils.ToStringDuration(Settings.timeToBalance),
                                     ref timeToBalanceAsFloat,
                                     0, 120000);
            Settings.timeToBalance = Convert.ToInt32(timeToBalanceAsFloat);
            listing.End();
            this.settings.Write();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "ItemTemperatureModName".Translate();
        }
    }
}
