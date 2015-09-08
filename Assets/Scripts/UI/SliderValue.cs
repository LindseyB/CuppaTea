using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {
	void Start () {
		if(PlayerPrefs.HasKey("MouseSensitivity")){
			gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MouseSensitivity");
		}
	}
}
