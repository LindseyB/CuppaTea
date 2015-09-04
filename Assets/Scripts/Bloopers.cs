using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bloopers : MonoBehaviour {
	void Start () {
		MovieTexture mt = gameObject.GetComponent<RawImage>().mainTexture as MovieTexture;
		mt.loop = true;
		mt.Play();
	}

	void Update() {
		if (Input.GetKey(KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
	}
}
