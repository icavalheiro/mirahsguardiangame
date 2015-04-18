//Codigo para trocar as cenas atraves de um botao :P

using UnityEngine;
using System.Collections;

public class BotaoCena : MonoBehaviour {
	
	//Variaveis
	
	public string nomeDaCena; //Setar na aba Inspector.
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public void Clique () { //Funçao que eh chamada quando se clica em um botao.
		Application.LoadLevel(nomeDaCena); //Troca a cena para a que foi carregada na aba Inspector.
	}
}
