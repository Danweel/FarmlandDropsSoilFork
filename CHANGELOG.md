# Changelog

Notable changes to this project:

## [1.22.0] - 2026-04-27
### Added
- Initial community-maintained fork for 1.22.0+.
- Updated ,csproj to target net10.0 instead of net461.
- Added `README.md` and `CHANGELOG.md`, mostly for practice.
- Added build.sh packaging script.
- ConfigLib support, with fallback to the hardcoded default.
- Some unlikely failure handling, as an excercise.

### Changed
- Updated `GetDrops` method signature to match VS 1.22 API.
- Fixed JSON patch syntax (quotes around keys) for modern VS versions.
- Updated and moved `modinfo.json` to root where it is now expected.

### Fixed
- Resolved build errors caused by outdated target framework (`net461` → `net10.0`).
- Added a divide by zero guard in case something unexpected happens. Probably not necessary, but good practice because a possible divide by zero just hanging out is spooky. o. o
- If the index '2' doesn't exist or doesn't contain what's expected, it'll now return nothing instead of crashing, and print an error to logs with a best-guess about the problem and a grep [FarmlandDropSoil] for searching.

## [1.4.0] - 2021 (Original)
- Original release by Copygirl.
- Implemented nutrient-based soil drop logic.
- Used `GetDrops` method for block drop control.
- Reads index 2 of 'farmland' type to determine correct block drop.