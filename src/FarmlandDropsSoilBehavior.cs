using System;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace FarmlandDropsSoil
{
	public class FarmlandDropsSoilBehavior : BlockBehavior
	{
		public FarmlandDropsSoilBehavior(Block block)
			: base(block) {  }

		public override ItemStack[] GetDrops(
			IWorldAccessor world, BlockPos pos, IPlayer byPlayer,
			ref float dropChanceMultiplier, ref EnumHandling handling)
		{
			// 1. Creative Mode Check (I don't think this is necessary, but just in case)
			if ((byPlayer?.WorldData.CurrentGameMode == EnumGameMode.Creative) ||
			    (world.BlockAccessor.GetBlockEntity(pos) is not BlockEntityFarmland farmland))
				return new ItemStack[0];

			handling = EnumHandling.PreventSubsequent;

			// 2. Get Config - Prevents crash to NullReferenceException if the config fails to load.
			var config = FarmlandDropsSoilSystem.Config;
			if (config == null)
			{
			config = new FarmlandDropsSoilConfig(); // Fallback to defaults
			world.Logger.Warning("[FarmlandDropsSoil] Config was null at runtime. Falling back to default settings.");
			}

			// 3. 'Always Drop' option
			if (config.AlwaysDrop)
			{
				string tier = this.block.FirstCodePart(2);
				var soilBlock = world.GetBlock(new AssetLocation("game", "soil-" + tier + "-none"));
				if (soilBlock != null)
				{
					return new ItemStack[] { new ItemStack(soilBlock) };
				}
				return new ItemStack[0];
			}

			// 4. Calculate Nutrient Ratio (with division-by-zero protection)
			var nutrients = farmland.Nutrients.Zip(farmland.OriginalFertility,
				(current, original) => original > 0 ? current / original : 0).Min();

			// 5. Apply configurable threshold
			if (nutrients < config.DropThresholdPercent)
			{
				// Calculate effective chance: (nutrients - min) / (threshold - min)
				double chance = 0;
				if (config.DropThresholdPercent > config.MinDropChance)
				{
					chance = (nutrients - config.MinDropChance) / (config.DropThresholdPercent - config.MinDropChance);
				}

				// Clamp chance between 0 and 1
				chance = Math.Max(0, Math.Min(1, chance));

				// RNG time
				if (world.Rand.NextDouble() > chance)
					return new ItemStack[0];

		}
			// 6. Determine soil tier & PreserveOriginalTier logic
			string fertilityTier;

			if (config.PreserveOriginalTier)
			{
				// Map OriginalFertility (number) to Tier Name (string)
				// Values based on VS wiki: Terra=80, High=65, Med=50, Low=25, Barren=5 (Not that Barren and Bony can be farmland but...)
				double original = farmland.OriginalFertility.Max();

				if (original >= 75) fertilityTier = "terra";
				else if (original >= 60) fertilityTier = "high";
				else if (original >= 40) fertilityTier = "medium";
				else if (original >= 15) fertilityTier = "low";
				else if (original > 0) fertilityTier = "barren";
				else fertilityTier = "bony";
			}
			else
			{
				// Use the current block's tier (from the code)
				fertilityTier = this.block.FirstCodePart(2);
			}

			// Construct the target soil block code
			var soilBlock2 = world.GetBlock(new AssetLocation("game", "soil-" + fertilityTier + "-none"));

			// 7. Safety Check
			if (soilBlock2 == null)
			{
				world.Logger.Warning("[FarmlandDropsSoil] Could not find soil block 'soil-{0}-none' for farmland tier '{1}'. " +
					"This may indicate a block naming change or a mod conflict.",
					fertilityTier, this.block.Code);
				return new ItemStack[0];
			}

			return new ItemStack[] { new ItemStack(soilBlock2) };
		}
	}
}