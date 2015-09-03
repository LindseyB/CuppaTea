using UnityEngine;
using System.Collections;

public class EndDoor : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Application.LoadLevel(1);
	}
}