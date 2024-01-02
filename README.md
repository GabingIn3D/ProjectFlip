# ProjectFlip
This is the project repository for Flip

For first-timers, the game will only properly save screenshots once the user loads up the Title Screen scene for the first time. A script called 'FolderManagement.cs' will run and create the necessary AppData/LocalLow/KIMMIE save directory to send the screenshots to, and a message will appear in the on-screen debug whether or not this has properly occurred, or will indicate they already exist. 

The scene "Kimmie's House" is the one to playtest from once the aforementioned directories are created. At this point there should not be any errors pertaining to screenshots.

Notes: The buttons to navigate the in-game phone are a work-in-progress, as you may notice some menus require clicking buttons with the mouse cursor while others will use the arrow keys, Enter button or Left/Right Click for the Left and Right Phone buttons. Sometimes the left and right click will conflict with the phone buttons so unexpected behaviour may occur on some menu screens. It's something I'm working on.

Regarding Controls: The game is intended to use interchangeable Gamepad/Keyboard + Mouse using Unity's InputSystem. While a bit disorganized, some inputs are still coded using InputManager (as they have not been changed yet/the person who coded them was unfamiliar with InputSystem), and the majority of the inputs are spread across two, potentially three InputActionMaps:

Assets/InputSystemFirstPersonCharacterScripts/InputSystemFirstPersonControls.asset
This Input Action Map currently dictates phone controls and player movement when in first-person mode.

Assets/DefaultControls.asset
This Input Action Map appears to be referenced in certain scripts (such as DialogueManager.cs) to find the Confirm input, for example.

Assets/Scripts/Robot and the World/Game Input Actions.asset
This Input Action Map is used in a PlayerInput component on the main Player gameobject when in third-person mode.

Important locations/notes:
Pretty much every script that we intend to use is in the Kimmie's House or Studio Warehouse scene.
Assets/Scripts/ScriptableObjects is where most of the narrative-related or dialogue scripts are.
Generally ignore the 'GabeFolder' and 'StephenStuff' folder.

To travel to the Studio from Kimmie's house at this time, once the GlobalPlaytestSettings boolaen 'HasStudio' is true, press T on the keyboard to move there. This is temporary as the phone's UI for the map screen is not working as expected.
