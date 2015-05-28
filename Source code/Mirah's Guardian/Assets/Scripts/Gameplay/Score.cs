using UnityEngine;
using System.Collections;
using UnityEngine.UI; //include necessario para usar variaveis que interferam na UI.

public class Score : MonoBehaviour {

	public Text score;
	public GameObject GameManager;
	public int numero; //Nao me diga!

	void Start () {
		score = GetComponent<Text>();
	}
	
	void Update () {
		numero = GameManager.GetComponent<GameplayManager>().guardian.municao;
		score.text = "Ammo " + numero;
	}
}
