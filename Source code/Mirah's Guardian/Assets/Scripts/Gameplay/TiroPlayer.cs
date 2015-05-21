using UnityEngine;
using System.Collections;

public class TiroPlayer : MonoBehaviour {

	//public Vector3 Ponto = (0,camera.main.nearClipPlane,0);

	
	void Start () {
		//Vector3 mouse_pos_on_world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//RaycastHit raycast_hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
		//if (raycast_hit)
		//	transform.position = Vector3.MoveTowards(transform.position, mouse_pos_on_world, 2* Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(3 * Vector3.forward * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy")
		{
			Destroy(other.gameObject);
		}
	}
}
