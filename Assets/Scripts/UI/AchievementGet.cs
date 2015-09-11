using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using Helpers;

public class AchievementGet : MonoBehaviour {
	[SerializeField] private GameObject achievementCountDisplay;
	[SerializeField] private GameObject pointsDisplay;

	private const float SLIDE_DURATION = 1f;
	private const float DISPLAY_DURATION = 5f;

	private const int NUMBER_OF_ACHIEVEMENTS = 16;

	private Vector3 disabledPos;
	private Vector3 enabledPos;
	private bool moving = false;
	private int achievedCount = 0;
	private int points = 0;

	private DateTime startTime;
	private Text speedRunText;
	private Text speedRunDisplay;
	private bool displaySpeedRun = false;


	void Start () {
		enabledPos = new Vector3(0, -Screen.height/7, 0);
		disabledPos = new Vector3(0, -Screen.height/7 - 80, 0);

		gameObject.transform.localPosition = disabledPos;
		speedRunText = GameObject.Find("SpeedRunTimer").GetComponent<Text>();
		speedRunDisplay = GameObject.Find("SpeedRunDisplay").GetComponent<Text>();
	}

	void Awake() {
		achievedCount = AchievementRecorder.readAchievements();

		if(achievedCount == NUMBER_OF_ACHIEVEMENTS) {
			// reset achievements for this session only 
			AchievementRecorder.initSpeedRunAchievements();
			GameState.SpeedRunMode = true;
			achievedCount = 0;
		}

		points = AchievementRecorder.totalPoints();
		pointsDisplay.GetComponent<TextMesh>().text = "points: " + points;
		achievementCountDisplay.GetComponent<TextMesh>().text = achievedCount.ToString();
	}

	void Update() {
		if(GameState.SpeedRunMode && !GameState.InMainMenu) {
			if(!displaySpeedRun) {
				displaySpeedRun = true;
				StartCoroutine(HideSpeedRun());
			}

			if(startTime == DateTime.MinValue){ 
				speedRunText.enabled = true;
				startTime = DateTime.Now;
			}

			if(achievedCount != NUMBER_OF_ACHIEVEMENTS){
				TimeSpan ts = startTime - DateTime.Now;
				string timerString = ts.ToString();
				speedRunText.text = timerString.Substring(0,timerString.IndexOf("."));
			}
		}
	}

	void OnApplicationQuit() {
		AchievementRecorder.writeAchievements();
	}

	public void TriggerAchievement(int achievement_index) {
		Achievement a = AchievementRecorder.getAchievement(achievement_index);
		if(!a.achieved) {
			achievedCount++;
			achievementCountDisplay.GetComponent<TextMesh>().text = achievedCount.ToString();
			a.Achieved();
			points += a.points;

			pointsDisplay.GetComponent<TextMesh>().text = "points: " + points;

			if(achievedCount == NUMBER_OF_ACHIEVEMENTS) {
				DisplayAchievement(AchievementRecorder.metaAchievement);
				GameState.Blackhole = true;
			} else {
				DisplayAchievement(a);
			}
		}
	}

	void DisplayAchievement(Achievement a) {
		Text[] textObjects = gameObject.GetComponentsInChildren<Text>();
		textObjects[0].text = a.name;
		textObjects[1].text = a.description;
		StartCoroutine(SlideDisplay());
	}
	
	IEnumerator SlideDisplay() {
		if (!moving) {
			moving = true;
			yield return StartCoroutine(SlideDisplayUp());
			yield return new WaitForSeconds(DISPLAY_DURATION);
			yield return StartCoroutine(SlideDisplayDown());
			moving = false;
		}
	}

	IEnumerator SlideDisplayUp() {
		float time = SLIDE_DURATION;
		float t = 0f;
		while (t < 1f) {
			t += Time.deltaTime / time;
			transform.localPosition = Vector3.Lerp(disabledPos, enabledPos, t);
			yield return 0;
		}
	}

	IEnumerator SlideDisplayDown() {
		float time = SLIDE_DURATION;
		float t = 0f;
		while (t < 1f) {
			t += Time.deltaTime / time;
			transform.localPosition = Vector3.Lerp(enabledPos, disabledPos, t);
			yield return 0;
		}
	}

	IEnumerator HideSpeedRun() {
		for(int i=0; i<5; i++){
			speedRunDisplay.enabled = true;
			yield return new WaitForSeconds(0.5f);
			speedRunDisplay.enabled = false;
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void resetAchievements() {
		AchievementRecorder.resetAchievements();
		points = 0;
		achievedCount = 0;
		pointsDisplay.GetComponent<TextMesh>().text = "points: " + points;
		achievementCountDisplay.GetComponent<TextMesh>().text = achievedCount.ToString();
	}
}
