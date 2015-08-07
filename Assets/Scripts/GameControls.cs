using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControls : MonoBehaviour {
	public enum Controls {
		Use = KeyCode.F,
		Forward = KeyCode.W,
		ForwardArrow = KeyCode.UpArrow,
		Backwards = KeyCode.S,
		BackwardsArrow = KeyCode.DownArrow,
		Left = KeyCode.A,
		LeftArrow = KeyCode.LeftArrow,
		Right = KeyCode.D,
		RightArrow = KeyCode.RightArrow,
		Up = KeyCode.Space,
		RotateLeft = KeyCode.Q,
		RotateForward = KeyCode.E,
		Rage = KeyCode.Period
	}	
}
