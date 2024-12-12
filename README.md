# Colonisation Game Project

This is a simple project made in C# and Unity.

Unity version: 2022.3.49f1

![Gameplay](https://github.com/VodVas/Bots/blob/main/Assets/ReadMeAssets/ColonisationGif.gif)


## Description
This project is a 3D game with a flat map. The camera allows you to observe the entire level space. The following mechanics are implemented:

* Starting Units: At the beginning of the game, there are three units at the base.
* Resource Generation: Resources (of your choice) are randomly generated in different places on the level.
* Base Scanning: The base can scan the level's space for resources.
* Resource Collection: If the base has a free unit and there is an uncollected resource, it sends the unit to gather the resource.
* Unit Gathering: When a unit receives the coordinates of a resource, it physically picks up the resource and carries it back to the base.
* Resource Tracking: The base has an indicator of the number of available resources. When a unit brings a new resource, this indicator increases. The unit then awaits further instructions.
* Unit Creation: When three units of resources are brought to the base, it spends them to create a new unit, which behaves the same as existing units.
* Base Resources: Each base should have its own collection of resources.
* Flag Placement: The player can set a base's flag on the level by clicking on the base and then on any point on the plane. The flag cannot be placed outside the map, and a base cannot have more than one flag. If the base already has a flag on the level at the time of the click, it should be moved to the new location.
* Changed Base Behavior: When a base's flag is set, its behavior changes, and a new priority is established. According to this priority, the base waits for the collection of 5 units of resources and then spends them to send a free unit to the flag to build a new base. From that moment, the sent unit belongs to the new base.
* Flag Disappearance: When a new base is built, the flag disappears, and the behavior of the old base returns to creating new units.
* Unit Limitation: You cannot build a new base if you have only one unit left.

## Start point
The main entry points for the game are the following installers, which set up dependencies and bindings using Zenject:

* BaseFactoryInstaller.cs
This installer sets up the base factory for creating new bases in the game.

* FlagInstaller.cs
This installer configures the flag prefab, which players can place on the map to direct base behavior.

* PrefabInstaller.cs
This installer binds the prefabs for resources and units.

* ResourcesSpawnerInstaller.cs
This installer sets up the position provider for spawning resources on the map.

* UnitControlInstaller.cs
This installer sets up the unit speed for movement.

These installers are responsible for setting up the game's dependencies and initial configurations using the Zenject dependency injection framework.

## Used plugins
* Zenject
* DoTween
