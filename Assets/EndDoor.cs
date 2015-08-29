using UnityEngine;
using System.Collections;

public class EndDoor : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Debug.Log("Walked through door");
		//Application.LoadLevel(0);
	}
}