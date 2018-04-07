using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Network Manager
//	@usage working with network in the game.
//	Network manager send HTTP requests for
//	API server, get JSON responce and work
//	with responce using GlobalGameModel
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
//=============================================
//	Network Manager Class
//=============================================
public class NetworkManager : MonoBehaviour {
	// Public Manager Params and Instances
	public static NetworkManager instance = null; // Instance
	public static bool connection = false; // Connection State
	
	// Private Params for NetworkManager
	private string server_uri = "https://example.com/"; // Server URI

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
		/* Here you can initialize network manager settings, but now it's an empty */
	}
	
	// Test Connection Method
	public delegate void OnConnectionStateChecked();
	public delegate void OnConnectionStateCheckError(string message);
	public IEnumerator CheckConnection(OnConnectionStateChecked loaded, OnConnectionStateCheckError error){
		// Request data building
		WWWForm data = new WWWForm(); // Create WWW Form
		data.AddField("lang", LangManager.settings.language); // Add Language from Language Manager for responce messages

		// Send request to API Server
		WWW request = new WWW(server_uri + "/check_connection/", data); // Create Request
		yield return request; // Send Request

		// Responce checking
		if (request.error != null) { // Request has error
			connection = false; // Server connection failture
			error ("Failed to send request. Please, check your internet connection."); // Do error
		} else { // Complete
			try{ // Trying to work with responce
				// Here we parse JSON data and set server connection state
				baseResponse responce = JsonUtility.FromJson<baseResponse>(request.text); // Convert from JSON to Base Responce Model
				if(responce.complete){ // Responce Complete
					connection = true; // Server connection = true
					loaded(); // Loaded
				}else{ // Responce Error
					connection = false; // Server connection failture
					error (responce.message); // Do error
					Debug.Log("API Error: " + responce.message); // Just log for self
				}
			}catch{ // Error
				connection = false; // Server connection failture
				error ("Failed to parse JSON data from server. Please, try again later."); // Do error
			}
		}
	}
}

//=============================================
//	Network Manager Base Models
//=============================================
[System.Serializable]
public class baseResponse{
	public bool complete = false; // Request State (true = ok, false = error)
	public string message = ""; // Request Message (Only for request state = false)
}