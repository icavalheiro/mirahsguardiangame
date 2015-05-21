using UnityEngine;
using System.Collections;

public class TiroInimigo : MonoBehaviour {
	
	//public Vector3 Ponto = (0,camera.main.nearClipPlane,0);
	public GameObject Player;	
	
	void Start () {
		transform.LookAt(Player.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(2 * Vector3.forward * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player")
		{
			//Nao deu tempo pra arrumar
			//Player.GetComponent<Character>().healthPoints
		}
	}
}