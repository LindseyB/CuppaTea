using UnityEngine;
using System.Collections;

public class HoverHighlight : MonoBehaviour {
	Texture2D overlayTexture;
	Material material;

	public void Start() {
		overlayTexture = Resources.Load("stripes") as Texture2D;
		material = gameObject.GetComponent<Renderer>().material;
	}

	public void OnMouseEnter() {
		material.SetTexture("_Detail", overlayTexture);
	}

	public void OnMouseExit() {
		DisableHighlight();
	}

	public void DisableHighlight() {
		material.SetTexture("_Detail", null);
	}
}
