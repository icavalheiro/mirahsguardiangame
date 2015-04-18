using UnityEngine;
using System.Collections;

public class BotaoSom : MonoBehaviour {

	public AudioListener caixaDeSom;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void Clique () {
		//Sistema de On/Off para mutar o audio do jogo.
		switch(caixaDeSom.enabled)
		{
			case true:
				caixaDeSom.enabled = false;
				break;
			case false:
				caixaDeSom.enabled = true;
				break;
		}
		//-------------------------------------------
	}
}