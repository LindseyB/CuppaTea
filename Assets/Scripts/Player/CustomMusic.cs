using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CustomMusic : MonoBehaviour {
	private List<AudioClip> audioClips;
	private AudioSource audioSource;
	private static string musicDir;


	void Start () {
		musicDir = Application.persistentDataPath + "/Music";
		if(!Directory.Exists(musicDir)){  Directory.CreateDirectory(musicDir); }

		audioSource = gameObject.GetComponent<AudioSource>() as AudioSource;
		audioClips = new List<AudioClip>();

		StartCoroutine("PlayAudioList");
	}

	IEnumerator DownloadPlaylist() {
		foreach(string file in Directory.GetFiles(musicDir, "*.ogg", SearchOption.TopDirectoryOnly)){
			WWW audioLoader = new WWW("file://" + file);
			while( !audioLoader.isDone ){ yield return null; }
			audioClips.Add(audioLoader.GetAudioClip(false));
		}    
	}

	IEnumerator PlayAudioList() { 
		yield return StartCoroutine("DownloadPlaylist");

		while(true){
			foreach(AudioClip file in audioClips){
				audioSource.clip = file;
				audioSource.Play();
				yield return new WaitForSeconds(file.length + 1.0f);
			}
		}  
	}
}