using UnityEngine;
using System.Collections;

public class SecondHand : MonoBehaviour {
	private bool moving = false;
	private float degrees = 0;

	void Update () {
		Quaternion rot = Quaternion.Euler(degrees -= 1f, 0, 0);
		StartCoroutine(Tick(rot,1f));
	}

	IEnumerator Tick(Quaternion targetRot, float time){
		Quaternion startRot = transform.rotation;
		if (!moving){
			moving = true;
			float t = 0f;
			while (t < 1f){
				t += Time.deltaTime / time;
				transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
				yield return 0;
			}
			moving = false;
		}
	}
}
