﻿using UnityEngine;
using System.Collections;

public class GlitchBear : MonoBehaviour {
	float timer = 0.01f;

	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0){
			gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Random.Range(0, 50f), Random.Range(0, 50f)));
			timer = Random.Range(0.03f, 0.1f);
		}
	}
}
