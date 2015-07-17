using UnityEngine;
using System.Collections;

public class UseObject : MonoBehaviour {
	private GameState gameState;
	private GrabAndDrop grabber;
	private float reach;

	void Start () {
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
		gameState = FindObjectOfType (typeof(GameState)) as GameState;
		reach = grabber.reach;
	}

	void Update () {
		if (gameState.InMainMenu) { return; }

		if (Input.GetKeyDown((KeyCode)GameControls.Controls.Use)) {
			GameObject hoverObject;
			hoverObject = grabber.GetMouseHoverObject(reach);

			if (hoverObject != null && hoverObject.GetComponent<Usable>() != null) {
				hoverObject.GetComponent<Usable>().Use();
			}
		}
	}
}
