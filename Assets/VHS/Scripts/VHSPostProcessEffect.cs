using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class VHSPostProcessEffect : MonoBehaviour {
	[SerializeField] public AudioSource rewindAudio;

	Material m;
	public Shader shader;
	public MovieTexture VHS;

	float yScanline, xScanline;

	void Start() {
		m = new Material(shader);
		m.SetTexture("_VHSTex", VHS);
		VHS.loop = true;
		VHS.Play();
	}

	void Update() {
		if(enabled && !rewindAudio.isPlaying){ 
			rewindAudio.Play(); 
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination){
		yScanline += Time.deltaTime * 0.8f;
		xScanline -= Time.deltaTime * 0.8f;

		if(yScanline >= 1){
			yScanline = Random.value;
		}
		if(xScanline <= 0 || Random.value < 0.05){
			xScanline = Random.value;
		}
		m.SetFloat("_yScanline", yScanline);
		m.SetFloat("_xScanline", xScanline);
		Graphics.Blit(source, destination, m);
	}
}