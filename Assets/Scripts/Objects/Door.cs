using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, Usable {
	public void Use() {
		Application.Quit();
	}
}
