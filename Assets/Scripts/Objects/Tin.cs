using UnityEngine;
using System.Collections;

public class Tin : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private GameObject teaObject;
	private Vector3 scale;

	public void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
	}

	public void Use() {
		// grab some tea leaves
		teaObject = gameObject.transform.GetChild(0).gameObject;

		if(teaObject.activeInHierarchy) {
			teaObject = Instantiate(teaObject);
			teaObject.transform.SetParent(gameObject.transform);
			teaObject.transform.localScale = scale;
		}

		grabber.TryGrabObject(teaObject);
		teaObject.SetActive(true);
	}

	public void OnMouseEnter() {
		gameObject.transform.GetChild(1).gameObject.SetActive(true);
	}

	public void OnMouseExit() {
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
	}
}
