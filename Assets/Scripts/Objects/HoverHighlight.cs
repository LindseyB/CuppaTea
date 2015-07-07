using UnityEngine;
using System.Collections;

public class HoverHighlight : MonoBehaviour {
	private Texture2D overlayTexture;
	private Material material;
	private GameState gameState;

	private GrabAndDrop grabber;

	public void Start() {
		overlayTexture = Resources.Load("stripes") as Texture2D;
		material = gameObject.GetComponent<Renderer>().material;
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
		gameState = FindObjectOfType (typeof(GameState)) as GameState;
	}

	public void OnMouseEnter() {
		if (gameState.InMainMenu) { return; }

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
