using UnityEngine;
using System.Collections;

public class Tin : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private GameObject teaObject;
	private GameObject teaObjectDupe;
	private Vector3 scale;
	private GameState gameState;

	public void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
		teaObject = gameObject.transform.GetChild(0).gameObject;
		gameState = FindObjectOfType (typeof(GameState)) as GameState;
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
		if (gameState.InMainMenu) { return; }
		gameObject.transform.GetChild(1).gameObject.SetActive(true);
	}

	public void OnMouseExit() {
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
	}
}
