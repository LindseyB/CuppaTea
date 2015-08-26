using UnityEngine;
using System.Collections;

public class Blackhole : MonoBehaviour {
	bool moving = false;

	void Update () {
		if(Input.GetKey(KeyCode.G)) { GameState.Blackhole = true; } // TODO: remove - just for testing

		if(GameState.Blackhole) {
			GameObject directionalLight = GameObject.Find("Directional Light");
			GameObject.Find("Music").GetComponent<AudioSource>().enabled = false;

			directionalLight.GetComponentInChildren<AudioSource>().enabled = true;
			directionalLight.GetComponentInChildren<ParticleSystem>().Play();

			Rigidbody rb;
			if(rb = gameObject.GetComponent<Rigidbody>()){
				rb.useGravity = false;
				rb.detectCollisions = false;
			}

			StartCoroutine(GravitySuck(directionalLight.transform.position, Random.Range(50.0f, 100f)));
		}
	}

	IEnumerator GravitySuck(Vector3 targetPosition, float time){
		if (!moving){
			moving = true;
			float t = 0f;
			while (t < 1f){
				t += Time.deltaTime / time;
				transform.position = Vector3.Lerp(transform.position, targetPosition, t); 
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, t);
				yield return 0;
			}
			moving = false;
		}
	}
}
