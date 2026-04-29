# Farmland Drops Soil (Community Maintained)

**Original Author:** [Copygirl](https://github.com/copygirl/FarmlandDropsSoil)

**Original License:** UNLICENSE

## Purpose
This repository is a fork of the original [FarmlandDropsSoil](https://github.com/copygirl/FarmlandDropsSoil) mod from 2021 and was incompatible with Vintage Story 1.22+ due to API changes.

## What this mod does
- Farmland blocks have a chance to return the soil when broken as opposed to being destroyed.
- Drop chance is calculated based on the farmland's nutrient levels (N, P, K).
- If nutrients are above 95%, soil is guaranteed to drop.
- If nutrients are lower, the drop chance scales down proportionally.
- Compatible with **Vintage Story 1.22.0**

## Changes from original
- Updated `GetDrops` method signature to match VS 1.22 API (no functional changes, just framework update).
- Migrated project to target `.NET 10` (net10.0).
- Fixed JSON patch syntax for modern VS versions.
- Added some information `modinfo.json`.

## Installation
1. Download the latest release from the [Releases tab](https://github.com/Danweel/FarmlandDropsSoil/releases).
2. Extract the `.zip` file into your `Mods` directory:
   - **Linux:** `~/.config/VintagestoryData/Mods/`
   - **Windows:** `%appdata%\VintagestoryData\Mods\`
3. Ensure you are running Vintage Story **1.22.0** or higher.
4. Launch the game and enable the mod in the Mods menu.

## Testing
To test the mod:
1. Create a **Survival** world.
2. Till some dirt to create farmland.
3. Farm the land repeatedly to deplete nutrients.
4. Break the farmland and observe the soil drops.
5. Compare drop rates at different nutrient levels.

## Contributing
This is a community-maintained project. Contributions are welcome!
- Please open an issue before submitting a PR.
- Test reasonably before submitting.

## License
This project is released under the **UNLICENSE**, in line with the original.
See the `UNLICENSE` file for details.

## Acknowledgments
- **Copygirl** for the original mod and code.

---
*Last updated: April 2026*
