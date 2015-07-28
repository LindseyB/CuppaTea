using UnityEngine;
using System.Collections;

public class UseObject : MonoBehaviour {
	private GrabAndDrop grabber;
	private float reach;

	void Start () {
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
		reach = grabber.reach;
	}

	void Update () {
		if (GameState.InMainMenu) { return; }

		if (Input.GetKeyDown((KeyCode)GameControls.Controls.Use)) {
			GameObject hoverObject;
			hoverObject = grabber.GetMouseHoverObject(reach);

			if (hoverObject != null && hoverObject.GetComponent<Usable>() != null) {
				hoverObject.GetComponent<Usable>().Use();
			}
		}
	}
}
