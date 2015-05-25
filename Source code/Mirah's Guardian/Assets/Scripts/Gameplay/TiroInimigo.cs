using UnityEngine;
using System.Collections;

public class TiroInimigo : MonoBehaviour {
	
	//public Vector3 Ponto = (0,camera.main.nearClipPlane,0);
	public GameObject Player;	
	
	void Start () {
		transform.LookAt(GameplayManager.GetMirah().transform.position + (Vector3.up * 0.2f));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(2 * Vector3.forward * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.transform.root.tag == "Player")
		{

			other.transform.root.gameObject.GetComponent<Guardian>().SetDamage(1);
		//	Debug.Log ("Acertou!");
			GameObject.Destroy(this.gameObject);
		}
		if(other.transform.tag == "Shield")
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}