using UnityEngine;
using System.Collections;

public class Mug : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private GameObject kettle;
	private ParticleSystem steam;
	private GameObject normalWater;
	private GameObject spilledWater;
	private GameObject overflowingWater;

	private bool hasTea;
	private bool hasWater;
	private bool hasSugar;
	private int temp;
	private float timer;
	private float scrollSpeed;
	private Color teaColor;
	private Vector3 startRot;

	void Start () {
		grabber = GameObject.Find ("FPSController").GetComponent<GrabAndDrop>();
		kettle = GameObject.Find("kettle");
		steam = GameObject.Find("steam").GetComponent<ParticleSystem>();

		normalWater = gameObject.transform.GetChild(0).gameObject;
		spilledWater = gameObject.transform.GetChild(1).gameObject;
		overflowingWater = gameObject.transform.GetChild(2).gameObject;

		hasTea = false;
		hasSugar = false;
		hasWater = false;
	
		temp = 20;
		timer = 20;
		scrollSpeed = 0.5f;

		// TODO: base this off of the type of tea brewing
		teaColor = new Color(0.1412f, 0.0863f, 0.0196f, 0.1f);
	}

	public void Use() {
		if (grabber.grabbedObject == kettle) {
			if(hasWater){
				normalWater.SetActive(false);
				overflowingWater.SetActive(true);
			} else {
				normalWater.SetActive(true);
				temp = kettle.GetComponent<Kettle>().temp;
				hasWater = true;
			}
		} else if(grabber.grabbedObject && grabber.grabbedObject.name == "sugar") {
			grabber.DropObject();
			hasSugar = true;
		} else if (grabber.grabbedObject && grabber.grabbedObject.tag == "Tea") {
			grabber.DropObject();
			hasTea = true;
		} else {
			// drink
			normalWater.SetActive(false);
			hasWater = false;
		}
	}

	void Update() {

		if (hasWater) {
			float offset = Time.time * scrollSpeed;
			normalWater.gameObject.GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", new Vector2 (offset, 0));
		}

		// cool temp over time
		if (hasWater && temp != 20) {timer -= Time.deltaTime;}
		if (temp != 20 && timer <= 0) {
			timer = 20;
			temp--;
		}

		if (hasWater && temp >= 100 && steam.isStopped) {
			steam.Play();
		} else if ((!hasWater || temp < 100) && steam.isPlaying) {
			steam.Stop();
		}

		if (hasWater && hasTea && temp > 40) {
			// darken
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			teaColor.a += (Time.deltaTime/51);
			if(teaColor.a > 0.8f){ teaColor.a = 0.8f; }
			gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = teaColor;
		}

		if (hasWater && gameObject.transform.forward.y < 1 /*&& 
		    (!grabber.grabbedObject ||
		    grabber.grabbedObject.name != gameObject.name)*/) {
			// TODO: figure out how to rotate this. :( 
			normalWater.SetActive(false);
			spilledWater.SetActive(true);
		} else if (spilledWater.activeSelf) {
			spilledWater.SetActive(false);
		}
	}
}
