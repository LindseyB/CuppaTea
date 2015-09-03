using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bloopers : MonoBehaviour {
	void Start () {
		MovieTexture mt = gameObject.GetComponent<RawImage>().mainTexture as MovieTexture;
		mt.loop = true;
		mt.Play();
	}
}
