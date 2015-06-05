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
		if (Input.GetKeyDown(KeyCode.E)) {
			GameObject hoverObject;
			hoverObject = grabber.GetMouseHoverObject(reach);

			if (hoverObject != null && hoverObject.GetComponent<Usable>() != null) {
				hoverObject.GetComponent<Usable>().Use();
			}
		}
	}
}
