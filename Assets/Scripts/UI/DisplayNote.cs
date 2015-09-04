using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayNote : MonoBehaviour {
	void Start () {
		gameObject.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
		gameObject.transform.FindChild("arrow").GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);

		if(!PlayerPrefs.HasKey("Glitch")){
			gameObject.GetComponent<Text>().CrossFadeAlpha(1.0f, 1.0f, false);
			gameObject.transform.FindChild("arrow").GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, false);
			PlayerPrefs.SetInt("Glitch", 1);
			PlayerPrefs.Save();
		}
	}
}
