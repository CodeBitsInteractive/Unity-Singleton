using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Language Manager
//	@usage works with language in the game
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
public class LangManager : MonoBehaviour {
	// Public Manager Params and Instances
	public static LangManager instance = null; // Instance
	public static LangSettingsModel settings = null; // Audio Settings Model
	private static string _settings_path = Application.persistentDataPath + "/languageSettings.gdf"; // Audio Settings Path
	private List<LangItem> languages; // List of languages

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

	// Update is called once per frame
	private void InitializeSettings(){
		// Create list of languages
		languages = new List<LangItem>(); // Create new List
		languages.Add(new LangItem("EN", SystemLanguage.English)); // Add English
		languages.Add(new LangItem("RU", SystemLanguage.Russian)); // Add Russian

		// Detect File Exists
		if (File.Exists (_settings_path)) { // Language Settings File Exists
			// Here we load language settings from file
			loadSettings(_settings_path); // Load from file and set to instance
		} else { // No File, First Run
			// Here we detect user language for our scripts
			settings = new LangSettingsModel(); // Create Lang Settings Model instance for manager
			settings.language = languages [0].code; // Default Language
			for (int i = 0; i < languages.Count; i++) { // Each language items
				if (Application.systemLanguage == languages [i].language) { // Detected system language
					settings.language = languages [i].code; // Set Language
					break; // Exit Loop
				}
			}
			
			// Now, we save detected language
			saveSettings(_settings_path, settings); 
		}
	}

	// Load Language Settings
	public void loadSettings(string path){
		// Load Data from Base64 encoded text file
		string _data = File.ReadAllText (path); // Read All Text
		byte[] _decodedBytes = Convert.FromBase64String (_data); // Decode Data
		string _decodedText = Encoding.UTF8.GetString (_decodedBytes); // Get Decoded Data to String
		settings = JsonUtility.FromJson<LangSettingsModel>(_decodedText); // Convert Data to instance by model
	}

	// Save Settings
	public void saveSettings(string path, LangSettingsModel data){
		// Save Data to Base64 encoded text file
		string _json_data = JsonUtility.ToJson(data); // Convert Data to JSON
		byte[] _bytesToEncode = Encoding.UTF8.GetBytes (_json_data); // Encode to bytes
		string _encodedText = Convert.ToBase64String (_bytesToEncode); // Convert Encoded bytes to string
		File.WriteAllText(_settings_path, _encodedText); // Save datas to file
	}
	
	// Switch to next language
	public void nextLanguage(){
		int current = 0; // Current Language Index
		
		// Each languages list
		for (int i = 0; i < languages.Count; i++) {
			if (languages [i].code == settings.language) { // List item equal current lang name
				current = i; // Set current lang index
				break; // Exit from loop
			}
		}
		
		// Change Index for language
		current = ((current + 1) == (languages.Count - 1))?0:(current+1); // Set New Index
		settings.language = languages[current].code; // Set Language
		saveSettings(_settings_path, settings); // Save Language Settings
	}
}

//=============================================
//	Language Item
//=============================================
public class LangItem{
	// Public Variables
	public string code; // Language Code
	public SystemLanguage language; // System Parametr. Like a "Russian"

	// Constructor
	public LangItem(string c, SystemLanguage l){
		code = c; // Set Code
		language = l; // Set Language
	}
}