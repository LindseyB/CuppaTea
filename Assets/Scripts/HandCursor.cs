using UnityEngine;
using System.Collections;

public class HandCursor : MonoBehaviour {
	[SerializeField] private Texture2D customCursor;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}

	void OnGUI () {
		GUI.DrawTexture(new Rect(
			Input.mousePosition.x - customCursor.width/2,
			Screen.height - Input.mousePosition.y - customCursor.height/2,
			customCursor.width,
			customCursor.height), 
		    customCursor, 
		    ScaleMode.ScaleToFit, 
		    true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
