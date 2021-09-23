# Description
A proof of concept for abilities that are decoupled from the character using the ability.

# Usage
The following steps should be taken when creating a new ability:
1. Add a new script to the Scripts/Abilities directory that extends `IAbility`
2. Update the enumeration in Scripts/Ability.cs to include the new ability
3. Update the factory in Scripts/AbilityFactory.cs to create the new ability added in step #1

The new ability can now be added to any character that has the `CharacterBehaviour` component.

# Demo
A demo scene has been provided in the /Scenes/ directory.  
* To run the demo, load the scene and press play
* Click any button to begin targeting for the ability
* Select a valid target and then left click to cast the ability
* You should be able to add or remove abilities by clicking on the player and modifying the abilities in the inspector
  * This can also be done during play

:warning: There is a known issue with the UI when making code changes while playing.  To resolve, exit play mode and reenter.  There are currently no plans to resolve this issue because this is not a UI demo.

# About the Design
This proof of concept attempts to adhere to [coding best practices](https://en.wikipedia.org/wiki/Coding_best_practices).  To this end, the following has been considered:
* The characters are not concerned with how to cast an ability, that is the job of the ability itself
* The characters are not concerned with if the ability successfully cast, they simply announce that they wish to cast an ability
* The ability manager will listen for any invocations of an ability and cast them accordingly
  * This means an ability can be cast from sources other than a character by adding a new listener 
* Abilities are contained to their own files to maximize simplicity, readability, and maintainability

# Licence
There is no licence for any assets in this repository.  Feel free to modify and/or ship the assets as-is, with or without credit, in personal or commercial products.