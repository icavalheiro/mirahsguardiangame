using UnityEngine;
using System.Collections;

public class Distraction : MonoBehaviour 
{
	void OnTriggerEnter(Collider p_other)
	{
		Transform __root = p_other.transform.root;

		if(__root.tag != "Mirah")
			return;

		Mirah __mirahScript = __root.gameObject.GetComponent<Mirah>();
		__mirahScript.SetDistraction(this.transform.position);
	}
}
