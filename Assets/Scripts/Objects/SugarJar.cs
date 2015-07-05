using UnityEngine;
using System.Collections;

public class SugarJar : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private Vector3 scale;
	private GameObject sugarObject;
	
	public void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
	}
	
	public void Use() {
		// grab some sugar 
		sugarObject = gameObject.transform.GetChild(0).gameObject;
		
		if(sugarObject.activeInHierarchy) {
			sugarObject = (GameObject) Instantiate(sugarObject);
			sugarObject.transform.SetParent(gameObject.transform);
			sugarObject.transform.localScale = scale;
		}
		
		grabber.TryGrabObject(sugarObject);
		sugarObject.SetActive(true);
	}

}