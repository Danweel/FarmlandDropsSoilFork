using System;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Client;

namespace FarmlandDropsSoil
{
    public class FarmlandDropsSoilSystem : ModSystem
    {
        public static FarmlandDropsSoilConfig Config { get; private set; }

        public override void Start(ICoreAPI api)
        {
            // Load config from ModConfig/FarmlandDropsSoil.json
            // If ConfigLib is installed, it will read configlib-patches.json and edit our FarmlandDropsSoil.json.
            // If ConfigLib is NOT installed, fallback to defaults.
            try
            {
                Config = api.LoadModConfig<FarmlandDropsSoilConfig>("FarmlandDropsSoil");
            }
            catch (Exception ex)
            {
                api.Logger.Warning("[FarmlandDropsSoil] Failed to load config: " + ex.Message);
            }

            if (Config == null)
            {
                Config = new FarmlandDropsSoilConfig();
                api.Logger.Notification("[FarmlandDropsSoil] Config not found. Using defaults (95% threshold). " +
                    "Install ConfigLib for in-game configuration.");
            }
            else
            {
                api.Logger.Notification("[FarmlandDropsSoil] Config loaded. " +
                    "Install ConfigLib for in-game configuration.");
            }

            api.RegisterBlockBehaviorClass("FarmlandDropsSoilBehavior", typeof(FarmlandDropsSoilBehavior));
        }
    }
}
