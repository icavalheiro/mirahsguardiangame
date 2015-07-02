using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource ostInGame;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		if(Application.loadedLevel > 3)
		{
			ostInGame.Play();
			//ostMenu.loop() = true;
		}
	}
	
	void Update () {
	
	}
}
