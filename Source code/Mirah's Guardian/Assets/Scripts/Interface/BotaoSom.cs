using UnityEngine;
using System.Collections;

public class BotaoSom : MonoBehaviour {

	//public AudioListener caixaDeSom;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void Clique () {
		//Sistema de On/Off para mutar o audio do jogo.
		switch(GameObject.Find("AudioManager").GetComponent<AudioListener>().enabled)
		{
			case true:
				GameObject.Find("AudioManager").GetComponent<AudioListener>().enabled = false;
				//caixaDeSom.enabled = false;
				break;
			case false:
				GameObject.Find("AudioManager").GetComponent<AudioListener>().enabled = true;
				//caixaDeSom.enabled = true;
				break;
		}
		//-------------------------------------------
	}
}