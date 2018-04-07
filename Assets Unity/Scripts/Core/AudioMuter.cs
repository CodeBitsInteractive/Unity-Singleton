using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Audio Muter
//	@usage on/off audio sources on objects
//
//	Developed by CodeBits Interactive
//	https://cdbits.net/
//=============================================
public class AudioMuter : MonoBehaviour {
	// Public Settings for AudioMuter
	public bool is_music = false; // This Object is music?

	// Private Params and object components
	private AudioSource _as; // AudioSource Component
	private float _base_volume = 1F; // Base Volume for this object

	// Use this for initialization
	void Start () {
		// Get AudioSource component and BaseVolume settings
		_as = this.gameObject.GetComponent<AudioSource> (); // Get Audio Source Component
		_base_volume = _as.volume; // Get Base Volume for this audio source

		// Add Event Listener for change AudioSource State
		AudioManager.instance.OnAudioSettingsChanged += _audioSettingsChanged; // Set Method
	}

	// Use on Destroy this Object
	void OnDestroy(){
		AudioManager.instance.OnAudioSettingsChanged -= _audioSettingsChanged; // Unset method
	}
	
	// Update is called once per frame
	private void _audioSettingsChanged(){
		if (is_music) // Load Audio Settings for Music
			_as.volume = (AudioManager.settings.music) ? _base_volume : 0F; // Set Audio Source Mute State
		if (!is_music) // Load Audio Settings for Sounds
			_as.volume = (AudioManager.settings.sounds) ? _base_volume : 0F; // Set Audio Source Mute State
	}
}