using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour, Usable {
	public void Use() {
		if(AudioListener.volume > 0f) {
			AudioListener.volume = 0f;
		} else {
			AudioListener.volume = 1f;
		}
	}
}
