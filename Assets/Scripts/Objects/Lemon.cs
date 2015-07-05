using UnityEngine;
using System.Collections;

public class Lemon : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private GameObject lemonObject;
	private GameObject lemonObjectDupe;
	private Vector3 scale;

	public void Start () {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
		lemonObject = gameObject.transform.GetChild(0).gameObject;	
	}

	public void Use() {
		// grab a lemon slice
		lemonObjectDupe = Instantiate(lemonObject);
		lemonObjectDupe.transform.SetParent(gameObject.transform);
		lemonObjectDupe.transform.localScale = scale;
		grabber.TryGrabObject(lemonObjectDupe);
		lemonObjectDupe.SetActive(true);
	}
}
