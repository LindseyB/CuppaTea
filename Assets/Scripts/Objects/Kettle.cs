using UnityEngine;
using System.Collections;
using Helpers;

public class Kettle : RewindObject, Usable {
	[SerializeField] private ParticleSystem steam;
	[SerializeField] private AudioSource collideAudio;
	[SerializeField] private AudioSource boilAudio;
	[SerializeField] public AudioSource pourAudio;

	public bool heating;
	public int temp;
	private float timer;

	new void Start () {
		heating = false;
		timer = 1;
		temp = 20;

		base.Start();
	}

	public void Use() {
		// toggle heating
		heating = !heating;

		if(heating){
			gameObject.GetComponent<Animator>().Play("Boil");
		} else {
			gameObject.GetComponent<Animator>().Play("None");
		}
	}

	new void Update() {
		if (heating) {
			gameObject.transform.Translate(Vector3.up * Time.deltaTime * 0.5f);

			if(!boilAudio.isPlaying){
				boilAudio.Play();
			}
		} else if(boilAudio.isPlaying) {
			boilAudio.Stop();
		}

		if(temp >= 467) {
			achievementGet.TriggerAchievement(AchievementRecorder.hotLikeVenus);
		}

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

		base.Update();
	}

	void OnCollisionEnter(Collision collision) {
		if(!GameState.Rewinding && !GameState.InMainMenu && !boilAudio.isPlaying){ collideAudio.Play(); }
	}

	private void updateTemp(){

		if (heating) {
			temp+=100; //temp--;
		} else if (temp > 20) {
			temp--;
		}
	}
}
