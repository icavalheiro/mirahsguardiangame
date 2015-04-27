using UnityEngine;
using System.Collections;

public class BarraDeVida : MonoBehaviour {

	void Start () {

	}
	
	void Update () {
		transform.localScale = new Vector3(GetComponentInParent<Character>().healthPoints/3, 1, 0);
	}
}