using UnityEngine;
using System.Collections;

public class Lemon : RewindObject, Usable {
	private GrabAndDrop grabber;
	private GameObject lemonObject;
	private GameObject lemonObjectDupe;
	private Vector3 scale;

	new void Start () {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
		lemonObject = gameObject.transform.GetChild(0).gameObject;

		base.Start();
	}

	public void Use() {
		if (grabber.grabbedObject) { return; }

		// grab a lemon slice
		lemonObjectDupe = Instantiate(lemonObject);
		lemonObjectDupe.transform.SetParent(gameObject.transform);
		lemonObjectDupe.transform.localScale = scale;
		grabber.TryGrabObject(lemonObjectDupe);
		lemonObjectDupe.SetActive(true);
	}
}
