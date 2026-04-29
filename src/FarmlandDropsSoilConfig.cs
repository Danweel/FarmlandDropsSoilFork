using System;
using Vintagestory.API.Config;

namespace FarmlandDropsSoil
{
    /// <summary>
    /// Configuration for Farmland Drops Soil.
    /// Note: [ConfigItem] attributes removed to avoid compilation errors.
    /// The UI will use property names as labels.
    /// </summary>
    public class FarmlandDropsSoilConfig
    {
        /// <summary>
        /// The nutrient threshold below which drops become probabilistic.
        /// </summary>
        public double DropThresholdPercent { get; set; } = 0.95;

        /// <summary>
        /// The minimum guaranteed drop chance even if nutrients are 0%.
        /// </summary>
        public double MinDropChance { get; set; } = 0.0;

        /// <summary>
        /// If true, drops the soil block corresponding to the farmland's ORIGINAL TIER.
        /// </summary>
        public bool PreserveOriginalTier { get; set; } = false;

        /// <summary>
        /// If true, always drop soil regardless of nutrient levels.
        /// </summary>
        public bool AlwaysDrop { get; set; } = false;
    }
}

///┌─────────────────────────────────────────────────────────────┐
///│ 1. Game Starts                                              │
///│    - ConfigLib scans all mods for configlib-patches.json    │
///└─────────────────────────────────────────────────────────────┘
///                           ↓
///┌─────────────────────────────────────────────────────────────┐
///│ 2. ConfigLib Reads the JSON:                                │
///│    - "FarmlandDropsSoil.json"                               │
///│    - 4 settings with ranges/types                           │
///└─────────────────────────────────────────────────────────────┘
///                           ↓
///┌─────────────────────────────────────────────────────────────┐
///│ 3. ConfigLib Creates/Updates File                           │
///│    - Creates: ~/.config/VintagestoryData/ModConfig/         │
///│              FarmlandDropsSoil.json                         │
///│    - Populates with default values                          │
///└─────────────────────────────────────────────────────────────┘
///                           ↓
///┌─────────────────────────────────────────────────────────────┐
///│ 4. C# Code Loads Config                                     │
///│    - api.LoadModConfig FarmlandDropsSoilConfig              │
///│    - Reads the same file ConfigLib created                  │
///│    - Works whether ConfigLib is installed or not            │
///└─────────────────────────────────────────────────────────────┘