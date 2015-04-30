using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Nao esquecer de sempre dar esse include.

public class BarraDeVida : MonoBehaviour {

	public Image image;

	void Start () {
		image = GetComponent<Image>();
	}
	
	void Update () {
		transform.localScale = new Vector3(GetComponentInParent<Character>().healthPoints/3, 1, 0);
		image.color = Color.Lerp(Color.red, Color.green,Mathf.Lerp(0,1,transform.localScale.x));
	}
}