using UnityEngine;
using System.Collections;

public class Musics : MonoBehaviour {

	public AudioSource ostMenu; //Setar na aba Inspector
	public AudioSource ostInGame;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		if(Application.loadedLevel < 4)
		{
			ostMenu.Play();
			//ostMenu.loop() = true;
		}
		else
		{
			ostMenu.Pause();
			ostInGame.Play();
			//ostInGame.loop() = true;
		}
	}
	
	// Update is called once per frame
	void Update () {


	}
}
