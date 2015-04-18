//Usado no objeto Contador na cena Logotipos, para trocar de cena depois que tiver passado um numero x de tempo.

using UnityEngine;
using System.Collections;

public class Logotipos : MonoBehaviour {

	//Variaveis

	public float tempo = 0;

	void Start () {
	
	}
	
	void Update () {
		tempo += Time.deltaTime;
		if(tempo >= 3) //Se tempo for maior ou igual a 3 :P
			Application.LoadLevel("Titulo"); //Carrega a tela de titulo.
	}
}
