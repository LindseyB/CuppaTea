using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour, Usable {
	private HoverHighlight highlight;
	private ParticleSystem system;

	void Start() {
		highlight = gameObject.GetComponent<HoverHighlight>() as HoverHighlight;
		system = gameObject.GetComponentInChildren<ParticleSystem>() as ParticleSystem;
	}		

	public void Use() {
		if(AudioListener.volume > 0f) {
			AudioListener.volume = 0f;
			system.Stop();
		} else {
			AudioListener.volume = 1f;
			system.Play();
		}
	}

	void Update() {
		if (GameState.InMainMenu) { return; }

		if(highlight.Enabled()){
			if(Input.GetButton("VolumeUp")) {
				AudioListener.volume += 0.1f;
			} else if(Input.GetButton("VolumeDown")) {
				AudioListener.volume -= 0.1f;
			}
		}
	}
}
