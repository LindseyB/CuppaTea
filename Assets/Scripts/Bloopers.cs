using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Helpers;

public class Bloopers : MonoBehaviour {
	void Start () {
		MovieTexture mt = gameObject.GetComponent<RawImage>().mainTexture as MovieTexture;
		mt.loop = true;

		Debug.Log(AchievementRecorder.totalPoints());

		mt.Play();
	}

	void Update() {
		if (Input.GetKey(KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
	}
}
