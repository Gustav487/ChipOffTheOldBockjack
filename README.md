# CSC-289-0001-Group-4-Poker-Game-Chips-Master

Created as a college group project, "Chip Off The Old Blockjack" is an offline blackjack game with a focus on customization in the form of swappable cosmetics and altered gameplay mechanics, as well as on tracking progress in the form of metrics and achievements.

# Contribution
Before contributing to the project please read the [general contribution guidelines](./docs/contribution_guidelines.md).

If you're interested in designing or working with swappable assets please read the [assets guidelines](./docs/asset_guidelines.md)

# Installation
## C#
The non-engine side of **Chip Off The Old Blockjack** is written in C#. To contribute, open the main solution file [/src/ChipMasters.sln](./src/ChipMasters.sln) in an editor of your choosing. 

Note: The project contains two solutions, [one](./src/ChipMasters/ChipMasters.sln) is strictly for the godot application, the [other](./src/ChipMasters.sln) is for the entire C# project including the automated tests.

## Godot
**Chip Off The Old Blockjack** is devloped using the [.NET version of Godot 4.3](https://godotengine.org/download/archive/4.3-stable/). After installing Godot open the project file to begin: [./src/ChipMassters/project.godot](./src/ChipMasters/project.godot)

# Exporting
To create a portable executable file for the game you'll need to export the project from the Godot editor. Inside of the editor select "Project" then "Export", in the menu that pops up select one of the export configurations, or add a new one, then select "Export Project". You may be prompted to install templates, following the instructions to install them should resolve this. Be sure the build output directory is already present, the build may fail if the directory is missing. Be sure that you've built the C# project at least once or the resulting executable may fail to execute. 

## Build directorys
Two build options currently exist, Windows x86_64 and Linux x86_64, the executable output paths are as follows:
-/build/win_x86_64/ChipOffTheOldBlockjack.exe
-/builds/linux_x86_64/ChipOffTheOldBlockjack.x86_64
