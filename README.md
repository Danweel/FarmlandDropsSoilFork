# Farmland Drops Soil (Fork)

**Original Author:** [Copygirl](https://github.com/copygirl/FarmlandDropsSoil)
**Original License:** UNLICENSE (please keep the licence as such)

## Purpose
This repository is a fork of the original [FarmlandDropsSoil](https://github.com/copygirl/FarmlandDropsSoil) and was incompatible with Vintage Story 1.22+ due to API changes.

## What this mod does
- Farmland blocks have a chance to return the soil when broken as opposed to being destroyed.
- Drop chance is calculated based on the farmland's nutrient levels (N, P, K).
- If nutrients are above 95%, soil is guaranteed to drop.
- If nutrients are lower, the drop chance scales down proportionally.
- Compatible with **Vintage Story 1.22.0 to 1.22.7**
- *Should* be compatible with **1.21.0+**

## Changes from original
- Updated `GetDrops` method signature to match VS 1.22 API (no functional changes, just framework update).
- Migrated project to target `.NET 10` / net10.0.
- Fixed JSON patch syntax for modern VS versions.
- Added some information to `modinfo.json`.
- Changed README
- Added CONTRIBUTING

## Installation
1. Download the latest release from the [Releases tab](https://github.com/Danweel/FarmlandDropsSoil/releases).
2. Extract the `.zip` file into your `Mods` directory:
   - **Linux:** `~/.config/VintagestoryData/Mods/`
   - **Windows:** `%appdata%\VintagestoryData\Mods\`
3. Ensure you are running Vintage Story **1.21.0** or higher.
4. Launch the game and enable the mod in the Mods menu. (Probably already selected.)

## Compatibility

This mod should work with Vintage Story 1.21.0 through 1.22.7. I've tested it on 1.22.2 and 1.22.3 specifically. Between these versions, the farmland API hasn't changed significantly—the `GetDrops()` signature stayed stable, and soil block names (`soil-{tier}-none`) are still the same.

If you're running something outside that range and it breaks, let me know anyway. I haven't tested every release, so there's some assumption involved here.

### Compatibility with other farmland-drops mods

In farmland.json I've changed `"op" : "add"` to `"op" : "addmerge"` so farmland blocks won't overwrite behaviors added by other mods. However, if another mod also overrides the `GetDrops()` method (like Farmland Drops With Nutrients), you'll still get a conflict — only one mod's drop logic will run.

### Important compatibility note

This mod overrides how farmland behaves when broken, so it will conflict with other mods that do the same thing:

| Mod | Conflict? |
|-----|-----------|
| Farmland Drops With Nutrients | Yes — pick one |
| Any mod modifying farmland GetDrops | Probably — watch for issues |

**Compatibility Lib** - Can help resolve some conflicts, check this out if you're finding small issues.

If you want nutrient-preserving drops, use **Farmland Drops With Nutrients**. If you want simpler soil-only drops, use this mod, but don't run both at the same time.

## Testing

To see if it works:

1. Create a Survival world
2. Till some dirt to make farmland
3. Farm it repeatedly until nutrients drain (optional)
4. Break the farmland and check for soil drops (keeping in mind the drop rate is related to nutrient drain)
5. Compare drop rates at different nutrient levels (optional)

If soil never drops even at full nutrients, something's wrong; check your logs for `[FarmlandDropsSoil]` messages.

## Reporting Issues

If something breaks:

1. Check your logs for `[FarmlandDropsSoil]` warnings.
2. Verify no conflicting farmland-drop mods are enabled.
3. Ideally, open an issue on GitHub with your game version, mod list, and relevant log entries.

I can't debug without enough information, so please include logs.

## Contributing
This is a community-maintained project. Contributions or takeovers are welcome.

- Please open an issue before submitting a PR.
- Test reasonably before submitting.
- Document any new config options.
- Fork if you want! Submit upstream if you can.

## Changelog

### 1.22.7
- Updated version number to reflect 1.22.7 compatibility, updated site numbers.
- Investigated and broadened supported version range to 1.21.0–1.22.
- Updated a few documenatation things, conflict notes.

### 1.22.0
- Updated GetDrops signature for VS 1.22 API (the main issue)
- Added ConfigLib support (a small upgrade to the original)
- Migrated to .NET 10 (a secondary issue)

### Original (Copygirl, 2021)
- Made farmland drop soil based on nutrient levels

---
*Last updated: August 2026*
