using UnityEngine;
using System.Collections;

public class GlitchBear : MonoBehaviour {	
	public MovieTexture glitch;

	void Start() {
		// For some reason I need to clamp before repeating - unity bug?
		glitch.wrapMode = TextureWrapMode.Clamp;
		gameObject.GetComponent<Renderer>().material.SetTexture("_Detail", glitch);
		glitch.loop = true;
		glitch.Play();
		glitch.wrapMode = TextureWrapMode.Repeat;

	}	
}
