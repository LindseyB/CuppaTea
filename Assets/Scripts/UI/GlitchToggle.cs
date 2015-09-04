using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlitchToggle : MonoBehaviour {
	void Start () {
		if(PlayerPrefs.HasKey("Glitch")){
			gameObject.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("Glitch") == 1);
		}
	}
}
