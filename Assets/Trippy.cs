using UnityEngine;
using System.Collections;

public class Trippy : MonoBehaviour {
	private Material mat;

	// Use this for initialization
	void Start () {
		mat = gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		mat.SetInt("_BumpAmt", (int)(Random.Range(10, 128)));
		mat.SetTextureScale("_BumpMap", new Vector2((int)Random.Range(1, 5), (int)Random.Range(1, 5)) );
		mat.SetTextureScale("_MainTex", new Vector2((int)Random.Range(1, 5), (int)Random.Range(1, 5)) );
	}
}
