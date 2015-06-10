using UnityEngine;
using System.Collections;

public class Mug : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private GameObject kettle;
	private ParticleSystem steam;

	private bool hasTea;
	private bool hasWater;
	private int temp;
	private float timer;

	void Start () {
		grabber = GameObject.Find ("FPSController").GetComponent<GrabAndDrop>();
		kettle = GameObject.Find("kettle");
		steam = gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

		hasTea = false;
		temp = 20;
		timer = 20;
	}

	public void Use() {
		if (grabber.grabbedObject == kettle) {
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			temp = kettle.GetComponent<Kettle>().temp;
			hasWater = true;
		} else if (grabber.grabbedObject && grabber.grabbedObject.tag == "Tea") {
			grabber.DropObject();
			hasTea = true;
		} else {
			// drink
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	void Update() {
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

		if (hasWater && hasTea) {
			// brew 
		}
	}
}
