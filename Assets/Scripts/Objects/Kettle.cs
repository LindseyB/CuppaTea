using UnityEngine;
using System.Collections;

public class Kettle : MonoBehaviour, Usable {
	[SerializeField] private ParticleSystem steam;

	private bool heating;
	private int temp;
	private float timer;

	void Start () {
		heating = false;
		timer = 1;
		temp = 20;
	}

	public void Use() {
		// toggle heating
		heating = !heating;
	}

	void Update() {
		// if we are heating or cooling
		if (temp != 20 || heating) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				updateTemp();
				timer = 1;
			}
		}

		// only play the steam when temp is above boiling
		if (temp >= 100 && steam.isStopped) {
			steam.Play();
		} else if (temp < 100 && steam.isPlaying) {
			steam.Stop();
		}
	}

	private void updateTemp(){
		if (heating) {
			temp++;
		} else if (temp > 20) {
			temp--;
		}
	}
}
