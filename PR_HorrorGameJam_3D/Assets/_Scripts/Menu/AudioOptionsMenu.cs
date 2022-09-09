using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptionsMenu : HidableMenu {

	[SerializeField] private AudioMixer mixer;

	[SerializeField] private Slider masterSlider;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider soundFXSlider;
	[SerializeField] private Slider voiceSlider;

	// Start is called before the first frame update
	void Start() {
		mixer.GetFloat("MasterVolume", out float masterVolume);
		mixer.GetFloat("MusicVolume", out float musicVolume);
		mixer.GetFloat("SoundFXVolume", out float soundFXVolume);
		mixer.GetFloat("VoiceVolume", out float voiceVolume);

		masterSlider.value = masterVolume;
		musicSlider.value = musicVolume;
		soundFXSlider.value = soundFXVolume;
		voiceSlider.value = voiceVolume;
	}

	public void UpdateMasterVolume() {
		float value = masterSlider.value;
		mixer.SetFloat("MasterVolume", value);
	}

	public void UpdateMusicVolume() {
		float value = musicSlider.value;
		mixer.SetFloat("MusicVolume", value);
	}

	public void UpdateSoundFXVolume() {
		float value = soundFXSlider.value;
		mixer.SetFloat("SoundFXVolume", value);
	}

	public void UpdateVoiceVolume() {
		float value = voiceSlider.value;
		mixer.SetFloat("VoiceVolume", value);
	}
}
