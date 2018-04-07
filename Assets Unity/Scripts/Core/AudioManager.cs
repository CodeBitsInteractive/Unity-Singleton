using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Audio Manager
//	@usage works with audio settings
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
public class AudioManager : MonoBehaviour {
	// Public Variables
	public static AudioManager instance = null; // Instance
	public static AudioSettingsModel settings = null; // Audio Settings Model
	private static string _settings_path = Application.persistentDataPath + "/audioSettings.gdf"; // Audio Settings Path

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
	
	// Initialize Audio Settings
	private void InitializeSettings () {
		// Create Audio Model
		if(settings==null) settings = new AudioSettingsModel(); // Create Settings Instance
		if (File.Exists (_settings_path)) { // Check File Exists
			loadSettings (_settings_path); // Load Audio Settings
		}
	}

	// Load Audio Settings
	public void loadSettings(string path){
		// Load Data from Base64 encoded text file
		string _data = File.ReadAllText (path); // Read All Text
		byte[] _decodedBytes = Convert.FromBase64String (_data); // Decode Data
		string _decodedText = Encoding.UTF8.GetString (_decodedBytes); // Get Decoded Data to String
		settings = JsonUtility.FromJson<AudioSettingsModel>(_decodedText); // Convert Data to instance by model
	}

	// Save Settings
	public void saveSettings(string path, AudioSettingsModel data){
		// Save Data to Base64 encoded text file
		string _json_data = JsonUtility.ToJson(data); // Convert Data to JSON
		byte[] _bytesToEncode = Encoding.UTF8.GetBytes (_json_data); // Encode to bytes
		string _encodedText = Convert.ToBase64String (_bytesToEncode); // Convert Encoded bytes to string
		File.WriteAllText(_settings_path, _encodedText); // Save datas to file
	}

	// Delegates for event listeners and change audio muter settings
	public delegate void AudioSettingsChanged(); // Add New Delegate
	public event AudioSettingsChanged OnAudioSettingsChanged; // Add New Event Listener

	// Toggle Sounds Settings
	public void toggleSounds(bool enabled){
		settings.sounds = enabled; // Change Music Settings
		saveSettings(_settings_path, settings); // Save Settings
		if(OnAudioSettingsChanged!=null) OnAudioSettingsChanged();
	}

	// Toggle Music Settings
	public void toggleMusic(bool enabled){
		settings.music = enabled; // Change Music Settings
		saveSettings(_settings_path, settings); // Save Settings
		if(OnAudioSettingsChanged!=null) OnAudioSettingsChanged();
	}

}
