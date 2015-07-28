using UnityEngine;
using System.Collections;

public class Tin : RewindObject, Usable {
	[SerializeField] private AudioSource collideAudio;

	private GrabAndDrop grabber;
	private GameObject teaObject;
	private GameObject teaObjectDupe;
	private Vector3 scale;

	new void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
		teaObject = gameObject.transform.GetChild(0).gameObject;

		base.Start();
	}

	public void Use() {
		if (grabber.grabbedObject) { return; }

		// grab some tea leaves
		teaObjectDupe = Instantiate(teaObject);
		teaObjectDupe.transform.SetParent(gameObject.transform);
		teaObjectDupe.transform.localScale = scale;
		grabber.TryGrabObject(teaObjectDupe);
		teaObjectDupe.SetActive(true);
	}

	public void OnMouseEnter() {
		if (GameState.InMainMenu) { return; }
		gameObject.transform.GetChild(1).gameObject.SetActive(true);
	}

	public void OnMouseExit() {
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
	}

	void OnCollisionEnter(Collision collision) {
		if(!GameState.Rewinding && !GameState.InMainMenu){ collideAudio.Play(); }
	}
}
