using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	[SerializeField] private AudioSource collideAudio;

	void OnCollisionEnter(Collision collision) {
		if(!GameState.Rewinding && !GameState.InMainMenu){ collideAudio.Play(); }
	}
}
