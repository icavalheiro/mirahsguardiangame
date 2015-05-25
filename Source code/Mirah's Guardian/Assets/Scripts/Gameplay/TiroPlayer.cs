using UnityEngine;
using System.Collections;

public class TiroPlayer : MonoBehaviour {

	//public Vector3 Ponto = (0,camera.main.nearClipPlane,0);

	
	void Start () {
		//Vector3 mouse_pos_on_world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//RaycastHit raycast_hit = Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
		//if (raycast_hit)
		//	transform.position = Vector3.MoveTowards(transform.position, mouse_pos_on_world, 2* Time.deltaTime);

		Ray __ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		//calculo para descobrir onde a linha passa pelo plano y=0, (no chao)
		Vector3 __updatedPosition = __ray.origin + (((-__ray.origin.y)/__ray.direction.y) * __ray.direction);

		this.transform.LookAt(__updatedPosition + (Vector3.up * 0.2f));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(3 * Vector3.forward * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy")
		{
			Destroy(other.gameObject);
			GameObject.Destroy(this.gameObject);
		}
	}
}
