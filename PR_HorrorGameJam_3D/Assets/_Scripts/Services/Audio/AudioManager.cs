using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	[Header("Audio Sources")]
	[SerializeField] private AudioSource globalAudioSource;
	[SerializeField] private GameObject prefabAudioSource;

	[Header("Audio")]
	[SerializeField] private AudioGroup[] audioGroups;

	[Header("Settings")]
	[SerializeField] private int maxFloatingAudioSources;

	private ObjectPool<GameObject> floatingAudioSources;

	private Dictionary<string, AudioGroup> mappedAudioGroups;

	// Start is called before the first frame update
	void Awake() {
		FillFloatingAudioSourcePool();
		MapAudioGroups();
	}

	private void FillFloatingAudioSourcePool() {
		floatingAudioSources = new ObjectPool<GameObject>();

		for (int _ = 0; _ < maxFloatingAudioSources; _++) {
			GameObject newFloatingAudioSource = Instantiate(prefabAudioSource);
			newFloatingAudioSource.SetActive(false);
			newFloatingAudioSource.transform.SetParent(transform);
			floatingAudioSources.AddToPool(newFloatingAudioSource);
		}
	}

	private void MapAudioGroups() {
		mappedAudioGroups = new Dictionary<string, AudioGroup>();

		for (int i = 0; i < audioGroups.Length; i++) {
			AudioGroup group = audioGroups[i];
			string name = group.name;

			if (mappedAudioGroups.ContainsKey(name)) {
				Debug.LogError($"ERROR! Multiple Audio Groups are named \"{name}\". Please ensure Adui Groups have unique names.");
			} else {
				mappedAudioGroups.Add(name, group);
			}
		}
	}

	public void PlayGlobal(string group, string clip) {
		AudioClip foundClip = GetClipFromGroup(group, clip);
		if (foundClip) PlayGlobal(foundClip);
	}

	public void PlayLocal(Vector3 position, string group, string clip) {
		AudioClip foundClip = GetClipFromGroup(group, clip);
		if (foundClip) PlayLocal(position, foundClip);
	}

	public void PlayRandomGlobal(string group) {
		if (mappedAudioGroups.ContainsKey(group)) {
			AudioClip foundClip = mappedAudioGroups[group].GetRandomClipWithoutRepeating();
			if (foundClip != null) {
				PlayGlobal(foundClip);
			} else {
				Debug.LogError($"Group \"{group}\" is empty!");
			}
		} else {
			Debug.LogError($"No group with name \"{group}\" found");
		}
	}

	public void PlayRandomLocal(Vector3 position, string group) {
		if (mappedAudioGroups.ContainsKey(group)) {
			AudioClip foundClip = mappedAudioGroups[group].GetRandomClipWithoutRepeating();
			if (foundClip != null) {
				PlayLocal(position, foundClip);
			} else {
				Debug.LogError($"Group \"{group}\" is empty!");
			}
		} else {
			Debug.LogError($"No group with name \"{group}\" found");
		}
	}

	public void PlayGlobal(AudioClip clip) {
		globalAudioSource.clip = clip;
		globalAudioSource.Play();
	}

	public void PlayGlobal(AudioClip clip, bool looping = false) {
		globalAudioSource.clip = clip;
		globalAudioSource.loop = looping;
		globalAudioSource.Play();
	}

	public void PlayLocal(Vector3 position, AudioClip clip) {
		GameObject newFloatingObject = floatingAudioSources.CreateNew();
		newFloatingObject.transform.position = position;

		FloatingAudioSource audio = newFloatingObject.GetComponent<FloatingAudioSource>();

		if (audio) {
			audio.Play(this, clip);
		}
	}

	private AudioClip GetClipFromGroup(string group, string clip) {
		if (mappedAudioGroups.ContainsKey(group)) {
			AudioClip foundClip = mappedAudioGroups[group].GetClipByName(clip);
			if (foundClip != null) {
				return foundClip;
			} else {
				Debug.LogError($"No clip with name \"{clip}\" found in group \"{group}\"");
			}
		} else {
			Debug.LogError($"No group with name \"{group}\" found");
		}

		return null;
	}

	public void RemoveFloatingAudioSource(GameObject floatingAudioSource) {
		floatingAudioSources.Destroy(floatingAudioSource);
	}
}
