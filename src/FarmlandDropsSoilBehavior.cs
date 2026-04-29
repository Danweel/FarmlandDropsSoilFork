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
			// 1. Creative Mode Check
			if ((byPlayer?.WorldData.CurrentGameMode == EnumGameMode.Creative) ||
			    (world.BlockAccessor.GetBlockEntity(pos) is not BlockEntityFarmland farmland))
				return new ItemStack[0];

			handling = EnumHandling.PreventSubsequent;

			// 2. Calculate Nutrient Ratio (with a divide by zero safety)
			// If OriginalFertility is 0 (corruption?), treat ratio as 0 to avoid crash.
			var nutrients = farmland.Nutrients.Zip(farmland.OriginalFertility,
				(current, original) => original > 0 ? current / original : 0).Min();

			// 3. Apply Drop Logic
			// If nutrients are below 95%, chance to drop is equal to the nutrient ratio.
			if ((nutrients < 0.95) && (world.Rand.NextDouble() > nutrients))
				return new ItemStack[0];

			// 4. Determine Soil Tier
			// Uses Index 2 (FirstCodePart(2)) which corresponds to the quality tier
			// in the current naming convention (e.g., "medium" in "farmland-[wetness]-medium").
			var fertility = this.block.FirstCodePart(2);

			// Construct the target soil block code (e.g., "soil-medium-none").
			// This is actually index 1, but we don't need to refer to it (in 0 indexing).
			var block = world.GetBlock(new AssetLocation("game", $"soil-{fertility}-none"));

			// 5. Safety Check: If the block code doesn't exist (e.g., naming overhaul or something), return nothing.
			// This prevents a crash if the lookup fails.
			if (block == null)
			{
				world.Logger.Warning("[FarmlandDropsSoil] Could not find soil block 'soil-{0}-none' for farmland tier '{1}'." +
					"This may indicate a block naming change or a mod conflict.",
					fertility, this.block.Code);
				return new ItemStack[0];
			}

			return new ItemStack[] { new(block) };
		}
	}
}