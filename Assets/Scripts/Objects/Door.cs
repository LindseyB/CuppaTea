using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, Usable {
	void Update() {
		if (Input.GetKey(KeyCode.Escape)) {
			Exit();
		}
	}

	public void Use() {
		Exit();
	}

	private void Exit() {
		if (!Application.isEditor) System.Diagnostics.Process.GetCurrentProcess().Kill();
	}
}
