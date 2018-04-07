# What is it?
This is a simple Singleton implementation for Unity Game Engine. This sample using game managers.
You can use this scripts to make your own game architecture.

You can learn more about this here (Warning! Russian Language!):
https://habrahabr.ru/post/341830/

# Get Started
So, you can use this scripts very simple. You need create prefabs with Managers and put it in your initialization object with GameLoader script.

## Usage Steps
- Create 4 empty objects with names: GameManager, AudioManager, NetworkManager and LanguageManager
- Put scripts to created objects from /Assets Unity/Scripts/Core/
- Save this objects as prefabs
- Remove this objects from scene
- Create new empty object with name "GameLoader" and put "GameLoader" script inside this object.
- Put into script params on this object instances of our Managers Prefabs

Done! You can use it. GameLoader initialize all managers automatically and save state between scenes.

## Example
You can find an example script "UsageExample.cs" in project files.

## Bonus
This scripts contain some examples with evented functions of managers.

# Support
You can get support here:
https://cdbits.net/

Or here:
https://vk.com/cdbits

Or here:
help@cdbits.net

# Developer info
This scripts created by Ilya Rastorguev from CodeBits Team.
Warning! This scripts written just for example. Don't use it at production!