using UnityEngine;
using System.Collections;

public class HoverHighlight : MonoBehaviour {
	[SerializeField] public GameObject renderObject;
	private Texture2D overlayTexture;
	private Material material;
	private GrabAndDrop grabber;

	public void Start() {
		overlayTexture = Resources.Load("stripes") as Texture2D;
		if(renderObject){
			material = renderObject.GetComponent<Renderer>().material;
		} else {
			material = gameObject.GetComponent<Renderer>().material;
		}

		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
	}

	public void OnMouseEnter() {
		if (GameState.InMainMenu) { return; }

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
