using UnityEngine;
using System.Collections;

public class HandCursor : MonoBehaviour {
	[SerializeField] private Texture2D openCursor;
	[SerializeField] private Texture2D closedCursor;

	private Texture2D customCursor;
	
	void Start () {
		customCursor = openCursor;
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

	public void ToggleCursor() {
		if (customCursor == openCursor) {
			customCursor = closedCursor;
		} else {
			customCursor = openCursor;
		}
	}
}
