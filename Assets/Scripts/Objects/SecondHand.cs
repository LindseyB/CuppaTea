using UnityEngine;
using System.Collections;

public class SecondHand : RewindObject {
	private bool ticking = false;
	private float degrees = 0;

	new void Update () {
		Quaternion rot = Quaternion.Euler(degrees -= 1f, 0, 0);
	
		if(!rewinding){
			StartCoroutine(Tick(rot,1f));
		}

		base.Update();
	}

	IEnumerator Tick(Quaternion targetRot, float time){
		Quaternion startRot = transform.rotation;
		if (!ticking && !rewinding){
			ticking = true;
			float t = 0f;
			while (t < 1f){
				t += Time.deltaTime / time;
				transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
				yield return 0;
			}
			ticking = false;
		}
	}
}
