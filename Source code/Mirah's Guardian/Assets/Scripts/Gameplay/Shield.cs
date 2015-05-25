using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	//public float timer = 0;

	void Start () {
	
	}
	
	void Update () {
		if(Input.GetButtonUp("Fire2"))
			GameObject.Destroy(this.gameObject);
	}
	/*
	void OnTriggerEnter(Collider other) {
		if(other.transform.root.tag == "EnemyBullet")
		{
			GameObject.Destroy(other.gameObject);
		}
	}
	*/
}