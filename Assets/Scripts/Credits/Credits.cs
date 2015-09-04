using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI;
using Helpers;

public class Credits : MonoBehaviour {
	private List<string> credits = new List<string>(){ 
								"By Lindsey Bieda",
	                            "Glitch Bear By Todd Gizzi",
	                            "Purr Programming support by Dash",
								"Public Domain Music:",
								"A Song of Old Hawaii by Cliff Edwards"
								};
	private Text text;
	
	void Start () {
		text = gameObject.GetComponent<Text>() as Text;
		text.canvasRenderer.SetAlpha(0.0f);

		credits.Add("Your score was: " + AchievementRecorder.totalPoints());

		StartCoroutine("DisplayCredits");
	}

	void Awake() {
		AchievementRecorder.readAchievements();
	}


	IEnumerator DisplayCredits() {
		while(true){
			foreach(string credit in credits){
				text.text = credit;
				text.CrossFadeAlpha(1.0f, 5.0f, false);
				yield return new WaitForSeconds(5.0f);
				text.CrossFadeAlpha(0.0f, 5.0f, false);
				yield return new WaitForSeconds(5.0f);
			}
		}
	}
	
}
