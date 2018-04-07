using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Usage Example
//	@usage here you can see an managers usage
//	example. This script is simple scene logic.
//
//	All managers initialized by GameLoader.cs
//	will be available from any scene and don't
//	destroyable.
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
public class UsageExample : MonoBehaviour {
	// Use this for initialization
	void Start () {
		// AudioManager usage Example
		AudioManager.instance.toggleSounds(true); // Toggle Sounds, use instance.* for non-static methods and variables
		Debug.Log(AudioManager.settings.sounds); // Returns "true". Use without instance.* for static methods and vars
		
		// Language Manager usage Example
		Debug.Log(LangManager.settings.language); // Returns current language, for example "RU" from list inside LangManager class
		LangManager.instance.nextLanguage(); // Switch to next language from list inside LangManager class, set it as default and save settings to file for next runs
		Debug.Log(LangManager.settings.language); // Returns next (now is current) language, for example "EN" from list inside LangManager class
		
		// Network Manager usage Example
		// Use Coroutine function to call NetworkManager methods
		// WARNING - THIS EXAMPLE DON'T USE REAL SCRIPT ON SERVER AND WILL RETURN
		// CONNECTION ERROR ANYTIME
		StartCoroutine(NetworkManager.instance.CheckConnection((()=>{ // Request return complete=true (See baseResponse inside NetworkManager)
			Debug.Log(NetworkManager.connection); // Log connection state
		}),((string message)=>{ // Error, request return complete=false (See baseResponse inside NetworkManager)
			Debug.Log(message); // Log error
		}));
		
		// Game Manager usage Example
		Debug.Log(GameManager.state.current_location); // Returns default location "MainCutscene"
	}
}