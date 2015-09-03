using UnityEngine;
using System.Collections;

public class RewindAudio : MonoBehaviour {
	private AudioSource audioSource;

	void Start() {
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	void Update() {
		if(GameState.Rewinding) {
			audioSource.pitch = -1;
		} else {
			audioSource.pitch = 1;
		}
	}
}
