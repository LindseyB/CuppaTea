using UnityEngine;
using System.Collections;

public class ScreenToWorldAlignment : MonoBehaviour {
	[SerializeField] private Vector3 screenPosition = new Vector3(0, 0, 0);

	void Update() {
		transform.position = Camera.main.ViewportToWorldPoint(screenPosition);
	}
}