# Player Toggled Auras
This allows players to toggle aura effects on their controlled creatures & minis.  

This plugin does not contain any aura effects.  

## Restrictions
**A Warning about using EAR (Extra Asset Registration) and CMP (Custom Mini Plugin) simultaneously.**  
*Have you ever loaded a world and realized that all your minis did not transform into your custom minis!?  
That's because EAR can load base-less auras/minis, and CMP will not start if there are auras/minis that have no base!*  

This plugin needs EAR to properly function.  
This plugin SHOULD work with CMP also installed...  
This plugin attaches auras to existing minis (that are assumed to have a base), therefore saving a world with a mini that has an aura works fine.  

##### The Problem:
If you MUST use CMP (for instance to load OBJ/MTL assets, like converted Heroforge Minis), the saved world must not contain any base-less auras/minis on loading. CMP will not start, and your custom minis won't show up until it does.  

##### The Solution:
If you load a world that has a base-less aura/mini, CMP will not start until you remove those minis/auras. Once they've been removed, CMP will start and all your custom minis will appear. You are then free to use base-less auras/minis like normal.  

Alternatively, convert your OBJ/MTL files to assets and disable CMP entirely.  

## Install
Use R2ModMan or similar installer to install this plugin.  
Use R2ModMan to configure optional settings for the plugin.  

## Config
Hotkey Keybinds are editable.  
RadialUI menu show/hide.  

## Usage
Right click creature and select 'auras' -> 'add/remove'  
*(RadialUI menu must be enabled in cofig)*  
or  
To Add an aura, select creature and press Right Control + 7  
To Remove an aura, select creature and press Right Control + 8  

## Change Log

```
v1.0.1
	FIXED BUG: Entering an aura while no creature selected makes a massive aura that covers the whole map. ConfirmCreatureSelected() function
	Added RadialUI menu and config option to enable/disable it.
v1.0.0:
	Initial release
```