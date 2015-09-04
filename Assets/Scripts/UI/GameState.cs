using UnityEngine;
using System.Collections;
using System;

public class GameState : MonoBehaviour {
	public static bool InMainMenu = true;	
	public static bool Rewinding = false;
	public static bool Blackhole = false;
	public static bool Glitch = true;
	public static bool FirstStart = true;

	void ToggleGlitch() {
		Glitch = !Glitch;
		PlayerPrefs.SetInt("Glitch", Convert.ToInt32(Glitch));
		PlayerPrefs.Save();
	}
}
