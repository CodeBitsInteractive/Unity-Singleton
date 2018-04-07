using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Game Core Loader
//	@usage load core managers (singleton based)
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
public class GameLoader : MonoBehaviour {
	// Game Managers
	[Header ("Put here managers prefabs")]
	public GameObject game_manager; // Game Base
	public GameObject audio_manager; // Audio Manager
	public GameObject lang_manager; // Language Manager
	public GameObject net_manager; // Network Manager

	// Use this for initialization
	void Awake () {
		// Initialize Game Base
		if (GameManager.instance == null) {
			Instantiate (game_manager);
		}

		// Initialize Audio Manager
		if (AudioManager.instance == null) {
			Instantiate (audio_manager);
		}

		// Initialize Language Manager
		if (LangManager.instance == null) {
			Instantiate (lang_manager);
		}

		// Initialize Network Manager
		if (NetworkManager.instance == null) {
			Instantiate (net_manager);
		}
	}
}
