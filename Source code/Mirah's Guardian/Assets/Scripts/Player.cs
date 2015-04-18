using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//Variaveis

	public int HP = 3;
	public float vel = 1; //Velocidade do player
	public bool pausado = false;
	public CanvasGroup MenuJogo;//Usado para fazer as interaçoes dos menus
	public CanvasGroup MenuPausa;//-----/\

	void Start () {
	
	}
	
	void Update () {
		//Movimentaçao----------------------------------
		if(Input.GetKey("w"))
			transform.Translate(0,vel*Time.deltaTime,0);
		if(Input.GetKey("s"))
			transform.Translate(0,-vel*Time.deltaTime,0);
		if(Input.GetKey("a"))
			transform.Translate(-vel*Time.deltaTime,0,0);
		if(Input.GetKey("d"))
			transform.Translate(vel*Time.deltaTime,0,0);
		//-----------------------------------------------
		//Abre menu de pausa-------------------------------
		if(Input.GetKeyDown("escape"))
		{
			switch(pausado)
			{
				case false://Se o jogo nao estiver pausado, agora ele esta
					MenuJogo.alpha = 0;
					MenuPausa.alpha = 1;
					MenuPausa.blocksRaycasts = true;
					MenuPausa.interactable = true;
					Time.timeScale = 0; //Pausa o jogo
					pausado = true;
					break;
				case true: //Contrario de cima
					MenuJogo.alpha = 1;
					MenuPausa.alpha = 0;
					MenuPausa.blocksRaycasts = false;
					MenuPausa.interactable = false;
					Time.timeScale = 1;
					pausado = false;
					break;
			}
		}
		//-----------------------------------------------
	}

	public void CliqueMenu () { //Usada quando se clica no botao RetornarJogo
		MenuJogo.alpha = 1;
		MenuPausa.alpha = 0;
		MenuPausa.blocksRaycasts = false;
		MenuPausa.interactable = false;
		Time.timeScale = 1;
		pausado = false;
	}
}
