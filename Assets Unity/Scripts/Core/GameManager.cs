using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Game Manager
//	@usage global game manager. use for global
//	functions
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
public class GameManager : MonoBehaviour {
	// Public Manager Params and Instances
	public static GameManager instance = null; // Instance
	public static gameState state = null; // Audio Settings Model
	private static string _settings_path = Application.persistentDataPath + "/gameData.gdf"; // Audio Settings Path

	// Use this for initialization
	void Start () {
		// Check Instance of Gamebase
		if (instance == null) { // Instance not found
			instance = this; // Set this object to instance
		} else if(instance == this){ // Has instance
			Destroy(gameObject); // Destroy this Gamebase
		}

		// Cant destroy this object when load another
		// scene for singleton working state
		DontDestroyOnLoad(gameObject); // Don't Destroy Gamebase

		// Initialize Settings
		InitializeSettings();
	}
	
	// Delegates for event listeners. Running on game initialization complete
	public delegate void GameStateInitialized(); // Add New Delegate
	public event GameStateInitialized OnGameStateLoaded; // Add New Event Listener
	
	// Update is called once per frame
	private void InitializeSettings(){
		// Find game data file and load or create an empty
		if(File.Exists (_settings_path)){ // File Exists
			loadSettings(_settings_path); // Get Game State
		}else{ // File not Exists
			state = new gameState(); // Create new Game State
			saveSettings(_settings_path, state); // Save Default game state
		}
		
		// Run Event Triggers
		if(OnGameStateLoaded!=null) OnGameStateLoaded();
	}
	
	// Load Game State
	public void loadSettings(string path){
		// Load Data from Base64 encoded text file
		string _data = File.ReadAllText (path); // Read All Text
		byte[] _decodedBytes = Convert.FromBase64String (_data); // Decode Data
		string _decodedText = Encoding.UTF8.GetString (_decodedBytes); // Get Decoded Data to String
		state = JsonUtility.FromJson<gameState>(_decodedText); // Convert Data to instance by model
	}

	// Save Game Data
	public void saveSettings(string path, gameState data){
		// Save Data to Base64 encoded text file
		string _json_data = JsonUtility.ToJson(data); // Convert Data to JSON
		byte[] _bytesToEncode = Encoding.UTF8.GetBytes (_json_data); // Encode to bytes
		string _encodedText = Convert.ToBase64String (_bytesToEncode); // Convert Encoded bytes to string
		File.WriteAllText(_settings_path, _encodedText); // Save datas to file
	}
}
