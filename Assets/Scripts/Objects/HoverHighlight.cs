using UnityEngine;
using System.Collections;

public class HoverHighlight : MonoBehaviour {
	Texture2D overlayTexture;
	Material material;

	private GrabAndDrop grabber;

	public void Start() {
		overlayTexture = Resources.Load("stripes") as Texture2D;
		material = gameObject.GetComponent<Renderer>().material;
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
	}

	public void OnMouseEnter() {
		if (gameObject != grabber.grabbedObject) {
			material.SetTexture("_Detail", overlayTexture);
		}
	}

	public void OnMouseExit() {
		DisableHighlight();
	}

	public void DisableHighlight() {
		material.SetTexture("_Detail", null);
	}
}
