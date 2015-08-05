using UnityEngine;
using System.Collections;

public class GlitchBear : MonoBehaviour {	
	public MovieTexture glitch;

	void Start() {
		gameObject.GetComponent<Renderer>().material.SetTexture("_Detail", glitch);
		glitch.loop = true;
		glitch.Play();
	}	
}
